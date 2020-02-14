using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Odbc;
using lib.Class;

namespace lib.Database.Drivers
{
  #region Classe utilizada para se conectar em objetos OleDb
  public class Odbc : DbBase
  {
    #region Constructor
    public Odbc(InfoConnection Info)
      : base(Info)
    { }
    public Odbc(DbConnection DbConnection)
      : base(DbConnection)
    { }
    #endregion

    #region protected override void drv_Inicialize()
    protected override void drv_Inicialize()
    {
      this.dbu.dbFormat.Date = "yyyy-MM-dd";
      this.dbu.dbFormat.Time = "HH:mm:ss";
      this.dbu.dbFormat.DateTime = "yyyy-MM-dd HH:mm:ss";
      this.dbu.dbFormat.Decimal = "0.0000";
      

            if (string.IsNullOrEmpty(Info.ConnectionString))
            { ConnectionString = string.Format("Dsn={0};Uid={1};Pwd={2}", Info.Database, Info.User, Info.Password); }
            else
            {
                ConnectionString = Info.ConnectionString;
            }

            this.DbConnection = new OdbcConnection(ConnectionString);      
    }
    #endregion

    #region protected override DbDataAdapter GetDataAdapter(string sql)
    public override DbDataAdapter GetDataAdapter(string sql)
    {
      return new OdbcDataAdapter(sql, ((OdbcConnection)this.DbConnection));
    }
    #endregion

    #region public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    {
      return new OdbcCommandBuilder(((OdbcDataAdapter)Adapter));
    }
    #endregion

    #region public override string GetLimitLines(string sql, int max_rows)
    public override string GetLimitLines(string sql, int max_rows)
    {
      return sql;
    }
    #endregion

    #region public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region public override string GetConvertField(string Field, enmFieldType FieldType)
    public override string GetConvertField(string Field, enmFieldType FieldType)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region public override string GetExtract(string Field, enmExtractType ExtractType)
    public override string GetExtract(string Field, enmExtractType ExtractType)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region public override ForeignKey[] GetForeignKeys()
    public override ForeignKey[] GetForeignKeys()
    {
      return base.GetForeignKeys(); 
    }
    #endregion

    public override string GetDisableKey(string Table)
    {
      return "";
    }

    public override string GetEnableKey(string Table)
    {
      return "";
    }

    
  }
  #endregion
}
