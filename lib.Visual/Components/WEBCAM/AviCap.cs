using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using lib.Visual.WinAPI;

namespace lib.Visual.Components
{
  #region public class AviCam : System.Windows.Forms.UserControl
  /// <summary>
  /// Summary description for UserControl1.
  /// </summary>
  [ToolboxItem(true), ToolboxBitmap(typeof(AviCap))]
  public class AviCap : CamBase
  {
    #region Type - Delegate e event
    // fired when a new image is captured
    public delegate void ImageCaptured_EventHandler(object sender, Image img);
    public event ImageCaptured_EventHandler ImageCaptured;
    private Image IMageClipboard;
    #endregion

    #region inicialize e destroy
    public AviCap()
    {
      // This call is required by the Windows.Forms Form Designer.
      InitializeComponent();
    }

    /// <summary>
    /// Override the class's finalize method, so we can stop
    /// the video capture on exit
    /// </summary>
    ~AviCap()
    {
      this.Stop();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
          components.Dispose();
      }
      base.Dispose(disposing);
    }
    #endregion

    #region Component Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.TimeToCapture = 100;
      //this.CaptureHeight = 240;
      //this.CaptureWidth = 320;
      this.components = new System.ComponentModel.Container();
      this.Tmr = new System.Windows.Forms.Timer(this.components);
      this.Tmr.Tick += new System.EventHandler(this.Tmr_Tick);
      //this.Name = "WebCamCapture";
      //this.Size = new System.Drawing.Size(342, 252);
    }
    #endregion

    #region Control Properties
    /// <summary>
    /// Variável de ponteiro da câmera
    /// </summary> 
    public IntPtr mCapHwnd { get; set; }
    /// <summary>
    /// Variável da thread de captura
    /// </summary>
    private System.Windows.Forms.Timer Tmr { get; set; }
    /// <summary>
    /// O timer necessita por padrão da criação em um container
    /// </summary>
    private System.ComponentModel.IContainer components { get; set; }
    /// <summary>
    /// The time intervale between frame captures
    /// </summary>
    public int TimeToCapture { get; set; }

    /// <summary>
    /// The height of the video capture image
    /// </summary>
    //public int CaptureHeight { get; set; }

    /// <summary>
    /// The width of the video capture image
    /// </summary>
    //public int CaptureWidth { get; set; }

    /// <summary>
    /// Index do driver da câmera
    /// </summary>
    public int DriverIndex { get; set; }

    /// <summary>
    /// Controle que manipula a camera
    /// </summary>
    //public Control Control { get; set; }

    /// <summary>
    /// Usa o clipboard para gravação das imagens
    /// </summary>
    public bool UseClipboard { get; set; }

    /// <summary>
    /// Status de conexão da câmera
    /// </summary>
    //public bool Connected { get; set; }
    #endregion

    #region Start
    /// <summary>
    /// Starts the video capture
    /// </summary>
    /// <param name="FrameNumber">the frame number to start at. 
    /// Set to 0 to let the control allocate the frame number</param>
    public override void Start()
    {
      #region for safety, call stop, just in case we are already running
      this.Stop();
      #endregion

      #region setup a capture window
      int Flags = 0;
      if (!this.UseClipboard)
      { Flags = IMAGECAPTURE.WS_VISIBLE | IMAGECAPTURE.WS_CHILD; }


      mCapHwnd = AVICAP32.capCreateCaptureWindowA("WebCap",
        Flags, 0, 0,
        this.Width,
        this.Height,
        this.Control.Handle,
        0
      );
      #endregion

      #region connect to the capture device
      Application.DoEvents();

      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DRIVER_CONNECT, DriverIndex, 0);
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_SET_PREVIEWRATE, 66, 0);   //66
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_SET_PREVIEW, 1, 0);

      //USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DLG_VIDEOSOURCE, 0, 0);
      //GetMCIDevice mci = new GetMCIDevice();
      //int i =USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_GET_MCI_DEVICE, Marshal.SizeOf(mci), mci);

      BITMAPINFO bInfo = new BITMAPINFO();
      bInfo.bmiHeader = new BITMAPINFOHEADER();
      bInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(bInfo.bmiHeader);
      bInfo.bmiHeader.biWidth = Width;
      bInfo.bmiHeader.biHeight = Height;
      bInfo.bmiHeader.biPlanes = 1;
      bInfo.bmiHeader.biBitCount = 24; // bits per frame, 24 - RGB
      USER32.SendBitmapMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_SET_VIDEOFORMAT, Marshal.SizeOf(bInfo), ref bInfo);


      CAPTUREPARMS CaptureParams = new CAPTUREPARMS();
      CaptureParams.fYield = 1;
      CaptureParams.fAbortLeftMouse = 0;
      CaptureParams.fAbortRightMouse = 0;
      CaptureParams.dwRequestMicroSecPerFrame = 66667;
      CaptureParams.fMakeUserHitOKToCapture = 0;
      CaptureParams.wPercentDropForError = 10;//10
      CaptureParams.wChunkGranularity = 0;
      CaptureParams.dwIndexSize = 324000;
      CaptureParams.wNumVideoRequested = 10;
      CaptureParams.wNumAudioRequested = 10;
      CaptureParams.fCaptureAudio = 1;
      CaptureParams.fMCIControl = 0;  //0
      CaptureParams.fStepMCIDevice = 0;   //0
      CaptureParams.dwMCIStartTime = 0;
      CaptureParams.dwMCIStopTime = 0;
      CaptureParams.fStepCaptureAt2x = 0;//0
      CaptureParams.wStepCaptureAverageFrames = 5;
      CaptureParams.dwAudioBufferSize = 10;
      USER32.SendMessageA(mCapHwnd, IMAGECAPTURE.WM_CAP_SET_SEQUENCE_SETUP, new IntPtr(Marshal.SizeOf(CaptureParams)), CaptureParams);  
      //ConfigFormat();
      #endregion

      #region set the timer information      
      this.Tmr.Interval = TimeToCapture;
      if (this.UseClipboard)
      { this.Tmr.Start(); }
      #endregion

      base.Start();
    }
    #endregion

    #region Stop
    /// <summary>
    /// Stops the video capture
    /// </summary>
    public override void Stop()
    {
      // stop the timer
      this.Tmr.Stop();

      // disconnect from the video source
      Application.DoEvents();
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DRIVER_DISCONNECT, DriverIndex, 0);
      USER32.DestroyWindow(mCapHwnd);
      base.Stop();
    }
    #endregion

    #region public void StartAvi()
    public void StartAvi()
    {
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_SEQUENCE, 0, 0);
    }
    #endregion

    #region public void SaveAvi(string FileName)
    public void SaveAvi(string FileName)
    {
      USER32.SendMessageA(mCapHwnd, IMAGECAPTURE.WM_CAP_FILE_SAVEAS, IntPtr.Zero, FileName);      
    }
    #endregion

    #region public VideoSource[] GetVideoSource()
    public VideoSource[] GetVideoSource()
    {
      List<VideoSource> lst = new List<VideoSource>();

      int index = 0;
      bool IsDevice = true;

      do
      {
        string vName = "".PadLeft(80, ' ');
        string vVersion = "".PadLeft(80, ' ');
        IsDevice = AVICAP32.capGetDriverDescriptionA(index, ref vName, 80, ref vVersion, 80);

        if (IsDevice)
        { lst.Add(new VideoSource(vName, vVersion)); }

        index++;
      }
      while (IsDevice);

      return lst.ToArray();
    }
    #endregion

    #region Video Capture Code

    /// <summary>
    /// Capture the next frame from the video feed
    /// </summary>
    private void Tmr_Tick(object sender, System.EventArgs e)
    {
      try
      {
        // pause the timer
        this.Tmr.Stop();

        // get the next frame;
        USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_GRAB_FRAME, 0, 0);
        if (this.UseClipboard)
        { SetToClipBoard(); }
      }
      finally
      {
        // restart the timer
        //Application.DoEvents();
        this.Tmr.Start();
      }
    }
    #endregion

    #region public void ConfigVideoDisplay()
    public void ConfigVideoDisplay()
    {
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DLG_VIDEODISPLAY, 0, 0);
    }
    #endregion

    #region public void ConfigFormat()
    public void ConfigVideoFormat()
    {
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DLG_VIDEOFORMAT, 0, 0);
    }
    #endregion

    #region public void ConfigVideoSource()
    public void ConfigVideoSource()
    {
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DLG_VIDEOSOURCE, 0, 0);
    }
    #endregion

    #region public void ConfigVideoCompression()
    public void ConfigVideoCompression() 
    {
      USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_DLG_VIDEOCOMPRESSION, 0, 0);
    }
    #endregion

    #region private void SetToClipBoard()
    private void SetToClipBoard()
    {
      try
      {
        // copy the frame to the clipboard
        USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_EDIT_COPY, 0, 0);
        //USER32.SendMessage(mCapHwnd, IMAGECAPTURE.WM_CAP_FILE_SAVEAS, 0, @"e:\tmp\" + DateTime.Now.ToString("HHmmss"));

        // paste the frame into the event args image

        // get from the clipboard
        IDataObject tempObj = Clipboard.GetDataObject();
        IMageClipboard = (System.Drawing.Bitmap)tempObj.GetData(System.Windows.Forms.DataFormats.Bitmap);
        // raise the event
        if (ImageCaptured != null)
        {
          this.ImageCaptured(this, IMageClipboard.GetThumbnailImage(Width, Height, null, System.IntPtr.Zero));
        }
        Clipboard.Clear();
      }
      catch { }
    }
    #endregion
  }
  #endregion
}
