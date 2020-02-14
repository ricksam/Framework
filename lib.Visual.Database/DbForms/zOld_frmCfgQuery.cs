using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;
using lib.Visual.Components;
using lib.Database;
using lib.Database.Query;
using lib.Database.Drivers;

namespace lib.Visual.Forms
{
  public partial class frmCfgQuery : lib.Visual.Models.frmBase
  {
    #region Constructor
    protected frmCfgQuery()
    {
      InitializeComponent();
    }
    #endregion

    public frmCfgQuery(Connection cnn)
      : this()
    {
      this.cnn = cnn;
    }

    #region Fields
    private Connection cnn { get; set; }
    private CfgQuery bs { get; set; }
    private DataTable Table { get; set; }
    #endregion

    #region Methods
    #region private void Abrir()
    private void Abrir() 
    {
      if (bs != null)
      { bs = null; }

      bs = new CfgQuery(Application.StartupPath, cmbAlias.Text);
      bs.Open();
      txtSql.Text = bs.Sql;
      cbTrazerPreenchido.Checked = bs.TrazerPreenchido;
      CarregaCampos();
      if (txtSql.Text != "")
      { Table = this.cnn.GetDataTable(txtSql.Text); }
    }
    #endregion

    #region private void CarregaCampos()
    private void CarregaCampos() 
    {
      grdCampos.Clear();
      grdCampos.AddColumn(new FieldColumn("Text", "Text", enmFieldType.String, 180));
      grdCampos.AddColumn(new FieldColumn("Name", "Name", enmFieldType.String, 180));
      grdCampos.AddColumn(new FieldColumn("Type", "Type", enmFieldType.String, 40));
      grdCampos.AddColumn(new FieldColumn("Size", "Size", enmFieldType.String, 40));
      grdCampos.AddColumn(new FieldColumn("Sensitive", "Sensitive", enmFieldType.String, 60));

      for (int i = 0; i < bs.Fields.Count; i++)
      { grdCampos.AddItem(bs.Fields[i]); }
    }
    #endregion

    #region private void AdicionaCampo()
    private void AdicionaCampo() 
    {
      if (Table == null) 
      {
        Msg.Warning("Realize um teste antes de adicionar os campos");
        return;
      }

      frmCfgQueryFields f = new frmCfgQueryFields(Table);
      if (f.Exec()) 
      {
        bs.Fields.Add(f.bs);
        grdCampos.AddItem(f.bs);
      }
    }
    #endregion

    #region private void RemoverCampo()
    private void RemoverCampo() 
    {
      if (grdCampos.SelectedRows.Count != 0) 
      {
        int idx = grdCampos.SelectedRows[0].Index;
        if (Msg.Question("Tem certesa que deseja remover este campo?")) 
        {
          bs.Fields.RemoveAt(idx);
          grdCampos.Rows.RemoveAt(idx);
        }
      }
    }
    #endregion

    #region private void AlterarCampo()
    private void AlterarCampo()
    {
      if (grdCampos.SelectedRows.Count != 0)
      {
        if (Table == null)
        {
          Msg.Warning("Realize um teste antes de adicionar os campos");
          return;
        }

        int idx = grdCampos.SelectedRows[0].Index;
        frmCfgQueryFields f = new frmCfgQueryFields(Table);
        f.bs.Assign(bs.Fields[idx]);

        if (f.Exec())
        {
          bs.Fields[idx].Assign(f.bs);
          grdCampos.AlterItem(idx, f.bs);
        }
      }
    }
    #endregion

    #region private void CarregaAlias()
    private void CarregaAlias() 
    {
      string xDir = Application.StartupPath + CfgQuery.QueryDirectory;
      if (!System.IO.Directory.Exists(xDir))
      { System.IO.Directory.CreateDirectory(xDir); }

      string[] lst = System.IO.Directory.GetFiles(xDir, "*.qry");

      cmbAlias.Items.Clear();
      for (int i = 0; i < lst.Length; i++)
      { cmbAlias.Items.Add(System.IO.Path.GetFileNameWithoutExtension(lst[i])); }

      if (cmbAlias.Items.Count != 0)
      { cmbAlias.SelectedIndex = 0; }
    }
    #endregion

    #region private void MoverCima()
    private void MoverCima() 
    {
      if (grdCampos.SelectedRows.Count != 0)
      {
        int idx = grdCampos.SelectedRows[0].Index;
        if (idx != 0)
        { MoverIdxBaixo(idx - 1); }
      }
    }
    #endregion

    #region private void MoverBaixo()
    private void MoverBaixo() 
    {
      if (grdCampos.SelectedRows.Count != 0)
      {
        int idx = grdCampos.SelectedRows[0].Index;
        if (idx != (grdCampos.Rows.Count - 1))
        { MoverIdxBaixo(idx); }
      }
    }
    #endregion

    #region private void MoverIdxBaixo(int idx)
    private void MoverIdxBaixo(int idx)
    {
      int next_idx = idx + 2;

      // Adiciona após o próximo item
      bs.Fields.Insert(next_idx, bs.Fields[idx]);
      grdCampos.Rows.Insert(next_idx, new DataGridViewRow());
      grdCampos.AlterItem(next_idx, bs.Fields[next_idx]);

      // Remove a posição atual
      bs.Fields.RemoveAt(idx);
      grdCampos.Rows.RemoveAt(idx);
    }
    #endregion

    #region private void RefazerCampos()
    private void RefazerCampos()
    {
      Table = this.cnn.GetDataTable(txtSql.Text);
      this.bs.Fields.Clear();
      for (int i = 0; i < Table.Columns.Count; i++)
      { this.bs.Fields.Add(new FieldColumn(Table.Columns[i])); }
      CarregaCampos();
    }
    #endregion

    #region private void Testar()
    private void Testar()
    {
      Table = this.cnn.GetDataTable(txtSql.Text);
      Msg.Information("Executado com sucesso");
    }
    #endregion

    #region private void Salvar()
    private void Salvar() 
    {
      bs.TrazerPreenchido = cbTrazerPreenchido.Checked;
      bs.Sql = txtSql.Text;
      bs.Save();
      Msg.Information("Dados gravados com sucesso!");
    }
    #endregion
    #endregion

    #region Events
    private void btnAbrir_Click(object sender, EventArgs e)
    {
      Abrir();
    }

    private void frmCfgQuery_Load(object sender, EventArgs e)
    {
      CarregaAlias();
    }

    private void btnAdicionar_Click(object sender, EventArgs e)
    {
      AdicionaCampo();
    }

    private void btnRemover_Click(object sender, EventArgs e)
    {
      RemoverCampo();
    }

    private void grdCampos_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter)
      { AlterarCampo(); }
    }

    private void btnSalvar_Click(object sender, EventArgs e)
    {
      Salvar();
    }

    private void grdCampos_DoubleClick(object sender, EventArgs e)
    {
      AlterarCampo();
    }

    private void btnBaixo_Click(object sender, EventArgs e)
    {
      MoverBaixo();
    }

    private void btnCima_Click(object sender, EventArgs e)
    {
      MoverCima();
    }

    private void btnRefazer_Click(object sender, EventArgs e)
    {
      RefazerCampos();
    }

    private void btnTestar_Click(object sender, EventArgs e)
    {
      Testar();
    }
    #endregion
  }
}
