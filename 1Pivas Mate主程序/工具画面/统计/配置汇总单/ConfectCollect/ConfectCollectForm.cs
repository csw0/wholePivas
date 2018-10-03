using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FastReport;
using System.Runtime.InteropServices;
using FastReport.Data;
using System.IO;
using PIVAsCommon.Helper;

namespace PackCollect
{
    public partial class ConfectCollectForm : Form
    {
        public ConfectCollectForm()
        {
            InitializeComponent();
        }

        DB_Help db = new DB_Help();
        Report report;
        DataTable dtward = new DataTable();
        DataTable ward = new DataTable();
        string startTime ="";
        string endTime = "";
        string isshowHS = "1"; //是否显示毫秒 1显示，0不显示

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count == 0) { MessageBox.Show("该时间段没有可统计数据!"); return; }
            if (getCheckedVulesToWardName() == "") { MessageBox.Show("请选择统计病区!"); return; }
            button1.Enabled = false;
            string PTY = "0";
            string KSS = "0";
            string HLY = "0";
            string YYY = "0";
            string KL="0";
            DataTable dt1 = new DataTable();
            DataTable dtdrug = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();

            DataTable dt5 = new DataTable();//病区主表
            dt5.Columns.Add("WardName");

            DataTable dt6 = new DataTable();//医嘱明细


