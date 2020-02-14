using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknTabControl))]
  public class sknTabControl:TabControl
  {
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          for (int i = 0; i < this.TabPages.Count; i++)
          {
            TabPages[i].BorderStyle = BorderStyle.None;
            TabPages[i].BackgroundImageLayout = ImageLayout.Stretch;
            TabPages[i].BackgroundImage = lib.Class.ProcessImage.StringToImage(Resources.Skin.Containers.TabImageBack); 
            //TabPages[i].BackColor = Resources.Skin.Containers.TabBackColor; 
          }
        }
      }
      catch { }
      base.CreateHandle();
    }

    /*void sknTabControl_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
    {
      System.Drawing.Drawing2D.LinearGradientBrush B = (e.Index == this.SelectedIndex) ?
          new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Silver, Color.White, System.Drawing.Drawing2D.LinearGradientMode.Vertical) :
          new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Silver, Color.White, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
      int X = (this.TabPages[e.Index] == this.SelectedTab) ? e.Bounds.X + 5 : e.Bounds.X + 2;
      int Y = (this.TabPages[e.Index] == this.SelectedTab) ? e.Bounds.Y + 5 : e.Bounds.Y + 2;

      e.Graphics.FillRectangle(B, e.Bounds);
      e.Graphics.DrawString(this.TabPages[e.Index].Text, e.Font, Brushes.Black, X, Y);
    }*/
  }
}
