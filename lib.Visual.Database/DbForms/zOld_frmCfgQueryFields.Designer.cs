namespace lib.Visual.Forms
{
  partial class frmCfgQueryFields
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
      this.txtTexto = new lib.Visual.Components.sknTextBox();
      this.sknLabel2 = new lib.Visual.Components.sknLabel();
      this.cbSensitive = new lib.Visual.Components.sknCheckBox();
      this.cmbTipo = new lib.Visual.Components.sknComboBox();
      this.sknLabel3 = new lib.Visual.Components.sknLabel();
      this.sknLabel4 = new lib.Visual.Components.sknLabel();
      this.txtTamanho = new lib.Visual.Components.sknTextBox();
      this.cmbName = new lib.Visual.Components.sknComboBox();
      this.pnlContext.SuspendLayout();
      this.pnlBottom.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlContext
      // 
      this.pnlContext.Controls.Add(this.cmbName);
      this.pnlContext.Controls.Add(this.txtTamanho);
      this.pnlContext.Controls.Add(this.sknLabel4);
      this.pnlContext.Controls.Add(this.sknLabel3);
      this.pnlContext.Controls.Add(this.cmbTipo);
      this.pnlContext.Controls.Add(this.cbSensitive);
      this.pnlContext.Controls.Add(this.sknLabel2);
      this.pnlContext.Controls.Add(this.txtTexto);
      this.pnlContext.Controls.Add(this.sknLabel1);
      this.pnlContext.Size = new System.Drawing.Size(256, 196);
      // 
      // pnlBottom
      // 
      this.pnlBottom.Location = new System.Drawing.Point(0, 196);
      this.pnlBottom.Size = new System.Drawing.Size(256, 45);
      // 
      // btnConfirm
      // 
      this.btnConfirm.Location = new System.Drawing.Point(-243, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(-147, 8);
      // 
      // sknLabel1
      // 
      this.sknLabel1.AutoSize = true;
      this.sknLabel1.Location = new System.Drawing.Point(12, 9);
      this.sknLabel1.Name = "sknLabel1";
      this.sknLabel1.Size = new System.Drawing.Size(34, 13);
      this.sknLabel1.TabIndex = 0;
      this.sknLabel1.Text = "Texto";
      // 
      // txtTexto
      // 
      this.txtTexto.AsDateTime = new System.DateTime(2010, 6, 17, 13, 44, 40, 156);
      this.txtTexto.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.txtTexto.AutoTab = true;
      this.txtTexto.Location = new System.Drawing.Point(12, 25);
      this.txtTexto.Name = "txtTexto";
      this.txtTexto.Size = new System.Drawing.Size(232, 20);
      this.txtTexto.TabIndex = 1;
      this.txtTexto.TextFormat = null;
      this.txtTexto.TextType = lib.Visual.Components.enmTextType.String;
      this.txtTexto.Leave += new System.EventHandler(this.txtTexto_Leave);
      // 
      // sknLabel2
      // 
      this.sknLabel2.AutoSize = true;
      this.sknLabel2.Location = new System.Drawing.Point(12, 48);
      this.sknLabel2.Name = "sknLabel2";
      this.sknLabel2.Size = new System.Drawing.Size(35, 13);
      this.sknLabel2.TabIndex = 2;
      this.sknLabel2.Text = "Nome";
      // 
      // cbSensitive
      // 
      this.cbSensitive.AutoSize = true;
      this.cbSensitive.Location = new System.Drawing.Point(12, 173);
      this.cbSensitive.Name = "cbSensitive";
      this.cbSensitive.Size = new System.Drawing.Size(69, 17);
      this.cbSensitive.TabIndex = 8;
      this.cbSensitive.Text = "Sensitive";
      this.cbSensitive.UseVisualStyleBackColor = true;
      // 
      // cmbTipo
      // 
      this.cmbTipo.AutoTab = true;
      this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbTipo.FormattingEnabled = true;
      this.cmbTipo.Location = new System.Drawing.Point(12, 104);
      this.cmbTipo.Name = "cmbTipo";
      this.cmbTipo.Size = new System.Drawing.Size(232, 21);
      this.cmbTipo.TabIndex = 5;
      // 
      // sknLabel3
      // 
      this.sknLabel3.AutoSize = true;
      this.sknLabel3.Location = new System.Drawing.Point(12, 88);
      this.sknLabel3.Name = "sknLabel3";
      this.sknLabel3.Size = new System.Drawing.Size(28, 13);
      this.sknLabel3.TabIndex = 4;
      this.sknLabel3.Text = "Tipo";
      // 
      // sknLabel4
      // 
      this.sknLabel4.AutoSize = true;
      this.sknLabel4.Location = new System.Drawing.Point(12, 128);
      this.sknLabel4.Name = "sknLabel4";
      this.sknLabel4.Size = new System.Drawing.Size(52, 13);
      this.sknLabel4.TabIndex = 6;
      this.sknLabel4.Text = "Tamanho";
      // 
      // txtTamanho
      // 
      this.txtTamanho.AsDateTime = new System.DateTime(2010, 6, 17, 13, 44, 40, 62);
      this.txtTamanho.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.txtTamanho.AutoTab = true;
      this.txtTamanho.Location = new System.Drawing.Point(12, 144);
      this.txtTamanho.Name = "txtTamanho";
      this.txtTamanho.Size = new System.Drawing.Size(232, 20);
      this.txtTamanho.TabIndex = 7;
      this.txtTamanho.Text = "0";
      this.txtTamanho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtTamanho.TextFormat = null;
      this.txtTamanho.TextType = lib.Visual.Components.enmTextType.Int;
      // 
      // cmbName
      // 
      this.cmbName.AutoTab = true;
      this.cmbName.FormattingEnabled = true;
      this.cmbName.Location = new System.Drawing.Point(12, 64);
      this.cmbName.Name = "cmbName";
      this.cmbName.Size = new System.Drawing.Size(232, 21);
      this.cmbName.TabIndex = 3;
      // 
      // frmCfgQueryFields
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(256, 241);
      this.Name = "frmCfgQueryFields";
      this.Text = "Campos";
      this.Load += new System.EventHandler(this.frmCfgQueryFields_Load);
      this.pnlContext.ResumeLayout(false);
      this.pnlContext.PerformLayout();
      this.pnlBottom.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private lib.Visual.Components.sknTextBox txtTexto;
    private lib.Visual.Components.sknLabel sknLabel1;
    private lib.Visual.Components.sknCheckBox cbSensitive;
    private lib.Visual.Components.sknLabel sknLabel2;
    private lib.Visual.Components.sknComboBox cmbTipo;
    private lib.Visual.Components.sknTextBox txtTamanho;
    private lib.Visual.Components.sknLabel sknLabel4;
    private lib.Visual.Components.sknLabel sknLabel3;
    private lib.Visual.Components.sknComboBox cmbName;
  }
}