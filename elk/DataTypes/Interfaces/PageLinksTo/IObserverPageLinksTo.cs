
namespace Elk.DataTypes.Interfaces
{
  using Elk.DataTypes.Dgv;
  using System.Collections.Generic;


  public interface IObserverPageLinksTo
  {
    void UpdateHyperLinkList(List<HyperLinkRecord> hyperLinkList);

    void UpdateHyHtmlCodeHyperLinks(string htmlCode);
  }
}
