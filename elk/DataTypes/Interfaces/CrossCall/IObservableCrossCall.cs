namespace Elk.DataTypes.Interfaces
{
  using System.Collections.Generic;
  using Elk.DataTypes.Dgv;


  public interface IObservableCrossCall
  {
    void AddObserverCrossCall(IObserverCrossCall observer);

    void NotifyCrossCallList(List<CrossCallRecord> crossCallList);

    void NotifyHtmlCode(string htmlCode);
  }
}
