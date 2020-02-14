using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public class Access:DbBase
  {
    public Access(string ConnectionString)
      : base(ConnectionString)
    {
      DbCreateConnection();
    }

    protected override void DbCreateConnection()
    {
      try
      {
        this.ConnectionType = Entity.ConnectionType.Access;
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

    public static string CreateConnectionString(string DatabaseFile)
    {
      if (System.IO.Path.GetExtension(DatabaseFile).ToUpper() == ".MDB")
      { return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", DatabaseFile); }
      else
      { return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", DatabaseFile); }
    }
  }
}
