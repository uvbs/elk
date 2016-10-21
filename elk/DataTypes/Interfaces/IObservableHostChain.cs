namespace Elk.DataTypes.Interfaces
{
  using System.Collections.Generic;


  interface IObservableHostChain
  {
    void AddObserverHostChain(IObserverHostChain o);
    
    void NotifyHostChain(List<ServerResponseEntity> logMessage);
  }
}
