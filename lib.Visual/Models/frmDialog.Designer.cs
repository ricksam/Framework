namespace lib.Visual.Models
{
  partial class frmDialog
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
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContext
            // 
            this.pnlContext.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pnlContext.Size = new System.Drawing.Size(389, 267);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.pnlBottom.Size = new System.Drawing.Size(389, 55);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(-74, 12);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(54, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // frmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(389, 322);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.MaximizeBox = false;
            this.Name = "frmDialog";
            this.Text = "frmDialog";
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

  }
}