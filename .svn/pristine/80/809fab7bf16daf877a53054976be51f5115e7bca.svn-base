using System;
using System.Collections.Generic;
using System.Text;
using lib.Class;

namespace lib.Database
{
  /// <summary>
  /// Classe utilizada para gerar atualização do banco de dados
  /// </summary>
  public class DbUpdate
  {
    public DbUpdate()
    {
      this.Cnv = new Conversion();
      this.Items = new List<ItemUpdate>();
      this.Log = new List<ItemLog>();
    }

    public delegate void OnExecute_Handle(int UpdateNumber, int MaxUpdateNumber, int SqlNumber, int MaxSqlNumber);

    #region Fields
    public const string IniStr = "/*[UPDATE]";
    public const string EndStr = "*/";
    private Conversion Cnv { get; set; }
    public string FileName { get; private set; }
    public List<ItemUpdate> Items { get; set; }
    public List<ItemLog> Log { get; set; }
    public OnExecute_Handle OnExecute { get; set; }
    public Connection Connection { get; set; }
    #endregion

    #region private int GetNumber(string line)
    /// <summary>
    /// Retorna o número do script conforme a linha /*[UPDATE]{0}*/
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    private int GetNumber(string line) 
    {
      return Cnv.ToInt(line.Replace(IniStr, "").Replace(EndStr, ""));
    }
    #endregion

    #region public void Load(string File)
    /// <summary>
    /// Carrega o arquivo para o array de itens
    /// </summary>
    public void Load(string FileName) 
    {
      this.FileName = FileName;
      TextFile TextFile = new TextFile();
      TextFile.Open(enmOpenMode.Reading, FileName);
      string[] lines = TextFile.GetLines();
      TextFile.Close();

      string buffer = "";
      for (int i = 0; i < lines.Length; i++)
      {
        if (lines[i].IndexOf(IniStr) != -1)
        { Items.Add(new ItemUpdate(GetNumber(lines[i]), true)); }
        else
        {
          if (Items.Count != 0)
          {
            buffer += lines[i] + " ";
            if (lines[i].IndexOf(';') != -1)
            {
              Items[Items.Count - 1].Sql.Add(buffer);
              buffer = "";
            }
          }
        }
      }

      if (!string.IsNullOrEmpty(buffer.Trim()))
      { Items[Items.Count - 1].Sql.Add(buffer); }
    }
    #endregion

    #region public void PrepareDb()
    /// <summary>
    /// Cria a tabela TB_UPDATE se esta não existir
    /// </summary>
    public void PrepareDb() 
    {
      if (!Connection.TableExists("TB_UPDATE"))
      {
        Connection.Exec("CREATE TABLE TB_UPDATE(UPDATE_NUMBER INTEGER);");
        if (!Connection.TableExists("TB_UPDATE"))
        { throw new Exception("Erro ao criar a tabela de atualização"); }
      }

      if (!Connection.TableExists("TB_LOG_UPDATE"))
      {
        Connection.Exec("CREATE TABLE TB_LOG_UPDATE(UPDATE_DATA VARCHAR(10), UPDATE_NUMBER INTEGER, UPDATE_MESSAGE VARCHAR(400));");
        if (!Connection.TableExists("TB_LOG_UPDATE"))
        { throw new Exception("Erro ao criar a tabela do log da atualização"); }
      }
    }
    #endregion

    #region public int GetDbUpdateNumber()
    /// <summary>
    /// Retorna o campo TB_UPDATE.UPDATE_NUMBER
    /// </summary>
    public int GetDbUpdateNumber() 
    {
      PrepareDb();
      return Connection.Sql("SELECT UPDATE_NUMBER FROM TB_UPDATE").ToInt();
    }
    #endregion

    #region public void SetDbUpdateNumber(int Number)
    /// <summary>
    /// Atualiza o campo TB_UPDATE.UPDATE_NUMBER
    /// </summary>
    public void SetDbUpdateNumber(int Number) 
    {
      if (Number == 0)
      { throw new Exception("O número da atualização não pode ser zero"); }

      int DbNum = GetDbUpdateNumber();
      if (DbNum == 0)
      { Connection.Exec(string.Format("INSERT INTO TB_UPDATE (UPDATE_NUMBER) VALUES ({0})", Number.ToString())); }
      else
      { Connection.Exec(string.Format("UPDATE TB_UPDATE SET UPDATE_NUMBER = {0}", Number.ToString())); }
    }
    #endregion

