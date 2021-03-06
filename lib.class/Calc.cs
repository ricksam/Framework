﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace lib.Class
{
  /// <summary>
  /// O objetivo desta classe é calcular expressões
  /// </summary>

    

  public class Calc
  {
      public Calc()
      {
          Variables = new List<VarCalc>();
          CmdInFixa = new List<string>();
          CmdPosFixa = new List<string>();
      }
    

    #region Fields
    private List<VarCalc> Variables { get; set; }
    private List<string> CmdInFixa { get; set; }
    private List<string> CmdPosFixa { get; set; }
    #endregion

    #region private bool IsVariable(string VarName)
    private bool IsVariable(string VarName)
    {
      return VariableIndex(VarName) != -1;
    }
    #endregion

    #region private int VariableIndex(string VarName)
    private int VariableIndex(string VarName)
    {
      for (int i = 0; i < Variables.Count; i++)
      {
        if (VarName == Variables[i].Name)
        { return i; }
      }
      return -1;
    }
    #endregion

    #region public String GetVarible(string VarName)
    private decimal GetVarible(string VarName)
    {
      int idx = VariableIndex(VarName);
      if (idx != -1)
      { return Variables[idx].Value; }
      else
      { return 0; }
    }
    #endregion

    #region private void SetInFixa(string Expression)
    private void SetInFixa(string Expression)
    {
      CmdCalc calc = new CmdCalc(Expression);
      string xcmd = "";
      CmdInFixa.Clear();
      while ((xcmd = calc.NextCmd()) != "")
      { CmdInFixa.Add(xcmd); }
    }
    #endregion

    #region private bool IsOperator(char c)
    private bool IsOperator(char c)
    {
      return !char.IsNumber(c) && !char.IsLetter(c);
    }
    #endregion

    #region private void SetPosFixa()
    private void SetPosFixa()
    {
      Stack pilha = new Stack();
      int prioridade = 0;

      for (int i = 0; i < CmdInFixa.Count; i++)
      {
        string cmd = CmdInFixa[i];

        if (cmd.Trim() == "")
        { continue; }

        if ("(" == cmd)
        { pilha.Push(cmd); }
        else if (")" == cmd)
        {
          String item = pilha.Pop().ToString();
          while (!item.Equals("("))
          {
            CmdPosFixa.Add(item);
            item = pilha.Pop().ToString();
          }
        }
        else if (IsOperator(cmd[0]))
        {
          prioridade = GetPriority(cmd[0]);
          while ((pilha.Count != 0) && (GetPriority(Convert.ToChar(pilha.Peek())) >= prioridade))
          { CmdPosFixa.Add(pilha.Pop().ToString()); }
          pilha.Push(cmd);
        }
        else
        { CmdPosFixa.Add(cmd); }
      }

      while (pilha.Count != 0)
      { CmdPosFixa.Add(pilha.Pop().ToString()); }
    }
    #endregion

    #region private int GetPriority(char caracter)
    private int GetPriority(char caracter)
    {
      int retorno = 0;
      String pri2 = "+-";
      String pri3 = "*/";
      if ('(' == caracter)
      {
        retorno = 1;
      }
      else if (pri2.IndexOf(caracter) >= 0)
      {
        retorno = 2;
      }
      else if (pri3.IndexOf(caracter) >= 0)
      {
        retorno = 3;
      }
      else if ('^' == caracter)
      {
        retorno = 4;
      }
      return retorno;
    }
    #endregion

    #region public void Clear()
    public void Clear()
    {
      Variables.Clear();
      CmdInFixa.Clear();
      CmdPosFixa.Clear();
    }
    #endregion

    #region public void AddVariable(string VarName, string VarValue)
    public void AddVariable(string VarName, decimal VarValue)
    { Variables.Add(new VarCalc(VarName, VarValue)); }
    #endregion    

    #region public void SetExpression(string Expression)
    public void SetExpression(string Expression) 
    {
      Expression = Expression.Replace(" ", "");
      if (Expression.Length != 0 && this.IsOperator(Expression[0]))
      { Expression = "0" + Expression; }
      SetInFixa(Expression);
      SetPosFixa();
    }
    #endregion

    #region public String GetPosFixa()
    public String GetPosFixa()
    {
      string Res = "";
      for (int i = 0; i < CmdPosFixa.Count; i++)
      { Res += CmdPosFixa[i]; }
      return Res;
    }
    #endregion

    #region public String GetResult()
    public decimal GetResult() 
    {
      Stack pilha = new Stack();
      for (int i = 0; i < CmdPosFixa.Count; i++)
      {
        string cmd = CmdPosFixa[i];

        if (cmd.Trim() == "") 
        { continue; }

        if (IsOperator(cmd[0]))
        {
          double y = Convert.ToDouble(pilha.Pop());
          double x = Convert.ToDouble(pilha.Pop());
          if (cmd == "+")
          { pilha.Push(Convert.ToString(x + y)); }
          else if (cmd == "-")
          { pilha.Push(Convert.ToString(x - y)); }
          else if (cmd == "*")
          { pilha.Push(Convert.ToString(x * y)); }
          else if (cmd == "/")
          { pilha.Push(Convert.ToString(x / y)); }
          else if (cmd == "^")
          { pilha.Push(Convert.ToString(Math.Pow(x, y))); }
        }
        else 
        {
          if (IsVariable(cmd))
          { pilha.Push(GetVarible(cmd)); }
          else
          { pilha.Push(cmd); }
        }
      }
      return Convert.ToDecimal(pilha.Pop().ToString());
    }
    #endregion
  }

  #region internal class VarCalc
  /// <summary>
  /// Esta classe define o modelo de variáveis do cálculo
  /// </summary>
  internal class VarCalc
  {
    public VarCalc(string Name, decimal Value) 
    {
      this.Name = Name;
      this.Value = Value;
    }

    public string Name { get; set; }
    public decimal Value { get; set; }
  }
  #endregion

  #region internal class CmdCalc
  /// <summary>
  /// Esta classe define os comandos necessário para os cálculos
  /// </summary>
  internal class CmdCalc
  {
    #region public CmdCalc(string Expression)
    public CmdCalc(string Expression) 
    {
      this.Expression = Expression;
    }
    #endregion

    #region private string Expression
    private string Expression { get; set; }
    #endregion

    #region private bool IsOperator(char c)
    private bool IsOperator(char c) 
    {
      return !char.IsNumber(c) && !char.IsLetter(c) && c != '.' && c != ',' && c != '_';
    }
    #endregion

    #region private bool PossuiLetras(string s)
    private bool PossuiLetras(string s) 
    {
      for (int i = 0; i < s.Length; i++)
      {
        if (char.IsLetter(s[i]))
        { return true; }
      }
      return false;
    }
    #endregion

    #region public string NextCmd()
    public string NextCmd() 
    {
      string Buf = "";
      for (int i = 0; i < Expression.Length; i++)
      {
        if (i == 0 && IsOperator(Expression[i]))
        {
          Buf = Expression[i].ToString();
          break;
        }

        if (!IsOperator(Expression[i]))
        {
          string s = Expression[i].ToString();
          if (s == ".")
          { Buf += PossuiLetras(Buf) ? "." : ","; }
          else
          { Buf += s; }
          
        }
        else
        { break; }
      }

      if (Buf.Length != 0)
      { Expression = Expression.Remove(0, Buf.Length); }
      return Buf;
    }
    #endregion
  }
  #endregion
}
