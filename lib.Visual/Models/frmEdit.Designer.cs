namespace lib.Visual.Models
{
    partial class frmEdit
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
            this.pnlBottom = new lib.Visual.Components.sknPanel();
            this.groupBox1 = new lib.Visual.Components.sknGroupBox();
            this.btnConfirm = new lib.Visual.Components.sknButton();
            this.btnCancel = new lib.Visual.Components.sknButton();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContext
            // 
            this.pnlContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContext.Location = new System.Drawing.Point(0, 0);
            this.pnlContext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlContext.Name = "pnlContext";
            this.pnlContext.Size = new System.Drawing.Size(441, 267);
            this.pnlContext.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.groupBox1);
            this.pnlBottom.Controls.Add(this.btnConfirm);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 267);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(441, 55);
            this.pnlBottom.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(441, 2);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "sknGroupBox";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(177, 10);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(120, 31);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "&Confirmar";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.Confirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(305, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 31);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Fechar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(441, 322);
            this.Controls.Add(this.pnlContext);
            this.Controls.Add(this.pnlBottom);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "frmEdit";
            this.Text = "frmEdit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmEdit_Load);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public lib.Visual.Components.sknPanel pnlContext;
        public lib.Visual.Components.sknPanel pnlBottom;
        public lib.Visual.Components.sknButton btnConfirm;
        public lib.Visual.Components.sknButton btnCancel;
        private lib.Visual.Components.sknGroupBox groupBox1;

    }
}