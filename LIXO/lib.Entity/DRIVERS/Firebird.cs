using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace lib.Entity
{
  public class Firebird : DbBase
  {
    public Firebird(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.Firebird;
        this.DbSetDateFormat = "";
        this.DbConnection = new FbConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o driver Fb", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new FbDataAdapter(sql, (FbConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new FbDataAdapter((FbCommand)cmd);
    }

    public static string CreateConnectionString(string Server, string DatabaseFile, string User, string Password)
    {
      return
        string.Format(
          "User={0};Password={1};Database={2};DataSource={3};Port=3050;" +
          "Dialect=3;Charset=NONE;Role=;Connection lifetime=0;Connection timeout=15;Pooling=True;" +
          "Packet Size=8192;Server Type=0",
          new object[]
            {
              User,
              Password,
              DatabaseFile,
              Server
            }
        );
    }
  }
}
