using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknListBox))]
  public class sknListBox: ListBox
  {
    protected override void CreateHandle()
    {
      if (!this.DesignMode && Resources.Skin.Enabled)
      {
        this.Font = Resources.Skin.Controls.Font;
        this.BackColor = Resources.Skin.Controls.BackColor;
        this.ForeColor = Resources.Skin.Controls.ForeColor;
        this.BorderStyle = BorderStyle.FixedSingle;
        this.borderDrawer.BorderColor = Resources.Skin.Controls.BorderColor;   
      }
      base.CreateHandle();
    }

    private BorderDrawer borderDrawer = new BorderDrawer();
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (Resources.Skin.Enabled)
      { borderDrawer.DrawBorder(ref m, this.Width, this.Height); }
    }
  }
}
