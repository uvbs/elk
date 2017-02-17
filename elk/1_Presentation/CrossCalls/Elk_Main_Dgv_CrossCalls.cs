namespace Elk.Presentation
{
  using Elk.DataTypes.Dgv;
  using Elk.DataTypes.Interfaces;
  using Elk.Presentation.CrossCalls;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main : IObserverCrossCall
  {

    #region MEMBERS

    private string htmlCode;

    #endregion


    #region PUBLIC

    private void InitializeDgvCrossCalls()
    {
      DataGridViewTextBoxColumn columnTag = new DataGridViewTextBoxColumn();
      columnTag.DataPropertyName = "Tag";
      columnTag.Name = "Tag";
      columnTag.HeaderText = "Tag";
      columnTag.ReadOnly = true;
      columnTag.Width = 40;
      this.dgv_CrossCalls.Columns.Add(columnTag);

      DataGridViewTextBoxColumn columnScheme = new DataGridViewTextBoxColumn();
      columnScheme.DataPropertyName = "Scheme";
      columnScheme.Name = "Scheme";
      columnScheme.HeaderText = "Scheme";
      columnScheme.ReadOnly = true;
      columnScheme.Width = 80;
      this.dgv_CrossCalls.Columns.Add(columnScheme);

      DataGridViewTextBoxColumn columnHostName = new DataGridViewTextBoxColumn();
      columnHostName.DataPropertyName = "HostName";
      columnHostName.Name = "HostName";
      columnHostName.HeaderText = "Host name";
      columnHostName.ReadOnly = true;
      columnHostName.Width = 180;
      this.dgv_CrossCalls.Columns.Add(columnHostName);

      DataGridViewTextBoxColumn columnPath = new DataGridViewTextBoxColumn();
      columnPath.DataPropertyName = "Path";
      columnPath.Name = "Path";
      columnPath.HeaderText = "Path";
      columnPath.ReadOnly = true;
      columnPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_CrossCalls.Columns.Add(columnPath);

      DataGridViewTextBoxColumn columnOuterHtml = new DataGridViewTextBoxColumn();
      columnOuterHtml.DataPropertyName = "OuterHtml";
      columnOuterHtml.Name = "OuterHtml";
      columnOuterHtml.HeaderText = "OuterHtml";
      columnOuterHtml.ReadOnly = true;
      columnOuterHtml.HeaderText = string.Empty;
      columnOuterHtml.Visible = false;
      columnOuterHtml.Width = 0;
      this.dgv_CrossCalls.Columns.Add(columnOuterHtml);

      DataGridViewTextBoxColumn columnStreamPosition = new DataGridViewTextBoxColumn();
      columnStreamPosition.DataPropertyName = "StreamPosition";
      columnStreamPosition.Name = "StreamPosition";
      columnStreamPosition.HeaderText = string.Empty;
      columnStreamPosition.ReadOnly = true;
      columnStreamPosition.Visible = false;
      columnStreamPosition.Width = 0;
      this.dgv_CrossCalls.Columns.Add(columnStreamPosition);

      DataGridViewTextBoxColumn columnLine = new DataGridViewTextBoxColumn();
      columnLine.DataPropertyName = "Line";
      columnLine.Name = "Line";
      columnLine.HeaderText = string.Empty;
      columnLine.ReadOnly = true;
      columnLine.Visible = false;
      columnLine.Width = 0;
      this.dgv_CrossCalls.Columns.Add(columnLine);

      DataGridViewTextBoxColumn columnLinePosition = new DataGridViewTextBoxColumn();
      columnLinePosition.DataPropertyName = "LinePosition";
      columnLinePosition.Name = "LinePosition";
      columnLinePosition.HeaderText = string.Empty;
      columnLinePosition.ReadOnly = true;
      columnLinePosition.Visible = false;
      columnLinePosition.Width = 0;
      this.dgv_CrossCalls.Columns.Add(columnLinePosition);

      this.crossCalls = new BindingList<CrossCallRecord>();
      this.dgv_CrossCalls.DataSource = this.crossCalls;
    }
    

    #region INTERFACE: IObserverCrossCall

    public void UpdateCrossCallList(List<CrossCallRecord> newCrossCallList)
    {
      this.crossCalls.Clear();

      if (newCrossCallList != null && newCrossCallList.Count > 0)
      {
        newCrossCallList.ForEach(elem => this.crossCalls.Add(elem));
      }
    }

    public void UpdateHtmlCode(string htmlCode)
    {
      if (string.IsNullOrEmpty(htmlCode) == false)
      {
        this.htmlCode = htmlCode;
      }
    }

    #endregion

    #endregion


    #region EVENTS

    private void DGV_CrossCalls_DoubleClick(object sender, EventArgs e)
    {
      try
      {
        int currentIndex = this.dgv_CrossCalls.CurrentCell.RowIndex;        
        string outerHtmlData = this.dgv_CrossCalls.Rows[currentIndex].Cells["OuterHtml"].Value.ToString();
        int line = (int) this.dgv_CrossCalls.Rows[currentIndex].Cells["Line"].Value;
        int linePosition = (int) this.dgv_CrossCalls.Rows[currentIndex].Cells["LinePosition"].Value;
        int streamPosition = (int)this.dgv_CrossCalls.Rows[currentIndex].Cells["StreamPosition"].Value;
        
        HtmlDetails_Main htmlView = new HtmlDetails_Main(this.htmlCode, streamPosition, outerHtmlData.Length);
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
