using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace lib.Class
{
  public class RegularExpression
  {
    public const string EMAIL = @"^[a-zA-Z0-9_\.-]{2,}@([A-Za-z0-9_-]{2,}\.)+[A-Za-z]{2,4}$";
    public const string NUMBER = @"^\d{0,}$";
    public const string DECIMAL = @"^[-+]?\d{1,3}(\.\d{3})*[.,]?\d{0,4}$";
    public const string TIME = @"^([0-1][0-9]|[2][0-3]):[0-5][0-9]$";
    public const string DATE = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[1-3][0-9]{3}$";
    public const string FONE = @"^[(]{1}\d{2}[)]{1}\d{4}[-]{1}\d{4}$";
    public const string CPF = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
    public const string CEP = @"^\d{5}-\d{3}$";
    public const string CNPJ = @"\d{2,3}.\d{3}.\d{3}/\d{4}-\d{2}$";

    public static bool Validate(string Expression, string Value)
    {
      Regex reg = new Regex(Expression);
      return reg.IsMatch(Value);
    }
  }
}
