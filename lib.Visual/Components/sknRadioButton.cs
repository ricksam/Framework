using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknRadioButton))]
  public class sknRadioButton:RadioButton
  {
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          this.FlatStyle = FlatStyle.Flat;
          this.BackColor = Color.Transparent;
          this.ForeColor = Resources.Skin.Labels.ForeColor;
          this.Font = Resources.Skin.Labels.Font;          
          this.borderDrawer.BorderColor = Resources.Skin.Controls.BorderColor;
        }
      }
      catch { }
      base.CreateHandle();
    }

    private BorderDrawer borderDrawer = new BorderDrawer();
    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (Resources.Skin.Enabled)
      { borderDrawer.DrawBorderRadio(ref m, 0, 2, 11, 11); }
    }
  }
}
