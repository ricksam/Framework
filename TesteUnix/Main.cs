using System;
using Gtk;

namespace TesteUnix
{
  class MainClass
  {
    public static void Main (string[] args)
    {
      Application.Init ();
      Principal win = new Principal();
      win.Show ();
      Application.Run ();
    }
  }
}

