namespace Elk.Task
{
  using Elk.DataTypes.Dgv;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using HAP = HtmlAgilityPack;


  public class PageLinksToHandler : IObservablePageLinksTo
  {

    #region MEMBERS

    List<IObserverPageLinksTo> observerList = new List<IObserverPageLinksTo>();

    #endregion


    #region PUBLIC

    public void DetermineHyperLinks(string url, string userAgent)
    {
      List<HyperLinkRecord> hyperLinkList = new List<HyperLinkRecord>();
      string cacheKey = string.Format("{0}/{1}", userAgent, url);
      Uri hyperLinkBasisUri;

      if (Uri.TryCreate(url, UriKind.Absolute, out hyperLinkBasisUri) == false)
      {
        return;
      }

      if (HostChainHandler.EntityCache.ContainsKey(cacheKey) == false)
      {
        return;
      }

      DataTypes.ServerResponseEntity dataRecord = HostChainHandler.EntityCache[cacheKey];
      var doc = new HAP.HtmlDocument();
      string htmlCode = dataRecord.Payload.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
      doc.LoadHtml(htmlCode);
      var root = doc.DocumentNode;

      var foundNodes = root.Descendants("a").ToList();
      if (foundNodes == null || foundNodes.Count <= 0)
      {
        return;
      }

      foreach (var tmpNode in foundNodes)
      {
        string link = tmpNode.GetAttributeValue("href", string.Empty);
        Uri tmpUri;

        if (Uri.TryCreate(link, UriKind.Absolute, out tmpUri) == true)
        {
          if (tmpUri.Host != hyperLinkBasisUri.Host &&
              tmpUri.Scheme != "//" &&
              tmpUri.Scheme.ToLower() != "file")              
          {
            HyperLinkRecord newHyperLink = new HyperLinkRecord(tmpUri.Scheme, tmpUri.Host, tmpUri.PathAndQuery);
            newHyperLink.OuterHtml = tmpNode.OuterHtml;
            newHyperLink.StreamPosition = tmpNode.StreamPosition;
            newHyperLink.Line = tmpNode.Line;
            newHyperLink.LinePosition = tmpNode.LinePosition;
            hyperLinkList.Add(newHyperLink);
          }
        }
      }

      // Notify observers about the html basis code
      this.NotifyHtmlCode(htmlCode);

      // Notify observers about the newly 
      // created cross call list
      this.NotifyHyperLinkList(hyperLinkList);

    }

    #endregion
    

    #region INTERFACE: IObservable

    public void AddObserverPageLinksTo(IObserverPageLinksTo observer)
    {
      observerList.Add(observer);
    }


    public void NotifyHyperLinkList(List<HyperLinkRecord> hyperLinkList)
    {
      foreach (IObserverPageLinksTo observer in observerList)
      {
        observer.UpdateHyperLinkList(hyperLinkList);
      }
    }


    public void NotifyHtmlCode(string htmlCode)
    {
      foreach (IObserverPageLinksTo observer in observerList)
      {
        observer.UpdateHyHtmlCodeHyperLinks(htmlCode);
      }
    }

    #endregion



  }
}
