using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace lib.Entity
{
  public class SimpleDataSource
  {
    public SimpleDataSource(DbBase DbBase)
    {
      this.cnv = new Class.Conversion();
      this.DbBase = DbBase;
    }

    protected DbBase DbBase { get; set; }
    protected DbConnection DbConnection { get { return DbBase.DbConnection; } }
    protected virtual void OnFillItem<T>(T Tab) { }
    protected lib.Class.Conversion cnv { get; set; }

    protected string DbQuoted(string s)
    {
      return DbBase.DbQuoted(s);
    }

    public void FillObject(DataTable dt, int Row, object Obj)
    {
      FillObject(dt.Rows[Row], Obj);
    }

    public void FillObject(DataRow dr, object Obj)
    {
      lib.Class.Reflection oa = new lib.Class.Reflection(Obj);
      for (int i = 0; i < dr.Table.Columns.Count; i++)
      {
        string ColName = dr.Table.Columns[i].ColumnName;
        oa.SetAttibute(ColName, dr[ColName].ToString());
      }
    }

    public T Get<T>(string Sql, System.Data.Common.DbTransaction transaction = null)
    {
      T[] lst = List<T>(Sql, transaction);
      if (lst.Length != 0)
      { return lst[0]; }
      else
      { return Activator.CreateInstance<T>(); }
    }

    public T[] List<T>(DbDataReader dr, List<string> columns = null)
    {
      List<T> list = new List<T>();
      while (dr.Read())
      {
        T md = Activator.CreateInstance<T>();
        lib.Class.Reflection rfc = new lib.Class.Reflection(md);
        string[] cols = rfc.GetProperties();

        for (int i = 0; i < cols.Length; i++)
        {
          int ordinal = -1;
          if (columns == null)
          {
            ordinal = dr.GetOrdinal(cols[i]);
          }
          else
          {
            ordinal = columns.IndexOf(cols[i].ToUpper());
          }

          if (ordinal >= 0)
          {
            string columnName =cols[i]; 
            object columnValue = dr.GetValue(ordinal);
            rfc.SetAttibute(columnName, columnValue);
          }
        }
        list.Add(md);
        DbBase.OnLineCounter();
      }
      return list.ToArray();
    }

    private List<string> GetDataReaderColumns(DbDataReader dr)
    {
      List<string> dr_cols = new List<string>();
      for (int i = 0; i < dr.FieldCount; i++)
      { dr_cols.Add(dr.GetName(i).ToUpper()); }
      return dr_cols;
    }

    public T[] List<T>(string Sql, System.Data.Common.DbTransaction transaction = null)
    {
      DbBase.LastSQL = Sql;

      System.Data.Common.DbCommand cmd = null;
      System.Data.Common.DbDataReader dataReader = null;
      T[] result = null;

      try
      {

        if (transaction == null && DbConnection.State != ConnectionState.Open)
        { DbConnection.Open(); }

        cmd = DbBase.DbConnection.CreateCommand();
        cmd.CommandText = Sql;

        if (transaction != null)
        { cmd.Transaction = transaction; }

        dataReader = cmd.ExecuteReader();
        List<string> columns = GetDataReaderColumns(dataReader);
        result = List<T>(dataReader, columns);
      }
      finally
      {
        if (dataReader != null)
        {
          dataReader.Close();
          dataReader.Dispose();
          dataReader = null;
        }

        if (cmd != null)
        {
          cmd.Dispose();
          cmd = null;
        }

        if (transaction == null)
        { DbConnection.Close(); }
      }

      if (result == null)
      { return new T[] { }; }

      return result;
    }

    public string GetDbValue(lib.Class.Reflection r, System.Reflection.PropertyInfo pi, bool IsSelect)
    {
      string value = "";
      Type tp = pi.PropertyType;

      lib.Class.Conversion Obj = new lib.Class.Conversion(r.GetPropertyValue(pi));

      if (tp == typeof(string))
      {
        if (IsSelect)
        { value = string.Format("{0}", DbQuoted("%" + Obj.ToString() + "%")); }
        else
        { value = string.Format("{0}", DbQuoted(Obj.ToString())); }
      }
      else if (tp == typeof(DateTime))
      {
        DateTime dt = Obj.ToDateTime();

        if (dt < lib.Class.Utils.StartGregorianCalendar())
        { dt = DateTime.MinValue; }

        if (dt == DateTime.MinValue)
        { value = "NULL"; }
        else
        { value = string.Format("'{0}'", dt.ToString("yyyy-MM-dd HH:mm:ss")); }
      }
      else if (tp == typeof(bool))
      { value = Obj.ToBool() ? "1" : "0"; }
      else if (tp == typeof(decimal) || tp == typeof(double) || tp == typeof(float))
      { value = Obj.ToDecimal().ToString("0.00").Replace(",", "."); }
      else if (tp == typeof(int) || tp == typeof(long))
      { value = Obj.ToString(); }
      else
      { value = Obj.ToString(); }
      return value;
    }

    public lib.Class.Conversion ReturnLastID(System.Data.Common.DbTransaction transaction)
    {
      return DbBase.ReturnLastID(transaction);
    }

    public string SetMaxLength(string s, int l)
    {
      if (string.IsNullOrEmpty(s))
      { return ""; }

      if (s.Length > l)
      { s = s.Substring(0, l); }

      return s;
    }

    private System.Reflection.PropertyInfo[] GetValidProperties(lib.Class.Reflection r)
    {
      List<System.Reflection.PropertyInfo> l = new List<System.Reflection.PropertyInfo>();
      for (int i = 0; i < r.Properties.Length; i++)
      {
        KeyTypeAttributeState state = GetKeyTypeAttributeState(r.Properties[i]);
        if (!r.IsDefaultValue(r.Properties[i]) && !state.IsQueryField)
        { l.Add(r.Properties[i]); }
      }
      return l.ToArray();
    }

    protected string Condition<T>(T values)
    {
      if (values == null)
      { return ""; }

      lib.Class.Reflection r = new lib.Class.Reflection(values);
      string where = "";
      System.Reflection.PropertyInfo[] props = GetValidProperties(r);

      for (int i = 0; i < props.Length; i++)
      {
        Type tp = props[i].PropertyType;
        string signal = (tp == typeof(string) ? "LIKE" : "=");
        string value = GetDbValue(r, props[i], true);
        where += string.Format("{0} {1} {2}", props[i].Name, signal, value) + (i == (props.Length - 1) ? " " : " and ");
      }

      return string.Format("WHERE {0}", where);
    }

    private KeyTypeAttributeState GetKeyTypeAttributeState(System.Reflection.PropertyInfo pi)
    {
      KeyTypeAttributeState state = new KeyTypeAttributeState();
      CustomAttributeField[] arr = (CustomAttributeField[])pi.GetCustomAttributes(typeof(CustomAttributeField), false);
      for (int i = 0; i < arr.Length; i++)
      {
        if (arr[i].KeyType == KeyTypeAttribute.PrimaryKey)
        { state.IsPrimaryKey = true; }
        else if (arr[i].KeyType == KeyTypeAttribute.ForeignKey)
        { state.IsForeignKey = true; }
        else if (arr[i].KeyType == KeyTypeAttribute.QueryField)
        { state.IsQueryField = true; }
        else if (arr[i].KeyType == KeyTypeAttribute.OmitIfIsNull)
        { state.IsOmitIfIsNull = true; }
      }
      return state;
    }

    private bool IsInCondition(string FieldName, string[] FieldsCondition)
    {
      if (FieldsCondition == null || FieldsCondition.Length == 0)
      { return false; }

      for (int i = 0; i < FieldsCondition.Length; i++)
      {
        if (FieldName == FieldsCondition[i])
        { return true; }
      }
      return false;
    }

    public T[] Select<T>(T Conditions, System.Data.Common.DbTransaction transaction = null)
    {
      return List<T>(string.Format("{0} SELECT * FROM {1} {2} {3}", DbBase.DbSetDateFormat, typeof(T).Name, DbBase.DbWithNolock, Condition(Conditions)), transaction);
    }

    public T GetFirstOrDefault<T>(T Conditions, System.Data.Common.DbTransaction transaction = null)
    {
      T[] list = Select(Conditions, transaction);
      if (list.Length != 0)
      { return list[0]; }
      else
      { return Activator.CreateInstance<T>(); }
    }

    public void Insert<T>(T entity, DbTransaction Transaction = null)
    {
        lib.Class.Reflection r = new lib.Class.Reflection(entity);

        if (r.Properties.Length == 0)
        { return; }

        string db_fields = "";
        for (int i = 0; i < r.Properties.Length; i++)
        {
            KeyTypeAttributeState state = GetKeyTypeAttributeState(r.Properties[i]);
            bool IsDefaultValue = r.IsDefaultValue(r.Properties[i]);

            if (state.IsQueryField || (IsDefaultValue && state.IsOmitIfIsNull))
            { continue; }

            if (!state.IsPrimaryKey && !(state.IsForeignKey && IsDefaultValue))
            { db_fields += (string.IsNullOrEmpty(db_fields) ? "" : ",") + r.Properties[i].Name; }
        }

        string db_values = "";
        for (int i = 0; i < r.Properties.Length; i++)
        {
            KeyTypeAttributeState state = GetKeyTypeAttributeState(r.Properties[i]);
            bool IsDefaultValue = r.IsDefaultValue(r.Properties[i]);

            if (state.IsQueryField || (IsDefaultValue && state.IsOmitIfIsNull))
            { continue; }

            if (!state.IsPrimaryKey && !(state.IsForeignKey && IsDefaultValue))
            { db_values += (string.IsNullOrEmpty(db_values) ? "" : ",") + GetDbValue(r, r.Properties[i], false); }
        }

        string sql = string.Format("{0} INSERT INTO {1} ({2}) VALUES ({3});", DbBase.DbSetDateFormat, typeof(T).Name, db_fields, db_values);
        DbBase.DbExecute(sql, Transaction);
    }

    public void Update<T>(T entity, T Conditions, DbTransaction Transaction = null)
    {
      lib.Class.Reflection r = new lib.Class.Reflection(entity);

      if (r.Properties.Length == 0)
      { return; }

      System.Reflection.PropertyInfo[] PropsCondition = null;

      if (Conditions != null)
      {
          PropsCondition = GetValidProperties(new lib.Class.Reflection(Conditions));
      }

      string db_fields_values = "";
      for (int i = 0; i < r.Properties.Length; i++)
      {
        KeyTypeAttributeState state = GetKeyTypeAttributeState(r.Properties[i]);
        bool IsDefaultValue = r.IsDefaultValue(r.Properties[i]);

        if (state.IsQueryField || state.IsPrimaryKey || (IsDefaultValue && state.IsOmitIfIsNull))
        { continue; }

        if (state.IsForeignKey && IsDefaultValue)
        { db_fields_values += (string.IsNullOrEmpty(db_fields_values) ? "" : ",") + string.Format("{0}=NULL", r.Properties[i].Name); }
        else
        { db_fields_values += (string.IsNullOrEmpty(db_fields_values) ? "" : ",") + string.Format("{0}={1}", r.Properties[i].Name, GetDbValue(r, r.Properties[i], false)); }
      }

      DbBase.DbExecute(
        string.Format("{0} UPDATE {1} SET {2} {3}", DbBase.DbSetDateFormat, typeof(T).Name, db_fields_values, Condition(Conditions)),
        Transaction);
    }

    public void RemoveAllData<T>(DbTransaction Transaction = null)
    {
      DbBase.DbExecute(string.Format("DELETE FROM {0}", typeof(T).Name), Transaction);
    }

    public int Count<T>(DbTransaction Transaction = null)
    {
      try
      {
        return Convert.ToInt32(DbBase.DbGetValue(string.Format("SELECT COUNT(*) FROM {0} {1}", typeof(T).Name, DbBase.DbWithNolock), Transaction));
      }
      catch { return 0; }
    }

    public void Remove<T>(T Conditions, DbTransaction Transaction = null)
    {
      DbBase.DbExecute(string.Format("{0} DELETE FROM {1} {2}", DbBase.DbSetDateFormat, typeof(T).Name, Condition(Conditions)), Transaction);
    }

    public virtual LockedField[] CheckField<T>(T Fields)
    { return new LockedField[] { }; }

    private static string[] GetAttributes(object _o)
    {
        List<string> lst = new List<string>();
        string[] fields = _o.GetType().GetFields().Select(q => q.Name).ToArray<string>();
        string[] properties = _o.GetType().GetProperties().Select(q => q.Name).ToArray<string>();
        lst.AddRange(fields);
        lst.AddRange(properties);
        return lst.ToArray();
    }

    private string getCondition(object condition)
    {
        string[] attrs = GetAttributes(condition);

        string result = "";
        foreach (var item in attrs)
        {
            result += (string.IsNullOrEmpty(result) ? "" : " and ") + string.Format("{0} = @{0}", item);
        }
        return "where " + result;
    }

    public IEnumerable<T> Where<T>(object condition)
    {
        return this.List<T>(
            @"select * 
            from " + string.Format("[{0}]", typeof(T).Name) + " " + getCondition(condition));
    }

    public T First<T>(object condition)
    {
        //return Where(condition).First();
        return FirstOrDefault<T>(condition);
    }

    public T FirstOrDefault<T>(object condition)
    {
        var entity = Where<T>(condition).FirstOrDefault();
        if (entity == null)
        { entity = Activator.CreateInstance<T>(); }
        return entity;
    }

  }
}
