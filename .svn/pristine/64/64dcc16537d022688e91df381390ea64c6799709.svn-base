using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Visual.Forms;
using lib.Visual.Components;

namespace lib.Visual.Models
{
  public partial class frmBaseCadastro  : lib.Visual.Models.frmBase
  {
    public frmBaseCadastro()
    {
      InitializeComponent();
    }

    private enmModo ModoAtual { get; set; }
    protected enum enmModo { Lista, Edicao }    
    public event FormSearch.OnSearch_Handle Search;

    #region Methods
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape)
      { this.Close(); }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected virtual void OnNewRecord()
    {
      Habilitar(enmModo.Edicao);
      btnNovo.Enabled = false;
    }

    protected virtual void OnAlterRecord(sknGrid Grid) 
    { Habilitar(enmModo.Edicao); }

    protected virtual void OnRemoveRecord()
    { Habilitar(enmModo.Lista); }

    protected virtual void OnConfirm()
    {
      OnNewRecord();
      Habilitar(enmModo.Lista);
    }

    protected virtual void OnCancel() 
    {
      if (Msg.Question("Tem certeza que deseja cancelar?"))
      {
        OnNewRecord();
        Habilitar(enmModo.Lista);
      }
    }
    
    protected void Habilitar(enmModo Modo)
    {
      ModoAtual = Modo;
      pnlEdit.Enabled = Modo == enmModo.Edicao;
      btnNovo.Enabled = Modo == enmModo.Lista;
      btnAlterar.Enabled = Modo == enmModo.Lista;
      btnRemover.Enabled = Modo == enmModo.Edicao;
      btnSalvar.Enabled = Modo == enmModo.Edicao;
      btnCancelar.Enabled = Modo == enmModo.Edicao;
    }
    #endregion

    #region Events
    private void btnNovo_Click(object sender, EventArgs e)
    {
      OnNewRecord();
    }

    private void btnAlterar_Click(object sender, EventArgs e)
    {
      FormQuery fs = new FormQuery();
      fs.Icon = this.Icon;
      fs.OnSearch = this.Search;
      if (fs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      { OnAlterRecord(fs.Grid); }
    }

    private void btnRemover_Click(object sender, EventArgs e)
    {
       OnRemoveRecord();
    }

    private void BaseCadastro_Load(object sender, EventArgs e)
    {
      Habilitar(enmModo.Lista);
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      OnCancel(); 
    }

    private void btnSalvar_Click(object sender, EventArgs e)
    {
      OnConfirm();
    }
    
    private void btnSair_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void frmBaseCadastro_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (ModoAtual == enmModo.Edicao)
      { OnCancel(); }

      e.Cancel = ModoAtual == enmModo.Edicao;
    }
    #endregion
  }
}
