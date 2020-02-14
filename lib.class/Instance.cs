using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace lib.Class
{
    public static class Instance
    {
        #region public static bool RunningInstance()
        /// <summary>
        /// Verifica se já existe uma instância da aplicação rodando
        /// </summary>
        /// <returns></returns>
        public static bool RunningInstance()
        {
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);
                return (processes.Length > 1);
            }
            catch { return false; }
        }
        #endregion

        #region public static bool RunningInstance(string ProcessOrFileName)
        public static bool RunningInstance(string ProcessOrFileName)
        {
            if (System.IO.File.Exists(ProcessOrFileName))
            { ProcessOrFileName = System.IO.Path.GetFileNameWithoutExtension(ProcessOrFileName); }

            Process[] processes = Process.GetProcessesByName(ProcessOrFileName);
            return (processes.Length != 0);
        }
        #endregion

        #region public static void ExecProcess(String ProcessName, String Arguments, bool WaitForExit)
        /// <summary>
        /// Executa um processo
        /// </summary>
        /// <param name="ProcessName"></param>
        /// <param name="Arguments"></param>
        /// <param name="WaitForExit"></param>
        public static void ExecProcess(String ProcessName, String Arguments, bool WaitForExit)
        {
            System.Diagnostics.Process pr = new System.Diagnostics.Process();

            if (Arguments != "")
            { pr.StartInfo = new System.Diagnostics.ProcessStartInfo(ProcessName, Arguments); }
            else
            { pr.StartInfo = new System.Diagnostics.ProcessStartInfo(ProcessName); }

            pr.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(ProcessName);
            pr.Start();
            if (WaitForExit)
            { pr.WaitForExit(); }
        }
        #endregion

        #region public static string[] GetProcess()
        public static string[] GetProcess(string MachineName = "")
        {
            Process[] lst = null;
            if (string.IsNullOrEmpty(MachineName))
            { lst = Process.GetProcesses(); }
            else
            { lst = Process.GetProcesses(MachineName); }

            string[] process = new string[lst.Length];
            for (int i = 0; i < lst.Length; i++)
            { process[i] = lst[i].ProcessName; }
            return process;
        }
        #endregion

        #region public static string[] GetModules(string ProcessName, string MachineName = "")
        public static string[] GetModules(string ProcessName, string MachineName = "")
        {
            Process[] lst = null;
            if (string.IsNullOrEmpty(MachineName))
            { lst = Process.GetProcessesByName(ProcessName); }
            else
            { lst = Process.GetProcessesByName(ProcessName, MachineName); }

            List<string> Ret = new List<string>();
            for (int i = 0; i < lst.Length; i++)
            {
                ProcessModuleCollection Modules = lst[i].Modules;
                for (int j = 0; j < Modules.Count; j++)
                { Ret.Add(Modules[j].FileName); }
            }
            return Ret.ToArray();
        }
        #endregion

        #region public static bool CloseProcess(string ProcessName, string MachineName = "")
        public static bool CloseProcess(string ProcessName, string MachineName = "")
        {
            try
            {
                Process[] lst = null;
                if (string.IsNullOrEmpty(MachineName))
                { lst = Process.GetProcessesByName(ProcessName); }
                else
                { lst = Process.GetProcessesByName(ProcessName, MachineName); }

                for (int i = 0; i < lst.Length; i++)
                { lst[i].CloseMainWindow(); }
                return true;
            }
            catch
            { return false; }
        }
        #endregion

        #region public static bool KillProcess(string ProcessName, string MachineName = "")
        public static bool KillProcess(string ProcessName, string MachineName = "")
        {
            try
            {
                Process[] lst = null;
                if (string.IsNullOrEmpty(MachineName))
                { lst = Process.GetProcessesByName(ProcessName); }
                else
                { lst = Process.GetProcessesByName(ProcessName, MachineName); }

                for (int i = 0; i < lst.Length; i++)
                { lst[i].Kill(); }
                return true;
            }
            catch
            { return false; }
        }
        #endregion
    }
}

