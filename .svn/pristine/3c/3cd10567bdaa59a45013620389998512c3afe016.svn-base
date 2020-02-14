using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Class
{
  #region public class Log
  /// <summary>
  /// Classe responsável por todo o gerenciamento de erros da aplicação
  /// </summary>
  [Serializable]
  public class Log
  {
    #region Constructor
    public Log(string LogFile)
    {
      this.LastLog = "";
      this.LogFile = LogFile;
      tf = new TextFile();
    }
    #endregion

    #region Fields
    private string LogFile { get; set; }
    private TextFile tf { get; set; }
    private string LastLog{ get; set; }
    #endregion

    #region public void Save(Log Log)
    /// <summary>
    /// Salva Log
    /// </summary>
    /// <param name="Msg"></param>
    public void Save(string Message)
    {
      if (Message != LastLog)
      {
        tf.Open(enmOpenMode.Writing, LogFile);
        tf.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " " + Message);
        tf.WriteLine(" ");
        tf.Close();
      }
      this.LastLog = Message;
    }
    #endregion

    public void Save(Exception ex) 
    {
      string endln = "\r\n";
      Save(
        ex.Message + endln +
        (ex.InnerException != null ? "Inner:" + ex.InnerException.Message + endln : "") +
        "Stack:" + ex.StackTrace
        );
    }
  }
  #endregion  
}
