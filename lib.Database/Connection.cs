﻿using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using lib.Database.Drivers;

namespace lib.Database
{
    public class Connection
    {
        #region Constructor
        public Connection()
        {
            this.dbType = enmConnection.NoDatabase;
            this.cnn = new DriverConnection();
        }
        #endregion

        #region Fields
        public int ExecutedCommands { get { return cnn.db.ExecutedCommands; } }
        private DriverConnection cnn { get; set; }
        public enmConnection dbType { get; set; }
        public DbConnection DbConnection { get { return cnn.db.DbConnection; } set { cnn.db.DbConnection = value; } }
        public DatabaseUtils dbu { get { return cnn.db.dbu; } }
        public string LastSql { get { return cnn.db.LastSql; } }
        public string ConnectionString { get { return cnn.db.ConnectionString; } }
        public InfoConnection Info { get; set; }
        public QueryParameters QueryParam { get { return cnn.db.QueryParam; } set { cnn.db.QueryParam = value; } }
        ForeignKey[] ForeignKeys { get; set; }
        #endregion

        #region Methods

        #region private string GetValue(string Field, string[] Fields)
        private string GetValue(string Field, string[] Fields)
        {
            for (int i = 0; i < Fields.Length; i++)
            {
                if (string.IsNullOrEmpty(Fields[i]))
                { continue; }

                string line = Fields[i];
                string FieldName = line.Substring(0, line.IndexOf("=")).Trim();
                string FieldValue = line.Substring(line.IndexOf("=") + 1, line.Length - (line.IndexOf("=") + 1)).Trim();

                if (Field == FieldName)
                { return FieldValue.Trim(); }
            }

            return "";
        }
        #endregion

        #region public bool Connect(string ConnectionString)
        public bool Connect(string ConnectionString)
        {
            string[] Fields = ConnectionString.Split(new char[] { ';' });
            Conversion cnv = new Conversion();
            enmConnection _dbType = GetDbType(GetValue("DbType", Fields));
            InfoConnection _info = new InfoConnection(cnv.ToBool(GetValue("UseWindowsAuthentication", Fields)), GetValue("Server", Fields), GetValue("Database", Fields), GetValue("User", Fields), GetValue("Password", Fields));
            return Connect(_dbType, _info);
        }
        #endregion

        #region public bool Connect(enmConnection dbType, InfoConnection Info)
        /*public bool Connect(String str_dbType, InfoConnection Info)
    {
      Array arr = Enum.GetNames(typeof(enmConnection));
      int idx_dbtype = -1;
      for (int i = 0; i < arr.Length; i++)
      {
        if (str_dbType == arr.GetValue(i).ToString())
        { idx_dbtype = i; }
      }

      this.dbType = ((enmConnection)idx_dbtype);
      this.Info = Info;
      return this.cnn.Connect(dbType, Info);
    }*/
        #endregion

        #region public bool Connect(enmConnection dbType, InfoConnection Info)
        public bool Connect(enmConnection dbType, InfoConnection Info)
        {
            this.ForeignKeys = null;
            this.dbType = dbType;
            this.Info = Info;
            return this.cnn.Connect(dbType, Info);
        }
        #endregion

        #region public bool Connect(enmConnection dbType, DbConnection DbConnection)
        public bool Connect(enmConnection dbType, DbConnection DbConnection)
        {
            this.dbType = dbType;
            return this.cnn.Connect(dbType, DbConnection);
        }
        #endregion

        #region public void Refresh()
        public void Refresh()
        {
            this.ForeignKeys = null;
            this.cnn.db = null;
            this.cnn = null;
            this.cnn = new DriverConnection();
            this.cnn.Connect(this.dbType, this.Info);
        }
        #endregion

        #region public void BeginTransaction()
        public void BeginTransaction()
        {
            this.cnn.db.BeginTransaction();
        }
        #endregion

        #region public bool InTransaction()
        public bool InTransaction()
        {
            return this.cnn.db.InTransaction();
        }
        #endregion

        #region public void CommitTransaction()
        public void CommitTransaction()
        {
            this.cnn.db.CommitTransaction();
        }
        #endregion

        #region public void RollbackTransaction()
        public void RollbackTransaction()
        {
            this.cnn.db.RollbackTransaction();
        }
        #endregion

        #region public Conversion Sql(string sql)
        public Conversion Sql(string sql)
        {
            return cnn.db.Sql(sql);
        }
        #endregion

        #region public DataSet GetDataSet(string sql)
        public DataSet GetDataSet(string sql)
        { return GetDataSet(sql, 0, false); }

        public DataSet GetDataSet(string sql, int max_rows, bool with_schema)
        {
            return cnn.db.GetDataSet(sql, max_rows, with_schema);
        }
        #endregion

        #region public DataTable GetDataTable(string sql)
        public DataTable GetDataTable(string sql)
        { return GetDataTable(sql, 0, false); }

        public DataTable GetDataTable(string sql, int max_rows, bool with_schema)
        { return GetDataTable(sql, "SQL", max_rows, with_schema); }

