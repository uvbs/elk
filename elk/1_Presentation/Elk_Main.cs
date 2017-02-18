namespace Elk.Presentation
{
  using Elk.DataTypes;
  using Elk.DataTypes.Dgv;
  using Elk.DataTypes.Interfaces;
  using Elk.Task;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main : Form
  {

    #region MEMBERS

    private AnalyzeServerHandler analyzeServerHandler;
    private CrossCallHandler crossCallHandler;
    private CrawlerHandler crawlerHandler;
    private PageLinksToHandler pageLinksToHandler;
    private List<ServerResponseEntity> responseEntityChain;
    private BindingList<RefererRecord> referers;
    private BindingList<CrossCallRecord> crossCalls;
    private BindingList<HyperLinkRecord> hyperLinks;
    private BindingList<IVulnerabilityDefinition> foundVulnerabilities;
    private List<UserAgent> userAgents;

    private VulnerabilityScanner vulnHandler;

    #endregion


    #region PUBLIC

    public Elk_Main()
    {
      this.InitializeComponent();

      // Initialize DataGridViews
      this.InitializeDgvVulnerabilities();
      this.InitializeDgvReferences();
      this.InitializeDgvCrossCalls();
      this.InitializeDgvPageLinksTo();

      // Initialize members
      this.responseEntityChain = new List<ServerResponseEntity>();
      this.crawlerHandler = new CrawlerHandler();
      this.analyzeServerHandler = new AnalyzeServerHandler();
      this.crossCallHandler = new CrossCallHandler();
      this.pageLinksToHandler = new PageLinksToHandler();
      this.vulnHandler = new VulnerabilityScanner();

      // Register at observables
      this.crawlerHandler.AddObserverCrawler(this);
      this.crawlerHandler.AddObserverReferer(this);
      this.analyzeServerHandler.AddObserverHostChain(this);
      this.crossCallHandler.AddObserverCrossCall(this);
      this.pageLinksToHandler.AddObserverPageLinksTo(this);

      // Populate UserAgent ComboBox
      this.userAgents = new List<UserAgent>();
      this.userAgents.Add(new UserAgent("Windows, Firefox", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1"));
      this.userAgents.Add(new UserAgent("Windows, Chrome", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36"));
      this.userAgents.Add(new UserAgent("Windows, Opera", "Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14"));
      this.userAgents.Add(new UserAgent("OSX, Safari", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_3) AppleWebKit/537.75.14 (KHTML, like Gecko) Version/7.0.3 Safari/7046A194A"));
      this.userAgents.Add(new UserAgent("Android ", "Mozilla/5.0 (Linux; U; Android 2.3.5; en-us; HTC Vision Build/GRI40) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1"));
      this.userAgents.Add(new UserAgent("IPhone", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3 like Mac OS X; en-gb) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8F190 Safari/6533.18.5"));
      this.userAgents.Add(new UserAgent("IPad", "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3 like Mac OS X; en-gb) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8F190 Safari/6533.18.5"));
      this.userAgents.Add(new UserAgent("Empty", string.Empty));

      this.cb_UserAgent.DisplayMember = "Name";
      this.cb_UserAgent.ValueMember = "Value";
      this.cb_UserAgent.DataSource = this.userAgents;
      this.cb_UserAgent.SelectedIndex = 1;

      ///
      System.Threading.Tasks.Task.Factory.StartNew(() =>
      {
        while (true)
        {
          System.Threading.Thread.Sleep(200);
          this.Task_CrawlerProcessor();
        }
      });

    }

    #endregion


    #region PRIVATE
    
    private void SetButtonToStart()
    {
      if (!this.bgw_Crawler.IsBusy && !this.bgw_HostChain.IsBusy)
      {
        this.tb_DestinationUrl.Enabled = true;
        this.bt_StartStop.Enabled = true;
        this.bt_StartStop.Text = "Start";
      }
    }


    private ServerResponseEntity GetEntytyByUrl(string url)
    {
      ServerResponseEntity foundEntity = null;

      if (string.IsNullOrEmpty(url))
      {
        return foundEntity;
      }

      foreach (ServerResponseEntity tmpElem in this.responseEntityChain)
      {
        if (tmpElem.RequestedUrl == url)
        {
          return tmpElem;
        }
      }

      return foundEntity;
    }


    private delegate void SetCheckboxStatusDelegate(CheckBox targetCheckBox, bool isEnabled);
    private void SetCheckboxStatus(CheckBox targetCheckBox, bool isEnabled)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new SetCheckboxStatusDelegate(this.SetCheckboxStatus), new object[] { targetCheckBox, isEnabled });
        return;
      }

      targetCheckBox.Checked = isEnabled;
    }

    #endregion


    #region BACKGROUNDWORKERS

    private List<string> crawlerLogBatch = new List<string>();
    private delegate void Task_CrawlerProcessorDelegate();
    private void Task_CrawlerProcessor()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Task_CrawlerProcessorDelegate(this.Task_CrawlerProcessor), new object[] { });
        return;
      }

      List<string> tmpCrawlerLogBatch;
      lock (this.crawlerLogBatch)
      {
        tmpCrawlerLogBatch = new List<string>(this.crawlerLogBatch);
        this.crawlerLogBatch.Clear();
      }

      string newLogData = string.Empty;
      foreach (string tmpRecord in tmpCrawlerLogBatch)
      {
        newLogData += tmpRecord + Environment.NewLine;
      }

      lock (this)
      {
        this.tb_CrawlerLog.AppendText(newLogData);
      }
    }

    #endregion
    
  }
}
