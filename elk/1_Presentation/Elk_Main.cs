namespace Elk
{
  using Elk.DataTypes;
  using Elk.DataTypes.DGV;
  using Elk.DataTypes.Interfaces;
  using Elk.Task;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main : Form, IObserverCrawler, IObserverHostChain, IObserverReferer
  {

    #region MEMBERS

    private AnalyzeServerHandler analyzeServerHandler;
    private List<ServerResponseEntity> responseEntityChain;
    private CrawlerHandler crawlerHandler;
    private BindingList<RefererRecord> referers;
    private BindingList<FoundVulnerabilitiesRecord> foundVulnerabilities;
    private TcpPortChecker portScanner;
    private VulnerabilityScanner vulnHandler;

    #endregion


    #region PUBLIC

    public Elk_Main()
    {
      this.InitializeComponent();

      DataGridViewTextBoxColumn columnEntryHostName = new DataGridViewTextBoxColumn();
      columnEntryHostName.DataPropertyName = "EntryHostName";
      columnEntryHostName.Name = "EntryHostName";
      columnEntryHostName.HeaderText = "Entry host name";
      columnEntryHostName.ReadOnly = true;
      columnEntryHostName.Width = 180;
      columnEntryHostName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_references.Columns.Add(columnEntryHostName);

      DataGridViewTextBoxColumn columnEndHostName = new DataGridViewTextBoxColumn();
      columnEndHostName.DataPropertyName = "EndHostName";
      columnEndHostName.Name = "EndHostName";
      columnEndHostName.HeaderText = "End host name";
      columnEndHostName.ReadOnly = true;
      columnEndHostName.Width = 180;
      //columnEndHostName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_references.Columns.Add(columnEndHostName);

      DataGridViewTextBoxColumn columnPacketType = new DataGridViewTextBoxColumn();
      columnPacketType.DataPropertyName = "Cookies";
      columnPacketType.Name = "Cookies";
      columnPacketType.HeaderText = "Cookies";
      columnPacketType.ReadOnly = true;
      //// columnRemHost.Width = 280;
      columnPacketType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_references.Columns.Add(columnPacketType);

      this.referers = new BindingList<RefererRecord>();
      this.dgv_references.DataSource = this.referers;

      this.foundVulnerabilities = new BindingList<FoundVulnerabilitiesRecord>();
      this.dgv_Vulnerabilities.DataSource = this.foundVulnerabilities;

      // Initialize members
      this.responseEntityChain = new List<ServerResponseEntity>();
      this.crawlerHandler = new CrawlerHandler();
      this.analyzeServerHandler = new AnalyzeServerHandler();
      this.portScanner = new TcpPortChecker();
      this.vulnHandler = new VulnerabilityScanner();

      // Register at observables
      this.crawlerHandler.AddObserverCrawler(this);
      this.crawlerHandler.AddObserverReferer(this);
      this.analyzeServerHandler.AddObserverHostChain(this);


      System.Threading.Tasks.Task.Factory.StartNew(() =>
      {
        while (true)
        {
          System.Threading.Thread.Sleep(2000);
          this.Task_RefererProcessor1();
        }
      });

      System.Threading.Tasks.Task.Factory.StartNew(() =>
      {
        while (true)
        {
          System.Threading.Thread.Sleep(2000);
          this.Task_RefererProcessor2();
        }
      });

      System.Threading.Tasks.Task.Factory.StartNew(() =>
      {
        while (true)
        {
          System.Threading.Thread.Sleep(2000);
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


    #region GUI EVENTS

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void bt_StartStop_Click(object sender, EventArgs e)
    {

      if (this.bt_StartStop.Text == "Start")
      {
        this.referers.Clear();

        this.cb_Http.Checked = false;
        this.cb_Https.Checked = false;

        this.tb_CrawlerLog.Text = string.Empty;
        this.bt_StartStop.Text = "Stop";

        this.l_HostChainValue.Text = "Running";
        this.bgw_HostChain.RunWorkerAsync();

        this.l_CrawlerValue.Text = "Running";
        this.bgw_Crawler.RunWorkerAsync();
      }
      else
      {
        this.bt_StartStop.Text = "Start";

        if (this.bgw_Crawler.IsBusy)
        {
          this.crawlerHandler.Cancel();
          this.bgw_Crawler.CancelAsync();
        }

        if (this.bgw_HostChain.IsBusy)
        {
          this.bgw_HostChain.CancelAsync();
        }
      }
    }


    private void tb_DestinationUrl_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Enter)
      {
        return;
      }

      this.bt_StartStop_Click(null, null);
    }


    private void tv_ResponseEntities_AfterSelect(object sender, TreeViewEventArgs e)
    {
      TreeNode node = this.tv_ResponseEntities.SelectedNode;
      ServerResponseEntity entity = this.GetEntytyByUrl(node.Text);

      if (entity != null)
      {
        this.tb_ResponseStatus.Text = entity.ResponseStatus;
        this.tb_Hsts.Text = entity.Hsts;
        this.tb_Hpkp.Text = entity.Hpkp;
        this.tb_Location.Text = entity.RedirectLocation;
        this.tb_Cookies.Text = this.ProcessCookies(entity.CookiesString);
        this.tb_RawHeaders.Text = entity.RawHeaders;
        this.tb_Server.Text = entity.Server;
      }
    }


    private void bgw_Crawler_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
      try
      {
        this.crawlerHandler.StartCrawling(this.tb_DestinationUrl.Text);
      }
      catch (Exception)
      {
      }
    }


    private void bgw_Crawler_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
      this.l_CrawlerValue.Text = "Done";
      this.SetButtonToStart();
    }

    private void bgw_HostChain_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
    {
      // Port scan
      string getRequest = "GET / HTTP.1.1\n\n";
      try
      {
        bool httpPortOpen = this.portScanner.IsPortOpen(this.tb_DestinationUrl.Text, 80, getRequest);
        this.SetCheckboxStatus(this.cb_Http, httpPortOpen);
      }
      catch (Exception)
      {
      }

      if (this.bgw_Crawler.CancellationPending)
      {
        return;
      }

      try
      {
        bool httpsPortOpen = this.portScanner.IsPortOpen(this.tb_DestinationUrl.Text, 443, getRequest);
        this.SetCheckboxStatus(this.cb_Https, httpsPortOpen);
      }
      catch (Exception)
      {
      }


      if (this.bgw_Crawler.CancellationPending)
      {
        return;
      }

      if (this.cb_Http.Checked)
      {
        // Determine hosts chain
        try
        {
          string url = string.Format("http://{0}", this.tb_DestinationUrl.Text);
          this.analyzeServerHandler.DetermineHostsChain(url);
        }
        catch (Exception ex)
        {
          this.bgw_HostChain.CancelAsync();
          MessageBox.Show(string.Format("Exception: {0}", ex.Message));
        }

        // Determine vulnerabilities
        DataForVulnerabilityScanner data = new DataForVulnerabilityScanner(this.responseEntityChain, this.cb_Http.Checked, this.cb_Https.Checked);
        List<IVulnerabilityDefinition> foundVulns = this.vulnHandler.StartScanning(data);
        this.UpdateVulnerabilitiesDGV(foundVulns);
      }
    }

    private delegate void UpdateVulnerabilitiesDGVDelegate(List<IVulnerabilityDefinition> foundVulns);
    private void UpdateVulnerabilitiesDGV(List<IVulnerabilityDefinition> foundVulns)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new UpdateVulnerabilitiesDGVDelegate(this.UpdateVulnerabilitiesDGV), new object[] { foundVulns });
        return;
      }

      this.dgv_Vulnerabilities.DataSource = foundVulns;
    }


    private void bgw_HostChain_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
      this.l_HostChainValue.Text = "Done";
      this.SetButtonToStart();
    }

    #endregion


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


    #region INTERFACE: IObserverHostsChain

    public delegate void UpdateHostChainDelegate(List<ServerResponseEntity> newHostChain);
    public void UpdateHostChain(List<ServerResponseEntity> newHostChain)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new UpdateHostChainDelegate(this.UpdateHostChain), new object[] { newHostChain });
        return;
      }

      this.responseEntityChain = newHostChain;
      this.tv_ResponseEntities.Nodes.Clear();
      if (this.responseEntityChain == null || this.responseEntityChain.Count <= 0)
      {
        return;
      }

      foreach (ServerResponseEntity tmpElem in this.responseEntityChain)
      {
        string key = string.Format("{0}", tmpElem.RequestedUrl);
        this.tv_ResponseEntities.Nodes.Add(key);
      }

      this.tv_ResponseEntities.SelectedNode = this.tv_ResponseEntities.Nodes[0];
    }

    #endregion


    #region INTERFACE: IObserverReferer

    private List<Tuple<string, string>> refererBatch = new List<Tuple<string, string>>();
    public delegate void UpdateRefererDelegate(string entryHostName, string cookies);
    public void UpdateReferer(string entryHostName, string cookies)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new UpdateRefererDelegate(this.UpdateReferer), new object[] { entryHostName, cookies });
        return;
      }

      lock (this.refererBatch)
      {
        this.refererBatch.Add(new Tuple<string, string>(entryHostName, cookies));
      }
    }

    #endregion

    private delegate void Task_RefererProcessorDelegate();
    private void Task_RefererProcessor1()
    {
      List<Tuple<string, string>> tmpRefererBatch;
      List<RefererRecord> newRefererRecords = new List<RefererRecord>();
      lock (this.refererBatch)
      {
        tmpRefererBatch = new List<Tuple<string, string>>(this.refererBatch);
        this.refererBatch.Clear();
      }

      foreach (Tuple<string, string> tmpRecord in tmpRefererBatch)
      {
        string chain = string.Empty;
        string entryHostName = tmpRecord.Item1;
        string cookies = tmpRecord.Item2;
        try
        {
          AnalyzeServerHandler serverAnalyzer = new AnalyzeServerHandler();
          string tmpUrl = string.Format("http://{0}/", entryHostName);
          List<ServerResponseEntity> hostChain = serverAnalyzer.DetermineHostsChain(tmpUrl);
          hostChain.ForEach(elem => chain += string.Format("{0}://{1}, ", elem.RequestedScheme, elem.RequestedHost));
          //chain = chain.TrimEnd(new char[] { ' ', ',' });
          cookies = hostChain[hostChain.Count - 1].CookiesString;
        }
        catch (Exception ex)
        {
          cookies = string.Format("Error occurred: {0}", ex.Message);
        }
        RefererRecord tmpRefererRecord = new RefererRecord(entryHostName, chain, cookies);
        lock (this.refererRecordBatch)
        {
          this.refererRecordBatch.Add(tmpRefererRecord);
        }
      }
    }

    List<RefererRecord> refererRecordBatch = new List<RefererRecord>();
    private delegate void Task_RefererProcessor2Delegate();
    private void Task_RefererProcessor2()
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new Task_RefererProcessor2Delegate(this.Task_RefererProcessor2), new object[] { });
        return;
      }
      List<RefererRecord> tmpRefererRecordBatch;

      lock (this.refererRecordBatch)
      {
        tmpRefererRecordBatch = new List<RefererRecord>(this.refererRecordBatch);
        this.refererRecordBatch.Clear();
      }

      this.BeginInvoke((MethodInvoker)delegate ()
      {
        foreach (RefererRecord tmpRecord in tmpRefererRecordBatch)
        {
          this.referers.Add(tmpRecord);
        }
      });
    }




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


    private string ProcessCookies(string cookies)
    {
      string processedCookies = cookies;

      //if (string.IsNullOrEmpty(cookies))
      //{
      //  return processedCookies;
      //}

      //string[] cookiesSplitter = cookies.Split(',');

      return processedCookies;
    }
  }
}
