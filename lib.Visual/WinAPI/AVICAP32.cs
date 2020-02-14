using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace lib.Visual
{
  public static class AVICAP32
  {
    [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindowA")]
    public static extern IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hwndParent, int nID);

    [DllImport("avicap32.dll", EntryPoint = "capGetDriverDescriptionA")]
    public static extern bool capGetDriverDescriptionA(int wDriverIndex,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszName, int cbName,
      [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);
  }

  public class VideoSource
  {
    public VideoSource(string Name, string Version)
    {
      this.Name = Name.Replace("\0", "");
      this.Version = Version.Replace("\0", "");
    }

    public string Name { get; set; }
    public string Version { get; set; }

    public override string ToString()
    {
      if(this.Name.IndexOf(' ')!=-1)
      { return this.Name.Substring(0, this.Name.IndexOf(' ')); }
      else 
      { return this.Name + "_" + this.Version; }
    }
  }
}
