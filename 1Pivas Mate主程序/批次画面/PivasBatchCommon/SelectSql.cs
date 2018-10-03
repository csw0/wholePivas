using PIVAsCommon.Helper;
using System.Text;

namespace PivasBatchCommon
{
    public class SelectSql
    {
        DB_Help db = new DB_Help();

        /// <summary>
        /// 瓶签信息(批次用)
        /// </summary>
        /// <param name="WardCode">病区Code</param>
        /// <param name="dt">时间</param>
        /// <param name="tags">状态，已发送，未发送</param>
        /// <param name="SelectText">查询数据条件</param>
        /// <returns></returns>
        public string IVRecord(string WardCode, string dt, int tags, string SelectText)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" select distinct i.PatCode");
            str.Append(" from dbo.IVRecord i");
            str.Append(" left join IVRecordDetail id on i.IVRecordID=id.IVRecordID ");
            str.Append(" inner join Prescription P on p.PrescriptionID = i.PrescriptionID  and P.Pstatus=2 ");
            //str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" where datediff(day,InfusionDT, '" + dt + "')=0 ");

            str.Append(" and IVStatus=0");
            str.Append(" and IsBatch=1");
            if (WardCode.Trim().Equals("0") && WardCode.Trim().Equals(""))
            {
                //str.Append(" and i.WardCode in (" + WardCode + ") ");
                str.Append(" and exists (select 'x' from DWard dw where dw.WardCode=i.WardCode and dw.WardCode in (" + WardCode + "))");
            }

            str.Append("and i.BatchSaved='" + tags + "'");

            if (pvb.Choose2.Trim().Length > 0 && !pvb.Choose.Equals("#/K/L"))
            {
                str.Append(" " + pvb.Choose2);
            }
            else
            {
                if (!pvb.Choose.Equals("#/K/L"))
                {
                    str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
                }
            }

