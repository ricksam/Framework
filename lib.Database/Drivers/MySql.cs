﻿using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using lib.Class;

namespace lib.Database.Drivers
{
    public class MySql : DbBase
    {
        #region Constructor
        public MySql(InfoConnection Info)
          : base(Info)
        { }
        public MySql(DbConnection DbConnection)
          : base(DbConnection)
        { }
        #endregion

        #region protected override void drv_Inicialize()
        protected override void drv_Inicialize()
        {
            if (Info.UseWindowsAuthentication)
            { this.CreateIfNotExists(Info.Database); }

            this.dbu.dbFormat.Date = "yyyy-MM-dd";
            this.dbu.dbFormat.Time = "HH:mm:ss";
            this.dbu.dbFormat.DateTime = "yyyy-MM-dd HH:mm:ss";
            this.dbu.dbFormat.Decimal = "0.0000";
            this.dbu.CmdGetDateTime = "now()";
            this.dbu.GetDateTime = "select " + this.dbu.CmdGetDateTime;
            this.dbu.GetLastId = "select last_insert_id()";
            this.dbu.CmdAutoIncrement = "Auto_Increment";

            if (string.IsNullOrEmpty(Info.ConnectionString))
            {
                ConnectionString =
          string.Format(
            "Server={0};Database={1};Uid={2};Pwd={3};",
            new object[]
            {
              Info.Server,
              Info.Database,
              Info.User,
              Info.Password
            }
          );
            }
            else
            {
                ConnectionString = Info.ConnectionString;
            }


            this.DbConnection = new MySqlConnection(ConnectionString);

        }
        #endregion

        #region private void CreateIfNotExists(string DbName)
        private void CreateIfNotExists(string DbName)
        {
            this.ConnectionString =
                string.Format(
                  "Server={0};Database={1};Uid={2};Pwd={3};",
                  new object[]
                  {
              Info.Server,
              "mysql",
              Info.User,
              Info.Password
                  }
                );

            this.DbConnection = new MySqlConnection(ConnectionString);

            DataTable TbDatabases = this.GetDataTable("show databases", "Databases", 0, false);
            for (int i = 0; i < TbDatabases.Rows.Count; i++)
            {
                if (TbDatabases.Rows[i][0].ToString().ToUpper() == DbName.ToUpper())
                { return; }
            }

            this.Exec(string.Format("create database {0}", DbName));
        }
        #endregion

        #region public override DbDataAdapter GetDataAdapter(string sql)
        public override DbDataAdapter GetDataAdapter(string sql)
        {
            return new MySqlDataAdapter(sql, ((MySqlConnection)this.DbConnection));
        }
        #endregion

        #region public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
        public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
        {
            MySqlCommandBuilder builder = new MySqlCommandBuilder(((MySqlDataAdapter)Adapter));
            //builder.ReturnGeneratedIdentifiers = false;
            return builder;
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

        #region public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
        public override string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
        {
            string con = "";
            for (int i = 0; i < Fields.Length; i++)
            { con += GetConvertField(Fields[i], FieldType) + (i != Fields.Length - 1 ? ", " : ""); }
            return " concat(" + con + ") as " + AliasName + " ";
        }
        #endregion

        #region public override string GetConvertField(string Field, enmFieldType FieldType)
        public override string GetConvertField(string Field, enmFieldType FieldType)
        {
            switch (FieldType)
            {
                case enmFieldType.String: { return " cast(" + Field + " as char)"; }
                case enmFieldType.Int: { return " cast(" + Field + " as decimal)"; }
                case enmFieldType.Decimal: { return " cast(" + Field + " as decimal)"; }
                case enmFieldType.Date: { return " cast(" + Field + " as date)"; }
                case enmFieldType.Time: { return " cast(" + Field + " as time)"; }
                case enmFieldType.DateTime: { return " cast(" + Field + " as datetime)"; }
                case enmFieldType.Bool: { return " cast(" + Field + " as char)"; }
                default: { return " cast(" + Field + " as char)"; }
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
              " SELECT " +
              "   constraint_name," +
              "   table_name," +
              "   column_name, " +
              "   referenced_table_name, " +
              "   referenced_column_name" +
              " FROM information_schema.KEY_COLUMN_USAGE" +
              " where referenced_table_name is not null" +
              " ORDER BY TABLE_NAME, COLUMN_NAME", "FOREIGNKEYS", 0, false
              );

            ForeignKey[] list = new ForeignKey[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list[i] = new ForeignKey();
                list[i].ConstraintName = dt.Rows[i]["CONSTRAINT_NAME"].ToString().ToUpper();
                list[i].TableName = dt.Rows[i]["TABLE_NAME"].ToString().ToUpper();
                list[i].ColumnName = dt.Rows[i]["COLUMN_NAME"].ToString().ToUpper();
                list[i].TableReference = dt.Rows[i]["REFERENCED_TABLE_NAME"].ToString().ToUpper();
                list[i].ColumnReference = dt.Rows[i]["REFERENCED_COLUMN_NAME"].ToString().ToUpper();
            }

            return list;
        }
        #endregion

        public override string GetDisableKey(string Table)
        {
            return string.Format("alter table {0} disable keys;", Table);
        }

        public override string GetEnableKey(string Table)
        {
            return string.Format("alter table {0} enable keys;", Table);
        }
    }
}
