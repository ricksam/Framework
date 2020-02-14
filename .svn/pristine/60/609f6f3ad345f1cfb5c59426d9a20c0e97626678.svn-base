using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace lib.Class
{
  #region public enum SerializeFormat
  public enum SerializeFormat
  {
    Bin,
    Xml
  }
  #endregion

  #region public class Serialization
  public class Serialization
  {
    #region Constructor
    public Serialization(SerializeFormat Format) 
    {
      this.Format = Format;
    }
    #endregion

    #region Fields
    private SerializeFormat Format { get; set; }
    #endregion

    #region Methods
    #region public void setFormat(SerializeFormats format)
    public void setFormat(SerializeFormat format)
    { Format = format; }
    #endregion

    #region public bool Serialize(string aFileName, object aObjInstance)
    public bool Serialize(string aFileName, object aObjInstance)
    { return Serialize(aFileName, aObjInstance, aObjInstance.GetType()); }

    public bool Serialize(string aFileName, object aObjInstance, Type Type)
    {
      #region Verifica Existencia do arquivos
      if (File.Exists(aFileName))
      { File.Delete(aFileName); }
      #endregion

      #region O objeto será serializado
      FileStream fs = new FileStream(aFileName, FileMode.OpenOrCreate); // Cria o arquivo

      try
      {
        if (Format == SerializeFormat.Bin)
        {
          BinaryFormatter sf = new BinaryFormatter(); // Cria o formato da serialização
          sf.Serialize(fs, aObjInstance); // Serializa o objeto
        } // if (aFormat == SerializeFormats.Bin)
        else
        {
          //SoapFormatter sf = new SoapFormatter();
          XmlSerializer sf = new XmlSerializer(Type);
          sf.Serialize(fs, aObjInstance);
        } // if (aFormat == SerializeFormats.Xml)
      } // try            
      finally
      { fs.Close(); }      
      #endregion
      return File.Exists(aFileName);
    } 
    #endregion

    #region public object Deserialize(string aFileName)
    public T Deserialize<T>(string aFileName)
    {
      return (T)Deserialize(aFileName, typeof(T));
    }

    public object Deserialize(string aFileName, Type Tp) 
    {
      // O objeto será deserializado
      FileStream fs = new FileStream(aFileName, FileMode.Open); // Abre o arquivo

      try
      {
        if (Format == SerializeFormat.Bin)
        {
          BinaryFormatter sf = new BinaryFormatter(); // Cria o formato de deserialização
          return sf.Deserialize(fs); // Carrega as palavras
        } // if (aFormat == SerializeFormats.Bin)
        else
        {
          //SoapFormatter sf = new SoapFormatter();
          XmlSerializer sf = new XmlSerializer(Tp);
          return sf.Deserialize(fs); // Carrega as palavras
        } // if (aFormat == SerializeFormats.Xml)
      } // try  
      finally
      { fs.Close(); }
    }
    #endregion
    #endregion
  }
  #endregion
}
