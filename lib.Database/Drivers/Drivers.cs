using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Drivers
{
  /*
   * Sugestão , o driver básico possuir o dbu
   * o driver de conexão possuir uma propriedade do dbu
   * cada driver implementa o dbu
   */

  #region Classe abstrata e modelo para os drivers de conexão
  public abstract class DbBase
  {
    #region Constructor
    public DbBase(InfoConnection Info)
    {      
      this.dbu = new DatabaseUtils();
      this.QueryParam = new QueryParameters(dbu);
      this.DbConnection = null;
      this.Info = Info;
      this.ConnectionString = "";
      this.LastSql = "";
      drv_Inicialize();
      TestConnection();
    }
    #endregion

    #region public DbBase(DbConnection DbConnection, ErrorList ErrorList)
    public DbBase(DbConnection DbConnection)
    {
      this.dbu = new DatabaseUtils();
      this.QueryParam = new QueryParameters(dbu);
      this.DbConnection = DbConnection;
      this.Info = Info;
      this.ConnectionString = DbConnection.ConnectionString;
      this.LastSql = "";
      // drv_Inicialize();
      TestConnection();
    }
    #endregion

    #region Fields
    public int ExecutedCommands { get; set; }
    public QueryParameters QueryParam { get; set; }
    public InfoConnection Info { get; set; }
    public string ConnectionString { get; set; }
    public string LastSql { get; set; }
    public DatabaseUtils dbu { get; set; }
    public DbConnection DbConnection { get; set; }
    public DbTransaction Transaction { get; set; }
    #endregion

    #region Abstract Methods - Protected
    protected abstract void drv_Inicialize();//Inicialica e cria o driver de conexao com o banco de dados
    public abstract string GetLimitLines(string sql, int max_rows);//retorna uma sql com instruções para limitar as linhas
    public abstract DbDataAdapter GetDataAdapter(string sql);//Retorna um Data Adapter específico para o driver    
    public abstract DbCommandBuilder GetCommandBuilder(DbDataAdapter Adapter);//Retorna um Data Adapter específico para o driver    
    public abstract string GetConcatFields(string[] Fields, string AliasName, enmFieldType FieldType);//retorna um conjunto de campos concatenados
    public abstract string GetConvertField(string Field, enmFieldType FieldType);//retorna o comando para converter um campo
    public abstract string GetExtract(string Field, enmExtractType ExtractType);//retorna o comando de extração para o campo informado
    public abstract string GetDisableKey(string Table);
    public abstract string GetEnableKey(string Table);
    public virtual ForeignKey[] GetForeignKeys()
    {
      bool CnnOpen = this.DbConnection != null && this.DbConnection.State == ConnectionState.Open;

      try
      {
        if (!CnnOpen)
        { this.DbConnection.Open(); }

        DataTable dt = this.DbConnection.GetSchema("ForeignKeys");
        ForeignKey[] list = new ForeignKey[dt.Rows.Count];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
          list[i] = new ForeignKey();
          list[i].ConstraintName = dt.Rows[i]["CONSTRAINT_NAME"].ToString();

          list[i].TableName = dt.Rows[i]["TABLE_NAME"].ToString();
          list[i].ColumnName = dt.Rows[i]["FKEY_FROM_COLUMN"].ToString();
          list[i].TableReference = dt.Rows[i]["FKEY_TO_TABLE"].ToString();
          list[i].ColumnReference = dt.Rows[i]["FKEY_TO_COLUMN"].ToString();
        }

        return list;
      }
      catch
      { return new ForeignKey[] { }; }
      finally
      {
        if (!CnnOpen)
        { this.DbConnection.Close(); }
      }
    }
    #endregion

    #region Methods
    #region private void TestConnection()
    private void TestConnection()
    {
      try
      { this.Open(); }      
      finally
      {
        if (this.DbConnection != null)
        { this.Close(); }
      }
    }
    #endregion

    #region private void PrepareSql(ref string sql)
    private void PrepareSql(ref string sql, int max_rows) 
    {
      if (max_rows != 0)
      { sql = GetLimitLines(sql, max_rows); }

      if (QueryParam.HasParameters)
      { sql = string.Format(sql, QueryParam.Get()); }

      QueryParam.Clear();

      LastSql = sql;
    }
    #endregion

    #region public bool InTransaction()
    public bool InTransaction()
    {
      return Transaction != null;
    }
    #endregion

    #region protected void Open()
    protected void Open()
    {
      if (this.DbConnection.State != ConnectionState.Open)
      { DbConnection.Open(); }
    }
    #endregion

    #region protected void Close()
    protected void Close()
    {
      if (!InTransaction())
      { this.DbConnection.Close(); }
    }
    #endregion

    #region public bool IsConnected()
    public bool IsConnected()
    { return DbConnection != null; }
    #endregion

    #region public void BeginTransaction()
    public void BeginTransaction()
    {
      if (this.DbConnection.State != ConnectionState.Open)
      { this.Open(); }
      Transaction = this.DbConnection.BeginTransaction();
    }
    #endregion

    #region public void CommitTransaction()
    public void CommitTransaction() 
    {
      try
      {
        if (Transaction != null)
        { Transaction.Commit(); }
      }
      finally
      {
        if (Transaction != null)
        {
          Transaction.Dispose();
          Transaction = null;
        }
        this.Close();
      }
    }
    #endregion

    #region public void RollbackTransaction()
    public void RollbackTransaction() 
    {
      try 
      {
        if (Transaction != null)
        {
          Transaction.Rollback();          
        }
      }
      finally
      {
        if (Transaction != null)
        {
          Transaction.Dispose();
          Transaction = null;          
        }
        this.Close();
      }
    }
    #endregion

    #region public Conversion Sql(string sql)
    public Conversion Sql(string sql) 
    {
      DbCommand cmd = null;
      DbDataReader dr = null;

      try
      {
        PrepareSql(ref sql, 0);
        this.Open();
        cmd = this.DbConnection.CreateCommand();
        cmd.CommandText = sql;

        if (Transaction != null)
        { cmd.Transaction = this.Transaction; }

        dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
          dr.Read();
          return new Conversion(dr.GetValue(0));
        }
        return new Conversion(null);
      }
      catch (Exception ex)
      {
        if (InTransaction())
        { RollbackTransaction(); }
        throw new Exception("Erro ao executar a sql:" + sql, ex); 
      }
      finally 
      {
        if (dr != null)
        {
          dr.Close();
          dr.Dispose();
          dr = null;
        }

        if (cmd != null) 
        {
          cmd.Dispose();
          cmd = null;
        }

        this.Close();
      }
    }
    #endregion

    #region public DataSet GetDataSet(string sql)
    public DataSet GetDataSet(string sql, int max_rows, bool WithSchema) 
    {
      DbDataAdapter da = null;

      try
      {
        PrepareSql(ref sql, max_rows);
        this.Open();
        da = this.GetDataAdapter(sql);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQL");

        if (WithSchema)
        { da.FillSchema(ds, SchemaType.Mapped); }

        return ds;
      }
      catch (Exception ex)
      {
        if (InTransaction())
        { RollbackTransaction(); }
        throw new Exception("Erro ao executar a sql : " + sql, ex); 
      }
      finally
      {
        if (da != null)
        {
          da.Dispose();
          da = null;
        }
        this.Close();
      }
    }
    #endregion

    #region public DataTable GetDataTable(string sql)    
    public DataTable GetDataTable(string sql, string TableName, int max_rows, bool WithSchema) 
    {
      DbDataAdapter da = null;

      try
      {
        PrepareSql(ref sql, max_rows);
        this.Open();
        da = this.GetDataAdapter(sql);
        DataTable tb = new DataTable(TableName);
        da.Fill(tb);

        if (WithSchema)
        { da.FillSchema(tb, SchemaType.Mapped); }

        return tb;
      }
      catch (Exception ex)
      {
        if (InTransaction())
        { RollbackTransaction(); }
        throw new Exception("Erro ao executar a sql : " + sql , ex); 
      }
      finally 
      {
        if (da != null) 
        {
          da.Dispose();
          da = null;
        }
        this.Close();
      }
    }
    #endregion

    #region public bool Exec(string sql)
    public bool Exec(string sql)
    {
      DbCommand cmd = null;
      try
      {
        PrepareSql(ref sql, 0);

        this.Open();
        cmd = this.DbConnection.CreateCommand();
        cmd.CommandText = sql;

        if (Transaction != null)
        { cmd.Transaction = Transaction; }

        ExecutedCommands = cmd.ExecuteNonQuery();
        return true;
      }
      catch (Exception ex)
      {
        if (InTransaction())
        { RollbackTransaction(); }
        throw new Exception("Erro ao executar a sql : " + sql, ex); 
      }
      finally 
      {
        if (cmd != null) 
        {
          cmd.Dispose();
          cmd = null;
        }

        this.Close();
      }
    }
    #endregion
    #endregion
  }
  #endregion
  
  #region public class InfoConnection
  public class InfoConnection
  {
    #region Constructor
    public InfoConnection()
    { SetInfoConnection(false, "", "", "", ""); }

    public InfoConnection(
      string aServer,
      string aDatabase,
      string aUser,
      string aPassword)
    { SetInfoConnection(false, aServer, aDatabase, aUser, aPassword); }

    public InfoConnection(
      bool aUseWindowsAuthentication,
      string aServer,
      string aDatabase,
      string aUser,
      string aPassword)
    { SetInfoConnection(aUseWindowsAuthentication, aServer, aDatabase, aUser, aPassword); }

    private void SetInfoConnection(
      bool aUseWindowsAuthentication,
      string aServer,
      string aDatabase,
      string aUser,
      string aPassword) 
    {
      this.UseWindowsAuthentication = aUseWindowsAuthentication;
      this.Server = aServer;
      this.Database = aDatabase;
      this.User = aUser;
      this.Password = aPassword;
    }
    #endregion

    #region Fields
    public bool UseWindowsAuthentication { get; set; }
    public string Server { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string ConnectionString { get; set; }
    #endregion

    #region Methods
    public override string ToString()
    {
      if (UseWindowsAuthentication)
      { return this.Server + "; " + this.Database + "; WindowsAuthentication"; }
      else
      { return this.Server + "; " + this.Database + "; " + this.User + "; " + this.Password; }
    }
    #endregion
  }
  #endregion

  #region Objeto centralizador das conecções
  public class DriverConnection
  {
    #region public virtual bool Connect(enmConnection dbType, InfoConnection Info)
    public virtual bool Connect(enmConnection dbType, InfoConnection Info)
    {
      switch (dbType)
      {
        case enmConnection.Access: { db = new Access(Info); break; }
        case enmConnection.Interbase: { db = new Interbase(Info); break; }
        case enmConnection.Firebird: { db = new Firebird(Info); break; }
        case enmConnection.MySql: { db = new MySql(Info); break; }
        case enmConnection.SqlServer: { db = new SqlServer(Info); break; }
        case enmConnection.SqlServerCe: { db = new SqlServerCe(Info); break; }
        case enmConnection.SQLite: { db = new SQLite(Info); break; }
        case enmConnection.Oracle: { db = new OracleODAC(Info); break; }
        case enmConnection.Odbc: { db = new Odbc(Info); break; }
        case enmConnection.OleDb: { db = new OleDb(Info); break; }
        default:
          { break; }
      }

      return db.IsConnected();
    }
    #endregion

    #region public virtual bool Connect(enmConnection dbType, DbConnection DbConnection, ErrorList ErrorList)
    public virtual bool Connect(enmConnection dbType, DbConnection DbConnection)
    {
      switch (dbType)
      {
        case enmConnection.Access: { db = new Access(DbConnection); break; }
        case enmConnection.Interbase: { db = new Interbase(DbConnection); break; }
        case enmConnection.Firebird: { db = new Firebird(DbConnection); break; }
        case enmConnection.MySql: { db = new MySql(DbConnection); break; }
        case enmConnection.SqlServer: { db = new SqlServer(DbConnection); break; }
        case enmConnection.SqlServerCe: { db = new SqlServerCe(DbConnection); break; }
        case enmConnection.SQLite: { db = new SQLite(DbConnection); break; }
        case enmConnection.Oracle: { db = new OracleODAC(DbConnection); break; }
        case enmConnection.Odbc: { db = new Odbc(DbConnection); break; }
        case enmConnection.OleDb: { db = new OleDb(DbConnection); break; }
        default:
          { break; }
      }

      return db.IsConnected();
    }
    #endregion

    #region Fields
    public DbBase db { get; set; }
    #endregion
  }//public class Connection 
  #endregion
}
