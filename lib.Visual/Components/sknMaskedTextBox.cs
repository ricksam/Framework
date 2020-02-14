using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;
using lib.Class;
using System.Drawing;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknMaskedTextBox))]
  public class sknMaskedTextBox:MaskedTextBox
  {
    public sknMaskedTextBox() 
    {
      AutoTab = true;
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
      get
      {
        CreateParams createParams = base.CreateParams;
        switch (this.CharacterCasing)
        {
          case CharacterCasing.Upper:
            createParams.Style |= 8;
            break;

          case CharacterCasing.Lower:
            createParams.Style |= 0x10;
            break;
        }

        return createParams;
      }
    }

    #region protected override void CreateHandle()
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          this.Font = Resources.Skin.Controls.Font;
          this.BackColor = Resources.Skin.Controls.BackColor;
          this.ForeColor = Resources.Skin.Controls.ForeColor;
          this.BorderStyle = BorderStyle.FixedSingle;
          this.borderDrawer.BorderColor = Resources.Skin.Controls.BorderColor;
        }
        this.KeyPress += new KeyPressEventHandler(txt_KeyPress);
        this.KeyDown += new KeyEventHandler(txt_KeyDown);

        PodePreencherText = true;
      }
      catch { }
      base.CreateHandle();
    }
    #endregion

    private BorderDrawer borderDrawer = new BorderDrawer();

    #region protected override void WndProc(ref Message m)
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (Resources.Skin.Enabled)
      { borderDrawer.DrawBorder(ref m, this.Width, this.Height); }
    }
    #endregion

    #region Fields
    private enmTextType _textType;
    private bool PodePreencherText { get; set; }
    private Conversion cnv = new Conversion();
    public CharacterCasing CharacterCasing { get; set; }
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
      set { setCnvText(value.ToString(TextFormat)); }
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
      bool IsTabDecimal = e.KeyChar == ',' && this.Text.IndexOf(",") == -1;
      bool IsTabDateTime = e.KeyChar == '/' || e.KeyChar == ':';
      bool IsBackSpace = e.KeyChar == ((char)8);
      bool IsDecimal = char.IsNumber(e.KeyChar) || IsBackSpace || IsTabDecimal;
      bool IsDateTime = char.IsNumber(e.KeyChar) || IsBackSpace || IsTabDateTime;

      switch (TextType)
      {
        case enmTextType.Int: { e.Handled = !char.IsNumber(e.KeyChar); break; }
        case enmTextType.Decimal: { e.Handled = !IsDecimal; break; }
        case enmTextType.DateTime: { e.Handled = !IsDateTime; break; }
      }

      if (e.KeyChar == ((char)13) && AutoTab)
      { e.Handled = true; }
    }
    #endregion

    #region public override string Text
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
    }
    #endregion

    public string OriginalText 
    {
      get 
      {
        string txt = "";

        if (this.Mask.Trim() != "")
        {
          for (int i = 0; i < this.Mask.Length; i++)
          {
            if ("A09#".IndexOf(this.Mask[i]) != -1)
            {
              if (i < this.Text.Length)
              { txt += this.Text[i]; }
            }
          }
          return txt.Trim();
        }
        else { return this.Text; }
      }
    }

    protected override void OnEnter(EventArgs e)
    {
      this.BeginInvoke((MethodInvoker)delegate()
      {
        this.SelectAll();
      });

      base.OnEnter(e);
    }
  }
}
