namespace Elk.DataTypes
{
  using System.Net;


  public class ServerResponseEntity
  {

    #region MEMBERS

    private string rawHeaders;
    private string requestedScheme;
    private string requestedHost;
    private string requestedPath;
    private string responseStatus;
    private int responseStatusCode;
    private string hsts;
    private string hpkp;
    private string hpkpReport;
    private string redirectLocation;
    private CookieCollection cookies;
    private string server;
    private string ipAddresses;
    private bool httpPortOpen;
    private bool httpsPortOpen;

    #endregion


    #region PROPERTIES

    public string RawHeaders { get { return this.rawHeaders; } set { this.rawHeaders = value; } }

    public string RequestedScheme { get { return this.requestedScheme; } set { this.requestedScheme = value; } }

    public string RequestedHost { get { return this.requestedHost; } set { this.requestedHost = value; } }

    public string RequestedPath { get { return this.requestedPath; } set { this.requestedPath = value; } }

    public string RequestedUrl { get { return string.Format("{0}://{1}{2}", this.requestedScheme, this.requestedHost, this.requestedPath); } }
    
    public string ResponseStatus { get { return this.responseStatus; } set { this.responseStatus = value; } }

    public int ResponseStatusCode { get { return this.responseStatusCode; } set { this.responseStatusCode = value; } }
    
    public string Hsts { get { return this.hsts; } set { this.hsts = value; } }

    public string Hpkp { get { return this.hpkp; } set { this.hpkp = value; } }

    public string HpkpReport { get { return this.hpkpReport; } set { this.hpkpReport = value; } }

    public string RedirectLocation { get { return this.redirectLocation; } set { this.redirectLocation = value; } }

    public string CookiesString { get {
        string cookieString = string.Empty;

        for (int i = 0; i < cookies.Count; i++)
        {
          cookieString += string.Format("{0}: ToString={1}, Name={2}, Value={3}, Domain={4}, HttpOnly={5}, Path={6}, Secure={7}, Version={8}\r\n", i,
          cookies[i].ToString(),
          cookies[i].Name,
          cookies[i].Value,
          cookies[i].Domain,
          cookies[i].HttpOnly,
          cookies[i].Path,
          cookies[i].Secure,
          cookies[i].Version);
        }

        return cookieString; }  }

    public CookieCollection Cookies { get { return this.cookies; } set { this.cookies = value; } }

    public string Server { get { return this.server; } set { this.server = value; } }

    public bool HttpPortOpen { get { return this.httpPortOpen; } set { this.httpPortOpen = value; } }

    public bool HttpsPortOpen { get { return this.httpsPortOpen; } set { this.httpsPortOpen = value; } }
    
    public string IpAddresses { get { return this.ipAddresses; } set { this.ipAddresses = value; } }
    
    #endregion


    #region PUBLIC

    public ServerResponseEntity()
    {
      this.rawHeaders = string.Empty;
      this.requestedScheme = string.Empty;
      this.requestedHost = string.Empty;
      this.requestedPath = string.Empty;
      this.responseStatus = string.Empty;
      this.hsts = string.Empty;
      this.hpkp = string.Empty;
      this.hpkpReport = string.Empty;
      this.redirectLocation = string.Empty;
      this.cookies = new CookieCollection();
    }

    #endregion

  }
}
