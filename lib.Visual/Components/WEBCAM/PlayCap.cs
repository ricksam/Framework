using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectShowLib;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Threading;

namespace lib.Visual.Components
{
  public enum PlayState
  {
    Stopped,
    Paused,
    Running,
    Init
  }

  [ToolboxItem(true), ToolboxBitmap(typeof(PlayCap))]
  public class PlayCap : CamBase
  {
    public PlayCap()
    {
      this.Height = 480;
      this.Width = 600;
    }
        
    PlayState CurrentState = PlayState.Stopped;
    public const int D = ((int)0X8000);
    public const int WM_GRAPHNOTIFY = D + 1;
    public IVideoWindow VideoWindow = null;
    IMediaControl MediaControl = null;
    IMediaEventEx MediaEventEx = null;
    IGraphBuilder GraphBuilder = null;
    ICaptureGraphBuilder2 CaptureGraphBuilder = null;
    DsROTEntry rot = null;

    [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
    private static extern void CopyMemory(IntPtr Destination, IntPtr Source, [MarshalAs(UnmanagedType.U4)] int Length);

    private void CaptureVideo()
    {
      int hr = 0;
      IBaseFilter sourceFilter = null;
      try
      {
        GetInterfaces();

        hr = this.CaptureGraphBuilder.SetFiltergraph(this.GraphBuilder);
        //Specifies filter graph "graphbuilder" for the capture graph builder "captureGraphBuilder" to use.
        Debug.WriteLine("Attach the filter graph to the capture graph : " + DsError.GetErrorText(hr));
        DsError.ThrowExceptionForHR(hr);

        sourceFilter = FindCaptureDevice();

        hr = this.GraphBuilder.AddFilter(sourceFilter, "Video Capture");
        Debug.WriteLine("Add capture filter to our graph : " + DsError.GetErrorText(hr));
        DsError.ThrowExceptionForHR(hr);

        hr = this.CaptureGraphBuilder.RenderStream(PinCategory.Preview, MediaType.Video, sourceFilter, null, null);
        Debug.WriteLine("Render the preview pin on the video capture filter : " + DsError.GetErrorText(hr));
        DsError.ThrowExceptionForHR(hr);

        Marshal.ReleaseComObject(sourceFilter);

        SetupVideoWindow();

        rot = new DsROTEntry(this.GraphBuilder);

        hr = this.MediaControl.Run();
        Debug.WriteLine("Start previewing video data : " + DsError.GetErrorText(hr));
        DsError.ThrowExceptionForHR(hr);

        this.CurrentState = PlayState.Running;
        Debug.WriteLine("The currentstate : " + this.CurrentState.ToString());

        //Click
        //DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
        //short iBPP = 24;
        //SetupGraph(capDevices[0], Width, Height, iBPP, Control);
        // tell the callback to ignore new images
        //m_PictureReady = new ManualResetEvent(false);
      }
      catch (Exception ex)
      {
        MessageBox.Show("An unrecoverable error has occurred.With error : " + ex.ToString());
      }
    }

    private void SetupGraph(DsDevice dev, int iWidth, int iHeight, short iBPP, Control hControl)
    {
      int hr;

      IAMVideoControl m_VidControl = null;
      IFilterGraph2 m_FilterGraph = null;
      ISampleGrabber sampGrabber = null;
      IBaseFilter capFilter = null;
      IPin pCaptureOut = null;
      IPin pSampleIn = null;
      IPin pRenderIn = null;

      // Get the graphbuilder object
      m_FilterGraph = new FilterGraph() as IFilterGraph2;

      try
      {
#if DEBUG
        DsROTEntry m_rot = new DsROTEntry(m_FilterGraph);
#endif
        // add the video input device
        hr = m_FilterGraph.AddSourceFilterForMoniker(dev.Mon, null, dev.Name, out capFilter);
        DsError.ThrowExceptionForHR(hr);

        // Find the still pin
        IPin m_pinStill = DsFindPin.ByCategory(capFilter, PinCategory.Still, 0);

        // Didn't find one.  Is there a preview pin?
        if (m_pinStill == null)
        {
          m_pinStill = DsFindPin.ByCategory(capFilter, PinCategory.Preview, 0);
        }

        // Still haven't found one.  Need to put a splitter in so we have
        // one stream to capture the bitmap from, and one to display.  Ok, we
        // don't *have* to do it that way, but we are going to anyway.
        if (m_pinStill == null)
        {
          IPin pRaw = null;
          IPin pSmart = null;

          // There is no still pin
          m_VidControl = null;

          // Add a splitter
          IBaseFilter iSmartTee = (IBaseFilter)new SmartTee();

          try
          {
            hr = m_FilterGraph.AddFilter(iSmartTee, "SmartTee");
            DsError.ThrowExceptionForHR(hr);

            // Find the find the capture pin from the video device and the
            // input pin for the splitter, and connnect them
            pRaw = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0);
            pSmart = DsFindPin.ByDirection(iSmartTee, PinDirection.Input, 0);

            hr = m_FilterGraph.Connect(pRaw, pSmart);
            DsError.ThrowExceptionForHR(hr);

            // Now set the capture and still pins (from the splitter)
            m_pinStill = DsFindPin.ByName(iSmartTee, "Preview");
            pCaptureOut = DsFindPin.ByName(iSmartTee, "Capture");

            // If any of the default config items are set, perform the config
            // on the actual video device (rather than the splitter)
            if (iHeight + iWidth + iBPP > 0)
            {
              //SetConfigParms(pRaw, iWidth, iHeight, iBPP);
            }
          }
          finally
          {
            if (pRaw != null)
            {
              Marshal.ReleaseComObject(pRaw);
            }
            if (pRaw != pSmart)
            {
              Marshal.ReleaseComObject(pSmart);
            }
            if (pRaw != iSmartTee)
            {
              Marshal.ReleaseComObject(iSmartTee);
            }
          }
        }
        else
        {
          // Get a control pointer (used in Click())
          m_VidControl = capFilter as IAMVideoControl;

          pCaptureOut = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0);

          // If any of the default config items are set
          if (iHeight + iWidth + iBPP > 0)
          {
            //SetConfigParms(m_pinStill, iWidth, iHeight, iBPP);
          }
        }

        // Get the SampleGrabber interface
        sampGrabber = new SampleGrabber() as ISampleGrabber;

        // Configure the sample grabber
        IBaseFilter baseGrabFlt = sampGrabber as IBaseFilter;
        //ConfigureSampleGrabber(sampGrabber);
        pSampleIn = DsFindPin.ByDirection(baseGrabFlt, PinDirection.Input, 0);

        // Get the default video renderer
        IBaseFilter pRenderer = new VideoRendererDefault() as IBaseFilter;
        hr = m_FilterGraph.AddFilter(pRenderer, "Renderer");
        DsError.ThrowExceptionForHR(hr);

        pRenderIn = DsFindPin.ByDirection(pRenderer, PinDirection.Input, 0);

        // Add the sample grabber to the graph
        hr = m_FilterGraph.AddFilter(baseGrabFlt, "Ds.NET Grabber");
        DsError.ThrowExceptionForHR(hr);

        /*if (m_VidControl == null)
        {
          // Connect the Still pin to the sample grabber
          hr = m_FilterGraph.Connect(m_pinStill, pSampleIn);
          DsError.ThrowExceptionForHR(hr);

          // Connect the capture pin to the renderer
          hr = m_FilterGraph.Connect(pCaptureOut, pRenderIn);
          DsError.ThrowExceptionForHR(hr);
        }
        else
        {
          // Connect the capture pin to the renderer
          hr = m_FilterGraph.Connect(pCaptureOut, pRenderIn);
          DsError.ThrowExceptionForHR(hr);

          // Connect the Still pin to the sample grabber
          hr = m_FilterGraph.Connect(m_pinStill, pSampleIn);
          DsError.ThrowExceptionForHR(hr);
        }*/

        // Learn the video properties
        //SaveSizeInfo(sampGrabber);
        //ConfigVideoWindow(hControl);

        // Start the graph
        IMediaControl mediaCtrl = m_FilterGraph as IMediaControl;
        hr = mediaCtrl.Run();
        DsError.ThrowExceptionForHR(hr);
      }
      finally
      {
        if (sampGrabber != null)
        {
          Marshal.ReleaseComObject(sampGrabber);
          sampGrabber = null;
        }
        if (pCaptureOut != null)
        {
          Marshal.ReleaseComObject(pCaptureOut);
          pCaptureOut = null;
        }
        if (pRenderIn != null)
        {
          Marshal.ReleaseComObject(pRenderIn);
          pRenderIn = null;
        }
        if (pSampleIn != null)
        {
          Marshal.ReleaseComObject(pSampleIn);
          pSampleIn = null;
        }
      }
    }

