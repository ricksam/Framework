﻿using System;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Drivers
{  
  public class SQLite : DbBase
  {
    public SQLite(InfoConnection Info)
      : base(Info)
    { }
    public SQLite(DbConnection DbConnection)
      : base(DbConnection)
    {    }

    #region protected override void drv_Inicialize()
    protected override void drv_Inicialize()
    {
      this.dbu.dbFormat.Date = "yyyy-MM-dd";
      this.dbu.dbFormat.Time = "HH:mm:ss";
      this.dbu.dbFormat.DateTime = "yyyy-MM-dd HH:mm:ss";
      this.dbu.dbFormat.Decimal = "0.0000";
      this.dbu.CmdGetDateTime = "strftime('%J','now')";
      this.dbu.GetDateTime = "select " + this.dbu.CmdGetDateTime;
      this.dbu.GetLastId = "select last_insert_rowid()";

            if (string.IsNullOrEmpty(Info.ConnectionString))
            {
                if (!File.Exists(Info.Database))
                { SQLiteConnection.CreateFile(Info.Database); }

                if (File.Exists(Info.Database))
                {
                    ConnectionString =
                      string.Format(
                      "Data Source={0};Pooling=true;FailIfMissing=false",
                      new object[] { Info.Database }
                    );
                }
            else
            {
                ConnectionString = Info.ConnectionString;
            }

            

        this.DbConnection = new SQLiteConnection(ConnectionString);
      }
      else
      { this.DbConnection = null; }
    }
    #endregion

    #region public override System.Data.Common.DbDataAdapter GetDataAdapter(string sql)
    public override System.Data.Common.DbDataAdapter GetDataAdapter(string sql)
    {
      return new SQLiteDataAdapter(sql, ((SQLiteConnection)this.DbConnection));
    }
    #endregion

    #region public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
    {
      return new SQLiteCommandBuilder(((SQLiteDataAdapter)Adapter));
    }
    #endregion

    #region public override string GetExtract(string Field, enmExtractType ExtractType)
    public override string GetExtract(string Field, enmExtractType ExtractType)
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

    #region public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    {
      throw new NotImplementedException();
    }
    #endregion

    #region public override string GetLimitLines(string sql, int max_rows)
    public override string GetLimitLines(string sql, int max_rows)
    {
        if (sql.ToUpper().IndexOf(" LIMIT ") != -1)
      { return sql; }

      return sql + " LIMIT " + max_rows.ToString();
    }
    #endregion

    #region private int UltimoUnderline(string s)
    private int UltimoUnderline(string s) 
    {
      int idx = -1;
      for (int i = 0; i < s.Length; i++)
      {
        if (s[i] == '_')
        { idx = i; }
      }
      return idx;
    }
    #endregion

    #region public override ForeignKey[] GetForeignKeys()
    public override ForeignKey[] GetForeignKeys()
    {
      bool CnnOpen = this.DbConnection != null && this.DbConnection.State == ConnectionState.Open;

      if (!CnnOpen)
      { this.DbConnection.Open(); }

      DataTable dt = this.DbConnection.GetSchema("ForeignKeys");
      ForeignKey[] list = new ForeignKey[dt.Rows.Count];

      for (int i = 0; i < dt.Rows.Count; i++)
      {
        list[i] = new ForeignKey();
        list[i].ConstraintName = dt.Rows[i]["CONSTRAINT_NAME"].ToString();

        int u_idx = UltimoUnderline(list[i].ConstraintName);
        if (u_idx != -1)
        {
          int len = list[i].ConstraintName.Length;
          list[i].ConstraintName = list[i].ConstraintName.Remove(u_idx, len - u_idx);
        }

        list[i].TableName = dt.Rows[i]["TABLE_NAME"].ToString();
        list[i].ColumnName = dt.Rows[i]["FKEY_FROM_COLUMN"].ToString();
        list[i].TableReference = dt.Rows[i]["FKEY_TO_TABLE"].ToString();
        list[i].ColumnReference = dt.Rows[i]["FKEY_TO_COLUMN"].ToString();
      }

      if (!CnnOpen)
      { this.DbConnection.Close(); }

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
