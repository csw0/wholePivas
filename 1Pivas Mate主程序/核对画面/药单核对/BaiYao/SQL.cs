using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiYaoCheck
{
    class SQL
    { 
        /// <summary>
        /// 病区及待审核药单数量 数量是每个病区未核对的药单数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetWard()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" select 'False' as 'check', d.WardCode,d.WardName,COUNT( RecipeID) as 'Count' from UseDrugListTemp udt2 ");
            sql.Append(" inner join Prescription p on udt2.GroupNo=p.GroupNo");
            sql.Append(" inner join DWard d on d.WardCode=p.WardCode  ");
            sql.Append("group by d.WardCode,d.WardName");
            return sql.ToString();
        }

        /// <summary>
        /// 显示的是该病区当天有药单的病人
        /// </summary>
        /// <param name="wardCode">病区号</param>
        /// <param name="date">药单执行日期</param>
        /// <returns></returns>
        public string GetPatient(string wardCode)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" select distinct p.PatientCode,pa.PatName,pa.BedNo,1 as 'a' from UseDrugListTemp2 udt2");
            str.Append(" inner join Prescription p on p.GroupNo=udt2.GroupNo ");
            str.Append("inner join PrescriptionDetail pd on pd.PrescriptionID=p.PrescriptionID   ");
            str.Append("inner join Patient pa on p.PatientCode=pa.PatCode ");
            str.Append("  where DATEDIFF(DD,OccDT,GETDATE()");
            str.Append(")=0 and p.WardCode in ('");
            str.Append(wardCode);
            str.Append("') and DATEDIFF(DD,OccDT,begindt)=0 ");
            str.Append(" union all select distinct p.PatientCode,pa.PatName,pa.BedNo,0 as 'a' from UseDrugListTemp2 udt2   ");
            str.Append("inner join Prescription p on p.GroupNo=udt2.GroupNo  ");
            str.Append("inner join PrescriptionDetail pd on pd.PrescriptionID=p.PrescriptionID  ");
            str.Append(" inner join Patient pa on p.PatientCode=pa.PatCode   ");
            str.Append("  where DATEDIFF(DD,OccDT,GETDATE()");
            str.Append(")=0 and p.WardCode in ('");
            str.Append(wardCode);
            str.Append("') and DATEDIFF(DD,OccDT,begindt)!=0 ");
            return str.ToString();
        }

        /// <summary>
        /// 病人药单
        /// </summary>
        /// <param name="patientCode"></param>
        /// <returns></returns>
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
            return str.ToString();
        }

        /// <summary>
        /// 病人药单
        /// </summary>
        /// <param name="groupno">组号</param>
        /// <param name="recipeid">药单号</param>
        /// <param name="occdt">执行时间</param>
        /// <returns></returns>
        public string UpdateTime(string groupno, string recipeid, string occdt)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  update UseDrugListTemp2 set OccDT='");
            str.Append(occdt);
            str.Append("',InfusionDate='" + occdt + "',Confirm=1   where GroupNo='" + groupno);
            str.Append("' and RecipeID='" + recipeid);
            str.Append("' and Schedule=1 ");

            return str.ToString();
        }
        //医嘱药单和摆药药单明细
        public string GetDrug(string GroupNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct pd.DrugCode,pd.DrugName,Spec ");
            str.Append(" from PrescriptionDetail pd  ");
            str.Append("where pd.GroupNo='"+GroupNo+"'");

            str.Append("  select distinct DrugCode,DrugName,Spec from UseDrugListTemp2   ");
            str.Append(" where  GroupNo='"+GroupNo);
            str.Append("' and DATEDIFF(DD,GETDATE(),OccDT)=0 ");
            return str.ToString();

        }

        public string ChangeSpec()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT distinct pa.PatCode,pa.PatName,pa.BedNo,d.WardName, pd.GroupNo,pd.RecipeNo, pd.DrugCode,pd.DrugName,pd.Spec,udt2.Spec as 'Spec1'  ");
            str.Append(" from PrescriptionDetail pd  ");
            str.Append("left join Prescription p on p.PrescriptionID=pd.PrescriptionID  ");
            str.Append("left join UseDrugListTemp2 udt2 on udt2.GroupNo=pd.GroupNo and pd.RecipeNo=left(RecipeID,charindex('+',RecipeID)-1)  ");
            str.Append("left join Patient pa on pa.PatCode=p.PatientCode  ");
            str.Append(" left join DWard d on d.WardCode=pa.WardCode  ");
            str.Append(" where p.PStatus=2 and PPause=0 and PDStatus=2 and pd.DrugCode!=udt2.DrugCode  ");

            return str.ToString();
        }
    }
}
