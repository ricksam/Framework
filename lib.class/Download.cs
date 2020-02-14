using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace lib.Class
{
  public class Download
  {
    public Download(int BufferSize)
    {
      this.BufferSize = BufferSize;
      this.cnv = new Conversion();
      this.Cancel = false;
    }
        
    public delegate void Download_Handle(long Index, long Count);

    public bool Cancel { get; set; }    
    private int BufferSize { get; set; }
    private Conversion cnv { get; set; }
    public Download_Handle OnDownload { get; set; }
        
    #region public byte[] ToArray(string LinkSouce)
    public byte[] ToArray(string LinkSouce)
    {
      this.Cancel = false;

      WebClient wc = new WebClient();
      Stream st = wc.OpenRead(LinkSouce);

      long len = cnv.ToLong(wc.ResponseHeaders["Content-Length"].ToString());
      long totalSize = 0;
      int bytesSize = 0;
      byte[] downBuffer = new byte[BufferSize];
      List<byte> resut = new List<byte>();
      resut.Clear();

      while ((bytesSize = st.Read(downBuffer, 0, downBuffer.Length)) > 0) //while (totalSize < len)      
      {
        //bytesSize = st.Read(downBuffer, 0, downBuffer.Length);
        if (this.Cancel)
        { break; }
        
        if (OnDownload != null)
        { OnDownload(totalSize, len); }

        for (int i = 0; i < bytesSize; i++)
        { resut.Add(downBuffer[i]); }
        
        totalSize += bytesSize;
      }
      return resut.ToArray();
    }
    #endregion

    #region public void ToFile(string LinkSouce, string FileDestination)
    public void ToFile(string LinkSouce, string FileDestination)
    {
      byte[] arrByte = ToArray(LinkSouce);

      if (arrByte.Length != 0)
      {
        if (File.Exists(FileDestination))
        { File.Delete(FileDestination); }
      }

      FileStream fileStream = new FileStream(FileDestination, FileMode.Create,
                          FileAccess.Write, FileShare.None);
      fileStream.Write(arrByte, 0, arrByte.Length);      
      fileStream.Close();
    }
    #endregion

    #region public Stream ToStream(string LinkSouce)
    public Stream ToStream(string LinkSouce)
    {
      byte[] arr = ToArray(LinkSouce);
      MemoryStream ms = new MemoryStream(arr);
      return ms;
      //WebClient wc = new WebClient();
      //return wc.OpenRead(LinkSouce);
    }
    #endregion

    public System.Drawing.Image ToImage(string LinkSource)
    {
      return System.Drawing.Image.FromStream(ToStream(LinkSource));
    }
        
    /*
    private class DownloadItem
    {
      public DownloadItem(int BufferSize, byte[] Buffer)
      {
        this.BufferSize = BufferSize;
        this.Buffer = Buffer;
      }
      public int BufferSize { get; set; }
      public byte[] Buffer { get; set; }
    }
    public static bool DownloadUpdate(string Origem, string Destino)
    {
      try
      {
        lib.Class.Conversion Cnv = new lib.Class.Conversion();
        

        WebClient wc = new WebClient();
        Stream st = wc.OpenRead(Origem);

        decimal len = Cnv.ToDecimal(wc.ResponseHeaders["Content-Length"].ToString());

        decimal totalSize = 0;
        int bytesSize = 0;
        byte[] downBuffer = new byte[128];
        List<DownloadItem> ListBuffer = new List<DownloadItem>();

        FileStream fileStream = new FileStream(Destino, FileMode.Create,
                          FileAccess.Write, FileShare.None);
        while ((bytesSize = st.Read(
          downBuffer, 0, downBuffer.Length)) > 0)
        {

          ListBuffer.Add(new DownloadItem(bytesSize, (byte[])downBuffer.Clone()));
          fileStream.Write(downBuffer, 0, bytesSize);
          totalSize += bytesSize;
          int Perc = Cnv.ToInt((totalSize / len) * 100);
        }

        fileStream.Close();

        FileStream fileStream2 = new FileStream(Destino+"copia.jpg", FileMode.Create,
                          FileAccess.Write, FileShare.None);
        for (int i = 0; i < ListBuffer.Count; i++)
        {
          fileStream2.Write(ListBuffer[i].Buffer, 0, ListBuffer[i].BufferSize);
        }
        fileStream2.Close();
        
        return true;
      }
      catch
      { return false; }
    }
    */
  }

  /*
   * 
   * private void btnDownload_Click(object sender, EventArgs e)
{
  WebClient webClient = new WebClient();
  webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
  webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
  webClient.DownloadFileAsync(new Uri("http://mysite.com/myfile.txt"), @"c:\myfile.txt");
}

private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
{
  progressBar.Value = e.ProgressPercentage;
}

private void Completed(object sender, AsyncCompletedEventArgs e)
{
  MessageBox.Show("Download completed!");
}
   */
}
