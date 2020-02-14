using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

namespace lib.Entity
{
  public class MySQL : DbBase
  {
    public MySQL(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.MySql;
        this.DbSetDateFormat = "";
        this.DbReturnLastID = "select last_insert_id()";
        this.DbWithNolock = "";

        this.DbConnection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o SQL Server", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new MySql.Data.MySqlClient.MySqlDataAdapter(sql, (MySql.Data.MySqlClient.MySqlConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new MySql.Data.MySqlClient.MySqlDataAdapter((MySql.Data.MySqlClient.MySqlCommand)cmd);
    }

    public static string CreateConnectionString(string Server, string Database, string User, string Password)
    {
      return
          string.Format(
            "Server={0};Database={1};Uid={2};Pwd={3};",
            new object[]
            {
              Server,
              Database,
              User,
              Password
            }
          );
    }
  }
}