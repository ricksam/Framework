﻿using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using lib.Class;

namespace lib.Database.Drivers
{
    public class SqlServerCe : DbBase
    {
        #region Constructor
        public SqlServerCe(InfoConnection Info)
          : base(Info)
        { }
        public SqlServerCe(DbConnection DbConnection)
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
                ConnectionString = string.Format("data source={0}; Persist Security Info=False; Password={1};",
   Info.Database, Info.Password);

                if (!System.IO.File.Exists(Info.Database))
                {
                    SqlCeEngine engine = new SqlCeEngine(ConnectionString);
                    engine.CreateDatabase();
                }
            }
            else
            {
                ConnectionString = Info.ConnectionString;
            }



            this.DbConnection = new SqlCeConnection(ConnectionString);
        }
        #endregion

        #region protected override DbDataAdapter GetDataAdapter(string sql)
        public override DbDataAdapter GetDataAdapter(string sql)
        {
            return new SqlCeDataAdapter(sql, ((SqlCeConnection)this.DbConnection));
        }
        #endregion

        #region public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
        public override DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
        {
            return new SqlCeCommandBuilder(((SqlCeDataAdapter)Adapter));
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
            { con += GetConvertField(Fields[i], FieldType) + (i != Fields.Length - 1 ? "+" : ""); }
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
}
