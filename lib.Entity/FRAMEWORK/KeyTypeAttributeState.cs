using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public struct KeyTypeAttributeState
  {
    public bool IsPrimaryKey;
    public bool IsForeignKey;
    public bool IsQueryField;
    public bool IsOmitIfIsNull;
  }
}
