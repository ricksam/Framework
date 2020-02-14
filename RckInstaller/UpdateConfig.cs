using System;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace RckInstaller
{
  public class UpdateConfig:Configuration
  {
    private UpdateConfig() 
    {
    }

    public UpdateConfig(string FileName) : base(FileName) 
    {
      this.LinkVersao = "";
      this.LinkUpdate = "";
      this.AppLocal = "";
      this.AppParams = "";
      this.AppText = "";
      this.AppSplash = "";
      this.InitialTime = 0;
    }

    public string LinkVersao { get; set; }
    public string LinkUpdate { get; set; }
    public string AppLocal { get; set; }
    public string AppParams { get; set; }
    public string AppText { get; set; }
    public string AppSplash { get; set; }
    public int InitialTime { get; set; }
  }
}
