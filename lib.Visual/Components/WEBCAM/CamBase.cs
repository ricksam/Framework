using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace lib.Visual.Components
{
  public class CamBase : Component
  {
    public CamBase() 
    {

    }

    public bool Connected = false;
    public Control Control { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public virtual void Start()
    { 
      this.Connected = true; 
    }

    public virtual void Stop()
    { 
      this.Connected = false; 
    }

    public Bitmap GetCurrentImage()
    {
      return lib.Visual.Functions.GetImageFromHandle(Control.Handle, Width - 2, Height - 2);
    }
  }
}
