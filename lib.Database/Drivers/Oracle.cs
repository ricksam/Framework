﻿using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
//using Oracle.ManagedDataAccess.Client;
using System.Data.OracleClient;

namespace lib.Database.Drivers
{
  public class OracleODAC:DbBase
  {
    public OracleODAC(InfoConnection Info)
      : base(Info)
    { }

    public OracleODAC(DbConnection DbConnection)
      : base(DbConnection)
    { }

    protected override void drv_Inicialize()
    {
      this.dbu.dbFormat.Date = "yyyy-MM-dd";
      this.dbu.dbFormat.Time = "HH:mm:ss";
      this.dbu.dbFormat.DateTime = "yyyy-MM-dd HH:mm:ss";
      this.dbu.dbFormat.Decimal = "0.0000";
      this.dbu.CmdGetDateTime = "sysdate";
      this.dbu.GetDateTime = "select to_char(sysdate, 'DD/MM/YYY')data from dual ";

            if (string.IsNullOrEmpty(Info.ConnectionString))
            {
                ConnectionString =
        string.Format(
          "User Id={0}; Password={1}; " +
          "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {2})(PORT = 1521)))" +
          "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = XE)));",
          new object[]
            {
              Info.User,
              Info.Password,
              Info.Server
            }
        );
            }
            else
            {
                ConnectionString = Info.ConnectionString;
            }

            

      this.DbConnection = new OracleConnection(ConnectionString);
    }

    #region public override string GetLimitLines(string sql, int max_rows)
    public override string GetLimitLines(string sql, int max_rows)
    {
      string cmd_rows = string.Format(" rownum <= {0} ", max_rows.ToString());
      sql = sql.ToUpper();
      int iw = sql.IndexOf("WHERE");
      int io = sql.IndexOf("WHERE");
      int ig = sql.IndexOf("WHERE");
      int ih = sql.IndexOf("WHERE");

      int im = -1;

      #region Atribui um valor diferente de -1 a im
      if (io != -1 && im == -1) { im = io; }
      if (ig != -1 && im == -1) { im = ig; }
      if (ih != -1 && im == -1) { im = ih; }
      #endregion

      #region Busca o menor valor para im
      if (im != -1)
      {
        if (io < im) { im = io; }
        if (ig < im) { im = ig; }
        if (ih < im) { im = ih; }
      }
      #endregion

      if (iw == -1 && io == -1 && ig == -1 && ih == -1)
      { return sql + " where " + cmd_rows; }
      else
      {
        string w_clause = (iw == -1 ? " where " : " and ");
        if (im != -1)
        { return sql.Insert(im, w_clause + cmd_rows); }
        else
        { return sql + w_clause + cmd_rows; }
      }
    }
    #endregion

    #region public override System.Data.Common.DbDataAdapter GetDataAdapter(string sql)
    public override System.Data.Common.DbDataAdapter GetDataAdapter(string sql)
    {
      return new OracleDataAdapter(sql, ((OracleConnection)this.DbConnection));
    }
    #endregion

    #region public override System.Data.Common.DbCommandBuilder GetCommandBuilder(System.Data.Common.DbDataAdapter Adapter)
    public override System.Data.Common.DbCommandBuilder GetCommandBuilder(System.Data.Common.DbDataAdapter Adapter)
    {
      return new OracleCommandBuilder(((OracleDataAdapter)Adapter));
    }
    #endregion

    public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
    {
      throw new NotImplementedException();
    }

    public override string GetConvertField(string Field, enmFieldType FieldType)
    {
      throw new NotImplementedException();
    }

    public override string GetExtract(string Field, enmExtractType ExtractType)
    {
      throw new NotImplementedException();
    }

    public override ForeignKey[] GetForeignKeys()
    {
      DataTable dt = this.GetDataTable(
         string.Format(" SELECT " +
         "    FK.OWNER, FK.CONSTRAINT_NAME, FK.TABLE_NAME, FC.COLUMN_NAME," +
         "    PK.OWNER AS REF_OWNER, PK.CONSTRAINT_NAME AS REF_CONSTRAINT_NAME, " +
         "    PK.TABLE_NAME AS REF_TABLE_NAME, PC.COLUMN_NAME AS REF_COLUMN_NAME" +
         "  FROM " +
         "    ALL_CONSTRAINTS FK" +
         "    JOIN ALL_CONS_COLUMNS FC ON (FK.OWNER = FC.OWNER AND FK.CONSTRAINT_NAME = FC.CONSTRAINT_NAME)" +
         "    JOIN (ALL_CONSTRAINTS PK" +
         "      JOIN ALL_CONS_COLUMNS PC ON (PK.OWNER = PC.OWNER AND PK.CONSTRAINT_NAME = PC.CONSTRAINT_NAME))" +
         "    ON (FK.R_OWNER = PK.OWNER AND FK.R_CONSTRAINT_NAME = PK.CONSTRAINT_NAME" +
         "      AND FC.POSITION = PC.POSITION)" +
         "    WHERE FK.CONSTRAINT_TYPE = 'R' AND PK.CONSTRAINT_TYPE IN ('P', 'U')" +
         "      AND FK.OWNER = '{0}'", this.Info.User.ToUpper()), "FOREIGNKEYS", 0, false
        //"      AND FK.OWNER = '{0}' AND FK.TABLE_NAME = '<table>'", 0, false
       );

      ForeignKey[] list = new ForeignKey[dt.Rows.Count];

      for (int i = 0; i < dt.Rows.Count; i++)
      {
        list[i] = new ForeignKey();
        list[i].ConstraintName = dt.Rows[i]["CONSTRAINT_NAME"].ToString().ToUpper().Trim();
        list[i].TableName = dt.Rows[i]["TABLE_NAME"].ToString().ToUpper().Trim();
        list[i].ColumnName = dt.Rows[i]["COLUMN_NAME"].ToString().ToUpper().Trim();
        list[i].TableReference = dt.Rows[i]["REF_TABLE_NAME"].ToString().ToUpper().Trim();
        list[i].ColumnReference = dt.Rows[i]["REF_COLUMN_NAME"].ToString().ToUpper().Trim();
      }

      return list;
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
