using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace lib.Class
{
  public class Compression
  {
    public static byte[] CompressToByte(byte[] data)
    {
      MemoryStream output = new MemoryStream();
      GZipStream gzip = new GZipStream(output, CompressionMode.Compress, true);
      gzip.Write(data, 0, data.Length);
      gzip.Close();
      return output.ToArray();
    }

    public static byte[] DecompressToByte(byte[] data)
    {
      //Armazena dados compactados no memorystream
      MemoryStream input = new MemoryStream();
      input.Write(data, 0, data.Length);
      input.Position = 0;
      //Descompacta o Stream
      GZipStream gzip = new GZipStream(input, CompressionMode.Decompress, true);
      //Lê stream compactado e armazena em um novo MemoryStream
      MemoryStream output = new MemoryStream();
      byte[] buff = new byte[64];
      int read = -1;
      read = gzip.Read(buff, 0, buff.Length);
      while (read > 0)
      {
        output.Write(buff, 0, read);
        read = gzip.Read(buff, 0, buff.Length);
      }
      gzip.Close();
      return output.ToArray();
    }

    public static string TextCompress(string text)
    {
      byte[] buffer = Encoding.UTF8.GetBytes(text);
      MemoryStream ms = new MemoryStream();
      using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
      {
        zip.Write(buffer, 0, buffer.Length);
      }

      ms.Position = 0;
      MemoryStream outStream = new MemoryStream();

      byte[] compressed = new byte[ms.Length];
      ms.Read(compressed, 0, compressed.Length);

      byte[] gzBuffer = new byte[compressed.Length + 4];
      System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
      System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
      return Convert.ToBase64String(gzBuffer);
    }

    public static string TextDecompress(string compressedText)
    {
      byte[] gzBuffer = Convert.FromBase64String(compressedText);
      using (MemoryStream ms = new MemoryStream())
      {
        int msgLength = BitConverter.ToInt32(gzBuffer, 0);
        ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

        byte[] buffer = new byte[msgLength];

        ms.Position = 0;
        using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
        {
          zip.Read(buffer, 0, buffer.Length);
        }
        return Encoding.UTF8.GetString(buffer);
      }
    }
  }
}
