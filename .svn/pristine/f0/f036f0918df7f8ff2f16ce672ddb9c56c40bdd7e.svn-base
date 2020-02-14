using System;
using System.IO;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using lib.Class;
using Microsoft.Office.Interop.Access;

namespace lib.Database.Drivers
{
    public class Access : OleDb
    {
        #region Constructor
        public Access(InfoConnection Info)
          : base(Info)
        { }
        public Access(DbConnection DbConnection)
          : base(DbConnection)
        { }
        #endregion

        protected override void drv_Inicialize()
        {
            this.dbu.dbFormat.Date = "yyyy-MM-dd";
            this.dbu.dbFormat.Time = "HH:mm:ss";
            this.dbu.dbFormat.DateTime = "yyyy-MM-dd HH:mm:ss";
            this.dbu.dbFormat.Decimal = "0.0000";

            if (string.IsNullOrEmpty(Info.ConnectionString))
            {
                //Novo access de extenção .accdb ........ Microsoft.ACE.OLEDB.12.0
                if (System.IO.Path.GetExtension(Info.Database).ToUpper() == ".MDB")
                { ConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", Info.Database); }
                else
                { ConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", Info.Database); }

            }
            else
            {
                ConnectionString = Info.ConnectionString;
            }


            this.DbConnection = new OleDbConnection(ConnectionString);
        }
    }
}
