using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public class OracleODAC:DbBase
  {
    public OracleODAC(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.Oracle;
        this.DbSetDateFormat = "";
        this.DbConnection = new Oracle.ManagedDataAccess.Client.OracleConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o driver", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new Oracle.ManagedDataAccess.Client.OracleDataAdapter(sql, (Oracle.ManagedDataAccess.Client.OracleConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new Oracle.ManagedDataAccess.Client.OracleDataAdapter((Oracle.ManagedDataAccess.Client.OracleCommand)cmd);
    }

    public static string CreateConnectionString(string Server, string UserId, string Password)
    {
      return 
        string.Format(
          "User Id={0}; Password={1}; " +
          "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {2})(PORT = 1521)))" +
          "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = XE)));",
          new object[]
            {              
              UserId,
              Password,
              Server
            }
        );
    }
  }
}
