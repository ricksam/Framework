namespace lib.Visual.Forms
{
  partial class Restore
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Restore));
      this.sknGroupBox1 = new lib.Visual.Components.sknGroupBox();
      this.lstBancos = new lib.Visual.Components.sknListBox();
      this.cmBancos = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.excluirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.btnUtilizarBackup = new lib.Visual.Components.sknButton();
      this.txtUtilizarBackup = new lib.Visual.Components.sknTextBox();
      this.rbCriarNovo = new lib.Visual.Components.sknRadioButton();
      this.rbSelecionaBanco = new lib.Visual.Components.sknRadioButton();
      this.rbUtilizaBackup = new lib.Visual.Components.sknRadioButton();
      this.btnFechar = new lib.Visual.Components.sknButton();
      this.btnExecutarRestauracao = new lib.Visual.Components.sknButton();
      this.pbRestauracao = new lib.Visual.Components.sknProgressBar();
      this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
      this.sknGroupBox2 = new lib.Visual.Components.sknGroupBox();
      this.lblAtual = new lib.Visual.Components.sknLabel();
      this.lblTempoEstimado = new System.Windows.Forms.Label();
      this.sknGroupBox1.SuspendLayout();
      this.cmBancos.SuspendLayout();
      this.sknGroupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // sknGroupBox1
      // 
      this.sknGroupBox1.Controls.Add(this.lstBancos);
      this.sknGroupBox1.Controls.Add(this.btnUtilizarBackup);
      this.sknGroupBox1.Controls.Add(this.txtUtilizarBackup);
      this.sknGroupBox1.Controls.Add(this.rbCriarNovo);
      this.sknGroupBox1.Controls.Add(this.rbSelecionaBanco);
      this.sknGroupBox1.Controls.Add(this.rbUtilizaBackup);
      this.sknGroupBox1.Location = new System.Drawing.Point(12, 60);
      this.sknGroupBox1.Name = "sknGroupBox1";
      this.sknGroupBox1.Size = new System.Drawing.Size(559, 195);
      this.sknGroupBox1.TabIndex = 0;
      this.sknGroupBox1.TabStop = false;
      this.sknGroupBox1.Text = "Opções para Restauração";
      // 
      // lstBancos
      // 
      this.lstBancos.ContextMenuStrip = this.cmBancos;
      this.lstBancos.FormattingEnabled = true;
      this.lstBancos.Location = new System.Drawing.Point(208, 49);
      this.lstBancos.Name = "lstBancos";
      this.lstBancos.Size = new System.Drawing.Size(305, 108);
      this.lstBancos.TabIndex = 4;
      // 
      // cmBancos
      // 
      this.cmBancos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excluirToolStripMenuItem});
      this.cmBancos.Name = "cmBancos";
      this.cmBancos.Size = new System.Drawing.Size(153, 48);
      // 
      // excluirToolStripMenuItem
      // 
      this.excluirToolStripMenuItem.Name = "excluirToolStripMenuItem";
      this.excluirToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.excluirToolStripMenuItem.Text = "Excluir";
      this.excluirToolStripMenuItem.Click += new System.EventHandler(this.excluirToolStripMenuItem_Click);
      // 
      // btnUtilizarBackup
      // 
      this.btnUtilizarBackup.Location = new System.Drawing.Point(519, 20);
      this.btnUtilizarBackup.Name = "btnUtilizarBackup";
      this.btnUtilizarBackup.Size = new System.Drawing.Size(34, 23);
      this.btnUtilizarBackup.TabIndex = 2;
      this.btnUtilizarBackup.Text = "...";
      this.btnUtilizarBackup.UseVisualStyleBackColor = true;
      this.btnUtilizarBackup.Click += new System.EventHandler(this.btnUtilizarBackup_Click);
      // 
      // txtUtilizarBackup
      // 
      this.txtUtilizarBackup.AsDateTime = new System.DateTime(((long)(0)));
      this.txtUtilizarBackup.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.txtUtilizarBackup.AutoTab = true;
      this.txtUtilizarBackup.Location = new System.Drawing.Point(208, 22);
      this.txtUtilizarBackup.Name = "txtUtilizarBackup";
      this.txtUtilizarBackup.Size = new System.Drawing.Size(305, 20);
      this.txtUtilizarBackup.TabIndex = 1;
      this.txtUtilizarBackup.TextFormat = null;
      this.txtUtilizarBackup.TextType = lib.Visual.Components.enmTextType.String;
      // 
      // rbCriarNovo
      // 
      this.rbCriarNovo.Location = new System.Drawing.Point(6, 163);
      this.rbCriarNovo.Name = "rbCriarNovo";
      this.rbCriarNovo.Size = new System.Drawing.Size(221, 24);
      this.rbCriarNovo.TabIndex = 5;
      this.rbCriarNovo.Text = "Criar um novo banco de dados vazio";
      this.rbCriarNovo.UseVisualStyleBackColor = true;
      this.rbCriarNovo.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbSelecionaBanco
      // 
      this.rbSelecionaBanco.Location = new System.Drawing.Point(6, 49);
      this.rbSelecionaBanco.Name = "rbSelecionaBanco";
      this.rbSelecionaBanco.Size = new System.Drawing.Size(196, 24);
      this.rbSelecionaBanco.TabIndex = 3;
      this.rbSelecionaBanco.Text = "Selecionar outro banco de dados";
      this.rbSelecionaBanco.UseVisualStyleBackColor = true;
      this.rbSelecionaBanco.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // rbUtilizaBackup
      // 
      this.rbUtilizaBackup.Checked = true;
      this.rbUtilizaBackup.Location = new System.Drawing.Point(6, 19);
      this.rbUtilizaBackup.Name = "rbUtilizaBackup";
      this.rbUtilizaBackup.Size = new System.Drawing.Size(165, 24);
      this.rbUtilizaBackup.TabIndex = 0;
      this.rbUtilizaBackup.TabStop = true;
      this.rbUtilizaBackup.Text = "Utilizar arquivo de Backup";
      this.rbUtilizaBackup.UseVisualStyleBackColor = true;
      this.rbUtilizaBackup.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
      // 
      // btnFechar
      // 
      this.btnFechar.Location = new System.Drawing.Point(496, 274);
      this.btnFechar.Name = "btnFechar";
      this.btnFechar.Size = new System.Drawing.Size(75, 23);
      this.btnFechar.TabIndex = 3;
      this.btnFechar.Text = "Fechar";
      this.btnFechar.UseVisualStyleBackColor = true;
      this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
      // 
      // btnExecutarRestauracao
      // 
      this.btnExecutarRestauracao.Location = new System.Drawing.Point(12, 274);
      this.btnExecutarRestauracao.Name = "btnExecutarRestauracao";
      this.btnExecutarRestauracao.Size = new System.Drawing.Size(202, 23);
      this.btnExecutarRestauracao.TabIndex = 1;
      this.btnExecutarRestauracao.Text = "Executar a restauração de sistema";
      this.btnExecutarRestauracao.UseVisualStyleBackColor = true;
      this.btnExecutarRestauracao.Click += new System.EventHandler(this.btnExecutarRestauracao_Click);
      // 
      // pbRestauracao
      // 
      this.pbRestauracao.Location = new System.Drawing.Point(220, 274);
      this.pbRestauracao.Name = "pbRestauracao";
      this.pbRestauracao.Size = new System.Drawing.Size(270, 23);
      this.pbRestauracao.TabIndex = 2;
      // 
      // dlgOpen
      // 
      this.dlgOpen.FileName = "openFileDialog1";
      // 
      // sknGroupBox2
      // 
      this.sknGroupBox2.Controls.Add(this.lblAtual);
      this.sknGroupBox2.Location = new System.Drawing.Point(12, 12);
      this.sknGroupBox2.Name = "sknGroupBox2";
      this.sknGroupBox2.Size = new System.Drawing.Size(559, 42);
      this.sknGroupBox2.TabIndex = 4;
      this.sknGroupBox2.TabStop = false;
      this.sknGroupBox2.Text = "Conexão atual";
      // 
      // lblAtual
      // 
      this.lblAtual.AutoSize = true;
      this.lblAtual.Location = new System.Drawing.Point(39, 16);
      this.lblAtual.Name = "lblAtual";
      this.lblAtual.Size = new System.Drawing.Size(91, 13);
      this.lblAtual.TabIndex = 0;
      this.lblAtual.Text = "Connection String";
      // 
      // lblTempoEstimado
      // 
      this.lblTempoEstimado.Location = new System.Drawing.Point(12, 258);
      this.lblTempoEstimado.Name = "lblTempoEstimado";
      this.lblTempoEstimado.Size = new System.Drawing.Size(559, 13);
      this.lblTempoEstimado.TabIndex = 5;
      // 
      // Restore
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(583, 307);
      this.Controls.Add(this.lblTempoEstimado);
      this.Controls.Add(this.sknGroupBox2);
      this.Controls.Add(this.pbRestauracao);
      this.Controls.Add(this.sknGroupBox1);
      this.Controls.Add(this.btnExecutarRestauracao);
      this.Controls.Add(this.btnFechar);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "Restore";
      this.Text = "Restore";
      this.Load += new System.EventHandler(this.Restore_Load);
      this.sknGroupBox1.ResumeLayout(false);
      this.sknGroupBox1.PerformLayout();
      this.cmBancos.ResumeLayout(false);
      this.sknGroupBox2.ResumeLayout(false);
      this.sknGroupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private lib.Visual.Components.sknGroupBox sknGroupBox1;
    private lib.Visual.Components.sknListBox lstBancos;
    private lib.Visual.Components.sknButton btnUtilizarBackup;
    private lib.Visual.Components.sknTextBox txtUtilizarBackup;
    private lib.Visual.Components.sknRadioButton rbCriarNovo;
    private lib.Visual.Components.sknRadioButton rbSelecionaBanco;
    private lib.Visual.Components.sknRadioButton rbUtilizaBackup;
    private lib.Visual.Components.sknButton btnFechar;
    private lib.Visual.Components.sknButton btnExecutarRestauracao;
    private lib.Visual.Components.sknProgressBar pbRestauracao;
    private System.Windows.Forms.OpenFileDialog dlgOpen;
    private lib.Visual.Components.sknGroupBox sknGroupBox2;
    private lib.Visual.Components.sknLabel lblAtual;
    private System.Windows.Forms.ContextMenuStrip cmBancos;
    private System.Windows.Forms.ToolStripMenuItem excluirToolStripMenuItem;
    private System.Windows.Forms.Label lblTempoEstimado;
  }
}