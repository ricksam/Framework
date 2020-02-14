using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public class SQLite : DbBase
  {
    public SQLite(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.SQLite;
        this.DbSetDateFormat = "";
        this.DbReturnLastID = "select last_insert_rowid()";
        this.DbWithNolock = "";
        this.DbConnection = new System.Data.SQLite.SQLiteConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o SQLite", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new System.Data.SQLite.SQLiteDataAdapter(sql, (System.Data.SQLite.SQLiteConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new System.Data.SQLite.SQLiteDataAdapter((System.Data.SQLite.SQLiteCommand)cmd);
    }

    public static string CreateConnectionString(string DatabaseFile)
    {
      return        
          string.Format(
          "Data Source={0};Pooling=true;FailIfMissing=false",
          new object[] { DatabaseFile }
        );
    }
  }
}
