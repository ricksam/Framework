using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace lib.Visual.WinAPI
{
  public static class USER32
  {
    // Mouse Event
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    // Desktop windows
    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    public static extern IntPtr GetDesktopWindow();
    
    [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
    public static extern int GetSystemMetrics(int abc);

    // Capture o Device Context
    [DllImport("user32.dll", EntryPoint = "GetDC")]
    public static extern IntPtr GetDC(IntPtr ptr);

    [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
    public static extern IntPtr GetWindowDC(Int32 ptr);

    [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
    public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

    // Message
    [DllImport("user32.dll", EntryPoint = "SendMessage")]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    //[DllImport("user32.dll", EntryPoint = "SendMessage")]
    //public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);
    
    //[DllImport("user32", EntryPoint = "SendMessageA", SetLastError = true)]
    //public static extern int SendMessageA(int webcam1, int Msg, IntPtr wParam, ref CAPTUREPARMS lParam);

    [DllImport("user32", EntryPoint = "SendMessageA")]
    public static extern int SendMessageA(IntPtr hwnd, int wMsg, IntPtr wParam, [MarshalAs(UnmanagedType.AsAny)] object lParam);

    //Bitmap
    [DllImport("user32", EntryPoint = "SendMessage")]
    public static extern int SendBitmapMessage(IntPtr hWnd, uint wMsg, int wParam, ref BITMAPINFO lParam);

    [DllImport("user32", EntryPoint = "DestroyWindow")]
    public static extern bool DestroyWindow(IntPtr hWnd);

    public const byte ModAlt = 1, ModControl = 2, ModShift = 4, ModWin = 8;

    [DllImport("user32.dll")]
    public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

    [DllImport("user32.dll")]
    public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    //[DllImport("user32", EntryPoint = "SendMessage")]
    //static extern int SendHeaderMessage(int hWnd, uint wMsg, int wParam, CallBackDelegate lParam);
  }

  public enum KeyModifier
  {
    None = 0,
    Alt = 1,
    Control = 2,
    Shift = 4,
    WinKey = 8
  }
}
