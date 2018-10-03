using System;
using System.Collections.Generic;
using System.Text;
using DWardChange;

namespace DWardChange
{
    public class SQL
    {
        public string SelectOLdDward()
        {
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("    select distinct WardCode,WardName from DWardOld");

            return sql.ToString();
        }

        public string SelectNewDward(string OldWDName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("    select NewWardCode,NewWardName from DWardOrder where OldWardName='" + OldWDName + "'");

            return sql.ToString();
        }

        public string AddNewDward(string OldWDC,string OldWDN,string NewWDc,string NewWDN)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("    insert into DWardOrder values('" + OldWDC + "','" + OldWDN + "','','" + NewWDc + "','" + NewWDN + "')");

            return str.ToString();
        }

        public string SelectNewCode(string OldWDN)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("  select MAX(substring (NewWardCode,len(NewWardCode)-5,6)) from DWardOrder where OldWardName='" + OldWDN + "'");

            return str.ToString();
        }

        public string DeleteNewDward(string NewWDC)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("    delete from DWardOrder where NewWardCode='" + NewWDC + "'");

            return str.ToString();
        }

        public string SelectBeds(string OldWDC,string NewWDC,string NoBedNo,string dy,string xy)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            //str.Append("    select distinct bedno from Prescription where WardCode='" + OldWDC + "'and BedNo not in ("+NoBedNo+") ");
            str.Append("  select  distinct bedno, case when patindex('%[0-9]%',BedNo)>1 then SUBSTRING(BedNo,1,patindex('%[0-9]%',BedNo)-1) when PATINDEX ('%[^0-9]%',BedNo)=0 then '0' else '1' end ss,");
            str.Append("  case when SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),case when patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))>0 then patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))-1 else 0 end)='' ");
            str.Append("  then case when patindex('%[0-9]%',BedNo)>0 then convert(int,SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno))) end");
            str.Append("  else SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),case when patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))>0 then patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))-1 else 0 end)");
            str.Append("  end dd from Prescription where WardCode in(" + OldWDC + ")and BedNo not in (" + NoBedNo + ")  order by ss,dd");

            

            StringBuilder str1 = new StringBuilder();
            str1.Length = 0;
            if (NewWDC != "")
            {
                str1.Append("   select distinct Beds from DWardOrder where NewWardCode='" + NewWDC + "'");
            }

            //StringBuilder str2 = new StringBuilder();
            //str2.Length = 0;
            //str2.Append("  select  distinct bedno, case when patindex('%[0-9]%',BedNo)>1 then SUBSTRING(BedNo,1,patindex('%[0-9]%',BedNo)-1) when PATINDEX ('%[^0-9]%',BedNo)=0 then '0' else '1' end ss,");
            //str2.Append("  case when SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),case when patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))>0 then patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))-1 else 0 end)='' ");
            //str2.Append("  then case when patindex('%[0-9]%',BedNo)>0 then convert(int,SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno))) end");
            //str2.Append("  else SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),case when patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))>0 then patindex('%[^0-9]%',SUBSTRING(BedNo,patindex('%[0-9]%',BedNo),LEN(bedno)))-1 else 0 end)");
            //str2.Append("  end dd from Prescription where WardCode='" + OldWDC + "'and BedNo not in (" + NoBedNo + ") ");

            //if (dy != "")
            //{
            //    str2.Append("  and BedNo >='" + dy + "'");
            //}

            //if (xy != "")
            //{
            //    str2.Append("  and BedNo <='" + xy + "'");
            //}
            //str2.Append("   order by ss,dd");

            return str.ToString() + str1.ToString() ;
        }

        public string UpdateBeds(string beds,string NewWDC)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("  update DWardOrder set Beds='" + beds + "' where NewWardCode='" + NewWDC + "'");

            return str.ToString();
        }

        /// <summary>
        /// 查找未被选择病区的床号
        /// </summary>
        /// <param name="OldWDC"></param>
        /// <param name="NewWDC"></param>
        /// <returns></returns>
        public string SelectOtherWDBeds(string OldWDC,string NewWDC)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("    select distinct WardCode,BedNo from Prescription where WardCode='"+ OldWDC +"'");

            StringBuilder str1=new StringBuilder();
            str1.Length=0;
            str1.Append("   select NewWardCode,Beds from DWardOrder where OldWardCode='"+OldWDC+"' and NewWardCode <> '"+NewWDC+"'");

            return str1.ToString();
        }

        public string SelDWName(string NewWDN)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("    select NewWardName from DWardOrder where NewWardName='" + NewWDN + "'");

            return str.ToString();
        }

        public string ReOldDward(string bads ,string OldWDC, string NewWDC)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append("    update Prescription set wardcode ='" + OldWDC + "' where WardCode='" + NewWDC + "'and BedNo in ("+bads+")");
            str.Append("    update Patient set wardcode ='" + OldWDC + "' where WardCode='" + NewWDC + "'and BedNo in (" + bads + ")");

            return str.ToString();
        }




    }
}