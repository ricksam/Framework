using System;

namespace lib.Unix
{
  public static class Msg
  {
    public static void Information(Gtk.Window window, string Message)
    {
      Information(window, Message, "Atenção");
    }
    
    public static void Information(Gtk.Window window, string Message, string Title)
    {
      Gtk.MessageDialog md = new Gtk.MessageDialog(window,
        Gtk.DialogFlags.Modal,
        Gtk.MessageType.Info,
        Gtk.ButtonsType.Ok,
        Message,
        new string[]{});
      md.Title=Title;
      md.Run();
      md.Destroy();
    }

    public static void Warning(Gtk.Window window, string Message)
    {
      Warning(window, Message, "Atenção");
    }   
    
    public static void Warning(Gtk.Window window, string Message, string Title)
    {
      Gtk.MessageDialog md = new Gtk.MessageDialog(window,
        Gtk.DialogFlags.Modal,
        Gtk.MessageType.Warning,
        Gtk.ButtonsType.Ok,
        Message,
        new string[]{});
      md.Title=Title;
      md.Run();
      md.Destroy();
    }

    public static bool Question(Gtk.Window window, string Message)
    {
      return Question(window, Message, "Atenção");
    }
    
    public static bool Question(Gtk.Window window, string Message, string Title)
    {
      Gtk.MessageDialog md = new Gtk.MessageDialog(window,
        Gtk.DialogFlags.Modal,
        Gtk.MessageType.Question,
        Gtk.ButtonsType.YesNo,
        Message,
        new string[]{});
      md.Title=Title;
      bool r = md.Run() == (int)Gtk.ResponseType.Yes;
      md.Destroy();  
      return r;
    }
  }
}

