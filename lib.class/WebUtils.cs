﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Threading;

namespace lib.Class
{
  public class WebUtils
  {
    public WebUtils() 
    {
      InternetOn = false;
      InternetVerify = false;
    }
    
    private string VerifyLink { get; set; }
    private bool InternetOn { get; set; }
    private bool InternetVerify { get; set; }

    #region void InvestigaInternet()
    void InvestigaInternet()
    {
      try
      {
        //GetWebResponse("http://ricksam.net84.net/");
        GetWebResponse(VerifyLink);
        InternetOn = true;
      }
      catch
      { InternetOn = false; }

      InternetVerify = false;
    }
    #endregion

    #region public bool InternetOnLine(string VerifyLink)
    public bool InternetOnLine(string VerifyLink)
    {
      /*
      // Busca OnLine
      try
      {
        GetWebResponse(VerifyLink);
        return true;
      }
      catch
      { return false; }*/

      
      // Off Line com thread
      InternetVerify = true;
      this.VerifyLink = VerifyLink;

      Thread tr = new Thread(new ThreadStart(InvestigaInternet));
      tr.Start();

      for (int i = 0; i < 2000 && InternetVerify; i++)
      { System.Threading.Thread.Sleep(10); }

      return InternetOn;
    }
    #endregion
                
    public static string GetWebResponse(string Url)
    {
      return GetWebResponse(Url, "", "application/x-www-form-urlencoded");
    }

    public static string GetWebResponse(string Url, string PostData, Encoding enc, string ContentType = "application/x-www-form-urlencoded", int Timeout = 100000) 
    {
        return GetWebResponse(Url, null, PostData, ContentType, enc);
    }

    #region public static string GetWebResponse(string Url, string PostData)
    public static string GetWebResponse(string Url, string PostData, string ContentType = "application/x-www-form-urlencoded")
    { return GetWebResponse(Url, null, PostData, ContentType, Encoding.Default); }
    #endregion

    #region public static string GetWebResponse(string Url, string PostData)
    public static string GetWebResponse(string Url, System.Collections.Specialized.NameValueCollection Headers, string PostData, string ContentType, Encoding enc, int Timeout = 100000, string Method="")
    {
        System.Net.HttpWebResponse rp = null;
        System.IO.StreamReader sr = null;
        try
        {
            System.Net.HttpWebRequest rq = ((System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url));
            
            if (!string.IsNullOrEmpty(Method))
            {
                rq.Method = Method;
            }

            if (Headers != null)
            {
                rq.PreAuthenticate = true;
                rq.Headers.Add(Headers);
            }

            rq.Timeout = Timeout;
            if (!string.IsNullOrEmpty(PostData))
            {
                byte[] data = Encoding.UTF8.GetBytes(PostData);
                if (string.IsNullOrEmpty(Method))
                {
                    rq.Method = "POST";
                }
                rq.ContentType = ContentType;
                rq.ContentLength = data.Length;
                //StreamWriter rw = new StreamWriter(rq.GetRequestStream());
                Stream rw = rq.GetRequestStream();
                rw.Write(data, 0, data.Length);
                rw.Close();
                rw = null;
            }

            rp = (System.Net.HttpWebResponse)rq.GetResponse();
            sr = new System.IO.StreamReader(rp.GetResponseStream(), enc);
            return sr.ReadToEnd();
        }
        catch (System.Net.WebException wex)
        {
            if (wex.Response != null)
            {
                using (var errorResponse = (System.Net.HttpWebResponse)wex.Response)
                {
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        throw new Exception(string.Format("{0} {1}", wex.Message, reader.ReadToEnd()));
                    }
                }
            }
            else
            {
                throw wex;
            }
        }
        finally
        {
            if (rp != null)
            { rp.Close(); }

            if (sr != null)
            { sr.Close(); }

            rp = null;
            sr = null;
        }
    }

    public static string GetWebResponse(string Url, Stream stream, string ContentType, Encoding enc, int Timeout = 100000)
    {
      System.Net.HttpWebResponse rp = null;
      System.IO.StreamReader sr = null;
      try
      {
        System.Net.HttpWebRequest rq = ((System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url));
        rq.Timeout = Timeout;
        if (stream != null)
        {
          byte[] bytearray = null;
          //Stream stream = PostFile.BaseStream;
          stream.Seek(0, SeekOrigin.Begin);
          bytearray = new byte[stream.Length];
          int count = 0;
          while (count < stream.Length)
          { bytearray[count++] = Convert.ToByte(stream.ReadByte()); }
          

          rq.Method = "POST";
          rq.ContentType = ContentType;
          rq.ContentLength = bytearray.Length;
          using (Stream requestStream = rq.GetRequestStream())
          {
            // Send the file as body request.
            requestStream.Write(bytearray, 0, bytearray.Length);
            requestStream.Close();
          }


          //Stream rw = rq.GetRequestStream();
          //PostFile.CopyTo(rw);
          //rw.Write(PostData);
          //rw.Close();
          //rw = null;
        }

        rp = (System.Net.HttpWebResponse)rq.GetResponse();
        sr = new System.IO.StreamReader(rp.GetResponseStream(), enc);
        return sr.ReadToEnd();
      }
      catch (System.Net.WebException wex)
      {
          if (wex.Response != null)
          {
              using (var errorResponse = (System.Net.HttpWebResponse)wex.Response)
              {
                  using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                  {
                      throw new Exception(string.Format("{0} {1}", wex.Message, reader.ReadToEnd()));
                  }
              }
          }
          else
          {
              throw wex;
          }
      }
      finally
      {
        if (rp != null)
        { rp.Close(); }

        if (sr != null)
        { sr.Close(); }

        rp = null;
        sr = null;
      }
    }

