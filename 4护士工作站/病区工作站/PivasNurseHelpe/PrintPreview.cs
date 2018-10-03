using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace PivasLabelSelect
{
    public partial class PrintPreview : Form
    {
        public PrintPreview()
        {
            InitializeComponent();
        }
        public PrintPreview(DataTable dt,string title,string count)
        {
            InitializeComponent();
            this.dt = dt;           
            this.Title = title;
            this.count=count ;
        }


        public PrintPreview(DataTable dt, string title, string count,string labelNos)
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

        private void PrintPreview_Load(object sender, EventArgs e)
        {      
            try
            {
                report.Preview = previewQD;
                report.Load(".\\Crystal\\CanelDrug.frx");
                report.GetParameter("Title").Value = CheckTitle();                
                report.GetParameter("PrintDT").Value = DateTime.Now.ToString();
                report.GetParameter("Total").Value = count;
                report.RegisterData(dt, "dt");
                ((report.FindObject("Data1")) as FastReport.DataBand).DataSource = report.GetDataSource("dt");
                report.Show();                  
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string  CheckTitle()
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
                if(previewQD.Print().ToString()=="True")
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
    }
}
