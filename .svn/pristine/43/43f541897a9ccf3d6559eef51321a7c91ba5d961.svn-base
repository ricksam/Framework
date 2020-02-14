using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public enum KeyTypeAttribute { PrimaryKey, ForeignKey, QueryField }

  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public class CustomAttributeField : System.Attribute
  {
    public CustomAttributeField(KeyTypeAttribute KeyType) 
    {
      this.KeyType = KeyType;
    }

    public KeyTypeAttribute KeyType { get; set; }
  }
}
