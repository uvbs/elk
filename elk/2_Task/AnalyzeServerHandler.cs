namespace Elk.Task
{
  using Elk.DataTypes;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Net;
  using System.Text;


  public class AnalyzeServerHandler : IObservableHostChain
  {

    #region MEMBERS

    private HttpRequestHandler requestHandler;
    private List<ServerResponseEntity> responseEntityChain;
    private List<IObserverHostChain> observersHostChain;
    private static Dictionary<string, ServerResponseEntity> entityCache = new Dictionary<string, ServerResponseEntity>();
    private TcpPortChecker portScanner;

    #endregion


    #region PROPERTIES

    public static Dictionary<string, ServerResponseEntity> EntityCache { get { return entityCache; } set { entityCache = value; } }

    #endregion


    #region PUBLIC

    public AnalyzeServerHandler()
    {
      this.requestHandler = new HttpRequestHandler();
      this.responseEntityChain = new List<ServerResponseEntity>();
      this.observersHostChain = new List<IObserverHostChain>();
      this.portScanner = new TcpPortChecker();
    }


    public List<ServerResponseEntity> DetermineHostsChain(string requestedUri, string userAgent)
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
        catch (Exception ex)
        {
          throw new Exception(string.Format("The URL is invalid: {0}", ex.Message));
        }

        redirected = false;
        string cacheKey = string.Format("{0}/{1}", userAgent, requestedUri);
        if (entityCache.ContainsKey(cacheKey))
        {
          response = entityCache[cacheKey];
        }
        else
        {
          Uri uri = new Uri(requestedUri);

          // Determine IP address
          string ipAddresses = this.GetIpAddresse(uri.Host);

          // Verify if http(s) port is open
          bool httpPortOpen = this.portScanner.IsPortOpen(uri.Host, 80);
          bool httpsPortOpen = this.portScanner.IsPortOpen(uri.Host, 443);
          
          // Send web request to server
          webResponse = this.requestHandler.SendGETRequest(requestedUri, string.Empty, string.Empty, false, userAgent);

          response = this.ProcessServerResponse(uri.Scheme, uri.Host, uri.PathAndQuery, webResponse, userAgent);
          response.HttpPortOpen = httpPortOpen;
          response.HttpsPortOpen = httpsPortOpen;
          response.IpAddresses = ipAddresses;
        }

        this.responseEntityChain.Add(response);

        // Initialize values for next round
        if (response != null && !string.IsNullOrEmpty(response.RedirectLocation))
        {
          if (!response.RedirectLocation.StartsWith("http"))
          {
            response.RedirectLocation = string.Format("{0}://{1}{2}", response.RequestedScheme, response.RequestedHost, response.RedirectLocation);
          }

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
    private ServerResponseEntity ProcessServerResponse(string requestedScheme, string requestedHost, string requestedPath, HttpWebResponse webResponse, string userAgent)
    {
      ServerResponseEntity serverResponse = new ServerResponseEntity();
      Dictionary<string, string> headers = new Dictionary<string, string>();
      string responseBody = string.Empty;
      string responseHeaders = string.Empty;
      string rawHeaders = string.Empty;
      string cacheKey = string.Format("{0}/{1}://{2}{3}", userAgent, requestedScheme, requestedHost, requestedPath);
      
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
      serverResponse.ResponseStatusCode = (int)webResponse.StatusCode;
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

      if (headers.ContainsKey("content-type") &&
        headers["content-type"].ToLower().StartsWith("text"))
      {
        StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
        serverResponse.Payload = reader.ReadToEnd();
        reader.Close();
      }

      entityCache.Add(cacheKey, serverResponse);

      return serverResponse;
    }


    private string GetIpAddresse(string hostName)
    {
      string ipAddresses = string.Empty;

      try
      {
        IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

        foreach (var elem in hostEntry.AddressList)
        {
          ipAddresses += string.Format("{0}, ", elem);
        }

        ipAddresses = ipAddresses.TrimEnd(new char[] { ',', ' ' });
      }
      catch (Exception)
      {
      }

      return ipAddresses;
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
