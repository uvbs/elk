namespace Elk.Presentation
{
  using Elk.DataTypes.Dgv;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main : IObserverPageLinksTo
  {

    #region MEMBERS

    private string htmlCodeHyperLinks;

    #endregion


    #region PUBLIC

    private void InitializeDgvPageLinksTo()
    {
      DataGridViewTextBoxColumn columnScheme = new DataGridViewTextBoxColumn();
      columnScheme.DataPropertyName = "Scheme";
      columnScheme.Name = "Scheme";
      columnScheme.HeaderText = "Scheme";
      columnScheme.ReadOnly = true;
      columnScheme.Width = 80;
      this.dgv_PageLinksTo.Columns.Add(columnScheme);

      DataGridViewTextBoxColumn columnHostName = new DataGridViewTextBoxColumn();
      columnHostName.DataPropertyName = "HostName";
      columnHostName.Name = "HostName";
      columnHostName.HeaderText = "Host name";
      columnHostName.ReadOnly = true;
      columnHostName.Width = 180;
      this.dgv_PageLinksTo.Columns.Add(columnHostName);

      DataGridViewTextBoxColumn columnPath = new DataGridViewTextBoxColumn();
      columnPath.DataPropertyName = "Path";
      columnPath.Name = "Path";
      columnPath.HeaderText = "Path";
      columnPath.ReadOnly = true;
      columnPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_PageLinksTo.Columns.Add(columnPath);

      DataGridViewTextBoxColumn columnOuterHtml = new DataGridViewTextBoxColumn();
      columnOuterHtml.DataPropertyName = "OuterHtml";
      columnOuterHtml.Name = "OuterHtml";
      columnOuterHtml.HeaderText = "OuterHtml";
      columnOuterHtml.ReadOnly = true;
      columnOuterHtml.HeaderText = string.Empty;
      columnOuterHtml.Visible = false;
      columnOuterHtml.Width = 0;
      this.dgv_PageLinksTo.Columns.Add(columnOuterHtml);

      DataGridViewTextBoxColumn columnStreamPosition = new DataGridViewTextBoxColumn();
      columnStreamPosition.DataPropertyName = "StreamPosition";
      columnStreamPosition.Name = "StreamPosition";
      columnStreamPosition.HeaderText = string.Empty;
      columnStreamPosition.ReadOnly = true;
      columnStreamPosition.Visible = false;
      columnStreamPosition.Width = 0;
      this.dgv_PageLinksTo.Columns.Add(columnStreamPosition);

      DataGridViewTextBoxColumn columnLine = new DataGridViewTextBoxColumn();
      columnLine.DataPropertyName = "Line";
      columnLine.Name = "Line";
      columnLine.HeaderText = string.Empty;
      columnLine.ReadOnly = true;
      columnLine.Visible = false;
      columnLine.Width = 0;
      this.dgv_PageLinksTo.Columns.Add(columnLine);

      DataGridViewTextBoxColumn columnLinePosition = new DataGridViewTextBoxColumn();
      columnLinePosition.DataPropertyName = "LinePosition";
      columnLinePosition.Name = "LinePosition";
      columnLinePosition.HeaderText = string.Empty;
      columnLinePosition.ReadOnly = true;
      columnLinePosition.Visible = false;
      columnLinePosition.Width = 0;
      this.dgv_PageLinksTo.Columns.Add(columnLinePosition);

      this.hyperLinks = new BindingList<HyperLinkRecord>();
      this.dgv_PageLinksTo.DataSource = this.hyperLinks;
    }


    #region INTERFACE: IObserverCrawler

    public void UpdateHyperLinkList(List<HyperLinkRecord> newHyperLinkList)
    {
      this.hyperLinks.Clear();

      if (newHyperLinkList != null && newHyperLinkList.Count > 0)
      {
        newHyperLinkList.ForEach(elem => this.hyperLinks.Add(elem));
      }
    }


    public void UpdateHyHtmlCodeHyperLinks(string htmlCode)
    {
      if (string.IsNullOrEmpty(htmlCode) == false)
      {
        this.htmlCodeHyperLinks = htmlCode;
      }
    }

    #endregion

    #endregion


    #region EVENTS

    private void DGV_HyperLinks_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        int currentIndex = this.dgv_PageLinksTo.CurrentCell.RowIndex;
        string outerHtmlData = this.dgv_PageLinksTo.Rows[currentIndex].Cells["OuterHtml"].Value.ToString();
        int line = (int)this.dgv_PageLinksTo.Rows[currentIndex].Cells["Line"].Value;
        int linePosition = (int)this.dgv_PageLinksTo.Rows[currentIndex].Cells["LinePosition"].Value;
        int streamPosition = (int)this.dgv_PageLinksTo.Rows[currentIndex].Cells["StreamPosition"].Value;

        HtmlDetails_Main htmlView = new HtmlDetails_Main(this.htmlCodeCrossCalls, streamPosition, outerHtmlData.Length);
        htmlView.ShowDialog();
      }
      catch (Exception ex)
      {
        string errorMsg = string.Format("The following error occurred: {0}\r\n\r\n{1}", ex.Message, ex.StackTrace);
        MessageBox.Show(errorMsg, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    #endregion

  }
}
