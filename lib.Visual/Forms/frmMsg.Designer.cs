namespace lib.Visual.Forms
{
  partial class frmMsg
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
      this.pnlContext = new lib.Visual.Components.sknPanel();
      this.imgMsg = new System.Windows.Forms.PictureBox();
      this.lblMensagem = new lib.Visual.Components.sknLabel();
      this.pnlBottom = new lib.Visual.Components.sknPanel();
      this.groupBox1 = new lib.Visual.Components.sknGroupBox();
      this.btnSim = new lib.Visual.Components.sknButton();
      this.btnNao = new lib.Visual.Components.sknButton();
      this.pnlContext.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.imgMsg)).BeginInit();
      this.pnlBottom.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlContext
      // 
      this.pnlContext.Controls.Add(this.imgMsg);
      this.pnlContext.Controls.Add(this.lblMensagem);
      this.pnlContext.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlContext.Location = new System.Drawing.Point(0, 0);
      this.pnlContext.Name = "pnlContext";
      this.pnlContext.Size = new System.Drawing.Size(306, 90);
      this.pnlContext.TabIndex = 0;
      // 
      // imgMsg
      // 
      this.imgMsg.Image = global::lib.Visual.Properties.Resources.Symbol_Information;
      this.imgMsg.Location = new System.Drawing.Point(12, 12);
      this.imgMsg.Name = "imgMsg";
      this.imgMsg.Size = new System.Drawing.Size(64, 64);
      this.imgMsg.TabIndex = 1;
      this.imgMsg.TabStop = false;
      this.imgMsg.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // lblMensagem
      // 
      this.lblMensagem.AutoSize = true;
      this.lblMensagem.Location = new System.Drawing.Point(92, 39);
      this.lblMensagem.Name = "lblMensagem";
      this.lblMensagem.Size = new System.Drawing.Size(56, 13);
      this.lblMensagem.TabIndex = 0;
      this.lblMensagem.Text = "sknLabel1";
      // 
      // pnlBottom
      // 
      this.pnlBottom.Controls.Add(this.groupBox1);
      this.pnlBottom.Controls.Add(this.btnSim);
      this.pnlBottom.Controls.Add(this.btnNao);
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 90);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(306, 45);
      this.pnlBottom.TabIndex = 3;
      // 
      // groupBox1
      // 
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(306, 2);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      // 
      // btnSim
      // 
      this.btnSim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSim.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnSim.Location = new System.Drawing.Point(108, 8);
      this.btnSim.Name = "btnSim";
      this.btnSim.Size = new System.Drawing.Size(90, 25);
      this.btnSim.TabIndex = 2;
      this.btnSim.Text = "&Sim";
      this.btnSim.UseVisualStyleBackColor = true;
      // 
      // btnNao
      // 
      this.btnNao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNao.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnNao.Location = new System.Drawing.Point(204, 8);
      this.btnNao.Name = "btnNao";
      this.btnNao.Size = new System.Drawing.Size(90, 25);
      this.btnNao.TabIndex = 1;
      this.btnNao.Text = "&Não";
      this.btnNao.UseVisualStyleBackColor = true;
      // 
      // frmMsg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(306, 135);
      this.ControlBox = false;
      this.Controls.Add(this.pnlContext);
      this.Controls.Add(this.pnlBottom);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmMsg";
      this.Text = "frmMsg";
      this.Load += new System.EventHandler(this.frmMsg_Load);
      this.pnlContext.ResumeLayout(false);
      this.pnlContext.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.imgMsg)).EndInit();
      this.pnlBottom.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    public lib.Visual.Components.sknPanel pnlContext;
    private lib.Visual.Components.sknLabel lblMensagem;
    public lib.Visual.Components.sknPanel pnlBottom;
    private lib.Visual.Components.sknGroupBox groupBox1;
    public lib.Visual.Components.sknButton btnSim;
    public lib.Visual.Components.sknButton btnNao;
    private System.Windows.Forms.PictureBox imgMsg;

  }
}