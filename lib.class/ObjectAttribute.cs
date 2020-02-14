using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using lib.Class;

namespace lib.Class
{
  [Serializable]
  public class ObjectAttribute
  {
    #region Constructor
    public ObjectAttribute(object o) 
    {
      this.cnv = new Conversion();
      this._o = o;

      this.Fields = this._o.GetType().GetFields();
      this.Properties = this._o.GetType().GetProperties();
      
      this.fields = GetFields();
      this.properties = GetProperties();
    }
    #endregion

    #region Fields
    private object _o { get; set; }
    public FieldInfo[] Fields { get; set; }
    public PropertyInfo[] Properties { get; set; }
    private Conversion cnv { get; set; }
    private string[] fields { get; set; }
    private string[] properties { get; set; }
    #endregion

    #region Field Property Attribute Exists
    public bool FieldExists(string FieldName)
    {
      for (int i = 0; i < fields.Length; i++)
      {
        if (fields[i] == FieldName)
        { return true; }
      }
      return false;
    }

    public bool PropertyExists(string PropertyName)
    {
      for (int i = 0; i < properties.Length; i++)
      {
        if (properties[i] == PropertyName)
        { return true; }
      }
      return false;
    }

    public bool AttibuteExists(string AttributeName)
    {
      return FieldExists(AttributeName) || PropertyExists(AttributeName);
    }
    #endregion

    #region Index Field Property Attribute 
    private int IndexField(string FieldName)
    {
      for (int i = 0; i < fields.Length; i++)
      {
        if (fields[i] == FieldName)
        { return i; }
      }
      return -1;
    }

    private int IndexProperty(string PropertyName)
    {
      for (int i = 0; i < properties.Length; i++)
      {
        if (properties[i] == PropertyName)
        { return i; }
      }
      return -1;
    }
    #endregion

    #region Get Fields Properties Attibutes
    public string[] GetFields()
    {
      string[] sl = new string[Fields.Length];
      for (int i = 0; i < Fields.Length; i++)
      { sl[i] = Fields[i].Name; }
      return sl;
    }
    
    public string[] GetProperties() 
    {
      string[] sl = new string[Properties.Length];
      for (int i = 0; i < Properties.Length; i++)
      { sl[i] = Properties[i].Name; }
      return sl;
    }

    public string[] GetAttributes() 
    {
      List<string> lst = new List<string>();
      lst.AddRange(fields);
      lst.AddRange(properties);
      return lst.ToArray();
    }
    #endregion

    #region Get Field Property Attibute
    public object GetField(string FieldName)
    {
      FieldInfo fi;
      if (this._o != null)
      {
        for (int i = 0; i < Fields.Length; i++)
        {
          fi = Fields[i];
          if (fi.Name == FieldName)
          { return GetField(fi); }
        }
      }
      return null;
    }

    public object GetField(FieldInfo fi)
    {
      return fi.GetValue(this._o);
    }

    public object GetProperty(string PropertyName)
    {
      PropertyInfo pi;
      if (this._o != null)
      {
        for (int i = 0; i < Properties.Length; i++)
        {
          pi = Properties[i];
          if (pi.Name == PropertyName && pi.CanRead)
          { return GetProperty(pi); }
        }
      }
      return null;
    }

    public object GetProperty(PropertyInfo pi)
    { return pi.GetValue(this._o, null); }

    public object GetAttribute(string AttibuteName) 
    {
      int Idx;
      if ((Idx = IndexField(AttibuteName)) != -1)
      { return GetField(Fields[Idx]); }

      if ((Idx = IndexProperty(AttibuteName)) != -1)
      { return GetProperty(Properties[Idx]); }
      return null;
    }
    #endregion

    #region GetAttributeType()
    public Type GetAttributeType(string AttributeName)
    {
      for (int i = 0; i < Fields.Length; i++)
      {
        if (Fields[i].Name == AttributeName)
        { return Fields[i].FieldType; }
      }

      for (int i = 0; i < Properties.Length; i++)
      {
        if (Properties[i].Name == AttributeName)
        { return Properties[i].PropertyType; }
      }

      return typeof(string);
    }
    #endregion

    #region private object GetDefultValue(Type ToType)
    private object GetDefultValue(Type ToType)
    {
      if (ToType == typeof(string))
      { return cnv.ToString(""); }
      else if (ToType == typeof(int))
      { return cnv.ToInt(0); }
      else if (ToType == typeof(long))
      { return cnv.ToLong(0); }
      else if (ToType == typeof(decimal))
      { return cnv.ToDecimal(0); }
      else if (ToType == typeof(DateTime))
      { return cnv.ToDateTime(DateTime.MinValue); }
      else if (ToType == typeof(bool))
      { return cnv.ToBool(false); }
      return null;
    }
    #endregion

