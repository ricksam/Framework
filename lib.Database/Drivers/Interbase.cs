﻿using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Drivers
{
  public class Interbase:Odbc
  {
    public Interbase(InfoConnection Info)
      : base(Info) 
    {
    }

    public Interbase(DbConnection DbConnection)
      : base(DbConnection)
    {
    }

    protected override void drv_Inicialize()
    {
      base.drv_Inicialize();

      this.dbu.dbFormat.Date = "yyyy-MM-dd";
      this.dbu.dbFormat.Time = "HH:mm:ss";
      this.dbu.dbFormat.DateTime = "yyyy-MM-dd HH:mm:ss";
      this.dbu.dbFormat.Decimal = "0.0000";
      this.dbu.CmdGetDateTime = "CURRENT_DATE+CURRENT_TIME";
    }

    #region public override string GetLimitLines(string sql, int max_rows)
    public override string GetLimitLines(string sql, int max_rows)
    {
        if (sql.ToUpper().IndexOf(" ROWS ") != -1)
      { return sql; }

      return sql + " ROWS " + max_rows.ToString();
    }
    #endregion

    #region public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    {
      string con = "";
      for (int i = 0; i < Fields.Length; i++)
      { con += Fields[i] + (i != Fields.Length - 1 ? "||" : ""); }
      return " (" + con + ") as " + AliasName + " ";
    }
    #endregion

    #region public override string GetConvertField(string Field, enmFieldType FieldType)
    public override string GetConvertField(string Field, enmFieldType FieldType)
    {
      switch (FieldType)
      {
        case enmFieldType.String: { return " cast( " + Field + " as varchar(2000)) "; }
        case enmFieldType.Int: { return " cast( " + Field + " as integer) "; }
        case enmFieldType.Decimal: { return " cast( " + Field + " as decimal(18,4)) "; }
        case enmFieldType.Date: { return " cast( " + Field + " as date) "; }
        case enmFieldType.Time: { return " cast( " + Field + " as time) "; }
        case enmFieldType.DateTime: { return " cast( " + Field + " as timestamp) "; }
        case enmFieldType.Bool: { return " cast( " + Field + " as char(1)) "; }
        default: { return " cast( " + Field + " as varchar(2000)) "; }
      }
    }
    #endregion

    #region public override string GetExtract(string Field, enmExtractType ExtractType)
    public override string GetExtract(string Field, enmExtractType ExtractType)
    {
      switch (ExtractType)
      {
        case enmExtractType.Day: { return " EXTRACT (DAY FROM " + Field + ") "; }
        case enmExtractType.Month: { return " EXTRACT (MONTH FROM " + Field + ") "; }
        case enmExtractType.Year: { return " EXTRACT (YEAR FROM " + Field + ") "; }
        default: { return Field; }
      }
    }
    #endregion

    #region public override ForeignKey[] GetForeignKeys()
    public override ForeignKey[] GetForeignKeys()
    {
      DataTable dt = this.GetDataTable(
        "  SELECT " +
        "   REF.RDB$CONSTRAINT_NAME AS CONSTRAINT_NAME, " +
        "   RC1.RDB$RELATION_NAME AS TABLE_NAME," +
        "   IDX1.RDB$FIELD_NAME AS COLUMN_NAME," +
        "   RC2.RDB$RELATION_NAME AS REFERENCED_TABLE_NAME," +
        "   IDX2.RDB$FIELD_NAME AS REFERENCED_COLUMN_NAME" +
        " FROM RDB$REF_CONSTRAINTS REF" +
        " INNER JOIN RDB$RELATION_CONSTRAINTS RC1 ON RC1.RDB$CONSTRAINT_NAME = REF.RDB$CONSTRAINT_NAME" +
        " INNER JOIN RDB$RELATION_CONSTRAINTS RC2 ON RC2.RDB$CONSTRAINT_NAME = REF.RDB$CONST_NAME_UQ" +
        " INNER JOIN RDB$INDEX_SEGMENTS IDX1 ON IDX1.RDB$INDEX_NAME = REF.RDB$CONSTRAINT_NAME" +
        " INNER JOIN RDB$INDEX_SEGMENTS IDX2 ON IDX2.RDB$INDEX_NAME = REF.RDB$CONST_NAME_UQ", "FOREIGNKEYS", 0, false
        );

      ForeignKey[] list = new ForeignKey[dt.Rows.Count];

      for (int i = 0; i < dt.Rows.Count; i++)
      {
        list[i] = new ForeignKey();
        list[i].ConstraintName = dt.Rows[i]["CONSTRAINT_NAME"].ToString().ToUpper().Trim();
        list[i].TableName = dt.Rows[i]["TABLE_NAME"].ToString().ToUpper().Trim();
        list[i].ColumnName = dt.Rows[i]["COLUMN_NAME"].ToString().ToUpper().Trim();
        list[i].TableReference = dt.Rows[i]["REFERENCED_TABLE_NAME"].ToString().ToUpper().Trim();
        list[i].ColumnReference = dt.Rows[i]["REFERENCED_COLUMN_NAME"].ToString().ToUpper().Trim();
      }

      return list;
    }
    #endregion
  }
}
