using System;
using System.Collections.Generic;
using System.Linq;

namespace lib.Entity
{
  public class SqlServer : DbBase
  {
    public SqlServer(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.SqlServer;
        this.DbSetDateFormat = "set dateformat ymd";
        this.DbReturnLastID = "select @@identity";
        this.DbWithNolock = "WITH(NOLOCK)";
        //this.DbWithNolock = "";
        this.DbConnection = new System.Data.SqlClient.SqlConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o SQL Server", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new System.Data.SqlClient.SqlDataAdapter(sql, (System.Data.SqlClient.SqlConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)cmd);
    }

    public static string CreateConnectionString(bool UseWindowsAuthentication, string DataSource, string InitialCatalog, string UserId, string Password)
    {
      if (UseWindowsAuthentication)
      {
        return
        string.Format(
        "Data Source={0};Initial Catalog={1};Integrated Security=True",
          new object[]
            {
              DataSource,
              InitialCatalog
            }
        );
      }
      else
      {
        return
          string.Format(
          "packet size=4096;user id={0};" +
          "password={1};data source={2};" +
          "persist security info=false;initial catalog={3}",
          new object[]
          {
            UserId,
            Password,
            DataSource,
            InitialCatalog            
          }
        );
      }
    }
  }
}