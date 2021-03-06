﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;
using System.Diagnostics;
using lib.Class;
using lib.Visual;

namespace RckInstaller
{
    static class Program
    {
        static frmInfo FormInfo { get; set; }
        static Conversion Cnv { get; set; }

        #region static void Main(string[] Args)
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            InstalaFrameworkNet4();
            Log Log = new Log(Functions.GetDirAppCondig() + "\\Update_" + DateTime.Now.ToString("yyMM") + ".log");

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Cnv = new Conversion();
                FormInfo = new frmInfo();
                FormInfo.setTitle("");
                FormInfo.setInfo("");
                FormInfo.VisibleControls(false);
                FormInfo.Show();

                if (Args.Length == 0)
                {
                    lib.Visual.Msg.Information(
                      "O RckInstaller necessita de um arquivo de configuração informado por parâmetro.\n" +
                      "Para criar um novo arquivo vazio informe por parâmetros a palavra \"new\""
                      );
                    return;
                }
                else if (Args.Length == 1)
                {
                    if (Args[0].ToUpper() == "NEW")
                    {
                        string ArqConfig = Functions.GetDirAppCondig() + "\\New_UpdateConfig.xml";
                        UpdateConfig u = new UpdateConfig(ArqConfig);
                        u.LinkVersao = "http://server/app.txt";
                        u.LinkUpdate = "http://server/app.exe";
                        u.AppLocal = "{pf}\\App\\App.exe";
                        u.AppParams = "-i";
                        u.AppText = "App";
                        u.AppSplash = "{localappdata}\\App\\Splash.jpg";
                        if (u.Save())
                        { Msg.Information("Arquivo de configuração salvo com sucesso em : " + ArqConfig); }
                    }
                    else if (File.Exists(Args[0])) // Informado um arquivo de configuração
                    {
                        #region Carrega configurações
                        string ArqConfig = Args[0];

                        System.Threading.Thread.Sleep(20);

                        Random rnd = new Random();
                        int iWait = rnd.Next(1, 500);
                        System.Threading.Thread.Sleep(iWait);

                        while (FileUtils.InUse(ArqConfig))
                        { System.Threading.Thread.Sleep(20); }

                        UpdateConfig u = new UpdateConfig(ArqConfig);
                        u.Open();

                        string pf = "";

                        if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)))
                        { pf = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles); }

                        u.AppLocal = u.AppLocal.Replace("{pf}", pf);
                        u.AppSplash = u.AppSplash.Replace("{pf}", pf);
                        u.AppParams = u.AppParams.Replace("{pf}", pf);

                        string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        u.AppLocal = u.AppLocal.Replace("{localappdata}", LocalAppData);
                        u.AppSplash = u.AppSplash.Replace("{localappdata}", LocalAppData);
                        u.AppParams = u.AppParams.Replace("{localappdata}", LocalAppData);

                        FormInfo.Text = u.AppText;
                        FormInfo.setBackground(u.AppSplash);
                        FormInfo.setTitle("Carregando ... ");
                        FormInfo.setInfo("Aguarde ... ");
                        FormInfo.Refresh();

                        for (int i = 0; i < u.InitialTime; i = i + 1000)
                        {
                            System.Threading.Thread.Sleep(1000);
                            Application.DoEvents();
                            int Perc = Cnv.ToInt(((decimal)i / (decimal)u.InitialTime) * 100);
                            FormInfo.setProgress(Perc);
                            FormInfo.setInfo("Aguarde ... " + Perc.ToString("0") + "%");
                        }
                        #endregion

                        if (Instance.RunningInstance(u.AppLocal))
                        { return; }

                        if ((new WebUtils()).InternetOnLine("http://www.rcksoftware.com.br/"))
                        {
                            #region Carrega Versão
                            string AppVersao = "";

                            if (File.Exists(u.AppLocal))
                            { AppVersao = FileUtils.Version(u.AppLocal); }

                            string VersaoSite = LoadUrlVersao(u.LinkVersao);
                            VersaoSite = getVersao(VersaoSite);
                            bool bVersion = IsVersion(VersaoSite);
                            #endregion

                            if (!string.IsNullOrEmpty(VersaoSite) && bVersion && VersaoSite != AppVersao)
                            {
                                #region Baixa e executa a atualização
                                FormInfo.VisibleControls(true);
                                FormInfo.setTitle("Baixando atualização ... ");

                                u.LinkUpdate = u.LinkUpdate.Replace("{version}", VersaoSite);
                                string AppName = System.IO.Path.GetFileName(u.LinkUpdate);
                                string Destino = lib.Visual.Functions.GetDirAppCondig() + "\\" + AppName;

                                bool bDownload = DownloadUpdate(u.LinkUpdate, Destino);

                                if (!bDownload)
                                {
                                    bDownload = DownloadUpdate(u.LinkUpdate + ".zip", Destino);
                                }

                                if (bDownload)
                                {
                                    FormInfo.Close();
                                    //Executa atualização
                                    if (System.IO.File.Exists(Destino))
                                    {
                                        string ext = System.IO.Path.GetExtension(Destino).ToUpper();
                                        if (ext != ".EXE")
                                        {
                                            if (System.IO.File.Exists(Destino + ".exe"))
                                            { System.IO.File.Delete(Destino + ".exe"); }

                                            System.IO.File.Move(Destino, Destino + ".exe");
                                            Destino += ".exe";
                                        }

                                        lib.Class.Instance.ExecProcess(Destino, "/silent", true);
                                    }
                                }
                                else
                                { FormInfo.Close(); }
                                #endregion
                            }
                            else
                            { FormInfo.Close(); }
                        }//if ((new WebUtils()).InternetOnLine())

                        if (System.IO.File.Exists(u.AppLocal))
                        { lib.Class.Instance.ExecProcess(u.AppLocal, u.AppParams, false); }
                    }
                    else { Log.Save("Parâmetro inválido ou arquivo não existe" + Args[0]); }
                }
                else { Log.Save("Quantidade de parâmetros inválidos" + Args.Length); }
            }
            catch (Exception ex)
            { Log.Save(ex); }
            //Application.Run(new Form1());
        }//static void Main(string[] Args)
        #endregion

        #region static bool IsVersion(string Text)
        /// <summary>
        /// Verifica se o conteúdo é uma string de versão
        /// </summary>
        /// <returns></returns>
        static bool IsVersion(string Text)
        {
            string s = "";
            for (int i = 0; i < Text.Length; i++)
            {
                char c = Text[i];
                if (!char.IsNumber(c) && c != '.')
                { return false; }
                else { s += c.ToString(); }
            }
            return s.Length != 0;
        }
        #endregion

        #region static string getVersao(string s)
        static string getVersao(string s)
        {
            string v = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsNumber(s[i]) || s[i] == '.')
                { v += s[i].ToString(); }
                else { break; }
            }
            return v;
        }
        #endregion

        #region static string LoadUrlVersao(string LinkVersao)
        static string LoadUrlVersao(string LinkVersao)
        {
            try
            {
                HttpWebRequest req = ((HttpWebRequest)HttpWebRequest.Create(LinkVersao));
                HttpWebResponse resp = ((HttpWebResponse)req.GetResponse());
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                return sr.ReadToEnd();
            }
            catch { return ""; }
        }
        #endregion

        #region static bool DownloadUpdate(string Origem, string Destino)
        static bool DownloadUpdate(string Origem, string Destino)
        {
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(Destino, FileMode.Create,
                                  FileAccess.Write, FileShare.None);

                WebClient wc = new WebClient();
                Stream st = wc.OpenRead(Origem);

                decimal len = Cnv.ToDecimal(wc.ResponseHeaders["Content-Length"].ToString());

                decimal totalSize = 0;
                int bytesSize = 0;
                byte[] downBuffer = new byte[2048];

                int Tentativas = 0;
                while (totalSize < len)
                {
                    bytesSize = st.Read(downBuffer, 0, downBuffer.Length);
                    if (bytesSize == 0)
                    {
                        Tentativas++;
                        if (Tentativas >= 10)
                        { break; }
                    }

                    fileStream.Write(downBuffer, 0, bytesSize);
                    totalSize += bytesSize;
                    int Perc = Cnv.ToInt((totalSize / len) * 100);
                    FormInfo.setProgress(Perc);
                    FormInfo.setInfo("Baixando " + (totalSize / 1000000).ToString("0.000") + " de " + (len / 1000000).ToString("0.000") + " Mb. " + Perc.ToString("0") + "%");

                    if (FormInfo.PodeCancelar)
                    { return false; }
                }

                return true;
            }
            catch (Exception ex)
            {
                (new lib.Class.Log(lib.Visual.Functions.GetDirAppCondig() + "\\LogError.log")).Save(ex);
                return false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }
        #endregion

        #region static void InstalaFrameworkNet4()
        static void InstalaFrameworkNet4()
        {
            try
            {
                string dir = Environment.GetEnvironmentVariable("windir") + "\\Microsoft.NET\\Framework";
                //bool sim = System.IO.File.Exists(dir + "\\v4.0*\\System.Web.Extensions.dll");

                bool Framework4Installed = false;
                string[] dirs = System.IO.Directory.GetDirectories(dir);
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (dirs[i].IndexOf("v4.0") != -1)
                    {
                        string[] files = System.IO.Directory.GetFiles(dirs[i]);
                        for (int j = 0; j < files.Length; j++)
                        {
                            if (System.IO.Path.GetFileName(files[j]) == "System.Web.Extensions.dll")
                            {
                                Framework4Installed = true;
                                break;
                            }
                        }
                        break;
                    }
                }

                if (!Framework4Installed)
                {
                    string fileInstallF4 = Application.StartupPath + "\\dotNetFx40_Full_setup.exe";
                    //string fileInstallF4 = Functions.GetDirAppCondig() + "\\dotNetFx40_Full_x86_x64.exe";
                    if (System.IO.File.Exists(fileInstallF4))
                    {
                        lib.Class.Instance.ExecProcess(fileInstallF4, "", false);
                        AguardaInstalacao("dotNetFx40_Full_setup.exe");
                    }
                    else
                    { MessageBox.Show("Não foi disponibilizado a instalação do .net"); }
                }
                //MessageBox.Show(Framework4Installed.ToString());
            }
            catch { return; }
        }
        #endregion

        #region private static void AguardaInstalacao(string ProcessName)
        private static void AguardaInstalacao(string ProcessName)
        {
            while (true)
            {
                string[] process = Instance.GetProcess();
                bool ProcessoRodando = false;
                for (int i = 0; i < process.Length; i++)
                {
                    if (process[i] == ProcessName)
                    {
                        ProcessoRodando = true;
                        break;
                    }
                }

                if (ProcessoRodando)
                { System.Threading.Thread.Sleep(1000); }
                else
                { break; }
            }
        }
        #endregion
    }
}
