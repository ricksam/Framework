using System;
using System.Collections.Generic;

namespace lib.Class
{
  #region public class ErrorList
  /// <summary>
  /// Classe responsável por todo o gerenciamento de erros da aplicação
  /// </summary>
  [Serializable]
  public class ErrorList
  {
    #region Constructor
    public ErrorList()
    {
      Items = new List<MsgError>();
    }
    #endregion

    #region Types
    //public delegate void ExceptionEvent(MsgError Msg);
    #endregion

    #region Fields
    private List<MsgError> Items { get; set; }
    //public ExceptionEvent OnException { get; set; }
    public bool HasError
    {
      get
      {
        System.Threading.Thread.Sleep(1);
        return Items.Count != 0;
      }
    }
    #endregion

    #region Methods
    #region private bool ErrorExists(MsgError Msg)
    /// <summary>
    /// Verifica semelhança no erro
    /// </summary>
    /// <param name="Msg"></param>
    /// <returns></returns>
    private bool ErrorExists(MsgError Msg)
    {
      for (int i = 0; i < Items.Count; i++)
      {
        if
        (
          Msg.Title == Items[i].Title &&
          Msg.Exception != null && Msg.Exception.Message == Items[i].Exception.Message &&
          Msg.AppName == Items[i].AppName
        )
        { return true; }
      }
      return false;
    }
    #endregion

    #region public MsgError getLastError()
    public MsgError getLastError()
    {
      if (HasError)
      {
        MsgError er = Items[Items.Count - 1];
        Items.RemoveAt(Items.Count - 1);
        return er;
      }
      else
      { return new MsgError("", new Exception()); }
    }
    #endregion

    #region public void Clear()
    public void Clear()
    { Items.Clear(); }
    #endregion

    #region public void Add(MsgError Msg)
    /// <summary>
    /// Adiciona erros
    /// </summary>
    /// <param name="Msg"></param>
    public void Add(MsgError Msg)
    {
      if (!ErrorExists(Msg))
      { Items.Add(Msg); }
      //if (OnException != null)
      //{ OnException(Msg); }
    }
    #endregion

    #region public List<MsgError> get()
    /// <summary>
    /// Retorna toda a lista de erros
    /// </summary>
    /// <returns></returns>
    public List<MsgError> get()
    {
      List<MsgError> l = new List<MsgError>();
      l.AddRange(Items);
      Items.Clear();
      return l;
    }
    #endregion

    #region public List<MsgError> get()
    /// <summary>
    /// Retorna toda a lista de erros de um determinado aplicativo
    /// </summary>
    /// <returns></returns>
    public List<MsgError> get(String AppName)
    {
      List<MsgError> l = new List<MsgError>();
      for (int i = 0; i < Items.Count; i++)
      {
        if (Items[i].AppName == AppName)
        {
          l.Add(Items[i]);
          Items.RemoveAt(i);
          i--;
        }
      }
      return l;
    }
    #endregion
    #endregion
  }
  #endregion

  #region public class MsgError
  /// <summary>
  /// Conteúdo do erro
  /// </summary>
  [Serializable]
  public class MsgError
  {
    public MsgError()
    {
      setMsgError("", new Exception(), "");
    }

    public MsgError(string Title, Exception Exception)
    {
      setMsgError(Title, Exception, "");
    }

    public MsgError(string Title, Exception Exception, String AppName)
    {
      setMsgError(Title, Exception, AppName);
    }

    public void setMsgError(string Title, Exception Exception, String AppName)
    {
      this.Title = Title;
      this.Exception = Exception;
      this.AppName = AppName;
      this.DateTime = DateTime.Now;
    }

    public string Title { get; set; }
    public Exception Exception { get; set; }
    public String AppName { get; set; }
    public DateTime DateTime { get; set; }
  }
  #endregion
}


