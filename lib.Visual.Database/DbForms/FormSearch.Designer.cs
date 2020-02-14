namespace lib.Visual.Forms
{
  partial class FormSearch
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
      this.pnlPesquisa = new lib.Visual.Components.sknPanel();
      this.btnPesquisar = new lib.Visual.Components.sknButton();
      this.txtPesquisa = new lib.Visual.Components.sknTextBox();
      this.sknLabel1 = new lib.Visual.Components.sknLabel();
      this.pnlBottom = new lib.Visual.Components.sknPanel();
      this.Grid = new lib.Visual.Components.sknGrid();
      this.pnlPesquisa.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlPesquisa
      // 
      this.pnlPesquisa.Controls.Add(this.btnPesquisar);
      this.pnlPesquisa.Controls.Add(this.txtPesquisa);
      this.pnlPesquisa.Controls.Add(this.sknLabel1);
      this.pnlPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlPesquisa.Location = new System.Drawing.Point(0, 0);
      this.pnlPesquisa.Name = "pnlPesquisa";
      this.pnlPesquisa.Size = new System.Drawing.Size(734, 58);
      this.pnlPesquisa.TabIndex = 0;
      // 
      // btnPesquisar
      // 
      this.btnPesquisar.Location = new System.Drawing.Point(402, 23);
      this.btnPesquisar.Name = "btnPesquisar";
      this.btnPesquisar.Size = new System.Drawing.Size(75, 23);
      this.btnPesquisar.TabIndex = 2;
      this.btnPesquisar.Text = "Pesquisar";
      this.btnPesquisar.UseVisualStyleBackColor = true;
      this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
      // 
      // txtPesquisa
      // 
      this.txtPesquisa.AsDateTime = new System.DateTime(((long)(0)));
      this.txtPesquisa.AsDecimal = new decimal(new int[] {
            0,
            0,
            0,
            0});
      this.txtPesquisa.AutoTab = true;
      this.txtPesquisa.Location = new System.Drawing.Point(12, 25);
      this.txtPesquisa.Name = "txtPesquisa";
      this.txtPesquisa.Size = new System.Drawing.Size(384, 20);
      this.txtPesquisa.TabIndex = 1;
      this.txtPesquisa.Text = "0";
      this.txtPesquisa.TextFormat = null;
      this.txtPesquisa.TextType = lib.Visual.Components.enmTextType.String;
      this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged);
      this.txtPesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquisa_KeyDown);
      // 
      // sknLabel1
      // 
      this.sknLabel1.AutoSize = true;
      this.sknLabel1.Location = new System.Drawing.Point(12, 9);
      this.sknLabel1.Name = "sknLabel1";
      this.sknLabel1.Size = new System.Drawing.Size(53, 13);
      this.sknLabel1.TabIndex = 0;
      this.sknLabel1.Text = "Pesquisar";
      // 
      // pnlBottom
      // 
      this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlBottom.Location = new System.Drawing.Point(0, 324);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Size = new System.Drawing.Size(734, 38);
      this.pnlBottom.TabIndex = 1;
      // 
      // Grid
      // 
      this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.Grid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Grid.Location = new System.Drawing.Point(0, 58);
      this.Grid.Name = "Grid";
      this.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
      this.Grid.Size = new System.Drawing.Size(734, 266);
      this.Grid.TabIndex = 2;
      this.Grid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GridPesquisa_KeyPress);
      // 
      // FormSearch
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(734, 362);
      this.Controls.Add(this.Grid);
      this.Controls.Add(this.pnlBottom);
      this.Controls.Add(this.pnlPesquisa);
      this.Name = "FormSearch";
      this.Text = "FormSearch";
      this.Load += new System.EventHandler(this.FormSearch_Load);
      this.pnlPesquisa.ResumeLayout(false);
      this.pnlPesquisa.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private lib.Visual.Components.sknPanel pnlPesquisa;
    private lib.Visual.Components.sknButton btnPesquisar;
    private lib.Visual.Components.sknLabel sknLabel1;
    public lib.Visual.Components.sknGrid Grid;
    public Components.sknPanel pnlBottom;
    protected Components.sknTextBox txtPesquisa;
  }
}