        public DataTable GetDataTable(string sql, string TableName, int max_rows, bool with_schema)
        {
            return cnn.db.GetDataTable(sql, TableName, max_rows, with_schema);
        }
        #endregion

        #region public DbDataAdapter GetDataAdapter(string sql)
        public DbDataAdapter GetDataAdapter(string sql)
        {
            return cnn.db.GetDataAdapter(sql);
        }
        #endregion

        #region public DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
        public DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter)
        {
            return cnn.db.GetCommandBuilder(Adapter);
        }
        #endregion

        #region public void Exec(string sql)
        public bool Exec(string sql)
        {
            ForeignKeys = null;
            return cnn.db.Exec(sql);
        }
        #endregion

        #region public virtual bool IsConnected()
        public virtual bool IsConnected()
        {
            try
            {
                return cnn.db.IsConnected();
            }
            catch { return false; }
        }
        #endregion

        #region public string GetLimitLines(string Sql, int max_rows)
        public string GetLimitLines(string sql, int max_rows)
        {
            return cnn.db.GetLimitLines(sql, max_rows);
        }
        #endregion

        #region public string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
        public string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType)
        {
            return cnn.db.GetConcatFields(Fields, AliasName, FieldType);
        }
        #endregion

        #region public string GetConvertField(string Field, enmFieldType FieldType)
        public string GetConvertField(string Field, enmFieldType FieldType)
        {
            return cnn.db.GetConvertField(Field, FieldType);
        }
        #endregion

        #region public string GetExtract(string Field, enmExtractType ExtractType)
        public string GetExtract(string Field, enmExtractType ExtractType)
        {
            return cnn.db.GetExtract(Field, ExtractType);
        }
        #endregion

        #region public DateTime GetDateTime()
        public DateTime GetDateTime()
        { return this.Sql(dbu.GetDateTime).ToDateTime(); }
        #endregion

        #region public int GetLastId()
        public Conversion GetLastId()
        { return this.Sql(dbu.GetLastId); }
        #endregion

        #region public string[] GetTables()
        public string[] GetTables()
        {
            return GetTables(true);
        }

        public string[] GetTables(bool WithViews)
        {
            DataSource ds = null;
            List<string> lst = new List<string>();

            // Busca mapa de tabelas
            try
            {
                ds = new DataSource(this.GetSchema("Tables"));
                for (int i = 0; i < ds.Count; i++)
                {
                    string Table_Name = ds.GetField(i, "TABLE_NAME").ToString().Trim();
                    if (Table_Name.IndexOf("$") == -1)
                    {
                        lst.Add(Table_Name);
                    }
                }
            }
            catch { }

            // Busca mapa de Views
            try
            {
                ds = new DataSource(this.GetSchema("Views"));
                string SearchField = ds.FieldExists("TABLE_NAME") ? "TABLE_NAME" : "VIEW_NAME";
                for (int i = 0; i < ds.Count; i++)
                {
                    string Table_Name = ds.GetField(i, SearchField).ToString().Trim();
                    if (WithViews)
                    {
                        if (Table_Name.IndexOf("$") == -1 && lst.IndexOf(Table_Name) == -1)
                        { lst.Add(Table_Name); }
                    }
                    else
                    {
                        if (Table_Name.IndexOf("$") == -1 && lst.IndexOf(Table_Name) != -1)
                        { lst.Remove(Table_Name); }
                    }
                }
            }
            catch { }

            return lst.ToArray();
        }
        #endregion

        #region public ForeignKey[] GetForeignKeys()
        public ForeignKey[] GetForeignKeys()
        {
            if (ForeignKeys == null)
            {
                ForeignKeys = cnn.db.GetForeignKeys();
            }
            return ForeignKeys;
        }
        #endregion

        #region public bool TableExists(string TableName)
        public bool TableExists(string TableName)
        {
            string[] tbls = GetTables();
            for (int i = 0; i < tbls.Length; i++)
            {
                if (tbls[i].Trim().ToUpper() == TableName.Trim().ToUpper())
                { return true; }
            }
            return false;
        }
        #endregion


        public List<string> GetIdentityTables() {
            List<string> list = new List<string>();
            string[] Tables = GetTables();
            foreach (var item in Tables)
            {
                /*System.Data.DataTable dt = GetDataTable(
                            string.Format(
                            @"SELECT B.Name AS IdentityColumn
                            FROM sys.tables A
                            INNER JOIN sys.columns B ON A.Object_ID = B.Object_ID
                            AND A.Name = {0}
                            AND COLUMNPROPERTY(A.Object_ID, B.Name, 'IsIdentity') = 1", dbu.Quoted(item)));
                if (dt.Rows.Count != 0) {
                    list.Add(item);
                }*/


                if (Sql(string.Format( "SELECT OBJECTPROPERTY(OBJECT_ID(N'{0}'),'TableHasIdentity')", item)).ToBool()) {
                    list.Add(item);
                }
            }

            return list;
        }

        public List<PrimaryKey> GetPrimaryKeys()
        {
            bool CnnOpen = this.DbConnection != null && this.DbConnection.State == ConnectionState.Open;
            List<PrimaryKey> list = new List<PrimaryKey>();
            try
            {
                if (!CnnOpen)
                { this.DbConnection.Open(); }

                DataTable dt = this.DbConnection.GetSchema("IndexColumns");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PrimaryKey primaryKey = new PrimaryKey();
                    primaryKey.Table = dt.Rows[i]["TABLE_NAME"].ToString();
                    primaryKey.Column = dt.Rows[i]["COLUMN_NAME"].ToString();
                    if (this.DbConnection is System.Data.SqlClient.SqlConnection)
                    {
                        if (dt.Rows[i]["INDEX_NAME"].ToString().Contains("PK"))
                        {
                            list.Add(primaryKey);
                        }
                    }
                    else if (this.DbConnection is MySql.Data.MySqlClient.MySqlConnection)
                    {
                        if (dt.Rows[i]["INDEX_NAME"].ToString().StartsWith("PRIMARY"))
                        {
                            list.Add(primaryKey);
                        }
                    }
                }
            }
            finally
            {
                if (!CnnOpen)
                { this.DbConnection.Close(); }
            }
            return list;
        }


        #region public DataTable GetSchema(string CollectionName)
        public DataTable GetSchema(string CollectionName)
        {
            bool CnnOpen = this.DbConnection != null && this.DbConnection.State == ConnectionState.Open;

            try
            {
                if (!CnnOpen)
                { this.DbConnection.Open(); }

                DataTable dt = null;

                if (CollectionName == "Views" && dbType == enmConnection.Odbc)
                { return new DataTable(); }

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
        #endregion

        #region public int GetGenID(string Table, string KeyField, enmTypeGenID Type)
        //Pode ser que esta volte para o driver por usar this.cnn.db
        public int GetGenID(string Table, string KeyField, enmTypeGenID Type)
        {
            GenID g = new GenID(this);
            if (Type == enmTypeGenID.Reading)
            { return g.GetGenID(Table); }
            else
            {
                int id = g.GetGenID(Table);
                g.SetID(Table, id);
                return id;
            }
        }
        #endregion

        #region public string GetDisableKey(string Table)
        public string GetDisableKey(string Table)
        {
            return cnn.db.GetDisableKey(Table);
        }
        #endregion

        #region public string GetEnableKey(string Table)
        public string GetEnableKey(string Table)
        {
            return cnn.db.GetEnableKey(Table);
        }
        #endregion
        #endregion

        #region public static enmConnection GetDbType(string ConnectionType)
        /// <summary>
        /// Retorna o tipo da conecxão
        /// </summary>
        /// <param name="ConnectionType"></param>
        /// <returns></returns>
        public static enmConnection GetDbType(string ConnectionType)
        {
            ConnectionType = ConnectionType.ToUpper();
            Array arr = Enum.GetValues(typeof(enmConnection));
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr.GetValue(i).ToString().ToUpper().IndexOf(ConnectionType) != -1)
                { return ((enmConnection)i); }
            }
            return enmConnection.NoDatabase;
        }
        #endregion
    }//public class Connection 

    #region public class GenID
    /// <summary>
    /// Classe responsável por gerar IDs para as tabelas do banco de dados
    /// </summary>
    public class GenID
    {
        #region Constructor
        public GenID(Connection cnn)
        {
            this.cnn = cnn;
        }
        #endregion

        #region Fields
        private Connection cnn { get; set; }
        #endregion

        #region private string GetWhere(string Table)
        private string GetWhere(string Table)
        { return " WHERE TABLE_NAME = " + cnn.dbu.Quoted(Table); }
        #endregion

        #region private void CreateIdControl()
        private void CreateIdControl()
        { cnn.Exec("create table idcontrol (ID INTEGER, TABLE_NAME VARCHAR(80))"); }
        #endregion

        #region private int GetCurrentID(string Table)
        private int GetCurrentID(string Table)
        {
            if (!cnn.TableExists("IDCONTROL"))
            { CreateIdControl(); }

            DataSet D = cnn.GetDataSet("SELECT ID FROM IDCONTROL " + GetWhere(Table), 0, false);
            if (D.Tables.Count == 0)
            {
                SetID(Table, 0);
                return 0;
            }
            else
            {
                if (D.Tables[0].Rows.Count == 0)
                {
                    SetID(Table, 0);
                    return 0;
                }
                else
                {
                    Conversion c = new Conversion(D.Tables[0].Rows[0][0]);
                    return c.ToInt();
                }
            }
        }
        #endregion

        #region public void SetID(string Table, int ID_Value)
        public void SetID(string Table, int ID_Value)
        {
            if (cnn.Sql("SELECT COUNT(ID) FROM IDCONTROL " + GetWhere(Table)).ToInt() == 0)
            { cnn.Exec("INSERT INTO IDCONTROL (ID, TABLE_NAME)VALUES (" + ID_Value + "," + cnn.dbu.Quoted(Table) + ")"); }
            else
            { cnn.Exec("UPDATE IDCONTROL SET ID = " + ID_Value + GetWhere(Table)); }
        }
        #endregion

        #region public int GetGenID(string Table)
        public int GetGenID(string Table)
        { return GetCurrentID(Table) + 1; }
        #endregion
    }
    #endregion
}
