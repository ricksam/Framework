using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace lib.Visual.Components.HL_RichTextBox
{
  [Serializable()]
  public class SyntaxHighlightItem
  {   
    /// <summary>
    /// Regex array defining patterns for highlighting entity
    /// </summary>
    public string[] Definition { get; set; }
    
    [System.Xml.Serialization.XmlIgnore]//Color is complex type, won't serialize
    public Color ForegroundColor { get; set; }
    [System.Xml.Serialization.XmlIgnore]
    public Color BackgroundColor { get; set; }
    //introducing int properties instead that will serialize nicely.
    public int ForegroundColorArgb
    {
      get { return ForegroundColor.ToArgb(); }
      set { ForegroundColor = Color.FromArgb(value); }
    }

    public int BackgroundColorArgb
    {
      get { return BackgroundColor.ToArgb(); }
      set { BackgroundColor = Color.FromArgb(value); }
    }

    public FontStyle FontStyle { get; set; }
    public RegexOptions Options { get; set; }

    [NonSerialized]
    private SyntaxHighlightSegmentList allSegments = null;
    [System.Xml.Serialization.XmlIgnore]
    public SyntaxHighlightSegmentList AllSegments
    {
      get { return allSegments; }
      set { allSegments = value; }
    }

    public SyntaxHighlightItem(string[] definition,
        Color foregroundColor, Color backgroundColor, FontStyle fontStyle, RegexOptions options)
    { Configure(definition, foregroundColor, backgroundColor, fontStyle, options); }

    public SyntaxHighlightItem(string[] definition,
        Color foregroundColor, Color backgroundColor, FontStyle fontStyle)
    { Configure(definition, foregroundColor, backgroundColor, fontStyle, new RegexOptions()); }

    public SyntaxHighlightItem(string[] definition,
        Color foregroundColor, Color backgroundColor)
    { Configure(definition, foregroundColor, backgroundColor, FontStyle.Regular, new RegexOptions()); }

    public SyntaxHighlightItem(string[] definition,
        Color foregroundColor, FontStyle fontStyle)
    { Configure(definition, foregroundColor, Color.Transparent, fontStyle, new RegexOptions()); }

    public SyntaxHighlightItem(string[] definition,
        Color foregroundColor)
    { Configure(definition, foregroundColor, Color.Transparent, FontStyle.Regular, new RegexOptions()); }
        
    private void Configure(
      string[] definition, 
      Color foregroundColor, Color backgroundColor, 
      FontStyle fontStyle, RegexOptions options) 
    {
      this.Definition = definition;
      this.FontStyle = fontStyle;
      this.ForegroundColor = foregroundColor;
      this.BackgroundColor = backgroundColor;
      this.Options = options;
    }
        
    public SyntaxHighlightItem() { }

    public virtual SyntaxHighlightSegmentList FindAllSegments(string text)
    {
      SyntaxHighlightSegmentList res = new SyntaxHighlightSegmentList();
      foreach (string def in Definition)
      {
        Regex regex = new Regex(def, Options);
        MatchCollection matches = regex.Matches(text);
        foreach (Match match in matches)
        {
          res.Add(new SyntaxHighlightSegment(match.Index, match.Index + match.Length));
        }
      }
      res.RemoveOverlappingSegments();
      return res;
    }

    public virtual SyntaxHighlightSegmentList FindAllSegments(string text, bool cached)
    {
      if (AllSegments == null || !cached)
      {
        AllSegments = FindAllSegments(text);
      }
      return AllSegments;
    }
  }
}