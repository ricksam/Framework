using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using lib.Visual.Forms;

namespace lib.Visual
{
  public static class Msg
  {
    #region public static void Information(string aMessage)
    public static void Information(string Message) 
    {
      Information(Message, "Atenção");
    }

    public static void Information(string aMessage, string Title)
    {
      Forms.frmMsg f = new lib.Visual.Forms.frmMsg();
      f.setMessage(frmMsg.enmTypeMsg.Information, Title, aMessage);
      f.ShowDialog();
    }
    #endregion

    #region public static void Warning(string aMessage)
    public static void Warning(string Message) 
    {
      Warning(Message, "Atenção");
    }

    public static void Warning(string Message, string Title)
    {
      Forms.frmMsg f = new lib.Visual.Forms.frmMsg();
      f.setMessage(frmMsg.enmTypeMsg.Warning, Title, Message);
      f.ShowDialog();
    }
    #endregion

    #region public static bool Question(string aMessage)
    public static bool Question(string Message) 
    {
      return Question(Message, "Confirmação");
    }

    public static bool Question(string Message, string Title)
    {
      Forms.frmMsg f = new lib.Visual.Forms.frmMsg();
      f.setMessage(frmMsg.enmTypeMsg.Question, Title, Message);
      return f.ShowDialog() == DialogResult.OK;
    }
    #endregion
  }
}
