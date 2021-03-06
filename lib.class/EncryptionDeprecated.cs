﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace lib.Class
{
  public class EncryptionDeprecated
  {
    #region Constructor
    public EncryptionDeprecated(string Key) 
    {
      this.Key = EncryptionDeprecated.GetSHA1(Key);
      Cnv = new Conversion();
      Chars = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüý";
    }
    #endregion

    #region Constructor
    public EncryptionDeprecated(string Key, string CharacterMap)
    {
      this.Key = EncryptionDeprecated.GetSHA1(Key);
      Cnv = new Conversion();
      Chars = CharacterMap;
    }
    #endregion

    #region Fields
    private string Key { get; set; }
    private Conversion Cnv { get; set; }
    public string Chars { get; set; }
    #endregion

    #region Methods
    #region private int getIdxChar(char c)
    private int getIdxChar(char c) 
    {
      for (int i = 0; i < Chars.Length; i++)
      {
        if (c == Chars[i])
        { return i; }
      }
      return -1;
    }
    #endregion

    #region private string EncriptyASCII(string text, bool use_even, int char_min, int char_max)
    /// <summary>
    /// Motor de criptografia para caracteres
    /// </summary>
    /// <param name="text">Texto a ser criptografado</param>
    /// <param name="use_even">usar retorno da posição impar ou par</param>
    /// <param name="char_min">menor caracter que pode ser utilizado(33)</param>
    /// <param name="char_max">maior caracter que pode ser utilizado(126)</param>
    /// <returns></returns>
    private string EncriptyASCII(string text, bool use_even) 
    {
      int char_min = 0;
      int char_max = Chars.Length - 1;

      string VCrip = "";
      for (int i = 0; i < text.Length; i++)
      {
        int vc;//Valor ASCII
        char c;//Caracter ASCII

        vc = getIdxChar(text[i]);

        #region Verifica a distancia percorrida do caracter
        int posk =(i % Key.Length);
        int iKey = 0;
        if (posk < (Key.Length - 1))
        {
          string nx = Key[posk].ToString() + Key[posk + 1].ToString();
          iKey = Cnv.ToInt(nx);
        }
        else
        { iKey = Cnv.ToInt(Key[posk].ToString()); }
        #endregion

        #region Soma ou subtrai quando está criptografando ou descriptografando
        if ((i % 2) == Convert.ToInt16(use_even))
        { vc += iKey; }
        else
        { vc -= iKey; }
        #endregion

        #region Verifica se não estrapolou o limite do array
        int Dif = Chars.Length;
        while (vc < char_min || vc > char_max)
        {
          if (vc < char_min)
          { vc += Dif; }
          else if (vc > char_max)
          { vc -= Dif; }
        }
        #endregion

        c = Chars[vc];
        VCrip += c;
      }
      return VCrip;
    }
    #endregion

    #region public string Encrypt(string Text)
    /// <summary>
    /// Criptografa valor passado.
    /// </summary>
    /// <param name="Value">Valor sem criptografia.</param>
    /// <returns>Retorna o valor criptografado </returns>
    public string Encrypt(string Text)
    {
      return EncriptyASCII(Text, false);
    }
    #endregion

    #region public string Descrypt(string Value)
    /// <summary>
    /// Descriptografa valor passado.
    /// </summary>
    /// <param name="Value">Valor com criptografia</param>
    /// <returns>Retorna o valor descriptografado</returns>
    public string Descrypt(string Text)
    {
      return EncriptyASCII(Text, true);
    }
    #endregion

    #region public string getCodeFromText(string Text)
    public static string getCodeFromText(int StatWith,string Text)
    {
      long Total = StatWith;
      for (int I = 0; I < Text.Length; I++)
      { Total += ((int)Text[I]); }
      return Total.ToString();
    }
    #endregion
    #endregion

    /*
     * Projetos antigos deverão copiar esta função e não utilizar mais da lib
     * */
    public static string GetSHA1(string Text)
    {
        try
        {
            byte[] SHA1HashValue = new SHA1Managed().ComputeHash(new UnicodeEncoding().GetBytes(Text));
            StringBuilder Retorno = new StringBuilder();

            foreach (byte b in SHA1HashValue)
            { Retorno.Append(b.ToString()); }
            return Retorno.ToString();
        }
        catch (Exception ex) { throw new Exception("Erro ao criptografar em SHA1", ex); }
    }

    public static string GetMD5(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        { sb.Append(hash[i].ToString("x2")); }
        return sb.ToString();
    }

    public static string SHA1_Hash(string input)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }

    public static string MD5_Hash(string input)
    {
      System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
      byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
      byte[] hash = md5.ComputeHash(inputBytes);
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      for (int i = 0; i < hash.Length; i++)
      { sb.Append(hash[i].ToString("x2")); }
      return sb.ToString();
    }

    
  }
}
