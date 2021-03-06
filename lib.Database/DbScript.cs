﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using lib.Database.Drivers;
using System.Linq;

namespace lib.Database
{
    /// <summary>
    /// Classe utilizada para gerar meta data do banco de dados
    /// </summary>
    public class DbScript
    {
        #region public DbScript(Connection cnn)
        public DbScript(Connection cnn, bool UseSquareBrackets)
        {
            this.cnn = cnn;
            this.sb = new SqlBuild(cnn.dbu, UseSquareBrackets);
            this.ForeignKeys = cnn.GetForeignKeys();
            this.DefaultBigText = "TEXT";

            this.DbScriptTypeList = new DbScriptTypeList();

        }
        #endregion


        public delegate void ScriptWriting_Handle(int Index, int Count);

        #region Fields
        Connection cnn { get; set; }
        SqlBuild sb { get; set; }
        private ForeignKey[] ForeignKeys { get; set; }
        public string DefaultBigText { get; set; }
        public DbScriptTypeList DbScriptTypeList { get; set; }
        public ScriptWriting_Handle OnScriptWriting { get; set; }
        public const string COMMIT_COMMAND = "/*COMMIT*/";
        #endregion

        #region private string GetDbTypeField(DataTable tb, int ColumnIndex)
        private string GetDbTypeField(DataTable tb, int ColumnIndex)
        {
            string type = "";

            if (tb.Columns[ColumnIndex].DataType == typeof(string))
            {
                if (tb.Columns[ColumnIndex].MaxLength < 10)
                { type = string.Format("CHAR({0})", tb.Columns[ColumnIndex].MaxLength); }
                else if (tb.Columns[ColumnIndex].MaxLength >= 65535)
                { type = this.DefaultBigText; }
                else
                { type = string.Format("VARCHAR({0})", tb.Columns[ColumnIndex].MaxLength); }
            }

            else
            { type = DbScriptTypeList.GetDbScriptType(tb.Columns[ColumnIndex].DataType); }

            string s_NotNull = tb.Columns[ColumnIndex].AllowDBNull ? "" : " NOT NULL";
            string PrimaryKey = IsPrimaryKey(tb.PrimaryKey, tb.Columns[ColumnIndex].ColumnName) ? " primary key" : "";
            string AutoIncrement = tb.Columns[ColumnIndex].AutoIncrement ? " " + cnn.dbu.CmdAutoIncrement.ToUpper() : "";

            return type + s_NotNull + PrimaryKey + AutoIncrement;
        }

