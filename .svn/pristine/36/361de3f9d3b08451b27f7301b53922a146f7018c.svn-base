using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Database;

namespace lib.Visual
{
  public partial class UpdateScript : lib.Visual.Models.frmBase
  {
    public UpdateScript()
    {
      InitializeComponent();
      DbUpdate = new DbUpdate();
      DbUpdate.OnExecute += new DbUpdate.OnExecute_Handle(DbUpdate_OnExecute);
    }

    public DbUpdate DbUpdate { get; set; }

    public void DbUpdate_OnExecute(int UpdateNumber, int MaxUpdateNumber, int SqlNumber, int MaxSqlNumber) 
    {
      this.pbUpdate.Maximum = MaxUpdateNumber;
      this.pbSql.Maximum = MaxSqlNumber;
      this.pbUpdate.Value = UpdateNumber + 1;
      this.pbSql.Value = SqlNumber + 1;
      this.Refresh();
    }

    private void tmrUpdate_Tick(object sender, EventArgs e)
    {
      txtArquivo.Text = DbUpdate.FileName;
      txtArquivo.Refresh();
      tmrUpdate.Enabled = false;
      DbUpdate.Exec();

      this.pbUpdate.Value = 0;
      this.pbSql.Value = 0;

      if (DbUpdate.Log.Count != 0)
      { Msg.Warning("Alguns scripts foram executados com erros clique em gravar arquivo de log."); }
      else 
      { this.Close(); }
    }

    private void btnExportarArquivo_Click(object sender, EventArgs e)
    {
      if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        if (System.IO.File.Exists(dlgSave.FileName))
        { System.IO.File.Delete(dlgSave.FileName); }
        DbUpdate.ExportLogs(dlgSave.FileName);
      }
    }

    private void btnFechar_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
