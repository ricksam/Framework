﻿using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lib.Class;


namespace lib.Entity
{
    public enum ConnectionType { None, Access, Firebird, MySql, Odbc, OleDb, Oracle, SQLite, SqlServer, SqlServerCe }
    public delegate void LogSQL_Handle(string sql);
    public delegate void LineCounter_Handler();

    /// <summary>
    /// Funções em comum para todo tipo de banco de dados
    /// </summary>
    public abstract class DbBase
    {
        public DbBase(string ConnectionString)
        {
            cnv = new Conversion();
            this.ConnectionString = ConnectionString;
        }

        public ConnectionType ConnectionType { get; set; }
        protected object ObjLock = new object();
        public Conversion cnv { get; set; }
        public string DbReturnLastID { get; set; }
        public string DbSetDateFormat { get; set; }
        public string DbWithNolock { get; set; }
        public string ConnectionString { get; set; }
        public DbConnection DbConnection { get; set; }
        protected abstract void DbCreateConnection();
        public abstract DbDataAdapter DbCreateDataAdapter(string sql);
        public abstract DbDataAdapter DbCreateDataAdapter(DbCommand cmd);
        private string _lastSQL { get; set; }
        public string LastSQL
        {
            get { return _lastSQL; }
            set
            {
                if (LogSQL != null)
                { LogSQL(value); }
                _lastSQL = value;
            }
        }
        public LogSQL_Handle LogSQL { get; set; }
        private bool IsOpen { get { return DbConnection != null && DbConnection.State == ConnectionState.Open; } }

        public LineCounter_Handler LineCounter { get; set; }

        public void OnLineCounter()
        {
            if (LineCounter != null)
            { LineCounter(); }
        }

        #region public string DbQuoted(string s)
        public string DbQuoted(string s)
        {
            if (!string.IsNullOrEmpty(s))
            { return string.Format("'{0}'", s.Replace("'", "''")); }
            else
            { return "''"; }
        }
        #endregion

        public string DbVarchar(string s, int size)
        {
            if (string.IsNullOrEmpty(s) || s.Length <= size)
            { return s; }

            return s.Substring(0, size);
        }

        public Conversion DbGet(string sql, DbTransaction Transaction = null)
        {
            return new Conversion(DbGetValue(sql, Transaction));
        }

        #region public object DbGetValue(string sql, DbTransaction Transaction = null)
        /// <summary>
        /// Função que retorna um valor simples do banco de dados 
        /// </summary>
        public object DbGetValue(string sql, DbTransaction Transaction = null)
        {
            while (Transaction == null && IsOpen)
            { System.Threading.Thread.Sleep(20); }

            LastSQL = sql;

            lock (ObjLock)
            {
                DbDataReader dataReader = null;
                DbCommand cmd = null;

                try
                {
                    if (Transaction == null)
                    { DbConnection.Open(); }

                    cmd = DbConnection.CreateCommand();
                    cmd.CommandText = sql;

                    if (Transaction != null)
                    { cmd.Transaction = Transaction; }

                    dataReader = cmd.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        return dataReader.GetValue(0);
                    }
                }
                catch (Exception ex)
                { throw new Exception(string.Format("Não foi possível executar a sql de consulta!\nSQL:{0}", sql), ex); }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                        dataReader.Dispose();
                        dataReader = null;
                    }

                    if (cmd != null)
                    {
                        cmd.Dispose();
                        cmd = null;
                    }

