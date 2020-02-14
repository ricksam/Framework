using System;
using System.Collections.Generic;
using System.Text;
using SpeechLib;

namespace lib.Class
{
  public class Speech
  {
    #region Constructor
    public Speech() 
    {
      vox = new SpVoice();
      SpFileStream = new SpFileStream();
      voiceList = new List<string>();
      idx = -1;      
      setVoices();
    }
    #endregion

    #region private Fields
    private SpFileStream SpFileStream { get; set; }
    private SpVoice vox { get; set; }
    private List<string> voiceList { get; set; }
    private int idx { get; set; }
    #endregion

    #region private void setVoices()
    /// <summary>
    /// Carrega as vozes instaladas no computador
    /// </summary>
    private void setVoices() 
    {
      foreach (ISpeechObjectToken Token in vox.GetVoices(string.Empty, string.Empty))
      { voiceList.Add(Token.GetAttribute("Name")); }
      Index = 0;
    }
    #endregion
    
    #region public int Index
    /// <summary>
    /// Index da voz que está selecionada
    /// </summary>
    public int Index
    {
      get { return idx; }
      set
      {
        if (value < voiceList.Count)
        {
          idx = value;          
          vox.Voice = vox.GetVoices("Name=" + voiceList[value], string.Empty).Item(0);
        }        
      }
    }
    #endregion

    #region public List<string> Voices
    /// <summary>
    /// Vozes instaladas no computador
    /// </summary>
    public List<string> Voices { get { return voiceList; } }
    #endregion

    #region public int Speed
    /// <summary>
    /// Velocidade da fala
    /// </summary>
    public int Speed
    {
      get { return vox.Rate; }
      set { vox.Rate = value; }
    }
    #endregion

    #region public int Volume
    /// <summary>
    /// Volume de saída da voz
    /// </summary>
    public int Volume { get { return vox.Volume; } set { vox.Volume = value; } }
    #endregion

    #region public void Skeak(string Text)
    /// <summary>
    /// Pronuncia o texto passado por parâmetro
    /// </summary>
    /// <param name="Text"></param>
    public void Speak(string Text)
    { Speak(Text, true); }
    public void Speak(string Text, bool Async)
    {
      if (Async)
      { vox.Speak(Text, SpeechVoiceSpeakFlags.SVSFlagsAsync); }
      else
      { vox.Speak(Text, SpeechVoiceSpeakFlags.SVSFDefault); }
    }
    #endregion

    #region public void SetVoiceOf(string Text)
    /// <summary>
    /// Seleciona uma voz que contenha o texto do parâmetro
    /// </summary>
    /// <param name="Text"></param>
    public void SetVoiceOf(string Text)
    {
      for (int i = 0; i < Voices.Count; i++)
      {
        if (Voices[i].ToUpper().IndexOf(Text.ToUpper()) != -1)
        {
          Index = i;
          break;
        }
      }
    }
    #endregion

    #region public void SpeakToWave(string Text, string FileName)
    /// <summary>
    /// Fala em arquivo wave
    /// </summary>
    /// <param name="Text"></param>
    /// <param name="FileName"></param>
    public void SpeakToWave(string Text, string FileName) 
    {
      SpFileStream.Open(FileName, SpeechStreamFileMode.SSFMCreateForWrite, false);
      vox.AudioOutputStream = SpFileStream;
      vox.Speak(Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
      vox.WaitUntilDone(-1);
      SpFileStream.Close();
    }
    #endregion
  }
}
