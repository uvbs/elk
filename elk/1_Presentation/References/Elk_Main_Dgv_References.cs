namespace Elk.Presentation
{
  using Elk.DataTypes.Dgv;
  using Elk.DataTypes.Interfaces;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main : IObserverReferer
  {

    #region PUBLIC

    private void InitializeDgvReferences()
    {
      DataGridViewTextBoxColumn columnEntryHostName = new DataGridViewTextBoxColumn();
      columnEntryHostName.DataPropertyName = "EntryHostName";
      columnEntryHostName.Name = "EntryHostName";
      columnEntryHostName.HeaderText = "Entry host name";
      columnEntryHostName.ReadOnly = true;
      columnEntryHostName.Width = 180;
      columnEntryHostName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_PageLinksTo.Columns.Add(columnEntryHostName);

      DataGridViewTextBoxColumn columnEndHostName = new DataGridViewTextBoxColumn();
      columnEndHostName.DataPropertyName = "EndHostName";
      columnEndHostName.Name = "EndHostName";
      columnEndHostName.HeaderText = "End host name";
      columnEndHostName.ReadOnly = true;
      columnEndHostName.Width = 180;
      this.dgv_PageLinksTo.Columns.Add(columnEndHostName);

      DataGridViewTextBoxColumn columnPacketType = new DataGridViewTextBoxColumn();
      columnPacketType.DataPropertyName = "Cookies";
      columnPacketType.Name = "Cookies";
      columnPacketType.HeaderText = "Cookies";
      columnPacketType.ReadOnly = true;
      columnPacketType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_PageLinksTo.Columns.Add(columnPacketType);

      this.referers = new BindingList<RefererRecord>();
      this.dgv_PageLinksTo.DataSource = this.referers;
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

  }
}
