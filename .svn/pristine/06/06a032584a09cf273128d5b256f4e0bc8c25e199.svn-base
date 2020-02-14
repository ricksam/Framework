using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using lib.Database.Query;
using lib.Database.Drivers;
using System.ComponentModel;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknGrid))]
  public class sknGrid:DataGridView
  {
    #region Constructor
    public sknGrid() 
    {
      Fields = new List<FieldColumn>();
      this.AutoGenerateColumns = false;
    }
    #endregion

    #region protected override void CreateHandle()
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode)
        {
          SetPropriedades();
          if (Resources.Skin.Enabled)
          {
            this.Font = Resources.Skin.Controls.Font;
            this.BackgroundColor = Resources.Skin.Controls.GridBackColor;
            this.ForeColor = Resources.Skin.Controls.ForeColor;
            this.borderDrawer.BorderColor = Resources.Skin.Controls.BorderColor;
          }//if (Resources.Skin.Enabled)
        }//if (!this.DesignMode)        
      }
      catch { }
      base.CreateHandle();
    }
    #endregion
    
    private BorderDrawer borderDrawer = new BorderDrawer();

    #region protected override void WndProc(ref Message m)
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (Resources.Skin.Enabled)
      { borderDrawer.DrawBorder(ref m, this.Width, this.Height); }
    }
    #endregion

    #region Fields
    private List<FieldColumn> Fields { get; set; }
    #endregion

    #region Methods
    #region private void SetPropriedades()
    private void SetPropriedades()
    {
      try
      {
        this.EnableHeadersVisualStyles = true;
        this.BorderStyle = BorderStyle.FixedSingle;
        this.ReadOnly = true;
        this.MultiSelect = false;
        
        this.AllowUserToResizeColumns = true;
        this.AllowUserToResizeRows = false;
        this.AllowUserToAddRows = false;
        this.AllowUserToDeleteRows = false;
        this.AllowUserToOrderColumns = false;

        this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        
        this.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        this.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
        this.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;

        //this.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        this.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        this.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
        this.ColumnHeadersHeight = 20;
        
        this.DefaultCellStyle.SelectionBackColor = Color.FromName("ActiveCaption");
        this.DefaultCellStyle.SelectionForeColor = Color.White;

        this.RowHeadersVisible = false;
        //this.RowHeadersWidth = 50;
        this.RowTemplate.Height = 20;
        this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        this.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromName("ActiveCaption");
        this.RowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 240);

        this.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromName("ActiveCaption");
        this.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        this.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

        this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      }
      catch { return; }
    }
    #endregion

    #region private string GetFormat(enmTypeColumn t)
    private string GetFormat(enmFieldType t)
    {
      switch (t)
      {
        case enmFieldType.String: return "";
        case enmFieldType.Int: return "0";
        case enmFieldType.Decimal: return "#,##0.00";
        case enmFieldType.Date: return "dd/MM/yyyy";
        case enmFieldType.Time: return "HH:mm:ss";
        case enmFieldType.DateTime: return "dd/MM/yyyy HH:mm:ss";
        default: return "";
      }
    }
    #endregion

    #region private DataGridViewContentAlignment GetAlingnment(enmTypeColumn t)
    private DataGridViewContentAlignment GetAlingnment(enmFieldType t)
    {
      switch (t)
      {
        case enmFieldType.String: return DataGridViewContentAlignment.MiddleLeft;
        case enmFieldType.Int: return DataGridViewContentAlignment.MiddleRight;
        case enmFieldType.Decimal: return DataGridViewContentAlignment.MiddleRight;
        case enmFieldType.Date: return DataGridViewContentAlignment.MiddleLeft;
        case enmFieldType.Time: return DataGridViewContentAlignment.MiddleLeft;
        case enmFieldType.DateTime: return DataGridViewContentAlignment.MiddleLeft;
        case enmFieldType.Bool: return DataGridViewContentAlignment.MiddleCenter;
        default: return DataGridViewContentAlignment.MiddleLeft;         
      }
    }
    #endregion

    #region private int GetSize(enmTypeColumn t)
    private int GetSize(enmFieldType t) 
    {
      switch (t)
      {
        case enmFieldType.String: return 240;
        case enmFieldType.Int: return 90;
        case enmFieldType.Decimal: return 120;
        case enmFieldType.Date: return 80;
        case enmFieldType.Time: return 60;
        case enmFieldType.DateTime: return 120;
        default: return 90;
      }
    }
    #endregion

    #region public void Clear()
    public void Clear() 
    {
      SetPropriedades();
      this.Fields.Clear();
      this.Rows.Clear();
      this.Columns.Clear();
    }
    #endregion

    #region public void AddColumns(List<FieldColumn> Fields)
    public void AddColumns(List<FieldColumn> Fields)
    {
      for (int i = 0; i < Fields.Count; i++)
      { AddColumn(Fields[i]); }
    }
    #endregion

    #region public void AddColumn(sknGridColumn Field)
    public void AddColumn(FieldColumn Field)
    {
      DataGridViewCellStyle sty = new DataGridViewCellStyle();
      sty.Format = GetFormat(Field.Type);
      sty.Alignment = GetAlingnment(Field.Type);

      DataGridViewColumn dvc = new DataGridViewColumn();
      
      dvc.DefaultCellStyle = sty;
      dvc.Name = Field.Name;
      dvc.DataPropertyName = Field.Name;
      
      dvc.HeaderText = Field.Text;
      dvc.ReadOnly = true;
      dvc.SortMode = DataGridViewColumnSortMode.Automatic;
      dvc.Width = (Field.Size == 0 ? GetSize(Field.Type) : Field.Size);

      if (Field.Type == enmFieldType.Bool)
      { dvc.CellTemplate = new DataGridViewCheckBoxCell(); }
      else
      { dvc.CellTemplate = new DataGridViewTextBoxCell(); }

      this.Columns.Add(dvc);
      Fields.Add(Field);
      //if (this.Rows.Count != 0)
      //{ this.Rows.Clear(); }
    }
    #endregion

    #region AddItem
    #region public void AddItem(object Class)
    public void AddItem(object Class)
    {
      lib.Class.ObjectAttribute fo = new lib.Class.ObjectAttribute(Class);

      object[] lstO = new object[Fields.Count];
      for (int i = 0; i < Fields.Count; i++)
      {
        //if (fo.AttibuteExists(Fields[i].Name))
        //{ 
          lstO[i] = fo.GetAttribute(Fields[i].Name);
          if (lstO[i] == null || (lstO[i].GetType() == typeof(DateTime) && (DateTime)lstO[i] == DateTime.MinValue))
          { lstO[i] = ""; }
        //}
      }

      if (lstO.Length == 0 || lstO.Length == 1 && lstO[0] == null)
      { return; }

      AddItem(lstO, Class);
    }
    #endregion

    #region private void AddCellsOfItem(object[] Cells, object Class)
    public void AddItem(object[] Cells, object Class) 
    {
      this.Rows.Add(Cells);
      this.Rows[this.Rows.Count - 1].Tag = Class;
    }
    #endregion

    #region public void AddItems(object[] Source)
    public void AddItems(object[] Source)
    {
      //int idx = this.Rows.Count;
      for (int i = 0; i < Source.Length; i++)
      { this.AddItem(Source[i]); }
    }
    #endregion
    #endregion

    #region public void AlterItem(object Class)
    public void AlterItem(object Class)
    {
      if (this.SelectedRows.Count != 0)
      {
        int Index = this.SelectedRows[0].Index;
        AlterItem(Index, Class);
        //lib.Class.ObjectAttribute fo = new lib.Class.ObjectAttribute(Class);
        //this.Rows[Index].Tag = Class;
        //for (int i = 0; i < Fields.Count; i++)
        //{ this.Rows[Index].Cells[i].Value = fo.GetAttribute(Fields[i].Name); }
      }
    }
    #endregion        

    #region public void AlterItem(int Index, object Class)
    public void AlterItem(int Index, object Class) 
    {
      lib.Class.ObjectAttribute fo = new lib.Class.ObjectAttribute(Class);
      this.Rows[Index].Tag = Class;
      for (int i = 0; i < Fields.Count; i++)
      {
        object val = fo.GetAttribute(Fields[i].Name);
        
        if (val == null || (val.GetType() == typeof(DateTime) && (DateTime)val == DateTime.MinValue))
        { val = ""; }

        this.Rows[Index].Cells[i].Value = val;
      }
    }
    #endregion        
    
    #region public T GetItem<T>()
    public T GetItem<T>()
    {
      if (this.SelectedRows.Count != 0)
      { return (T)this.SelectedRows[0].Tag; }
      else
      { return Activator.CreateInstance<T>(); }
    }
    #endregion

    #region public T GetItem<T>(int row)
    public T GetItem<T>(int row)
    {
      if (this.SelectedRows.Count != 0)
      { return (T)this.Rows[row].Tag; }
      else
      { return Activator.CreateInstance<T>(); }
    }
    #endregion

    #region public T[] GetItems<T>()
    public T[] GetItems<T>()
    {
      T[] o_lst = new T[this.Rows.Count];
      for (int i = 0; i < this.Rows.Count; i++)
      { o_lst[i] = this.GetItem<T>(i); }
      return o_lst;
    }
    #endregion

    #region public int GetIdxField(string FieldName)
    public int GetFieldIndex(string FieldName) 
    {
      for (int i = 0; i < Fields.Count; i++)
      {
        if (Fields[i].Name.ToUpper() == FieldName.ToUpper())
        { return i; }
      }
      return -1;
    }
    #endregion

    #region public lib.Class.Conversion GetField(string FieldName)
    public lib.Class.Conversion GetField(int row, string FieldName)
    {
      int fIdx = GetFieldIndex(FieldName);
      if (row != -1 && fIdx != -1)
      { return new lib.Class.Conversion(this.Rows[row].Cells[fIdx].Value); }
      else if (row != -1 && this.Rows[row].Tag != null)
      {
        lib.Class.ObjectAttribute ObjAtr = new Class.ObjectAttribute(this.Rows[row].Tag);
        if (ObjAtr.AttibuteExists(FieldName))
        { return new lib.Class.Conversion(ObjAtr.GetAttribute(FieldName)); }
      }

      return new lib.Class.Conversion();
    }

    public lib.Class.Conversion GetField(string FieldName) 
    {
      if (this.SelectedRows.Count != 0)
      {
        int row = this.SelectedRows[0].Index;
        return GetField(row, FieldName);
      }
      else { return new lib.Class.Conversion(); }
    }
    #endregion
    #endregion
  }
}
