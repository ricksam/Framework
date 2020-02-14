using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace lib.Class
{
  public enum enmOpenMode { Reading, Writing }

  public class TextFile
  {
    private string FileName { get; set; }
    private enmOpenMode Mode { get; set; }
    private object FILE { get; set; }
    
    public void Open(enmOpenMode Mode, string FileName) 
    {
      Open(Mode, FileName, Encoding.Default);
    }

    public void Open(enmOpenMode Mode, string FileName, Encoding Encoder)
    {
      this.Mode = Mode;
      this.FileName = FileName;
      if (this.Mode == enmOpenMode.Reading)
      { FILE = new StreamReader(FileName, Encoder); }
      else if (this.Mode == enmOpenMode.Writing)
      { FILE = new StreamWriter(FileName, true, Encoder); }
    }
    
    public void Write(string Text)
    { ((StreamWriter)FILE).Write(Text); }

    public void WriteLine(string Text)
    { ((StreamWriter)FILE).WriteLine(Text); }
    
    public string Read()
    { return ((StreamReader)FILE).ReadToEnd(); }

    public string ReadLine()
    { return ((StreamReader)FILE).ReadLine(); }
    
    public string[] GetLines()
    {
      List<string> lines = new List<string>();
      string line;
      while ((line = this.ReadLine()) != null)
      { lines.Add(line); }
      return lines.ToArray();
    }
    
    public void Close()
    {
      if (FILE != null)
      {
        if (this.Mode == enmOpenMode.Reading)
        { ((StreamReader)FILE).Close(); }
        else if (this.Mode == enmOpenMode.Writing)
        { ((StreamWriter)FILE).Close(); }
      }
    }

    public long GetSize() 
    {
      FileInfo fi = new FileInfo(FileName);
      return fi.Length;
    }
  }
}
