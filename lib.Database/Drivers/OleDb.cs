﻿using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using lib.Class;

namespace lib.Database.Drivers
{
  public class OleDb : DbBase
  {
    #region Constructor
    public OleDb(InfoConnection Info)
      : base(Info)
    { }

    public OleDb(DbConnection DbConnection)
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
            {
                //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\Aatag\Documents\Database1.mdb
                ConnectionString = string.Format("Provider={0};Data Source={1}", Info.Server, Info.Database);
            }
            else
            {
                ConnectionString = Info.ConnectionString;
            }

            this.DbConnection = new OleDbConnection(ConnectionString); 
    }
    #endregion

    #region protected override DbDataAdapter GetDataAdapter(string sql)
    public override DbDataAdapter GetDataAdapter(string sql)
    {
      return new OleDbDataAdapter(sql, ((OleDbConnection)this.DbConnection));
    }
    #endregion

    #region public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    {
      return new OleDbCommandBuilder(((OleDbDataAdapter)Adapter));
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

    public override ForeignKey[] GetForeignKeys()
    {
      return base.GetForeignKeys();
    }

    public override string GetDisableKey(string Table)
    {
      return "";
    }

    public override string GetEnableKey(string Table)
    {
      return "";
    }
  }
}
