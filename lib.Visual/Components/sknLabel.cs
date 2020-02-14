using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknLabel))]
  public class sknLabel:Label
  {
    public sknLabel() 
    {
      this.AutoSize = true;
    }

    protected override void CreateHandle()
    {
      if (!this.DesignMode && Resources.Skin.Enabled)
      {
        this.Font = Resources.Skin.Labels.Font;
        if (Resources.Skin.Labels.Transparent)
        { this.BackColor = System.Drawing.Color.Transparent; }
        else 
        { this.BackColor = Resources.Skin.Labels.BackColor; }
        this.ForeColor = Resources.Skin.Labels.ForeColor;
        this.BorderStyle = Resources.Skin.Labels.BorderStyle;
      }
      base.CreateHandle();
    }
  }
}
