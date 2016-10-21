namespace Elk.Task
{
  using Elk.DataTypes;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Net;


  public class AnalyzeServerHandler : IObservableHostChain
  {

    #region MEMBERS

    private HttpRequestHandler requestHandler;
    private List<ServerResponseEntity> responseEntityChain;
    private List<IObserverHostChain> observersHostChain;
    private Dictionary<string, ServerResponseEntity> entityCache;

    #endregion


    #region PUBLIC

    public AnalyzeServerHandler()
    {
      this.requestHandler = new HttpRequestHandler();
      this.responseEntityChain = new List<ServerResponseEntity>();
      this.observersHostChain = new List<IObserverHostChain>();
      this.entityCache = new Dictionary<string, ServerResponseEntity>();
    }


    public List<ServerResponseEntity> DetermineHostsChain(string requestedUri)
    {
      ServerResponseEntity response = null;
      HttpWebResponse webResponse = null;
      bool redirected = false;
      Uri tmpUri = null;

      this.responseEntityChain.Clear();

      do
      {
        // Parse 
        try
        {
          tmpUri = new Uri(requestedUri);
          requestedUri = tmpUri.AbsoluteUri;
        }
        catch (Exception)
        {
          throw new Exception("The URL is invalid");
        }

        redirected = false;
        if (this.entityCache.ContainsKey(requestedUri))
        {
          response = this.entityCache[requestedUri];
        }
        else
        {
          webResponse = this.requestHandler.SendGETRequest(requestedUri, string.Empty, string.Empty, false);

          Uri uri = new Uri(requestedUri);
          response = this.ProcessServerResponse(uri.Scheme, uri.Host, uri.PathAndQuery, webResponse);
        }

        this.responseEntityChain.Add(response);

        // Initialize values for next round
        if (response != null && !string.IsNullOrEmpty(response.RedirectLocation))
        {
          requestedUri = response.RedirectLocation;
          redirected = true;
          webResponse = null;
        }
      }
      while (redirected);

      this.NotifyHostChain(this.responseEntityChain);

      return this.responseEntityChain;
    }

    #endregion


    #region PRIVATE

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webResponse"></param>
    /// <returns></returns>
    private ServerResponseEntity ProcessServerResponse(string requestedScheme, string requestedHost, string requestedPath, HttpWebResponse webResponse)
    {
      ServerResponseEntity serverResponse = new ServerResponseEntity();
      Dictionary<string, string> headers = new Dictionary<string, string>();
      string responseBody = string.Empty;
      string responseHeaders = string.Empty;
      string rawHeaders = string.Empty;
      string cacheKey = string.Format("{0}://{1}{2}", requestedScheme, requestedHost, requestedPath);
      
      if (webResponse == null || webResponse.Headers == null)
      {
        return serverResponse;
      }

      foreach (string tmpKey in webResponse.Headers.Keys)
      {
        headers.Add(tmpKey.ToLower(), webResponse.Headers[tmpKey]);
        rawHeaders += string.Format("{0}: {1}{2}", tmpKey, webResponse.Headers[tmpKey], Environment.NewLine);
      }

      serverResponse.RequestedScheme = requestedScheme;
      serverResponse.RequestedHost = requestedHost;
      serverResponse.RequestedPath = requestedPath;
      serverResponse.ResponseStatus = string.Format("{0,4}  {1}", (int)webResponse.StatusCode, webResponse.StatusDescription);
      serverResponse.RawHeaders = rawHeaders;

      if (headers.ContainsKey("server"))
      {
        serverResponse.Server = headers["server"];
      }

      if (headers.ContainsKey("location"))
      {
        serverResponse.RedirectLocation = headers["location"];
      }

      if (webResponse.Cookies != null)
      {
        serverResponse.Cookies = webResponse.Cookies;
      }

      if (headers.ContainsKey("strict-transport-security"))
      {
        serverResponse.Hsts = headers["strict-transport-security"];
      }

      if (headers.ContainsKey("public-key-pins"))
      {
        serverResponse.Hpkp = headers["public-key-pins"];
      }

      if (headers.ContainsKey("public-key-pins-report-only"))
      {
        serverResponse.HpkpReport = headers["public-key-pins-report-only"];
      }

      this.entityCache.Add(cacheKey, serverResponse);
      return serverResponse;
    }

    #endregion


    #region INTERFACE: IOservableHostChain

    public void AddObserverHostChain(IObserverHostChain newObserver)
    {
      this.observersHostChain.Add(newObserver);
    }

    public void NotifyHostChain(List<ServerResponseEntity> logMessage)
    {
      if (this.observersHostChain == null || this.observersHostChain.Count <= 0)
      {
        return;
      }

      if (logMessage == null || logMessage.Count <= 0)
      {
        return;
      }

      foreach (IObserverHostChain observer in this.observersHostChain)
      {
        observer.UpdateHostChain(this.responseEntityChain);
      }
    }

    #endregion

  }
}
