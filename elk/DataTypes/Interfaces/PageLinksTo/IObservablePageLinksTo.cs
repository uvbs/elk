namespace Elk.DataTypes.Interfaces
{
  using System.Collections.Generic;
  using Elk.DataTypes.Dgv;


  public interface IObservablePageLinksTo
  {
    void AddObserverPageLinksTo(IObserverPageLinksTo observer);

    void NotifyHyperLinkList(List<HyperLinkRecord> hyperLinkList);

    void NotifyHtmlCode(string htmlCode);
  }
}
