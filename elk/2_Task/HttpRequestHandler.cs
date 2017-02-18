namespace Elk.Task
{
  using System;
  using System.Collections.Generic;
  using System.Text;
  using System.Net;
  using System.IO;


  public class Connection
  {
    public HttpWebRequest Request;
    public HttpWebResponse Response;
  }


  public class HttpRequestHandler
  {

    #region MEMBERS

    CookieContainer cookies = new CookieContainer();

    #endregion


    #region PROPERTIES

    public string LastResponse { protected set; get; }

    internal string GetCookieValue(Uri SiteUri, string name)
    {
      Cookie cookie = cookies.GetCookies(SiteUri)[name];
      return (cookie == null) ? null : cookie.Value;
    }

    #endregion


    #region PUBLIC 

    public string GetResponseContent(HttpWebResponse response)
    {
      if (response == null)
      {
        throw new ArgumentNullException("response");
      }

      Stream dataStream = null;
      StreamReader reader = null;
      string responseFromServer = null;

      try
      {
        dataStream = response.GetResponseStream();
        reader = new StreamReader(dataStream);
        responseFromServer = reader.ReadToEnd();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        if (reader != null)
        {
          reader.Close();
        }
        if (dataStream != null)
        {
          dataStream.Close();
        }
        response.Close();
      }
      LastResponse = responseFromServer;
      return responseFromServer;
    }


    public Connection SendPOSTRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string userAgent, List<string> headers)
    {
      Connection connection = new Connection();
      connection.Request = GeneratePOSTRequest(uri, content, login, password, allowAutoRedirect, userAgent, headers);
      connection.Response = GetResponse(connection.Request);

      return connection;
    }


    public Connection SendGETRequest(string uri, string login, string password, bool allowAutoRedirect, string userAgent, List<string> headers)
    {
      Connection connection = new Connection();
      connection.Request = this.GenerateGETRequest(uri, login, password, allowAutoRedirect, userAgent, headers);
      connection.Response = this.GetResponse(connection.Request);

      return connection;
    }


    public Connection SendRequest(string uri, string content, string method, string login, string password, bool allowAutoRedirect, string userAgent, List<string> headers)
    {
      Connection connection = new Connection();
      connection.Request = this.GenerateRequest(uri, content, method, login, password, allowAutoRedirect, userAgent, headers);
      connection.Response = this.GetResponse(connection.Request);

      return connection;
    }


    public HttpWebRequest GenerateGETRequest(string uri, string login, string password, bool allowAutoRedirect, string userAgent, List<string> headers)
    {
      return this.GenerateRequest(uri, null, "GET", null, null, allowAutoRedirect, userAgent, headers);
    }


    public HttpWebRequest GeneratePOSTRequest(string uri, string content, string login, string password, bool allowAutoRedirect, string userAgent, List<string> headers)
    {
      return this.GenerateRequest(uri, content, "POST", null, null, allowAutoRedirect, userAgent, headers);
    }


    public Dictionary<string, string> GetResponseHeaders(HttpWebResponse responseHandle)
    {
      Dictionary<string, string> headers = new Dictionary<string, string>();

      if (responseHandle == null || responseHandle.Headers == null || responseHandle.Headers.Count <= 0)
      {
        return headers;
      }

      foreach (string tmpHeader in responseHandle.Headers.Keys)
      {
        headers.Add(tmpHeader, responseHandle.Headers[tmpHeader]);
      }

      return headers;
    }

    public string GetResponseBody(HttpWebResponse responseHandle)
    {
      byte[] buffer = new byte[1024];
      int bytesRead = -1;
      StringBuilder responseBody = new StringBuilder();
      
      if (responseHandle == null || responseHandle.GetResponseStream() == null)
      {
        return responseBody.ToString();
      }
      
      while ((bytesRead = responseHandle.GetResponseStream().Read(buffer, 0, buffer.Length)) > 0)
      {
        responseBody.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
      }

      return responseBody.ToString();
    }

    #endregion


    #region PRIVATE/INTERNAL

    internal HttpWebRequest GenerateRequest(string uri, string content, string method, string login, string password, bool allowAutoRedirect, string userAgent, List<string> headers)
    {
      if (uri == null)
      {
        throw new ArgumentNullException("uri");
      }

      // Create a request using a URL that can receive a post. 
      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
      request.Method = method;
      request.CookieContainer = cookies;
      request.AllowAutoRedirect = allowAutoRedirect;
      request.UserAgent = userAgent;

      // If login is empty use defaul credentials
      if (string.IsNullOrEmpty(login))
      {
        request.Credentials = CredentialCache.DefaultNetworkCredentials;
      }
      else
      {
        request.Credentials = new NetworkCredential(login, password);
      }

      if (headers != null && headers.Count > 0)
      {
        headers.ForEach(elem => request.Headers.Add(elem));
      }


      if (method == "POST")
      {
        // Convert POST data to a byte array.
        byte[] byteArray = Encoding.UTF8.GetBytes(content);
        // Set the ContentType property of the WebRequest.
        request.ContentType = "application/x-www-form-urlencoded";
        // Set the ContentLength property of the WebRequest.
        request.ContentLength = byteArray.Length;
        // Get the request stream.
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.
        dataStream.Close();
      }

      return request;
    }

    internal HttpWebResponse GetResponse(HttpWebRequest request)
    {
      if (request == null)
      {
        throw new ArgumentNullException("request");
      }

      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      
      return response;
    }

    #endregion

  }
}
