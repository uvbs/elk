namespace Elk.Presentation
{
  using Elk.DataTypes.Dgv;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main
  {

    private void InitializeDgvReferences()
    {
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
      this.dgv_references.Columns.Add(columnEndHostName);

      DataGridViewTextBoxColumn columnPacketType = new DataGridViewTextBoxColumn();
      columnPacketType.DataPropertyName = "Cookies";
      columnPacketType.Name = "Cookies";
      columnPacketType.HeaderText = "Cookies";
      columnPacketType.ReadOnly = true;
      columnPacketType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_references.Columns.Add(columnPacketType);

      this.referers = new BindingList<RefererRecord>();
      this.dgv_references.DataSource = this.referers;
    }
  }
}
