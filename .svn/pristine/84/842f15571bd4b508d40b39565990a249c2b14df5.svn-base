using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace lib.Visual.WinAPI
{
  [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
  public struct SYSTEMTIME
  {
    public ushort Year;
    public ushort Month;
    public ushort DayOfWeek;
    public ushort Day;
    public ushort Hour;
    public ushort Minute;
    public ushort Second;
    public ushort Milliseconds;
  }

  public class KERNEL32
  {    
    [DllImport("kernel32.dll", EntryPoint = "SetLocalTime", SetLastError = true)]
    public static extern int SetLocalTime(ref SYSTEMTIME lpSystemTime);
    [DllImport("kernel32.dll", EntryPoint = "GetLocalTime", SetLastError = true)]
    public static extern int GetLocalTime(ref SYSTEMTIME lpSystemTime);
  }
}
