namespace Elk.Presentation
{
  using Elk.DataTypes.Interfaces;
  using System.ComponentModel;
  using System.Windows.Forms;


  public partial class Elk_Main
  {

    #region PRIVATE

    private void InitializeDgvVulnerabilities()
    {
      DataGridViewTextBoxColumn columnTitle = new DataGridViewTextBoxColumn();
      columnTitle.DataPropertyName = "Title";
      columnTitle.Name = "Title";
      columnTitle.HeaderText = "Vulnerability";
      columnTitle.ReadOnly = true;
      columnTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgv_Vulnerabilities.Columns.Add(columnTitle);

      DataGridViewTextBoxColumn columnDescription = new DataGridViewTextBoxColumn();
      columnDescription.DataPropertyName = "Description";
      columnDescription.Name = "Description";
      columnDescription.HeaderText = "Description";
      columnDescription.ReadOnly = true;
      columnDescription.Visible = false;
      this.dgv_Vulnerabilities.Columns.Add(columnDescription);

      DataGridViewTextBoxColumn columnConsequences = new DataGridViewTextBoxColumn();
      columnConsequences.DataPropertyName = "Consequences";
      columnConsequences.Name = "Consequences";
      columnConsequences.HeaderText = "Consequences";
      columnConsequences.ReadOnly = true;
      columnConsequences.Visible = false;
      this.dgv_Vulnerabilities.Columns.Add(columnConsequences);

      DataGridViewTextBoxColumn columnSetup = new DataGridViewTextBoxColumn();
      columnSetup.DataPropertyName = "Consequences";
      columnSetup.Name = "Consequences";
      columnSetup.HeaderText = "Consequences";
      columnSetup.ReadOnly = true;
      columnSetup.Visible = false;
      this.dgv_Vulnerabilities.Columns.Add(columnSetup);

      this.foundVulnerabilities = new BindingList<IVulnerabilityDefinition>();
      this.dgv_Vulnerabilities.DataSource = this.foundVulnerabilities;
    }

    #endregion 

  }
}
