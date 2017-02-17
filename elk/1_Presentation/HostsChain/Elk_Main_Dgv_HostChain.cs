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


  public partial class Elk_Main : IObserverHostChain
  {

    #region PUBLIC

    private void InitializeDgvHostChain()
    {
    }


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

      // Manage TreeView
      foreach (ServerResponseEntity tmpElem in this.responseEntityChain)
      {
        string key = string.Format("{0}", tmpElem.RequestedUrl);
        this.tv_ResponseEntities.Nodes.Add(key);
      }

      this.tv_ResponseEntities.SelectedNode = this.tv_ResponseEntities.Nodes[0];

      // Manage DataGridView
      DataForVulnerabilityScanner data = new DataForVulnerabilityScanner(this.responseEntityChain);
      List<IVulnerabilityDefinition> foundVulns = this.vulnHandler.StartScanning(data);
      this.UpdateVulnerabilitiesDgv(foundVulns);
    }

    #endregion


    #endregion
  }
}
