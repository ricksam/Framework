using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using lib.Database.Drivers;

namespace lib.Database.Query
{
  [Serializable]
  public class FieldColumn
  {
    public FieldColumn() 
    { 
    }

    public FieldColumn(DataColumn dtc)
    {
      ConfiguraField(dtc.ColumnName.ToUpper(), dtc.ColumnName.ToUpper(), getTypeColumn(dtc.DataType), 0, false);
    }
		
    public FieldColumn(string Name)
	  {
		  ConfiguraField(Name, Name, enmFieldType.String, 0, false);
	  }

    public FieldColumn(string Name, int Size)
    {
      ConfiguraField(Name, Name, enmFieldType.String, Size, false);
    }

    public FieldColumn(string Text, string Name)
    {
      ConfiguraField(Text, Name, enmFieldType.String, 0, false);
    }

    public FieldColumn(string Text, string Name, enmFieldType Type)
    {
      ConfiguraField(Text, Name, Type, 0, false);
    }

    public FieldColumn(string Text, string Name, enmFieldType Type, int Size)
    {
      ConfiguraField(Text, Name, Type, Size, false);
    }
                
    public FieldColumn(string Text, string Name, enmFieldType Type, int Size, bool Sensitive)
    {
      ConfiguraField(Text, Name, Type, Size, Sensitive); 
    }

    public void ConfiguraField(string Text, string Name, enmFieldType Type, int Size, bool Sensitive)
    {
      this.Text = Text;
      this.Name = Name;
      this.Type = Type;
      this.Size = Size;
      this.Sensitive = Sensitive;
    }

    public void Assign(FieldColumn f) 
    {
      this.Text = f.Text;
      this.Name = f.Name;
      this.Type = f.Type;
      this.Size = f.Size;
      this.Sensitive = f.Sensitive;
    }

    public string Text { get; set; }
    public string Name { get; set; }
    public enmFieldType Type { get; set; }
    public int Size { get; set; }
    public bool Sensitive { get; set; }

    #region private enmFieldType getTypeColumn(Type t)
    private enmFieldType getTypeColumn(Type t)
    {
      if (t == typeof(int)) { return enmFieldType.Int; }
      else if (t == typeof(DateTime)) { return enmFieldType.DateTime; }
      else if (t == typeof(bool)) { return enmFieldType.Bool; }
      else if (t == typeof(decimal)) { return enmFieldType.Decimal; }
      else { return enmFieldType.String; }
    }
    #endregion
  }
}