        private DbColumn GetDbColumn(DbColumn[] columns, string TABLE_NAME, string COLUMN_NAME)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].TABLE_NAME.ToUpper() == TABLE_NAME.ToUpper() && columns[i].COLUMN_NAME.ToUpper() == COLUMN_NAME.ToUpper())
                { return columns[i]; }
            }
            return null;
        }

        private DbDataType GetDbDataType(DbDataType[] types, string TypeName)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (TypeName.ToUpper() == types[i].TypeName.ToUpper())
                { return types[i]; }
            }
            return null;
        }

        private bool IsPrimaryKey(DataColumn[] PrimaryKeys, string ColumnName)
        {
            foreach (var item in PrimaryKeys)
            {
                if (item.ColumnName.ToUpper() == ColumnName.ToUpper())
                { return true; }
            }
            return false;
        }

        private string GetDbTypeField(DbDataType[] types, DbColumn[] columns, DataTable tb, int ColumnIndex)
        {
            string type = "";

            for (int i = 0; i < types.Length; i++)
            {
                DbColumn dbCol = GetDbColumn(columns, tb.TableName, tb.Columns[ColumnIndex].ColumnName);
                if (dbCol == null) { return GetDbTypeField(tb, ColumnIndex); }

                DbDataType dbType = GetDbDataType(types, dbCol.DATA_TYPE);
                if (dbType == null) { return GetDbTypeField(tb, ColumnIndex); }

                if (string.IsNullOrEmpty(dbType.CreateFormat))
                {
                    if (dbType.TypeName.ToUpper() == "CHAR" || dbType.TypeName.ToUpper() == "VARCHAR" ||
                      dbType.TypeName.ToUpper() == "NCHAR" || dbType.TypeName.ToUpper() == "NVARCHAR"
                      )
                    {
                        type = string.Format("{0}({1})", dbType.TypeName, dbCol.COLUMN_SIZE);
                        break;
                    }
                    else if (dbType.TypeName.ToUpper() == "NUMERIC" || dbType.TypeName.ToUpper() == "DECIMAL")
                    {
                        type = string.Format("{0}({1},{2})", dbType.TypeName, dbCol.COLUMN_SIZE, dbCol.DECIMAL_DIGITS);
                        break;
                    }
                    else
                    {
                        type = dbType.TypeName;
                        break;
                    }
                }
                else
                {
                    type = string.Format(dbType.CreateFormat, dbCol.COLUMN_SIZE, dbCol.DECIMAL_DIGITS);
                    break;
                }
            }

            if (string.IsNullOrEmpty(type))
            { type = string.Format("UNDEFINED[{0}({1})]", tb.Columns[ColumnIndex].DataType.ToString(), tb.Columns[ColumnIndex].MaxLength); }

            string s_NotNull = tb.Columns[ColumnIndex].AllowDBNull ? "" : " not null";
            string PrimaryKey = IsPrimaryKey(tb.PrimaryKey, tb.Columns[ColumnIndex].ColumnName) ? " primary key" : "";
            string AutoIncrement = tb.Columns[ColumnIndex].AutoIncrement ? " " + cnn.dbu.CmdAutoIncrement : "";

            return type + s_NotNull + PrimaryKey + AutoIncrement;
        }
        #endregion

        #region private ForeignKey[] GetForeignKeyThisTable(string TableName)
        private ForeignKey[] GetForeignKeyThisTable(string TableName)
        {
            List<ForeignKey> list = new List<ForeignKey>();
            for (int i = 0; i < ForeignKeys.Length; i++)
            {
                if (ForeignKeys[i].TableName.ToUpper() == TableName.ToUpper())
                { list.Add(ForeignKeys[i]); }
            }
            return list.ToArray();
        }
        #endregion

        #region private int GetIdxTable(List<string> Tables, string Table)
        private int GetIdxTable(List<string> Tables, string Table)
        {
            for (int i = 0; i < Tables.Count; i++)
            {
                if (Table.ToUpper() == Tables[i].ToString().ToUpper())
                { return i; }
            }
            return -1;
        }
        #endregion

        #region private bool ListIsEquals(string[] List1, string[] List2)
        private bool ListIsEquals(List<string> List1, List<string> List2)
        {
            if (List1.Count != List2.Count)
            { return false; }

            for (int i = 0; i < List1.Count; i++)
            {
                if (List1[i] != List2[i])
                { return false; }
            }
            return true;
        }
        #endregion

        #region private string[] GetTablesInOrder()
        public string[] GetTablesInOrder(int MaxIteration)
        {
            List<string> OldList = new List<string>();
            List<string> Tables = new List<string>();
            Tables.AddRange(cnn.GetTables(false));

            int Count = 0;
            while (!ListIsEquals(Tables, OldList))
            {
                Count++;
                OldList.Clear();
                OldList.AddRange(Tables);

                for (int i = 0; i < ForeignKeys.Length; i++)
                {
                    int idxTable = GetIdxTable(Tables, ForeignKeys[i].TableName);
                    int idxReference = GetIdxTable(Tables, ForeignKeys[i].TableReference);

                    if (idxTable == -1 || idxReference == -1)
                    { continue; }

                    if (idxReference > idxTable)
                    {
                        Tables.RemoveAt(idxReference);
                        Tables.Insert(idxTable, ForeignKeys[i].TableReference);
                    }
                }

                if (Count >= MaxIteration)
                { throw new Exception("Erro em referencia circular"); }
            }

            return Tables.ToArray();
        }
        #endregion

        #region public string GetMetadataTable(DataTable tb)
        public DbDataType[] GetDbDataTypes()
        {
            lib.Class.Conversion cnv = new Conversion();
            DataTable dt = cnn.GetSchema("DataTypes");
            List<DbDataType> types = new List<DbDataType>();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DbDataType t = new DbDataType();
                    t.TypeName = dt.Rows[i]["TypeName"].ToString();
                    t.ColumnSize = cnv.ToLong(dt.Rows[i]["ColumnSize"].ToString());
                    t.CreateFormat = dt.Rows[i]["CreateFormat"].ToString();
                    t.DataType = dt.Rows[i]["DataType"].ToString();
                    types.Add(t);
                }
            }

            return types.OrderBy(c => c.ColumnSize).ToArray();
        }

        public string GetMetadataTable(DbColumn[] columns, DataTable tb, bool UseSquareBrackets)
        {
            if (tb == null)
            { return ""; }

            string fields = "";
            string pk = "";
            string fk = "";
            ForeignKey[] fks = GetForeignKeyThisTable(tb.TableName);

            DbDataType[] types = GetDbDataTypes();

            // Fields
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                string virg = (i == tb.Columns.Count - 1 ? "" : ", ");
                if (UseSquareBrackets)
                {
                    fields += string.Format("  [{0}]        {1}{2}\r\n", tb.Columns[i].ColumnName, GetDbTypeField(types, columns, tb, i), virg);
                }
                else
                {
                    fields += string.Format("  {0}        {1}{2}\r\n", tb.Columns[i].ColumnName, GetDbTypeField(types, columns, tb, i), virg);
                }
            }

            // Foreign keys
            for (int i = 0; i < fks.Length; i++)
            {
                string virg = (i == (fks.Length - 1) ? "" : ", ");
                fk += "  " + fks[i].getPlainScript() + virg + " \r\n";
            }

            return string.Format("create table {0} (\r\n{1}{2}{3})", tb.TableName, fields, pk, fk);
        }
        #endregion

        #region private bool ExistsInList(string[] list, string Value)
        private bool ExistsInList(string[] list, string Value)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (Value == list[i])
                { return true; }
            }
            return false;
        }
        #endregion

        #region private void AtribuiCampos(int Row, DataSource ds, string [] Exceptions)
        private void AtribuiCampos(int Row, DataSource ds, string[] Exceptions, bool UpdateMode)
        {
            #region Campos
            for (int j = 0; j < ds.Columns.Count; j++)
            {
                string col_name = ds.Columns[j].ColumnName;
                if (ExistsInList(Exceptions, col_name))
                { continue; }

                //enmFieldType ft = cnn.dbu.GetFieldType(ds.Columns[j].DataType);
                enmFieldType ft = DbScriptTypeList.GetFieldType(ds.Columns[j].DataType);
                //Não atribui campos quando são valores default 
                //para não dar problemas com chaves estrangeiras
                if (ds.GetField(Row, col_name) == null) 
                {
                    sb.AddField(col_name, null);
                    continue;
                }

                // Precisa corrigir esta linha , está travada para colocar todos os campos no insert
                UpdateMode = true;

                switch (ft)
                {
                    case enmFieldType.Int:
                        {
                            int Val = ds.GetField(Row, col_name).ToInt();
                            if (Val != 0 || UpdateMode)
                            { sb.AddField(col_name, Val); }
                            break;
                        }
                    case enmFieldType.Long:
                        {
                            long Val = ds.GetField(Row, col_name).ToLong();
                            if (Val != 0 || UpdateMode)
                            { sb.AddField(col_name, Val); }
                            break;
                        }
                    case enmFieldType.Decimal:
                        {
                            decimal Val = ds.GetField(Row, col_name).ToDecimal();
                            if (Val != 0 || UpdateMode)
                            { sb.AddField(col_name, Val); }
                            break;
                        }
                    case enmFieldType.DateTime:
                        {
                            DateTime val = ds.GetField(Row, col_name).ToDateTime();

                            if (val == DateTime.MinValue)
                            {
                                if (UpdateMode)
                                { sb.AddField(col_name, null); }

                                break;
                            }

                            bool IsDate = val.ToString("dd/MM/yyyy") != DateTime.MinValue.ToString("dd/MM/yyyy");
                            bool IsTime = val.ToString("HH:mm:ss") != "00:00:00";
                            if (IsDate && IsTime)
                            { sb.AddField(col_name, val, enmFieldType.DateTime); break; }
                            else if (IsDate && !IsTime)
                            { sb.AddField(col_name, val, enmFieldType.Date); break; }
                            else if (!IsDate && IsTime)
                            { sb.AddField(col_name, val, enmFieldType.Time); break; }
                            break;
                        }
                    case enmFieldType.Bool:
                        {
                            bool Val = ds.GetField(Row, col_name).ToBool();
                            if (Val != false || UpdateMode)
                            { sb.AddField(col_name, Val); }
                            break;
                        }
                    default:
                        {
                            if (!string.IsNullOrEmpty(col_name) || UpdateMode)
                            {
                                string Val = ds.GetField(Row, col_name).ToString();
                                if (!string.IsNullOrEmpty(Val))
                                { Val = Val.Trim(); }
                                sb.AddField(col_name, Val);
                            }
                            break;
                        }
                }
            }
            #endregion
        }
        #endregion

        #region public void SalveScriptInsert(DataTable tb, string FileName)
        public void SalveScriptInsert(string TableName, string FileName, bool UseInsertIdentity, bool UseSquareBrackets)
        {
            SalveScriptInsert(cnn.GetDataTable("select * from " + TableName, TableName, 0, false), FileName, UseInsertIdentity, UseSquareBrackets);
        }

        public void SalveScriptInsert(string TableName, TextFile TextFile, bool UseInsertIdentity, bool UseSquareBrackets)
        {
            SalveScriptInsert(cnn.GetDataTable("select * from " + TableName, TableName, 0, false), TextFile, UseInsertIdentity, UseSquareBrackets);
        }

        public void SalveScriptInsert(DataTable tb, string FileName, bool UseInsertIdentity, bool UseSquareBrackets)
        {
            TextFile tf = new TextFile();
            tf.Open(enmOpenMode.Writing, FileName);
            SalveScriptInsert(tb, tf, UseInsertIdentity, UseSquareBrackets);
            tf.Close();
        }

        public void SalveScriptInsert(DataTable tb, TextFile TextFile, bool UseInsertIdentity, bool UseSquareBrackets)
        {
            if (tb == null)
            { return; }

            DataSource ds = new DataSource(tb);
            bool TableHasPrimaryKey = tb.PrimaryKey.Length != 0;
            for (int i = 0; i < ds.Count; i++)
            {
                if (OnScriptWriting != null)
                { OnScriptWriting(i, ds.Count); }

                sb.Clear();
                sb.Table = tb.TableName;
                AtribuiCampos(i, ds, new string[] { }, false);

                string table = sb.Table;
                if (UseSquareBrackets)
                { table = string.Format("[{0}]", sb.Table); }

                TextFile.WriteLine((UseInsertIdentity && TableHasPrimaryKey ? string.Format("SET IDENTITY_INSERT {0} ON ", table) : "") + sb.getInsert() + ";");

                if (i != 0 && (i % 10000) == 0)
                { TextFile.WriteLine(COMMIT_COMMAND); }
            }

            if (ds.Count != 0)
            { TextFile.WriteLine(COMMIT_COMMAND); }
        }
        #endregion

        #region private Type GetDataType(string ColumnName, DataSource ds)
        private Type GetDataType(string ColumnName, DataSource ds)
        {
            for (int i = 0; i < ds.Columns.Count; i++)
            {
                if (ds.Columns[i].ColumnName == ColumnName)
                { return ds.Columns[i].DataType; }
            }
            return typeof(string);
        }
        #endregion

        #region public void SalveScriptUpdate(DataTable tb, string FileName, string[] ColumnsConditions)
        //Criar mais versões desta função
        public void SalveScriptUpdate(DataTable tb, string FileName, string[] ColumnsConditions)
        {
            TextFile tf = new TextFile();
            tf.Open(enmOpenMode.Writing, FileName);

            DataSource ds = new DataSource(tb);
            for (int i = 0; i < ds.Count; i++)
            {
                if (OnScriptWriting != null)
                { OnScriptWriting(i, ds.Count); }

                string Condition = "";
                #region Gera condição
                if (ColumnsConditions.Length != 0)
                {
                    Condition = "WHERE ";
                    for (int j = 0; j < ColumnsConditions.Length; j++)
                    {
                        string col_name = ColumnsConditions[j];
                        enmFieldType ft = cnn.dbu.GetFieldType(GetDataType(col_name, ds));
                        switch (ft)
                        {
                            case enmFieldType.Int:
                                {
                                    Condition += col_name + " = " + ds.GetField(i, col_name).ToString();
                                    break;
                                }
                            case enmFieldType.Decimal:
                                {
                                    Condition += col_name + " = " + ds.GetField(i, col_name).ToDecimal().ToString(cnn.dbu.dbFormat.Decimal).Replace(",", ".");
                                    break;
                                }
                            case enmFieldType.DateTime:
                                {
                                    DateTime val = ds.GetField(i, col_name).ToDateTime();
                                    bool IsDate = val.ToString("dd/MM/yyyy") != DateTime.MinValue.ToString("dd/MM/yyyy");
                                    bool IsTime = val.ToString("HH:mm:ss") != "00:00:00";
                                    string DtFormat = cnn.dbu.dbFormat.DateTime;
                                    if (IsDate && !IsTime) { DtFormat = cnn.dbu.dbFormat.Date; }
                                    if (!IsDate && IsTime) { DtFormat = cnn.dbu.dbFormat.Date; }
                                    Condition += col_name + " = " + ds.GetField(i, col_name).ToString(DtFormat);
                                    break;
                                }
                            case enmFieldType.Bool:
                                {
                                    Condition += col_name + " = " + (ds.GetField(i, col_name).ToBool() ? "1" : "0");
                                    break;
                                }
                            default:
                                {
                                    Condition += col_name + " = " + cnn.dbu.Quoted(ds.GetField(i, col_name).ToString());
                                    break;
                                }
                        }

                        if (j != (ColumnsConditions.Length - 1))
                        { Condition += " AND "; }
                    }
                }
                #endregion

                sb.Clear();
                sb.Table = tb.TableName;
                AtribuiCampos(i, ds, ColumnsConditions, true);
                tf.WriteLine(sb.getUpdate(Condition) + ";");
            }

            tf.Close();
        }
        #endregion

        #region public void SalveScriptDelete(string TableName, string FileName)
        public void SalveScriptDelete(string TableName, string FileName)
        {
            TextFile tf = new TextFile();
            tf.Open(enmOpenMode.Writing, FileName);
            SalveScriptDelete(TableName, tf);
            tf.Close();
        }
        public void SalveScriptDelete(string TableName, TextFile TextFile)
        {
            TextFile.WriteLine("delete from " + TableName + ";");
        }
        #endregion
    }

    #region public class DbScriptTypeList
    [Serializable]
    public class DbScriptTypeList
    {
        public DbScriptTypeList()
        {
            this.Items = new List<DbScriptType>(); // DbScriptTypeList.CreateItems();
        }

        public DbScriptType this[int index]
        {
            get { return this.Items[index]; }
            set { this.Items[index] = value; }
        }

        #region public int Count
        public int Count { get { return this.Items.Count; } }
        #endregion

        #region public List<DbScriptType> Items
        public List<DbScriptType> Items { get; private set; }
        #endregion

        #region public void Clear()
        public void Clear()
        { this.Items.Clear(); }
        #endregion

        #region public void Add(DbScriptType dbt)
        public void Add(DbScriptType dbt)
        { this.Items.Add(dbt); }
        #endregion

        #region public void AddRange(List<DbScriptType> Items)
        public void AddRange(List<DbScriptType> lst)
        { this.Items.AddRange(lst); }
        #endregion

        #region public void RemoveAt(int index)
        public void RemoveAt(int index)
        { this.RemoveAt(index); }
        #endregion

        #region public string GetDbScriptType(Type t)
        public string GetDbScriptType(Type t)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].SystemType == t)
                { return Items[i].DatabaseType.ToUpper(); }
            }

            return string.Format("UNDEFINED({0})", t.ToString());
        }
        #endregion

        #region public enmFieldType GetFieldType_FromDatabaseType(string DatabaseType)
        public enmFieldType GetFieldType_FromDatabaseType(string DatabaseType)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].DatabaseType == DatabaseType)
                { return Items[i].FieldType; }
            }

            return enmFieldType.String;
        }
        #endregion

        #region public string GetFieldType(Type t)
        public enmFieldType GetFieldType(Type t)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].SystemType == t)
                { return GetFieldType_FromDatabaseType(Items[i].DatabaseType); }
            }

            return enmFieldType.String;
        }
        #endregion

        #region public static List<DbScriptType> CreateItems()
        public static List<DbScriptType> CreateItems()
        {
            List<DbScriptType> lst = new List<DbScriptType>();
            lst.Add(new DbScriptType(typeof(bool), "BIT", enmFieldType.Bool));
            lst.Add(new DbScriptType(typeof(Int16), "SMALLINT", enmFieldType.Int));
            lst.Add(new DbScriptType(typeof(int), "INTEGER", enmFieldType.Int));
            lst.Add(new DbScriptType(typeof(long), "BIGINT", enmFieldType.Long));
            lst.Add(new DbScriptType(typeof(DateTime), "DATETIME", enmFieldType.DateTime));
            lst.Add(new DbScriptType(typeof(TimeSpan), "TIME", enmFieldType.DateTime));
            lst.Add(new DbScriptType(typeof(decimal), "NUMERIC(18, 4)", enmFieldType.Decimal));
            lst.Add(new DbScriptType(typeof(double), "NUMERIC(18, 4)", enmFieldType.Decimal));
            lst.Add(new DbScriptType(typeof(Single), "NUMERIC(18, 4)", enmFieldType.Decimal));
            lst.Add(new DbScriptType(typeof(byte[]), "BLOB", enmFieldType.Undefined));
            return lst;
        }
        #endregion
    }
    #endregion

    public class DbDataType
    {
        public string TypeName { get; set; }
        public long ColumnSize { get; set; }
        public string DataType { get; set; }
        public string CreateFormat { get; set; }
    }

    public class DbColumn
    {
        public string TABLE_NAME = "";
        public string COLUMN_NAME = "";
        public string DATA_TYPE = "";
        public int COLUMN_SIZE = 0;
        public int DECIMAL_DIGITS = 0;
    }

    #region public class DbScriptType
    [Serializable]
    public class DbScriptType
    {
        public DbScriptType()
        {
            this.SystemType = typeof(string);
            this.DatabaseType = "VARCHAR(60)";
            this.FieldType = enmFieldType.String;
        }

        public DbScriptType(Type SystemType, string DatabaseType, enmFieldType FieldType)
        {
            this.SystemType = SystemType;
            this.DatabaseType = DatabaseType;
            this.FieldType = FieldType;
        }

        public Type SystemType { get; set; }
        public string DatabaseType { get; set; }
        public enmFieldType FieldType { get; set; }

        public override string ToString()
        { return SystemType + " -> " + DatabaseType; }
    }
    #endregion
}
