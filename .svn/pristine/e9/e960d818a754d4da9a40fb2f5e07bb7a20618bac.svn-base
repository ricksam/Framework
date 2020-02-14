using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lib.Visual.Forms
{
  public partial class Login : lib.Visual.Models.frmDialog
  {
    public Login()
    {
      InitializeComponent();
    }

    public string getLogin()
    { return this.txtLogin.Text; }

    public string getPassword()
    { return this.txtSenha.Text; }

    private void Login_Load(object sender, EventArgs e)
    {
      this.Text = "Login "+ lib.Class.Utils.GetVersion();
      this.txtLogin.Text = "";
      this.txtSenha.Text = "";
      this.txtLogin.Select();
    }
  }
}
