using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lib.Visual.Components
{
  public class EmguCV:CamBase
  {
    Emgu.CV.Capture EmguCap { get; set; }

    public override void Start()
    {
      EmguCap = new Emgu.CV.Capture();
      EmguCap.QueryFrame();
      Application.Idle += new EventHandler(FrameGrabber_Parrellel);

      base.Start();
    }

    public override void Stop()
    {
      if (EmguCap != null)
      {
        Application.Idle -= new EventHandler(FrameGrabber_Parrellel);
        EmguCap.Dispose();
        EmguCap = null;
      }

      base.Stop();
    }

    void FrameGrabber_Parrellel(object sender, EventArgs e)
    {
      //Get the current frame form capture device
      //Image<Bgr, Byte> currentFrame = cap.QueryFrame().Resize(Config.PhotoBooth_CapWidth, Config.PhotoBooth_CapHeight, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
      Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> currentFrame = EmguCap.QueryFrame().Resize(this.Width, this.Height, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

      if (Control is PictureBox)
      {
        ((PictureBox)Control).Image = currentFrame.ToBitmap();
        //((PictureBox)Control).Image.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX);
      }
    }
  }
}
