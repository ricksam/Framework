﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;
using lib.Database.Drivers;

namespace lib.Visual.Forms
{
  /// <summary>
  /// Exemplo de uso
  /// 
  /// Por meio do Utilities:
  ///   public static void Start ()
  ///   {
  ///     FormConnection f = new FormConnection();
  ///     if (f.LoadCfg()) 
  ///     {
  ///       Cnn = new Connection();
  ///       Cnn.Connect(f.DbType, f.InfoConnection);
  ///       Sb = new SqlBuild(Cnn.dbu);
  ///     }
  ///   }
  ///   
  /// Por meio de botão de configuração:
  ///     FormConnection f = new lib.Visual.Forms.FormConnection(true);
  ///     if (f.Exec())
  ///     {
  ///       Cnn = new Connection();
  ///       Cnn.Connect(f.DbType, f.InfoConnection);
  ///       Sb = new SqlBuild(Cnn.dbu);
  ///     }
  /// </summary>
  public partial class FormConnection : lib.Visual.Models.frmDialog
  {
    #region Constructor
    public FormConnection() 
    {
      InitializeComponent();

      if (string.IsNullOrEmpty(FileName))
      {
        sri = new Serialization(SerializeFormat.Xml);
        FileName = Functions.GetDirAppCondig() + "\\cfg.DB.xml";
        FileNameHistorico = Functions.GetDirAppCondig() + "\\cfg.DBHistorico.xml";
        cfg = new cfgDb();
        CfgHistorico = new List<cfgDb>();
      }
    }

    public FormConnection(string ConfigFolder)
      : this()
    {
      sri = new Serialization(SerializeFormat.Xml);

      if (string.IsNullOrEmpty(ConfigFolder))
      {
        FileName = Functions.GetDirAppCondig() + "\\cfg.DB.xml";
      }
      else
      {
        FileName = ConfigFolder + "\\cfg.DB.xml";
      }

      cfg = new cfgDb();
      CfgHistorico = new List<cfgDb>();
    }

    public FormConnection(bool UseAlias)
      : this()
    {
      //lblNome.Visible = true;
      //txtNome.Visible = true;
    }
    #endregion

    #region private class cfgDb
    [Serializable]
    public class cfgDb
    {
      public cfgDb() 
      {
        //this.Alias = "";
        this.DbType = enmConnection.NoDatabase;
        this.Info = new InfoConnection();
      }

      //public string Alias { get; set; }
      public enmConnection DbType { get; set; }
      public InfoConnection Info { get; set; }

      public override string ToString()
      {
        return DbType.ToString() + "; " + Info.Server + "; " + Info.Database+" "+Info.ConnectionString;
        //return string.IsNullOrEmpty(Alias) ? DbType.ToString() + "; " + Info.Server + "; " + Info.Database : Alias;
      }
    }
    #endregion

    #region Properties
    private Serialization sri { get; set; }
    private cfgDb cfg { get; set; }
    private List<cfgDb> CfgHistorico { get; set; }
    public string FileName { get; private set; }
    public string FileNameHistorico { get; private set; }
    public enmConnection DbType { get { return cfg.DbType; } }
    public InfoConnection InfoConnection { get { return cfg.Info; } }
    public string Alias { get { return cfg.ToString(); } }
    public bool NotConfigured { get { return string.IsNullOrEmpty(FileName) || !System.IO.File.Exists(FileName); } }
    #endregion

    #region Methods
    #region private void OpenDbParam()
    /// <summary>
    /// Abre o arquivo de configuração
    /// </summary>
    private bool OpenDbParam()
    {
      try
      {
        cfg = sri.Deserialize<cfgDb>(FileName);
        CfgHistorico = sri.Deserialize<List<cfgDb>>(FileNameHistorico);
        return true;
      }
      catch
      {
        cfg = new cfgDb();
        CfgHistorico = new List<cfgDb>();
        return false;
      }
    }
    #endregion

    #region private void SaveDbParam()
    /// <summary>
    /// Salva/Serializa o arquivo de configuração
    /// </summary>
    private void SaveDbParam()
    {
      sri.Serialize(FileName, cfg);

      while (CfgHistorico.Count > 200)
      { CfgHistorico.RemoveAt(0); }

      sri.Serialize(FileNameHistorico, CfgHistorico);
    }
    #endregion

    #region public bool LoadCfg()
    public bool LoadCfg()
    {
      if (FileName != null)
      {
        if (System.IO.File.Exists(FileName))
        {
          if (OpenDbParam())
          {
            if (cfg.DbType == enmConnection.NoDatabase)
            { return this.Exec(); }
            else
            { return true; }
          }
          else { return false; }
        }
        else
        { return this.Exec(); }
      }
      else
      { return false; }
    }
    #endregion

