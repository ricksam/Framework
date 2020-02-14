using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknPanel))]
  public class sknPanel: Panel
  {
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          this.BackColor = Color.Transparent;
        }
      }
      catch { }
      base.CreateHandle();
    }
  }
}
