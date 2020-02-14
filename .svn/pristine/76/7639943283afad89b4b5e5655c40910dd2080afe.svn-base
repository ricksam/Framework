using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace lib.Class.Sock
{
  #region public class ClientSock
  public class ClientSock
  {
    public ClientSock() 
    {
      m_Client = null;
      ProcessClient = null;
      _Resp = "";
      _CmdEnd = "";
    }

    #region Fields
    public Socket m_Client { get; set; }
    Thread ProcessClient { get; set; }
    string _Resp { get; set; }
    string _CmdEnd { get; set; }
    int _data_size = 256;
    #endregion

    #region Methods
    void RecebendoDados()
    {
      while (true)
      {
        try
        {
          String s = "";
          byte[] bites = new byte[_data_size];

          m_Client.Receive(bites);

          s = Encoding.ASCII.GetString(bites);

          if (s != "")
          {
            _Resp += s;
            if (s == _CmdEnd)
            {
              Stop();
              break; 
            }
          }
        }
        catch { break; }
      }//while (true)
    }

    public bool Start(string IP, int Port, string CmdEnd, int DataSize, ProtocolType ProtocolType)
    {
      try
      {
        if (DataSize != 0)
        { _data_size = DataSize; }

        _CmdEnd = CmdEnd;

        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
        
        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType);
        m_Client.Connect(localEndPoint);

        ProcessClient = new Thread(new ThreadStart(RecebendoDados));
        ProcessClient.Start();

        return true;
      }
      catch { return false; }
    }

    public void Stop()
    {
      if (m_Client != null)
      {
        if (m_Client.Connected)
        {
          m_Client.Shutdown(SocketShutdown.Both);
          m_Client.Disconnect(false);          
        }

        m_Client = null;
      }

      if (ProcessClient != null)
      {
        ProcessClient.Abort();
        ProcessClient = null;
      }

      GC.Collect();
    }

    public string Receive()
    {
      string s = _Resp.Trim().Replace("\0", "");
      _Resp = "";
      return s;
    }

    public void Send(string Command)
    {
      byte[] byData = System.Text.Encoding.ASCII.GetBytes(Command);
      m_Client.Send(byData);
    }
    #endregion
  }
  #endregion

  public delegate void ReceiveClient(VirtualClient Client);

  #region public class ServerSock
  /// <summary>
  /// Classe Sock do servidor, responsável por ouvir os clientes
  /// </summary>
  public class ServerSock
  {
    Socket m_Server;
    string _DisconnectCommand = "";
    int _TimeOut = 1;
    List<VirtualClient> Clients { get; set; }
    System.Threading.Thread Processo { get; set; }
    int _data_size { get; set; }
    public DateTime DtLastClient { get; set; }
    public ReceiveClient ReceiveClient { get; set; }
    public bool RemoveIpConflict { get; set; }

    #region public string Address(int Index)
    public string Address(int Index)
    {
      if (Clients.Count > Index)
      { return Clients[Index].Address; }
      else
      { return ""; }
    }
    #endregion

    #region void RecebendoClients()
    void RecebendoClients()
    {
      while (true)
      {
        try
        {
          if (_data_size == 0)
          { _data_size = 256; }

          Socket m_Client;          
          if ((m_Client = m_Server.Accept()) != null)
          {
            DtLastClient = DateTime.Now;
            RemoveConflito(m_Client);
            
            VirtualClient Client = new VirtualClient(m_Client, _DisconnectCommand, _TimeOut, _data_size);
            if (ReceiveClient != null)
            { ReceiveClient(Client); }

            Clients.Add(Client);
          }
        }
        catch { continue; }
      }
    }
    #endregion

    #region public void RemoveClient(int Index)
    public void RemoveClient(int Index)
    {
      if (Clients[Index] == null)
      {
        Clients.RemoveAt(Index);
        return;
      }
            
      Clients[Index].Stop();
      Clients.RemoveAt(Index);
    }
    #endregion

    void RemoveConflito(Socket m_Client) 
    {
      for (int i = 0; i < Clients.Count; i++)
      {
        if (Clients[i] == null || Clients[i].m_Client == null || !Clients[i].m_Client.Connected)
        {
          RemoveClient(i);
          i--;
          continue;
        }

        if (RemoveIpConflict)
        {
          string ArrAddress = Clients[i].Address;
          string ObjAddress = ((IPEndPoint)m_Client.RemoteEndPoint).Address.ToString();

          if (ArrAddress == ObjAddress)
          {
            RemoveClient(i);
            i--;
            continue;
          }
        }
      }
    }
    
    public void Start(string IP, int Port, string DisconnectCommand, int TimeOut = 1, int DataSize = 256, ProtocolType ProtocolType = ProtocolType.Tcp)
    {
      _data_size = DataSize;
      _DisconnectCommand = DisconnectCommand;
      _TimeOut = TimeOut;
      DtLastClient = DateTime.Now;

      Clients = new List<VirtualClient>();
      IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
      m_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType);
      m_Server.Bind(localEndPoint);
      m_Server.Listen(1000);

      Processo = new Thread(new ThreadStart(RecebendoClients));
      Processo.Start();
    }

    public void Stop()
    {
      if (Clients != null && Clients.Count != 0)
      {
        for (int i = 0; i < Clients.Count; i++)
        { RemoveClient(i); }
        Clients.Clear();
      }

      if (m_Server != null)
      {
        if (m_Server.Connected)
        { m_Server.Shutdown(SocketShutdown.Both); }

        m_Server.Close();
        m_Server = null;
      }

      Clients = null;

      if (Processo != null)
      {
        Processo.Abort();
        Processo = null;
      }
    }

    public string Receive(int Index)
    {
      return Clients[Index].Receive();
    }
    
    public bool IsLazy(int Index) 
    {
      return Clients[Index].IsLazy();
    }
    
    public byte[] ReceiveBytes(int Index)
    {
      return Clients[Index].ReceiveBytes();
    }

    public void Send(int Index, string Command)
    {
      Clients[Index].Send(Command);
    }

    public int ClientCount()
    {
      return Clients.Count;
    }

    public VirtualClient GetClient(int ID)
    {
      for (int i = 0; i < Clients.Count; i++)
      {
        if (Clients[i].ID == ID)
        { return Clients[i]; }
      }

      return null;
    }

    public void CleanInactiveClients() 
    {
      for (int i = 0; i < Clients.Count; i++)
      {
        if (!Clients[i].IsActive)
        {
          RemoveClient(i);
          i--;
        }
      }
    }

    public void CleanLazyClients()
    {
      for (int i = 0; i < Clients.Count; i++)
      {
        if (Clients[i].IsLazy())
        {
          RemoveClient(i);
          i--;
        }
      }
    }
  }
  #endregion

  #region class VirtualClient
  /// <summary>
  /// Esta classe é uma representação do client no servidor. É com esta classe que o servidor conversa com o cliente
  /// </summary>
  public class VirtualClient
  {
    public Socket m_Client;
    private int _TimeOut;
    private string _DisconnectCommand = "";
    private bool AbortProcess = false;
    private List<byte[]> Bytes = new List<byte[]>();
    Thread tr = null;
    int _data_size = 256;
    Conversion cnv = new Conversion();
    public string LastResponse = "";
    public bool IsActive = true;
    public int ID = 0;

    public string Address
    {
      get
      {
        if (m_Client != null && m_Client.Connected)
        { return ((IPEndPoint)m_Client.RemoteEndPoint).Address.ToString(); }

        return "";
      }
    }

    private void RecebendoDados()
    {
      while (!AbortProcess)
      {
        IsActive = true;
        try
        {
          string s = "";
          byte[] bites = new byte[_data_size];

          if (m_Client.Connected)
          {
            this.LastResponse = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            m_Client.Receive(bites); 
          }

          s = Encoding.ASCII.GetString(bites);

          if (!string.IsNullOrEmpty(s.Replace("\0", "")))
          {
            Bytes.Add(bites);
            //_Resp += s;
            string comp = s.Replace("\0", "").Replace(" ", "");
            //if (comp.IndexOf(_DisconnectCommand) != -1)
            //{ 
              break; 
            //}
          }
        }
        catch (Exception ex){ break; }
      }//while (true)

      IsActive = false;
    }

    public VirtualClient(Socket am_Client, string DisconnectCommand, int TimeOut, int DataSize)
    {
      if (TimeOut == 0)
      { TimeOut = 1; }
      
      this._TimeOut = TimeOut;
      this._DisconnectCommand = DisconnectCommand;
      this.LastResponse = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
      if (DataSize != 0)
      { this._data_size = DataSize; }

      this.m_Client = am_Client;
      this.tr = new Thread(new ThreadStart(RecebendoDados));
      this.tr.Start();
    }

    public void Stop()
    {
      AbortProcess = true;

      tr.Abort();
      tr = null;

      if (m_Client.Connected)
      { m_Client.Close(); }

      if (m_Client != null)
      { m_Client = null; }
    }

    public string Receive()
    {
      //string ret = _Resp.Trim().Replace("\0", "");
      //_Resp = "";
      //return ret;

      string Resp = "";
      while (Bytes.Count != 0) 
      {
        if (Bytes[0] != null)
        { Resp += Encoding.ASCII.GetString(Bytes[0]).Replace("\0", ""); }
        Bytes.RemoveAt(0);
      }
      return Resp;
    }

    public byte[] ReceiveBytes() 
    {
      if (Bytes.Count != 0)
      {
        this.LastResponse = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        byte[] bt = Bytes[0];
        Bytes.RemoveAt(0);
        return bt;
      }

      return new byte[] { };
    }

    public void Send(string Command)
    {
      byte[] byData = System.Text.Encoding.ASCII.GetBytes(Command);
      m_Client.Send(byData);
    }

    public bool IsLazy() 
    {
      DateTime dt = cnv.ToDateTime(this.LastResponse);
      TimeSpan ts = DateTime.Now.Subtract(dt);
      return ts.TotalSeconds > this._TimeOut;
    }
  }
  #endregion
}
