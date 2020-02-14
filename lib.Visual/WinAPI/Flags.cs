using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace lib.Visual.WinAPI
{
  #region public struct MOUSEEVENT
  [StructLayout(LayoutKind.Sequential)]
  public struct MOUSEEVENT
  {
    public const uint LEFTDOWN = 0x00000002;
    public const uint LEFTUP = 0x00000004;
    public const uint MIDDLEDOWN = 0x00000020;
    public const uint MIDDLEUP = 0x00000040;
    public const uint MOVE = 0x00000001;
    public const uint ABSOLUTE = 0x00008000;
    public const uint RIGHTDOWN = 0x00000008;
    public const uint RIGHTUP = 0x00000010;
  }
  #endregion

  #region public struct SCREENCAPTURE
  [StructLayout(LayoutKind.Sequential)]
  public struct SCREENCAPTURE
  {
    public const int SM_CXSCREEN = 0;
    public const int SM_CYSCREEN = 1;    
  }
  #endregion

  #region public struct CAPTUREPARMS
  [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
  public struct CAPTUREPARMS
  {
    public UInt32 dwRequestMicroSecPerFrame;
    public Int32 fMakeUserHitOKToCapture;
    public UInt32 wPercentDropForError;
    public Int32 fYield;
    public UInt32 dwIndexSize;
    public UInt32 wChunkGranularity;
    public Int32 fCaptureAudio;
    public UInt32 wNumVideoRequested;
    public UInt32 wNumAudioRequested;
    public Int32 fAbortLeftMouse;
    public Int32 fAbortRightMouse;
    public Int32 fMCIControl;
    public Int32 fStepMCIDevice;
    public UInt32 dwMCIStartTime;
    public UInt32 dwMCIStopTime;
    public Int32 fStepCaptureAt2x;
    public UInt32 wStepCaptureAverageFrames;
    public UInt32 dwAudioBufferSize;
    public void SetParams(System.Int32 fYield, System.Int32 fAbortLeftMouse, System.Int32 fAbortRightMouse, System.UInt32 dwRequestMicroSecPerFrame, System.Int32 fMakeUserHitOKToCapture,
    System.UInt32 wPercentDropForError, System.UInt32 dwIndexSize, System.UInt32 wChunkGranularity, System.UInt32 wNumVideoRequested, System.UInt32 wNumAudioRequested, System.Int32 fCaptureAudio, System.Int32 fMCIControl,
    System.Int32 fStepMCIDevice, System.UInt32 dwMCIStartTime, System.UInt32 dwMCIStopTime, System.Int32 fStepCaptureAt2x, System.UInt32 wStepCaptureAverageFrames, System.UInt32 dwAudioBufferSize)
    {
      this.dwRequestMicroSecPerFrame = dwRequestMicroSecPerFrame;
      this.fMakeUserHitOKToCapture = fMakeUserHitOKToCapture;
      this.fYield = fYield;
      this.wPercentDropForError = wPercentDropForError;
      this.dwIndexSize = dwIndexSize;
      this.wChunkGranularity = wChunkGranularity;
      this.wNumVideoRequested = wNumVideoRequested;
      this.wNumAudioRequested = wNumAudioRequested;
      this.fCaptureAudio = fCaptureAudio;
      this.fAbortLeftMouse = fAbortLeftMouse;
      this.fAbortRightMouse = fAbortRightMouse;
      this.fMCIControl = fMCIControl;
      this.fStepMCIDevice = fStepMCIDevice;
      this.dwMCIStartTime = dwMCIStartTime;
      this.dwMCIStopTime = dwMCIStopTime;
      this.fStepCaptureAt2x = fStepCaptureAt2x;
      this.wStepCaptureAverageFrames = wStepCaptureAverageFrames;
      this.dwAudioBufferSize = dwAudioBufferSize;
    }
  }
  #endregion

  #region public struct IMAGECAPTURE
  [StructLayout(LayoutKind.Sequential)]
  public struct IMAGECAPTURE 
  {
    public const int SRCCOPY = 13369376;
    public const int WM_USER = 1024;

    public const int WM_CAP_START = WM_USER;
    public const int WM_CAP_GET_CAPSTREAMPTR = WM_CAP_START + 1;
    public const int WM_CAP_SET_CALLBACK_ERROR = WM_CAP_START + 2;
    public const int WM_CAP_SET_CALLBACK_STATUS = WM_CAP_START + 3;
    public const int WM_CAP_SET_CALLBACK_YIELD = WM_CAP_START + 4;
    public const int WM_CAP_SET_CALLBACK_FRAME = WM_CAP_START + 5;
    public const int WM_CAP_SET_CALLBACK_VIDEOSTREAM = WM_CAP_START + 6;
    public const int WM_CAP_SET_CALLBACK_WAVESTREAM = WM_CAP_START + 7;
    public const int WM_CAP_GET_USER_DATA = WM_CAP_START + 8;
    public const int WM_CAP_SET_USER_DATA = WM_CAP_START + 9;

    public const int WM_CAP_DRIVER_CONNECT = WM_CAP_START + 10;
    public const int WM_CAP_DRIVER_DISCONNECT = WM_CAP_START + 11;
    public const int WM_CAP_DRIVER_GET_NAME = WM_CAP_START + 12;
    public const int WM_CAP_DRIVER_GET_VERSION = WM_CAP_START + 13;
    public const int WM_CAP_DRIVER_GET_CAPS = WM_CAP_START + 14;

    public const int WM_CAP_FILE_SET_CAPTURE_FILE = WM_CAP_START + 20;
    public const int WM_CAP_FILE_GET_CAPTURE_FILE = WM_CAP_START + 21;
    public const int WM_CAP_FILE_ALLOCATE = WM_CAP_START + 22;
    public const int WM_CAP_FILE_SAVEAS = WM_CAP_START + 23;
    public const int WM_CAP_FILE_SET_INFOCHUNK = WM_CAP_START + 24;
    public const int WM_CAP_FILE_SAVEDIB = WM_CAP_START + 25;

    public const int WM_CAP_EDIT_COPY = WM_CAP_START + 30;

    public const int WM_CAP_SET_AUDIOFORMAT = WM_CAP_START + 35;
    public const int WM_CAP_GET_AUDIOFORMAT = WM_CAP_START + 36;

    public const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP_START + 41;
    public const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP_START + 42;
    public const int WM_CAP_DLG_VIDEODISPLAY = WM_CAP_START + 43;
    public const int WM_CAP_GET_VIDEOFORMAT = WM_CAP_START + 44;
    public const int WM_CAP_SET_VIDEOFORMAT = WM_CAP_START + 45;
    public const int WM_CAP_DLG_VIDEOCOMPRESSION = WM_CAP_START + 46;

    public const int WM_CAP_SET_PREVIEW = WM_CAP_START + 50;
    public const int WM_CAP_SET_OVERLAY = WM_CAP_START + 51;
    public const int WM_CAP_SET_PREVIEWRATE = WM_CAP_START + 52;
    public const int WM_CAP_SET_SCALE = WM_CAP_START + 53;
    public const int WM_CAP_GET_STATUS = WM_CAP_START + 54;
    public const int WM_CAP_SET_SCROLL = WM_CAP_START + 55;

    public const int WM_CAP_GRAB_FRAME = WM_CAP_START + 60;
    public const int WM_CAP_GRAB_FRAME_NOSTOP = WM_CAP_START + 61;

    public const int WM_CAP_SEQUENCE = WM_CAP_START + 62;
    public const int WM_CAP_SEQUENCE_NOFILE = WM_CAP_START + 63;
    public const int WM_CAP_SET_SEQUENCE_SETUP = WM_CAP_START + 64;
    public const int WM_CAP_GET_SEQUENCE_SETUP = WM_CAP_START + 65;
    public const int WM_CAP_SET_MCI_DEVICE = WM_CAP_START + 66;
    public const int WM_CAP_GET_MCI_DEVICE = WM_CAP_START + 67;
    public const int WM_CAP_STOP = WM_CAP_START + 68;
    public const int WM_CAP_ABORT = WM_CAP_START + 69;

    public const int WM_CAP_SINGLE_FRAME_OPEN = WM_CAP_START + 70;
    public const int WM_CAP_SINGLE_FRAME_CLOSE = WM_CAP_START + 71;
    public const int WM_CAP_SINGLE_FRAME = WM_CAP_START + 72;

    public const int WM_CAP_PAL_OPEN = WM_CAP_START + 80;
    public const int WM_CAP_PAL_SAVE = WM_CAP_START + 81;
    public const int WM_CAP_PAL_PASTE = WM_CAP_START + 82;
    public const int WM_CAP_PAL_AUTOCREATE = WM_CAP_START + 83;
    public const int WM_CAP_PAL_MANUALCREATE = WM_CAP_START + 84;

    public const int WS_CHILD = 0x40000000;
    public const int WS_VISIBLE = 0x10000000;

    // Following added post VFW 1.1
    public const int WM_CAP_SET_CALLBACK_CAPCONTROL = WM_CAP_START + 85;

    // Defines end of the message range
    public const int WM_CAP_END = WM_CAP_SET_CALLBACK_CAPCONTROL;
  }
  #endregion

  #region public struct BITMAPINFOHEADER
  [StructLayout(LayoutKind.Sequential)]
  public struct BITMAPINFOHEADER
  {
    public uint biSize;
    public int biWidth;
    public int biHeight;
    public ushort biPlanes;
    public ushort biBitCount;
    public uint biCompression;
    public uint biSizeImage;
    public int biXPelsPerMeter;
    public int biYPelsPerMeter;
    public uint biClrUsed;
    public uint biClrImportant;
  }
  #endregion

  #region public struct BITMAPINFO
  [StructLayout(LayoutKind.Sequential)]
  public struct BITMAPINFO
  {
    public BITMAPINFOHEADER bmiHeader;
    public int bmiColors;
  }
  #endregion

  /*[StructLayout(LayoutKind.Sequential)]
  public struct GetMCIDevice 
  {
    public IntPtr LpVoid;
    public byte[] LPSTR; 
    public char[] lpszName;
  }*/
}
