using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.SqlServerCe;

namespace lib.Entity
{
  public class SqlServerCe : DbBase
  {
    public SqlServerCe(string ConnectionString, string DatabaseFile)
      : base(ConnectionString)
    {
      this.DatabaseFile = DatabaseFile;
      DbCreateConnection();
    }

    public string DatabaseFile { get; set; }

    protected override void DbCreateConnection()
    {
      try
      {
        this.DbSetDateFormat = "set dateformat ymd";
        this.DbReturnLastID = "select @@identity";

        if (!System.IO.File.Exists(DatabaseFile))
        {
          System.Data.SqlServerCe.SqlCeEngine engine = new System.Data.SqlServerCe.SqlCeEngine(ConnectionString);
          engine.CreateDatabase();
        }

        this.DbConnection = new System.Data.SqlServerCe.SqlCeConnection(ConnectionString);
      }
      catch (Exception ex)
      { throw new Exception("Erro ao criar a conexão com o driver", ex); }
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(string sql)
    {
      return new System.Data.SqlServerCe.SqlCeDataAdapter(sql, (System.Data.SqlServerCe.SqlCeConnection)this.DbConnection);
    }

    public override System.Data.Common.DbDataAdapter DbCreateDataAdapter(System.Data.Common.DbCommand cmd)
    {
      return new System.Data.SqlServerCe.SqlCeDataAdapter((System.Data.SqlServerCe.SqlCeCommand)cmd);
    }

    public static string CreateConnectionString(string DataSource, string Password)
    {
      return
          string.Format("data source={0}; Persist Security Info=False; Password={1};",
           DataSource, Password);
    }
  } 
}