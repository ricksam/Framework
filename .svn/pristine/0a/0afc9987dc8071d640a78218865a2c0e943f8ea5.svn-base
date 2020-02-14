using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using lib.Visual.WinAPI;

namespace lib.Visual
{
  public enum enmMouseButton { Left, Right }
  public static class Mouse
  {   
    public static void Click(enmMouseButton Button)
    {
      if (Button == enmMouseButton.Left)
      {
        USER32.mouse_event((uint)MOUSEEVENT.LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        USER32.mouse_event((uint)MOUSEEVENT.LEFTUP, 0, 0, 0, UIntPtr.Zero);
      }
      else 
      {
        USER32.mouse_event((uint)MOUSEEVENT.RIGHTDOWN, 0, 0, 0, UIntPtr.Zero);
        USER32.mouse_event((uint)MOUSEEVENT.RIGHTUP, 0, 0, 0, UIntPtr.Zero);
      }
    }
        
    public static void Move(int x, int y)
    { USER32.SetCursorPos(x, y); }
  }

  

  
}
