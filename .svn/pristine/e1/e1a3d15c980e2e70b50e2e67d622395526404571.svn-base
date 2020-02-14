using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Class
{
  [Serializable]
  public class Conversion
  {
    #region Conversion
    public Conversion()
    { }

    public Conversion(object o)
    { _o = o; }
    #endregion

    #region Source
    private object _o = null;
    public object Source { get { return _o; } set { _o = value; } }
    #endregion

    #region ToString
    public override string ToString()
    { try { return _o.ToString(); } catch { return ""; } }

    public string ToString(object o)
    {
      _o = o;
      return ToString();
    }
    #endregion

    #region ToChar
    public char ToChar()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return '\0'; }

            return Convert.ToChar(_o);
        }
        catch { return '\0'; }
    }

    public char ToChar(object o)
    {
      _o = o;
      return ToChar();
    }
    #endregion

    #region ToInt
    public int ToInt()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return 0; }

            return Convert.ToInt32(_o);
        }
        catch { return 0; }
    }

    public int ToInt(object o)
    {
      _o = o;
      return ToInt();
    }
    #endregion

    #region ToLong
    public long ToLong()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return 0; }

            return Convert.ToInt64(_o);
        }
        catch { return 0; }
    }

    public long ToLong(object o)
    {
      _o = o;
      return ToLong();
    }
    #endregion

    #region ToDouble
    public double ToDouble()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return 0; }

            return Convert.ToDouble(_o);
        }
        catch { return 0; }
    }

    public double ToDouble(object o)
    {
      _o = o;
      return ToDouble();
    }
    #endregion

    #region ToBool
    public bool ToBool()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return false; }

            return
              _o.ToString() == "1" ||
              _o.ToString().ToUpper() == "S" ||
              _o.ToString().ToUpper() == "SIM" ||
              _o.ToString().ToUpper() == "T" ||
              _o.ToString().ToUpper() == "TRUE" ||
              _o.ToString().ToUpper() == "Y" ||
              _o.ToString().ToUpper() == "YES" ||
              _o.ToString().ToUpper() == "V" ||
              _o.ToString().ToUpper() == "VERDADE" ||
              _o.ToString().ToUpper() == "VERDADEIRO" ||
              _o.ToString().ToUpper() == "VERDADEIRA"
              ;
        }
        catch { return false; }
    }

    public bool ToBool(object o)
    {
      _o = o;
      return ToBool();
    }
    #endregion

    #region ToDecimal
    public decimal ToDecimal()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return 0; }

            return Convert.ToDecimal(_o);
        }
        catch { return 0; }
    }

    public decimal ToDecimal(object o)
    {
      _o = o;
      return ToDecimal();
    }
    #endregion

    #region ToDateTime
    public DateTime ToDateTime()
    {
        try
        {
            if (_o == null || (_o is string && string.IsNullOrEmpty(_o.ToString())))
            { return DateTime.MinValue; }

            return Convert.ToDateTime(_o);
        }
        catch { return DateTime.MinValue; }
    }

    public DateTime ToDateTime(object o)
    {
      _o = o;
      return ToDateTime();
    }
    #endregion

    #region public static string ByteToHex(byte[] bt)
    public static string ByteToHex(byte[] bt)
    {
      if (bt == null)
      { return ""; }

      string r = "";
      for (int i = 0; i < bt.Length; i++)
      {
        string val = bt[i].ToString("X").PadLeft(2, '0');
        r += val;
      }
      return r;
    }
    #endregion

    #region public static byte[] HexToByte(string Hex)
    public static byte[] HexToByte(string Hex)
    {
      byte[] bt = new byte[Hex.Length / 2];
      for (int i = 0; i < Hex.Length; i += 2)
      {
        string val = Hex.Substring(i, 2);
        int idx = (i + 1) / 2;
        bt[idx] = Convert.ToByte(val, 16);
      }
      return bt;
    }
    #endregion

    #region public static byte[] TextToByte(string s)
    public static byte[] TextToByte(string s)
    {
      byte[] bt = new byte[s.Length];
      for (int i = 0; i < s.Length; i++)
      { bt[i] = (byte)s[i]; }
      return bt;
    }
    #endregion

    #region public static string ByteToText(byte[] bt)
    public static string ByteToText(byte[] bt)
    {
      string s = "";
      for (int i = 0; i < bt.Length; i++)
      { s += ((char)bt[i]).ToString(); }
      return s;
    }
    #endregion

    #region public string ByteToBinary(byte Value)
    public string ByteToBinary(byte Value)
    {
      // Declare a few variables we're going to need
      long BinaryHolder;
      char[] BinaryArray;

      string BinaryResult = "";
      while (Value > 0)
      {
        BinaryHolder = Value % 2;
        BinaryResult += BinaryHolder;
        Value = (byte)(Value / 2);
      }

      // The algoritm gives us the binary number in reverse order (mirrored)
      // We store it in an array so that we can reverse it back to normal
      BinaryArray = BinaryResult.ToCharArray();
      Array.Reverse(BinaryArray);
      BinaryResult = new string(BinaryArray);
      return BinaryResult;
    }
    #endregion

    #region public byte BinaryToByte(string Value)
    public byte BinaryToByte(string Value)
    {
      return Convert.ToByte(Value, 2);
    }
    #endregion

    public static ushort[] DecimalBaseForBase(ulong n, ushort b)
    {
      if (n == 0 || b == 0)
      { return new ushort[] { }; }

      List<ushort> rslt = new List<ushort>();
      uint digitPos = 1;

      while (n != 0)
      {
        ulong r = (n % b) * digitPos;
        if (digitPos != 0)
        { r /= digitPos; }
        rslt.Add((ushort)r);
        n /= b;
        digitPos *= 10;
      }
      return rslt.ToArray();
    }
  }
}
