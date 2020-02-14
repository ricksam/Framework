using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;
using lib.Database;

namespace lib.Visual.Forms
{
  public partial class Query : frmQuery
  {
    protected Query()
    {
      // Permite exibir a tela em tempo de desenvolvimento
      InitializeComponent();
    }

    public Query(Connection cnn, string QueryName)
      : base(cnn, QueryName)
    {
      InitializeComponent();
      grdItens.DoubleClick += new EventHandler(grdItens_DoubleClick);
      this.btnConfirmar.Left = (this.Width / 2) - 93;
      this.btnCancelar.Left = (this.Width / 2) + 3;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Enter && grdItens.Focused)
      {
        OnConfirm();
        return true;
      }
      else if (keyData == Keys.Escape)
      {
        OnCancel();
        return true;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected virtual void OnConfirm()
    {
      this.DialogResult = DialogResult.OK;
    }

    protected virtual void OnCancel()
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void grdItens_DoubleClick(object sender, EventArgs e)
    {
      OnConfirm();
    }

    public bool Exec()
    { return (this.ShowDialog() == DialogResult.OK); }
  }
}
