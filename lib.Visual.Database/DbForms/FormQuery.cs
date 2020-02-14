using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lib.Visual.Forms
{
  public partial class FormQuery : lib.Visual.Forms.FormSearch
  {
    public FormQuery()
    {
      InitializeComponent();
    }

    public bool Exec()
    {
      return this.ShowDialog() == System.Windows.Forms.DialogResult.OK;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape)
      { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; }

      if (keyData == Keys.Enter)
      {
        if (this.ActiveControl == txtPesquisa) 
        {
          Grid.Select();
          //Grid.Focused = true;
          return true;
        }
        else if (this.ActiveControl == Grid && Grid.SelectedRows.Count != 0)
        {
          this.DialogResult = System.Windows.Forms.DialogResult.OK;
          return true;
        }
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }
  }
}
