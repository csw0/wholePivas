using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasBatchCommon
{
    public class InsertSql
    {
        public string OrderFormSet(string UID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Insert into OrderFormSet(DEmployeeID,NextDay)");
            str.Append(" values('" + UID + "','18,00')");
            return str.ToString();
        }

        public string OrderColor(Int16 OrderID, string BColor, string TColor)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Insert into OrderColor(OrderID,OrderColor,OrderTColor)");
            str.Append(" values(" + OrderID + ",'" + BColor + "','" + TColor + "')");
            return str.ToString();
        }

        public string OrderChangelog(string ID, string EmpId, string EmpName, string old, string New, string IVStatus, string Reason, string ReasonDetail)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" insert  into OrderChangelog");
            str.Append(" values(");
            str.Append(ID + ",");
            str.Append("GETDATE(),");
            str.Append("'" + EmpId + "',");
            str.Append("'" + EmpName + "',");
            str.Append("'" + old + "',");
            str.Append("'" + New + "',");
            str.Append("'" + IVStatus + "',");
            str.Append("'" + Reason + "','");
            str.Append(ReasonDetail + "')");
            return str.ToString();
        }

        public string OrderLog(string EmpId, string ward, string patcode, string Dat)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" Insert into Orderlog(LogID,StartTime,[WardCode],[PatCode],[Dat]) ");
            str.Append(" values(CONVERT(varchar(100), GETDATE(), 112)+ replace(CONVERT(varchar(100), GETDATE(), 114),':','')+'_'+'" + EmpId + "',GETDATE(),'" + ward + "','" + patcode + "','" + Dat + "')");
            str.Append("  select * from OrderLog where ID=@@IDENTITY");
            str.Append("  select getdate()");
            return str.ToString();
        }
    }

}