    #region public void RemoveCfg()
    /// <summary>
    /// Remove o arquivo de configuração
    /// </summary>
    public void RemoveCfg()
    {
      System.IO.File.Delete(FileName);
    }
    #endregion

    #region public void Reconfigure(enmConnection DbType, InfoConnection InfoConnection)
    public void Reconfigure(enmConnection DbType, InfoConnection InfoConnection) 
    {
      this.cfg.DbType = DbType;
      this.cfg.Info = InfoConnection;
      SaveDbParam();
    }
    #endregion
    #endregion

    #region Events
    private void FormConnection_Load(object sender, EventArgs e)
    {
      OpenDbParam();

      cmbTipo.Items.Clear();
      cmbTipo.Items.AddRange(Enum.GetNames(typeof(enmConnection)));
      
      this.cmbTipo.SelectedIndex = ((int)cfg.DbType);
      //this.txtNome.Text = cfg.Alias;
      this.cbWindowsAutenticate.Checked = cfg.Info.UseWindowsAuthentication;
      this.txtServer.Text = cfg.Info.Server;
      this.txtBanco.Text = cfg.Info.Database;
      this.txtUsuario.Text = cfg.Info.User;
      this.txtSenha.Text = cfg.Info.Password;
      this.txtConnectionString.Text = cfg.Info.ConnectionString;

      for (int i = (CfgHistorico.Count - 1); i >= 0; i--)
      { lstConnections.Items.Add(CfgHistorico[i]); }
    }

    protected override void OnConfirm()
    {
      cfg.DbType = ((enmConnection)this.cmbTipo.SelectedIndex);
      cfg.Info.UseWindowsAuthentication = this.cbWindowsAutenticate.Checked;
      cfg.Info.Server = this.txtServer.Text;
      cfg.Info.Database = this.txtBanco.Text;
      cfg.Info.User = this.txtUsuario.Text;
      cfg.Info.Password = this.txtSenha.Text;
      cfg.Info.ConnectionString = this.txtConnectionString.Text;
      
      for (int i = 0; i < CfgHistorico.Count; i++)
      {
        if (CfgHistorico[i].ToString() == cfg.ToString())
        {
          CfgHistorico.RemoveAt(i);
          break;
        }
      }

      CfgHistorico.Add(cfg); 
      
      SaveDbParam();
      base.OnConfirm();
    }
    #endregion

    #region private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
    private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
      //txtNome.Text = "";
    }
    #endregion

    #region private void btnDB_Click(object sender, EventArgs e)
    private void btnDB_Click(object sender, EventArgs e)
    {
      if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      { txtBanco.Text = dlgSave.FileName; }
    }
    #endregion

    private void lstConnections_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lstConnections.SelectedIndex != -1) 
      {
        cfgDb cfg = (cfgDb)lstConnections.SelectedItem;
        cmbTipo.SelectedIndex = ((int)cfg.DbType);
        txtServer.Text = cfg.Info.Server;
        txtBanco.Text = cfg.Info.Database;
        txtUsuario.Text = cfg.Info.User;
        txtSenha.Text = cfg.Info.Password;
        cbWindowsAutenticate.Checked = cfg.Info.UseWindowsAuthentication;
        txtConnectionString.Text = cfg.Info.ConnectionString;
      }
    }

    private void apagarHistóricoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CfgHistorico.Clear();
      lstConnections.Items.Clear();
    }

    private void lstConnections_DoubleClick(object sender, EventArgs e)
    {
      this.OnConfirm();
    }

    private void lstConnections_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter)
      { this.OnConfirm(); }
    }

    private void apagarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (lstConnections.SelectedIndex != -1)
      {
          string str = lstConnections.Items[lstConnections.SelectedIndex].ToString();

          for (int i = 0; i < CfgHistorico.Count; i++)
          {
              if (str == CfgHistorico[i].ToString())
              {
                  CfgHistorico.RemoveAt(i);
                  break;
              }
          }

          lstConnections.Items.RemoveAt(lstConnections.SelectedIndex); 
      }
    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
            lstConnections.Items.Clear();
            if (textBox1.Text.Length != 0)
            {
                lstConnections.Items.AddRange(CfgHistorico.Where(q => q.ToString().ToUpper().Contains(textBox1.Text.ToUpper())).ToArray());
                /*for (int i = 0; i < lstConnections.Items.Count; i++)
                {
                    if (lstConnections.Items[i].ToString().ToUpper().Contains(textBox1.Text.ToUpper()))
                    {
                        lstConnections.SelectedIndex = i;
                    }
                }*/
            }
            else {
                lstConnections.Items.AddRange(CfgHistorico.ToArray());
            }
    }
  }
}
