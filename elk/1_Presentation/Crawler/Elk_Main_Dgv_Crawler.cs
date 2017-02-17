namespace Elk.Presentation
{
  using System;
  using Elk.DataTypes.Interfaces;


  public partial class Elk_Main : IObserverCrawler
  {

    #region PUBLIC

    private void InitializeDgvCrawler()
    {
    }

    
    #region INTERFACE: IObserverCrawler

    public void UpdateLog(string logMessage)
    {
      lock (this.crawlerLogBatch)
      {
        string timestampedLogMessage = string.Format("{0} - {1}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), logMessage);
        this.crawlerLogBatch.Add(timestampedLogMessage);
      }
    }

    #endregion

    #endregion

  }
}