    public static string GetWebResponse(string url, string method, string contentType, System.Collections.Specialized.NameValueCollection Headers, System.Collections.Specialized.NameValueCollection formData, System.Collections.Specialized.NameValueCollection files)
    {
        string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

        System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        if (Headers != null)
        {
            httpWebRequest.Headers.Add(Headers);
        }
        httpWebRequest.ContentType = contentType + "; boundary=" + boundary;//multipart/form-data
        //httpWebRequest.ContentType = contentType + "; charset=utf-8";
        httpWebRequest.Method = method;
        httpWebRequest.KeepAlive = true;
        httpWebRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
        Stream memStream = new System.IO.MemoryStream();

        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        if (formData != null)
        {
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            foreach (string key in formData.Keys)
            {
                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, formData[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }
        }


        if (files != null)
        {
            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            foreach (string key in files.Keys)
            {
                string paramName = key;
                string file = files[key];
                string fileName = System.IO.Path.GetFileName(file);

                string header = string.Format(headerTemplate, paramName, fileName);
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);
                FileStream fileStream = new FileStream(file, FileMode.Open,
                FileAccess.Read);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                fileStream.Close();
            }
        }

        if (method == "POST")
        {
            httpWebRequest.ContentLength = memStream.Length;
            Stream requestStream = httpWebRequest.GetRequestStream();
            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();
        }

        System.Net.WebResponse webResponse = null;
        try
        {
            webResponse = httpWebRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
            return reader.ReadToEnd();

        }
        catch (System.Net.WebException wex)
        {
            if (wex.Response != null)
            {
                using (var errorResponse = (System.Net.HttpWebResponse)wex.Response)
                {
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        throw new Exception(string.Format("{0} {1}", wex.Message, reader.ReadToEnd()));
                    }
                }
            }
            else
            {
                throw wex;
            }
        }
        finally
        {
            if (webResponse != null)
            {
                webResponse.Close();
                webResponse = null;
            }

            httpWebRequest = null;
        }
        //return "";
    }
    #endregion

    #region public static string GetRemoteIP()
    public static string GetRemoteIP()
    {
      try
      {
        // exemple: <html><head><title>Current IP Check</title></head><body>Current IP Address: 187.115.128.126</body></html>
        //string content = GetWebResponse("http://checkip.dyndns.org/");
        //int idx = content.IndexOf("Current IP Address:");
        //content = content.Remove(0, idx + 20);
        //return content.Substring(0, content.IndexOf("</"));

        // Mas enquanto tiver o locaweb
        return GetWebResponse("http://www.rcksoftware.com.br/service/myip.php");
      }
      catch { return "0.0.0.0"; }
    }
    #endregion

    #region public static DateTime GetRightTime()
    public static DateTime GetRightTime() 
    {
      try
      {
        string source = GetWebResponse("http://www.horacerta.com.br/index.php?city=sao_paulo");
        string search = "<input name=\"initial_date\" type=\"hidden\" value=\"";
        int idx = source.IndexOf(search);
        if (idx != -1)
        {
          source = source.Remove(0, idx + search.Length);
          int idx_fim = source.IndexOf("\" />");
          string sval_hora = source.Substring(0, idx_fim);

          string[] campos = sval_hora.Split(new char[] { ' ' });
          return Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", campos));
        }
        return DateTime.Now;
      }
      catch { return DateTime.Now; }
    }
    #endregion

    #region public static Mail GetMailDeveloper()
    public static Mail GetMailDeveloper() 
    {
      return new Mail("smtp.gmail.com", "contato.rcksoftware@gmail.com", "contatorcksoftware", true, true, 587);
    }
    #endregion

    [System.Runtime.InteropServices.DllImport("sensapi.dll")]
    public static extern bool IsNetworkAlive(out int flags);

    public static bool IsNetworkAlive() 
    {
      return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
      //int flags;
      //bool connected = IsNetworkAlive(out flags);
      //return flags == 1 && connected;
    }

    public static string ConvertHtmlUTF8(string s)
    {
      string r = "";
      for (int i = 0; i < s.Length; i++)
      { r += ((int)s[i] < 32 || (int)s[i] > 127 ? string.Format("&#{0};", (int)s[i]) : s[i].ToString()); }
      return r;
    }
  }
}
