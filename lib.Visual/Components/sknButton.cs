using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace lib.Visual.Components
{
  [ToolboxItem(true), ToolboxBitmap(typeof(sknButton), "bmp/sknButton.bmp")]
  public class sknButton : Button
  {
    protected override void CreateHandle()
    {
      try
      {
        if (!this.DesignMode && Resources.Skin.Enabled)
        {
          this.FlatStyle = FlatStyle.Flat;
          this.FlatAppearance.BorderSize = 1;
          this.FlatAppearance.BorderColor = Resources.Skin.Buttons.BorderColor;
          this.ForeColor = Resources.Skin.Buttons.ForeColor;
          this.Font = Resources.Skin.Buttons.Font;
          this.BackgroundImageLayout = ImageLayout.Stretch;
          this.BackgroundImage = lib.Class.ProcessImage.StringToImage(Resources.Skin.Buttons.ImageBack);
          this.MouseHover += new EventHandler(sknButton_MouseHover);
          this.MouseLeave += new EventHandler(sknButton_MouseLeave);
          this.MouseDown += new MouseEventHandler(sknButton_MouseDown);
        }
      }
      catch { }
      base.CreateHandle();
    }

    void sknButton_MouseDown(object sender, MouseEventArgs e)
    {
      this.BackgroundImage = lib.Class.ProcessImage.StringToImage(Resources.Skin.Buttons.ImageDown);
    }

    void sknButton_MouseLeave(object sender, EventArgs e)
    {
      this.BackgroundImage = lib.Class.ProcessImage.StringToImage(Resources.Skin.Buttons.ImageBack);
    }

    void sknButton_MouseHover(object sender, EventArgs e)
    {
      this.BackgroundImage = lib.Class.ProcessImage.StringToImage(Resources.Skin.Buttons.ImageHover);
    }
  }
}
