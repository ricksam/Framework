using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database.Drivers
{
    public class DataSource
    {
        #region Constructors
        public DataSource(DataSet DataSet)
        {
            if (DataSet.Tables.Count != 0)
            { SetDataTable(DataSet.Tables[0]); }
            else
            { SetDataTable(new DataTable()); }
        }

        public DataSource(DataTable DataTable)
        { SetDataTable(DataTable); }
        #endregion

        #region Fields
        private DataTable DataTable { get; set; }
        public int Row { get; set; }
        public int Count { get { return this.DataTable.Rows.Count; } }
        public DataColumnCollection Columns { get { return DataTable.Columns; } }
        #endregion

        #region Methods
        #region private void SetDataTable(DataTable DataTable)
        private void SetDataTable(DataTable DataTable)
        {
            this.Row = (DataTable.Rows.Count == 0 ? -1 : 0);
            this.DataTable = DataTable;
            this.First();
        }
        #endregion

        #region private bool CanNext()
        private bool CanNext()
        { return this.Row < Count; }
        #endregion

        #region private bool CanBack()
        private bool CanBack()
        {
            return this.Row >= 0;
        }
        #endregion

        #region public void First()
        public void First()
        { this.Row = 0; }
        #endregion

        #region public void Last()
        public void Last()
        { this.Row = Count - 1; }
        #endregion

        #region public bool Eof()
        public bool Eof()
        { return this.Row >= Count; }
        #endregion

        #region public bool Bof()
        public bool Bof()
        { return this.Row < 0; }
        #endregion

        #region public void Next()
        public void Next()
        {
            if (CanNext())
            { this.Row += 1; }
        }
        #endregion

        #region public void Back()
        public void Back()
        {
            if (CanBack())
            { this.Row -= 1; }
        }
        #endregion

        #region public bool FieldExists(string Field)
        public bool FieldExists(string Field)
        {
            for (int i = 0; i < this.DataTable.Columns.Count; i++)
            {
                if (this.DataTable.Columns[i].ColumnName == Field)
                { return true; }
            }
            return false;
        }
        #endregion

        #region public Conversion GetField(string Field[int Index, int Row])
        public Conversion GetField(string Field)
        { return GetField(this.Row, Field); }

        public Conversion GetField(int Index)
        { return GetField(this.Row, Index); }

        public Conversion GetField(int Row, string Field)
        {
            this.Row = Row;

            if (this.DataTable.Rows[Row][Field] == null || this.DataTable.Rows[Row][Field] == DBNull.Value)
            { return null; }

            if (Row < this.DataTable.Rows.Count)
            { return new Conversion(this.DataTable.Rows[Row][Field].ToString()); }
            else
            { return new Conversion(); }
        }

        public Conversion GetField(int Row, int Index)
        {
            this.Row = Row;
            if (Row < this.DataTable.Rows.Count)
            {
                if (Index < this.DataTable.Rows[Row].ItemArray.Length)
                { return new Conversion(this.DataTable.Rows[Row][Index].ToString()); }
                else
                { return new Conversion(); }
            }
            else
            { return new Conversion(); }
        }
        #endregion
        #endregion

        #region public void FillObject(int Row, object Obj)
        public void FillObject(object Obj)
        { FillObject(0, Obj); }

        public void FillObject(int Row, object Obj)
        {
            ObjectAttribute oa = new ObjectAttribute(Obj);

            for (int i = 0; i < this.DataTable.Columns.Count; i++)
            {
                string ColName = this.DataTable.Columns[i].ColumnName;
                oa.SetAttibute(ColName, this.DataTable.Rows[Row][ColName].ToString());
            }
        }
        #endregion
    }
}
