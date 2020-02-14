using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace lib.Class
{
  public class JSON
  {
    public string Serialize(object o)
    {
      System.Text.StringBuilder txt = new System.Text.StringBuilder();
      JavaScriptSerializer jav = new JavaScriptSerializer();
      jav.Serialize(o, txt);
      return txt.ToString();
    }

    public T Deserialize<T>(string s)
    {
      JavaScriptSerializer jav = new JavaScriptSerializer();
      return jav.Deserialize<T>(s);
    }

    public string Indent(string s)
    {
      string r = "";
      int l = 0;
      
      for (int i = 0; i < s.Length; i++)
      {
        if (s[i] == ',')
        { r += s[i] + "\n".PadRight(l); }
        else if (s[i] == '{' || s[i] == '[')
        {
          l += 2;
          r += s[i] + "\n".PadRight(l);
        }
        else if (s[i] == '}' || s[i] == ']')
        {
          l -= 2;
          r += "\n".PadRight(l) + s[i];
        }
        else
        { r += s[i].ToString(); }
      }

      return r;
    }

    public void AdjustTimeZone(object object_class)
    {
      lib.Class.ObjectAttribute obj = new ObjectAttribute(object_class);
      string[] atrbs = obj.GetAttributes();
      for (int i = 0; i < atrbs.Length; i++)
      {
        object val = obj.GetAttribute(atrbs[i]);
        if (val is DateTime)
        { obj.SetAttibute(atrbs[i], Convert.ToDateTime(val).AddHours(lib.Class.Utils.TimeZone)); }
      }
    }
  }
}