            if (checkBox1.Checked)//选择汇总
            {
                try
                {
                    DataSet ds = GetPackCollect();
                    if (ds != null)
                        dt1 = ds.Tables[0];
                    else
                    {
                        dt1.Columns.Add("WardName");
                        dt1.Columns.Add("Batch");
                        dt1.Columns.Add("AllCount");
                        dt1.Columns.Add("ConfectCount");
                        dt1.Columns.Add("EmptyCount");
                        dt1.Columns.Add("IsCommandCount");
                        //dt1.Columns.Add("DBPrintWardCode");
                        MessageBox.Show("数据量太多，请合理选择时间段和病区数!");
                        return;
                    }
                    // MessageBox.Show( dt1.Columns[0].ToString());

                    //DataRow dr0 = null;//如果汇总表查出来没有值，每个病区表的字段填充0;
                    //string s = getCheckedVulesToWardName();
                    //string[] s1 = s.Split(",".ToCharArray());
                    //foreach (string s2 in s1)
                    //{
                    //    DataRow[] dr00 = dt1.Select(" WardName = '" + s2 + "'");
                    //    if (dr00.Length <= 0)
                    //    {
                    //        foreach (Control ctr in panel2.Controls)
                    //        {
                    //            if (ctr is CheckBox)
                    //            {
                    //                CheckBox cb = ctr as CheckBox;
                    //                if (cb.Checked)
                    //                {
                    //                    dr0 = dt1.NewRow();
                    //                    switch (cb.Text)
                    //                    {
                    //                        case "1#": dr0["Batch"] = "第1批"; break;
                    //                        case "2#": dr0["Batch"] = "第2批"; break;
                    //                        case "3#": dr0["Batch"] = "第3批"; break;
                    //                    }
                    //                    ;
                    //                    //dr0["Batch"]=cb.Text;

                    //                    dr0["WardName"] = s2;
                    //                    dr0["AllCount"] = "0";
                    //                    dr0["ConfectCount"] = "0";
                    //                    dr0["EmptyCount"] = "0";
                    //                    dr0["IsCommandCount"] = "0";
                    //                    dr0["AheadPackCount"] = "0";
                    //                    dt1.Rows.Add(dr0);
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {
                dt1.Columns.Add("WardName");
                dt1.Columns.Add("Batch");
                dt1.Columns.Add("AllCount");
                dt1.Columns.Add("ConfectCount");
                dt1.Columns.Add("EmptyCount");
                dt1.Columns.Add("IsCommandCount");
                //dt1.Columns.Add("AheadPackCount");

            }
            if (checkBox2.Checked)//选择转床
            {
                string strCollected = getCheckedVulesToWardCode();
                dt2 = GetBed(strCollected);
            }
            else
            {
                dt2.Columns.Add("PatName");
                dt2.Columns.Add("OldBed");
                dt2.Columns.Add("NewBed");
                dt2.Columns.Add("ChangeDate");
            }
            if (checkBox3.Checked)//选择统药
            {
                dt3 = GetPackCollectDrug();

            }
            else
            {
                dt3.Columns.Add("HuoJiaNo");
                dt3.Columns.Add("DrugName");
                dt3.Columns.Add("Spec");
                dt3.Columns.Add("FormUnit");
                dt3.Columns.Add("Count");
            }
            if (checkBox4.Checked)//选择提前打包
            {
                dt4 = GetAdvancePackCollectDrug(getCheckedVulesToWardCode(),startTime, endTime);
            }
            else
            {
                dt4.Columns.Add("HuoJiaNo");
                dt4.Columns.Add("DrugName");
                dt4.Columns.Add("Spec");
                dt4.Columns.Add("FormUnit");
                dt4.Columns.Add("Count");
            }
            if (checkBox6.Checked)
            {
                DataSet dsPre = GetPreDetail(getCheckedVulesToWardCode(), startTime,endTime);//医嘱明细
                dt6 = dsPre.Tables[0];
                //printer(dt6);
            }
            else
            {

                dt6.Columns.Add("BedNo");
                dt6.Columns.Add("GroupNo");
                dt6.Columns.Add("PatName");
                dt6.Columns.Add("DrugName");
                dt6.Columns.Add("Spec");
                dt6.Columns.Add("Quantity");
            }
            try
            {
                //主从报表参考文章http://www.csframework.com/archive/1/arc-1-20110612-1535.htm
                //创建WardName主表
                string s = getCheckedVulesToWardName();
                string[] s1 = s.Split(",".ToCharArray());
                ward.Columns.Clear();
                ward.Rows.Clear();
                ward.Columns.Add("WardName");
                ward.Columns.Add("PTY");
                ward.Columns.Add("KSS");
                ward.Columns.Add("HLY");
                ward.Columns.Add("YYY");
                ward.Columns.Add("DBCode");
                DataRow drow;
                //foreach (string s2 in s1)
                 for (int i=0;i<s1.Length ;i++)
                {
                    drow = ward.NewRow();
                    DataTable drugType = new DataTable();
                    string WardCode = getWardCodeByWardName(s1[i]);
                     string DBCode="";
                     //if (radioButton1.Checked)
                     //{
                     DBCode = getDBCode(i);
                     //}
                     //else
                     //{
                     //    DBCode = getDBCode(WardCode);
                     //}
                    drugType = GetDrugtype(WardCode);
                    DataRow[] dr = drugType.Select(" drugtype='1'");//普通药
                    if (dr.Length > 0)
                        PTY = dr[0]["count"].ToString();
                    else
                        PTY = "0";
                    dr = drugType.Select(" drugtype='2' ");//抗生素
                    if (dr.Length > 0)
                        KSS = dr[0]["count"].ToString();
                    else
                        KSS = "0";
                    dr = drugType.Select(" drugtype='3' ");//化疗药
                    if (dr.Length > 0)
                        HLY = dr[0]["count"].ToString();
                    else
                        HLY = "0";
                    dr = drugType.Select(" drugtype='4' ");//营养药
                    if (dr.Length > 0)
                        YYY = dr[0]["count"].ToString();
                    else
                        YYY = "0";
                   // PTY = (int.Parse(PTY) + int.Parse(YYY)).ToString();

                    drow["WardName"] = s1[i];
                    drow["PTY"] = PTY;
                    drow["KSS"] = KSS;
                    drow["HLY"] = HLY;
                    drow["YYY"] = YYY;
                    drow["DBCode"] = DBCode; 
                    ward.Rows.Add(drow);
                    
                }
                 if (!checkBox12.Checked)
                 {
                     foreach (DataRow dr in dt1.Rows)
                     {
                         dr["IsCommandCount"] = "0";
                     }

                 }
                 if (!checkBox13.Checked)
                 {
                     foreach (DataRow dr in dt1.Rows)
                     {
                         dr["EmptyCount"] = "0";
                     }

                 }
                 if (!checkBox14.Checked)
                 {
                     foreach (DataRow dr in dt1.Rows)
                     {
                         dr["ConfectCount"] = "0";
                     }
                 }

                 foreach (DataRow dr in dt1.Rows)
                 {
                     dr["AllCount"] = Convert.ToInt32(dr["IsCommandCount"]) + Convert.ToInt32(dr["EmptyCount"]) + Convert.ToInt32(dr["ConfectCount"]);
                 }

                report = new Report();
                report.Preview = previewControl1;
                report.Load(".\\Crystal\\ConfectCollect.frx");
                //report.GetParameter("Title").Value = s2;
                report.GetParameter("date").Value = startTime + "至" + endTime;
                report.GetParameter("PTY").Value = PTY;
                report.GetParameter("KSS").Value = KSS;
                report.GetParameter("HLY").Value = HLY;
                report.GetParameter("YYY").Value = YYY;
                

                report.RegisterData(ward, "wd");
                //MessageBox.Show(ward.Rows.Count.ToString());
                //((report.FindObject("Data5")) as FastReport.DataBand).DataSource = report.GetDataSource("DT");

                report.RegisterData(dt1, "DT");
                //MessageBox.Show(dt1.Rows.Count.ToString());
                report.RegisterData(dt3, "dt3");

                //给DataBand(主表数据)绑定数据源
                DataBand masterBand = report.FindObject("Data1") as DataBand;
                masterBand.DataSource = report.GetDataSource("wd"); //主表

                //给DataBand(明细数据)绑定数据源
                DataBand detailBand = report.FindObject("Data5") as DataBand;
                detailBand.DataSource = report.GetDataSource("DT"); //明细表 


                //report.RegisterData(dt3, "dt3");
                //((report.FindObject("Data7")) as FastReport.DataBand).DataSource = report.GetDataSource("dt3");



                //重要！！给明细表设置主外键关系！
                detailBand.Relation = new Relation();
                detailBand.Relation.ParentColumns = new string[] { "WardName" };
                detailBand.Relation.ParentDataSource = report.GetDataSource("wd"); //主表
                detailBand.Relation.ChildColumns = new string[] { "WardName" };
                detailBand.Relation.ChildDataSource = report.GetDataSource("DT"); //明细表 


                //给DataBand(主表数据)绑定数据源
                DataBand masterBandD = report.FindObject("Data3") as DataBand;
                masterBandD.DataSource = report.GetDataSource("wd"); //主表

                //给DataBand(明细数据)绑定数据源
                DataBand detailBandD = report.FindObject("Data7") as DataBand;
                detailBandD.DataSource = report.GetDataSource("dt3"); //明细表 

                detailBandD.Relation = new Relation();
                detailBandD.Relation.ParentColumns = new string[] { "WardName" };
                detailBandD.Relation.ParentDataSource = report.GetDataSource("wd"); //主表
                detailBandD.Relation.ChildColumns = new string[] { "WardName" };
                detailBandD.Relation.ChildDataSource = report.GetDataSource("dt3"); //明细表 



                report.RegisterData(dt6, "preTitle");
                //report.RegisterData(dt7, "preDel");

                DataBand preBand = report.FindObject("Data6") as DataBand;
                preBand.DataSource = report.GetDataSource("preTitle"); //医嘱明细主表

                //DataBand preDelBand = report.FindObject("Data7") as DataBand;
                //preDelBand.DataSource = report.GetDataSource("preDel"); //明细表 



                //preDelBand.Relation = new Relation();
                //preDelBand.Relation.ParentColumns = new string[] { "PatName" };
                //preDelBand.Relation.ParentDataSource = report.GetDataSource("preTitle"); //主表
                //preDelBand.Relation.ChildColumns = new string[] { "PatName" };
                //preDelBand.Relation.ChildDataSource = report.GetDataSource("preDel"); //明细表 


                report.RegisterData(dt2, "dt2");
                ((report.FindObject("Data2")) as FastReport.DataBand).DataSource = report.GetDataSource("dt2");

                report.RegisterData(dt4, "dt4");
                ((report.FindObject("Data4")) as FastReport.DataBand).DataSource = report.GetDataSource("dt4");
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            button1.Enabled = true;
            button3.Enabled = true;
        }
        /// <summary>
        /// 打印出log
        /// </summary>
        /// <param name="dt"></param>
        private void printerLog(DataTable dt)
        {
            string[,] arr = new string[dt.Rows.Count, dt.Columns.Count];


            string fileName = "Log.txt";

            StreamWriter sw = new StreamWriter(fileName, true);

            sw.Flush();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string wr = string.Empty;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[i, j] = dt.Rows[i][j].ToString();
                    wr += arr[i, j] + " ";
                }
                sw.WriteLine(wr);

            }
            sw.Close();
            sw.Dispose();

        }
        /// <summary>
        /// 得到统药单的二维码
        /// </summary>
        /// <returns></returns>
        private string getDBCode(int i)
        {

            string DBCode = string.Empty;
            Random rad = new Random();//实例化随机数产生器rad；
            int value = rad.Next(10, 100);
            string dt = DateTime.Now.ToString("yy-MM-dd HH:mm:ss");

            dt = dt.Replace("-", "").Replace(" ", "").Replace(":", "");
            //dt = dt.Substring(4, 8);//月份+日期+小时+分钟
            //wardCode = wardCode.Substring(wardCode.Length-5, 4);
            DBCode = dt + (i+1).ToString ().PadLeft(3,'0');//二维码组成：当前时间的后12位+2位随机数,共14位
            return DBCode;

        }

