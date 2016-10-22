namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class HttpsByPermanentRedirectsOnly : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Https used by \"Permanent redirects\" (301, 308)";
    private string description = "The server connection can't be attacked except by client vulnerabilities.";
    private string consequences = "-";
    private string setup = "-";
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

    public HttpsByPermanentRedirectsOnly()
    {
      vulnElementAndConsequences.Add(new VulnerableElementAndConsequences(EnumRequestTriggerSource.None, EnumExposedDataType.None));
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
      int numberOfRedirectElements = scannerData.ResponseEntityChain.Count - 1;
      if (numberOfRedirectElements > 0)
      {
        List<ServerResponseEntity> permanentlyRedirectedElements = new List<ServerResponseEntity>();
        for (int i = 0; i < numberOfRedirectElements; i++)
        {
          if (scannerData.ResponseEntityChain[i].ResponseStatusCode == 301)
          {
            permanentlyRedirectedElements.Add(scannerData.ResponseEntityChain[i]);
          }
        }

        // Check if system chain is vulnerable
        if (lastItem.HttpsPortOpen == true &&
            lastItem.RequestedScheme.ToLower() == "https" &&
            numberOfRedirectElements == permanentlyRedirectedElements.Count)
        {
          isVulnerable = true;
        }
      }

      return isVulnerable;
    }

    #endregion

  }
}
