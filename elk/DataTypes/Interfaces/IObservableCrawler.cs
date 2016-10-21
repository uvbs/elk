namespace Elk.DataTypes.Interfaces
{
  interface IObservableCrawler
  {
    void AddObserverCrawler(IObserverCrawler observer);

    void NotifyCrawler(string logMessage);
  }
}
