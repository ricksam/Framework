namespace lib.Visual.Forms
{
  partial class frmQuery
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      this.pnlTop = new lib.Visual.Components.sknPanel();
      this.btnPesquisa = new lib.Visual.Components.sknButton();
      this.txtCriterio = new lib.Visual.Components.sknTextBox();
      this.cmbCampo = new lib.Visual.Components.sknComboBox();
      this.pnlBottom = new lib.Visual.Components.sknPanel();
      this.sknPanel3 = new lib.Visual.Components.sknPanel();
      this.grdItens = new lib.Visual.Components.sknGrid();
      this.lblResult = new lib.Visual.Components.sknLabel();
      this.pnlTop.SuspendLayout();
      this.sknPanel3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdItens)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlTop
      // 
      this.pnlTop.Controls.Add(this.btnPesquisa);
      this.pnlTop.Controls.Add(this.txtCriterio);
      this.pnlTop.Controls.Add(this.cmbCampo);
      this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlTop.Location = new System.Drawing.Point(0, 0);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Size = new System.Drawing.Size(542, 46);
      this.pnlTop.TabIndex = 0;
      // 
      // btnPesquisa
      // 
      this.btnPesquisa.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.btnPesquisa.Location = new System.Drawing.Point(454, 10);
      this.btnPesquisa.Name = "btnPesquisa";
      this.btnPesquisa.Size = new System.Drawing.Size(76, 23);
      this.btnPesquisa.TabIndex = 2;
      this.btnPesquisa.Text = "&Pesquisar";
      this.btnPesquisa.UseVisualStyleBackColor = true;
      this.btnPesquisa.Click += new System.EventHandler(this.btnPesquisa_Click);
      // 
      // txtCriterio
      // 
      this.txtCriterio.AsDateTime = new System.DateTime(((long)(0)));
      this.txtCriterio.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.txtCriterio.AutoTab = false;
      this.txtCriterio.Location = new System.Drawing.Point(164, 12);
      this.txtCriterio.Name = "txtCriterio";
      this.txtCriterio.Size = new System.Drawing.Size(284, 20);
      this.txtCriterio.TabIndex = 1;
      this.txtCriterio.TextFormat = null;
      this.txtCriterio.TextType = lib.Visual.Components.enmTextType.String;
      this.txtCriterio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCriterio_KeyDown);
      // 
      // cmbCampo
      // 
      this.cmbCampo.AutoTab = true;
      this.cmbCampo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cmbCampo.FormattingEnabled = true;
      this.cmbCampo.Location = new System.Drawing.Point(12, 12);
      this.cmbCampo.Name = "cmbCampo";
      this.cmbCampo.Size = new System.Drawing.Size(146, 21);
      this.cmbCampo.TabIndex = 0;
      // 
      // pnlBottom
      // 
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 337);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(542, 19);
      this.pnlBottom.TabIndex = 1;
      // 
      // sknPanel3
      // 
      this.sknPanel3.Controls.Add(this.grdItens);
      this.sknPanel3.Controls.Add(this.lblResult);
      this.sknPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sknPanel3.Location = new System.Drawing.Point(0, 46);
      this.sknPanel3.Name = "sknPanel3";
      this.sknPanel3.Size = new System.Drawing.Size(542, 291);
      this.sknPanel3.TabIndex = 2;
      // 
      // grdItens
      // 
      this.grdItens.AllowUserToAddRows = false;
      this.grdItens.AllowUserToOrderColumns = true;
      this.grdItens.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
      this.grdItens.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.grdItens.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.grdItens.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
      this.grdItens.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grdItens.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
      this.grdItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.grdItens.DefaultCellStyle = dataGridViewCellStyle3;
      this.grdItens.Dock = System.Windows.Forms.DockStyle.Fill;
      this.grdItens.EnableHeadersVisualStyles = false;
      this.grdItens.Location = new System.Drawing.Point(0, 13);
      this.grdItens.MultiSelect = false;
      this.grdItens.Name = "grdItens";
      this.grdItens.ReadOnly = true;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.grdItens.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
      this.grdItens.RowHeadersVisible = false;
      this.grdItens.RowHeadersWidth = 5;
      this.grdItens.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
      this.grdItens.RowsDefaultCellStyle = dataGridViewCellStyle5;
      this.grdItens.RowTemplate.Height = 15;
      this.grdItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.grdItens.Size = new System.Drawing.Size(542, 278);
      this.grdItens.TabIndex = 0;
      this.grdItens.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdItens_KeyPress);
      // 
      // lblResult
      // 
      this.lblResult.AutoSize = true;
      this.lblResult.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblResult.Location = new System.Drawing.Point(0, 0);
      this.lblResult.Name = "lblResult";
      this.lblResult.Size = new System.Drawing.Size(0, 13);
      this.lblResult.TabIndex = 1;
      this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // frmQuery
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(542, 356);
      this.Controls.Add(this.sknPanel3);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.pnlTop);
      this.Name = "frmQuery";
      this.Text = "Pesquisa";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmQuery_FormClosed);
      this.Load += new System.EventHandler(this.frmQuery_Load);
      this.pnlTop.ResumeLayout(false);
      this.pnlTop.PerformLayout();
      this.sknPanel3.ResumeLayout(false);
      this.sknPanel3.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.grdItens)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private lib.Visual.Components.sknButton btnPesquisa;
    private lib.Visual.Components.sknTextBox txtCriterio;
    private lib.Visual.Components.sknComboBox cmbCampo;
    private lib.Visual.Components.sknLabel lblResult;
    public lib.Visual.Components.sknPanel pnlBottom;
    public lib.Visual.Components.sknPanel pnlTop;
    public lib.Visual.Components.sknGrid grdItens;
    public lib.Visual.Components.sknPanel sknPanel3;
  }
}