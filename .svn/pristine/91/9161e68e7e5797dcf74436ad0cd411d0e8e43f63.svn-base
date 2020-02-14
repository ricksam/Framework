using System;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Query
{
  [Serializable]
  public class CfgQueryUser : lib.Class.Configuration
  {
    private CfgQueryUser()
    {
    }

    public CfgQueryUser(String WorkDirectory, String Alias)
      : base(WorkDirectory + "\\qry\\CfgQry." + Alias + ".usr")
    { }

    public int FieldIndex { get; set; }
    public String Filter { get; set; }
  }
}
