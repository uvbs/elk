﻿namespace Elk.Presentation
{
  using Elk.DataTypes;
  using Elk.DataTypes.Dgv;
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
    private BindingList<IVulnerabilityDefinition> foundVulnerabilities;
    private TcpPortChecker portScanner;
    private VulnerabilityScanner vulnHandler;

    #endregion


    #region PUBLIC

    public Elk_Main()
    {
      this.InitializeComponent();

      // Initialize DataGridViews
      this.InitializeDgvVulnerabilities();
      this.InitializeDgvReferences();

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


    #region BACKGROUNDWORKERS


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


    private void dgv_Vulnerabilities_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
      {
        return;
      }

      try
      {
        DataGridView.HitTestInfo hti = this.dgv_Vulnerabilities.HitTest(e.X, e.Y);
        if (hti.RowIndex >= 0)
        {
          string title = this.dgv_Vulnerabilities.Rows[hti.RowIndex].Cells[this.dgv_Vulnerabilities.Columns["Title"].Index].Value.ToString();
          string description = this.dgv_Vulnerabilities.Rows[hti.RowIndex].Cells[this.dgv_Vulnerabilities.Columns["Description"].Index].Value.ToString();
          string concequences = this.dgv_Vulnerabilities.Rows[hti.RowIndex].Cells[this.dgv_Vulnerabilities.Columns["Consequences"].Index].Value.ToString();
          string setup = this.dgv_Vulnerabilities.Rows[hti.RowIndex].Cells[this.dgv_Vulnerabilities.Columns["Setup"].Index].Value.ToString();
          
          Form_VulnerabilityDetails vulnerabilityDetails = new Form_VulnerabilityDetails(title, description, concequences, setup);
          vulnerabilityDetails.ShowDialog();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(string.Format("Exception occurred: {0}", ex.Message));
      }
    }
  }
}
