using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.Entity
{
  public class UpdateScript
  {
    public UpdateScript(string FileName, DbBase DbBase)
    {
      this.cnv = new lib.Class.Conversion();
      this.FileName = FileName;
      this.DbBase = DbBase;
    }

    public string FileName { get; set; }
    public DbBase DbBase { get; set; }
    lib.Class.Conversion cnv { get; set; }

    #region private int GetDbUpdateNumber()
    private int GetDbUpdateNumber()
    {
      System.Data.DataTable dt = null;
      try
      {
        DbBase.DbConnection.Open();
        dt = DbBase.DbConnection.GetSchema("Tables");
      }
      finally
      {
        DbBase.DbConnection.Close();
      }

      bool PossuitdUpdate = false;
      for (int i = 0; i < dt.Rows.Count; i++)
      {
        if (dt.Rows[i]["TABLE_NAME"].ToString().ToUpper() == "TB_UPDATE")
        {
          PossuitdUpdate = true;
          break;
        }
      }

      if (!PossuitdUpdate)
      {
        DbBase.DbExecute("CREATE TABLE TB_UPDATE (UPDATE_NUMBER INTEGER);");
        DbBase.DbExecute("INSERT INTO TB_UPDATE (UPDATE_NUMBER) VALUES (0);");
      }

      return cnv.ToInt(DbBase.DbGetValue("SELECT UPDATE_NUMBER FROM TB_UPDATE"));
    }
    #endregion

    public void Execute()
    {

      lib.Class.TextFile tf = new lib.Class.TextFile();
      tf.Open(lib.Class.enmOpenMode.Reading, FileName);
      string[] lines = tf.GetLines();
      tf.Close();

      #region Gera a lista de comandos
      List<ItemUpdate> itms = new List<ItemUpdate>();
      for (int i = 0; i < lines.Length; i++)
      {
        try
        {
          if (string.IsNullOrEmpty(lines[i]))
          { continue; }

          string line = lines[i];

          if (DbBase is lib.Entity.MySQL)
          {
              line = lines[i].ToUpper();
          }
          
          if (line.IndexOf("/*[UPDATE]") != -1)
          {
            int idx_ini = line.IndexOf("/*[UPDATE]") + 10;
            int idx_end = line.IndexOf("*/");
            itms.Add(new ItemUpdate(cnv.ToInt(line.Substring(idx_ini, idx_end - idx_ini))));
          }
          else
          { itms[itms.Count - 1].Script += line; }
        }
        catch { continue; }
      }
      #endregion

      #region Executa atualização de Scripts
      int UpdateNumber = GetDbUpdateNumber();
      for (int i = 0; i < itms.Count; i++)
      {
        if (itms[i].Number > UpdateNumber)
        {
          try
          {
            string[] cmds = itms[i].GetCommands();
            for (int c = 0; c < cmds.Length; c++)
            {
              if (string.IsNullOrEmpty(cmds[c]))
              { continue; }
              DbBase.DbExecute(cmds[c]);
            }
          }
          catch(Exception ex)
          {
              string mensagem = ex.Message;
              mensagem = mensagem + "";
          }

          DbBase.DbExecute("UPDATE TB_UPDATE SET UPDATE_NUMBER = " + itms[i].Number);
        }
      }
      #endregion
    }
  }

  public class ItemUpdate
  {
    public ItemUpdate(int Number)
    {
      this.Number = Number;
      this.Script = "";
    }

    public int Number { get; set; }
    public string Script { get; set; }
    public string[] GetCommands()
    { return Script.Split(new char[] { ';' }); }
  }
}
