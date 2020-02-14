using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Class
{
  //delegate void UploadCompleted();

  public class Upload
  {
    //"http://localhost:56767/EnviaLog?Identificacao=aaTagPC&Grupo=aaTag"

    public Upload() 
    {
      Completed = false;
    }

    public bool Completed { get; set; }
    //public UploadCompleted UploadCompleted { get; set; }
    //private bool RemoveOnFinish { get; set; }
    private string queryFileName { get; set; }
    private string _boundaryNo = DateTime.Now.Ticks.ToString("x");


    public void Send(string Link, string QueryFileName, string PathFile)
    {
      try
      {
        this.queryFileName = QueryFileName;
        System.IO.Stream stream = System.IO.File.OpenRead(PathFile);
        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
        stream.CopyTo(memoryStream);
        System.Net.WebClient webClient = new System.Net.WebClient();
        webClient.Headers["Content-type"] = "multipart/form-data; boundary=---------------------------" + _boundaryNo;
        webClient.OpenWriteAsync(new Uri(Link, UriKind.Absolute), "POST", new { Stream = memoryStream, FileName = PathFile });
        webClient.OpenWriteCompleted += new System.Net.OpenWriteCompletedEventHandler(webClient_OpenWriteCompleted);
      }
      catch { Completed = true; }
    }
    
    void webClient_OpenWriteCompleted(object sender, System.Net.OpenWriteCompletedEventArgs e)
    {
      try
      {
        if (e.Error == null)
        {
          System.IO.Stream responseStream = e.Result as System.IO.Stream;
          dynamic obj = e.UserState;
          System.IO.MemoryStream memoryStream = obj.Stream as System.IO.MemoryStream;
          string fileName = obj.FileName;
          if (responseStream != null && memoryStream != null)
          {
            string headerTemplate = string.Format("-----------------------------{0}\r\n", _boundaryNo);

            memoryStream.Position = 0;
            byte[] byteArr = memoryStream.ToArray();
            string data = headerTemplate + string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", queryFileName, fileName);
            byte[] header = Encoding.UTF8.GetBytes(data);
            responseStream.Write(header, 0, header.Length);

            responseStream.Write(byteArr, 0, byteArr.Length);
            header = Encoding.UTF8.GetBytes("\r\n");
            responseStream.Write(byteArr, 0, byteArr.Length);

            byte[] trailer = System.Text.Encoding.UTF8.GetBytes(string.Format("-----------------------------{0}--\r\n", _boundaryNo));
            responseStream.Write(trailer, 0, trailer.Length);
          }
          memoryStream.Close();
          responseStream.Close();

          memoryStream.Dispose();
          responseStream.Dispose();

          memoryStream = null;
          responseStream = null;
        }
      }
      finally { Completed = true; }
    }
  }
}
