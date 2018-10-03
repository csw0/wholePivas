using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrugListConform
{
    class SQL
    {
        public string GetWard(string date)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select 'False' as 'check', d.WardCode,d.WardName,COUNT( udt2.GroupNo) as 'Count' from UseDrugListTemp2 udt2 ");
            sql.Append(" inner join Prescription p on udt2.GroupNo=p.GroupNo");
            sql.Append(" inner join DWard d on d.WardCode=p.WardCode  ");
            sql.Append(" inner join Patient pa on p.PatientCode=pa.PatCode  ");
            sql.Append("where p.GroupNo is not null  and Confirm=0 ");
            sql.Append("group by d.WardCode,d.WardName");
            return sql.ToString();
        }
        public string GetPatient(string wardCode,string date)
        {
            StringBuilder str = new StringBuilder();

            //str.Append(" select distinct p.PatientCode,pa.PatName,pa.BedNo,udt2.GroupNo, p.FregCode ,pd.StartDT,pd.EndDT ");
            str.Append(" select distinct p.PatientCode,pa.PatName,pa.BedNo,1 as 'a' from UseDrugListTemp2 udt2");
            str.Append(" inner join Prescription p on p.GroupNo=udt2.GroupNo ");
            str.Append("inner join PrescriptionDetail pd on pd.PrescriptionID=p.PrescriptionID   ");
            str.Append("inner join Patient pa on p.PatientCode=pa.PatCode ");
            str.Append("  where DATEDIFF(DD,OccDT,'");
            str.Append(date);
            str.Append("')=0 and p.WardCode in ('");
            str.Append(wardCode);
            str.Append("') and DATEDIFF(DD,OccDT,begindt)=0 ");


            str.Append(" union all select distinct p.PatientCode,pa.PatName,pa.BedNo,0 as 'a' from UseDrugListTemp2 udt2   ");
            str.Append("inner join Prescription p on p.GroupNo=udt2.GroupNo  ");

            str.Append("inner join PrescriptionDetail pd on pd.PrescriptionID=p.PrescriptionID  ");
            str.Append(" inner join Patient pa on p.PatientCode=pa.PatCode   ");
            str.Append("  where DATEDIFF(DD,OccDT,'");
            str.Append(date);
            str.Append("')=0 and p.WardCode in ('");
            str.Append(wardCode);
            str.Append("') and DATEDIFF(DD,OccDT,begindt)!=0 ");
            return str.ToString();
        }
       public string GetDrugList(string patientCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select udt2.GroupNo,udt2.RecipeID,p.FregCode,udt2.DrugCode,d.DrugName,d.Spec,udt2.OccDT,udt2.begindt,udt2.enddt  from UseDrugListTemp2 udt2 ");
            str.Append(" left join Prescription p on p.GroupNo=udt2.GroupNo ");
            str.Append(" left join Patient pa on pa.PatCode=p.PatientCode ");
            str.Append(" left join DDrug d on d.DrugCode=udt2.DrugCode ");
            str.Append(" where PatientCode='");
            str.Append(patientCode);
            str.Append("' ");
            str.Append(" and udt2.GroupNo in (select GroupNo from UseDrugListTemp2 where  Confirm=0 )");
            //str.Append("')=0  ");
            return str.ToString();        
        }

     

       public string UpdateTime(string groupno, string recipeid,string occdt)
       {
           StringBuilder str = new StringBuilder();
           str.Append("  update UseDrugListTemp2 set OccDT='");
           str.Append(occdt);
           str.Append("',InfusionDate='" + occdt + "',Confirm=1   where GroupNo='" + groupno);
           str.Append("' and RecipeID='"+recipeid);
           str.Append("'  ");


           return str.ToString();
       }
    }
}
