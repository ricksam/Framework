using System;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using lib.Database.Drivers;

namespace lib.Database
{
    public class SqlBuild
    {
        #region Constructor
        public SqlBuild(DatabaseUtils dbu, bool UseSquareBrackets)
        {
            Table = "";
            this.dbu = dbu;
            this.UseSquareBrackets = UseSquareBrackets;
            cnv = new Conversion();
            Fields = new List<SqlField>();
        }
        #endregion

        #region Fields
        private bool UseSquareBrackets { get; set; }
        private Conversion cnv { get; set; }
        private DatabaseUtils dbu { get; set; }
        private List<SqlField> Fields { get; set; }
        public string Table { get; set; }
        #endregion

        #region Methods
        #region bool DeleteField(string FieldName)
        bool DeleteField(string FieldName)
        {
            try
            {
                for (int i = 0; i < Fields.Count; i++)
                {
                    if (FieldName == Fields[i].FieldName)
                    {
                        Fields.RemoveAt(i);
                        i--;
                    }//if (FieldName==Fields[i].FieldName)        
                }//for (int i = 0; i < Fields.Count; i++)
                return true;
            }
            catch { return false; }
        }//bool DeleteField(string FieldName)
        #endregion

        #region public void Clear()
        public void Clear()
        {
            Fields.Clear();
        }
        #endregion

        #region public void AddCommand(string FieldName, string Command)
        public void AddCommand(string FieldName, string Command)
        {
            DeleteField(FieldName);
            Fields.Add(new SqlField(FieldName, Command));
        }
        #endregion

        #region public void AddField(FieldName, FieldValue)
        public void AddField(string FieldName, string FieldValue, int Size)
        {
            if (!string.IsNullOrEmpty(FieldValue) && FieldValue.Length >= Size)
            { FieldValue = FieldValue.Substring(0, Size); }

            AddField(FieldName, FieldValue);
        }

        public void AddField(string FieldName, string FieldValue)
        {
            if (FieldValue != null)
            { AddCommand(FieldName, dbu.Quoted(FieldValue)); }
            else
            { AddCommand(FieldName, "null"); }
        }

        public void AddField(string FieldName, bool FieldValue)
        { AddCommand(FieldName, cnv.ToBool(FieldValue) ? "1" : "0"); }

        public void AddField(string FieldName, int FieldValue)
        { AddCommand(FieldName, cnv.ToInt(FieldValue).ToString()); }

        public void AddField(string FieldName, long FieldValue)
        { AddCommand(FieldName, cnv.ToLong(FieldValue).ToString()); }

        public void AddField(string FieldName, char FieldValue)
        { AddCommand(FieldName, dbu.Quoted(cnv.ToChar(FieldValue).ToString())); }

        public void AddField(string FieldName, decimal FieldValue)
        { AddCommand(FieldName, FieldValue.ToString(dbu.dbFormat.Decimal).Replace(",", ".")); }

        public void AddField(string FieldName, DateTime FieldValue, enmFieldType fDt)
        {
            switch (fDt)
            {
                case enmFieldType.DateTime: { AddCommand(FieldName, dbu.Quoted(FieldValue.ToString(dbu.dbFormat.DateTime))); break; }
                case enmFieldType.Date: { AddCommand(FieldName, dbu.Quoted(FieldValue.ToString(dbu.dbFormat.Date))); break; }
                case enmFieldType.Time: { AddCommand(FieldName, dbu.Quoted(FieldValue.ToString(dbu.dbFormat.Time))); break; }
                default: { AddCommand(FieldName, FieldValue.ToString()); break; }
            }
        }
        #endregion

        #region public string getInsert()
        public string getInsert()
        {
            try
            {
                string Ret = "";

                if (UseSquareBrackets)
                {
                    Ret = "INSERT INTO " + "[" + Table + "]" + " (";
                }
                else
                {
                    Ret = "INSERT INTO " + Table + " (";
                }

                for (int i = 0; i < Fields.Count; i++)
                {
                    if (UseSquareBrackets)
                    {
                        Ret += "[" + Fields[i].FieldName + "]" + ",";
                    }
                    else
                    {
                        Ret += Fields[i].FieldName + ",";
                    }
                }
                Ret = Ret.Remove(Ret.Length - 1, 1) + ") VALUES (";
                for (int i = 0; i < Fields.Count; i++)
                { Ret += Fields[i].FieldValue + ","; }
                return Ret.Remove(Ret.Length - 1, 1) + ")";
            }
            catch { return ""; }
        }
        #endregion

        #region public string getUpdate(string Condition)
        public string getUpdate(string Condition)
        {
            try
            {
                string Ret = "";

                if (UseSquareBrackets)
                {
                    Ret = "UPDATE " + "[" + Table + "]" + " SET ";
                }
                else
                {
                    Ret = "UPDATE " + Table + " SET ";
                }

                for (int i = 0; i < Fields.Count; i++)
                {
                    if (UseSquareBrackets)
                    {
                        Ret += "[" + Fields[i].FieldName + "]" + " = " + Fields[i].FieldValue + ",";
                    }
                    else
                    {
                        Ret += Fields[i].FieldName + " = " + Fields[i].FieldValue + ",";
                    }
                }
                return Ret.Remove(Ret.Length - 1, 1) + " " + Condition;
            }
            catch { return ""; }
        }
        #endregion

        #region public string getSelect()
        public string getSelect()
        {
            try
            {
                string Ret = "SELECT ";
                for (int i = 0; i < Fields.Count; i++)
                {
                    if (UseSquareBrackets)
                    {
                        Ret += "[" + Fields[i].FieldName + "]" + ",";
                    }
                    else
                    {
                        Ret += Fields[i].FieldName + ",";
                    }
                }
                Ret = Ret.Remove(Ret.Length - 1, 1);

                if (UseSquareBrackets)
                {
                    Ret += " FROM " + "[" + Table + "]";
                }
                else
                {
                    Ret += " FROM " + Table;
                }
                return Ret;
            }
            catch
            { return ""; }
        }
        #endregion

        #region public string getSelect(string Condition)
        public string getSelect(string Condition)
        {
            try
            {
                string Ret = "SELECT ";

                for (int i = 0; i < Fields.Count; i++)
                {
                    if (UseSquareBrackets)
                    {
                        Ret += "[" + Fields[i].FieldName + "]" + ",";
                    }
                    else
                    {
                        Ret += Fields[i].FieldName + ",";
                    }
                }
                Ret = Ret.Remove(Ret.Length - 1, 1);

                if (UseSquareBrackets)
                {
                    Ret += " FROM " + "[" + Table + "]" + " " + Condition;
                }
                else
                {
                    Ret += " FROM " + Table + " " + Condition;
                }

                return Ret;
            }
            catch
            { return ""; }
        }
        #endregion

        #region public string getDelete(string Condition)
        public string getDelete(string Condition)
        {
            try
            {
                if (UseSquareBrackets)
                {
                    return "DELETE FROM " + "[" + Table + "]" + " " + Condition;
                }
                else
                {
                    return "DELETE FROM " + Table + " " + Condition;
                }
            }
            catch { return ""; }
        }
        #endregion
        #endregion
    }

    #region class SqlField
    class SqlField
    {
        public SqlField(string FieldName, string FieldValue)
        {
            this.FieldName = FieldName;
            this.FieldValue = FieldValue;
        }

        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }
    #endregion
}
