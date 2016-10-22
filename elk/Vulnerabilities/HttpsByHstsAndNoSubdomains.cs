namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class HttpsByHstsAndNoSubdomains : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Target system is protected by HSTS but not sub domains";
    private string description = "The target system is protected by HSTS and cannot be intercepted because the client requests the server with HTTPS by default." +
                         "If HSTS did not set the subdomain feature the attacker can inject a redirect to a subdomain what can give him access to sensitive " +
                         "header data.";
    private string consequences = "By having access to the header data the attacker can see sensitive data like cookies (session information).";
    private string setup = "MITM, DNS poisoning, HTTP reverse proxy, sniffing";
    private List<VulnerableElementAndConsequences> vulnElementAndConsequences = new List<VulnerableElementAndConsequences>();

    #endregion


    #region PROPERTIES

    public string Title { get { return this.title; } }

    public string Description { get { return this.description; } }

    public string Consequences { get { return this.consequences; } }

    public string Setup { get { return this.setup; } }

    public List<VulnerableElementAndConsequences> VulnElementAndConsequences { get { return this.vulnElementAndConsequences; } }

    #endregion


    #region PUBLIC

    public HttpsByHstsAndNoSubdomains()
    {
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.ClientApplication, EnumExposedDataType.HttpHeader));
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.User, EnumExposedDataType.HttpHeader));
    }

    #endregion


    #region INTERFACE: IVulnerabilityDefinition

    public bool IsTargetVulnerable(DataForVulnerabilityScanner scannerData)
    {
      bool isVulnerable = false;

      if (scannerData == null ||
          scannerData.ResponseEntityChain == null ||
          scannerData.ResponseEntityChain.Count <= 0)
      {
        return isVulnerable;
      }

      // Check if system chain is vulnerable
      var lastItem = scannerData.ResponseEntityChain[scannerData.ResponseEntityChain.Count - 1];
      if (lastItem.HttpsPortOpen == true &&
          lastItem.RequestedScheme.ToLower() == "https" &&
          !string.IsNullOrEmpty(lastItem.Hsts) &&
          lastItem.Hsts.ToLower().Contains("max-age=") &&
          !lastItem.Hsts.ToLower().Contains("includesubdomains "))
      {
        isVulnerable = true;
      }

      return isVulnerable;
    }

    #endregion

  }
}
