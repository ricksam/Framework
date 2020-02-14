using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace lib.Entity
{
  public class DefaultEntity
  {
    #region Constructor
    public DefaultEntity()
    {
      foThis = new lib.Class.Reflection(this);
      this.Clear();
    }
    #endregion

    #region Fields
    private lib.Class.Reflection foThis { get; set; }
    #endregion

    #region public void Assign(object o)
    public void Assign(object o)
    {
      lib.Class.Reflection foParam = new lib.Class.Reflection(o);

      string[] flds = foParam.GetFields();
      for (int i = 0; i < flds.Length; i++)
      { foThis.SetField(flds[i], foParam.GetFieldValue(flds[i])); }

      string[] prps = foParam.GetProperties();
      for (int i = 0; i < prps.Length; i++)
      { foThis.SetProperty(prps[i], foParam.GetPropertyValue(prps[i])); }
    }
    #endregion

    #region public void Clear()
    public void Clear()
    {
      foThis.Clear();      
    }
    #endregion
  }
}
