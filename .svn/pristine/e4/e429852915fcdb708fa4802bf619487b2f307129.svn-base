using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Database.Drivers
{
  public class ForeignKey
  {
    public ForeignKey() 
    {
      TableName = "";
      ColumnName = "";
      TableReference = "";
      ColumnReference = "";
    }

    public string ConstraintName { get; set; }
    public string TableName { get; set; }
    public string ColumnName { get; set; }
    public string TableReference { get; set; }
    public string ColumnReference { get; set; }

    public string getScript() 
    {
      return string.Format(
        "ALTER TABLE {0} ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES {3} ({4})",
        new string[]
        {
          TableName.ToUpper(),
          ConstraintName.ToUpper(),
          ColumnName.ToUpper(),
          TableReference.ToUpper(),
          ColumnReference.ToUpper()
        });
    }

    public string getPlainScript()
    {
      return string.Format(
        "constraint {1} foreign key ({2}) references {3} ({4})",
        new string[]
        {
          TableName,
          ConstraintName,
          ColumnName,
          TableReference,
          ColumnReference
        });
    }
  }

  
}
