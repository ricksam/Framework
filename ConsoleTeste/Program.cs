/*using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Text;*/

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            /*string link = "http://apiaguia.e-construmarket.com.br/api/Token";
            Console.WriteLine("link:"+link+Environment.NewLine);

            System.Collections.Specialized.NameValueCollection Headers = new System.Collections.Specialized.NameValueCollection();
            Headers.Add("Authorization", "Basic NDMwNDQwOGIzY2QxNDI4OGFiNTRjNjYwNDI5ZjE0N2Q6Y29uc3RydXBvaW50QGNvbnN0cnVwb2ludDIwMTc=");

            string postData = "grant_type=password&username=construpoint@econstrumarket.com.br&password=mobi@1234";
            string contantType = "application/x-www-form-urlencoded";

            Console.WriteLine("postData:" + postData + Environment.NewLine);
            Console.WriteLine("contantType:" + contantType + Environment.NewLine);

            string response = GetWebResponse(link, Headers, postData, contantType, Encoding.Default);

            Console.WriteLine("response:" + response + Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Confere ai Ju que com este código tudo funciona certinho !!!" + Environment.NewLine);
            Console.ReadKey();*/
        }

        /*
        public static string GetWebResponse(string Url, System.Collections.Specialized.NameValueCollection Headers, string PostData, string ContentType, Encoding enc, int Timeout = 100000)
        {
            System.Net.HttpWebResponse rp = null;
            System.IO.StreamReader sr = null;
            try
            {
                System.Net.HttpWebRequest rq = ((System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url));
                if (Headers != null)
                {
                    rq.PreAuthenticate = true;
                    rq.Headers.Add(Headers);
                }

                rq.Timeout = Timeout;
                if (!string.IsNullOrEmpty(PostData))
                {
                    byte[] data = Encoding.UTF8.GetBytes(PostData);
                    rq.Method = "POST";
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
        */
    }
}
