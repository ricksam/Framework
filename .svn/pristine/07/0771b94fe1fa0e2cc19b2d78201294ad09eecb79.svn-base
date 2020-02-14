using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using lib.Class;
using lib.Database.Drivers;

namespace lib.Visual.Forms
{
  public class Link
  {
    public Link(lib.Database.Connection cnn, string QueryName, string PrimaryKey)
    {
      this.cnn = cnn;
      this.PrimaryKey = PrimaryKey;
      Qry = new Query(cnn, QueryName);
      Rets = new List<LinkReturn>();
    }

    #region Fields
    private lib.Database.Connection cnn { get; set; }
    private string PrimaryKey { get; set; }
    private TpVinc Type = TpVinc.Number;
    private System.Windows.Forms.TextBoxBase Text; // Componente de Entrada de Dados
    public enum TpVinc { Number, Text }    
    public lib.Visual.Forms.Query Qry { get; set; }
    public List<LinkReturn> Rets { get; set; } // Lista de Campos de Retorno
    public BeforeResearchEventHandle BeforeResearch { get; set; }
    public AfterResearchEventHandle AfterResearch { get; set; }
    #endregion

    #region Types
    public delegate void BeforeResearchEventHandle(object sender);
    public delegate void AfterResearchEventHandle(object sender, DataSource ds);
    #endregion

    #region Methods
    public void Update()
    {
      if (!IsEmpty(Text.Text))
      { Pesquisa(Text.Text, true); }
    }
    public void SetTxtComp(lib.Visual.Components.sknTextBox txt)
    {
      Type = txt.TextType == lib.Visual.Components.enmTextType.Int ? TpVinc.Number : TpVinc.Text;
      txt.AutoTab = false;
      txt.Leave += new EventHandler(txtCod_Leave);
      txt.KeyDown += new KeyEventHandler(txtCod_KeyDown);
      Text = txt;
    }
    public void SetBtnComp(Button btn)
    {
      btn.Text = "";
      btn.Image = lib.Visual.Database.Properties.Resources.lupa_16;
      btn.Click += new EventHandler(btn_Click);
    }
    private bool IsEmpty(string cv)
    {
      if (Type == TpVinc.Number)
      { return cv == "0" || cv == ""; }
      else
      { return cv == ""; }
    }

    private string SqlValue(string Value)
    {
      if (Type == TpVinc.Number)
      { return Value; }
      else
      { return this.cnn.dbu.Quoted(Value); }
    }

    /*private string getAditionalFilter()
    {
      if (Qry.Cfg.AditionalFilter != null && Adv.AditionalFilter != "")
      { return " AND " + Adv.AditionalFilter; }
      else
      { return ""; }
    }*/

    private void Pesquisa(string Cod, bool ExecPesquisa)
    {
      if (BeforeResearch != null)
      { BeforeResearch(this); }

      // Limpa Controls
      Text.Clear();
      for (int i = 0; i < Rets.Count; i++)
      { Rets[i].RetField.Clear(); }

      // Pesquisa
      if (IsEmpty(Cod) && ExecPesquisa)
      {
        if (Qry.Exec())
        { Cod = Qry.GetField(PrimaryKey).ToString(); }
      }

      if (!IsEmpty(Cod))
      {
        DataSource ds = new DataSource(this.cnn.GetDataSet(Qry.Cfg.Sql + " Where " + PrimaryKey + " = " + SqlValue(Cod)));
        for (int i = 0; i < Rets.Count; i++)
        { Rets[i].RetField.Text = ds.GetField(Rets[i].SqlField).ToString(); }

        if (AfterResearch != null)
        { AfterResearch(this, ds); }
      }
    }
    #endregion

    #region Events
    #region btn_Click
    private void btn_Click(object sender, EventArgs e)
    {
      Pesquisa("", true);
    }
    #endregion

    #region txtCod_Leave
    private void txtCod_Leave(object sender, EventArgs e)
    {
      Pesquisa(Text.Text, false);
    }
    #endregion

    #region txtCod_KeyDown
    private void txtCod_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter)
      {
        Pesquisa(((System.Windows.Forms.TextBoxBase)sender).Text, true);
        SendKeys.Send("{TAB}");
        SendKeys.Send("{TAB}");
      }
    }
    #endregion
    #endregion
  }

  public class LinkReturn
  {
    public LinkReturn(System.Windows.Forms.TextBoxBase aRetField, string aSqlField)
    {
      RetField = aRetField;
      SqlField = aSqlField;
    }
    public System.Windows.Forms.TextBoxBase RetField;
    public string SqlField;
  }  
}
