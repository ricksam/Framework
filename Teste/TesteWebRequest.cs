﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Teste
{
    public partial class TesteWebRequest : Form
    {
        public TesteWebRequest()
        {
            InitializeComponent();
        }

        List<SendRequest> ListRequest = new List<SendRequest>();
        string LogFile = Environment.CurrentDirectory + "\\Request.log";

        public bool InListRequest(SendRequest s)
        {
            foreach (var item in ListRequest)
            {
                if (item.Link == s.Link && item.Header == s.Header  && item.PostData == s.PostData && item.ContentType == s.ContentType)
                {
                    return true;
                }
            }

            return false;
        }

        private void SaveListRequest()
        {
            if (System.IO.File.Exists(LogFile))
            { System.IO.File.Delete(LogFile); }

            lib.Class.TextFile tf = new lib.Class.TextFile();
            tf.Open(lib.Class.enmOpenMode.Writing, LogFile);
            foreach (var item in ListRequest)
            {
                tf.WriteLine(item.Link.Replace(Environment.NewLine, ""));
                tf.WriteLine(item.getHeaderKey(0).Replace(Environment.NewLine, ""));
                tf.WriteLine(item.getHeaderValue(0).Replace(Environment.NewLine, ""));
                tf.WriteLine(item.PostData.Replace(Environment.NewLine, ""));
                tf.WriteLine(item.ContentType);
            }
            tf.Close();
        }

        private void UpdateCombo()
        {
            cmbLink.Items.Clear();
            List<SendRequest> list = new List<SendRequest>();
            list.AddRange(ListRequest);
            list.Reverse();
            cmbLink.Items.AddRange(list.ToArray());
        }

        private void LoadListRequest()
        {
            try
            {
                if (!System.IO.File.Exists(LogFile))
                { return; }

                ListRequest.Clear();
                lib.Class.TextFile tf = new lib.Class.TextFile();
                tf.Open(lib.Class.enmOpenMode.Reading, LogFile);
                string line = "";
                while ((line = tf.ReadLine()) != null)
                {
                    SendRequest s = new SendRequest();
                    s.Link = line;

                    SendRequestHeader sh = new SendRequestHeader();
                    sh.key = tf.ReadLine();
                    sh.value = tf.ReadLine();

                    s.Header = new List<SendRequestHeader>();
                    s.Header.Add(sh);

                    s.PostData = tf.ReadLine();
                    s.ContentType = tf.ReadLine();
                    ListRequest.Add(s);
                }
                tf.Close();

                UpdateCombo();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtResposta.Text = "Aguarde ... ";
                txtResposta.Refresh();

                SendRequest s = new SendRequest();
                s.Link = cmbLink.Text;
                s.Method = cmbMethod.Text;
                s.Header = new List<SendRequestHeader>();
                s.Header.Add(new SendRequestHeader()
                {
                    key = txtHeaderKey.Text,
                    value = txtHeaderValue.Text
                });
                s.PostData = txtPostData.Text;
                s.ContentType = txtContentType.Text;

                if (!InListRequest(s))
                {
                    ListRequest.Add(s);
                    while (ListRequest.Count > 1000)
                    { ListRequest.RemoveAt(0); }
                }

                UpdateCombo();

                System.Collections.Specialized.NameValueCollection Headers = null;

                if (!string.IsNullOrEmpty(txtHeaderKey.Text) && !string.IsNullOrEmpty(txtHeaderValue.Text))
                {
                    Headers = new System.Collections.Specialized.NameValueCollection();
                    Headers.Add(txtHeaderKey.Text, txtHeaderValue.Text);
                }

                txtResposta.Text = lib.Class.WebUtils.GetWebResponse(s.Link,  Headers, s.PostData, s.ContentType, (Encoding)cmbEncoding.SelectedItem, 100000, cmbMethod.Text);
            }
            catch(System.Net.WebException wex)
            {
                txtResposta.Text = wex.Message;
                if (wex.Response != null)
                {
                    using (var errorResponse = (System.Net.HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            txtResposta.Text = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex) { txtResposta.Text = ex.Message; }
        }

        private void txtResposta_Enter(object sender, EventArgs e)
        {
            txtResposta.SelectAll();
        }

        private void txtResposta_TextChanged(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = txtResposta.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Stream stream = (new StreamReader(dlgOpen.FileName)).BaseStream;
                    txtResposta.Text = lib.Class.WebUtils.GetWebResponse(cmbLink.Text, stream, txtContentType.Text, System.Text.Encoding.Default);
                }
                catch (Exception ex) { txtResposta.Text = ex.Message; }
            }
        }

        private void TesteWebRequest_Load(object sender, EventArgs e)
        {
            LoadListRequest();

            cmbEncoding.Items.Clear();
            cmbEncoding.Items.Add(System.Text.Encoding.ASCII);
            cmbEncoding.Items.Add(System.Text.Encoding.BigEndianUnicode);
            cmbEncoding.Items.Add(System.Text.Encoding.Default);
            cmbEncoding.Items.Add(System.Text.Encoding.Unicode);
            cmbEncoding.Items.Add(System.Text.Encoding.UTF32);
            cmbEncoding.Items.Add(System.Text.Encoding.UTF7);
            cmbEncoding.Items.Add(System.Text.Encoding.UTF8);

            cmbEncoding.SelectedIndex = 2;
        }

        private void TesteWebRequest_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveListRequest();
        }

        private void cmbLink_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLink.SelectedIndex != -1)
            {
                SendRequest s = (SendRequest)cmbLink.SelectedItem;
                if (s.Header != null && s.Header.Count > 0)
                {
                    txtHeaderKey.Text = s.Header[0].key;
                    txtHeaderValue.Text = s.Header[0].value;
                }
                else
                {
                    txtHeaderKey.Text = "";
                    txtHeaderValue.Text = "";
                }
                txtContentType.Text = s.ContentType;
                txtPostData.Text = s.PostData;
            }
        }


        private void txtPostData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.A | Keys.Control))
            {
                e.Handled = true;
                txtPostData.SelectAll();
            }
        }

        private void txtPostData_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtResposta.Text);
        }

        private void txtResposta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A)) {
                txtResposta.SelectAll();
            }
        }

    }

    public class SendRequest
    {
        public string Method { get; set; }
        public string Link { get; set; }
        public List<SendRequestHeader> Header { get; set; }
        public string PostData { get; set; }
        public string ContentType { get; set; }

        public override string ToString()
        {
            return Link;
        }

        public string getHeaderKey(int index)
        {
            if (Header != null && Header.Count > index)
            {
                return Header[index].key;
            }
            else
            {
                return "";
            }
        }

        public string getHeaderValue(int index)
        {
            if (Header != null && Header.Count > index)
            {
                return Header[index].value;
            }
            else
            {
                return "";
            }
        }
    }

    public class SendRequestHeader 
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
