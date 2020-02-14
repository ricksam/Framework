using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Database;
using lib.Database.Query;
using lib.Class;

namespace lib.Visual.Forms
{
  public partial class frmQuery :lib.Visual.Models.frmBase
  {
    protected frmQuery()
    {
      // Permite exibir a tela em tempo de desenvolvimento
      InitializeComponent();
    }

    public frmQuery(Connection cnn, string QueryName)
      : this()
    {
      this.Cfg = new CfgQuery(Application.StartupPath, QueryName);
      this.CfgUser = new CfgQueryUser(Application.StartupPath, QueryName);
      this.cnn = cnn;
      LoadCfg(QueryName);
    }

    public CfgQueryUser CfgUser { get; set; }
    public CfgQuery Cfg { get; set; }
    private Connection cnn { get; set; }

    private void LoadCfg(string Alias)
    {
      Cfg.Open();
      CarregaCombo();
      CarregaColunas();
      if (Cfg.TrazerPreenchido)
      { Pesquisar(); }
    }

    private void CarregaCombo() 
    {
      cmbCampo.Items.Clear();
      for (int i = 0; i < Cfg.Fields.Count; i++)
      { cmbCampo.Items.Add(Cfg.Fields[i].Text); }
      SelecionaCampo(0);
    }

    private void SelecionaCampo(int Index) 
    {
      if (Index < cmbCampo.Items.Count) 
      {
        cmbCampo.SelectedIndex = Index;
        txtCriterio.CharacterCasing = 
          Cfg.Fields[Index].Sensitive ? 
          CharacterCasing.Normal : 
          CharacterCasing.Upper;
      }
    }

    private void CarregaColunas() 
    {
      grdItens.Clear();
      grdItens.AddColumns(Cfg.Fields);
    }

    private string getCriterio() 
    {
      return
        " where " +
        Cfg.Fields[cmbCampo.SelectedIndex].Name +
        " like " +
        cnn.dbu.Quoted(txtCriterio.Text.Replace(",", "%") + "%");
    }

    private void Pesquisar() 
    {
      DataSet D = cnn.GetDataSet(Cfg.Sql + getCriterio());
      grdItens.DataSource = D;
      grdItens.DataMember = "SQL";
      grdItens.Select();
    }

    public lib.Class.Conversion GetField(string FieldName) 
    {
      return grdItens.GetField(FieldName);
    }

    private void frmQuery_Resize(object sender, EventArgs e)
    {
      txtCriterio.Width = this.Width - 266;
    }

    private void btnPesquisa_Click(object sender, EventArgs e)
    {
      Pesquisar();
    }

    private void txtCriterio_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter)
      {
        e.Handled = true;
        Pesquisar();
      }
      else if (e.KeyData == Keys.Up || e.KeyData == Keys.Down)
      { cmbCampo.Select(); }
    }

    private void grdItens_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == ((char)13))
      { e.Handled = true; }
      else if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ((char)8) || e.KeyChar == '%') 
      {
        e.Handled = true;
        txtCriterio.Select();
        if (e.KeyChar == ((char)8) && txtCriterio.Text.Length != 0)
        { 
          txtCriterio.Text = txtCriterio.Text.Remove(txtCriterio.Text.Length - 1, 1);
          txtCriterio.SelectionStart = txtCriterio.Text.Length;
          txtCriterio.SelectionLength = 0;
        }
        else
        {
          txtCriterio.Text = e.KeyChar.ToString();
          txtCriterio.SelectionStart = 1;
          txtCriterio.SelectionLength = 1;
        }
      }
    }

    private void frmQuery_Load(object sender, EventArgs e)
    {
      // Carrega Config User
      if (CfgUser.Open()) 
      {
        SelecionaCampo(CfgUser.FieldIndex);
        txtCriterio.Text = CfgUser.Filter;
        txtCriterio.Select();
      }
    }

    private void frmQuery_FormClosed(object sender, FormClosedEventArgs e)
    {
      // Salva Config User
      CfgUser.FieldIndex = cmbCampo.SelectedIndex;
      CfgUser.Filter = txtCriterio.Text;
      CfgUser.Save();
    }
  }
}
