using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;
using lib.Database;
using lib.Database.Drivers;
using lib.Visual;
using lib.Visual.Forms;

namespace lib.Visual.Forms
{
  public partial class Restore : lib.Visual.Models.frmBase
  {
    protected Restore()
    {
      InitializeComponent();
    }

    public Restore(string Alias, Connection Connection, FormError FormError, string ConfigFolder)
      : this() 
    {
      this.Alias = Alias;
      this.Cnn = Connection;
      this.FormError = FormError;
      this.ConfigFolder = ConfigFolder;
    }

    int uSeg { get; set; }
    string Alias { get; set; }
    string ConfigFolder { get; set; }
    Connection Cnn { get; set; }
    FormError FormError { get; set; }

    #region private void Carregar()
    private void Carregar()
    {
      lblAtual.Text = Cnn.dbType.ToString() + " // " + Cnn.Info.Server + " : " + Cnn.Info.Database;
      Habilitar();
      ListaBancos();
    }
    #endregion

    #region private bool DbExiste(string DbName)
    private bool DbExiste(string DbName) 
    {
      for (int i = 0; i < lstBancos.Items.Count; i++)
      {
        if (lstBancos.Items[i].ToString().ToUpper() == DbName.ToUpper())
        { return true; }
      }
      return false;
    }
    #endregion

    #region private string GetNewDbName()
    /// <summary>
    /// Retorna novo nome de banco de dados
    /// </summary>
    private string GetNewDbName()
    {
      int Num = 1;
      string NovoNome = "";
      do
      {
        NovoNome = Alias + Num.ToString("000000");
        Num++;
      }
      while (DbExiste(NovoNome));
      return NovoNome;
    }
    #endregion

    #region private void UsarBanco(string DbName)
    private void UsarBanco(string DbName) 
    {
      Cnn.Info.Database = DbName;
      FormConnection f = new FormConnection(ConfigFolder);
      f.Reconfigure(Cnn.dbType, Cnn.Info);
      f.Dispose();
      f = null;
      Cnn.Connect(Cnn.dbType, Cnn.Info);
    }
    #endregion

    #region private void CriaBanco()
    private void CriaBanco() 
    {
      string NovoNome = GetNewDbName();
      Cnn.Exec("create database " + NovoNome);
      UsarBanco(NovoNome);
      Msg.Information(string.Format("Banco de dados {0} criado com sucesso!", NovoNome));
    }
    #endregion

    #region private void ListaBancos()
    private void ListaBancos() {
      DataSource ds = new lib.Database.Drivers.DataSource(Cnn.GetDataTable("show databases"));
      lstBancos.Items.Clear();
      for (int i = (ds.Count - 1); i >= 0; i--)
      {
        string DatabaseName = ds.GetField(i, "Database").ToString();
        if (DatabaseName.ToUpper().IndexOf(Alias.ToUpper()) != -1)
        { lstBancos.Items.Add(DatabaseName); }
      }
    }
    #endregion

    #region private void Habilitar()
    private void Habilitar() 
    {
      txtUtilizarBackup.Enabled = rbUtilizaBackup.Checked;
      btnUtilizarBackup.Enabled = rbUtilizaBackup.Checked;
      lstBancos.Enabled = rbSelecionaBanco.Checked;
    }
    #endregion

    #region private void DbRestore(string FileName)
    private void DbRestore(string FileName)
    {
      TextFile tf = new TextFile();
      tf.Open(enmOpenMode.Reading, FileName, Encoding.UTF8);

      DbUpdate Dbup = new DbUpdate();
      Dbup.Connection = Cnn;
      Dbup.PrepareDb();

      string buffer = "";
      string[] lines = tf.GetLines();

      pbRestauracao.Maximum = lines.Length;
      EstimatedTime et = new EstimatedTime();

      Dbup.Connection.BeginTransaction();

      try
      {
        for (int i = 0; i < lines.Length; i++)
        {
          if (DateTime.Now.Second != uSeg)
          {
            lblTempoEstimado.Text = "Tempo estimado:" + et.Get(i, lines.Length);
            lblTempoEstimado.Refresh();
            pbRestauracao.Value = i + 1;
            pbRestauracao.Refresh();
            uSeg = DateTime.Now.Second;
            this.Refresh();
            System.Threading.Thread.Sleep(10);
            Application.DoEvents();
          }

          if (string.IsNullOrEmpty(lines[i]))
          { continue; }

          if (lines[i] == DbScript.COMMIT_COMMAND)
          {
            Dbup.Connection.CommitTransaction();
            Dbup.Connection.BeginTransaction();
            continue;
          }

          if (lines[i].StartsWith(DbUpdate.IniStr))
          { continue; }

          buffer += " " + lines[i];
          if (buffer.IndexOf(";") != -1)
          {
            Cnn.Exec(buffer);
            buffer = "";
          }
        }

        if (!string.IsNullOrEmpty(buffer))
        { Cnn.Exec(buffer); }

        Dbup.Connection.CommitTransaction();
      }
      catch 
      {
        Dbup.Connection.RollbackTransaction();
      }
    }
    #endregion

    #region private void Restaurar()
    private void Restaurar() 
    {
      try
      {
        if (rbUtilizaBackup.Checked)
        {          
          CriaBanco();
          DbRestore(txtUtilizarBackup.Text);
        }
        else if (rbSelecionaBanco.Checked)
        {
          if (lstBancos.SelectedIndex != -1)
          { UsarBanco(lstBancos.Items[lstBancos.SelectedIndex].ToString()); }
        }
        else if (rbCriarNovo.Checked)
        { CriaBanco(); }
        else
        {
          Msg.Warning("Selecione uma opção para restauração");
          return;
        }

        Msg.Information("Restauração concluída com sucesso!");
        //Utilities.Start();
        this.DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      catch (Exception ex)
      { FormError.ShowError(ex); }
    }
    #endregion

    #region private void ExcluirBanco()
    private void ExcluirBanco() 
    {
      try
      {
        if (lstBancos.Items.Count == 1)
        {
          Msg.Warning("É necessário que exista pelo menos um banco de dados!");
          return;
        }

        if (lstBancos.SelectedIndex != -1)
        {
          string DbName = lstBancos.Items[lstBancos.SelectedIndex].ToString();

          if (DbName.ToUpper() == Cnn.Info.Database.ToUpper())
          {
            Msg.Warning("Não é permitido excluir o banco de dados em que o sistema está conectado!");
            return;
          }

          if (Msg.Question(
              string.Format(
              "Tem certeza que deseja apagar o banco de dados:\n" +
              " {0}.\n(Obs.:Esta operação é irreversível)", DbName)))
          {
            Cnn.Exec("drop database " + DbName);
            ListaBancos();
          }
        }
      }
      catch (Exception ex) 
      { 
        FormError.ShowError(ex); 
      }
    }
    #endregion

    #region Events
    private void btnFechar_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnExecutarRestauracao_Click(object sender, EventArgs e)
    {
      btnExecutarRestauracao.Enabled = false;
      Restaurar();
      btnExecutarRestauracao.Enabled = true;
    }

    private void rb_CheckedChanged(object sender, EventArgs e)
    {
      Habilitar();
    }

    private void btnUtilizarBackup_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      { txtUtilizarBackup.Text = dlgOpen.FileName; }
    }

    private void Restore_Load(object sender, EventArgs e)
    {
      Carregar();
    }

    private void excluirToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ExcluirBanco();
    }
    #endregion
  }
}
