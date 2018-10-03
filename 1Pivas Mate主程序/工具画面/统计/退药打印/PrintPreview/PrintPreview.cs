using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PrintPreview
{
    public partial class PrintPreview : Form
    {
        public PrintPreview()
        {
            InitializeComponent();
        }

        public PrintPreview(DataTable dt, string title, string count)
        {
            InitializeComponent();
            this.dt = dt;
            this.Title = title;
            this.count = count;
        }

        /// <summary>
        /// 调用的方法
        /// </summary>
        /// <param name="dt">打印的瓶签信息列表</param>
        /// <param name="title">报表名称</param>
        /// <param name="count">总数</param>
        /// <param name="labelNos">瓶签号拼成的字符串，用,分割，如20140919100010,20140919100010,20140919100010</param>
        public PrintPreview(DataTable dt, string title, string count, string labelNos)
        {
            InitializeComponent();
            this.dt = dt;
            this.labelNos = labelNos;
            this.Title = title;
            this.count = count;
        }
        private DataTable dt;
        private string labelNos;
        private string Title;
        private string count;
        DB_Help db = new DB_Help();
        private string DBCode = string.Empty;

        private void PrintPreview_Load(object sender, EventArgs e)
        {

            DBCode = getDBCode();
            try
            {
                report.Preview = previewQD;
                report.Load(".\\Crystal\\CanelDrug.frx");
                report.GetParameter("Title").Value = CheckTitle();
                report.GetParameter("PrintDT").Value = DateTime.Now.ToString();
                report.GetParameter("Total").Value = count;
                //report.GetParameter("DBCode").Value = getDBCode();
                report.SetParameterValue("DBCode", DBCode);
               
                report.RegisterData(dt, "dt");
                ((report.FindObject("Data1")) as FastReport.DataBand).DataSource = report.GetDataSource("dt");
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string CheckTitle()
        {
            string s = Title;
            switch (Title)
            {
                case "": s = "全部";
                    break;
                case "<全部>": s = "全部";
                    break;
                case "已排药": s = "排药";
                    break;
                case "已进仓": s = "进仓";
                    break;
                case "已配置": s = "配置";
                    break;
                case "配置取消": s = "配置取消";
                    break;
                case "已出仓": s = "出仓";
                    break;
                case "已打包": s = "打包";
                    break;
                case "已退药": s = "退药";
                    break;
                case "已签收": s = "签收";
                    break;
                case "提前打包": s = "提前打包";
                    break;
            }
            return s;
            //if (Title == "" || Title == "<全部>")
            //    return "全部";
            //else if (Title == "已排药")
            //    return "排药";
            //else if (Title == "已配置")
            //    return "配置";
            //else if (Title == "配置取消")
            //    return "配置取消";
            //else if (Title == "已签收")
            //    return "签收";
            //else if (Title == "提前打包")
            //    return "提前打包";
            //else if (Title == "已退药")
            //    return "退药";
            //else
            //    return "";
        }

        private void mPrint_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateTY();
                if (previewQD.Print().ToString() == "True")
                {
                    switch (Title)
                    {
                        case "": //db.SetPIVAsDB("");
                            break;
                        case "<全部>": //db.SetPIVAsDB("");
                            break;
                        case "未打印": db.SetPIVAsDB("update PrintList set unprintList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已打印": db.SetPIVAsDB("update PrintList set printedList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已排药": db.SetPIVAsDB("update PrintList set PYList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已进仓": db.SetPIVAsDB("update PrintList set JCList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已配置": db.SetPIVAsDB("update PrintList set PZList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已出仓": db.SetPIVAsDB("update PrintList set CCList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已打包": db.SetPIVAsDB("update PrintList set DBList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已签收": db.SetPIVAsDB("update PrintList set QSList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "已退药": db.SetPIVAsDB("update PrintList set TYList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "提前打包": db.SetPIVAsDB("update PrintList set TQDBList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                        case "配置取消": db.SetPIVAsDB("update PrintList set PZQXList = 1 where LabelNo in (" + labelNos + ")");
                            break;
                    }
                }
                
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 得到二维码
        /// </summary>
        /// <returns></returns>
        private string getDBCode()
        {

            string DBCode = string.Empty;
            Random rad = new Random();//实例化随机数产生器rad；
            int value = rad.Next(10, 100);
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            dt = dt.Replace("-", "").Replace(" ", "").Replace(":", "");
            dt = dt.Substring(4, 10);//月份+日期+小时+分钟+秒
            DBCode = dt + value;//二维码组成：当前时间的后8位+病区4位+2位随机数,共14位
            return DBCode;

        }

        private void UpdateTY()
        {

            string[] labels = labelNos.Split(',');

            for (int i = 0; i < labels.Length; i++)
            {
                //MessageBox.Show(labels[i].ToString());
                using (DataSet dsPCode = db.GetPIVAsDB("select PrinterID from IVRecord where LabelNo='" + labels[i] + "'"))
                {
                    using (DataSet dsDBCode = db.GetPIVAsDB("select DBCode from IVRecord_TY where IVRecordID='" + labels[i] + "'"))
                    {
                        //在插入表时候先进行查询，如果表中有值就更新，否则就插入
                        if (dsDBCode.Tables[0].Rows.Count == 0)
                        {
                            string sqlInsert = "insert into IVRecord_TY (IVRecordID,TYDT,PCode,DBCode) values('{0}','{1}','{2}','{3}')";
                            sqlInsert = string.Format(sqlInsert, labels[i], DateTime.Now.ToString(), dsPCode.Tables[0].Rows[0]["PrinterID"].ToString(), DBCode);
                            db.GetPIVAsDB(sqlInsert);
                        }
                        else
                        {
                            string sqlUpdate = "update IVRecord_TY set DBCode='{0}' where  IVRecordID='{1}'";
                            sqlUpdate = string.Format(sqlUpdate, DBCode, labels[i]);
                            db.GetPIVAsDB(sqlUpdate);
                        }
                    }
                }
               

            }
        }
    }

}
