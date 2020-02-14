namespace Teste
{
  partial class TesteWebRequest
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
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtPostData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtContentType = new System.Windows.Forms.ComboBox();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtHeaderValue = new System.Windows.Forms.TextBox();
            this.txtHeaderKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbLink = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtResposta = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 37);
            this.button1.TabIndex = 6;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 317);
            this.panel1.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txtPostData);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 126);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(734, 145);
            this.panel8.TabIndex = 17;
            // 
            // txtPostData
            // 
            this.txtPostData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPostData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostData.Location = new System.Drawing.Point(0, 13);
            this.txtPostData.Multiline = true;
            this.txtPostData.Name = "txtPostData";
            this.txtPostData.Size = new System.Drawing.Size(734, 132);
            this.txtPostData.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "PostData";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel3);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 83);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(734, 43);
            this.panel7.TabIndex = 16;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtContentType);
            this.panel3.Controls.Add(this.cmbMethod);
            this.panel3.Controls.Add(this.cmbEncoding);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 13);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(734, 31);
            this.panel3.TabIndex = 12;
            // 
            // txtContentType
            // 
            this.txtContentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContentType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContentType.FormattingEnabled = true;
            this.txtContentType.Items.AddRange(new object[] {
            "application/x-www-form-urlencoded",
            "multipart/form-data",
            "application/json",
            "binary/octet-stream",
            "text/plain"});
            this.txtContentType.Location = new System.Drawing.Point(0, 0);
            this.txtContentType.Name = "txtContentType";
            this.txtContentType.Size = new System.Drawing.Size(406, 28);
            this.txtContentType.TabIndex = 4;
            this.txtContentType.Text = "application/x-www-form-urlencoded";
            // 
            // cmbMethod
            // 
            this.cmbMethod.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE"});
            this.cmbMethod.Location = new System.Drawing.Point(406, 0);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(128, 28);
            this.cmbMethod.TabIndex = 10;
            this.cmbMethod.Text = "GET";
            // 
            // cmbEncoding
            // 
            this.cmbEncoding.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbEncoding.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            "ASCII",
            "BigEndianUnicode",
            "Default",
            "Unicode",
            "UTF32",
            "UTF7",
            "UTF8"});
            this.cmbEncoding.Location = new System.Drawing.Point(534, 0);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(200, 28);
            this.cmbEncoding.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Content Type";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 42);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(734, 41);
            this.panel5.TabIndex = 15;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtHeaderValue);
            this.panel6.Controls.Add(this.txtHeaderKey);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 13);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(734, 28);
            this.panel6.TabIndex = 16;
            // 
            // txtHeaderValue
            // 
            this.txtHeaderValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHeaderValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeaderValue.Location = new System.Drawing.Point(240, 0);
            this.txtHeaderValue.Multiline = true;
            this.txtHeaderValue.Name = "txtHeaderValue";
            this.txtHeaderValue.Size = new System.Drawing.Size(494, 28);
            this.txtHeaderValue.TabIndex = 16;
            // 
            // txtHeaderKey
            // 
            this.txtHeaderKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtHeaderKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeaderKey.Location = new System.Drawing.Point(0, 0);
            this.txtHeaderKey.Multiline = true;
            this.txtHeaderKey.Name = "txtHeaderKey";
            this.txtHeaderKey.Size = new System.Drawing.Size(240, 28);
            this.txtHeaderKey.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Header";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbLink);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(734, 42);
            this.panel4.TabIndex = 14;
            // 
            // cmbLink
            // 
            this.cmbLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLink.FormattingEnabled = true;
            this.cmbLink.Location = new System.Drawing.Point(0, 13);
            this.cmbLink.Name = "cmbLink";
            this.cmbLink.Size = new System.Drawing.Size(734, 28);
            this.cmbLink.TabIndex = 10;
            this.cmbLink.SelectedIndexChanged += new System.EventHandler(this.cmbLink_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Link";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 271);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(734, 46);
            this.panel2.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(84, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "Send File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 323);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(734, 338);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtResposta);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(726, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HTML Response";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtResposta
            // 
            this.txtResposta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResposta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResposta.Location = new System.Drawing.Point(3, 3);
            this.txtResposta.Multiline = true;
            this.txtResposta.Name = "txtResposta";
            this.txtResposta.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResposta.Size = new System.Drawing.Size(720, 306);
            this.txtResposta.TabIndex = 0;
            this.txtResposta.TextChanged += new System.EventHandler(this.txtResposta_TextChanged);
            this.txtResposta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResposta_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(726, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "WEB Response";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(720, 306);
            this.webBrowser1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 317);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(734, 6);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // TesteWebRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "TesteWebRequest";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TesteWebRequest_FormClosing);
            this.Load += new System.EventHandler(this.TesteWebRequest_Load);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog dlgOpen;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TextBox txtResposta;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Panel panel8;
    private System.Windows.Forms.TextBox txtPostData;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panel7;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.ComboBox txtContentType;
    private System.Windows.Forms.ComboBox cmbEncoding;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.TextBox txtHeaderValue;
    private System.Windows.Forms.TextBox txtHeaderKey;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.ComboBox cmbLink;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbMethod;


  }
}

