namespace Elk.Presentation
{
  using Elk.DataTypes;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.Windows.Forms;


  public partial class Elk_Main
  {

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
        this.tb_Cookies.Text = entity.CookiesString;
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
        this.UpdateVulnerabilitiesDgv(foundVulns);
      }
    }

    private delegate void UpdateVulnerabilitiesDgvDelegate(List<IVulnerabilityDefinition> foundVulns);
    private void UpdateVulnerabilitiesDgv(List<IVulnerabilityDefinition> foundVulns)
    {
      if (this.InvokeRequired)
      {
        this.BeginInvoke(new UpdateVulnerabilitiesDgvDelegate(this.UpdateVulnerabilitiesDgv), new object[] { foundVulns });
        return;
      }

      this.foundVulnerabilities.Clear();

      foreach (IVulnerabilityDefinition newRecord in foundVulns)
      {
        this.foundVulnerabilities.Add(newRecord);
      }
    }


    private void bgw_HostChain_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
    {
      this.l_HostChainValue.Text = "Done";
      this.SetButtonToStart();
    }

    #endregion

  }
}