    #region public bool HasUpdate()
    public bool HasUpdate()
    {
      int LastDbNum = GetDbUpdateNumber();
      int LastItemNum = 0;
      if (Items.Count != 0)
      { LastItemNum = Items[Items.Count - 1].Number; }
      return LastDbNum < LastItemNum;
    }
    #endregion

    #region public void Exec()
    /// <summary>
    /// Executa o arquivo de script
    /// </summary>
    public void Exec()
    {
      int LastNumber = GetDbUpdateNumber();
      for (int i = 0; i < Items.Count; i++)
      {
        if (Items[i].Number > LastNumber) 
        {
          for (int j = 0; j < Items[i].Sql.Count; j++)
          {
            if (OnExecute != null)
            { OnExecute(i, Items.Count, j, Items[i].Sql.Count); }

            try
            { Connection.Exec(Items[i].Sql[j]); }
            catch(Exception ex)
            {
              if (ex.InnerException != null)
              { Log.Add(new ItemLog(Items[i].Number, ex.InnerException.Message)); }
              else
              { Log.Add(new ItemLog(Items[i].Number, ex.Message)); }
            }
          }
        }
        SetDbUpdateNumber(Items[i].Number);
      }

      if (Log.Count != 0)
      { SaveLog(); }
    }
    #endregion

    #region public void Save()
    /// <summary>
    /// Salva no arquivo os itens não bloqueados
    /// </summary>
    public void Save() 
    {
      TextFile TextFile = new TextFile();
      TextFile.Open(enmOpenMode.Writing, this.FileName);
      for (int i = 0; i < Items.Count; i++)
      {
        if (!Items[i].Block)
        {
          TextFile.WriteLine(Items[i].GetHeader("/*[UPDATE]{0}*/"));
          TextFile.WriteLine(Items[i].GetContent());
        }
      }
      TextFile.Close();
    }
    #endregion

    #region private void SaveLog()
    /// <summary>
    /// Salva os logs de erros no banco de dados
    /// </summary>
    private void SaveLog() 
    {
      PrepareDb();
      

      for (int i = 0; i < Log.Count; i++)
      {
        if (Log[i].Message.Length > 400)
        { Log[i].Message = Log[i].Message.Substring(0, 400); }

        Connection.Exec(
          string.Format(
          "INSERT INTO TB_LOG_UPDATE(UPDATE_DATA, UPDATE_NUMBER, UPDATE_MESSAGE) VALUES({0},{1},{2})",
          Connection.dbu.Quoted(DateTime.Now.ToString("yyyy-MM-dd")),
          Log[i].Number,
          Connection.dbu.Quoted(Log[i].Message))
        );
      }
    }
    #endregion

    #region public void ExportLogs(string FileName)
    public void ExportLogs(string FileName)
    {
      TextFile tf = new TextFile();
      tf.Open(enmOpenMode.Writing, FileName);
      for (int i = 0; i < Log.Count; i++)
      { tf.WriteLine(string.Format("UPDATE_NUMBER:{0}, UPDATE_MESSAGE:{1}", Log[i].Number.ToString("000000"), Log[i].Message)); }
      tf.Close();
    }
    #endregion
  }

  #region public class ItemUpdate
  public class ItemUpdate 
  {
    public ItemUpdate(int Number, bool Block) 
    {
      this.Number = Number;
      this.Sql = new List<string>();
      this.Block = Block;
    }
    public int Number { get; set; }
    public List<string> Sql { get; set; }
    public bool Block { get; set; }

    public string GetHeader(string Format) 
    {
      return string.Format(Format, Number.ToString());
    }

    public string GetContent() 
    {
      string Conteudo = "";
      for (int i = 0; i < Sql.Count; i++)
      { Conteudo += Sql[i]; }
      return Conteudo;
    }
  }
  #endregion

  #region public class ItemLog
  public class ItemLog 
  {
    public ItemLog(int Number, string Message) 
    {
      this.Number = Number;
      this.Message = Message;
    }
    public int Number { get; set; }
    public string Message { get; set; }
  }
  #endregion
}
