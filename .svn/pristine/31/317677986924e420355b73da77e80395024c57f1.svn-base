using System;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Query
{
  [Serializable]
  public class CfgQuery:lib.Class.Configuration
  {
    private CfgQuery() 
    {
    }

    public CfgQuery(string WorkDirectory, string Alias)
      : base(WorkDirectory + QueryDirectory + "\\" + Alias + ".qry")
    {
      Sql = "";
      Fields = new List<FieldColumn>();
    }

    public static string QueryDirectory { get { return "\\qry"; } }
    private string Alias { get; set; }
    public bool TrazerPreenchido { get; set; }
    public string Sql { get; set; }
    public List<FieldColumn> Fields { get; set; }

    public override bool Open()
    {      
      if (!System.IO.File.Exists(this.FileName))
      { Save(); }

      if (base.Open()) 
      {
        this.Sql = this.Sql.Replace("\n", "\r\n");
        return true;
      }
      return false;
    }
  }
}
