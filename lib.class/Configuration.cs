using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace lib.Class
{
  [Serializable]
  [System.Xml.Serialization.XmlInclude(typeof(lib.Class.Configuration))]
  public class Configuration
  {
    #region Constructor
    public Configuration()
    {
      setConfiguration(Class.SerializeFormat.Xml, "");
    }

    public Configuration(String FileName)
    {
      setConfiguration(Class.SerializeFormat.Xml, FileName);
    }

    public Configuration(lib.Class.SerializeFormat format, String FileName)
    {
      setConfiguration(format, FileName);
    }
    #endregion

    private void setConfiguration(lib.Class.SerializeFormat format, String FileName) 
    {
      this.SerializeFormat = format;
      this.FileName = FileName;
    }

    #region Fields
    public String FileName { get; set; }
    public SerializeFormat SerializeFormat { get; set; }
    #endregion

    #region public bool Open()
    public virtual bool Open()
    {
      if (File.Exists(FileName))
      {
        Configuration cfg = new Configuration();
        Serialization srl = new Serialization(SerializeFormat);
        object arq = srl.Deserialize(FileName, this.GetType());

        ObjectAttribute objThis = new ObjectAttribute(this);
        ObjectAttribute objArq = new ObjectAttribute(arq);
        ObjectAttribute objCfg = new ObjectAttribute(cfg);

        string[] Atr = objThis.GetAttributes();

        for (int i = 0; i < Atr.Length; i++)
        {
          //if (!objCfg.AttibuteExists(Atr[i]))
          //{ 
            objThis.SetAttibute(Atr[i], objArq.GetAttribute(Atr[i])); 
          //}
        }
        return true;
      }
      return false;
    }
    #endregion

    #region public void Save()
    public virtual bool Save()
    {
      Serialization srl = new Serialization(SerializeFormat);
      return srl.Serialize(FileName, this);
    }
    #endregion
  }
}
