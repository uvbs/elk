﻿namespace Elk.Vulnerabilities
{
  using Elk.DataTypes;
  using Elk.DataTypes.Enumerations;
  using Elk.DataTypes.Interfaces;
  using System.Collections.Generic;


  public class HttpsByHstsAndSubdomains : IVulnerabilityDefinition
  {

    #region MEMBERS

    private string title = "Chain elements and their sub-domains to target protected by HSTS";
    private string description = "The whole system chain including all subdomains to the target system is protected by HSTS and cannot be intercepted because client requests to any element of the chain will start with HTTPS by default. " +
                                 "Therefore the server connection can't be attacked except by client vulnerabilities.";
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

    public HttpsByHstsAndSubdomains()
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

      // Check if system chain is vulnerable
      var lastItem = scannerData.ResponseEntityChain[scannerData.ResponseEntityChain.Count - 1];
      if (lastItem.HttpsPortOpen == true &&
          lastItem.RequestedScheme.ToLower() == "https" &&
          !string.IsNullOrEmpty(lastItem.Hsts) &&
          lastItem.Hsts.ToLower().Contains("max-age=") &&
          lastItem.Hsts.ToLower().Contains("includesubdomains "))
      {
        isVulnerable = true;
      }

      return isVulnerable;
    }

    #endregion

  }
}
