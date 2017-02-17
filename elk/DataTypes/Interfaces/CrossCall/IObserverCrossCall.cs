namespace Elk.DataTypes.Interfaces
{
  using System.Collections.Generic;
  using Elk.DataTypes.Dgv;


  public interface IObserverCrossCall
  {
    void UpdateCrossCallList(List<CrossCallRecord> crossCallList);

    void UpdateHtmlCode(string htmlCode);
  }
}
