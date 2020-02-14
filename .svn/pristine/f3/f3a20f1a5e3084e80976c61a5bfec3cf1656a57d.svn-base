using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using lib.Database;
using lib.Database.Drivers;

namespace lib.Database.MVC
{
  public class DefaultDataSource<T> where T : DefaultEntity
  {    
    public DefaultDataSource(Connection cnn) 
    {
      this.cnn = cnn;
      this.sb = new SqlBuild(cnn.dbu, false);
    }
    
    public Database.Connection cnn { get; set; }
    public Database.SqlBuild sb { get; set; }
        
    protected virtual void OnFillItem(T Tab) { }

    public virtual LockedField[] GetLockedFields(T Tab) 
    { return new LockedField[] { }; }

    protected T Get(string Sql)
    {
      T[] lst = GetList(Sql, 1);
      if (lst.Length != 0)
      { return lst[0]; }
      else
      { return Activator.CreateInstance<T>(); }
    }

    protected T[] GetList(string Sql, int max_rows)
    {
      DataSource ds = new DataSource(this.cnn.GetDataTable(Sql, max_rows, false));
      T[] lst = new T[ds.Count];
      for (int i = 0; i < ds.Count; i++)
      {
        T Tab = Activator.CreateInstance<T>();        
        ds.FillObject(i, Tab);
        OnFillItem(Tab);
        lst[i] = Tab;
      }
      return lst;
    }

    public T[] GetList() 
    {
      return GetList(string.Format("select * from {0}", typeof(T).Name), 0);
    }
  }
}
