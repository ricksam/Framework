﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;
using lib.Database;
using lib.Visual;

namespace lib.Visual.Forms
{
  public partial class Backup : lib.Visual.Models.frmBase
  {
    protected Backup()
    {
      InitializeComponent();
      estimatedTime = new EstimatedTime();
      Unds = new List<SysUnidades>();
    }

    public Backup(Connection Connection, string ScriptFile)
      : this() 
    {
      this.Cnn = Connection;
      this.ArqScript = ScriptFile;
    }

    int uSeg { get; set; }
    int Percent { get; set; }
    EstimatedTime estimatedTime { get; set; }
    List<SysUnidades> Unds { get; set; }
    Connection Cnn { get; set; }
    string ArqScript { get; set; }

    #region class SysUnidades
    class SysUnidades 
    {
      public SysUnidades(DriveInfo di) 
      {        
        this.Name = di.Name;
        try
        {
          this.TotalSize = GetValor(di.TotalSize); 
          this.FreeSize = GetValor(di.TotalFreeSpace); 
        }
        catch
        {
          this.TotalSize = "";
          this.FreeSize = "";
        }
      }

      public string GetValor(long Val)
      {
        if (Val >= 1000000000000)
        { return (Val / 1000000000000).ToString() + " Tb."; }
        else if (Val >= 1000000000)
        { return (Val / 1000000000).ToString() + " Gb."; }
        else if (Val >= 1000000)
        { return (Val / 1000000).ToString() + " Mb."; }
        else if (Val >= 1000)
        { return (Val / 1000).ToString() + " Kb."; }
        else
        { return Val.ToString() + " Bytes"; }
      }
      public string Name { get; set; }
      public string TotalSize { get; set; }
      public string FreeSize { get; set; }
    }
    #endregion

    #region private void Carregar()
    private void Carregar() 
    {
      DriveInfo[] drvs = DriveInfo.GetDrives();
      Unds.Clear();
      for (int i = 0; i < drvs.Length; i++)
      {
        if (drvs[i].DriveType == DriveType.Removable)
        {
          Unds.Add(new SysUnidades(drvs[i]));
          cmbRemovivel.Items.Add(drvs[i].Name);
        }
      }

      if (cmbRemovivel.Items.Count != 0)
      { cmbRemovivel.SelectedIndex = 0; }
      else
      { lblRemovivel.Text = "Nenhuma unidade removivel foi encontrada"; }
    }
    #endregion

    #region private void Habilitar()
    private void Habilitar() 
    {
      cmbRemovivel.Enabled = rbUnidade.Checked && rbUnidade.Enabled;
      txtArquivo.Enabled = rbArquivo.Checked;
      btnArquivo.Enabled = rbArquivo.Checked;
    }
    #endregion

