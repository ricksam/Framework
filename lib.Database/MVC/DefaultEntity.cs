using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using lib.Database.Drivers;

namespace lib.Database.MVC
{
  public class DefaultEntity
  {    
    public DefaultEntity() 
    {
      foThis = new ObjectAttribute(this);
      this.Clear();
    }
    
    private ObjectAttribute foThis { get; set; }
    
    public void Assign(object o)
    {      
      ObjectAttribute foParam = new ObjectAttribute(o);

      string[] flds = foParam.GetFields();
      for (int i = 0; i < flds.Length; i++)
      { foThis.SetField(flds[i], foParam.GetField(flds[i])); }

      string[] prps = foParam.GetProperties();
      for (int i = 0; i < prps.Length; i++)
      { foThis.SetProperty(prps[i], foParam.GetProperty(prps[i])); }
    }

    public void Fill(object o)
    {
      ObjectAttribute foParam = new ObjectAttribute(o);

      string[] flds = foThis.GetFields();
      for (int i = 0; i < flds.Length; i++)
      { foParam.SetField(flds[i], foThis.GetField(flds[i])); }

      string[] prps = foThis.GetProperties();
      for (int i = 0; i < prps.Length; i++)
      { foParam.SetProperty(prps[i], foThis.GetProperty(prps[i])); }
    }
    
    public void Clear() 
    {
      foThis.Clear();
    }
  }
}
