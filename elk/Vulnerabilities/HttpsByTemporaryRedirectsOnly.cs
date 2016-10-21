namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class HttpsByTemporaryRedirectsOnly : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Https used by \"Temporary redirects\" (302, 307)";
    private string description = "The server uses \"Temporary redirects\" (302, 307) to lead the user to the HTTPS server.";
    private string concequences = "By redirecting the HTTP connection through the attackers HTTP(S) reverse proxy somewhere in the " +
                          "plain text communication the reverse proxy server must do the final request to the HTTPS server in place of the client.";
    private string setup = "MITM, DNS poisoning, HTTP reverse proxy, Sniffing";
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

    public HttpsByTemporaryRedirectsOnly()
    {
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.ClientApplication, EnumExposedDataType.HttpHeader));
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.ClientApplication, EnumExposedDataType.HttpPayload));
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.User, EnumExposedDataType.HttpHeader));
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.User, EnumExposedDataType.HttpPayload));
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
