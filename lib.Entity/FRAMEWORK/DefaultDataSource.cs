using System;
using System.Data;
using System.Data.Common;
//using System.Collections.Generic;
using System.Text;

using lib.Class;

namespace lib.Entity
{
  public class DefaultDataSource<T> : SimpleDataSource where T : DefaultEntity
  {
    public DefaultDataSource(DbBase DbBase)
      : base(DbBase)
    {
    }

    public int Count(DbTransaction Transaction = null)
    {
      return base.Count<T>(Transaction);
    }

    protected T Get(string Sql, System.Data.Common.DbTransaction transaction = null) 
    {
      return base.Get<T>(Sql, transaction);
    }

    protected T[] List(string Sql, System.Data.Common.DbTransaction transaction = null) 
    {
      return base.List<T>(Sql, transaction);
    }

    public T[] List(System.Data.Common.DbTransaction transaction = null)
    {
      return base.List<T>(string.Format("SELECT * FROM {0} {1}", typeof(T).Name, DbBase.DbWithNolock), transaction);
    }

    /*
    protected T Get(string Sql, System.Data.Common.DbTransaction transaction = null)
    {
      T[] lst = List<T>(Sql, transaction);
      if (lst.Length != 0)
      { return lst[0]; }
      else
      { return Activator.CreateInstance<T>(); }
    }

    protected T[] List(string Sql, System.Data.Common.DbTransaction transaction = null)
    {
      DataTable dt = DbBase.DbGetDataTable(Sql, transaction);
      T[] lst = new T[dt.Rows.Count];
      for (int i = 0; i < dt.Rows.Count; i++)
      {
        T Tab = Activator.CreateInstance<T>();
        FillObject(dt, i, Tab);
        OnFillItem<T>(Tab);
        lst[i] = Tab;
      }
      return lst;
    }

    public T[] List(System.Data.Common.DbTransaction transaction = null)
    {
      return List<T>(string.Format("SELECT * FROM {0} {1}", typeof(T).Name, DbBase.DbWithNolock), transaction);
    }

    
    */
  }
}