    /*#region private object GetDefultValue(Type ToType)
    private object GetDefultValue(Type ToType)
    {
      if (ToType == typeof(string))
      { return Convert.ToString(""); }
      else if (ToType == typeof(int))
      { return Convert.ToInt32(0); }
      else if (ToType == typeof(long))
      { return Convert.ToInt64(0); }
      else if (ToType == typeof(decimal))
      { return Convert.ToDecimal(0); }
      else if (ToType == typeof(DateTime))
      { return Convert.ToDateTime(DateTime.MinValue); }
      else if (ToType == typeof(bool))
      { return Convert.ToBoolean(false); }
      return null;
    }
    #endregion*/

    #region private object GetValue(Type ToType, object Value)
    private object GetValue(Type ToType, object Value)
    {
      if (ToType == typeof(string))
      { return cnv.ToString(Value); }
      else if (ToType == typeof(int))
      { return cnv.ToInt(Value); }
      else if (ToType == typeof(long))
      { return cnv.ToLong(Value); }
      else if (ToType == typeof(decimal))
      { return cnv.ToDecimal(Value); }
      else if (ToType == typeof(DateTime))
      { return cnv.ToDateTime(Value); }
      else if (ToType == typeof(bool))
      { return cnv.ToBool(Value); }
      else
      {
        if (Value != null)
        {
          if (ToType == Value.GetType())
          { return Value; }
        }
      }
      return null;
    }
    #endregion

    /*#region private object GetValue(Type ToType, object Value)
    private object GetValue(Type ToType, object Value)
    {
      if (ToType == typeof(string))
      { return Convert.ToString(Value); }
      else if (ToType == typeof(int))
      { return Convert.ToInt32(Value); }
      else if (ToType == typeof(long))
      { return Convert.ToInt64(Value); }
      else if (ToType == typeof(decimal))
      { return Convert.ToDecimal(Value); }
      else if (ToType == typeof(DateTime))
      { return Convert.ToDateTime(Value); }
      else if (ToType == typeof(bool))
      { return Convert.ToBoolean(Value); }
      else
      {
        if (Value != null)
        {
          if (ToType == Value.GetType())
          { return Value; }
        }
      }
      return null;
    }
    #endregion*/

    #region Set Field Property Attibute
    public void SetField(string FieldName, object Value)
    {
      FieldInfo fi;
      if (this._o != null)
      {
        for (int i = 0; i < Fields.Length; i++)
        {
          fi = Fields[i];
          if (fi.Name == FieldName)
          {
            SetField(fi, Value);
            //fi.SetValue(this._o, Value);
            break;
          }
        }
      }
    }

    public void SetField(FieldInfo fi, object Value)
    {
      fi.SetValue(this._o, GetValue(fi.FieldType, Value));
    }

    public void SetProperty(string PropertyName, object Value)
    {
      PropertyInfo pi;
      if (this._o != null)
      {
        for (int i = 0; i < Properties.Length; i++)
        {
          pi = Properties[i];
          if (pi.Name == PropertyName) 
          {
            SetProperty(pi, Value);
            break;
          }          
        }
      }
    }

    public void SetProperty(PropertyInfo pi, object Value)
    {
      if (pi.CanWrite)
      { pi.SetValue(this._o, GetValue(pi.PropertyType, Value), null); }
    }

    public void SetAttibute(string AttibuteName, object Value) 
    {
      int Idx;
      if ((Idx = IndexField(AttibuteName)) != -1)
      { SetField(Fields[Idx], Value); }

      if ((Idx = IndexProperty(AttibuteName)) != -1)
      { SetProperty(Properties[Idx], Value); }
    }
    #endregion

    #region public void CopyTo(object Target)
    public void CopyTo(object Target) 
    {
      ObjectAttribute ob_a = new ObjectAttribute(Target);

      string[] atrs = this.GetAttributes();
      for (int i = 0; i < atrs.Length; i++)
      {
        object Value = this.GetAttribute(atrs[i]);
        ob_a.SetAttibute(atrs[i], Value);        
      }
    }
    #endregion

    #region public void Clear()
    public void Clear()
    {
      for (int i = 0; i < Fields.Length; i++)
      { SetField(Fields[i], GetDefultValue(Fields[i].FieldType)); }

      for (int i = 0; i < Properties.Length; i++)
      { SetProperty(Properties[i], GetDefultValue(Properties[i].PropertyType)); }
    }
    #endregion
  }
}
