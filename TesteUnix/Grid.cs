using System;
using System.Collections.Generic;
using lib.Database.Drivers;
using lib.Database.Query;
using lib.Database.MVC;

namespace TesteUnix
{
  [System.ComponentModel.ToolboxItem(true)]
  public partial class Grid : Gtk.Bin
  {
    public Grid ()
    {
      this.Build ();
      Items = new List<DefaultEntity>();
    }
    
    List<FieldColumn> Columns { get; set; }
    List<DefaultEntity> Items { get; set; }
    
    #region private void AddColumns(string[] Names)
    private void AddColumns(string[] Names)
    {
      for (int i = 0; i < Names.Length; i++) 
      {
        // Create a column for the artist name
        Gtk.TreeViewColumn col = new Gtk.TreeViewColumn ();
        col.Title = Names[i];
                             
        // Create the text cell that will display the artist name
        Gtk.CellRendererText cell = new Gtk.CellRendererText ();
                
        // Add the cell to the column
        col.PackStart (cell, true);
         
        // Tell the Cell Renderers which items in the model to display
        col.AddAttribute (cell, "text", i);
        
        // Add the columns to the TreeView
        tree.AppendColumn(col);
      }      
      
      Renderize();
    }
    #endregion
    
    #region private void Renderize()
    private void Renderize()
    {
      Type[] par = new Type[tree.Columns.Length];
      for (int i = 0; i < par.Length; i++) {
        par[i] = typeof (string);
      }
      
      // Create a model that will hold two strings - Artist Name and Song Title
      Gtk.ListStore musicListStore = new Gtk.ListStore (par);
      
      // Assign the model to the TreeView
      tree.Model = musicListStore;
    }
    #endregion
    
    #region private void AddItem(params object[] Values)
    private void AddItem(params object[] Values)
    {
      ((Gtk.ListStore)tree.Model).AppendValues(Values);
    }
    #endregion
    
    #region private int SelectedIndex() 
    private int SelectedIndex() 
    {
      Gtk.TreeIter iter;
      tree.Selection.GetSelected(out iter);
      Gtk.TreePath path = tree.Model.GetPath(iter);
      if (path != null)
      { return path.Indices.Length != -1 ? path.Indices[0] : -1; }
      else 
      { return -1; }
    }
    #endregion
    
    
    #region public void AddColumns(List<FieldColumn> Fields)
    public void AddColumns(List<FieldColumn> Columns)
    {
      this.Columns = Columns;
      string[] lst = new string[Columns.Count];
      for (int i = 0; i < lst.Length; i++) 
      { lst[i] = Columns[i].Text; }
      
      AddColumns(lst);
      Renderize();
    }
    #endregion
    
    #region public void AddItems(DefaultEntity Items)
    public void AddItems(List<DefaultEntity> Items)
    {
      for (int i = 0; i < Items.Count; i++) {
        AddItem(Items[i]); 
      }      
    }
    #endregion
    
    #region public void AddItem(DefaultEntity def)
    public void AddItem(DefaultEntity def)
    {
      Items.Add(def);
      
      lib.Class.ObjectAttribute obj = new lib.Class.ObjectAttribute(def);
      string[] item = new string [Columns.Count];
      
      for (int i = 0; i < Columns.Count; i++) 
      {
        if(Columns[i].Type == enmFieldType.Date)
        { item[i] = ((DateTime)obj.GetAttribute(Columns[i].Name)).ToString("dd/MM/yyyy"); }        
        else if(Columns[i].Type == enmFieldType.DateTime)
        { item[i] = ((DateTime)obj.GetAttribute(Columns[i].Name)).ToString("dd/MM/yyyy HH:mm:ss"); }
        else if(Columns[i].Type == enmFieldType.Decimal)
        { item[i] = ((DateTime)obj.GetAttribute(Columns[i].Name)).ToString("#,##0.00"); }
        else
        { item[i] = obj.GetAttribute(Columns[i].Name).ToString(); }
      }
      AddItem (item);
    }
    #endregion
  }
}

