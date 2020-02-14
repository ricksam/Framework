namespace lib.Visual.Forms
{
  partial class FormQuery
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
      this.label1 = new System.Windows.Forms.Label();
      this.pnlBottom.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlBottom
      // 
      this.pnlBottom.BackColor = System.Drawing.SystemColors.Control;
      this.pnlBottom.Controls.Add(this.label1);
      this.pnlBottom.Location = new System.Drawing.Point(0, 341);
      this.pnlBottom.Size = new System.Drawing.Size(734, 21);
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(734, 21);
      this.label1.TabIndex = 0;
      this.label1.Text = "Pressione [Enter] para selecionar o registro ou [Esc] para sair";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FormQuery
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(734, 362);
      this.Name = "FormQuery";
      this.Text = "Pesquisa";
      this.pnlBottom.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;

  }
}