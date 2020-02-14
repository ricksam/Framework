using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace lib.Class
{
  public static class FileUtils
  {
    public delegate void FileProcessing_Handle(string FileName);

    static uint GENERIC_READ = 0x80000000;
    static uint GENERIC_WRITE = 0x40000000;
    static uint OPEN_EXISTING = 3;
    static uint FILE_ATTRIBUTE_NORMAL = 0x80;
    static uint INVALID_HANDLE_VALUE = 0xffffffff;

    [DllImport("kernel32.dll", EntryPoint = "CreateFileA")]
    static extern uint CreateFileA(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
                                       uint lpSecurityAttributes,
                                       uint dwCreationDisposition, uint dwFlagsAndAttributes,
                                       uint hTemplateFile);

    [DllImport("kernel32.dll", EntryPoint = "CloseHandle")]
    static extern int CloseHandle(uint hObject);
    
    public static bool InUse(string File)
    {
      if (!System.IO.File.Exists(File))
      { return false; }
      uint h = CreateFileA(File, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);
      bool r = (h == INVALID_HANDLE_VALUE);
      if (!r)
      { CloseHandle(h); }
      return r;
    }
    
    public static string Version(string File) 
    {
      FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(File);
      return fvi.FileVersion;
    }
    
    public static void RemoveFiles(string DirectoryName)
    {
      RemoveFiles(null, DirectoryName);
    }

    public static void RemoveFiles(DirectoryInfo d) 
    {
      RemoveFiles(null, d);
    }

    public static void RemoveFiles(FileProcessing_Handle Handle, string DirectoryName) 
    {
      DirectoryInfo d = new DirectoryInfo(DirectoryName);
      RemoveFiles(Handle, d);
    }
    public static void RemoveFiles(FileProcessing_Handle Handle, DirectoryInfo d)
    {      
      // Obtém a lista de arquivos dessa subpasta
      FileInfo[] arquivos = d.GetFiles();

      // Percorre a lista de arquivos da subpasta
      foreach (FileInfo a in arquivos)
      {
        try
        {
          if (Handle != null)
          { Handle(a.Name); }

          // O arquivo está marcado como ReadOnly?
          if ((a.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
          {
            // Sim! Então remove esse atributo...
            a.Attributes ^= FileAttributes.ReadOnly;
          }

          // Apaga o arquivo
          a.Delete();
        }
        catch { continue; }
      }
    }
    
    public static void RemoveDirectory(string DirectoryName) 
    {
      RemoveDirectory(null, DirectoryName);
    }
 
    public static void RemoveDirectory(DirectoryInfo d) 
    {
      RemoveDirectory(null, d);
    }

    public static void RemoveDirectory(FileProcessing_Handle Handle, string DirectoryName) 
    {
      DirectoryInfo d = new DirectoryInfo(DirectoryName);
      RemoveDirectory(Handle, d);
    }

    public static void RemoveDirectory(FileProcessing_Handle Handle, DirectoryInfo Directory)
    {
      if (Handle != null)
      { Handle(Directory.Name); }
      // Obtém as subspastas da pasta atual
      DirectoryInfo[] subpastas = Directory.GetDirectories();

      // Percorre a lista de subpastas
      foreach (DirectoryInfo d in subpastas)
      {
        try
        {
          RemoveFiles(Handle, d);

          // Limpa as subpastas da subpasta atual
          RemoveDirectory(Handle, d);

          // A subpasta está marcada como ReadOnly?
          if ((d.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
          {
            // Sim! Então remove esse atributo...
            d.Attributes ^= FileAttributes.ReadOnly;
          }

          // Finalmente, apaga a subpasta
          d.Delete();
        }
        catch { continue; }
      }

      // Finalmente apaga a pasta
      RemoveFiles(Handle, Directory);
      Directory.Delete();
    }

    public static void SaveAs(System.IO.Stream Stream, string FileName) 
    {
        var fileStream = File.Create(FileName);
        Stream.Seek(0, SeekOrigin.Begin);
        Stream.CopyTo(fileStream);
        fileStream.Close();
    }

    /*public static byte[] ZipFiles(List<ZipFile> files)
    {
        using (var outStream = new System.IO.MemoryStream())
        {
            using (var archive = new System.IO.Compression.ZipArchive(outStream, System.IO.Compression.ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var fileInArchive = archive.CreateEntry(file.FileName, System.IO.Compression.CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    {
                        entryStream.Flush();
                        file.Stream.CopyTo(entryStream);
                        file.Stream.Flush();
                    }
                }
            }

            using (var fileStream = new System.IO.MemoryStream())
            {
                outStream.Seek(0, System.IO.SeekOrigin.Begin);
                outStream.CopyTo(fileStream);
                return fileStream.ToArray();
            }
        }
    }*/
  }

  /*public class ZipFile
  {
      public System.IO.Stream Stream { get; set; }
      public string FileName { get; set; }
  }*/
}
