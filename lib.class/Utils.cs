﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management;

namespace lib.Class
{
  public static class Utils
  {
    public static Conversion GetAppConfig( string ConfigName, ConfigurationType type = ConfigurationType.appSettings)
    {
      try
      {
          if (type == ConfigurationType.appSettings) {
              return new Conversion(System.Configuration.ConfigurationManager.AppSettings[ConfigName]);
          }
          if (type == ConfigurationType.connectionStrings) {
              return new Conversion(System.Configuration.ConfigurationManager.ConnectionStrings[ConfigName]);
          }
          else {
              return new Conversion(System.Configuration.ConfigurationManager.AppSettings[ConfigName]);
          }
      }
      catch { return new Conversion(""); }
    }

    #region public static bool IsValidCPF(string CPF)
    /// <summary>
    /// Esta função verifica se o número de CPF passado por parâmetro é válido
    /// </summary>
    /// <param name="CPF"></param>
    /// <returns></returns>
    public static bool IsValidCPF(string CPF)
    {
      if (string.IsNullOrEmpty(CPF))
      { return false; }

      int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      string tempCpf;
      string digito;
      int soma;
      int resto;

      CPF = CPF.Trim();
      CPF = CPF.Replace(".", "").Replace("-", "");

      if (CPF.Length != 11)
      { return false; }

      tempCpf = CPF.Substring(0, 9);
      soma = 0;
      for (int i = 0; i < 9; i++)
      { soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i]; }

      resto = soma % 11;
      if (resto < 2)
      { resto = 0; }
      else
      { resto = 11 - resto; }

      digito = resto.ToString();
      tempCpf = tempCpf + digito;

      soma = 0;
      for (int i = 0; i < 10; i++)
      { soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i]; }

      resto = soma % 11;
      if (resto < 2)
      { resto = 0; }
      else
      { resto = 11 - resto; }

