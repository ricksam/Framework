namespace lib.Visual.Forms
{
  partial class Expressao
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
      this.sknLabel1 = new lib.Visual.Components.sknLabel();
      this.txtExpressao = new lib.Visual.Components.sknTextBox();
      this.SuspendLayout();
      // 
      // sknLabel1
      // 
      this.sknLabel1.AutoSize = true;
      this.sknLabel1.Location = new System.Drawing.Point(12, 9);
      this.sknLabel1.Name = "sknLabel1";
      this.sknLabel1.Size = new System.Drawing.Size(262, 13);
      this.sknLabel1.TabIndex = 0;
      this.sknLabel1.Text = "Digite uma expressao e pressione enter para confirmar";
      // 
      // txtExpressao
      // 
      this.txtExpressao.AsDateTime = new System.DateTime(((long)(0)));
      this.txtExpressao.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.txtExpressao.AutoTab = false;
      this.txtExpressao.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtExpressao.Location = new System.Drawing.Point(12, 25);
      this.txtExpressao.Name = "txtExpressao";
      this.txtExpressao.Size = new System.Drawing.Size(591, 31);
      this.txtExpressao.TabIndex = 1;
      this.txtExpressao.TextFormat = null;
      this.txtExpressao.TextType = lib.Visual.Components.enmTextType.String;
      this.txtExpressao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sknTextBox1_KeyDown);
      // 
      // Expressao
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(614, 62);
      this.Controls.Add(this.txtExpressao);
      this.Controls.Add(this.sknLabel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Expressao";
      this.Text = "Expressao";
      this.Load += new System.EventHandler(this.Expressao_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private lib.Visual.Components.sknLabel sknLabel1;
    private lib.Visual.Components.sknTextBox txtExpressao;
  }
}