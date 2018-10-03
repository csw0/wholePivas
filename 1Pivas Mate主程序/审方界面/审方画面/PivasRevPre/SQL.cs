using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PivasRevPre
{
    public class SQL
    {
        /// <summary>
        /// 病人所有药
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string SelDrug(string s)//
        {
            StringBuilder str = new StringBuilder();
            //str.Append("SELECT PD.GroupNo,P.FregCode Batch,PD.DrugName,D.Dosage,D.DosageUnit,P.PStatus from DDrug D ");修改审方界面药品剂量使用医嘱详细表中数据
            str.Append("SELECT PD.GroupNo,P.FregCode Batch,PD.DrugName,PD.Dosage,D.DosageUnit,P.PStatus from DDrug D ");
            str.Append("INNER JOIN PrescriptionDetail PD ON PD.DrugCode = D.DrugCode ");
            str.Append("INNER JOIN Prescription P on P.PrescriptionID =PD.PrescriptionID ");
            //------------------
            //str.Append(" LEFT JOIN DDrug DD ON DD.DrugCode=PD.DrugCode and DD.Spec=PD.Spec ");
            //==================
            str.Append(" WHERE P.PatientCode = '" + s + "' AND P.PStatus !=4 ");
            str.Append(" Order BY PD.GroupNo,NoName");

            return str.ToString();
        }

        /// <summary>
        /// 列表模式 搜索
        /// </summary>
        /// <param name="WardCode"></param>
        /// <param name="strSea"></param>
        /// <returns></returns>
        public string SeaTCheckResult(string WardCode, string strSea)//按条件查审方结果
        {
            StringBuilder str = new StringBuilder();
            if (WardCode.Trim() != "")
            {
                str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,PD.InceptDT,PD.GroupNo,CD.DoctorExplain,");
                str.Append(" Pa.BedNo,Pa.PatName,D.WardName,CD.IsPass ,P.PStatus,P.Level,P.Attention,PrescriptionDetailID from Prescription P ");
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
                str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
                str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
                str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
                //------------------
                //str.Append(" LEFT JOIN DDurg DD ON DD.DurgCode=PD.DrugCode and DD.Spec=PD.Spec ");
                //==================
                str.Append(" WHERE  (Pa.BedNo like '%" + strSea + "%' ");
                str.Append(" OR P.CaseID like '%" + strSea + "%' )");
                str.Append(" AND D.IsOpen <> 0 AND Pa.PatStatus = 1 ");
                str.Append(" AND D.WardCode = '" + WardCode + "' AND P.PStatus <> 4");
                //str.Append(" ");
            }
            else
            {
                str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,PD.InceptDT,PD.GroupNo,CD.DoctorExplain,");
                str.Append(" Pa.BedNo,Pa.PatName,D.WardName,CD.IsPass ,P.PStatus,P.Level,P.Attention,PrescriptionDetailID from Prescription P ");
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
                str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
                str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
                str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
                //------------------
                //str.Append(" LEFT JOIN DDurg DD ON DD.DurgCode=PD.DrugCode and DD.Spec=PD.Spec ");
                //==================
                str.Append(" WHERE  (Pa.BedNo like '%" + strSea + "%' ");
                str.Append(" OR P.CaseID like '%" + strSea + "%' )");
                str.Append(" AND D.IsOpen <> 0 AND Pa.PatStatus = 1  AND P.PStatus <> 4");
            }
            return str.ToString();
        }

        /// <summary>
        /// 明细模式搜索
        /// </summary>
        /// <param name="WardCode"></param>
        /// <param name="strSea"></param>
        /// <returns></returns>
        public string SeaDCheckResult(string WardCode, string strSea)//按条件查审方结果
        {
            StringBuilder str = new StringBuilder();
            if (WardCode.Trim() != "")
            {
                str.Append("DECLARE @OrderLabelCountByPatient TABLE ( ");
                str.Append("PatCode varchar(16), ");
                str.Append("PatName varchar(50),");
                str.Append("UnCheckCount int,");
                str.Append("TotalCount int,back bit) ");
                str.Append("BEGIN ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT  ");
                str.Append("Pa.PatCode, ");
                str.Append("Pa.PatName, ");
                str.Append("SUM(case when P.PStatus = 1 then 1 else 0 end) AS UnCheckCount, ");
                str.Append("SUM(Case when DATEDIFF(DD, P.InceptDT, GETDATE()) =0 then 1 else 0 end) AS TotalCount,");
                //str.Append("COUNT(DISTINCT P.PrescriptionID) AS TotalCount, ");
                str.Append("Sum(case when P.Pstatus=3 then 1 else 0 end) as back ");
                str.Append("FROM Patient Pa ");
                str.Append("INNER JOIN Prescription P (NOLOCK) ON Pa.PatCode=P.PatientCode ");
                str.Append("WHERE ");
                str.Append("P.PStatus in(1,2,3) ");
                str.Append("AND Pa.WardCode ='" + WardCode + "' ");
                str.Append("AND (Pa.PatName LIKE '%" + strSea + "%' OR Pa.BedNo LIKE '%" + strSea + "%') ");
                str.Append("GROUP BY Pa.PatCode, Pa.PatName ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT PatCode, PatName, 0 ,0 ,0 ");
                str.Append("FROM Patient ");
                str.Append("WHERE PatCode NOT IN (SELECT PatCode FROM @OrderLabelCountByPatient) ");
                str.Append("AND WardCode='" + WardCode + "' AND (PatName LIKE '%" + strSea + "%' OR BedNo LIKE '%" + strSea + "%') ");
                str.Append("END ");
                str.Append("SELECT O.* FROM @OrderLabelCountByPatient O ");
                str.Append("LEFT OUTER JOIN Patient P ON O.PatCode = P.PatCode ");
            }
            else
            {
                str.Append("DECLARE @OrderLabelCountByPatient TABLE ( ");
                str.Append("PatCode varchar(16), ");
                str.Append("PatName varchar(50),");
                str.Append("UnCheckCount int,");
                str.Append("TotalCount int,back bit) ");
                str.Append("BEGIN ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT  ");
                str.Append("Pa.PatCode, ");
                str.Append("Pa.PatName, ");
                str.Append("SUM(case when P.PStatus = 1 then 1 else 0 end) AS UnCheckCount, ");
                str.Append("SUM(Case when DATEDIFF(DD, P.InceptDT, GETDATE()) =0 then 1 else 0 end) AS TotalCount,");
                str.Append("COUNT(DISTINCT P.PrescriptionID) AS TotalCount, ");
                str.Append("Sum(case when P.Pstatus=3 then 1 else 0 end) as back ");
                str.Append("FROM Patient Pa ");
                str.Append("INNER JOIN Prescription P (NOLOCK) ON Pa.PatCode=P.PatientCode ");
                str.Append("WHERE ");
                str.Append("P.PStatus in(1,2,3) ");
                str.Append("AND Pa.WardCode ='" + WardCode + "' ");
                str.Append("AND (Pa.PatName LIKE '%" + strSea + "%' OR Pa.BedNo LIKE '%" + strSea + "%') ");
                str.Append("GROUP BY Pa.PatCode, Pa.PatName ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT PatCode, PatName, 0 ,0 ,0 ");
                str.Append("FROM Patient ");
                str.Append("WHERE PatCode NOT IN (SELECT PatCode FROM @OrderLabelCountByPatient) ");
                str.Append("AND (PatName LIKE '%" + strSea + "%' OR BedNo LIKE '%" + strSea + "%') ");
                str.Append("END ");
                str.Append("SELECT O.* FROM @OrderLabelCountByPatient O ");
                str.Append("LEFT OUTER JOIN Patient P ON O.PatCode = P.PatCode ");
            }
            return str.ToString();
        }

        /// <summary>
        /// 审方结果查询方法
        /// </summary>
        /// <param name="sysstatus">系统审方条件</param>
        /// <param name="perstatus">人工审方条件</param>
        /// <param name="WardCode">病区条件</param>
        /// <param name="time">时间条件</param>
        /// <param name="ststatus">长期临时状态</param>
        /// <param name="drugtype">药品种类信息</param>
        /// <returns></returns>
        public string SeaPerCheckResult(string sysstatus, string perstatus, string WardCode, string time,string ststatus,string drugtype)//
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,P.CaseID,PD.InceptDT,PD.GroupNo,CD.DoctorExplain,PD.StartDT,");
            str.Append(" P.BedNo,Pa.PatName,D.WardName,P.PStatus ,P.Level,P.Attention ,CD.DoctorOperate,P.Remark1,P.Remark2,P.Remark3,P.Remark4,P.Remark5,P.Remark6,PrescriptionDetailID ");
            str.Append("from Prescription P ");
            str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
            str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
            str.Append(" LEFT JOIN BPRecord BP ON BP.PrescriptionID = P.PrescriptionID ");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
           // str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");//按病人表的wardcode查询病区
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");//按处方上的wardcode查询病区
            //------------------
            //str.Append(" LEFT JOIN DDurg DD ON DD.DurgCode=PD.DrugCode and DD.Spec=PD.Spec ");
            //==================  
            str.Append(" WHERE D.IsOpen <> 0 AND Pa.PatStatus = 1  ");
            if (WardCode.Trim() == "")
            {
                //str.Append("  AND Pa.WardCode in ( '')");//按病人表的wardcode查询病区
                str.Append("  AND P.WardCode in ( '')");//按处方上的wardcode查询病区
            }
            else
            {
               // str.Append("  AND Pa.WardCode in ( " + WardCode + ")");//按病人表的wardcode查询病区
                str.Append("  AND P.WardCode in ( " + WardCode + ")");//按处方上的wardcode查询病区
            }

            str.Append(" AND P.Level in (" + sysstatus + ")");
            if (perstatus=="1"||perstatus=="1,3")
            {
                str.Append(" AND P.PStatus in (" + perstatus + ") AND DATEDIFF(DD, P.InceptDT , '" + time + "')>=0");
            }
            else if (perstatus == "3")
            {
                str.Append(" AND P.PStatus in (" + perstatus + ") AND DATEDIFF(DD, P.InceptDT , '" + time + "')=0"); 
            }
            else
            {
                str.Append(" AND P.PStatus in (" + perstatus + ") AND DATEDIFF(DD, P.InceptDT ,'" + time + "')=0 ");
                //str.Append(" OR DATEDIFF(DD, BP.BPDT , '" + time + "')=0 )"); 
            }

                //ADD 罗璨0701长期临时医嘱
            if (ststatus == "2")//临时
            {
                str.Append(" AND P.Remark5='ST' ");
            }
            else if (ststatus == "1")//长期
            {
                str.Append(" AND (P.Remark5 IS NULL or Ltrim(Rtrim(P.Remark5))='' )  ");
            }
            else if (ststatus == "0")//全部
            {
                str.Append("");
            }
            else
            {
                str.Append(" AND 1 = 0 ");
            }
            //ADD 罗璨0703药品的分类
            if (drugtype != "")
            {
                str.Append(" AND P.DrugType in (" + drugtype + ") ");
            }
            else
            {
                str.Append(" AND P.DrugType in ('100') ");
            }
            str.Append("and not exists (select PrescriptionID from PrescriptionDetail PD1 where PD1.PrescriptionDetailID< PD.PrescriptionDetailID and P.PrescriptionID =PD1.PrescriptionID)");

            return str.ToString();
        }


        /// <summary>
        /// 当前处方的审方结果，暂时作废
        /// </summary>
        /// <param name="PreID"></param>
        /// <returns></returns>
        public string SelRes(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
            str.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
            str.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level, ");
            str.Append(" CT.Level from CPRecord CD INNER JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID ");
            str.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode");
            str.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
            str.Append(" WHERE P.PrescriptionID = " + PreID);
            return str.ToString();
        }

        ///// <summary>
        ///// 获取审方查询结果
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="strTime"></param>
        ///// <param name="per"></param>
        ///// <param name="st"></param>
        ///// <returns></returns>
        //public string SelWardRes(string code, string strTime, string per)
        //{
        //    StringBuilder str = new StringBuilder();
        //        str.Append(" SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,P.InceptDT,P.GroupNo,CD.DoctorExplain,");
        //        str.Append("case when P.PStatus = 1 then '未审' when P.PStatus = 2 then '已审通过' when P.PStatus = 3 then '不通过' end IsPass,");
        //        str.Append("case when P.Attention = 0 then ' ' when P.Attention = 1 then '关注' end Attention,");
        //        str.Append("case when CD.DoctorOperate = 1 then '强制执行' when CD.DoctorOperate = 2 then '接受退单' else '未处理' end DoctorOperate,");
        //        str.Append(" Pa.BedNo,Pa.PatName,D.WardName,P.PStatus,P.Level from Prescription P ");
        //        str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
        //        str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
        //        str.Append(" LEFT JOIN BPRecord BP ON BP.PrescriptionID = P.PrescriptionID ");                
        //        str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
        //        str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");
        //        str.Append(" WHERE D.IsOpen <> 0 ");
        //        if (per=="3")
        //        {
        //            str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
        //        }
        //        else if (per == "2,3")
        //        {
        //            str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 "); 
        //        }
        //        else
        //        {
        //            str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')>=0 ");
        //        }
        //        if (code.Trim() == "")
        //        {
        //            str.Append(" AND P.WardCode in ('') ");
        //        }
        //        else
        //        {
        //            str.Append(" AND P.WardCode in (" + code + " ) ");
        //        }
        //        str.Append(" AND P.PStatus in (" + per + ") ");
        //        str.Append(" AND Pa.PatStatus=1");
            
        //    return str.ToString();
        //}

        /// <summary>
        /// 按病区和时间查病人名字
        /// </summary>
        /// <param name="code"></param>
        /// <param name="strTime"></param>
        /// <param name="per"></param>
        /// <returns></returns>
        public string SelPatName(string wards, string strTime, string per)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT DISTINCT ");
            str.Append(" Pa.PatName,Pa.PatCode,P.CaseID,P.BedNo from Prescription P ");
            str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
            //str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");//按病人表的wardcode查询病区
            str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");//按处方上的wardcode查询病区

            str.Append(" WHERE D.IsOpen <> 0 ");
            if (per == "3")
            {
                str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
            }
            else if (per == "2,3")
            {
                str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
            }
            else
            {
                str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')>=0 ");
            }
            if (wards.Trim().Length > 0)
            {
                // str.Append(" AND Pa.WardCode in (" + wards + ")");//按病人表的wardcode查询病区
                str.Append(" AND P.WardCode in (" + wards + ")");//按处方上的wardcode查询病区
            }
            else
            {
                //str.Append(" AND Pa.WardCode in ('') ");按病人表的wardcode查询病区
                str.Append(" AND P.WardCode in ('') ");//按处方上的wardcode查询病区
            }
            str.Append(" AND P.PStatus in (" + per + ") ");
            str.Append(" AND Pa.PatStatus=1");
            str.Append(" order by Pa.PatName");
            return str.ToString();
        }
        
        /// <summary>
        /// 根据查询条件查询选中病区的病人信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="strTime"></param>
        /// <param name="per"></param>
        /// <param name="PatCode"></param>
        /// <returns></returns>
        public string SelPatWardRes(string code, string strTime, string per,string PatCode)
        {
            StringBuilder str = new StringBuilder();
            if (code.Trim() != "")
            {
                str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,P.CaseID,PD.InceptDT,PD.GroupNo,CD.DoctorExplain,PD.StartDT,");
                str.Append(" P.BedNo,Pa.PatName,D.WardName,P.PStatus ,P.Level,P.Attention ,CD.DoctorOperate,P.Remark1,P.Remark2,P.Remark3,P.Remark4,P.Remark5,P.Remark6,PrescriptionDetailID ");
                str.Append(" from Prescription P ");
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
                str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
                str.Append(" LEFT JOIN BPRecord BP ON BP.PrescriptionID = P.PrescriptionID ");
                str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
                //str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");//按病人表的wardcode查询病区
                str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");//按处方表的wardcode查询病区
                str.Append(" WHERE D.IsOpen <> 0 ");
                if (per == "3")
                {
                    str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
                }
                else if (per == "2,3")
                {
                    str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
                }
                else
                {
                    str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')>=0 ");
                }

                //str.Append(" AND Pa.WardCode = '" + code + "'");//按病人表的wardcode查询病区
                str.Append(" AND P.WardCode = '" + code + "'");//按处方表的wardcode查询病区

                str.Append(" AND P.PStatus in (" + per + ") ");
                str.Append(" AND Pa.PatStatus=1");
                str.Append(" AND P.PatientCode='" + PatCode + "'");
                str.Append(" and not exists (select PrescriptionID from PrescriptionDetail PD1 where PD1.PrescriptionDetailID< PD.PrescriptionDetailID and P.PrescriptionID =PD1.PrescriptionID)");
            }
            else
            {
                str.Append("SELECT DISTINCT P.PrescriptionID,P.WardCode,P.PatientCode,P.CaseID,PD.InceptDT,PD.GroupNo,CD.DoctorExplain,PD.StartDT,");
                str.Append(" Pa.BedNo,Pa.PatName,D.WardName,P.PStatus ,P.Level,P.Attention ,CD.DoctorOperate,P.Remark1,P.Remark2,P.Remark3,P.Remark4,P.Remark5,P.Remark6,PrescriptionDetailID");
                str.Append(" from Prescription P ");
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID");
                str.Append(" LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID");
                str.Append(" LEFT JOIN BPRecord BP ON BP.PrescriptionID = P.PrescriptionID ");
                str.Append(" LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode");
                //str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");//按病人表的wardcode查询病区
                str.Append(" LEFT JOIN DWard D ON D.WardCode=P.WardCode ");//按处方表的wardcode查询病区

                str.Append(" WHERE D.IsOpen <> 0 ");
                if (per == "3")
                {
                    str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
                }
                else if (per == "2,3")
                {
                    str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')=0 ");
                }
                else
                {
                    str.Append(" AND DATEDIFF(DD, P.InceptDT , '" + strTime + "')>=0 ");
                }

                str.Append(" AND P.PStatus in (" + per + ") ");
                str.Append(" AND Pa.PatStatus=1");
                str.Append(" AND P.PatientCode='" + PatCode + "'");
                str.Append(" and not exists (select PrescriptionID from PrescriptionDetail PD1 where PD1.PrescriptionDetailID< PD.PrescriptionDetailID and P.PrescriptionID =PD1.PrescriptionID)");
            }
            return str.ToString();
        }


        /// <summary>
        /// 查询处方信息 列表模式
        /// </summary>
        /// <param name="PreID"></param>
        /// <returns></returns>
        public string INFO(string PreID, string perresult)
        {
            StringBuilder str = new StringBuilder();
            if (perresult == "未审")
            {
                str.Append(" SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.GroupNo,P.CaseID,P.FregCode,P.Level,P.PStatus,PD.Dosage,PD.DosageUnit,'' Cper,''CPtime,'' Explain, ");
                str.Append(" DE1.DEmployeeName DocName,D.WardName,PD.EndDT,PD.StartDT ,PD.DrugName,PD.Spec,DD.UniPreparationID,P.UsageName, ");
                str.Append(" Pa.BedNo,Pa.PatName,Pa.Sex,Pa.Height, Pa.Weight,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,PD.Remark9,PD.Remark10,PD.Remark7,PD.Remark8,");
                str.Append(" PD.Quantity,NoName,");
                //======================================
                str.Append(" '' as CPtimeRG"); //张牧云20160907 添加人工审方时间  
                //======================================
                str.Append(",PrescriptionDetailID ");
                str.Append(" FROM Prescription P ");
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
                str.Append(" INNER JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
                str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");

                //=======================================
                str.Append(" inner JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");//陆卓春20140612修改
                //=======================================
                str.Append(" WHERE P.PrescriptionID = " + PreID);
                str.Append(" order by NoName");


                str.Append(" SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
                str.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
                str.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level, ");
                str.Append(" CT.Level from CPRecord CD INNER JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
                str.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID ");
                str.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode");
                str.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
                str.Append(" WHERE P.PrescriptionID = " + PreID);
            }
            else if (perresult == "已审通过")
            {
                str.Append(" SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.GroupNo,P.CaseID,P.FregCode,P.Level,P.PStatus,PD.Dosage,PD.DosageUnit, ");
                str.Append(" DE2.DEmployeeName+'('+DE2.AccountID+')' Cper,CD.CPDT CPtime,CD.DoctorExplain Explain,  ");
                str.Append(" DE1.DEmployeeName DocName,D.WardName,PD.EndDT,PD.StartDT ,PD.DrugName,PD.Spec,DD.UniPreparationID,P.UsageName, ");
                str.Append(" Pa.BedNo,Pa.PatName,Pa.Sex, Pa.Height, Pa.Weight,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,PD.Remark9,PD.Remark10,PD.Remark7,PD.Remark8,");
                str.Append(" PD.Quantity,NoName,");
                //======================================
                str.Append(" CR.CheckDT as CPtimeRG"); //张牧云20160907 添加人工审方时间  
                str.Append(",PrescriptionDetailID ");
                str.Append(" FROM Prescription P ");
                str.Append(" lEFT JOIN CPRecord CR ON CR.PrescriptionID=P.PrescriptionID");
                //======================================
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
                str.Append(" INNER JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
                str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
                str.Append(" LEFT JOIN CPRecord CD ON CD.PrescriptionID = P.PrescriptionID ");
                str.Append(" LEFT JOIN DEmployee DE2 ON DE2.DEmployeeID = CD.CheckDCode ");
                //=======================================
                str.Append(" inner JOIN DDrug DD ON DD.DrugCode=PD.DrugCode  ");//陆卓春20140612修改
                //=======================================
                str.Append(" WHERE P.PrescriptionID = " + PreID);
                str.Append(" order by NoName");


                str.Append(" SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
                str.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
                str.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level, ");
                str.Append(" CT.Level from CPRecord CD INNER JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
                str.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID ");
                str.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode");
                str.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
                str.Append(" WHERE P.PrescriptionID = " + PreID);
            }
            else if (perresult == "不通过")
            {
                str.Append(" SELECT DISTINCT P.PrescriptionID,P.DoctorCode,PiShi,P.GroupNo,P.CaseID,P.FregCode,P.Level,P.PStatus,PD.Dosage,PD.DosageUnit, ");
                str.Append(" DE2.DEmployeeName+'('+DE2.AccountID+')' Cper,BD.BPDT CPtime,BD.BPExplain Explain,  ");
                str.Append(" DE1.DEmployeeName DocName,D.WardName,PD.EndDT,PD.StartDT ,PD.DrugName,PD.Spec,DD.UniPreparationID,P.UsageName, ");
                str.Append(" Pa.BedNo,Pa.PatName,Pa.Sex, Pa.Height, Pa.Weight,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,PD.Remark9,PD.Remark10,PD.Remark7,PD.Remark8,");
                str.Append(" PD.Quantity,NoName,");
                //======================================
                str.Append(" CR.CheckDT as CPtimeRG"); //张牧云20160907 添加人工审方时间  
                str.Append(",PrescriptionDetailID ");
                str.Append(" FROM Prescription P ");
                str.Append(" lEFT JOIN CPRecord CR ON CR.PrescriptionID=P.PrescriptionID");
                //======================================
                str.Append(" LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
                str.Append(" LEFT JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode ");
                str.Append(" INNER JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
                str.Append(" LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
                str.Append(" LEFT JOIN BPRecord BD ON BD.PrescriptionID = P.PrescriptionID ");
                str.Append(" LEFT JOIN DEmployee DE2 ON DE2.DEmployeeID = BD.BPCode ");
                //=======================================
                str.Append(" inner JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");//陆卓春20140612修改
                //=======================================
                str.Append(" WHERE P.PrescriptionID = " + PreID);
                str.Append(" order by NoName");


                str.Append(" SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
                str.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
                str.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level, ");
                str.Append(" CT.Level from CPRecord CD INNER JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
                str.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID ");
                str.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode");
                str.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
                str.Append(" WHERE P.PrescriptionID = " + PreID);
            }      

            return str.ToString();
        }

        /// <summary>
        /// 明细模式  按时间选个人未审医嘱
        /// </summary>
        /// <param name="sys"></param>
        /// <param name="per"></param>
        /// <param name="PatCode"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string SelDetail(string sys, string per, string PatCode, string time)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID, P.Level,P.InceptDT,P.FregCode,P.GroupNo,P.CaseID,P.PStatus, ");
            str.Append("DE.DEmployeeName,P.DoctorCode,D.WardCode,D.WardName, DD.PiShi,DD.Spec,P.Attention, ");
            str.Append("Pa.PatCode,Pa.PatName,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,Pa.BedNo,Pa.Sex,Pa.Weight,");
            str.Append("PD.StartDT,PD.EndDT,PD.DrugName,PD.Dosage,PD.DosageUnit,PD.Quantity,");
            str.Append("DE1.DEmployeeName checker ,PrescriptionDetailID ");
            str.Append("FROM Prescription P ");
            str.Append("LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
            str.Append("LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            // str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("LEFT JOIN DEmployee DE ON DE.DEmployeeCode=P.DoctorCode ");
            str.Append("LEFT JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("LEFT JOIN DEmployee DE1 ON DE1.DEmployeeID=CD.CheckDCode ");
            str.Append("WHERE PatCode='" + PatCode + "' AND DATEDIFF(DD,P.InceptDT,'" + time + "')>=0 ");
            str.Append("AND P.Level IN (" + sys + ") AND P.PStatus IN (" + per + ") ");
            str.Append("ORDER BY P.PrescriptionID ");

            return str.ToString();
        }


        /// <summary>
        /// 明细模式用 按时间选个人所有未停止医嘱
        /// </summary>
        /// <param name="PatCode"></param>
        /// <param name="per"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public string SelAllDetail(string PatCode, string per, string time)//
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT P.PrescriptionID, P.Level,P.InceptDT,P.FregCode,P.GroupNo,P.CaseID,P.PStatus, ");
            str.Append("DE.DEmployeeName,P.DoctorCode,D.WardCode,D.WardName, DD.PiShi,DD.Spec,P.Attention, ");
            str.Append("Pa.PatCode,Pa.PatName,(Convert(varchar,Pa.Age,120)+Pa.AgeSTR) as Birthday,Pa.BedNo,Pa.Sex,Pa.Weight,");
            str.Append("PD.StartDT,PD.EndDT,PD.DrugName,PD.Dosage,PD.DosageUnit,PD.Quantity,");
            str.Append("DE1.DEmployeeName checker,PrescriptionDetailID ");
            str.Append("FROM Prescription P ");
            str.Append("LEFT JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("LEFT JOIN DWard D ON D.WardCode=Pa.WardCode ");
            str.Append("LEFT JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("LEFT JOIN CPRecord CD ON CD.PrescriptionID=P.PrescriptionID ");
            //str.Append("LEFT JOIN CPResult CT ON CT.CheckRecID=CD.CPRecordID ");
            str.Append("LEFT JOIN DEmployee DE ON DE.DEmployeeID=P.DoctorCode ");
            str.Append("LEFT JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");
            str.Append("LEFT JOIN DEmployee DE1 ON DE1.DEmployeeID=CD.CheckDCode ");
            str.Append("WHERE PatCode='" + PatCode + "' AND DATEDIFF(DD,P.InceptDT,'" + time + "')>=0 ");
            str.Append("and P.Pstatus < 4 ");
            str.Append("ORDER BY P.PrescriptionID ");

            return str.ToString();
        }

        /// <summary>
        /// 统计长期处方
        /// </summary>
        /// <returns></returns>
        public string SelLongCount(string dt)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(1) as longCount from Prescription as P  where (P.Remark5 IS NULL or Ltrim(Rtrim(P.Remark5))='' ) and DATEDIFF(DD, P.InceptDT ,'" + dt + "')=0  and P.PStatus =1");
            return str.ToString();
        }
        
        /// <summary>
        /// 统计临时处方
        /// </summary>
        /// <returns></returns>
        public string SelLingCount(string dt)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select count(1) as lingCount from Prescription as P where  P.Remark5='ST' and DATEDIFF(DD, P.InceptDT ,'" + dt + "')=0 and P.PStatus =1");
            return str.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sys"></param>
        /// <param name="per"></param>
        /// <param name="wardcode"></param>
        /// <param name="time"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public string SelDetailSys(string sys, string per, string wardcode, string time,string st)
        {
            StringBuilder str = new StringBuilder();
            if (wardcode.Trim() != "")
            {
                str.Append("DECLARE @OrderLabelCountByPatient TABLE ( ");
                str.Append("PatCode varchar(16), ");
                str.Append("PatName varchar(50),");
                str.Append("UnCheckCount int,");
                str.Append("TotalCount int,back bit) ");
                str.Append("BEGIN ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT  ");
                str.Append("Pa.PatCode, ");
                str.Append("Pa.PatName, ");
                str.Append("SUM(case when P.PStatus in(" + per + ") AND P.Level in(" + sys + ") then 1 else 0 end) AS UnCheckCount, ");
                //str.Append("COUNT(DISTINCT P.PrescriptionID) AS TotalCount, ");
                str.Append("SUM(Case when DATEDIFF(DD, P.InceptDT, '"+time+"') =0 then 1 else 0 end) AS TotalCount,");
                str.Append("Sum(case when P.Pstatus=3 then 1 else 0 end) as back ");
                str.Append("FROM Patient Pa ");
                str.Append("INNER JOIN Prescription P (NOLOCK) ON Pa.PatCode=P.PatientCode ");
                str.Append("WHERE ");
                str.Append("P.PStatus in(1,2,3) ");
                if(per=="1"||per=="1,3")
                {
                    str.Append("AND DATEDIFF(DD, P.InceptDT, '" + time + "') >=0 ");
                }
                else
                {
                    str.Append("AND DATEDIFF(DD, P.InceptDT, '" + time + "') =0 ");
                }
                str.Append("AND P.WardCode ='" + wardcode + "'");
                str.Append("GROUP BY Pa.PatCode, Pa.PatName ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT PatCode, PatName, 0 ,0 ,0 ");
                str.Append("FROM Patient ");
                str.Append("WHERE PatCode NOT IN (SELECT PatCode FROM @OrderLabelCountByPatient) ");
                str.Append("AND WardCode='" + wardcode + "' ");
                str.Append("END ");
                str.Append("SELECT O.* FROM @OrderLabelCountByPatient O ");
                str.Append("LEFT OUTER JOIN Patient P ON O.PatCode = P.PatCode ");
            }
            else
            {
                str.Append("DECLARE @OrderLabelCountByPatient TABLE ( ");
                str.Append("PatCode varchar(16), ");
                str.Append("PatName varchar(50),");
                str.Append("UnCheckCount int,");
                str.Append("TotalCount int,back bit) ");
                str.Append("BEGIN ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT  ");
                str.Append("Pa.PatCode, ");
                str.Append("Pa.PatName, ");
                str.Append("SUM(case when P.PStatus in(" + per + ") AND P.Level in(" + sys + ") then 1 else 0 end) AS UnCheckCount, ");
                //str.Append("COUNT(DISTINCT P.PrescriptionID) AS TotalCount, ");
                str.Append("SUM(Case when DATEDIFF(DD, P.InceptDT, '" + time + "') =0 then 1 else 0 end) AS TotalCount,");
                str.Append("Sum(case when P.Pstatus=3 then 1 else 0 end) as back ");
                str.Append("FROM Patient Pa ");
                str.Append("INNER JOIN Prescription P (NOLOCK) ON Pa.PatCode=P.PatientCode ");
                str.Append("WHERE ");
                str.Append("P.PStatus in(1,2,3) ");
                if (per == "1" || per == "1,3")
                {
                    str.Append("AND DATEDIFF(DD, P.InceptDT, '" + time + "') >=0 ");
                }
                else
                {
                    str.Append("AND DATEDIFF(DD, P.InceptDT, '" + time + "') =0 ");
                }
                str.Append("GROUP BY Pa.PatCode, Pa.PatName ");
                str.Append("INSERT @OrderLabelCountByPatient ");
                str.Append("SELECT PatCode, PatName, 0 ,0 ,0 ");
                str.Append("FROM Patient ");
                str.Append("WHERE PatCode NOT IN (SELECT PatCode FROM @OrderLabelCountByPatient) ");
                //str.Append("AND WardCode='" + wardcode + "' ");
                str.Append("END ");
                str.Append("SELECT O.* FROM @OrderLabelCountByPatient O ");
                str.Append("LEFT OUTER JOIN Patient P ON O.PatCode = P.PatCode ");
            }
            return str.ToString();
        }

        /// <summary>
        /// 删除处方
        /// </summary>
        /// <param name="PrescriptionID"></param>
        /// <returns></returns>
        public string DeletePrescription(string PrescriptionID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from CPResult where CheckRecID in (select CheckRecID from CPRecord where PrescriptionID='");
            str.Append(PrescriptionID);
            str.Append("')  ");
            str.Append("delete from CPRecord where PrescriptionID='" + PrescriptionID + "' ");

            str.Append("delete from PrescriptionDetail where PrescriptionID='");
            str.Append(PrescriptionID);
            str.Append("'");

            str.Append(" delete from PrescriptionTemp where GroupNo in (select GroupNo from Prescription where PrescriptionID='");
            str.Append(PrescriptionID + "') ");
            str.Append("  delete from Prescription where PrescriptionID='");
            str.Append(PrescriptionID);
            str.Append("'");

            return str.ToString();
        }


    }

    public class DLLWrapper
    {
        ///<summary>
        /// API LoadLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int LoadLibrary(String funcname);

        ///<summary>
        /// API GetProcAddress
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int GetProcAddress(int handle, String funcname);

        ///<summary>
        /// API FreeLibrary
        ///</summary>
        [DllImport("Kernel32")]
        public static extern int FreeLibrary(int handle);

        ///<summary>
        ///通过非托管函数名转换为对应的委托, by jingzhongrong
        ///</summary>
        ///<param name="dllModule">通过LoadLibrary获得的DLL句柄</param>
        ///<param name="functionName">非托管函数名</param>
        ///<param name="t">对应的委托类型</param>
        ///<returns>委托实例，可强制转换为适当的委托类型</returns>
        public static Delegate GetFunctionAddress(int dllModule, string functionName, Type t)
        {
            int address = GetProcAddress(dllModule, functionName);
            if (address == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }

        ///<summary>
        ///将表示函数地址的IntPtr实例转换成对应的委托, by jingzhongrong
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(IntPtr address, Type t)
        {
            if (address == IntPtr.Zero)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(address, t);
        }

        ///<summary>
        ///将表示函数地址的int转换成对应的委托，by jingzhongrong
        ///</summary>
        public static Delegate GetDelegateFromIntPtr(int address, Type t)
        {
            if (address == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);
        }
    }
}