      digito = digito + resto.ToString();
      return CPF.EndsWith(digito);
    }
    #endregion

    #region public static bool IsValidCnpj(string CNPJ)
    /// <summary>
    /// Esta função verifica se o número de CNPJ passado por parâmetro é válido
    /// </summary>
    /// <param name="CNPJ"></param>
    /// <returns></returns>
    public static bool IsValidCnpj(string CNPJ)
    {
      if (string.IsNullOrEmpty(CNPJ))
      { return false; }

      int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

      int soma;
      int resto;
      string digito;
      string tempCnpj;

      CNPJ = CNPJ.Trim();
      CNPJ = CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");

      if (CNPJ.Length != 14)
      { return false; }

      tempCnpj = CNPJ.Substring(0, 12);

      soma = 0;
      for (int i = 0; i < 12; i++)
      { soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i]; }

      resto = (soma % 11);
      if (resto < 2)
      { resto = 0; }
      else
      { resto = 11 - resto; }

      digito = resto.ToString();
      tempCnpj = tempCnpj + digito;

      soma = 0;
      for (int i = 0; i < 13; i++)
      { soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i]; }

      resto = (soma % 11);
      if (resto < 2)
      { resto = 0; }
      else
      { resto = 11 - resto; }

      digito = digito + resto.ToString();
      return CNPJ.EndsWith(digito);
    }
    #endregion

    #region public static bool IsValidPis(string PIS)
    /// <summary>
    /// Esta função verifica se o número de PIS passado por parâmetro é válido
    /// </summary>
    /// <param name="pis"></param>
    /// <returns></returns>
    public static bool IsValidPis(string PIS)
    {
      if (string.IsNullOrEmpty(PIS))
      { return false; }

      int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int soma;
      int resto;

      if (PIS.Trim().Length == 0)
      { return false; }

      PIS = PIS.Trim();
      PIS = PIS.Replace("-", "").Replace(".", "").PadLeft(11, '0');

      soma = 0;
      for (int i = 0; i < 10; i++)
      { soma += int.Parse(PIS[i].ToString()) * multiplicador[i]; }

      resto = soma % 11;

      if (resto < 2)
      { resto = 0; }
      else
      { resto = 11 - resto; }

      return PIS.EndsWith(resto.ToString());
    }
    #endregion

    public static bool IsValidEmail(string Email) 
    {
      if (string.IsNullOrEmpty(Email))
      { return false; }

      bool arroba = false;
      bool ponto = false;
      for (int i = 1; i < Email.Length; i++)
      {
        if (Email[i] == '@')
        { arroba = true; }

        if (Email[i] == '.')
        { ponto = true; }
      }
      return arroba && ponto;
    }

    public static bool IsValidCarPlate(string CarPlate)
    {
        return CarPlate == SetMask(CarPlate, "AAA0000");
    }

    #region public static string GetLocalIP()
    public static string[] GetLocalIP()
    {
      ManagementClass _mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
      ManagementObjectCollection _moc = _mc.GetInstances();
      List<string> lIp = new List<string>();

      ManagementObject moc = new ManagementObject();

      foreach (ManagementObject moc_ in _moc)
      {
        if (!(bool)moc_["ipEnabled"])
        { continue; }

        string[] enderecoIP = (string[])moc_["IPAddress"];
        foreach (string sIP in enderecoIP)
        {
          if (sIP != "0.0.0.0")
          { lIp.Add(sIP); }
        }
      }
      if (lIp.Count == 0)
      { return new string[] { "0.0.0.0" }; }
      else
      { return lIp.ToArray(); }
    }
    #endregion

    #region public static string SetMask(string Number, string Mask)
    public static string SetMask(string Number, string Mask)
    {
      string Resp = "";

      Number = GetNumbersOrLetters(Number);

      for (int i = 0, n = 0; i < Mask.Length && n < Number.Length; i++)
      {
        if (char.IsLetter(Mask[i]) || char.IsNumber(Mask[i]))
        {
          Resp += Number[n].ToString();
          n++;
        }
        else { Resp += Mask[i].ToString(); }
      }
      return Resp;
    }
    #endregion

    #region public static string GetVersion()
    public static string GetVersion() 
    {
      System.Reflection.Assembly entryPoint = System.Reflection.Assembly.GetEntryAssembly();
      System.Reflection.AssemblyName entryPointName = entryPoint.GetName();
      return entryPointName.Version.ToString();
    }
    #endregion

    public static Machine GetMachine() 
    {
        Machine machine = new Machine();

        ManagementObjectSearcher searcher =
         new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
        foreach (ManagementObject queryObj in searcher.Get())
        {
            machine.Architecture = queryObj["Architecture"].ToString(); 
            machine.Caption = queryObj["Caption"].ToString(); 
            machine.Family = queryObj["Family"].ToString(); 
            machine.ProcessorId = queryObj["ProcessorId"].ToString();
        }

        /*searcher =
         new ManagementObjectSearcher("root\\CIMV2", "Select * FROM Win32_NetworkAdapterConfiguration");
        foreach (ManagementObject queryObj in searcher.Get())
        {
            machine.MacAddress = queryObj["MacAddress"].ToString();    
        }*/

        return machine;
    }

    

    public static double TimeZone 
    {
      get { return DateTime.Now.Subtract(DateTime.UtcNow).TotalHours; }
    }

    public static string GetResultFromCharacterMap(ushort[] v, string m)
    {
      string r = "";
      for (int i = (v.Length - 1); i >= 0; i--)
      { r += m[v[i]].ToString(); }
      return r;
    }

    public static string GetTimeMap(DateTime d)
    {
      string map = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
      ulong u_long_date = Convert.ToUInt64(d.ToString("yyyyMMddHHmmssfff"));
      ushort u_base = Convert.ToUInt16(map.Length);
      ushort[] array = Conversion.DecimalBaseForBase(u_long_date, u_base);
      return GetResultFromCharacterMap(array, map);
    }

    public static string GetToken()
    {
      return lib.Class.Utils.GetTimeMap(DateTime.UtcNow);
    }

    public static string[] GenerateCodes(int start, byte delta, int quantity) 
    {
      string char_map = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      return GenerateCodes(char_map, start, delta, quantity);
    }

    public static string[] GenerateCodes(string char_map, int start, byte delta, int quantity)
    {
      List<string> codes = new List<string>();
      ushort bs = (ushort)char_map.Length;
      for (int i = 0; i < quantity; i++)
      {
        ulong val = (ulong)((i + 1) * delta) + (ulong)start;
        ushort[] values = lib.Class.Conversion.DecimalBaseForBase(val, bs);
        codes.Add(lib.Class.Utils.GetResultFromCharacterMap(values, char_map));
      }
      return codes.ToArray();
    }

    public static string GetNumbers(string s) 
    {
        if (string.IsNullOrEmpty(s))
        { return ""; }

      string r = "";
      for (int i = 0; i < s.Length; i++)
      {
        if (char.IsNumber(s[i]))
        { r += s[i]; }
      }
      return r;
    }

    public static string GetNumbersOrLetters(string s)
    {
      string r = "";
      for (int i = 0; i < s.Length; i++)
      {
        if (char.IsNumber(s[i]) || char.IsLetter(s[i]))
        { r += s[i]; }
      }
      return r;
    }

    public static DateTime StartGregorianCalendar() 
    {
      return Convert.ToDateTime("1753-01-01");
    }

    public static DateTime BrazilianDatetimeNow()
    {
        var info = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        DateTimeOffset localServerTime = DateTimeOffset.Now;
        DateTimeOffset brazilTime = TimeZoneInfo.ConvertTime(localServerTime, info);
        return Convert.ToDateTime(brazilTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }

    public static int BrazilianTimeZone()
    {
        var info = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        return info.BaseUtcOffset.Hours;
    }
  }

  public enum ConfigurationType { appSettings, connectionStrings }

  public class Machine
  {
      public string Architecture { get; set; }
      public string Caption { get; set; }
      public string Family { get; set; }
      public string ProcessorId { get; set; }
      public string MacAddress { get; set; }
  }
  
}