                    if (Transaction == null)
                    { DbConnection.Close(); }
                }
                return null;
            }
        }
        #endregion

        #region public DataTable DbGetDataTable(DbConnection Conexao, string sql)
        /// <summary>
        /// Função que retorna um DataTable com um conjunto de dados do banco
        /// </summary>
        public DataTable DbGetDataTable(string sql, System.Data.Common.DbTransaction Transaction = null)
        {
            while (Transaction == null && IsOpen)
            { System.Threading.Thread.Sleep(20); }

            LastSQL = sql;

            lock (ObjLock)
            {
                DbDataAdapter da = null;

                try
                {
                    DbCommand cmd = DbConnection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.Transaction = Transaction;
                    da = DbCreateDataAdapter(cmd);

                    DataTable tb = new DataTable();
                    da.Fill(tb);
                    return tb;
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar a sql : " + sql, ex);
                }
                finally
                {
                    if (da != null)
                    {
                        da.Dispose();
                        da = null;
                    }
                }
            }
        }
        #endregion

        #region public bool DbExecute(string sql, DbTransaction Transaction = null, bool IsLogTable = false)
        /// <summary>
        /// Função que executa uma instrução SQL no banco de dados
        /// </summary>
        public int DbExecute(string sql, DbTransaction Transaction = null)
        {
            while (Transaction == null && IsOpen)
            { System.Threading.Thread.Sleep(20); }

            LastSQL = sql;

            lock (ObjLock)
            {
                DbCommand cmd = null;

                try
                {
                    if (Transaction == null && DbConnection.State != ConnectionState.Open)
                    { DbConnection.Open(); }

                    cmd = DbConnection.CreateCommand();
                    cmd.CommandText = sql;

                    if (Transaction != null)
                    { cmd.Transaction = Transaction; }

                    return cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Dispose();
                        cmd = null;
                    }

                    if (Transaction == null)
                    { DbConnection.Close(); }
                }
            }
        }
        #endregion

        #region public int ReturnLastID(System.Data.Common.DbTransaction transaction)
        public Conversion ReturnLastID(System.Data.Common.DbTransaction transaction)
        {
            return DbGet(DbReturnLastID, transaction);
        }
        #endregion

        public DataTable GetSchema(string CollectionName)
        {
            bool CnnOpen = this.DbConnection != null && this.DbConnection.State == ConnectionState.Open;

            try
            {
                if (!CnnOpen)
                { this.DbConnection.Open(); }

                DataTable dt = null;

                //if (CollectionName == "Views" && dbType == enmConnection.Odbc)
                //{ return new DataTable(); }

                dt = this.DbConnection.GetSchema();

                if (string.IsNullOrEmpty(CollectionName))
                { return dt; }

                bool PossuiCollectionName = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == CollectionName)
                    {
                        PossuiCollectionName = true;
                        break;
                    }
                }

                if (PossuiCollectionName)
                {
                    dt = this.DbConnection.GetSchema(CollectionName);
                }
                else
                { dt = new DataTable(); }

                //if (dt.Rows.Count == 0 && CollectionName.ToUpper()=="TABLES" && dbType == enmConnection.MySql)
                //{ dt = GetDataTable("show tables"); }

                return dt;
            }
            catch { return null; }
            finally
            {
                if (!CnnOpen)
                { this.DbConnection.Close(); }
            }
        }

        public string[] GetTables()
        {
            List<string> lst = new List<string>();

            try
            {
                System.Data.DataTable dt = this.GetSchema("Tables");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Table_Name = dt.Rows[i]["TABLE_NAME"].ToString().ToUpper().Trim();
                    if (Table_Name.IndexOf("$") == -1)
                    { lst.Add(Table_Name); }
                }
            }
            catch { }

            return lst.ToArray();
        }

        public string[] GetViews()
        {
            List<string> lst = new List<string>();

            try
            {
                System.Data.DataTable dt = this.GetSchema("Views");

                string SearchField = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "VIEW_NAME")
                    {
                        SearchField = "VIEW_NAME";
                        break;
                    }

                    if (dt.Columns[i].ColumnName == "TABLE_NAME")
                    {
                        SearchField = "TABLE_NAME";
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(SearchField))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Table_Name = dt.Rows[i]["TABLE_NAME"].ToString().ToUpper().Trim();
                        if (Table_Name.IndexOf("$") == -1)
                        { lst.Add(Table_Name); }
                    }
                }
            }
            catch { }

            return lst.ToArray();
        }

        public string GetFieldConnection(string Field)
        {
            string[] fields = ConnectionString.Split(new char[] { ';' });

            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i] != null)
                {
                    string[] field_dados = fields[i].ToString().Split(new char[] { '=' });
                    if (field_dados.Length == 2)
                    {
                        string name = field_dados[0];
                        string value = field_dados[1];
                        if (name.ToUpper().Trim() == Field.ToUpper())
                        { return value; }
                    }
                }
            }
            return "";
        }

        public System.Data.Common.DbTransaction BeginTransaction()
        {
            Open();
            return DbConnection.BeginTransaction();
        }

        public void Open()
        {
            if (DbConnection.State != ConnectionState.Open)
            { DbConnection.Open(); }
        }

        public void Close()
        {
            if (DbConnection.State != ConnectionState.Closed)
            { DbConnection.Close(); }
        }

        public List<string> GetDataReaderColumns(DbDataReader dr)
        {
            List<string> dr_cols = new List<string>();
            for (int i = 0; i < dr.FieldCount; i++)
            { dr_cols.Add(dr.GetName(i).ToUpper()); }
            return dr_cols;
        }
    }

    public static class CreateConnection
    {
        public static lib.Entity.DbBase Create(ConnectionType Type, string ConnectionString)
        {
            if (Type == ConnectionType.Firebird) { return new lib.Entity.Firebird(ConnectionString); }
            if (Type == ConnectionType.MySql) { return new lib.Entity.MySQL(ConnectionString); }
            if (Type == ConnectionType.Odbc) { return new lib.Entity.Odbc(ConnectionString); }
            if (Type == ConnectionType.SQLite) { return new lib.Entity.SQLite(ConnectionString); }
            if (Type == ConnectionType.SqlServer) { return new lib.Entity.SqlServer(ConnectionString); }
            return null;
        }

        public static ConnectionType GetConnectionType(string Type)
        {
            if (Type == ConnectionType.Firebird.ToString()) { return ConnectionType.Firebird; }
            if (Type == ConnectionType.MySql.ToString()) { return ConnectionType.MySql; }
            if (Type == ConnectionType.Odbc.ToString()) { return ConnectionType.Odbc; }
            if (Type == ConnectionType.SQLite.ToString()) { return ConnectionType.SQLite; }
            if (Type == ConnectionType.SqlServer.ToString()) { return ConnectionType.SqlServer; }
            return ConnectionType.None;
        }

        public static string GetConnectionString(ConnectionType Type, bool WindowsAuthentication, string Server, string Dns, string User, string Password)
        {
            switch (Type)
            {
                case ConnectionType.None: return "";
                case ConnectionType.Access: return Access.CreateConnectionString(Dns);
                case ConnectionType.Firebird: return Firebird.CreateConnectionString(Server, Dns, User, Password);
                case ConnectionType.MySql: return MySQL.CreateConnectionString(Server, Dns, User, Password);
                case ConnectionType.Odbc: return Odbc.CreateConnectionString(Dns, User, Password);
                case ConnectionType.OleDb: return OleDb.CreateConnectionString(Server, Dns, User, Password);
                case ConnectionType.Oracle: return OracleODAC.CreateConnectionString(Server, User, Password);
                case ConnectionType.SQLite: return SQLite.CreateConnectionString(Dns);
                case ConnectionType.SqlServer: return SqlServer.CreateConnectionString(WindowsAuthentication, Server, Dns, User, Password);
                case ConnectionType.SqlServerCe: return SqlServerCe.CreateConnectionString(Dns, Password);
                default: return "";
            }
        }
    }
}