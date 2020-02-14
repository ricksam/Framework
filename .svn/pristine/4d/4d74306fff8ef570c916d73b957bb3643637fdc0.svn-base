using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Visual.Components;
using System.Threading;

namespace lib.Visual.Forms
{
  public partial class FormSearch : lib.Visual.Models.frmBase
  {
    public FormSearch()
    {
      InitializeComponent();
      ProcessoPesquisa = new Thread(new ThreadStart(Pesquisa));
    }


    public delegate void OnSearch_Handle(object sender, lib.Visual.Components.sknGrid Grid, string TextSearch);
    public OnSearch_Handle OnSearch { get; set; }
    private Thread ProcessoPesquisa { get; set; }

    private void Pesquisa() 
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new MethodInvoker(Pesquisa));
        return;
      }

      if (OnSearch != null)
      {
        if (string.IsNullOrEmpty(txtPesquisa.Text.Trim()))
        { Grid.Clear(); }
        else
        { OnSearch(this, Grid, txtPesquisa.Text); }
      }
    }

    private void ReiniciaThread() 
    {
      if (ProcessoPesquisa != null)
      { ProcessoPesquisa = null; }
      ProcessoPesquisa = new Thread(new ThreadStart(Pesquisa));
      ProcessoPesquisa.Start();
    }

    private void ExecutaThreadPesquisa(bool Enter)
    {
      if (Enter)
      {
        pnlPesquisa.Enabled = false;
        Grid.Visible = false;

        while (ProcessoPesquisa != null && ProcessoPesquisa.IsAlive) ;
        ReiniciaThread();

        pnlPesquisa.Enabled = true;
        Grid.Visible = true;
        
        Grid.Focus();
      }
      else
      {
        if (ProcessoPesquisa != null && !ProcessoPesquisa.IsAlive)
        { ReiniciaThread(); }
        else 
        { return; }
      }
    }

    private void btnPesquisar_Click(object sender, EventArgs e)
    {
      ExecutaThreadPesquisa(true);
    }

    private void txtPesquisa_TextChanged(object sender, EventArgs e)
    {
      ExecutaThreadPesquisa(false);
    }

    private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter)
      {
        e.Handled = true;
        ExecutaThreadPesquisa(true);
      }
      else if (e.KeyData == Keys.Up || e.KeyData == Keys.Down)
      {
        ExecutaThreadPesquisa(true);
        Grid.Select();

        if (e.KeyData == Keys.Up)
        { SendKeys.Send("{UP}"); }
        else if (e.KeyData == Keys.Down)
        { SendKeys.Send("{DOWN}"); }
      }
    }

    private void GridPesquisa_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == ((char)13))
      { e.Handled = true; }
      else if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ((char)8) || e.KeyChar == '%')
      {
        e.Handled = true;
        txtPesquisa.Select();
        if (e.KeyChar == ((char)8) && txtPesquisa.Text.Length != 0)
        {
          txtPesquisa.Text = txtPesquisa.Text.Remove(txtPesquisa.Text.Length - 1, 1);
          txtPesquisa.SelectionStart = txtPesquisa.Text.Length;
          txtPesquisa.SelectionLength = 0;
        }
        else
        {
          txtPesquisa.Text = e.KeyChar.ToString();
          txtPesquisa.SelectionStart = 1;
          txtPesquisa.SelectionLength = 1;
        }
      }
    }

    private void FormSearch_Load(object sender, EventArgs e)
    {
      txtPesquisa.Text = "";
    }    
  }
}
