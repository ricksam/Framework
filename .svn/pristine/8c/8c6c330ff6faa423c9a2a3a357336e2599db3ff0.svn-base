using System;
using Gtk;

namespace TesteUnix
{
  public partial class Principal : Gtk.Window
  {
    public Principal () : 
        base(Gtk.WindowType.Toplevel)
    {
      this.Build ();
    }

    protected void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
    {
      Application.Quit();
      args.RetVal = true;
    }
  }
}

