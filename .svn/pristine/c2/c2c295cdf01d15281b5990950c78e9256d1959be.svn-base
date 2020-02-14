using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknGroupBox))]
  public class sknGroupBox : GroupBox
  {
    public sknGroupBox() 
    {
      /*this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.Height = 100;
      this.Width = 200;
      this.Text = "sknGroupBox";*/
    }

    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          //this.draw
          //this.FlatStyle = FlatStyle.Flat;
          this.SetStyle(ControlStyles.UserPaint, true);
          this.BackColor = Color.Transparent;
          this.ForeColor = Resources.Skin.Labels.ForeColor;
          this.Font = Resources.Skin.Labels.Font;          
        }
        this.Paint += new PaintEventHandler(sknGroupBox_Paint);
      }
      catch { }
      base.CreateHandle();
    }

    void sknGroupBox_Paint(object sender, PaintEventArgs e)
    {
      if (Resources.Skin.Enabled)
      {
        Rectangle r = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        e.Graphics.DrawRectangle(new Pen(Resources.Skin.Controls.BorderColor), r);

        if (!string.IsNullOrEmpty( Resources.Skin.Controls.ImageGroupBok))
        {
          e.Graphics.DrawImage(
            lib.Class.ProcessImage.StringToImage(Resources.Skin.Controls.ImageGroupBok),
            new Rectangle(1, 1, this.Width - 2, 20)
            );
        }
        else
        {
          e.Graphics.FillRectangle(
            Brushes.Silver,
            new Rectangle(1, 1, this.Width - 2, 20)
            );
        }

        e.Graphics.DrawString(
          this.Text,
          Resources.Skin.Labels.Font,
          new SolidBrush(Resources.Skin.Labels.ForeColor),
          4, 4
        );
      }
    }
  }
}