    #region public void ExecBackup(string FileName)
    public void ExecBackup(string FileName, List<string> NaoCopiarTabelas, bool UseInsertIdentity, bool UseSquareBrackets)
    {
      try
      {
        btnGerarBackup.Enabled = false;
        btnFechar.Enabled = false;
        grpOpcoes.Enabled = false;

        if (System.IO.File.Exists(FileName))
        { System.IO.File.Delete(FileName); }

        DbScript DbScript = new DbScript(Cnn, UseSquareBrackets);
        DbScript.OnScriptWriting += new lib.Database.DbScript.ScriptWriting_Handle(DbScript_ScriptWriting);
        DbScript.DbScriptTypeList.Clear();
        DbScript.DbScriptTypeList.AddRange(DbScriptTypeList.CreateItems());
        DbScript.DbScriptTypeList.Add(new DbScriptType(typeof(UInt64), "BOOLEAN", lib.Database.Drivers.enmFieldType.Bool));

        string[] TablesInOrder = DbScript.GetTablesInOrder(100);
                
        if (System.IO.File.Exists(ArqScript))
        { System.IO.File.Copy(ArqScript, FileName); }

        TextFile tf = new TextFile();
        tf.Open(enmOpenMode.Writing, FileName, Encoding.UTF8);
        tf.WriteLine("");

        pbBackup.Maximum = TablesInOrder.Length * 2;
        pbBackup.Value = 0;

        estimatedTime.Start();

        // Script de Limpeza        
        for (int i = (TablesInOrder.Length - 1); i >= 0; i--)
        {
          if ((i % 100) == 0)
          { this.Refresh(); }

          pbBackup.Value += 1;
          if (NaoCopiarTabelas.IndexOf(TablesInOrder[i].ToUpper()) == -1)
          { DbScript.SalveScriptDelete(TablesInOrder[i], tf); }
        }

        tf.WriteLine(DbScript.COMMIT_COMMAND);

        // Desabilita chaves
        //for (int i = (TablesInOrder.Length - 1); i >= 0; i--)
        //{ tf.WriteLine(Cnn.GetDisableKey(TablesInOrder[i])); }

        // Script de Inserção        
        for (int i = 0; i < TablesInOrder.Length; i++)
        {
          this.Percent = (i + 1) * 100 / TablesInOrder.Length;
          if ((i % 100) == 0)
          { this.Refresh(); }

          pbBackup.Value += 1;
          if (NaoCopiarTabelas.IndexOf(TablesInOrder[i].ToUpper()) == -1)
          { DbScript.SalveScriptInsert(TablesInOrder[i], tf,UseInsertIdentity, UseSquareBrackets); }
        }

        // Habilita chaves
        //for (int i = (TablesInOrder.Length - 1); i >= 0; i--)
        //{ tf.WriteLine(Cnn.GetEnableKey(TablesInOrder[i])); }

        lblTempoEstimado.Text = "";
        tf.Close();
      }
      finally
      {
        btnGerarBackup.Enabled = true;
        btnFechar.Enabled = true;
        grpOpcoes.Enabled = true;
      }
    }
    #endregion

    #region public void GerarBackup()
    public void GerarBackup() 
    {
      string FileName = "";

      if (rbUnidade.Checked)
      {
        if (rbUnidade.Enabled)
        { FileName = cmbRemovivel.Text + "Database_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bkp"; }
        else
        { Msg.Warning("Verifique se a unidade removível está conectada"); }
      }
      else if (rbArquivo.Checked)
      {
        if (string.IsNullOrEmpty(txtArquivo.Text))
        { Msg.Warning("Informe um arquivo"); }
        else
        { FileName = txtArquivo.Text; }
      }

      if (!string.IsNullOrEmpty(FileName))
      {
        ExecBackup(FileName,new List<string>(),false, false);
        pbBackup.Value = 0;
        pbTable.Value = 0;
        Msg.Information(string.Format("Arquivo de backup gerado com sucesso em {0} !", FileName));
      }
    }
    #endregion

    #region Events
    private void Backup_Load(object sender, EventArgs e)
    {
      Carregar();
    }

    void DbScript_ScriptWriting(int Index, int Count)
    {
      if (DateTime.Now.Second != uSeg)
      {
        lblTempoEstimado.Text = "Tempo estimado: " + estimatedTime.Get(Index * Percent / 100, Count);
        lblTempoEstimado.Refresh();

        pbTable.Maximum = Count;
        pbTable.Value = Index + 1;
        pbTable.Refresh();
        uSeg = DateTime.Now.Second;
      }
    }

    private void cmbRemovivel_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cmbRemovivel.SelectedIndex != -1)
      {
        lblRemovivel.Text =
        "Capacidade: " + Unds[cmbRemovivel.SelectedIndex].TotalSize +
        " Espaço Livre:" + Unds[cmbRemovivel.SelectedIndex].FreeSize;
        btnGerarBackup.Enabled = true;
      }
    }

    private void sknButton2_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnArquivo_Click(object sender, EventArgs e)
    {
      if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        txtArquivo.Text = dlgSave.FileName;
        btnGerarBackup.Enabled = true;
      }
    }

    private void btnGerarBackup_Click(object sender, EventArgs e)
    {
      GerarBackup();
    }

    private void rbUnidade_CheckedChanged(object sender, EventArgs e)
    {
      Habilitar();
    }

    private void rbArquivo_CheckedChanged(object sender, EventArgs e)
    {
      Habilitar();
    }
    #endregion
  }
}
