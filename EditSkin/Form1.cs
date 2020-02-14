using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Visual.Components;

namespace EditSkin
{
  public partial class Form1 : lib.Visual.Models.frmBase
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void sknButton1_Click(object sender, EventArgs e)
    {
      lib.Visual.Components.Resources.Edit();
    }

    private void sknButton2_Click(object sender, EventArgs e)
    {
      if (dlOpen.ShowDialog() == DialogResult.OK) 
      {
        txtFile.Text = dlOpen.FileName;
        Resources.Skin.FileName = dlOpen.FileName;
        Resources.Skin.Open();      
      }
    }

    private void sknButton1_Click_1(object sender, EventArgs e)
    {
      Resources.Edit();
    }

    private void sknButton3_Click(object sender, EventArgs e)
    {
      if (dlSave.ShowDialog() == DialogResult.OK)
      {
        txtFile.Text = dlSave.FileName;
        Resources.Skin.FileName = dlSave.FileName;        
        Resources.Skin.Save();
      }
    }

    private void sknButton4_Click(object sender, EventArgs e)
    {
      Resources.Skin = new Template();
      txtFile.Text = Resources.Skin.FileName;
    }
  }
}