    private void GetInterfaces()
    {
      int hr = 0;
      this.GraphBuilder = (IGraphBuilder)new FilterGraph();
      this.CaptureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
      this.MediaControl = (IMediaControl)this.GraphBuilder;
      this.VideoWindow = (IVideoWindow)this.GraphBuilder;
      this.MediaEventEx = (IMediaEventEx)this.GraphBuilder;
      hr = this.MediaEventEx.SetNotifyWindow(Control.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
      //This method designates a window as the recipient of messages generated by or sent to the current DirectShow object
      DsError.ThrowExceptionForHR(hr);
      //ThrowExceptionForHR is a wrapper for Marshal.ThrowExceptionForHR, but additionally provides descriptions for any DirectShow specific error messages.If the hr value is not a fatal error, no exception will be thrown:
      Debug.WriteLine("I started Sub Get interfaces , the result is : " + DsError.GetErrorText(hr));
    }

    private IBaseFilter FindCaptureDevice()
    {
      Debug.WriteLine("Start the Sub FindCaptureDevice");
      int hr = 0;
      UCOMIEnumMoniker classEnum = null;
      UCOMIMoniker[] moniker = new UCOMIMoniker[1];
      object source = null;
      ICreateDevEnum devEnum = (ICreateDevEnum)new CreateDevEnum();
      hr = devEnum.CreateClassEnumerator(FilterCategory.VideoInputDevice, out classEnum, CDef.None);
      Debug.WriteLine("Create an enumerator for the video capture devices : " + DsError.GetErrorText(hr));
      DsError.ThrowExceptionForHR(hr);
      Marshal.ReleaseComObject(devEnum);
      if (classEnum == null)
      {
        throw new ApplicationException("No video capture device was detected.\\r\\n\\r\\n" + "This sample requires a video capture device, such as a USB WebCam,\\r\\n" + "to be installed and working properly.  The sample will now close.");
      }
      int celt = 0;
      if (classEnum.Next(moniker.Length, moniker, out celt) == 0)
      {
        Guid iid = typeof(IBaseFilter).GUID;
        moniker[0].BindToObject(null, null, ref iid, out source);
      }
      else
      {
        throw new ApplicationException("Unable to access video capture device!");
      }
      Marshal.ReleaseComObject(moniker[0]);
      Marshal.ReleaseComObject(classEnum);
      return (IBaseFilter)source;
    }

    private void SetupVideoWindow()
    {
      int hr = 0;
      //set the video window to be a child of the main window
      //putowner : Sets the owning parent window for the video playback window. 
      hr = this.VideoWindow.put_Owner(Control.Handle);
      DsError.ThrowExceptionForHR(hr);

      hr = this.VideoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
      DsError.ThrowExceptionForHR(hr);

      //Use helper function to position video window in client rect of main application window
      ResizeVideoWindow();

      //Make the video window visible, now that it is properly positioned
      //put_visible : This method changes the visibility of the video window. 
      hr = this.VideoWindow.put_Visible(OABool.True);
      DsError.ThrowExceptionForHR(hr);
    }

    public void HandleGraphEvent()
    {
      int hr = 0;
      EventCode evCode = default(EventCode);
      int evParam1 = 0;
      int evParam2 = 0;
      if (this.MediaEventEx == null)
      {
        return;
      }
      while (this.MediaEventEx.GetEvent(out evCode, out evParam1, out evParam2, 0) == 0)
      {
        //// Free event parameters to prevent memory leaks associated with
        //// event parameter data.  While this application is not interested
        //// in the received events, applications should always process them.
        hr = this.MediaEventEx.FreeEventParams(evCode, evParam1, evParam2);
        DsError.ThrowExceptionForHR(hr);

        //// Insert event processing code here, if desired
      }
    }

    private void CloseInterfaces()
    {
      ////stop previewing data
      if ((this.MediaControl != null))
      {
        this.MediaControl.StopWhenReady();
      }

      this.CurrentState = PlayState.Stopped;

      ////stop recieving events
      if ((this.MediaEventEx != null))
      {
        this.MediaEventEx.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);
      }

      //// Relinquish ownership (IMPORTANT!) of the video window.
      //// Failing to call put_Owner can lead to assert failures within
      //// the video renderer, as it still assumes that it has a valid
      //// parent window.
      if ((this.VideoWindow != null))
      {
        this.VideoWindow.put_Visible(OABool.False);
        this.VideoWindow.put_Owner(IntPtr.Zero);
      }

      // // Remove filter graph from the running object table
      if ((rot != null))
      {
        rot.Dispose();
        rot = null;
      }

      //// Release DirectShow interfaces
      if (this.MediaControl != null)
      {
        Marshal.ReleaseComObject(this.MediaControl);
        this.MediaControl = null;
      }

      if (this.MediaEventEx != null)
      {
        Marshal.ReleaseComObject(this.MediaEventEx);
        this.MediaEventEx = null;
      }

      if (this.VideoWindow != null)
      {
        Marshal.ReleaseComObject(this.VideoWindow);
        this.VideoWindow = null;
      }

      if (this.GraphBuilder != null)
      {
        Marshal.ReleaseComObject(this.GraphBuilder);
        this.GraphBuilder = null;
      }

      if (this.CaptureGraphBuilder != null)
      {
        Marshal.ReleaseComObject(this.CaptureGraphBuilder);
        this.CaptureGraphBuilder = null;
      }
    }

    private void ChangePreviewState(bool showVideo)
    {
      int hr = 0;
      //// If the media control interface isn't ready, don't call it
      if (this.MediaControl == null)
      {
        Debug.WriteLine("MediaControl is nothing");
        return;
      }
      if (showVideo == true)
      {
        if (!(this.CurrentState == PlayState.Running))
        {
          Debug.WriteLine("Start previewing video data");
          hr = this.MediaControl.Run();
          this.CurrentState = PlayState.Running;
        }
      }
      else
      {
        Debug.WriteLine("Stop previewing video data");
        hr = this.MediaControl.StopWhenReady();
        this.CurrentState = PlayState.Stopped;
      }
    }

    private void ResizeVideoWindow()
    {
      //Resize the video preview window to match owner window size
      //left , top , width , height
      //if the videopreview is not nothing
      if ((this.VideoWindow != null))
      {
        this.VideoWindow.SetWindowPosition(0, 0, this.Width, this.Height);
      }
    }

    public override void Start()
    {
      base.Start();
      CaptureVideo();
    }

    public override void Stop()
    {
      CloseInterfaces();
      base.Stop();
    }
  }
}
