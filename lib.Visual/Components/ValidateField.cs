using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace lib.Visual.Components
{
  #region public class ValidadeField
  /// <summary>
  /// Esta classe é responsável por fazer a validação de campos
  /// </summary>
  public class ValidateField
  {
    private List<ItemField> Fields = new List<ItemField>();

    public void Clear()
    { Fields.Clear(); }

    public void Add(Control aControl, string aMessage, bool aBloquote)
    { Fields.Add(new ItemField(aControl, aMessage, aBloquote)); }

    public bool Blocked(string MsgAlert)
    {
      bool bloq = false;

      for (int i = 0; i < Fields.Count; i++)
      {
        if (Fields[i].Bloq)
        {
          bloq = true;
          Fields[i].SelControl.Focus();
          break;
        }
      }

      string xMsg = "";
      for (int i = 0; i < Fields.Count; i++)
      {
        if (Fields[i].Bloq)
        { xMsg += Fields[i].Message + "\n"; }
      }

      if (xMsg != "")
      { Msg.Warning(MsgAlert + "\n" + xMsg); }

      return bloq;
    }
  }
  #endregion

  #region public class ItemField
  public class ItemField
  {
    public ItemField(Control aSelControl, string aMessage, bool aBloq)
    {
      SelControl = aSelControl;
      Message = aMessage;
      Bloq = aBloq;
    }

    public Control SelControl { get; set; }
    public string Message { get; set; }
    public bool Bloq { get; set; }
  }
  #endregion
}
