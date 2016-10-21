namespace Elk.DataTypes.Interfaces
{
  using System.Collections.Generic;


  public interface IObserverHostChain
  {
    void UpdateHostChain(List<ServerResponseEntity> logMessage);
  }
}
