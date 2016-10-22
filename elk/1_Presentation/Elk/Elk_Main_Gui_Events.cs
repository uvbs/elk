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

        //this.l_CrawlerValue.Text = "Running";
        //this.bgw_Crawler.RunWorkerAsync();
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
        this.tb_IpAddress.Text = entity.IpAddresses;
        this.cb_Http.Checked = entity.HttpPortOpen;
        this.cb_Https.Checked = entity.HttpsPortOpen;
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
        //DataForVulnerabilityScanner data = new DataForVulnerabilityScanner(this.responseEntityChain);
        //List<IVulnerabilityDefinition> foundVulns = this.vulnHandler.StartScanning(data);
        //this.UpdateVulnerabilitiesDgv(foundVulns);
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
    #endregion

  }
}
