using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public class LockedField
  {
    public LockedField(string FieldName, string Reason) 
    {
      this.FieldName=FieldName;
      this.Reason = Reason;
    }

    public string FieldName { get; set; }
    public string Reason { get; set; }
  }
}
