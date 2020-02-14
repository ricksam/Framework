using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public class OleDb:DbBase
  {
    public OleDb(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.OleDb;
        this.DbSetDateFormat = "";
        this.DbConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o driver", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new System.Data.OleDb.OleDbDataAdapter(sql, (System.Data.OleDb.OleDbConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new System.Data.OleDb.OleDbDataAdapter((System.Data.OleDb.OleDbCommand)cmd);
    }

    public static string CreateConnectionString(string Provider, string DataSource, string UserId, string Password)
    {
      return string.Format("Provider={0};Data Source={1};User Id={2};Password={3};", Provider, DataSource, UserId, Password);
    }
  }
}
