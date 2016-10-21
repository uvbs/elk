namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class HttpsByHstsAndNoSubdomains : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Target system is protected by HSTS";
    private string description = "The target system is protected by HSTS and cannot be intercepted because the client requests the server with HTTPS by default." +
                         "If HSTS did not set the subdomain feature the attacker can inject a redirect to a subdomain what can give him access to sensitive " +
                         "header data.";
    private string concequences = "By having access to the header data the attacker can see sensitive data like cookies (session information).";
    private string setup = "MITM, DNS poisoning, HTTP reverse proxy, sniffing";
    private List<VulnerableElementAndConsequences> vulnElementAndConsequences = new List<VulnerableElementAndConsequences>();

    #endregion


    #region PROPERTIES

    public string Title { get { return this.title; } }

    public string Description { get { return this.description; } }

    public string Concequences { get { return this.concequences; } }

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


      return isVulnerable;
    }

    #endregion
  }
}
