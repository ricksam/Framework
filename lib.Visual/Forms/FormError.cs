using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;

namespace lib.Visual.Forms
{
  public partial class FormError : lib.Visual.Models.frmDialog
  {
    public FormError()
    {
      InitializeComponent();
      ErrorList = new ErrorList();
      Errors = new List<MsgError>();
      //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
    }

    #region Fields
    private List<MsgError> Errors { get; set; }//Lista local para navegação na combo
    private ErrorList ErrorList { get; set; }
    #endregion

    #region Methods
    protected override void OnConfirm()
    {
      try
      {
        btnConfirm.Enabled = false;
        btnConfirm.Text = "Transmitindo...";
        btnConfirm.Refresh();

        Mail mail = lib.Class.WebUtils.GetMailDeveloper();
        string MailMessage = "";

        for (int i = 0; i < Errors.Count; i++)
        {
          MailMessage += string.Format(
            @"
            <h1>LOGERROR</h1>
            <b>DateTime:</b>{0}<br />
            <b>Title:</b>{1}<br />
            <b>Message:</b>{2}<br />
            <b>InnerException:</b>{3}<br />
            <b>Sistema:</b>{4}<br />
            <b>LocalIp:</b>{5}<br />
            <b>RemoteIp:</b>{6}<br />
            <b>StackTrace:</b>{7}<br />
          ",
            Errors[i].DateTime,
            Errors[i].Title,
            Errors[i].Exception.Message,
            (Errors[i].Exception.InnerException != null ? Errors[i].Exception.InnerException.Message : ""),
            Application.ExecutablePath,
            Utils.GetLocalIP()[0],
            WebUtils.GetRemoteIP(),
            Errors[i].Exception.StackTrace.Replace("\n","<br />")
            );
        }

        mail.SendMail(MailMessage, true, "jricksam@gmail.com", "LOG ERROR");
        base.OnConfirm();
        btnConfirm.Text = "Confirmar";
        btnConfirm.Enabled = true;
      }
      catch (Exception ex)
      { Msg.Warning("Erro ao enviar o log. Message:" + ex.Message); }
    }

    #region private void setError(MsgError Error)
    private void setError(MsgError Error) 
    {
      txtTitle.Text = Error.Title;
      txtLevel.Text = Error.AppName;
      
      if (Error.Exception != null)
      {
        txtMessage.Text = Error.Exception.Message;
        txtStackTrace.Text = Error.Exception.StackTrace;

        if (Error.Exception.InnerException != null)
        { txtErroInterno.Text = Error.Exception.InnerException.Message; }
      }
    }
    #endregion

    #region private void setErrors(List<MsgError> Errors)
    private void setErrors(List<MsgError> Errors)
    {
      this.Errors.Clear();
      cmbItems.Items.Clear();
      for (int i = Errors.Count - 1; i >= 0; i--)
      {
        this.Errors.Add(Errors[i]);
        cmbItems.Items.Add(
          Errors[i].DateTime.ToString("dd/MM/yyyy HH:mm:ss") + " - " +
          Errors[i].Title
        );
        cmbItems.SelectedIndex = 0;
      }

      if (this.Errors.Count != 0)
      { setError(this.Errors[0]); }
    }
    #endregion

    #region private void cmbItems_SelectedIndexChanged(object sender, EventArgs e)
    private void cmbItems_SelectedIndexChanged(object sender, EventArgs e)
    {
      setError(Errors[cmbItems.SelectedIndex]);
    }
    #endregion

    #region public bool HasError()
    public bool HasError()
    { return ErrorList.HasError; }
    #endregion


    #region public void Add(MsgError MsgError)
    /// <summary>
    /// Adiciona um erro
    /// </summary>
    /// <param name="MsgError"></param>
    public void Add(Exception ex)
    { Add(ex.Message, ex); }

    public void Add(string Title, Exception ex) 
    {
      ErrorList.Add(new MsgError(Title, ex));
    }
    #endregion

    #region public void ShowError(MsgError MsgError)
    /// <summary>
    /// Adiciona um erro e exibe os erros listados
    /// </summary>
    /// <param name="MsgError"></param>
    
    public void ShowError(Exception ex)
    { ShowError(ex.Message, ex); }

    public void ShowError(string Title, Exception ex)
    {
      Add(Title, ex);
      ShowError();
    }
    #endregion

    #region public void ShowError()
    /// <summary>
    /// Exibe erros listados
    /// </summary>
    public void ShowError()
    {
      this.setErrors(ErrorList.get());
      this.Exec();
    }
    #endregion
    #endregion
  }
}
