using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lib.Visual.Models
{
  public partial class frmEdit : frmBase
  {
    #region Constructor
    public frmEdit()
    {
      InitializeComponent();
      this.ID = new lib.Class.Conversion();
      this.VerifyCancel = true;
    }
    #endregion

    #region Fields
    public lib.Class.Conversion ID { get; set; }
    protected bool VerifyCancel { get; set; }
    #endregion

    #region Methods
    protected override void CreateHandle()
    {      
      pnlBottom.Height = 45;
      btnCancel.Left = this.Width - 118;
      btnConfirm.Left = this.Width - 214; 
      btnCancel.Top = 8;
      btnConfirm.Top = 8;
      base.CreateHandle();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Escape)
      { this.OnCancel(); }
      return base.ProcessCmdKey(ref msg, keyData);
    }
    
    protected virtual void OnConfirm()
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    protected virtual void OnCancel()
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    public bool Exec()
    { return (this.ShowDialog() == DialogResult.OK); }
    #endregion

    #region Events
    private void Cancel_Click(object sender, EventArgs e)
    {
      OnCancel();
    }

    private void Confirm_Click(object sender, EventArgs e)
    {
      OnConfirm();
    }

    private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (VerifyCancel && this.DialogResult != DialogResult.OK)
      { e.Cancel = !lib.Visual.Msg.Question("Tem certeza que deseja cancelar?"); }
    }
    #endregion

    private void frmEdit_Load(object sender, EventArgs e)
    {
      if (lib.Visual.Components.Resources.Skin.Enabled)
      { pnlBottom.BackColor = lib.Visual.Components.Resources.Skin.Containers.ButtonAreaBackColor; }
    }
  }
}
