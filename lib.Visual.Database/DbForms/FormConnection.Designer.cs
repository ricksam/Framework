﻿namespace lib.Visual.Forms
{
  partial class FormConnection
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new lib.Visual.Components.sknTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBanco = new lib.Visual.Components.sknTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsuario = new lib.Visual.Components.sknTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSenha = new lib.Visual.Components.sknTextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.cbWindowsAutenticate = new lib.Visual.Components.sknCheckBox();
            this.btnDB = new lib.Visual.Components.sknButton();
            this.lstConnections = new System.Windows.Forms.ListBox();
            this.cmHistorico = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.apagarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apagarHistóricoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlContext.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.cmHistorico.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContext
            // 
            this.pnlContext.Controls.Add(this.groupBox2);
            this.pnlContext.Controls.Add(this.panel1);
            this.pnlContext.Size = new System.Drawing.Size(384, 392);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Location = new System.Drawing.Point(0, 392);
            this.pnlBottom.Size = new System.Drawing.Size(384, 30);
            this.pnlBottom.TabIndex = 10;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(1005, 8);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1101, 8);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo do Banco";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Servidor";
            // 
            // txtServer
            // 
            this.txtServer.AsDateTime = new System.DateTime(((long)(0)));
            this.txtServer.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtServer.AutoTab = true;
            this.txtServer.Location = new System.Drawing.Point(12, 62);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(169, 20);
            this.txtServer.TabIndex = 6;
            this.txtServer.TextFormat = null;
            this.txtServer.TextType = lib.Visual.Components.enmTextType.String;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Banco de Dados";
            // 
            // txtBanco
            // 
            this.txtBanco.AsDateTime = new System.DateTime(((long)(0)));
            this.txtBanco.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBanco.AutoTab = true;
            this.txtBanco.Location = new System.Drawing.Point(187, 62);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(154, 20);
            this.txtBanco.TabIndex = 8;
            this.txtBanco.TextFormat = null;
            this.txtBanco.TextType = lib.Visual.Components.enmTextType.String;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Usuário";
            // 
            // txtUsuario
            // 
            this.txtUsuario.AsDateTime = new System.DateTime(((long)(0)));
            this.txtUsuario.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUsuario.AutoTab = true;
            this.txtUsuario.Location = new System.Drawing.Point(12, 101);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(169, 20);
            this.txtUsuario.TabIndex = 11;
            this.txtUsuario.TextFormat = null;
            this.txtUsuario.TextType = lib.Visual.Components.enmTextType.String;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.AsDateTime = new System.DateTime(((long)(0)));
            this.txtSenha.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSenha.AutoTab = true;
            this.txtSenha.Location = new System.Drawing.Point(187, 101);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(185, 20);
            this.txtSenha.TabIndex = 13;
            this.txtSenha.TextFormat = null;
            this.txtSenha.TextType = lib.Visual.Components.enmTextType.String;
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(12, 19);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(169, 21);
            this.cmbTipo.TabIndex = 1;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // cbWindowsAutenticate
            // 
            this.cbWindowsAutenticate.Location = new System.Drawing.Point(190, 19);
            this.cbWindowsAutenticate.Name = "cbWindowsAutenticate";
            this.cbWindowsAutenticate.Size = new System.Drawing.Size(184, 24);
            this.cbWindowsAutenticate.TabIndex = 4;
            this.cbWindowsAutenticate.Text = "Usar autenticação pelo windows";
            this.cbWindowsAutenticate.UseVisualStyleBackColor = true;
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(347, 60);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(25, 23);
            this.btnDB.TabIndex = 9;
            this.btnDB.Text = "...";
            this.btnDB.UseVisualStyleBackColor = true;
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // lstConnections
            // 
            this.lstConnections.ContextMenuStrip = this.cmHistorico;
            this.lstConnections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstConnections.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstConnections.FormattingEnabled = true;
            this.lstConnections.HorizontalScrollbar = true;
            this.lstConnections.Location = new System.Drawing.Point(3, 49);
            this.lstConnections.Name = "lstConnections";
            this.lstConnections.ScrollAlwaysVisible = true;
            this.lstConnections.Size = new System.Drawing.Size(378, 142);
            this.lstConnections.TabIndex = 14;
            this.lstConnections.SelectedIndexChanged += new System.EventHandler(this.lstConnections_SelectedIndexChanged);
            this.lstConnections.DoubleClick += new System.EventHandler(this.lstConnections_DoubleClick);
            this.lstConnections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstConnections_KeyDown);
            // 
            // cmHistorico
            // 
            this.cmHistorico.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apagarToolStripMenuItem,
            this.apagarHistóricoToolStripMenuItem});
            this.cmHistorico.Name = "cmHistorico";
            this.cmHistorico.Size = new System.Drawing.Size(180, 70);
            // 
            // apagarToolStripMenuItem
            // 
            this.apagarToolStripMenuItem.Name = "apagarToolStripMenuItem";
            this.apagarToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.apagarToolStripMenuItem.Text = "Apagar Selecionado";
            this.apagarToolStripMenuItem.Click += new System.EventHandler(this.apagarToolStripMenuItem_Click);
            // 
            // apagarHistóricoToolStripMenuItem
            // 
            this.apagarHistóricoToolStripMenuItem.Name = "apagarHistóricoToolStripMenuItem";
            this.apagarHistóricoToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.apagarHistóricoToolStripMenuItem.Text = "Apagar Histórico";
            this.apagarHistóricoToolStripMenuItem.Click += new System.EventHandler(this.apagarHistóricoToolStripMenuItem_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.CheckPathExists = false;
            this.dlgSave.OverwritePrompt = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Connection String";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(12, 140);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(360, 48);
            this.txtConnectionString.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstConnections);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 194);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Histórico de Conexões";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(3, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(378, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Pesquisa no histórico";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtConnectionString);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtServer);
            this.panel1.Controls.Add(this.btnDB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbWindowsAutenticate);
            this.panel1.Controls.Add(this.txtBanco);
            this.panel1.Controls.Add(this.cmbTipo);
            this.panel1.Controls.Add(this.txtUsuario);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtSenha);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 198);
            this.panel1.TabIndex = 19;
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 422);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.Name = "FormConnection";
            this.Text = "DB";
            this.Load += new System.EventHandler(this.FormConnection_Load);
            this.Controls.SetChildIndex(this.pnlBottom, 0);
            this.Controls.SetChildIndex(this.pnlContext, 0);
            this.pnlContext.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.cmHistorico.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private global::lib.Visual.Components.sknTextBox txtServer;
    private System.Windows.Forms.Label label3;
    private global::lib.Visual.Components.sknTextBox txtBanco;
    private System.Windows.Forms.Label label4;
    private global::lib.Visual.Components.sknTextBox txtUsuario;
    private System.Windows.Forms.Label label5;
    private global::lib.Visual.Components.sknTextBox txtSenha;
    private System.Windows.Forms.ComboBox cmbTipo;
    private Components.sknCheckBox cbWindowsAutenticate;
    private Components.sknButton btnDB;
    private System.Windows.Forms.ListBox lstConnections;
    private System.Windows.Forms.ContextMenuStrip cmHistorico;
    private System.Windows.Forms.ToolStripMenuItem apagarHistóricoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem apagarToolStripMenuItem;
    private System.Windows.Forms.SaveFileDialog dlgSave;
    private System.Windows.Forms.TextBox txtConnectionString;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label6;
  }
}