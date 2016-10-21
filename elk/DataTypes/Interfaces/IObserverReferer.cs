namespace Elk.DataTypes.Interfaces
{
  using System.Collections.Generic;


  public interface IObserverReferer
  {
    void UpdateReferer(string entryHostName, string cookies);
  }
}
