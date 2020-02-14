using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;

namespace lib.Visual.Components
{
  public enum enmTextType { String, Int, Decimal, DateTime }

  [ToolboxItem(true), ToolboxBitmap(typeof(sknTextBox))]
  public class sknTextBox : TextBox
  {
    public sknTextBox() 
    {
      AutoTab = true;
    }

    #region protected override void CreateHandle()
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          this.Font = Resources.Skin.Controls.Font;
          this.BackColor = (this.ReadOnly ? Resources.Skin.Controls.BackColorInactive : Resources.Skin.Controls.BackColor);
          this.ForeColor = Resources.Skin.Controls.ForeColor;
          this.BorderStyle = BorderStyle.FixedSingle;          
          this.borderDrawer.BorderColor = Resources.Skin.Controls.BorderColor;
        }
        this.KeyPress += new KeyPressEventHandler(txt_KeyPress);
        this.KeyDown += new KeyEventHandler(txt_KeyDown);
        this.Leave+=new EventHandler(txt_Leave);

        PodePreencherText = true;
      }
      catch { }
      base.CreateHandle();
    }
    #endregion

    private BorderDrawer borderDrawer = new BorderDrawer();

    private void txt_Leave(object sender, EventArgs e)
    {
      if (TextFormat != "")
      {
        if (this.TextType == enmTextType.Decimal)
        { setCnvText(cnv.ToDecimal(this.Text).ToString(TextFormat)); }
        else if (this.TextType == enmTextType.DateTime)
        { setCnvText(cnv.ToDateTime(this.Text).ToString(TextFormat)); }        
      }
    }

    #region protected override void WndProc(ref Message m)
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (Resources.Skin.Enabled)
      { borderDrawer.DrawBorder(ref m, this.Width, this.Height); }
    }
    #endregion

    public new bool Enabled
    {
      set
      {
        this.BackColor = (value ? Resources.Skin.Controls.BackColor : Resources.Skin.Controls.BackColorInactive);
        if (!this.DesignMode)
        {
          base.Enabled = true;
          this.TabStop = value;
          this.ReadOnly = !value;
        }
        else
        { base.Enabled = value; }
      }
      get
      {
        if (!this.DesignMode)
        {
          base.Enabled = true;
          return !this.ReadOnly && this.TabStop;
        }
        else { return base.Enabled; }
      }
    }


    #region Fields
    private enmTextType _textType;
    private bool PodePreencherText { get; set; }
    private Conversion cnv = new Conversion();
    public bool AutoTab { get; set; }    
    #endregion

    #region public int AsInt
    [Browsable(false), DefaultValue(0)]
    public int AsInt
    {
      get { return cnv.ToInt(this.Text); }
      set { setCnvText(value.ToString(TextFormat)); }
    }
    #endregion

    #region public int AsDecimal
    [Browsable(false), DefaultValue(0)]
    public decimal AsDecimal
    {
      get { return cnv.ToDecimal(this.Text); }
      set { setCnvText(value.ToString(TextFormat)); }
    }
    #endregion

    #region public int AsDateTime
    [Browsable(false)]
    public DateTime AsDateTime
    {
      get { return cnv.ToDateTime(this.Text); }
      set
      {
        if (value != DateTime.MinValue)
        { setCnvText(value.ToString(TextFormat)); }
        else { setCnvText(""); }
      }
    }
    #endregion

    #region public enmTextType TextType
    public enmTextType TextType
    {
      get { return _textType; }
      set
      {
        _textType = value;
        if (_textType == enmTextType.Int || _textType == enmTextType.Decimal)
        { TextAlign = HorizontalAlignment.Right; }
      }
    }
    #endregion

    #region public string TextFormat
    public string TextFormat { get; set; }
    #endregion

    #region private void setCnvText(string aText)
    /// <summary>
    /// As propriedades AsInt AsDecimal, AsDateTime só podem marcar este valor se 
    /// o componente possui nome após o InicializaComponent do Form
    /// </summary>
    /// <param name="aText"></param>
    private void setCnvText(string aText)
    {
      if (this.PodePreencherText)
      { base.Text = aText; }
    }
    #endregion

    #region private void txt_KeyDown(object sender, KeyEventArgs e)
    private void txt_KeyDown(object sender, KeyEventArgs e) 
    {
      if (e.KeyData == Keys.Enter && AutoTab)
      {
        e.Handled = true;
        SendKeys.Send("{TAB}"); 
      }
    }
    #endregion

    #region private void txt_KeyPress(object sender, KeyPressEventArgs e)
    private void txt_KeyPress(object sender, KeyPressEventArgs e)
    {
      bool IsTabNegativo = e.KeyChar == '-';
      bool IsTabDecimal = e.KeyChar == ',';
      bool IsTabMilhar = e.KeyChar == '.';
      bool IsTabDateTime = e.KeyChar == '/' || e.KeyChar == ':';
      bool IsBackSpace = e.KeyChar == ((char)8);
      bool IsNumber = char.IsNumber(e.KeyChar) || IsTabNegativo;
      bool IsDecimal = char.IsNumber(e.KeyChar) || IsBackSpace || IsTabDecimal || IsTabMilhar || IsTabNegativo;
      bool IsDateTime = char.IsNumber(e.KeyChar) || IsBackSpace || IsTabDateTime;

      switch (TextType)
      {
        case enmTextType.Int: { e.Handled = !IsNumber; break; }
        case enmTextType.Decimal: { e.Handled = !IsDecimal; break; }
        case enmTextType.DateTime: { e.Handled = !IsDateTime; break; }
      }

      if (e.KeyChar == ((char)13) && AutoTab)
      { e.Handled = true; }
    }
    #endregion

    #region public override string Text
    /*
    public override string Text
    {
      get
      {
        switch (TextType)
        {
          case enmTextType.String: { return base.Text; }
          case enmTextType.Int: { return cnv.ToInt(base.Text).ToString(TextFormat); }
          case enmTextType.Decimal: { return cnv.ToDecimal(base.Text).ToString(TextFormat); }
          case enmTextType.DateTime: { return cnv.ToDateTime(base.Text).ToString(TextFormat); }
          default: { return base.Text; }
        }
      }
      set
      { base.Text = value; }      
    }*/
    #endregion

  }
}
