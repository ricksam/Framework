using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Class
{
  public class LockedField
  {
    public LockedField()
    {
      this.Field = "";
      this.Message = "";
    }

    public LockedField(string Field, string Message)
    {
      this.Field = Field;
      this.Message = Message;
    }

    public string Field { get; set; }
    public string Message { get; set; }
  }
}
