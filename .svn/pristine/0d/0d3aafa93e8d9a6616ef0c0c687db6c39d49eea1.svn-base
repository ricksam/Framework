using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using lib.Class;

namespace lib.Visual.Components
{
  public static class Resources
  {
    private static Template _skin = new Template();
    public static Template Skin { get { return _skin; } set { _skin = value; } }

    public static void Edit() 
    {
      frmEditTemplate edt = new frmEditTemplate();
      edt.Exec();
    }
  }

  #region public class Template :lib.Class.Configuration
  [Serializable]
  public class Template :lib.Class.Configuration
  {
    public Template():base()//base(Application.StartupPath + "\\Skin\\Default.skin")
    {
      this.Containers = new TplContainers();
      this.Buttons = new TplButtons();
      this.Controls = new TplControls();
      this.Labels = new TplLabel();
      this.SerializeFormat = SerializeFormat.Bin;
      Open();      
    }

    public bool Enabled { get; set; }
    public TplContainers Containers { get; set; }
    public TplButtons Buttons { get; set; }
    public TplControls Controls { get; set; }    
    public TplLabel Labels { get; set; }
  }
  #endregion

  #region public class TplContainers
  [Serializable]
  public class TplContainers 
  {
    public TplContainers() 
    {
      TabImageBack = "";
      ImageBack = "";
      BackColor = SystemColors.Control;
      ButtonAreaBackColor = SystemColors.Control;
    }

    public string TabImageBack { get; set; }
    public string ImageBack { get; set; }
    public Color BackColor { get; set; }
    public Color ButtonAreaBackColor { get; set; }
  }
  #endregion

  #region public class TplControls
  [Serializable]
  public class TplControls 
  {
    public TplControls()
    {
      Font = new System.Drawing.Font(
        "Microsoft Sans Serif",
        8.25F,
        FontStyle.Regular,
        System.Drawing.GraphicsUnit.Point,
        ((byte)(0))
      );

      ColumnBackColor = SystemColors.Control;
      GridBackColor = SystemColors.AppWorkspace;
      ImageGroupBok = "";
      ImageProgressBar = "";
      BackColor = SystemColors.Window;
      BackColorInactive = SystemColors.Control;
      ForeColor = SystemColors.WindowText;
      BorderColor = Color.Black;
    }

    public Font Font { get; set; }
    public String ImageGroupBok { get; set; }
    public String ImageProgressBar { get; set; }
    public Color ColumnBackColor { get; set; }
    public Color GridBackColor { get; set; }
    public Color BackColorInactive { get; set; }
    public Color BackColor { get; set; }
    public Color ForeColor { get; set; }    
    public Color BorderColor { get; set; }
  }
  #endregion

  #region public class TplLabel
  [Serializable]
  public class TplLabel 
  {
    public TplLabel()
    {
      Font = new System.Drawing.Font(
        "Microsoft Sans Serif",
        8.25F,
        FontStyle.Regular,
        System.Drawing.GraphicsUnit.Point,
        ((byte)(0))
      );

      Transparent = false;
      BackColor = Color.Transparent;
      ForeColor = SystemColors.ControlText;
      BorderColor = Color.Black;
      BorderStyle = BorderStyle.None;
    }

    public bool Transparent { get; set; }
    public Font Font { get; set; }
    public Color BackColor { get; set; }
    public Color ForeColor { get; set; }
    public Color BorderColor { get; set; }
    public BorderStyle BorderStyle { get; set; }
  }
  #endregion

  #region public class TplButtons
  [Serializable]
  public class TplButtons 
  {
    public TplButtons() 
    {
      ImageBack = "";
      ImageHover = "";
      ImageDown = "";
      BorderColor = SystemColors.Control;
      ForeColor = SystemColors.ControlText;
      Font = new System.Drawing.Font(
        "Microsoft Sans Serif",
        8.25F,
        FontStyle.Regular,
        System.Drawing.GraphicsUnit.Point,
        ((byte)(0))
      );
    }

    public string ImageBack { get; set; }
    public string ImageHover { get; set; }
    public string ImageDown { get; set; }
    public Color BorderColor { get; set; }
    public Color ForeColor { get; set; }
    public Font Font { get; set; }
  }
  #endregion
}
