namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class NoHttpsUsed : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Https not used by server";
    private string description = "The target server supports HTTPS but does not use/redirect to it.";
    private string consequences = "All sensitive user and header data is transferred in clear text and can be intercepted.";
    private string setup = "MITM, Sniffer";
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

    public NoHttpsUsed()
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
