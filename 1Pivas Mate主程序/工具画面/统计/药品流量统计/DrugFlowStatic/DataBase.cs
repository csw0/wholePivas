using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PIVAsDBhelp;
using System.Data;

namespace DrugFlowStatic
{
    class DataBase
    {
        DB_Help db = new DB_Help();

        public DataSet getWard()
        {
            DataSet ds = null;
            try 
            {
                ds = db.GetPIVAsDB("SELECT * FROM DWard WHERE IsOpen = 1");
            }
            catch (Exception e) { }
            return ds;
        }

        public DataSet getDrug()
        {
            DataSet ds = null;
            try 
            {
                ds = db.GetPIVAsDB("SELECT DrugCode, DrugName FROM DDrug WHERE IsValid = 1");
            }
            catch (Exception e) { }
            return ds;
        }
    }
}
