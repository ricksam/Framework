using System;
using System.Collections.Generic;
using System.Text;

namespace lib.Database.Drivers
{
  public enum enmConnection { NoDatabase, Access, Firebird, Interbase, MySql, Odbc, OleDb, Oracle, SQLite, SqlServer, SqlServerCe }
  public enum enmTypeGenID { Reading, Writing }
  public enum enmFieldType { Undefined, String, Int, Long, Decimal, Date, Time, DateTime, Bool }
  public enum enmExtractType { Day, Month, Year }
}
