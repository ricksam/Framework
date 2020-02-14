using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace lib.Class
{
  public class Sound
  {
    [DllImport("WinMM.dll")]
    private static extern bool PlaySound(string fname, int Mod, int flag);

    // these are the SoundFlags we are using here, check mmsystem.h for more
    private static int SND_ASYNC = 0x0001; // play asynchronously
    private static int SND_FILENAME = 0x00020000; // use file name
    private static int SND_PURGE = 0x0040; // purge non-static events

    public static void Play(string FileName)
    { PlaySound(FileName, 0, SND_FILENAME | SND_ASYNC); }

    public static void Stop()
    { PlaySound(null, 0, SND_PURGE); }
  }
}
