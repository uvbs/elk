namespace Elk.Presentation.CrossCalls
{
  using System.Windows.Forms;


  public partial class HtmlDetails_Main : Form
  {

    #region PUBLIC

    public HtmlDetails_Main(string htmlData, int streamPosition, int selectionLength = 1)
    {
      InitializeComponent();

      this.tb_Html.Text = htmlData;
      this.tb_Html.SelectionStart = streamPosition;
      this.tb_Html.SelectionLength = selectionLength;

      this.tb_Html.Select(streamPosition, selectionLength);

      this.tb_Html.ScrollToCaret();
    }

    #endregion


    #region PRIVATE
    
    /// <summary>
    /// Close GUI on Escape.
    /// </summary>
    /// <param name="keyData"></param>
    /// <returns></returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Escape)
      {
        this.Dispose(true);
        return false;
      }
      else if (keyData == (Keys.Control | Keys.R))
      {
        return false;
      }
      else
      {
        return base.ProcessDialogKey(keyData);
      }
    }

    #endregion

  }
}
