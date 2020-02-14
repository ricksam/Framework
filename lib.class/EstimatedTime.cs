using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Class
{
  public class EstimatedTime
  {
    #region public EstimatedTime()
    public EstimatedTime() 
    {
      this.Start();
    }
    #endregion
        
    #region Fields
    Conversion Cnv { get; set; }
    string StartTime { get; set; }
    double FaltaAnt { get; set; }
    #endregion

    #region public void Start()
    public void Start()
    {
      Cnv = new Conversion();
      StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
    #endregion

    #region public string Get(int Index, int Count)
    public string Get(int Index, int Count)
    {
      DateTime DtStart = Cnv.ToDateTime(StartTime);
      DateTime DtAtual = DateTime.Now;

      TimeSpan TsPass = DtAtual.Subtract(DtStart);

      double PercDeco = ((double)Index / (double)Count);
      double PercFalta = (1 - PercDeco);
      double Falta = PercDeco == 0 ? Count : TsPass.TotalSeconds * PercFalta / PercDeco;

      if (FaltaAnt != 0 && FaltaAnt != Count)
      { Falta = (FaltaAnt + Falta) / 2; }
      FaltaAnt = Falta;

      TimeSpan TsFalta = TimeSpan.FromSeconds(Falta);

      string Dias = string.Format(" {0} dias ", TsFalta.Days);
      string Horas = string.Format(" {0} horas ", TsFalta.Hours);
      string Minutos = string.Format(" {0} minutos ", TsFalta.Minutes);
      string Segundos = string.Format(" {0} segundos ", TsFalta.Seconds);

      if (TsFalta.Days != 0) { return Dias + Horas; }
      if (TsFalta.Hours != 0) { return Horas + Minutos; }
      if (TsFalta.Minutes != 0) { return Minutos + Segundos; }
      else { return Segundos; }
    }
    #endregion
  }
}