            if (SelectText.Length > 0)
            {
                str.Append("and (i.BedNo like '%" + SelectText + "%' or i.PatName like '%" + SelectText + "%' ");
                str.Append(" )");
            }
            str.Append("  and LabelOver>=0 ");
            str.Append(" order by PatCode");
            return str.ToString();

        }


        //批次用。 病区列表显示明细数据
        public string INFO(string Groupno)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT DISTINCT DD.DrugName,DD.Spec,");
            str.Append("case when cast(PD.Dosage as float) > cast(cast(PD.Dosage as float) as INT) then cast(PD.Dosage as float)  else cast(cast(PD.Dosage as float) as int) end as 剂量,");
            str.Append("case when  PD.DosageUnit is null then '' else  (PD.DosageUnit) end AS 单位, ");
            str.Append("DgNo, CASE PiShi WHEN 0 THEN '--'else '皮试'  END AS PiShi,PD.Remark7, P.CaseID,");
            str.Append(" (select top (1)FreqName from DFreq df where df.FreqCode=P.FregCode )FreqName,");
            str.Append(" P.Level,P.PStatus,p.UsageName,");
            str.Append("(select top (1)DEmployeeName from DEmployee where DEmployeeCode=DoctorCode)DocName,");
            str.Append("(select top (1)WardName from DWard D where D.WardCode=P.WardCode)WardName,");
            str.Append(" P.PrescriptionID,P.DoctorCode, Convert(varchar,PD.EndDT,20) EndDT ,Convert(varchar,PD.StartDT,20) StartDT, Pa.BedNo, Pa.PatName,");
            str.Append(" Pa.Sex, Pa.Weight, Convert(varchar(10),Pa.Age)+pa.AgeSTR 年龄,DD.UniPreparationID ");
            str.Append(" FROM Prescription P ");
            str.AppendFormat(" INNER JOIN IVRecord I On I.PrescriptionID = P.PrescriptionID and P.Groupno = '{0}'", Groupno);
            str.Append("INNER JOIN PrescriptionDetail PD ON P.PrescriptionID=PD.PrescriptionID ");
            str.Append("inner join IVRecordDetail ivd on ivd.IVRecordID = i.IVRecordID ");
            str.Append("left JOIN Patient Pa ON Pa.PatCode=P.PatientCode ");
            str.Append("left JOIN DDrug DD ON DD.DrugCode=PD.DrugCode ");


            //str.Append(" SELECT DISTINCT DD.DrugName,DD.Spec, ");
            //str.Append("case when cast(IVD.Dosage as float) > cast(cast(IVD.Dosage as float) as INT) then cast(IVD.Dosage as float) else cast(cast(IVD.Dosage as float) as int) end as 剂量,  ");
            //str.Append(" case when  IVD.DosageUnit is null then '' else  (IVD.DosageUnit) end AS 单位,IVD.DgNo, ");
            //str.Append(" CASE PiShi WHEN 0 THEN '--'else '皮试'  END AS PiShi,IVD.Remark7,  DD.UniPreparationID  ");
            //str.Append(" FROM Prescription P  ");
            //str.Append(" INNER JOIN IVRecord I On I.PrescriptionID = P.PrescriptionID  ");
            //str.Append(" inner join IVRecordDetail IVD on ivd.IVRecordID = i.IVRecordID  ");
            //str.Append(" INNER JOIN DEmployee DE1 ON P.DoctorCode=DE1.DEmployeeCode   ");
            //str.Append("  INNER JOIN DWard D ON D.WardCode=P.WardCode   ");
            //str.Append("  INNER JOIN DDrug DD ON DD.DrugCode=IVD.DrugCode ");
            //str.Append(" WHERE P.Groupno = '" + Groupno + "' order by ");

            str.Append(" SELECT  ");
            str.Append(" pd.DrugName,");
            str.Append(" pd.Spec, ");
            str.Append(" case when cast(pd.Dosage as float) > cast(cast(pd.Dosage as float) as INT) then cast(pd.Dosage as float) else cast(cast(pd.Dosage as float) as int) end as 剂量, ");
            str.Append(" case when  pd.DosageUnit is null then '' else  (pd.DosageUnit) end AS 单位,");
            str.Append(" CEILING(Quantity) DgNo, ");
            str.Append(" CASE PiShi WHEN 0 THEN '--'else '皮试'  END AS PiShi,");
            str.Append(" Remark7, DD.UniPreparationID, ");
            str.Append(" CASE pd.DosageUnit WHEN 'ml' THEN pd.Dosage WHEN 'l' THEN (1000 * pd.Dosage) ");
            str.Append("  ELSE isnull(dd.Capacity, 0) * pd.Quantity END AS Remark9,dd.CapacityUnit ");
            str.Append(" FROM  PrescriptionDetail  pd ");
            str.Append(" inner join DDrug dd on dd.DrugCode=pd.DrugCode ");
            str.AppendFormat(" WHERE Groupno = '{0}' order by NoName ", Groupno);
            return str.ToString();
        }

        //批次用。 病区列表显示明细数据
        public string INFO(string Groupno, string dt)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT DISTINCT case IVstatus when  '1' then '已打印' else '' end 'StatasString',");
            str.Append(" FreqName,labelNo,batch,TeamNumber,IVstatus,BatchSaved from IVRecord");
            str.Append(" where LabelNo like '" + dt + "%'");
            str.Append("  and LabelOver>=0 ");
            str.Append(" and Groupno = '" + Groupno + "'");
            str.Append(" and IsBatch=1");
            return str.ToString();
        }

        /// <summary>
        ///  查询瓶签明细中的信息（病区列表）
        /// </summary>
        /// <param name="WarCode"></param>
        /// <param name="dt"></param>
        /// <param name="tags"></param>
        /// <param name="Patcode"></param>
        /// <returns></returns>
        public string IVRecordDetail(string WarCode, string dt, int tags, string Patcode)
        {

            StringBuilder str = new StringBuilder();

            str.Append("SELECT DISTINCT ");
            str.Append(" case when i.IVStatus>=3 then '已打印' when i.BatchSaved = 0 then '未发送' else '已发送' end Status,");
            str.Append(" id.DrugName DrugName1,df.FreqName, case when cast(id.Dosage as float) > cast(cast(id.Dosage as float) as INT) then cast(id.Dosage as float)");
            str.Append(" else cast(cast(id.Dosage as float) as int) end as Dosage,  case when  id.DosageUnit is null then '' else  (id.DosageUnit) end AS Unit,'--' Batch1,i.GroupNo,dd.IsMenstruum,");

            str.Append(" i.WardName,i.BedNo,i.PatName,dd.NoName,BatchSaved,0 as batch,'--' as BatchS");
            str.Append(" ");
            str.Append(" from IVRecordDetail id");
            str.Append(" left join IVRecord i  on i.IVRecordID=id.IVRecordID  ");
            str.Append(" left join Prescription p on p.groupno=i.groupno");
            str.Append(" left join DDrug dd on dd.DrugCode=id.DrugCode");
            //str.Append(" Left join DDrug dd2 on dd2.DrugCode=i.Menstruum");
            str.Append(" left join DFreq df on df.FreqCode=p.FregCode");
            str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" and IsBatch=1");

            if (!WarCode.Equals("") && !WarCode.Equals("0"))
            {
                str.Append(" and i.WardCode in (" + WarCode + ") ");
            }

            if (pvb.Choose2.Trim().Length > 0 && !pvb.Choose.Equals("#/K/L"))
            {
                str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
            }
            else
            {
                if (!pvb.Choose.Equals("#/K/L"))
                {
                    str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
                }
            }
            str.Append(" and patcode='" + Patcode + "' ");
            str.Append(" and LabelOver>=0");
            str.Append(" order by  i.GroupNo,Status,dd.NoName ");

            if (pvb.combineBatch == "0")
            {
                str.Append(" select  i.GroupNo,i.Batch,i.TeamNumber,");
            }
            else
            {
                str.Append(" select distinct i.GroupNo,i.Batch,i.TeamNumber,");
            }
            str.Append(" i.WardName,i.BedNo,i.PatName");
            //str.Append(" from IVRecordDetail id");
            str.Append(" from IVRecord i ");
            //str.Append(" left join IVRecord i  on i.IVRecordID=id.IVRecordID ");
            str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" and IsBatch=1");

            if (pvb.Choose2.Trim().Length > 0 && !pvb.Choose.Equals("#/K/L"))
            {
                str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
            }
            else
            {
                if (!pvb.Choose.Equals("#/K/L"))
                {
                    str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
                }
            }

            if (!WarCode.Equals("") && !WarCode.Equals("0"))
            {
                str.Append(" and i.WardCode in (" + WarCode + ") ");
            }
            str.Append("  and LabelOver>=0 ");
            str.Append(" and patcode='" + Patcode + "' ");
            str.Append(" order by  i.TeamNumber,i.GroupNo,i.Batch");
            return str.ToString();
        }





        /// <summary>
        /// 查询单个用户的批次设置
        /// </summary>
        /// <param name="Ecode"></param>
        /// <returns></returns>
        public string IVRecordSetUp(string Ecode)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" select * from OrderFormSet ");
            str.Append(" where DEmployeeID='" + Ecode + "'");
            return str.ToString();
        }

        /// <summary>
        /// 查询批次的颜色和批次字体颜色
        /// </summary>
        /// <returns></returns>
        public string IVRecordColor()
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" select D.OrderID,OrderColor,OrderTColor from OrderColor OC ");
            str.Append(" right join DOrder D on  oc.OrderID=D.OrderID");
            str.Append(" where IsValid=1");
            str.Append(" order by D.OrderID");
            return str.ToString();
        }

        /// <summary>
        /// 查询批次记录
        /// </summary>
        /// <param name="LogId"></param>
        /// <returns></returns>
        public string OrderLog(string LogId)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select * from OrderLog where Logid='" + LogId + "'");
            return str.ToString();
        }

        /// <summary>
        /// 查询生成
        /// </summary>
        /// <returns></returns>
        public string PBatchTemp()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select LogID from [PBatchTemp] ");
            str.Append(" where DATEDIFF(MI,DT,GETDATE())<15");
            str.Append(" AND ISend=0");
            str.Append(" order by ID desc");
            return str.ToString();
        }

        /// <summary>
        /// 查询所有批次
        /// </summary>
        /// <param name="WardCode"></param>
        /// <returns></returns>
        public string DOrder()
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" select OrderID from DOrder ");
            str.Append(" where IsValid=1");
            str.Append(" order by OrderID");
            return str.ToString();
        }


        /// <summary>
        /// 批次汇总
        /// </summary>
        /// <param name="WardCode">病区编码，格式：'code1','code2'</param>
        /// <param name="dt">瓶签号前八位 +%：20140630%</param>
        /// <param name="tags">是否发送标识 0、未发；1、已发；2、未发和已发</param>
        /// <param name="SelectText">上方查询框文本</param>        
        /// <param name="IsSame">是否有改动</param>
        /// <param name="LongorTemp">长期还是临时（指的是医嘱）</param>
        /// <returns></returns>
        public string BatchNum(string WardCode, string dt, int tags, string SelectText, int IsSame, int LongorTemp)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;


            str.Append(" select Batch ,COUNT(LabelNo) AS count ");
            str.Append(" from dbo.IVRecord i");
            str.Append(" inner join Prescription P on p.PrescriptionID = i.PrescriptionID ");
            str.Append(" left join DDrug dd on dd.DrugCode=i.MarjorDrug ");
            str.Append(" left join DDrug dd2 on dd2.DrugCode=i.Menstruum ");
            //str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" where datediff(day,InfusionDT, '" + dt + "')=0 ");
            str.Append(" and IsBatch=1 and (i.Remark2 <> '-1' or i.Remark2 is null) ");

            switch (LongorTemp)
            {
                case 1:
                    str.Append(" and (P.Remark5 <> 'st' or P.Remark5 is null) ");
                    break;
                case 2:
                    str.Append(" and P.Remark5 = 'st' ");
                    break;
                case 3: break;
                default: break;
            }

            if (!WardCode.Equals("''") && !WardCode.Equals("0"))
            {
                if (!WardCode.Equals("0"))
                {
                    //str.Append(" and i.WardCode in(" + WardCode + ") ");                    
                    str.Append(" and exists (select 'x' from DWard dw where dw.WardCode=i.WardCode and dw.WardCode in (" + WardCode + "))");
                }
            }
            if (tags < 2)
            {
                str.Append(" and IVStatus=0 ");
                str.Append("and BatchSaved='" + tags + "'");
            }
            else
            {
                str.Append("and IVStatus > = 3 ");
            }

            if (SelectText.Trim().Length > 0)
            {

                str.Append("  and (i.BedNo like '%" + SelectText + "%' or PatName like '%" + SelectText + "%' ");
                str.Append(" or PatCode like '%" + SelectText + "%' or i.GroupNo like '%" + SelectText + "%' or dd.DrugName like '%" + SelectText + "%' or FreqName like '%" + SelectText + "%')");

            }

            if (IsSame < 2)
            {
                if (IsSame == 0)
                {
                    //str.Append(" and PatCode not in ");
                    //str.Append("(select distinct PatCode from IVRecord where IsSame='1' ");
                    ////str.Append(" and LabelNo like '" + dt + "%' and IVStatus=0 ");
                    //str.Append(" datediff(day,InfusionDT,  '" + dt + "')=0 and IVStatus=0 ");
                    //if (!WardCode.Equals("") && !WardCode.Equals("0"))
                    //{

                    //    str.Append("  and WardCode in(" + WardCode + ") ");
                    //    str.Append(" and LabelOver>=0)");
                    //}

                    str.Append(" and IsSame = '0' ");
                }
                else
                {
                    //str.Append(" and PatCode in ");
                    //str.Append("(select distinct PatCode from IVRecord where IsSame='1' ");
                    ////str.Append(" and LabelNo like '" + dt + "%' and IVStatus=0 ");
                    //str.Append(" datediff(day,InfusionDT,  '" + dt + "')=0 and IVStatus=0 ");
                    //if (!WardCode.Equals("") && !WardCode.Equals("0"))
                    //{

                    //    str.Append("  and WardCode in(" + WardCode + ")");
                    //}
                    //str.Append("and LabelOver>=0)");

                    str.Append(" and IsSame = '1' ");
                }
            }

            str.Append(pvb.Choose2);

            str.Append("  and LabelOver>=0");
            str.Append(" group by Batch  order by batch ");

            return str.ToString();
        }

        /// <summary>
        /// 瓶签信息(批次用)
        /// </summary>
        /// <param name="WardCode">病区Code</param>
        /// <param name="dt">时间</param>
        /// <param name="tags">状态，已发送，未发送，已打印</param>
        /// <param name="IsSame">是否改动</param>
        /// <param name="SelectText">查询数据条件</param>
        /// <param name="LongorTemp">是长期医嘱还是临时医嘱产生的瓶签：1，长期、2，临时，3长期和临时</param>
        /// <returns></returns>
        public string IVRecord(string WardCode, string dt, int tags, string SelectText, string Wardcode, int IsSame, int LongorTemp, string getdrugtype)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            StringBuilder str1 = new StringBuilder();
            str1.Length = 0;
            str1.Append(" select distinct PatCode from ivrecord i");
            str1.Append(" inner join Prescription P on p.PrescriptionID = i.PrescriptionID  ");
            str1.Append(" left join DDrug dd on dd.DrugCode=i.MarjorDrug ");
            str1.Append(" left join DDrug dd2 on dd2.DrugCode=i.Menstruum ");
            //str1.Append(" where LabelNo like '" + dt + "%'");
            str1.Append(" where datediff(day,InfusionDT, '" + dt + "')=0 ");

            str1.Append(" and IsBatch=1 and (i.Remark2 <> '-1' or i.Remark2 is null) ");

            str.Append(" select distinct i.BedNo 床号,i.PatName 姓名,Batch 批次,dd.DrugName 主药,dd2.DrugName 溶媒,P.UsageName 用药途径,FreqName 频序,(i.WardName)+'('+i.WardCode+')' 病区,InfusionDT 用药时间,i.GroupNo 组号,");
            str.Append(" LabelNo 瓶签号,PrintDt 打印时间, PrinterName 打印人,PatCode 病人编码,TeamNumber,BatchRule 批次规则,CASE IsSame WHEN 1 THEN '有改动' else '无改动' end 是否改动,i.Remark1 备注,i.IVStatus");
            str.Append(" from dbo.IVRecord i");
            str.Append(" inner join Prescription P on p.PrescriptionID = i.PrescriptionID ");
            str.Append(" left join DDrug dd on dd.DrugCode=i.MarjorDrug ");
            str.Append(" left join DDrug dd2 on dd2.DrugCode=i.Menstruum ");
            //str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" where datediff(day,InfusionDT, '" + dt + "')=0 ");

            str.Append(" and IsBatch=1 and ((i.Remark2 <> '-1' and i.Remark2 <> '-2') or i.Remark2 is null ) ");

            switch (LongorTemp)
            {
                case 1:
                    str.Append(" and (P.Remark5 <> 'st' or P.Remark5 is null) ");
                    str1.Append(" and (P.Remark5 <> 'st' or P.Remark5 is null) ");
                    break;
                case 2:
                    str.Append(" and P.Remark5 = 'st' ");
                    str1.Append(" and P.Remark5 = 'st' ");
                    break;
                case 3: break;
                default: break;
            }

            if (!WardCode.Equals("") && !WardCode.Equals("0"))
            {
                if (!WardCode.Equals("0"))
                {
                    //str.Append(" and i.WardCode in(" + WardCode + ") ");
                    //str1.Append(" and i.WardCode in(" + WardCode + ")");
                    str.Append(" and exists (select 'x' from DWard dw where dw.WardCode=i.WardCode and dw.WardCode in (" + WardCode + "))");
                    str1.Append(" and exists (select 'x' from DWard dw where dw.WardCode=i.WardCode and dw.WardCode in (" + WardCode + "))");
                }
            }
            if (tags < 2)
            {
                str.Append(" and IVStatus=0 ");
                str.Append("and BatchSaved='" + tags + "'");
                str1.Append(" and IVStatus=0 ");
                str1.Append("and BatchSaved='" + tags + "'");
            }
            else
            {
                str.Append("and IVStatus > = 3 ");
                str1.Append("and IVStatus > = 3 ");
            }

            if (SelectText.Trim().Length > 0)
            {

                str.Append("  and (i.BedNo like '%" + SelectText + "%' or PatName like '%" + SelectText + "%' ");
                str.Append(" or PatCode like '%" + SelectText + "%' or i.GroupNo like '%" + SelectText + "%' or dd.DrugName like '%" + SelectText + "%' or FreqName like '%" + SelectText + "%')");

                str1.Append("  and (i.BedNo like '%" + SelectText + "%' or PatName like '%" + SelectText + "%' ");
                str1.Append(" or PatCode like '%" + SelectText + "%' or i.GroupNo like '%" + SelectText + "%' or dd.DrugName like '%" + SelectText + "%' or FreqName like '%" + SelectText + "%')");
            }

            if (IsSame < 2)
            {
                if (IsSame == 0)
                {
                    str.Append(" and IsSame = '0' ");
                    //str.Append(" and PatCode not in ");
                    //str.Append("(select distinct PatCode from IVRecord where IsSame='1' ");
                    //str.Append(" and LabelNo like '" + dt + "%' and IVStatus=0 ");
                    //str.Append(" and LabelNo like '" + dt + "%' ");
                    //str.Append(" and datediff(day,InfusionDT, '" + dt + "')=0 ");
                    //if (!WardCode.Equals("") && !WardCode.Equals("0"))
                    //{

                    //    str.Append("  and WardCode in(" + WardCode + ") ");
                    //    str.Append(" and LabelOver>=0)");
                    //}
                }
                else
                {
                    str.Append(" and IsSame = '1' ");
                    //str.Append(" and PatCode in ");
                    //str.Append("(select distinct PatCode from IVRecord where IsSame='1' ");
                    //str.Append(" and LabelNo like '" + dt + "%' and IVStatus=0 ");
                    //str.Append(" and LabelNo like '" + dt + "%'  ");
                    //str.Append(" and datediff(day,InfusionDT, '" + dt + "')=0 ");

                    //if (!WardCode.Equals("") && !WardCode.Equals("0"))
                    //{

                    //    str.Append("  and WardCode in(" + WardCode + ")");
                    //}
                    //str.Append("and LabelOver>=0)");
                }
            }
            //if (pvb.Choose2.Trim().Length>0 && !pvb.Choose.Equals("#/K/L"))
            //{
            //    str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
            //    str1.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
            //}
            //else
            //{
            //    if (!pvb.Choose.Equals("#/K/L"))
            //    {
            //      //  str.Append(" and Batch like '%" + pvb.Choose + "%' ");
            //        str.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
            //        str1.Append(" and Batch like '%" + pvb.Choose + "%' " + pvb.Choose2);
            //       // str1.Append(" and Batch like '%" + pvb.Choose + "%'  ");
            //    }
            //}

            str.Append(pvb.Choose2);
            str1.Append(pvb.Choose2);

            str.Append("  and LabelOver>=0");
            str1.Append("  and LabelOver>=0 and IsSame=1");

            if (getdrugtype != "")
            {
                str.Append("and p.DrugType in(" + getdrugtype + ")");
                str1.Append("and p.DrugType in(" + getdrugtype + ")");
            }
            else
            {
                str.Append("and 1=0 ");
                str1.Append("and 1=0 ");
            }



            str.Append(" order by i.BedNo,PatCode,TeamNumber ");
            str1.Append(" order by PatCode");

            //System.Windows.Forms.MessageBox.Show(str.ToString() + str1.ToString());

            return str.ToString() + str1.ToString();
        }

        /// <summary>
        /// 病人列表模式下。查询病人瓶签详细信息
        /// </summary>
        /// <param name="WardCode"></param>
        /// <param name="dt"></param>
        /// <param name="s">BatchSaved</param>
        /// <param name="tags"></param>
        /// <param name="SelectText"></param>
        /// <param name="LabelOrderBy"></param>
        /// <returns></returns>
        public string IVRecordPatient(string WardCode, string dt, int s, bool tags, string SelectText, int LabelOrderBy, string BatchSelect)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" select Count(LabelNo),patcode,patname from IVRecord");
            //str.Append(" where WardCode in (" + WardCode + ") ");
            //str.Append(" and LabelNo like '" + dt + "%'");
            str.Append(" where  LabelNo like '" + dt + "%'");
            str.Append("  and patcode='" + SelectText + "'");
            str.Append(" and IsBatch=1");
            if (WardCode != "")
            {
                str.Append(" and WardCode in (" + WardCode + ") ");
            }
            if (tags)
            {
                str.Append(" and ivstatus=1");
            }
            else
            {
                str.Append(" and ivstatus=0");
                if (s != 2)
                {
                    str.Append(" and BatchSaved=" + s);
                }
            }

            if (SelectText != "")
            {

                str.Append("and patcode='" + SelectText + "'");
            }
            str.Append("  and LabelOver>=0 ");

            str.Append(" group by patcode,PatName");
            str.Append(" select distinct i.PatCode,Batch,BatchRule,OC.OrderColor,OC.OrderTColor as TColor,TeamNumber,i.WardCode,i.GroupNo,LabelNo,i.IsSame,");
            str.Append(" FreqName,id.Spec,id.DrugName,i.PatName,i.BedNo,CASE WHEN sex= '1' THEN '男' WHEN sex= '2' THEN '女' ELSE '其他' END sex,");
            str.Append(" age,i.InsertDT,case BatchSaved when 0 then '未发送' else '已发送' end BatchSaved,DgNo,");
            str.Append(" case IVStatus when 0 then '未打印' else '已打印' end IVStatus,");
            str.Append(" case BatchSaved when 0 then '未发送' else '已发送' end IsCommand,");
            str.Append(" case when cast(id.Dosage as float) > cast(cast(id.Dosage as float) as INT) then cast(id.Dosage as float)");
            str.Append(" else cast(cast(id.Dosage as float) as int) end as Dosage,");
            str.Append(" case when  id.DosageUnit is null then '' else  id.DosageUnit end AS DosageUnit ");
            str.Append(" from dbo.IVRecord i");
            str.Append(" left join IVRecordDetail id on i.IVRecordID=id.IVRecordID ");
            str.Append(" left join OrderColor OC on OC.OrderID=i.TeamNumber");
            str.Append(" inner join Prescription P on i.GroupNo=P.GroupNo ");
            str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" and IsBatch=1 ");

            str.Append(BatchSelect);

            if (WardCode != "")
            {
                str.Append(" and  i.WardCode in (" + WardCode + ") ");
            }
            if (tags)
            {
                str.Append(" and ivstatus=1");
            }
            else
            {
                str.Append(" and ivstatus=0");

                if (s != 2)
                {
                    str.Append(" and BatchSaved=" + s);
                }
            }
            if (SelectText != "")
            {

                str.Append("  and patcode='" + SelectText + "'");
            }
            str.Append("  and LabelOver>=0 ");
            str.Append(" order by TeamNumber,");
            if (LabelOrderBy == 0)
            {
                str.Append("i.LabelNo,");
            }
            else if (LabelOrderBy == 1)
            {
                str.Append("i.FreqCode,");
            }
            else
            {
                str.Append("i.GroupNo,");
            }
            str.Append("PatCode,DgNo");
            return str.ToString();
        }

        /// <summary>
        /// 病人列表模式下。查询病人瓶签详细信息
        /// </summary>
        /// <param name="WardCode"></param>
        /// <param name="dt"></param>
        /// <param name="s">BatchSaved</param>
        /// <param name="tags"></param>
        /// <param name="SelectText"></param>
        /// <param name="LabelOrderBy"></param>
        /// <returns></returns>
        public string IVRecordPatient(string WardCode, string dt, int s, bool tags, string SelectText, int LabelOrderBy)
        {
            string send = db.GetPivasAllSet("批次-病人明细模式-未发送-已发送已打印瓶签");
            string print = db.GetPivasAllSetValue2("批次-病人明细模式-未发送-已发送已打印瓶签");

            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" select Count(LabelNo),patcode,patname from IVRecord i ");
            //str.Append(" where WardCode in (" + WardCode + ") ");
            //str.Append(" and LabelNo like '" + dt + "%'");
            str.Append(" where  LabelNo like '" + dt + "%'");
            str.Append("  and patcode='" + SelectText + "'");
            str.Append(" and IsBatch=1");
            if (WardCode != "")
            {
                str.Append(" and WardCode in (" + WardCode + ") ");
            }
            if (s == 2)
            {
                str.Append(" and ivstatus >=3 ");
            }
            else
            {
                if (tags)
                {
                    str.Append(" and ivstatus=1");
                }
                else
                {
                    str.Append(" and ivstatus=0");
                    //if (s != 2)
                    //{
                    str.Append(" and BatchSaved=" + s);
                    //}
                }
            }
            if (SelectText != "")
            {

                str.Append("and patcode='" + SelectText + "'");
            }
            str.Append("  and LabelOver>=0 ");
            str.Append(" and (i.Remark2 <> '-1' or i.Remark2 is null or i.Remark2 <> '-2') ");
            str.Append(" group by patcode,PatName");


            str.Append(" select distinct i.PatCode,Batch,BatchRule,OC.OrderColor,OC.OrderTColor as TColor,P.UsageName, TeamNumber,i.WardCode,i.WardName,i.GroupNo,LabelNo,i.IsSame,");
            str.Append(" FreqName,id.Spec,id.DrugName,pa.PatName,pa.BedNo,CASE WHEN pa.sex= '1' THEN '男' WHEN pa.sex= '2' THEN '女' ELSE '其他' END sex,");
            str.Append("  pa.age,pa.AgeSTR, i.InsertDT,case BatchSaved when 0 then '未发送' else '已发送' end BatchSaved,DgNo,");
            str.Append(" case IVStatus when 0 then '未打印' else '已打印' end IVStatus,");
            str.Append(" case BatchSaved when 0 then '未发送' else '已发送' end IsCommand,");
            str.Append(" case when cast(id.Dosage as float) > cast(cast(id.Dosage as float) as INT) then cast(id.Dosage as float)");
            str.Append(" else cast(cast(id.Dosage as float) as int) end as Dosage,");
            str.Append(" case when  id.DosageUnit is null then '' else  id.DosageUnit end AS DosageUnit ");
            str.Append(" , CASE id.DosageUnit WHEN 'ml' THEN id.Dosage WHEN 'l' THEN (1000 * id.Dosage)  ");
            str.Append("  ELSE isnull(dd.Capacity, 0) * id.DgNo END AS Remark9,dd.CapacityUnit  ");
            str.Append(" from dbo.IVRecord i");
            str.Append("  left join Patient pa on pa.PatCode=i.PatCode ");
            str.Append(" left join IVRecordDetail id on i.IVRecordID=id.IVRecordID ");
            str.Append(" left join DDrug dd on dd.DrugCode=id.DrugCode ");
            str.Append(" left join OrderColor OC on OC.OrderID=i.TeamNumber");
            str.Append(" inner join Prescription P on i.GroupNo=P.GroupNo ");
            str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" and IsBatch=1 ");




            if (WardCode != "")
            {
                str.Append(" and  i.WardCode in (" + WardCode + ") ");
            }
            if (s == 2)
            {
                str.Append(" and ivstatus >=3 ");
            }
            else
            {
                if (tags)
                {
                    str.Append(" and ivstatus=1");
                }
                else
                {
                    if (print == "0")
                    {
                        str.Append(" and ivstatus=0");
                    }
                    if (send == "0")
                    {
                        str.Append(" and BatchSaved=" + s);
                    }
                }
            }
            if (SelectText != "")
            {

                str.Append("  and i.patcode='" + SelectText + "'");
            }
            str.Append("  and LabelOver>=0 ");
            str.Append(" and ((i.Remark2 <> '-1' and i.Remark2 <> '-2') or i.Remark2 is null ) ");
            str.Append(" order by TeamNumber,");
            if (LabelOrderBy == 0)
            {
                str.Append("i.LabelNo,");
            }
            else if (LabelOrderBy == 1)
            {
                str.Append("i.FreqCode,");
            }
            else
            {
                str.Append("i.GroupNo,");
            }
            str.Append("i.PatCode,DgNo");
            return str.ToString();
        }
    }
}
