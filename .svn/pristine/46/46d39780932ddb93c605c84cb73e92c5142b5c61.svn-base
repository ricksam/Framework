using System;
namespace TesteUnix
{
  [System.ComponentModel.ToolboxItem(true)]
  public partial class WidList : Gtk.Bin
  {
    public WidList ()
    {
      this.Build ();
    }
    
    public Gtk.TreeModel Model{
      get{ return tree.Model; }
      set{ tree.Model = value; }
    }
    
    public void AddColumns(string[] Names)
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
    
    public void Renderize()
    {
      Type[] par = new Type[tree.Columns.Length];
      for (int i = 0; i < par.Length; i++) {
        par[i] = typeof (string);
      }
      
      // Create a model that will hold two strings - Artist Name and Song Title
      Gtk.ListStore ListStore = new Gtk.ListStore (par);
      
      // Assign the model to the TreeView
      tree.Model = ListStore;
    }
    
    public void AddItem(params object[] Values)
    {
      ((Gtk.ListStore)tree.Model).AppendValues(Values);
    }
    
    public int ColumnCount{get {return tree.Columns.Length;}}
    
    public Gtk.TreeIter GetSelectedItem()
    {
      Gtk.TreeIter iter;       
      tree.Selection.GetSelected(out iter);
      
      return iter;
    }
    
    
  }
}

