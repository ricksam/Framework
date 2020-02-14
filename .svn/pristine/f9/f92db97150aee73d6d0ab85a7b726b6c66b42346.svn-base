using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RckInstaller
{
  public partial class frmInfo : lib.Visual.Models.frmBase
  {
    public frmInfo()
    {
      InitializeComponent();
      PodeCancelar = false;
    }

    public bool PodeCancelar { get; set; }

    #region public void VisibleControls(bool Visible)
    public void VisibleControls(bool Visible) 
    {
      pbUpdate.Visible = Visible;
      btnCancelar.Visible = Visible;
    }
    #endregion

    #region public void setBackground(string FileName)
    public void setBackground(string FileName)
    {
      if (System.IO.File.Exists(FileName))
      {
        this.BackgroundImage = Image.FromFile(FileName);
        this.BackgroundImageLayout = ImageLayout.Stretch;
      }
    }
    #endregion

    #region public void setTitle(string Title)
    public void setTitle(string Title) 
    {
      lblTitle.Text = Title;
      lblTitle.Refresh();
      System.Threading.Thread.Sleep(1);
      Application.DoEvents();
    }
    #endregion

    #region public void setInfo(string Info)
    public void setInfo(string Info)
    {
      lblInfo.Text = Info;
      lblInfo.Refresh();
      System.Threading.Thread.Sleep(1);
      Application.DoEvents();
    }
    #endregion

    #region public void setProgress(int Percent)
    public void setProgress(int Percent) 
    {
      pbUpdate.Value = Percent;
    }
    #endregion

    #region private void btnCancelar_Click(object sender, EventArgs e)
    private void btnCancelar_Click(object sender, EventArgs e)
    {
      PodeCancelar = true;
    }
    #endregion

    #region private void frmInfo_FormClosing(object sender, FormClosingEventArgs e)
    private void frmInfo_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.BackgroundImage != null)
      {
        this.BackgroundImage.Dispose();
        this.BackgroundImage = null;
      }
    }
    #endregion
  }
}
