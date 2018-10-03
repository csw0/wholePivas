using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PIVAsDBhelp;
using System.Xml;
using System.IO;

using TransTuiYao.ServiceReference2;

namespace TransTuiYao
{
    class MakeYtjXml
    {
        ServiceReference2.UpInterfaceSoapClient upin = new UpInterfaceSoapClient();

        DB_Help DB = new DB_Help();

        public void transITJ()
        {
            try
            {
                DataSet ds = getResourse();
                string transXML = makeXml(ds);

                if (string.IsNullOrEmpty(transXML))
                    return;//没有数据需要发送

                DB.PreserveLog("Debug", "发送给英特吉的xml数据：" + transXML);
                string s = upin.LabelInfo(transXML);
                DB.PreserveLog("Debug", "英特吉返回的xml数据：" + s);

                DataSet dsxml = new DataSet();
                StringReader read = new StringReader(s);
                XmlTextReader readxml = new XmlTextReader(read);
                dsxml.ReadXml(readxml);
                DataTable dt1 = dsxml.Tables[0];
                int i = int.Parse(dt1.Rows[0]["code"].ToString());
                if (i == 1)
                {
                    //将瓶签批次的保存时间保存到英特吉瓶签表
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            //因BatchSavedDT在IVRecord中格式是YYYYMMDDHHmm.fff,直接用ToString丢失毫秒
                            string strBatchSavedDT = (Convert.ToDateTime(r["BatchSavedDT"])).ToString("yyyy-MM-dd HH:mm:ss.fff");
                            updateDB(r["LabelNo"].ToString().Trim(), strBatchSavedDT);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        /// <summary>
        /// 搜索需要向英特吉发送的瓶签资源
        /// </summary>
        /// <returns></returns>
        private DataSet getResourse()
        {
            string labelSql = "select LabelNo,BatchSavedDT from IVRecord(nolock) iv where IVRecordID in " +
            //从英特吉返回的药品表，需存在才发送
            "(select ivd.IVRecordID from IVRecordDetail(nolock) ivd where DrugCode in (select DrugCode from Drug_YTJ)) " +
            //已进入打印的下个流程的，不再发送给英特吉去打印；and Remark3 >= 10；
            //BatchSaved标记是否已在界面点击发送（已发送=1，已发送后才能发给英特吉）
            "and iv.IVStatus <= 5  and iv.BatchSaved = 1 " +
            //已因各种原因被取消的瓶签，不再发送给英特吉去打印
            "and iv.LabelOver >= 0 " + "and DATEDIFF(DD, InfusionDT, GETDATE()) < 2 " +
            //发送成功保存在IVRecordToYTJ中，再次发送时，需不在此表中。例外：批次更新后需重新发送
            "and not exists (select * from IVRecordToYTJ ivi where ivi.labelno = iv.labelno "+
            "and ISNULL(ivi.BatchSavedDT,1)=ISNULL(iv.BatchSavedDT,1)) union "+

            //union 营养药
            "select LabelNo,BatchSavedDT from IVRecord(nolock) iv " +
            "inner join Prescription p on iv.GroupNo =p.GroupNo where " +              
            //已进入打印的下个流程的，不再发送给英特吉去打印；and Remark3 >= 10；
            //BatchSaved标记是否已在界面点击发送（已发送=1，已发送后才能发给英特吉）
            "iv.IVStatus <= 5  and iv.BatchSaved = 1 " +
            //已因各种原因被取消的瓶签，不再发送给英特吉去打印
            "and iv.LabelOver >= 0 " + "and DATEDIFF(DD, InfusionDT, GETDATE()) < 2 " +
            //发送成功保存在IVRecordToYTJ中，再次发送时，需不在此表中。例外：批次更新后需重新发送
            "and not exists (select * from IVRecordToYTJ ivi where ivi.labelno = iv.labelno " +
            "and ISNULL(ivi.BatchSavedDT,1)=ISNULL(iv.BatchSavedDT,1)) " +
            //判断是TPN用药方式的才能送；根据drugType只能确定药品的类别，但可能营养医嘱里面含有普通药物
            "and (p.UsageCode = 'TPN' or p.UsageCode = '111' or p.UsageCode = '1' or p.DrugType = 4)";

            DataSet labelDs = DB.GetPIVAsDB(labelSql);

            return labelDs;
        }

        private string makeXml(DataSet labelDs)
        {
            try
            {
                //瓶签xml
                string labelXml = "";

                if (labelDs.Tables.Count > 0 && !(labelDs == null) && labelDs.Tables[0].Rows.Count > 0)
                {
                    labelXml += "<Labels>\n";
                    
                    foreach (DataRow r in labelDs.Tables[0].Rows)
                    {
                        //查询瓶签信息
                        string labelNo = r["LabelNo"].ToString().Trim();

                        string labelInfoSql = "  select cast(iv.GroupNo as decimal(18,0)) RecipeID,cast(iv.GroupNo as decimal(18,0)) "+
                            "GroupNo,cast(p.CaseID as decimal(18,0)) ZYNo,iv.labelno LabelNO, "+
                            //csw修改,不是营养处方，就认为是化疗处方；
                            "CASE WHEN p.DrugType=4 THEN '营' WHEN p.UsageCode = 'TPN' THEN '营' WHEN p.UsageCode = '111' THEN '营' " +
                            "WHEN p.UsageCode = '1' THEN '营' ELSE '化' END PresType," +
                            "de.DEmployeeName DoctorName," +
                            "(SELECT DEmployeeName FROM  dbo.DEmployee WHERE (DEmployeeCode = P.DrawerCode)) NurseName,"+
                            "CASE WHEN '1'=iv.Remark4 THEN 1 ELSE 0 END IsTemporary,iv.Batch BatchNo,iv.InfusionDT LabelDate,iv.InfusionDT "+
                            "UseTime,iv.WardCode WardNo,iv.WardName WardName,iv.BedNo BedNo,PatientCode PatientCode,PatName PatientName,"+
                            "iv.Sex PatientSex,iv.Age PatientAge,ABS(iv.IsPack - 1) Iscompound,RTRIM(p.UsageName) UsageRoute,"+
                            "iv.FreqCode FregCode,FreqNum LabelIndex, (SELECT TimesOfDay FROM dbo.DFreq AS df WHERE (FreqCode = p.FregCode)) "+
                            "LabelTotal,p.DrugCount DrugNum,ISNULL(p.Remark1, '') presremark1,'' presremark2,'' presremark3,"+
                            "ISNULL((select DEmployeeCode from DEmployee where DEmployeeID=(SELECT max(cp.[CheckDCode]) FROM [dbo].[CPRecord] cp "+
                            "where cp.PrescriptionID=iv.PrescriptionID)),'') ExminePerson from IVRecord iv left join Prescription p "+
                            "on iv.GroupNo = p.GroupNo left join DEmployee de on p.DoctorCode = de.DEmployeeCode ";

                        labelInfoSql += "where iv.LabelNo = ";
                        labelInfoSql += "'" + labelNo + "'";

                        DataSet labelinfoDs = DB.GetPIVAsDB(labelInfoSql);

                        labelXml += "\t<Label>\n";
                        labelXml += "\t\t<RecipeID>" + labelinfoDs.Tables[0].Rows[0]["RecipeID"].ToString().Trim() + "</RecipeID>\n";
                        labelXml += "\t\t<GroupNo>" + labelinfoDs.Tables[0].Rows[0]["GroupNo"].ToString().Trim() + "</GroupNo>\n";
                        labelXml += "\t\t<ZYNo>" + labelinfoDs.Tables[0].Rows[0]["ZYNo"].ToString().Trim() + "</ZYNo>\n";
                        labelXml += "\t\t<LabelNO>" + labelinfoDs.Tables[0].Rows[0]["LabelNO"].ToString().Trim() + "</LabelNO>\n";
                        labelXml += "\t\t<PresType>" + labelinfoDs.Tables[0].Rows[0]["PresType"].ToString().Trim() + "</PresType>\n";
                        labelXml += "\t\t<DoctorName>" + labelinfoDs.Tables[0].Rows[0]["DoctorName"].ToString().Trim() + "</DoctorName>\n";
                        labelXml += "\t\t<NurseName>" + labelinfoDs.Tables[0].Rows[0]["NurseName"].ToString().Trim() + "</NurseName>\n";
                        labelXml += "\t\t<IsTemporary>" + labelinfoDs.Tables[0].Rows[0]["IsTemporary"].ToString().Trim() + "</IsTemporary>\n";
                        labelXml += "\t\t<BatchNo>" + labelinfoDs.Tables[0].Rows[0]["BatchNo"].ToString().Trim() + "</BatchNo>\n";
                        labelXml += "\t\t<PresDate>" + labelinfoDs.Tables[0].Rows[0]["LabelDate"].ToString().Trim() + "</PresDate>\n";
                        labelXml += "\t\t<UseTime>" + labelinfoDs.Tables[0].Rows[0]["UseTime"].ToString().Trim() + "</UseTime>\n";
                        labelXml += "\t\t<WardNo>" + labelinfoDs.Tables[0].Rows[0]["WardNo"].ToString().Trim() + "</WardNo>\n";
                        labelXml += "\t\t<WardName>" + labelinfoDs.Tables[0].Rows[0]["WardName"].ToString().Trim() + "</WardName>\n";
                        labelXml += "\t\t<BedNo>" + labelinfoDs.Tables[0].Rows[0]["BedNo"].ToString().Trim() + "</BedNo>\n";
                        labelXml += "\t\t<PatientCode>" + labelinfoDs.Tables[0].Rows[0]["PatientCode"].ToString().Trim() + "</PatientCode>\n";
                        labelXml += "\t\t<PatientName>" + labelinfoDs.Tables[0].Rows[0]["PatientName"].ToString().Trim() + "</PatientName>\n";
                        labelXml += "\t\t<PatientSex>" + labelinfoDs.Tables[0].Rows[0]["PatientSex"].ToString().Trim() + "</PatientSex>\n";
                        labelXml += "\t\t<PatientAge>" + labelinfoDs.Tables[0].Rows[0]["PatientAge"].ToString().Trim() + "</PatientAge>\n";
                        labelXml += "\t\t<IsCompound>" + labelinfoDs.Tables[0].Rows[0]["Iscompound"].ToString().Trim() + "</IsCompound>\n";
                        labelXml += "\t\t<UsageRoute>" + labelinfoDs.Tables[0].Rows[0]["UsageRoute"].ToString().Trim() + "</UsageRoute>\n";
                        labelXml += "\t\t<FreqCode>" + labelinfoDs.Tables[0].Rows[0]["FregCode"].ToString().Trim() + "</FreqCode>\n";
                        labelXml += "\t\t<LabelIndex>" + labelinfoDs.Tables[0].Rows[0]["LabelIndex"].ToString().Trim() + "</LabelIndex>\n";
                        labelXml += "\t\t<LabelTotal>" + labelinfoDs.Tables[0].Rows[0]["LabelTotal"].ToString().Trim() + "</LabelTotal>\n";
                        labelXml += "\t\t<DrugNum>" + labelinfoDs.Tables[0].Rows[0]["DrugNum"].ToString().Trim() + "</DrugNum>\n";
                        labelXml += "\t\t<Presremark1>" + labelinfoDs.Tables[0].Rows[0]["presremark1"].ToString().Trim() + "</Presremark1>\n";
                        labelXml += "\t\t<Presremark2>" + labelinfoDs.Tables[0].Rows[0]["presremark2"].ToString().Trim() + "</Presremark2>\n";
                        labelXml += "\t\t<Presremark3>" + labelinfoDs.Tables[0].Rows[0]["presremark3"].ToString().Trim() + "</Presremark3>\n";
                        labelXml += "\t\t<ExaminePerson>" + labelinfoDs.Tables[0].Rows[0]["ExminePerson"].ToString().Trim() + "</ExaminePerson>\n";
                        labelXml += "\t\t<Drugs>\n";;

                        string drugSql = " select dd.TheDrugType DrugType,ivd.DrugCode DrugCode,ivd.DrugName DrugName,ivd.Spec DrugUnit,ivd.DgNo DrugAMT," +
                            "ivd.Dosage DosageAMT,ivd.DosageUnit DosageUnit,dd.AsMajorDrug IsMajorDrug,dd.IsMenstruum  IsMenstruum,"+
                            "CASE WHEN 0 < CHARINDEX('嘱托', ivd.DrugName) THEN 1 ELSE 0 END  IsSelf,case ivd.DosageUnit when 'ml' "+
                            "then ivd.Dosage when 'l' then (1000*ivd.Dosage) else isnull(dd.Capacity,0)*ivd.DgNo end Capacity,"+
                            "(SELECT TOP 1 aa from (SELECT LabelNo,aa=stuff((select ','+DrugPlusConditionName FROM [dbo].[V_DrugAC] dd "+
                            "WHERE dd.LabelNo = d.LabelNo FOR XML PATH('')),1,1,'') from [dbo].[V_DrugAC] d)a where iv.LabelNo = a.LabelNo) "+
                            "DrugRemark1,pd.Quantity DrugRemark2,'' DrugRemark3,'' DrugRemark4 from IVRecord(nolock) iv left join IVRecordDetail(nolock) ivd on " +
                            "iv.IVRecordID = ivd.IVRecordID left join DDrug(nolock) dd on ivd.DrugCode = dd.DrugCode "+
                            "left join PrescriptionDetail(nolock) pd on pd.GroupNo=iv.GroupNo and pd.DrugCode=ivd.DrugCode ";

                        drugSql += "where iv.LabelNo = ";
                        drugSql += "'" + labelNo + "'";

                        DataSet drugDS = DB.GetPIVAsDB(drugSql);

                        foreach (DataRow rr in drugDS.Tables[0].Rows)
                        {
                            DB.PreserveLog("Info", "药品表中药品类型=" + rr["DrugType"].ToString().Trim());

                            labelXml += "\t\t\t<Drug>\n";
                            labelXml += "\t\t\t\t<DrugCode>" + rr["DrugCode"].ToString().Trim() + "</DrugCode>\n";
                            labelXml += "\t\t\t\t<DrugName>" + rr["DrugName"].ToString().Trim() + "</DrugName>\n";
                            labelXml += "\t\t\t\t<DrugUnit>" + rr["DrugUnit"].ToString().Trim() + "</DrugUnit>\n";
                            labelXml += "\t\t\t\t<DrugAMT>" + rr["DrugAMT"].ToString().Trim() + "</DrugAMT>\n";
                            labelXml += "\t\t\t\t<DosageAmt>" + rr["DosageAMT"].ToString().Trim() + "</DosageAmt>\n";
                            labelXml += "\t\t\t\t<DosageUnit>" + rr["DosageUnit"].ToString().Trim() + "</DosageUnit>\n";
                            labelXml += "\t\t\t\t<IsMajorDrug>" + rr["IsMajorDrug"].ToString().Trim() + "</IsMajorDrug>\n";
                            labelXml += "\t\t\t\t<IsMenstruum>" + rr["IsMenstruum"].ToString().Trim() + "</IsMenstruum>\n";
                            labelXml += "\t\t\t\t<IsSelf>" + rr["IsSelf"].ToString().Trim() + "</IsSelf>\n";
                            labelXml += "\t\t\t\t<Capacity>" + rr["Capacity"].ToString().Trim() + "</Capacity>\n";
                            labelXml += "\t\t\t\t<DrugRemark1>" + rr["DrugRemark1"].ToString().Trim() + "</DrugRemark1>\n";
                            labelXml += "\t\t\t\t<DrugRemark2>" + rr["DrugRemark2"].ToString().Trim() + "</DrugRemark2>\n";
                            labelXml += "\t\t\t\t<DrugRemark3>" + rr["DrugRemark3"].ToString().Trim() + "</DrugRemark3>\n";
                            labelXml += "\t\t\t\t<DrugRemark4>" + rr["DrugRemark4"].ToString().Trim() + "</DrugRemark4>\n";
                            labelXml += "\t\t\t</Drug>\n";
                        }

                        labelXml += "\t\t</Drugs>\n";
                        labelXml += "\t</Label>\n";
                    }

                    labelXml += "</Labels>\n";
                }
                return labelXml;
            }
            catch (Exception ex)
            {
                //处理异常
                return "";
            }
        }

        private void updateDB(string labelNo,string strBatchSavedDT)
        {
            try
            {
                string sqlExist = "select * from IVRecordToYTJ where IVRecordToYTJ.LabelNo = '" + labelNo + "'";
                DataSet dsExist =  DB.GetPIVAsDB(sqlExist);
                bool bExist = false;
                if (dsExist != null && dsExist.Tables.Count > 0  && dsExist.Tables[0].Rows.Count > 0)
                    bExist = true;

                string ustr = String.Empty;
                //如存在，则需更新;结合前置条件，可以判定时批次更新了，才会更新
                if (bExist)
                {
                    ustr = " Update IVRecordToYTJ set RQ=GETDATE(),BatchSavedDT ='" + strBatchSavedDT + "' where LabelNo = '"+labelNo+"'";
                }
                else//不存在，则插入
                {
                    if (string.IsNullOrEmpty(strBatchSavedDT))
                    {
                        //IVRecord表中批次保存日期为NULL（可能是直接从HIS获取批次未在pivas中生成），也保存NULL
                        ustr = " insert into IVRecordToYTJ(LabelNo,RQ,BatchSavedDT) values('" + labelNo + "', GETDATE(),NULL)";
                    }
                    else
                    {
                        ustr = " insert into IVRecordToYTJ(LabelNo,RQ,BatchSavedDT) values('" + labelNo + "', GETDATE(),'" 
                            + strBatchSavedDT + "')";
                    }
                }
                DB.SetPIVAsDB(ustr);
            }
            catch (Exception ex)
            {
 
            }
        }
    }
}