        private string getDBCode(string wardcode)
        {
            string sql = "  select count (distinct labelno) ,wardcode ,PZPrintwardcode from [IVRecord_PZPrint] group by WardCode,PZPrintwardcode";
            return db.GetPIVAsDB(sql).Tables[0].Rows[0]["PZPrintwardcode"].ToString();

        }

        /// <summary>
        /// 得到床号
        /// </summary>
        /// <param name="Dward"></param>
        /// <returns></returns>
        private DataTable GetBed(string Dward)
        {
            StringBuilder str = new StringBuilder();
            DataTable dt = new DataTable();
            DateTime a = Convert.ToDateTime(endTime);
            DateTime b = Convert.ToDateTime(startTime);
            //string yestdaytime = DateTime.Now.AddDays(-1).ToShortDateString() + " 06:00:00";

            str.Append(" select distinct patient.PatName  ,ivrecord.BedNo as OldBed,Patient.BedNo as NewBed ,GETDATE() as ChangeDate");
            str.Append(" ,IVRecord.WardCode as OldWardCode,Patient.WardCode as NewWardCode,IVRecord.WardName");
            str.Append(" from IVRecord,Patient where IVRecord.PatCode=Patient.PatCode  and (ivrecord.BedNo<>Patient.BedNo ");
            str.Append(" or IVRecord.WardCode<>Patient.WardCode) ");
            //str.Append(" and  DATEDIFF(MI,PrintDT,GETDATE())>0 and DATEDIFF(MI,PrintDT,'" + yestdaytime + "')<0");
            str.Append(" and InfusionDT between '" + b + "' and '" + a + "' ");
            str.Append(" and Patient.WardCode in (" + Dward + ")");
            dt = db.GetPIVAsDB(str.ToString()).Tables[0];

            return dt;
        }
        /// <summary>
        /// 得到医嘱明细
        /// </summary>
        /// <returns></returns>
        private DataSet GetPreDetail(string wardCode, string beginDT, string endDT)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" select distinct p.BedNo,pa.PatName,p.GroupNo,u.DrugName,u.Spec,u.Quantity from Prescription p ");
            str.Append(" left join UseDrugList u on p.GroupNo=u.GroupNo");
            str.Append(" left join Patient pa on p.PatientCode=pa.PatCode");
            str.Append("   left join IVRecord iv on p.GroupNo=iv.GroupNo ");
            str.Append(" where p.WardCode in (" + wardCode + ") and iv.InfusionDT between '" + beginDT + "' and '" + endDT + "' ");
            str.Append(" and u.DrugName is not null order by p.GroupNo asc");
            DataSet ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
        //汇总
        private DataSet GetPackCollect()
        {
            DataSet ds = new DataSet();
            DateTime a = Convert.ToDateTime(endTime);
            DateTime b = Convert.ToDateTime(startTime);
            
            if (a < b)
            {
                MessageBox.Show("时间选择错误，最大时间段不能小于最小时间段！");
            }
            else
            {
                StringBuilder str = new StringBuilder();

                string pubstr1 = SelectRule(startTime,endTime, getCheckedVulesToWardCode());//获取界面上的时间和病区
                string pubstr2 = SelectRule1(startTime, endTime, getCheckedVulesToWardCode());
                if (radioButton2.Checked)//已打印
                {
                    //string pubstr2="select left(batch,1) as batch,COUNT(1)as count from IVRecord,IVRecord_DB where IVRecord.LabelNo=IVRecord_DB.IVRecordID and  ScanCount=0 and Invalid is null ";
                    ////str.Append(" select a.WardName,a.Batch,a.AllCount,isnull(b.ConfectCount,0)ConfectCount,isnull(c.EmptyCount,0)EmptyCount,isnull(d.IsCommandCount,0)IsCommandCount  from ");
                    ////str.Append(" (select  WardName,WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,COUNT(distinct LabelNo) as AllCount from IVRecord where IVRecord.LabelNo in (select LabelNo from IVRecord_DBPrint)  " + pubstr2 + " group by WardCode,WardName,TeamNumber )a ");
                    ////str.Append(" left join  ");
                    ////str.Append(" (select WardName,IVRecord.WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,count(distinct LabelNo) as ConfectCount from IVRecord where IVRecord.LabelNo in (select LabelNo from IVRecord_DBPrint) " + pubstr2 + " and  Batch like '%#%' and Batch not like '%l%' group by IVRecord.WardCode,WardName,TeamNumber)b ");
                    ////str.Append(" on a.WardCode=b.WardCode and a.Batch=b.Batch ");
                    ////str.Append(" left join ");
                    ////str.Append(" (select WardName,WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,count(distinct LabelNo)as EmptyCount from IVRecord where IVRecord.LabelNo in (select LabelNo from IVRecord_DBPrint) " + pubstr2 + " and Batch like '%K%' and Batch not like '%l%'   group by WardCode,WardName,TeamNumber)c");
                    ////str.Append(" on a.WardCode=c.WardCode and a.Batch=c.Batch ");
                    ////str.Append(" left join ");
                    ////str.Append(" (select WardName,WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,count(distinct LabelNo)as IsCommandCount from IVRecord where IVRecord.LabelNo in (select LabelNo from IVRecord_DBPrint) " + pubstr2 + " and Batch like '%L%' group by WardCode,WardName,TeamNumber)d ");
                    ////str.Append(" on a.WardCode=d.WardCode and a.Batch=d.Batch");

                    //str.Append(" left join (select '第'+convert(varchar,TeamNumber)+'批' as Batch,count(LabelNo)as Advancecount from IVRecord,IVRecord_DB where IVRecord.LabelNo=IVRecord_DB.IVRecordID " + pubstr1 + " group by TeamNumber)e on a.batch=e.batch");
                    //str.Append(" order by a.WardCode");
                    /*
                    str.Append(" select IVRecord.WardName,p.drugtype, COUNT(*)as count from IVRecord");
                    str.Append(" inner join IVRecord_DB on IVRecord.LabelNo=IVRecord_DB.IVRecordID ");
                    str.Append(" inner join Prescription p on IVRecord.PrescriptionID=p.PrescriptionID ");
                    str.Append(" where 1=1 " + pubstr1 + " ");
                    str.Append(" group by p.DrugType order by p.DrugType ");
                     * */

                    str.Append("   select wardname ,'第'+convert(varchar,TeamNumber)+'批' as Batch,COUNT (distinct IVRecord.LabelNo)as AllCount");
                    str.Append("   ,COUNT(case when Batch like '%#%' and Batch not like '%l%' then 1 end ) as ConfectCount");
                    str.Append("   ,count (case when Batch like '%K%' and Batch not like '%l%' then 1 end )as EmptyCount");
                    str.Append("    ,count (case when Batch like '%L%' then 1  end ) as IsCommandCount");
                    str.Append("   from IVRecord ");
                    str.Append("    inner join IVRecord_PZPrint dbp on IVRecord.LabelNo =dbp.LabelNo");
                    str.Append("   where 1=1" + pubstr2 + " and ScanCount='0'");
                    //--labelno in (select LabelNo from IVRecord_DBPrint  ) and 
                    //str.Append("ivstatus>=13 and WardRetreat='0' and LabelOver=0  ");
                    //str.Append("   and WardCode in('B31N01')  ");
                    //and (InsertDT between '2015-01-01 00:00' and '2015-03-05 14:25')  
                    //and (1=2 or Batch like '%1%' ) 
                    //--and DBPrintWardCode is not null
                    str.Append("   group by WardName,TeamNumber");
                }
                else if (radioButton1.Checked)//未打印
                {
                    str.Append(" select a.WardName,a.Batch,a.AllCount,isnull(b.ConfectCount,0)ConfectCount,isnull(c.EmptyCount,0)EmptyCount,isnull(d.IsCommandCount,0)IsCommandCount  from ");
                    str.Append(" (select  WardName,WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,COUNT(distinct LabelNo) as AllCount from IVRecord where IVRecord.LabelNo in (select IVRecordID from IVRecord_DB except select labelno from IVRecord_PZPrint)  " + pubstr2 + " group by WardCode,WardName,TeamNumber )a ");
                    str.Append(" left join  ");
                    str.Append(" (select WardName,IVRecord.WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,count(distinct LabelNo) as ConfectCount from IVRecord where IVRecord.LabelNo in (select IVRecordID from IVRecord_DB except select labelno from IVRecord_PZPrint) " + pubstr2 + " and  Batch like '%#%' and Batch not like '%l%' group by IVRecord.WardCode,WardName,TeamNumber)b ");
                    str.Append(" on a.WardCode=b.WardCode and a.Batch=b.Batch ");
                    str.Append(" left join ");
                    str.Append(" (select WardName,WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,count(distinct LabelNo)as EmptyCount from IVRecord where IVRecord.LabelNo in (select IVRecordID from IVRecord_DB except select labelno from IVRecord_PZPrint) " + pubstr2 + " and Batch like '%K%' and Batch not like '%l%'   group by WardCode,WardName,TeamNumber)c");
                    str.Append(" on a.WardCode=c.WardCode and a.Batch=c.Batch ");
                    str.Append(" left join ");
                    str.Append(" (select WardName,WardCode,'第'+convert(varchar,TeamNumber)+'批' as Batch,count(distinct LabelNo)as IsCommandCount from IVRecord where IVRecord.LabelNo in (select IVRecordID from IVRecord_DB except select labelno from IVRecord_PZPrint) " + pubstr2 + " and Batch like '%L%' group by WardCode,WardName,TeamNumber)d ");
                    str.Append(" on a.WardCode=d.WardCode and a.Batch=d.Batch");
                }
                else //全部
                {
                    str.Append("   select wardname ,'第'+convert(varchar,TeamNumber)+'批' as Batch,COUNT (distinct IVRecord.LabelNo)as AllCount");
                    str.Append("   ,COUNT(case when Batch like '%#%' and Batch not like '%l%' then 1 end ) as ConfectCount");
                    str.Append("   ,count (case when Batch like '%K%' and Batch not like '%l%' then 1 end )as EmptyCount");
                    str.Append("    ,count (case when Batch like '%L%' then 1  end ) as IsCommandCount");
                    str.Append("   from IVRecord ");
                    str.Append("    inner join IVRecord_PZ db on IVRecord.LabelNo =db.IVRecordID");
                    str.Append("   where 1=1" + pubstr2 + " and ScanCount='0'");
                    str.Append("   group by WardName,TeamNumber");
                }
                ds = db.GetPIVAsDB(str.ToString());

            }
            return ds;
        }
        //药品类型统计
        private DataTable GetDrugtype(string WardCode)
        {

            StringBuilder str = new StringBuilder();
            DataSet ds = new DataSet();
            string pubstr1 = SelectRule(startTime, endTime, WardCode);//获取界面上的时间和病区
            str.Append(" select p.drugtype, COUNT(*)as count from IVRecord");
            str.Append(" inner join IVRecord_PZ on IVRecord.LabelNo=IVRecord_PZ.IVRecordID ");
            str.Append(" inner join Prescription p on IVRecord.PrescriptionID=p.PrescriptionID ");
            str.Append(" where 1=1 " + pubstr1 + " ");
            str.Append(" group by p.DrugType order by p.DrugType ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds.Tables[0];
        }
        //提前打包
        private DataTable GetAdvancePackCollectDrug(string wardcode,string beginDT,string endDT)
        {
            StringBuilder str = new StringBuilder();
            string rule = SelectRule1(startTime, endTime, getCheckedVulesToWardCode());
            DataTable dt = new DataTable();
            str.Append(" select DDrug.PortNo as HuoJiaNo,ivd.DrugName ,ivd.spec,DDrug.FormUnit,sum(DgNo)as count from IVRecord  ");
            str.Append(" left join IVRecordDetail ivd on IVRecord.IVRecordID=ivd.IVRecordID ");
            str.Append(" inner join DDrug on ivd.DrugCode=DDrug.DrugCode");
            str.Append(" where IVRecord.PackAdvance=1   ");
            str.Append(" and IVRecord.InfusionDT between '" + beginDT + "' and '" + endDT + "'");
            str.Append(" and WardCode in(" + wardcode + ") ");
            str.Append(rule);
            str.Append(" group by ivd.drugcode,ivd.DrugName,ivd.Spec ,DDrug.FormUnit,DDrug.PortNo  order by ivd.drugcode  ");
            dt = db.GetPIVAsDB(str.ToString()).Tables[0];
            return dt;
        }
        //统药
        private DataTable GetPackCollectDrug()
        {
            StringBuilder str = new StringBuilder();
            string rule = SelectRule(startTime, endTime, getCheckedVulesToWardCode());
            DataTable dt = new DataTable();
            str.Append(" select dw.WardName,dw.WardCode,DDrug.PortNo as HuoJiaNo,ivd.DrugName ,ivd.spec,DDrug.FormUnit,sum(DgNo)as count from IVRecord  ");
            str.Append(" left join IVRecordDetail ivd on IVRecord.IVRecordID=ivd.IVRecordID ");
            str.Append(" inner join DDrug on ivd.DrugCode=DDrug.DrugCode ");
            str.Append(" inner join DWard dw on dw.WardCode= IVRecord.WardCode");
            str.Append(" where 1=1   ");
            //str.Append(" and DATEDIFF(DD,InfusionDT,'" + datepick.Text + "')=0");
            //str.Append(" and WardCode='" + wardcode + "'");
            str.Append(rule);
            str.Append(" group by dw.WardName,dw.WardCode,ivd.drugcode,ivd.DrugName,ivd.Spec ,DDrug.FormUnit,DDrug.PortNo  order by ivd.drugcode ");
            dt = db.GetPIVAsDB(str.ToString()).Tables[0];
            return dt;
        }


        private void PackCollect_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(db.IniReadValuePivas("TimeShowType", "IsshowHS")))
            {
                db.IniWriteValuePivas("TimeShowType", "IsshowHS", "1");
                isshowHS = "1";
            }
            else
            {
                isshowHS = db.IniReadValuePivas("TimeShowType", "IsshowHS");

            }
            datetimeType();
         
            bddvg();
            bdbatch();
            dbcob();
           
            //foreach (Control ctr in panel2.Controls)
            //{
            //    if (ctr is CheckBox)
            //    {
            //        CheckBox cb = ctr as CheckBox;
            //        cb.Checked = true;
            //    }
            //}
            //if (checkedListBox1.Items.Count != 0) {
            //    this.button1_Click(null, null);
            //}
            

        }
        private void addDBCode()
        {
            //从DataTable中拿到病区和条码
            string time = DateTime.Now.ToString("yy-MM-dd hh:mm:ss");
            time = time.Replace("-", "").Replace(" ", "").Replace(":", "");
            string pubstr2 = SelectRule1(startTime, endTime, getCheckedVulesToWardCode());
            string printcode = time + (DateTime.Now.Year - DateTime.Now.DayOfYear - DateTime.Now.Minute).ToString();
            if (radioButton1.Checked)//未打印
            {
                for (int i = 0; i < ward.Rows.Count; i++)
                {
                    DataRow r = ward.Rows[i];
                    string wardName = r["WardName"].ToString();
                    string sql = "select distinct db.IVRecordID from IVRecord_PZ db left join IVRecord   on db.IVRecordID=IVRecord.LabelNo where 1=1 " + SelectRule1(startTime, endTime, getWardCodeByWardName(wardName)) + " ";
                    //sql += "    where DATEDIFF(DD,db.DBDT,'{0}')=0 and IVRecord.WardName ='{1}' " + pubstr2 + "";
                    sql += "  except select dbp.labelno from IVRecord_PZPrint dbp";
                   // sql = string.Format(sql, datepick.Value, wardName);
                    DataSet ds = db.GetPIVAsDB(sql);
                    string lableNo = string.Empty;
                    string DBCode = r["DBCode"].ToString();
                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        lableNo = ds.Tables[0].Rows[row]["IVRecordID"].ToString();
                        //string sqlUp = "update IVRecord_DB set DBCode={0} where IVRecordID ={1}";
                        //sqlUp = string.Format(sqlUp, DBCode, lableNo);
                        string sqlIn = "insert into IVRecord_PZPrint values ('" + lableNo + "'," + getWardCodeByWardName(wardName) + ",'" + DateTime.Now.ToString() + "','','0','" + printcode + "','" + DBCode + "','')";
                        db.SetPIVAsDB(sqlIn);
                    }
                }
            }
            else if (radioButton2.Checked)//已打印
            {
                for (int i = 0; i < ward.Rows.Count; i++)
                {
                    DataRow r = ward.Rows[i];
                    string wardName = r["WardName"].ToString();
                    string sql = "select distinct dbp.LabelNo from IVRecord_PZPrint dbp left join IVRecord   on dbp.LabelNo=IVRecord.LabelNo where 1=1 " + SelectRule1(startTime, endTime, getWardCodeByWardName(wardName)) + " ";
                    //sql += "";
                    //sql = string.Format(sql, datepick.Value, getWardCodeByWardName(wardName));
                    DataSet ds = db.GetPIVAsDB(sql);
                    string lableNo = string.Empty;
                    string DBCode = r["DBCode"].ToString();

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        lableNo = ds.Tables[0].Rows[row]["LabelNo"].ToString();
                        string str = "select Max(ScanCount) from [IVRecord_PZPrint] where LabelNo='" + lableNo + "'";
                        int n = Convert.ToInt32(db.GetPIVAsDB(str).Tables[0].Rows[0][0].ToString()) + 1;
                        string sqlIn = "insert into IVRecord_PZPrint values ('" + lableNo + "'," + getWardCodeByWardName(wardName) + ",'" + DateTime.Now.ToString() + "','','" + n.ToString() + "','" + printcode + "','" + DBCode + "','')";
                        db.SetPIVAsDB(sqlIn);
                    }
                }
            }
            else //全部
            {
                for (int i = 0; i < ward.Rows.Count; i++)
                {
                    DataRow r = ward.Rows[i];
                    string wardName = r["WardName"].ToString();
                    string sql = "select distinct db.IVRecordID from IVRecord_PZ db left join IVRecord  on db.IVRecordID=IVRecord.LabelNo where 1=1 " + SelectRule1(startTime, endTime, getWardCodeByWardName(wardName)) + " ";
                    //sql += "    where " + pubstr2 + "";                         //DATEDIFF(DD,db.DBDT,'{0}')=0 and IVRecord.WardName ='{1}'
                   // sql += "  except select dbp.labelno from IVRecord_DBPrint dbp";
                    sql = string.Format(sql, startTime, wardName);
                    DataSet ds = db.GetPIVAsDB(sql);
                    string lableNo = string.Empty;
                    string DBCode = r["DBCode"].ToString();
                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        lableNo = ds.Tables[0].Rows[row]["IVRecordID"].ToString();
                        //string sqlUp = "update IVRecord_DB set DBCode={0} where IVRecordID ={1}";
                        //sqlUp = string.Format(sqlUp, DBCode, lableNo);
                        string sqlIn = "insert into IVRecord_PZPrint values ('" + lableNo + "'," + getWardCodeByWardName(wardName) + ",'" + DateTime.Now.ToString() + "','','0','" + printcode + "','" + DBCode + "','')";
                        db.SetPIVAsDB(sqlIn);
                    }
                }
            }


            

        }
        /// <summary>
        /// 加载病区
        /// 楼层
        /// </summary>
        private void dbcob()
        {
            string sql = "select  distinct WardArea from dward where WardArea!=''";
            DataTable dt = db.GetPIVAsDB(sql).Tables[0];
            DataRow dr = dt.NewRow();
            dr["WardArea"] = "<全部病区组>";
            dt.Rows.InsertAt(dr, 0);
            if (dt.Rows.Count <= 0)
                return;
            cbarea.DataSource = dt;
            cbarea.DisplayMember = "WardArea";
            cbarea.ValueMember = "WardArea";
            cbarea.Text = "<全部病区组>";

        }

        /// <summary>
        /// 加载病区
        /// 科室
        /// </summary>
        private void bddvg()
        {
            try
            {
                checkedListBox1.Items.Clear();
                //string sql = "select distinct dw.WardName ,dw.WardCode,dw.SpellCode,dw.WardArea,dw.WardSeqNo from (select WardCode from  IVRecord_DB db left join IVRecord iv on db.IVRecordID=iv.LabelNo) a left join DWard dw on dw.WardCode=a.WardCode ";
                //string sql = "select distinct WardName,WardCode,SpellCode,WardArea,WardSeqNo from DWard";
                string sql = "select distinct dw.WardName,dw.WardCode,dw.SpellCode,dw.WardArea,dw.WardSeqNo ";
                sql += "from  DWard dw inner join (select distinct WardCode from IVRecord  where InfusionDT  between '{0}'and '{1}') a on dw.WardCode=a.WardCode ";
                sql = string.Format(sql, startTime, endTime);
                DataTable dt = db.GetPIVAsDB(sql.ToString()).Tables[0];
                if (dt.Rows.Count < 0)
                    return;
                //dataGridView1.DataSource = dt;

                //checkedListBox1.Items.Add();
                if (dt.Rows.Count > 0)
                {
                    //checkedListBox1.Items.Add("全选");
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        checkedListBox1.Items.Add(dataRow[0]); //= dataRow[1].ToString()
                       
                    }

                }

                //dataGridView1.Columns[1].Visible = dataGridView1.Columns[2].Visible = dataGridView1.Columns[3].Visible = dataGridView1.Columns[4].Visible = false;
                // dataGridView1.Columns[0].Width = 180;
                dtward = dt;
                checkBox5.Checked = true;
                checkedListBox1.SelectedValueChanged += new EventHandler(checkBox1_CheckedChanged);
                checkBox5_CheckedChanged(null,null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 加载批次
        /// 界面设置批次显示
        /// </summary>
        private void bdbatch()
        {
            panel2.Controls.Clear();
            string SqlCob1 = "select distinct OrderID from DOrder  where IsValid='1'";
            DataTable DtCob1 = db.GetPIVAsDB(SqlCob1).Tables[0];
            if (DtCob1.Rows.Count < 0)
                return;
            for (int i = 0; i < DtCob1.Rows.Count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Checked = false;
                cb.Width = 50;
                cb.Left = i * 50;
                cb.Text = DtCob1.Rows[i]["OrderID"].ToString() + "批";
                cb.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
                panel2.Controls.Add(cb);
            }
        }

        //拼接字符串函数
        private string SelectRule(string Date, string date1, string wardcode)
        {
            StringBuilder strrule = new StringBuilder();
            strrule.Append(" and ivstatus>=13 and WardRetreat='0' and LabelOver=0 ");
            if (wardcode != ""  && wardcode != null)
                strrule.Append(" and IVRecord.WardCode in (" + wardcode + ") ");

          

                strrule.Append(" and (InfusionDT between '" + Date + "' and '" + date1 + "') ");

                strrule.Append(" and (1=2");
            




            foreach (Control ctr in panel2.Controls)
            {
                if (ctr is CheckBox)
                {
                    CheckBox cb = ctr as CheckBox;
                    if (cb.Checked)
                    {
                        strrule.Append(" or Batch like '%" + cb.Text.Replace("批", "").Trim() + "%'");
                    }
                }
            }
            //strrule.Append(" ) ");
            strrule.Append(" ) and (1=2");
            foreach (Control ctr in panel3.Controls)
            {
                if (ctr is CheckBox)
                {
                    CheckBox cb = ctr as CheckBox;
                    if (cb.Checked)
                    {
                        strrule.Append(" or  Batch like '%" + cb.Text + "%' ");
                    }
                }
            }
            strrule.Append(" )  ");
            return strrule.ToString();
        }

        private string SelectRule1(string Date, string date1, string wardcode)
        {

            StringBuilder strrule = new StringBuilder();
            strrule.Append(" and ivstatus>=13 and WardRetreat='0' and LabelOver=0 ");
            if (wardcode != "")
                strrule.Append(" and IVRecord.WardCode in(" + wardcode + ") ");
            if (datepick.Value.Day==dateTimePicker1.Value.Day)
            {
                strrule.Append(" and DATEDIFF(DD,InfusionDT,'" + Date + "')=0 ");
                strrule.Append(" and (1=2");
            }
            else
            {
                strrule.Append(" and (InfusionDT between '" + Date + "' and '" + date1 + "') ");
                //strrule.Append(" and DATEDIFF(DD,InfusionDT,'" + Date + "')<=0 ");
                //strrule.Append(" and DATEDIFF(DD,InfusionDT,'" + date1 + "')>=0 ");
                strrule.Append(" and (1=2");
            }
            foreach (Control ctr in panel2.Controls)
            {
                if (ctr is CheckBox)
                {
                    CheckBox cb = ctr as CheckBox;
                    if (cb.Checked)
                    {
                        strrule.Append(" or Batch like '%" + cb.Text.Replace("批", "").Trim() + "%'");
                    }
                }
            }
            strrule.Append(" ) ");

            return strrule.ToString();
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        //单个打印
        private void button3_Click(object sender, EventArgs e)
        {
            //previewControlPL.Print();
            previewControl1.Print();//打印

            addDBCode();
            //button3.Enabled = false;

        }

        private void txtward_TextChanged(object sender, EventArgs e)
        {
            if (dtward.Rows.Count <= 0)
                return;

            SelectDward();
            checkBox5.Checked = false;
        }

        private void txtward_Enter(object sender, EventArgs e)
        {
            if (txtward.Text == "病区名/简拼")
            {
                txtward.Text = "";
                txtward.ForeColor = Color.Black;
            }
        }

        private void txtward_Leave(object sender, EventArgs e)
        {
            if (txtward.Text == "")
            {
                txtward.Text = "病区名/简拼";
                txtward.ForeColor = Color.Gray;
            }
        }
        //当病区值改变
        private void cbarea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dtward.Rows.Count <= 0)
                return;
            SelectDward();
            checkBox5.Checked = false;
        }
        //模糊搜索
        private void SelectDward()
        {
            string rule1 = txtward.Text.Trim() == "" || txtward.Text.Trim() == "病区名/简拼" ? "" : " and (WardName like '%" + txtward.Text.Trim() + "%' or Spellcode like '%" + txtward.Text.Trim() + "%')";
            string rule2 = cbarea.SelectedValue.ToString() == "<全部病区组>" ? "1=1 " : "WardArea='" + cbarea.SelectedValue.ToString().Trim() + "'";
            DataTable dt = dtward.Copy();
            dt.Rows.Clear();
            DataRow[] DR = dtward.Select(rule2 + rule1, " WardSeqNo");
            foreach (DataRow dr in DR)
            {
                dt.ImportRow(dr);
            }
            // 重新加载下病区

            // dataGridView1.DataSource = dt;
            checkedListBox1.Items.Clear();
            if (dt.Rows.Count < 0)
                return;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    checkedListBox1.Items.Add(dataRow[0]); //= dataRow[1].ToString()
                }
            }


        }

        private void cbarea_Enter(object sender, EventArgs e)
        {
            cbarea.ForeColor = Color.Black;
        }

        private void cbarea_Leave(object sender, EventArgs e)
        {
            cbarea.ForeColor = Color.Gray;
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Gainsboro;
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        private void Panel_Max_MouseHover(object sender, EventArgs e)
        {
            Panel_Max.BackColor = Color.Gainsboro;
        }

        private void Panel_Max_MouseLeave(object sender, EventArgs e)
        {
            Panel_Max.BackColor = Color.Transparent;
        }

        private void Panel_Min_MouseHover(object sender, EventArgs e)
        {
            Panel_Min.BackColor = Color.Gainsboro;
        }

        private void Panel_Min_MouseLeave(object sender, EventArgs e)
        {
            Panel_Min.BackColor = Color.Transparent;
        }

        private void Panel_Max_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                Panel_Max.BackgroundImage = global::ConfectCollect.Properties.Resources.还原;

            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                Panel_Max.BackgroundImage = global::ConfectCollect.Properties.Resources._20;
            }
        }

        private void Panel_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        /// <summary>
        /// 获取科室选择项
        /// 返回多个WardCode值，用“，”分开
        /// </summary>
        /// <returns></returns>
        private string getCheckedVulesToWardCode()
        {
            string strCollected = string.Empty;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))//判断是否被选中
                {
                    string WardCode = getWardCodeByWardName(checkedListBox1.Items[i].ToString());//通过选择的WardName获取WardNode
                    if (strCollected == string.Empty)
                    {
                        strCollected = WardCode;
                    }
                    else
                    {
                        strCollected = strCollected + "," + WardCode;
                    }
                }
            }
            //MessageBox.Show("病区数:" + checkedListBox1.Items.Count);
            return strCollected;
        }
        /// <summary>
        /// 获取科室选择项
        /// 返回多个WardName值，用“，”分开
        /// </summary>
        /// <returns></returns>
        private string getCheckedVulesToWardName()
        {
            string strCollected = string.Empty;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {

                if (checkedListBox1.GetItemChecked(i))//判断是否被选中
                {
                    string WardName = checkedListBox1.GetItemText(checkedListBox1.Items[i]);
                    if (strCollected == string.Empty)
                    {
                        strCollected = WardName;
                    }
                    else
                    {
                        strCollected = strCollected + "," + WardName;
                    }
                }
            }
            return strCollected;
        }
        /// <summary>
        /// 根据科室名找到科室code
        /// </summary>
        /// <param name="WardName"></param>
        /// <returns></returns>
        private string getWardCodeByWardName(string WardName)
        {
            string WardCode = string.Empty;
            if (dtward.Rows.Count > 0 && WardName!="")
            {
                DataRow[] dr = dtward.Select("WardName = '" + WardName + "'");
                WardCode = dr[0]["WardCode"].ToString();
            }
            return "'" + WardCode + "'";

        }
        //病区全选
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            else
            {

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
            button3.Enabled = false;
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            checkBox5.CheckedChanged -= new EventHandler(this.checkBox5_CheckedChanged);
            checkBox5.Checked = false;
            checkBox5.CheckedChanged += new EventHandler(this.checkBox5_CheckedChanged);
        }

        private void datepick_ValueChanged(object sender, EventArgs e)
        {
            if (isshowHS == "1")
            {
                startTime = datepick.Value.ToString("yyyy-MM-dd HH:mm");
                endTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                startTime = datepick.Value.ToString("yyyy-MM-dd ") + "00:00:00";
                endTime = dateTimePicker1.Value.ToString("yyyy-MM-dd ") + "23:59:00";
            }
            bddvg();
            button3.Enabled = false;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (isshowHS == "1")
            {
                startTime = datepick.Value.ToString("yyyy-MM-dd HH:mm");
                endTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                startTime = datepick.Value.ToString("yyyy-MM-dd ") + "00:00:00";
                endTime = dateTimePicker1.Value.ToString("yyyy-MM-dd ") + "23:59:00";
            }
            bddvg();
            button3.Enabled = false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        /// <summary>
        /// 时间格式
        /// </summary>
        private void datetimeType()
        {
            if (isshowHS == "1")
            {
                checkBox7.Checked = true;
                datepick.CustomFormat = "yyyy-MM-dd HH:mm";
                dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
                dateTimePicker1.Size = new Size(136, 21);
                datepick.Size = new Size(136, 21);
                datepick.Value = Convert.ToDateTime(datepick.Value.ToString("yyyy-MM-dd ") + "00:00:00");
                dateTimePicker1.Value = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd ") + "23:59:00");
            }
            else
            {
                checkBox7.Checked = false;
                datepick.CustomFormat = "yyyy-MM-dd";
                dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            }
            //startTime = datepick.Value.ToString("yyyy-MM-dd ") + "00:00:00";
            //endTime = dateTimePicker1.Value.AddDays(1).ToString("yyyy-MM-dd ") + "23:59:00";
          

        }
        private void checkBox7_Click(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                isshowHS = "1";
            }
            else
            {
                isshowHS = "0";
            }
            db.IniWriteValuePivas("TimeShowType", "IsshowHS", isshowHS);
            datetimeType();
        }




    }

}
