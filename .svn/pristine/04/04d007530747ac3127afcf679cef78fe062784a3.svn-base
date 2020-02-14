using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknProgressBar))]
  public class sknProgressBar : ProgressBar
  {
    public sknProgressBar() 
    {      
      this.Paint += new PaintEventHandler(sknProgessBar_Paint);
    }

    protected override void CreateHandle()
    {
      if (!this.DesignMode && Resources.Skin.Enabled)
      {
        this.SetStyle(ControlStyles.UserPaint, true);
        this.BackColor = Resources.Skin.Controls.BackColor;
      }
      base.CreateHandle();
    }

    void sknProgessBar_Paint(object sender, PaintEventArgs e)
    {
      if (Resources.Skin.Enabled)
      {
        int hg = this.Height - 1;
        int wd = this.Width - 1;
        Rectangle rBorda = new Rectangle(0, 0, wd, hg);
        Rectangle rPre = new Rectangle(1, 1, getFator(getPercent(), wd - 1), hg - 1);

        e.Graphics.DrawRectangle(new Pen(Resources.Skin.Controls.BorderColor), rBorda);
        if (!string.IsNullOrEmpty( Resources.Skin.Controls.ImageProgressBar))
        { e.Graphics.DrawImage(lib.Class.ProcessImage.StringToImage(Resources.Skin.Controls.ImageProgressBar), rPre); }
        else
        { e.Graphics.FillRectangle(Brushes.Silver, rPre); }
      }
    }

    private int getPercent() 
    {
      try
      {
        int Range = Maximum - Minimum;
        return Convert.ToInt32(((Value - Minimum) / ((double)Range) * 100));
      }
      catch { return 0; }
    }

    private int getFator(int Percent, int Max)
    {
      if (Percent <= 0)
      { return 0; }
      else if (Percent <= 100)
      { return Max * Percent / 100; }
      else
      { return 100; }
    }

    /*public int Maximum { get; set; }
    public int Minimum { get; set; }
    public int Value { get; set; }*/
  }  
}
