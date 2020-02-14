using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Database.Drivers
{
  #region public class DatabaseUtils
  public class DatabaseUtils
  {
    public DatabaseUtils()
    {
      dbFormat = new dbFormat();
      CmdGetDateTime = "";
      GetDateTime = "";
      GetLastId = "";
      CmdAutoIncrement = "";
    }

    #region Campos da classe
    public dbFormat dbFormat { get; set; }
    public string CmdGetDateTime { get; set; }
    public string GetDateTime { get; set; }
    public string GetLastId { get; set; }
    public string CmdAutoIncrement { get; set; }
    #endregion

    #region Métodos
    public string Quoted(string Value)
    {
      if (Value != null)
      { return "'" + Value.Replace("'", "''") + "'"; }
      else 
      { return "''"; }
    }
    #endregion

    #region public enmFieldType GetTypeColumn(Type t)
    public enmFieldType GetFieldType(Type t)
    {
      if (t == typeof(int)) { return enmFieldType.Int; }
      else if (t == typeof(long)) { return enmFieldType.Long; }
      else if (t == typeof(DateTime)) { return enmFieldType.DateTime; }
      else if (t == typeof(bool)) { return enmFieldType.Bool; }
      else if (t == typeof(decimal)) { return enmFieldType.Decimal; }
      else { return enmFieldType.String; }
    }
    #endregion
  }
  #endregion

  #region public class dbFormat
  public class dbFormat
  {
    #region Fields
    public string DateTime { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string Decimal { get; set; }
    #endregion
  }
  #endregion
}

