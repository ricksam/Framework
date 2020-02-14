using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Touchless.Vision.Camera;

namespace lib.Visual.Components
{
  public delegate void WebCamVisionCapture_Handle(System.Drawing.Bitmap frame);
  public class WCVision:CamBase
  {
    public override void Start()
    {
      StartVision(GetCameras()[0]);
      base.Start();
    }

    public override void Stop()
    {
      StopVision();
      base.Stop();
    }

    private CameraFrameSource _frameSource;
    private static System.Drawing.Bitmap _latestFrame;
    public event WebCamVisionCapture_Handle WebCamVisionCapture;

    public Camera[] GetCameras()
    {
      List<Camera> list = new List<Camera>();
      foreach (Camera cam in CameraService.AvailableCameras)
      {
        list.Add(cam);
      }
      return list.ToArray();
    }

    public void StartVision(Camera c)
    {
      try
      {
        setFrameSource(new CameraFrameSource(c));
        _frameSource.Camera.CaptureWidth = 320;
        _frameSource.Camera.CaptureHeight = 240;
        _frameSource.Camera.Fps = 20;
        _frameSource.NewFrame += OnImageCaptured;

        Control.Paint += new System.Windows.Forms.PaintEventHandler(drawLatestImage);
        _frameSource.StartFrameCapture();
      }
      catch (Exception ex)
      {
        throw new Exception("Select a Camera");
      }
    }

    private void drawLatestImage(object sender, System.Windows.Forms.PaintEventArgs e)
    {
      if (_latestFrame != null)
      {
        // Draw the latest image from the active camera
        //.Bitmap bmp = 
        e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
      }
    }

    protected void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
    {
      _latestFrame = frame.Image;

      if (WebCamVisionCapture != null)
      { WebCamVisionCapture(_latestFrame); }

      Control.Invalidate();
    }

    private void setFrameSource(CameraFrameSource cameraFrameSource)
    {
      if (_frameSource == cameraFrameSource)
        return;

      _frameSource = cameraFrameSource;
    }

    public void StopVision()
    {
      // Trash the old camera
      if (_frameSource != null)
      {
        _frameSource.NewFrame -= OnImageCaptured;
        _frameSource.Camera.Dispose();
        setFrameSource(null);
        Control.Paint -= new System.Windows.Forms.PaintEventHandler(drawLatestImage);
      }
    }

    public void Config()
    {
      if (_frameSource != null)
        _frameSource.Camera.ShowPropertiesDialog();
    }
  }
}
