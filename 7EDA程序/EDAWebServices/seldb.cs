using System.Data;
using System.Text;
using PIVAsDBhelp;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace EDAWebServices
{
    /// <summary>
    /// 数据库sql集合
    /// </summary>
    public class seldb
    {
        protected static string conn = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
        SqlConnection con = new SqlConnection(conn);

        /// <summary>
        /// 扫描总瓶签
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Select"></param>
        /// <returns></returns>
        public DataSet IVRecordPrint(string Code)
        {
           
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct IVRecord_Print.LabelNo from IVRecord_Print ");
            sql.Append("inner join IVRecord on IVRecord_Print.LabelNo=IVRecord.LabelNo ");
            sql.Append("  where (DrugQRCode='" + Code + "'or OrderQRCode='" + Code + "')");
            SqlDataAdapter sda = new SqlDataAdapter(sql.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 查询瓶签的信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetLabelInfor(string LabelNo,string othercond)
        {

            StringBuilder str = new StringBuilder();
            str.Append("    select iv.LabelNo,iv.ivstatus,iv.PatName,iv.PatCode,iv.Batch,iv.Remark5,p.UsageName ");
            str.Append("    ,iv.WardName,d.WardCode,pa.BedNo,pa.Age,pa.AgeSTR ");
            str.Append("    from IVRecord iv ");
            str.Append("    left join Patient pa on iv.PatCode=pa.PatCode ");
            str.Append("    left join DWard d on d.WardCode=pa.WardCode ");
            str.Append("    left join Prescription p on p.PrescriptionID=iv.PrescriptionID ");
            str.Append("    where 1=1 ");
            if (LabelNo != "" && LabelNo != string.Empty)
            {
                str.Append("    and LabelNo = '" + LabelNo + "'");
                str.Append("    or iv.PrescriptionID='" + LabelNo + "'");//处方号，差错用
            }
            str.Append(othercond);

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 瓶签药瓶信息
        /// </summary>
        /// <param name="LabelNo"></param>
        /// <returns></returns>
        public DataSet GetDrugInfor(string LabelNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select DrugCode,DrugName,Spec,Dosage,DosageUnit,DgNo from IVRecordDetail ivd ");
            str.Append("left join IVRecord iv on iv.IVRecordID=ivd.IVRecordID ");
            str.Append("where LabelNo='");
            str.Append(LabelNo);
            str.Append("' ");
            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;

        }

        /// <summary>
        /// 药品瓶支数量
        /// </summary>
        /// <param name="LabelNo"></param>
        /// <returns></returns>
        public DataSet GetDrugNum(string DemployeeID, string checkpro)
        {
            string checkdt = returnDT(checkpro);

            StringBuilder str = new StringBuilder();
            str.Append("select  distinct iv.labelno ,sum(DgNo) DGNO ");
            str.Append("    from IVRecordDetail ivd");
            str.Append("    left join IVRecord iv on ivd.IVRecordID=iv.IVRecordID");
            str.Append("    left join " + checkpro + " PY on iv.LabelNo=py.IVRecordID ");
            str.Append("    left join DEmployee D on DEmployeeID=PY.PCode");
            str.Append("    where DATEDIFF(DD," + checkdt + ",GETDATE())=0 and DemployeeID='" + DemployeeID + "'");
            str.Append("    group by iv.LabelNo");
            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;

        }

        /// <summary>
        /// 是否符合扫描条件
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="ivstatus"></param>
        /// <returns></returns>
        public int Iseffective(string labelno, int ivstatus)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  select ivstatus from IVRecord where LabelNo ='"+labelno+"' ");
            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                if (i < ivstatus && i >= 3)
                {
                    return ivstatus;
                }
                else
                {
                    return i;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 总瓶签信息
        /// </summary>
        /// <param name="labelnos"></param>
        /// <returns></returns>
        public DataSet GetQRCodeInfor(string labelnos)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    select distinct iv.LabelNo,iv.ivstatus,iv.PatName,iv.PatCode,iv.Batch,iv.Remark5,p.UsageName ");
            str.Append("    ,iv.WardName,d.WardCode,pa.BedNo,pa.Age,pa.AgeSTR ");
            str.Append("    from IVRecord iv ");
            str.Append("    left join Patient pa on iv.PatCode=pa.PatCode ");
            str.Append("    left join DWard d on d.WardCode=pa.WardCode ");
            str.Append("    left join Prescription p on p.PrescriptionID=iv.PrescriptionID ");
            str.Append("    left join IVRecord_Print ivrp on iv.LabelNo= ivrp.LabelNo ");
            str.Append("    where (ivrp.DrugQRCode='" + labelnos + "'or OrderQRCode='" + labelnos + "')");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 汇总信息
        /// </summary>
        /// <param name="demployeeid"></param>
        /// <param name="checkpro"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet GetAllInfor(string demployeeid, string checkpro, string date)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    select ivr.wardName 病区,COUNT(IVR.WardCode)总数,isnull(COUNT(IVR.WardCode)-isnull(count(py.IVRecordID),0),0) 未核对 ");
            str.Append("    ,isnull(count(py.IVRecordID),0)已核对");
            str.Append("    from IVRecord ivr   ");
            str.Append("    left join " + checkpro + " py on  IVR.LabelNo=py.IVrecordID and py.ScanCount='0'  ");
            str.Append("    where 1=1 and IVR.IVStatus >='3'    and DATEDIFF(DD,InfusionDT,'" + date + "')=0 ");
            str.Append("    and  IVR.WardRetreat='0' and IVR.LabelOver>=0    ");
            str.Append("    group by ivr.WardName,WardCode  order by ivr.WardCode");
 
            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public DataSet InforByDward(string checkpro,string date,string wardname)
        {
            StringBuilder str = new StringBuilder();
            str.Append("    select WardName,ivr.Batch ,ivr.LabelNo");
            str.Append("    from IVRecord ivr   ");
            str.Append("    left join "+checkpro+" py on  IVR.LabelNo=py.IVrecordID and py.ScanCount='0'   ");
            str.Append("    where 1=1 and IVR.IVStatus >='3'    and DATEDIFF(DD,InfusionDT,'"+date+"')=0 ");
            str.Append("    and  IVR.WardRetreat='0' and IVR.LabelOver>=0   ");
            str.Append("    and WardName='"+wardname+"'");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;

        }

        /// <summary>
        /// 单个瓶签的所有操作信息
        /// </summary>
        /// <param name="labelno"></param>
        /// <returns></returns>
        public DataSet SelectStepDetail(string labelno)
        {
            StringBuilder str = new StringBuilder ();
            str.Append("    select distinct iv.LabelNo,'审方' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    , replace(replace(replace(CONVERT(varchar, cp.CPDT, 120 ),'-',''),' ',''),':','') as DT,1 as ivstuts  from IVRecord iv ");
            str.Append("    left join CPRecord cp on iv.PrescriptionID=cp.PrescriptionID");
            str.Append("    left join DEmployee d on d.DEmployeeID=cp.CheckDCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            //str.Append("    union");
            //str.Append("    select distinct iv.LabelNo,'排批次' as step,d.DEmployeeName,yp.ChangeDT as DT,2 as ivstuts  from IVRecord iv ");
            //str.Append("    left join OrderChangeLog yp on iv.LabelNo=yp.LabelNo");
            //str.Append("    left join DEmployee d on d.DEmployeeID=yp.DEmployeeID");
            //str.Append("    where iv.LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'打印' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.PrintDT, 120 ),'-',''),' ',''),':','') as DT ,3 as ivstuts from IVRecord iv ");
            str.Append("    left join IVRecord_Print yp on iv.LabelNo=yp.LabelNo");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PrintCode");
            str.Append("    where iv.LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'溶媒' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.YPDT, 120 ),'-',''),' ',''),':','') as DT,4 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_YP_ZJG yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'溶剂' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.YSDT, 120 ),'-',''),' ',''),':','') as DT,4 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_YS_ZJG yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");
                
            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'排药' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.PYDT, 120 ),'-',''),' ',''),':','') as DT,5 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_PY yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'进仓' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.JCDT, 120 ),'-',''),' ',''),':','') as DT,7 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_JC yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'配置' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.PZDT, 120 ),'-',''),' ',''),':','') as DT,9 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_PZ yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'出仓' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.CCDT, 120 ),'-',''),' ',''),':','') as DT,11 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_CC yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'打包' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.DBDT, 120 ),'-',''),' ',''),':','') as DT,13 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_DB yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    union");
            str.Append("    select distinct iv.LabelNo,'签收' as step,d.DEmployeeName,d.DEmployeeID");
            str.Append("    ,replace(replace(replace(CONVERT(varchar, yp.QSDT, 120 ),'-',''),' ',''),':','') as DT,15 as ivstuts  from IVRecord iv ");
            str.Append("    left join IVRecord_QS yp on iv.LabelNo=yp.IVRecordID");
            str.Append("    left join DEmployee d on d.DEmployeeID=yp.PCode");
            str.Append("    where LabelNo ='" + labelno + "'");

            str.Append("    order by ivstuts");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;

        }

        /// <summary>
        /// 返回差错类型
        /// </summary>
        /// <param name="errorstep">差错步骤</param>
        /// <returns></returns>
        public DataSet SelectErrorRule(string errorstep)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select TypeCode,TypeName from ErrorRule where StatusName = '" + errorstep + "'");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        /// <summary>
        /// 插入错误记录
        /// </summary>
        /// <param name="labelno">瓶签号</param>
        /// <param name="errorstep">差错步骤</param>
        /// <param name="errorDid">差错人</param>
        /// <param name="findDid">发现人</param>
        /// <param name="errortime">差错时间</param>
        /// <param name="eType">差错类型</param>
        public void AddErrorRecord(string labelno,string errorcode,string errorDid,string findDid,string errortime,string typecode)
        {
            StringBuilder str = new StringBuilder();
            string Errorcode = ErrorCode(errorcode, typecode);
            str.Append("insert into errorrecord values ('"+Errorcode+"','" + labelno + "','EDA插入;FindStatus:0为EDA使用','" + errorcode + "','00','" + errorDid + "','" + findDid + "','" + errortime + "','" + DateTime.Now.ToString() + "','" + typecode + "')");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
        }

        /// <summary>
        /// 获得差错编号
        /// </summary>
        public string ErrorCode(string stautscode,string typecode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  select case LEN(statuscode) when 1 then '0'+convert(varchar,statuscode) else convert(varchar,statuscode) end as statuscode");
            str.Append(",case LEN(TypeCode) when 1 then '0'+convert(varchar,TypeCode) else convert(varchar,TypeCode) end as Typecode ");
            str.Append("from ErrorRule where StatusCode ='" + stautscode + "'and TypeCode='" + typecode + "'");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            string str1 = "  select isnull(MAX(substring (ErrorCode,14,6)),'000000') from ErrorRecord where DATEDIFF(DD,findtime,'" + DateTime.Now.Date.ToString() + "')=0";
            SqlDataAdapter sda1 = new SqlDataAdapter(str1.ToString(), conn);
            DataSet ds1 = new DataSet();
            sda1.Fill(ds1);
            string s = ds1.Tables[0].Rows[0][0].ToString();
            int num = Convert.ToInt32(s);
            num++;
            s = 'E' + DateTime.Now.Date.ToString("yyyyMMdd") + ds.Tables[0].Rows[0][0].ToString() + ds.Tables[0].Rows[0][1].ToString() + num.ToString().PadLeft(6, '0');
            return s;
        }


        public DataSet SelectDward()
        {
            StringBuilder str = new StringBuilder();
            str.Append("    select null as WardCode,'全部' as WardName");
            str.Append("    union");
            str.Append("    select distinct WardCode,WardName from DWard ");

            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }


        public int IsToday(string labelno)
        {
            string sql = "select case datediff(dd,infusiondt,getdate())when 0 then 1 else 0 end as Istoday from IVRecord where LabelNo ='" + labelno + "'";

            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int i = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                return i;//1为当天。0为否
                
            }
            else
            {
                return 0;//不存在瓶签
            }
        }


        private string returnDT(string checkpro)
        {
            string checkdt = string.Empty;
            switch (checkpro)
            {
                case "IVRecord_YS_ZJG": checkdt = "YSDT";
                    break;
                case "IVRecord_YP_ZJG": checkdt = "YPDT";
                    break;
                case "IVRecord_PY": checkdt = "PYDT";
                    break;
                case "IVRecord_JC": checkdt = "JCDT";
                    break;
                case "IVRecord_PZ": checkdt = "PZDT";
                    break;
                case "IVRecord_CC": checkdt = "CCDT";
                    break;
                case "IVRecord_DB": checkdt = "DBDT";
                    break;
                case "IVRecord_QS": checkdt = "QSDT";
                    break;
            }
            return checkdt;
        }


        /// <summary>
        /// 扫描是否是第三方瓶签，返回对应labelNo
        /// </summary>
        /// <param name="HisLabelNo"></param>
        public string CheckIsHisLabel(string HisLabelNo)
        {
            string labelno = "";
            //DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("Select labelNo from IVRecord where Remark6 = '" + HisLabelNo + "'");
            SqlDataAdapter sda = new SqlDataAdapter(str.ToString(), conn);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                labelno = ds.Tables[0].Rows[0][0].ToString();
            }
            return labelno;
        }

    }
}