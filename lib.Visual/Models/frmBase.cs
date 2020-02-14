using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Visual.Components;

namespace lib.Visual.Models
{
  public partial class frmBase : Form
  {
    public frmBase()
    {
      InitializeComponent();      
      if (Resources.Skin.Enabled) 
      {
        if (!string.IsNullOrEmpty(Resources.Skin.Containers.ImageBack))
        {
          this.BackgroundImageLayout = ImageLayout.Stretch;
          this.BackgroundImage = lib.Class.ProcessImage.StringToImage(Resources.Skin.Containers.ImageBack);
        }

        this.BackColor = Resources.Skin.Containers.BackColor;
      }
    }
  }
}
