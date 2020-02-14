using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing;
using lib.Visual.Components.HL_RichTextBox;

namespace lib.Visual.Components
{
  public class SyntaxRichTextBox : System.Windows.Forms.RichTextBox
  {
    public SyntaxRichTextBox()
    {
      Settings = new SyntaxSettings();
      highlighter.Dictionary = dic;
      EnableHighlight = true;
    }
    
    #region Fields
    private const int MaxLengthHighlight = 12000;
    private SyntaxHighlightDictionary dic = new SyntaxHighlightDictionary();
    private RichTextBoxHighlighter highlighter = new RichTextBoxHighlighter();    
    public SyntaxSettings Settings { get; set; }
    private bool WithHighlight { get; set; }
    private bool EnableHighlight 
    {
      set
      {
        if (value != WithHighlight)
        {
          if (value)
          { highlighter.HookToRichTextBox(this); }
          else 
          { highlighter.UnhookRichTextBox(this); }
          WithHighlight = value;
        }
      }
      get { return WithHighlight; }
    }
    #endregion

    protected override void OnTextChanged(EventArgs e)
    {
      this.EnableHighlight = (this.Text.Length < MaxLengthHighlight);      
      base.OnTextChanged(e);
    }
    
    #region protected override void OnKeyDown(KeyEventArgs e)
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (e.Control && e.KeyValue == 17)
      {
        try
        {
          string t = Clipboard.GetText();
          Clipboard.Clear();
          if (!string.IsNullOrEmpty(t))
          { Clipboard.SetText(t); }
        }
        catch { return; }
      }
      base.OnKeyDown(e);
    }
    #endregion

    #region public void Configure()
    public void Configure() 
    {      
      dic.Font = this.Font;
      dic.ForegroundColor = this.ForeColor;
      dic.BackgroundColor = this.BackColor;

      dic.Clear();

      /*if (Settings.EnablePunctuations)
      {
        //dic.Add(new SyntaxHighlightItem(getPunctuationArray(),
          //    Settings.PunctuationColor, Color.Transparent, FontStyle.Bold));
        dic.Add(new SyntaxHighlightItem(new string[] { string.Format("[{0}]*", getPunctuationArray()) },
              Color.Purple, Color.Transparent, FontStyle.Bold));
      }*/

      // KeyWords
      dic.Add(new SyntaxHighlightItem(getKeyWordsArray(),
          Settings.KeywordColor, Color.Transparent, FontStyle.Regular, RegexOptions.IgnoreCase));

      if (Settings.EnableMembers)
      {
        dic.Add(new SyntaxHighlightItem(getMembersArray(),
            Settings.MemberColor, Color.Transparent, FontStyle.Regular, RegexOptions.IgnoreCase));
      }
      
      if (Settings.EnableNumbers)
      {
        dic.Add(new SyntaxHighlightItem(
            new string[] { "\\b(?:[0-9]*\\.)?[0-9]+\\b" },
            Settings.NumberColor));
      }

      if (Settings.EnableStrings)
      {
        dic.Add(new SyntaxHighlightItem(
            new string[] { "'[^'\r\n]*'", "\"[^\"\r\n]*\"" },
            Settings.StringColor));
      }
      
      if (
        Settings.EnableComments && 
        !string.IsNullOrEmpty(Settings.Comment)&&
        !string.IsNullOrEmpty(Settings.BeginComment)&&
        !string.IsNullOrEmpty(Settings.EndComment)
        )
      {
        string bcmt = ReplaceReservedChars(Settings.BeginComment);
        string ecmt = ReplaceReservedChars(Settings.EndComment);

        dic.Add(new SyntaxHighlightItem(
            new string[] { 
              Settings.Comment+".*", 
              bcmt+ "[\\d\\D]*?" +ecmt                
            },
            Settings.CommentColor, FontStyle.Italic));
      }

      if (this.Text.Length < MaxLengthHighlight)
      { highlighter.HighlightRichTextBox(this); }
    }
    #endregion

    #region private string[] getKeyWordsArray()
    private string[] getKeyWordsArray() 
    {      
      string[] arr = Settings.Keywords.ToArray();
      for (int i = 0; i < arr.Length; i++)
      { arr[i] = "\\b" + arr[i] + "\\b"; }
      return arr;
    }
    #endregion

    #region private string[] getMembersArray()
    private string[] getMembersArray()
    {
      string[] arr = Settings.Members.ToArray();
      for (int i = 0; i < arr.Length; i++)
      { arr[i] = "\\b" + arr[i] + "\\b"; }
      return arr;
    }
    #endregion

    /*private string getPunctuationArray() 
    {
      string[] arr = Settings.Punctuations.ToArray();
      string cmd = "";
      for (int i = 0; i < arr.Length; i++)
      { cmd += "\\" + arr[i]; }
      return cmd;
    }*/

    #region private string ReplaceReservedChars(string s)
    private string ReplaceReservedChars(string s) 
    {
      /*
       * O colchete de abertura [
        A contra-barra \
        O acento circunflexo ^
        O cifrão $
        O ponto .
        A barra vertical ou pipe |
        O ponto de interrogação ?
        O asterisco *
        O sinal de soma +
        O parêntese de abertura ( e
        O parêntese de fechamento )*/
      s = s.Replace("[", "\\[");
      s = s.Replace("]", "\\]");
      s = s.Replace("\\", "\\\\");
      s = s.Replace("^", "\\^");
      s = s.Replace("$", "\\$");
      s = s.Replace(".", "\\.");
      s = s.Replace("|", "\\|");
      s = s.Replace("?", "\\?");
      s = s.Replace("*", "\\*");
      s = s.Replace("+", "\\+");
      s = s.Replace("(", "\\(");
      s = s.Replace(")", "\\)");
      return s;
    }
    #endregion
  }

  #region public class SyntaxList
  /// <summary>
  /// Class to store syntax objects in.
  /// </summary>
  public class SyntaxList
  {
    public List<string> m_rgList = new List<string>();
    public Color m_color = new Color();
  }
  #endregion

  #region public class SyntaxSettings
  /// <summary>
  /// Settings for the keywords and colors.
  /// </summary>
  public class SyntaxSettings
  {
    SyntaxList m_rgKeywords = new SyntaxList();
    SyntaxList m_rgMembers = new SyntaxList();
    //SyntaxList m_rgPunctuations = new SyntaxList();
    
    #region Properties    
    public List<string> Keywords
    {
      get { return m_rgKeywords.m_rgList; }
    }
    public List<string> Members
    {
      get { return m_rgMembers.m_rgList; }
    }
    /*public List<string> Punctuations
    {
      get { return m_rgPunctuations.m_rgList; }
    }*/
    
    public Color KeywordColor
    {
      get { return m_rgKeywords.m_color; }
      set { m_rgKeywords.m_color = value; }
    }
    public Color MemberColor 
    {
      get { return m_rgMembers.m_color; }
      set { m_rgMembers.m_color = value; }
    }
    /*public Color PunctuationColor
    {
      get { return m_rgPunctuations.m_color; }
      set { m_rgPunctuations.m_color = value; }
    }*/
    public Color CommentColor { get; set; }
    public Color StringColor { get; set; }
    public Color NumberColor { get; set; }

    public string BeginComment { get; set; }
    public string EndComment { get; set; }    
    public string Comment { get; set; }  
   
    public bool EnableComments { get; set; }    
    public bool EnableNumbers { get; set; }    
    public bool EnableStrings { get; set; }
    //public bool EnablePunctuations { get; set; }
    public bool EnableMembers { get; set; }
    #endregion
  }
  #endregion
}
