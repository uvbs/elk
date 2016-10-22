namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class HttpsByTemporaryAndPermanentHttpRedirectsMix : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Https used by \"Temporary redirects\" (302, 307)";
    private string description = "The server uses \"Temporary redirects\" (302, 307) to lead the user to the HTTPS server.";
    private string consequences = "By redirecting the HTTP connection through the attackers HTTP(S) reverse proxy somewhere in the " +
                          "plain text communication the reverse proxy server must do the final request to the HTTPS server in place of the client.";
    private string setup = "MITM, DNS poisoning, HTTP reverse proxy, Sniffing";
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

    public HttpsByTemporaryAndPermanentHttpRedirectsMix()
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
      int numberOfRedirectElements = scannerData.ResponseEntityChain.Count - 1;
      if (numberOfRedirectElements > 0)
      {
        List<ServerResponseEntity> temporarilyRedirectedElements = new List<ServerResponseEntity>();
        List<ServerResponseEntity> permanentlyRedirectedElements = new List<ServerResponseEntity>();
        
        for (int i = 0; i < numberOfRedirectElements; i++)
        {
          if (scannerData.ResponseEntityChain[i].RequestedScheme == "http" &&
              scannerData.ResponseEntityChain[i].ResponseStatusCode == 301)
          {
            permanentlyRedirectedElements.Add(scannerData.ResponseEntityChain[i]);
          }
          else if (scannerData.ResponseEntityChain[i].RequestedScheme == "http" &&
                   (scannerData.ResponseEntityChain[i].ResponseStatusCode == 302 ||
                    scannerData.ResponseEntityChain[i].ResponseStatusCode == 303 ||
                    scannerData.ResponseEntityChain[i].ResponseStatusCode == 307))
          {
            temporarilyRedirectedElements.Add(scannerData.ResponseEntityChain[i]);
          }
        }

        // Check if system chain is vulnerable
        if (lastItem.HttpsPortOpen == true &&
            lastItem.RequestedScheme.ToLower() == "https" && 
            temporarilyRedirectedElements.Count > 0 &&
            permanentlyRedirectedElements.Count > 0 &&
            numberOfRedirectElements == (temporarilyRedirectedElements.Count + permanentlyRedirectedElements.Count))
        {
          isVulnerable = true;
        }
      }

      return isVulnerable;
    }

    #endregion

  }
}
