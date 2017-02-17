namespace Elk.Presentation
{
  partial class HtmlDetails_Main
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.tb_Html = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // tb_Html
      // 
      this.tb_Html.AcceptsReturn = true;
      this.tb_Html.AcceptsTab = true;
      this.tb_Html.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Html.Location = new System.Drawing.Point(8, 8);
      this.tb_Html.Multiline = true;
      this.tb_Html.Name = "tb_Html";
      this.tb_Html.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.tb_Html.Size = new System.Drawing.Size(1198, 401);
      this.tb_Html.TabIndex = 0;
      this.tb_Html.WordWrap = false;
      // 
      // HtmlDetails_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1215, 416);
      this.Controls.Add(this.tb_Html);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "HtmlDetails_Main";
      this.Text = "Html ...";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tb_Html;
  }
}