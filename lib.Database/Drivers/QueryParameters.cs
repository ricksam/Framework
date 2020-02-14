using System;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Drivers
{
  public class QueryParameters
  {
    #region Constructor
    public QueryParameters(DatabaseUtils dbu) 
    {
      this.dbu = dbu;
      this.cnv = new Conversion();
      this.Params = new List<string>();
    }
    #endregion

    #region Fields
    private DatabaseUtils dbu { get; set; }
    private Conversion cnv { get; set; }
    private List<string> Params { get; set; }
    #endregion

    #region public void Add(int Count)
    public void setLength(int Length)
    {
      for (int i = 0; i < Length; i++)
      { AddCommand("null"); }
    }
    #endregion

    #region public void Add(... Param)
    public void AddCommand(string p)
    { this.Params.Add(p); }

    public void Add(string p)
    { this.Alter(Params.Count, p, enmFieldType.String); }

    public void Add(bool p)
    { this.Alter(Params.Count, p, enmFieldType.Bool); }

    public void Add(int p)
    { this.Alter(Params.Count, p, enmFieldType.Int); }

    public void Add(decimal p)
    { this.Params.Add(cnv.ToDecimal(p).ToString(this.dbu.dbFormat.Decimal).Replace(",", ".")); }
    #endregion

    #region public void Add(object Param, enmFieldType FieldType)
    public void Add(object p, enmFieldType FieldType)
    { Alter(Params.Count, p, FieldType); }
    #endregion

    #region public void Alter(int Index, object Param, enmFieldType FieldType)
    public void Alter(int Index, object p, enmFieldType FieldType)
    {
      while (Index > (Params.Count - 1))
      { AddCommand("null"); }

      if (p == null)
      { return; }

      switch (FieldType)
      {
        case enmFieldType.Int:
          {
            this.Params[Index] = p.ToString();
            break;
          }
        case enmFieldType.Decimal:
          {
            this.Params[Index] = cnv.ToDecimal(p).ToString(this.dbu.dbFormat.Decimal).Replace(",", ".");
            break;
          }
        case enmFieldType.Bool:
          {
            this.Params[Index] = cnv.ToBool(p) ? "1" : "0";
            break;
          }
        case enmFieldType.String:
          {
            this.Params[Index] = dbu.Quoted(p.ToString());
            break;
          }
        case enmFieldType.DateTime:
          {
            string pValue = cnv.ToDateTime(p).ToString(this.dbu.dbFormat.DateTime);
            this.Params[Index] = this.dbu.Quoted(pValue);
            break;
          }
        case enmFieldType.Date:
          {
            string pValue = cnv.ToDateTime(p).ToString(this.dbu.dbFormat.Date);
            this.Params[Index] = this.dbu.Quoted(pValue);
            break;
          }
        case enmFieldType.Time:
          {
            string pValue = cnv.ToDateTime(p).ToString(this.dbu.dbFormat.Time);
            this.Params[Index] = this.dbu.Quoted(pValue);
            break;
          }
        default:
          this.Params[Index] = p.ToString();
          break;
      }
    }
    #endregion

    #region public string[] Get()
    public string[] Get() 
    {
      return Params.ToArray();
    }
    #endregion

    #region public bool HasParameters()
    public bool HasParameters
    { get { return Params.Count != 0; } }
    #endregion

    #region public void Clear()
    public void Clear()
    { this.Params.Clear(); }
    #endregion
  }
}
