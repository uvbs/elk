namespace Elk.DataTypes.Interfaces
{

  interface IObservableReferer
  {
    void AddObserverReferer(IObserverReferer o);

    void NotifyObserverReferer(string entryHostName, string cookies);
  }
}
