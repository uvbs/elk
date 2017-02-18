namespace Elk.Task
{
  using Elk.DataTypes.Dgv;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using HAP = HtmlAgilityPack;


  public class CrossCallHandler : IObservableCrossCall
  {

    #region MEMBERS

    List<IObserverCrossCall> observerList = new List<IObserverCrossCall>();

    #endregion


    #region PUBLIC

    public void DetermineCrossCalls(string url, string userAgent)
    {
      List<CrossCallRecord> crossCallList = new List<CrossCallRecord>();
      string cacheKey = string.Format("{0}/{1}", userAgent, url);
      Uri crossCallBasisUri;

      if (Uri.TryCreate(url, UriKind.Absolute, out crossCallBasisUri) == false)
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
      Dictionary<string, List<string>> relevantTags = new Dictionary<string, List<string>>() { { "script", new List<string>() { "src" } },
                                                                                   { "embed", new List<string>() { "src" } },
                                                                                   { "form", new List<string>() { "action" } },
                                                                                   { "source", new List<string>() { "src", "srcset" } },
                                                                                   { "object", new List<string>() { "data" } },
                                                                                   { "frame", new List<string>() { "src" } },
                                                                                   { "iframe", new List<string>() { "src" } },
                                                                                   { "base", new List<string>() { "href" } },
                                                                                   { "link", new List<string>() { "href" } },
                                                                                   { "img", new List<string>() { "src" } }
                                                                                };
      var root = doc.DocumentNode;

      foreach (string tagName in relevantTags.Keys)
      {

        foreach (string attributeName in relevantTags[tagName])
        {
          var foundNodes = root.Descendants(tagName).ToList();
          if (foundNodes == null || foundNodes.Count <= 0)
          {
            continue;
          }

          foreach (var tmpNode in foundNodes)
          {
            string link = tmpNode.GetAttributeValue(attributeName, string.Empty);
            Uri tmpUri;

            if (Uri.TryCreate(link, UriKind.Absolute, out tmpUri) == true)
            {

              if ((tmpUri.Host != crossCallBasisUri.Host ||
                  tmpUri.Scheme != crossCallBasisUri.Scheme) &&
                  (tmpUri.Scheme != "//" &&
                   tmpUri.Scheme.ToLower() != "file")
                  )
              {
                CrossCallRecord newCrossCall = new CrossCallRecord(tagName, tmpUri.Scheme, tmpUri.Host, tmpUri.PathAndQuery);
                newCrossCall.OuterHtml = tmpNode.OuterHtml;
                newCrossCall.StreamPosition = tmpNode.StreamPosition;
                newCrossCall.Line = tmpNode.Line;
                newCrossCall.LinePosition = tmpNode.LinePosition;
                crossCallList.Add(newCrossCall);
              }
            }
          }
        }
      }

      // Notify observers about the html basis code
      this.NotifyHtmlCode(htmlCode);

      // Notify observers about the newly 
      // created cross call list
      this.NotifyCrossCallList(crossCallList);
    }

    #endregion



    #region INTERFACE: IObservable

    public void AddObserverCrossCall(IObserverCrossCall observer)
    {
      observerList.Add(observer);
    }


    public void NotifyCrossCallList(List<CrossCallRecord> crossCallList)
    {
      foreach (IObserverCrossCall observer in observerList)
      {
        observer.UpdateCrossCallList(crossCallList);
      }
    }


    public void NotifyHtmlCode(string htmlCode)
    {
      foreach (IObserverCrossCall observer in observerList)
      {
        observer.UpdateHtmlCodeCrossCalls(htmlCode);
      }
    }

    #endregion

  }
}
