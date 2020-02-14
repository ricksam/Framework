using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lib.Visual.Forms
{
  public partial class frmMsg : lib.Visual.Models.frmBase
  {
    public frmMsg()
    {
      InitializeComponent();
    }

    public enum enmTypeMsg { Information, Warning, Question }

    public void setMessage(enmTypeMsg Type, string Title,string Message) 
    {
      this.Text = Title;
      lblMensagem.Text = Message;

      this.Width = (this.Width - this.ClientRectangle.Width) + lblMensagem.Width + lblMensagem.Left + 20;
      this.Width = (this.Width < 250 ? 250 : this.Width);
      this.Height = (this.Height - this.ClientRectangle.Height) + pnlBottom.Height + lblMensagem.Height + lblMensagem.Top;
      this.Height = (this.Height < 180 ? 180 : this.Height);


      if (Type== enmTypeMsg.Question)
      {        
        this.btnSim.Left = (this.Width / 2) - 93;
        this.btnNao.Left = (this.Width / 2) + 3;
      }
      else
      {
        btnSim.Text = "&Ok";
        btnNao.Visible = false;
        this.btnSim.Left = (this.Width / 2) - (this.btnSim.Width / 2);
      }

      if (Type == enmTypeMsg.Information)
      { imgMsg.Image = global::lib.Visual.Properties.Resources.Symbol_Information; }
      else if (Type == enmTypeMsg.Warning)
      { imgMsg.Image = global::lib.Visual.Properties.Resources.Symbol_Exclamation; }
      else if (Type == enmTypeMsg.Question)
      { imgMsg.Image = global::lib.Visual.Properties.Resources.Symbol_Help; }
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void frmMsg_Load(object sender, EventArgs e)
    {
      if (lib.Visual.Components.Resources.Skin.Enabled)
      { pnlBottom.BackColor = lib.Visual.Components.Resources.Skin.Containers.ButtonAreaBackColor; }
    }


  }
}
