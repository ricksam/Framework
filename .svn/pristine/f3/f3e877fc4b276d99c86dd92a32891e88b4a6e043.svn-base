<<<<<<< .mine
using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
  public MainWindow () : base(Gtk.WindowType.Toplevel)
  {
    Build ();
    CriarWideList();
  }
    
  private void CriarWideList()
  {
    widlist1.AddColumns(new string[]{"Nome", "Telefone"});
    widlist1.AddItem("Ricardo","96455614","1");
    widlist1.AddItem("Josiane","96726562","2");
  }

  protected void OnDeleteEvent (object sender, DeleteEventArgs a)
  {
    Application.Quit ();
    a.RetVal = true;
  }

  protected virtual void OnButton138Clicked (object sender, System.EventArgs e)
  {
    if(lib.Unix.Msg.Question(this, "Olá Mundo!"))
    { lib.Unix.Msg.Information(this, "Respondeu Sim!"); }
    else
    { lib.Unix.Msg.Information(this, "Respondeu Não!"); }
    combobox1.Active = 1;
    
    lib.Unix.Msg.Information(this, widlist1.ColumnCount.ToString ());
    
    TreeIter sel_iter ;
    sel_iter = widlist1.GetSelectedItem();
    string selval = (string)widlist1.Model.GetValue(sel_iter, 2);
    lib.Unix.Msg.Information(this, "Selection:"+selval);
    
    TreeIter iter ;
    ((ListStore)widlist1.Model).GetIterFirst(out iter);
        
    do
    {
      string val = (string)widlist1.Model.GetValue(iter,2);
      lib.Unix.Msg.Information(this, val);
    }
    while(((ListStore)widlist1.Model).IterNext(ref iter));
  }
  
  protected virtual void OnTreeCursorChanged (object sender, System.EventArgs e)
  {
  }
  
  
  
}

=======
using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
  public MainWindow () : base(Gtk.WindowType.Toplevel)
  {
    Build ();
    CriarListBox();
    CriarWideList();
  }
  
  private void CriarListBox()
  {    
    // Create a column for the artist name
    Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
    artistColumn.Title = "Artist";
 
    // Create a column for the song title
    Gtk.TreeViewColumn songColumn = new Gtk.TreeViewColumn ();
    songColumn.Title = "Song Title";
 
    // Add the columns to the TreeView
    tree.AppendColumn (artistColumn);
    tree.AppendColumn (songColumn);
 
    // Create a model that will hold two strings - Artist Name and Song Title
    Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (string), typeof (string));
 
 
    // Assign the model to the TreeView
    tree.Model = musicListStore;
    
    // Add some data to the store
    musicListStore.AppendValues ("Garbage", "Dog New Tricks");
    musicListStore.AppendValues ("Roxelle", "Queen of Rain");
    musicListStore.AppendValues ("Roxelle", "Listen to your Heart");
    
    // Create the text cell that will display the artist name
    Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText ();
      
    // Add the cell to the column
    artistColumn.PackStart (artistNameCell, true);
   
    // Do the same for the song title column
    Gtk.CellRendererText songTitleCell = new Gtk.CellRendererText ();
    songColumn.PackStart (songTitleCell, true);
    
    // Tell the Cell Renderers which items in the model to display
    artistColumn.AddAttribute (artistNameCell, "text", 0);
    songColumn.AddAttribute (songTitleCell, "text", 1);
  }
  
  private void CriarWideList()
  {
    //widlist1.AddColumns(new string[]{"Nome", "Telefone"});
    //widlist1.Columns[0].
    
    Pessoa p1 = new Pessoa ("Ricardo","96455614");
    Pessoa p2 = new Pessoa ("Josiane","96726562");
    
    //widlist1.AddItem(p1.Nome, p1.Telefone);
    //widlist1.AddItem(p2.Nome, p2.Telefone);
  }
  
  public class Pessoa
  {
    public Pessoa(string Nome, string Telefone)
    {
      this.Nome = Nome;
      this.Telefone = Telefone;
    }
    public string Nome{ get; set; }
    public string Telefone{ get; set; }
    public override string ToString ()
    {
      return Nome;
    }
  }

  protected void OnDeleteEvent (object sender, DeleteEventArgs a)
  {
    Application.Quit ();
    a.RetVal = true;
  }

  protected virtual void OnButton138Clicked (object sender, System.EventArgs e)
  {
    
  }
  
  protected virtual void OnTreeCursorChanged (object sender, System.EventArgs e)
  {
  }
  
  
  
}

>>>>>>> .r276
