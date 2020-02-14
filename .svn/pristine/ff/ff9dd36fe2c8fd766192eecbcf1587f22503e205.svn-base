using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknComboBox))]
  public class sknComboBox:ComboBox
  {
    public sknComboBox()
    {
      AutoTab = true;
    }

    public bool AutoTab { get; set; }

    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          this.FlatStyle = FlatStyle.Flat;
          this.BackColor = Resources.Skin.Controls.BackColor;          
          this.borderDrawer.BorderColor = Resources.Skin.Controls.BorderColor;
          this.Font= Resources.Skin.Controls.Font;
          this.ForeColor = Resources.Skin.Controls.ForeColor;
        }

        this.KeyPress += new KeyPressEventHandler(cmb_KeyPress);
        this.KeyDown += new KeyEventHandler(cmb_KeyDown);
      }
      catch { }
      base.CreateHandle();
    }

    private BorderDrawer borderDrawer = new BorderDrawer();
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (Resources.Skin.Enabled)
      { borderDrawer.DrawBorder(ref m, this.Width, this.Height); }
    }

    private void cmb_KeyPress(object sender, KeyPressEventArgs e) 
    {
      if (e.KeyChar == ((char)13))
      { e.Handled = true; }
    }

    private void cmb_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyData == Keys.Enter && AutoTab)
      {
        e.Handled = true;
        SendKeys.Send("{TAB}");
      }
    }
  }
}
