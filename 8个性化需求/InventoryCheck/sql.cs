using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryCheck
{
    class sql
    {
       public  string GetHistoyDrug(string date1,string date2)
        {
           
            StringBuilder str = new StringBuilder();
            str.Append("select distinct ivd.DrugCode,ivd.DrugName,ivd.spec,d.PortNo, SUM(DgNo) dcount   from IVRecord iv ");
            str.Append("left join IVRecordDetail ivd on ivd.IVRecordID=iv.IVRecordID ");
            str.Append(" left join DDrug d on d.DrugCode=ivd.DrugCode ");
            str.Append(" where InfusionDT between '");
            str.Append(date1);
            str.Append("' and '");
            str.Append(date2);
            str.Append("'");
           
            str.Append("  group by ivd.DrugCode,ivd.DrugName,ivd.spec,d.PortNo");
            return str.ToString();
        
        }

       public string GetDayDrug(string drugcodes)
       {
           StringBuilder str = new StringBuilder();
           str.Append("select distinct d.DrugCode, d.DrugCode,d.PortNo,d.DrugName,d.spec,'' hiscount , sum(case when DATEDIFF(dd,getdate(),infusiondt)=1 then DgNo when InfusionDT Is null then 0 else 0 end) dcount,'' scount  from DDrug d  ");
           str.Append(" left  join IVRecordDetail ivd on d.DrugCode=ivd.DrugCode ");
           str.Append("  left  join IVRecord iv on iv.IVRecordID=ivd.IVRecordID   ");
           str.Append("  where d.DrugCode in (" + drugcodes + ") ");
           str.Append("   group by d.DrugCode,d.DrugName,d.spec,d.PortNo");
           return str.ToString();
       
       }
        
    }
}
