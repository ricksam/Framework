﻿using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using lib.Class;

namespace lib.Database.Drivers
{
  public class SqlServer : DbBase
  {
    #region Constructor
    public SqlServer(InfoConnection Info)
      : base(Info)
    { }
    public SqlServer(DbConnection DbConnection)
      : base(DbConnection)
    { }
    #endregion

    #region protected override void drv_Inicialize()
    protected override void drv_Inicialize()
    {
      //"dd/MM/yyyy";
      this.dbu.dbFormat.Date = "yyyy-MM-dd";
      this.dbu.dbFormat.Time = "HH:mm:ss";
      this.dbu.dbFormat.DateTime = this.dbu.dbFormat.Date + " " + this.dbu.dbFormat.Time;
      this.dbu.dbFormat.Decimal = "0.0000";
      this.dbu.CmdGetDateTime = "getDate()";
      this.dbu.GetDateTime = "select " + this.dbu.CmdGetDateTime;
      this.dbu.GetLastId = "select @@identity";
      this.dbu.CmdAutoIncrement = "Identity";

      if (string.IsNullOrEmpty(Info.ConnectionString))
      {
        if (Info.UseWindowsAuthentication)
        {
          ConnectionString =
          string.Format(
          "Data Source={0};Initial Catalog={1};Integrated Security=True",
            new object[]
            {
              Info.Server,
              Info.Database
            }
          );
        }
        else
        {
          ConnectionString =
            string.Format(
            "packet size=4096;user id={0};" +
            "password={1};data source={2};" +
            "persist security info=false;initial catalog={3}",
            new object[]
          {
            Info.User,
            Info.Password,
            Info.Server,
            Info.Database            
          }
          );
        }
      }
      else
      { ConnectionString = Info.ConnectionString; }
      
      this.DbConnection = new SqlConnection(ConnectionString);
    }
    #endregion

    #region protected override DbDataAdapter GetDataAdapter(string sql)
    public override DbDataAdapter GetDataAdapter(string sql)
    {
      return new SqlDataAdapter(sql, ((SqlConnection)this.DbConnection));
    }
    #endregion

    #region public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    {
      return new SqlCommandBuilder(((SqlDataAdapter)Adapter));
    }
    #endregion

    #region public override string GetLimitLines(string sql, int max_rows)
    public override string GetLimitLines(string sql, int max_rows)
    {
      int idxSel = sql.ToUpper().IndexOf("SELECT");
      if (idxSel >= 0 && sql.ToUpper().IndexOf(" TOP ") == -1)
      {
        string TopLinhas = " TOP " + max_rows.ToString() + " ";
        sql = sql.Insert(idxSel + 6, TopLinhas);
      }
      return sql;
    }
    #endregion

    #region public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    {
      string con = "";
      for (int i = 0; i < Fields.Length; i++)
      { con += GetConvertField(Fields[i],FieldType) + (i != Fields.Length - 1 ? "+" : ""); }
      return " (" + con + ") as " + AliasName + " ";
    }
    #endregion

    #region public override string GetConvertField(string Field, enmFieldType FieldType)
    public override string GetConvertField(string Field, enmFieldType FieldType)
    {
      switch (FieldType)
      {
        case enmFieldType.String: { return " cast(" + Field + " as varchar(400)) "; }
        case enmFieldType.Int: { return " cast(" + Field + " as integer) "; }
        case enmFieldType.Decimal: { return " cast(" + Field + " as decimal) "; }
        case enmFieldType.Date: { return " cast(" + Field + " as date) "; }
        case enmFieldType.Time: { return " cast(" + Field + " as time) "; }
        case enmFieldType.DateTime: { return " cast(" + Field + " as datetime) "; }
        case enmFieldType.Bool: { return " cast(" + Field + " as char) "; }
        default: { return " cast(" + Field + " as varchar) "; }
      }
    }
    #endregion

    #region public override string GetExtract(string Field, enmExtractType ExtractType)
    public override string GetExtract(string Field, enmExtractType ExtractType)
    {
      switch (ExtractType)
      {
        case enmExtractType.Day: { return " DAY(" + Field + ") "; }
        case enmExtractType.Month: { return " MONTH(" + Field + ") "; }
        case enmExtractType.Year: { return " YEAR(" + Field + ") "; }
        default: { return Field; }
      }
    }
    #endregion

    #region public override ForeignKey[] GetForeignKeys()
    public override ForeignKey[] GetForeignKeys()
    {
      DataTable dt = this.GetDataTable(
        " SELECT f.name AS CONSTRAINT_NAME," +
        "   OBJECT_NAME(f.parent_object_id) AS TABLE_NAME," +
        "   COL_NAME(fc.parent_object_id," +
        "   fc.parent_column_id) AS COLUMN_NAME," +
        "   OBJECT_NAME (f.referenced_object_id) AS REFERENCE_TABLE_NAME," +
        "   COL_NAME(fc.referenced_object_id," +
        "   fc.referenced_column_id) AS REFERENCE_COLUMN_NAME" +
        " FROM sys.foreign_keys AS f" +
        " INNER JOIN sys.foreign_key_columns AS fc" +
        " ON f.OBJECT_ID = fc.constraint_object_id", "FOREIGNKEYS", 0, false
        );
      ForeignKey[] list = new ForeignKey[dt.Rows.Count];

      for (int i = 0; i < dt.Rows.Count; i++)
      {
        list[i] = new ForeignKey();
        list[i].ConstraintName = dt.Rows[i]["CONSTRAINT_NAME"].ToString();
        list[i].TableName = dt.Rows[i]["TABLE_NAME"].ToString();
        list[i].ColumnName = dt.Rows[i]["COLUMN_NAME"].ToString();
        list[i].TableReference = dt.Rows[i]["REFERENCE_TABLE_NAME"].ToString();
        list[i].ColumnReference = dt.Rows[i]["REFERENCE_COLUMN_NAME"].ToString();
      }

      return list;
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
}
