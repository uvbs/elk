namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class NoHttpsAvailable : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Http only server";
    private string description = "The target server does not support HTTPS.";
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

    public NoHttpsAvailable()
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

      if (scannerData == null || 
          scannerData.ResponseEntityChain == null || 
          scannerData.ResponseEntityChain.Count <= 0)
      {
        return isVulnerable;
      }

      var lastItem = scannerData.ResponseEntityChain[scannerData.ResponseEntityChain.Count - 1];
      if (lastItem.HttpPortOpen == true && 
          lastItem.HttpsPortOpen == false &&
          lastItem.RequestedScheme.ToLower() == "http")
      {
        isVulnerable = true;
      }

      return isVulnerable;
    }

    #endregion

  }
}
