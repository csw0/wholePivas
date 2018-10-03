using System;
using System.Data;
using System.Windows.Forms;
using PIVAsDBhelp;
using TransTuiYao.ServiceReference1;

using TransTuiYao.ServiceReference2;
using System.Threading;

namespace TransTuiYao
{
    public partial class Form1 : Form
    {
        ServiceReference1.HisServiceClient wls = new HisServiceClient();
        ServiceReference2.UpInterfaceSoapClient upin = new UpInterfaceSoapClient();

        public Form1()
        {
            InitializeComponent();
        }

        DB_Help DB = new DB_Help();

        private void timer1_Tick(object sender, EventArgs e)
        {
            Two25 twot = new Two25();
            twot.twoTwoFive_batchChanged();
            twot.twoTwoFive_stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            CangNei cn = new CangNei();
            CangWai cw = new CangWai();
            cn.twowtofiveone();
            cw.twowToFiveTwo();
            YiZhuHuiFu re = new YiZhuHuiFu();
            re.twotwofivetwo();
        }

        //记录已经传给韦乐海茨的瓶签批次号以及更新后面更改
        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                string insertSQL = " insert into TransBatch(LabelNo,Batch) select LabelNo,Batch from "+
                    "IVRecord(nolock) where LabelNo in (select top 1000 LabelNo from IVRecordToFC(nolock) fc "+
                    "where DATEDIFF(DD,fc.FCDT,GETDATE()) < 2 and not exists(select 1 from TransBatch(nolock) tb "+
                    "where tb.LabelNo = fc.LabelNo)) ";

                DB.GetPIVAsDB(insertSQL);
            }
            catch (Exception ex)
            {
                DB.PreserveLog("error","韦乐海茨的接口出错：" +ex.Message);
            }
        }

        /// <summary>
        /// 定时器30秒，向英特吉设备发送瓶签数据
        /// </summary>
        private void timerSendToYTJ_Tick(object sender, EventArgs e)
        {
            MakeYtjXml mXml = new MakeYtjXml();
            mXml.transITJ();
        }

        /// <summary>
        /// 清空IVRecordToYTJ表定时，一个小时间隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerClearTable_Tick(object sender, EventArgs e)
        {
            try
            {
                //每天在夜里2点清IVRecordToYTJ表(只保留最近四天的数据)
                if (DateTime.Now.Hour == 2)
                    DB.SetPIVAsDB("delete from IVRecordToYTJ where DATEDIFF(DD,RQ,GETDATE()) > 3");
            }
            catch (Exception ex)
            {
                DB.PreserveLog("eRROR", "清IVRecordToYTJ表出错" + ex.Message);
            }
        }

        Thread threadUpdateStatus = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            threadUpdateStatus = new Thread(UpdateStatus);
            threadUpdateStatus.Start();
        }

        /// <summary>
        /// 120秒一次；获取英特吉瓶签打印状态；然后更新瓶签打印状态=3,
        /// </summary>
        void UpdateStatus()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(100000);//100s
                    DataSet dsFinished = null;

                    //访问英特吉判断是否已排药完成;这里的分钟值（10）最好设置成定时器的间隔的2倍
                    string sqlFinished = "select distinct LabelNO from vLabelDispensed where " +
                        "DATEDIFF(MI,CONVERT(DateTime,FinishTime,120),GETDATE())<=10";
                    dsFinished = DB.GetPIVAsHISDBSQL(sqlFinished);

                    //拉取的已打印的，存在重复，还有上次已经获取到的
                    if (dsFinished != null && dsFinished.Tables.Count > 0)
                    {
                        foreach (DataRow item in dsFinished.Tables[0].Rows)
                        {
                            string labelNo = item["LabelNO"].ToString().Trim();

                            string strSQl = "if not exists (select 1 from IVRecord_Print where LabelNo='" + labelNo + "') " +
                                "Insert into IVRecord_Print values('" + labelNo + "', GETDATE(), '1', 'NULL', 0," +
                                "'NULL', 'NULL', 'NULL', 'NULL', 0)";
                            int rtn = DB.SetPIVAsDB(strSQl);
                            DB.PreserveLog("Debug", "在pivas中完成更新打印表，瓶签号" + labelNo +
                                "返回值(-1是失败，0是未执行，1是正常):" + rtn);
                            Thread.Sleep(1000);

                            strSQl = "update IVRecord set IVStatus = 3," +
                                "PrintDT = case when PrintDT is null then GETDATE() else PrintDT end," +
                                " PrinterID = '1',PrinterName = 'LaennecSysadmin' where LabelNo = '" + labelNo +
                                "' and IVStatus <= 3  and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1";
                            rtn = DB.SetPIVAsDB(strSQl);
                            for (int i = 0; i < 5; i++)//最多5次
                            {
                                if (rtn <= 0)//失败或未执行成功
                                {
                                    rtn = DB.SetPIVAsDB(strSQl);
                                    Thread.Sleep(1000);
                                }
                                else break;
                            }

                            DB.PreserveLog("Debug", "在pivas中完成更新瓶签表，瓶签号" + labelNo +
                                "返回值(-1是失败，0是未执行，1是正常):" + rtn);

                            strSQl = "if not exists(select 1 from IVRecord_Print_AllEmp where LabelNo = '" + labelNo + "') " +
                                "Insert into IVRecord_Print_AllEmp values('" + labelNo +
                                "', '" + "8888" + DateTime.Now.Ticks.ToString() + "', '9999-LaennecSysadmin', ''," +
                                "'9999-LaennecSysadmin')";
                            rtn = DB.SetPIVAsDB(strSQl);
                            DB.PreserveLog("Debug", "在pivas中完成更新打印临时表，瓶签号" + labelNo +
                                "返回值(-1是失败，0是未执行，1是正常):" + rtn);
                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (ThreadAbortException) { }
                catch (Exception ex)
                {
                    DB.PreserveLog("Error", "在pivas中更新瓶签打印状态出错" + ex.Message);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (threadUpdateStatus != null)
                {
                    threadUpdateStatus.Abort();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                threadUpdateStatus = null;
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 从英特吉设备厂家的数据库中，手动获取数据
        /// </summary>
        private void btnGetDrugData_Click(object sender, EventArgs e)
        {
            try
            {
                button2.BackColor = System.Drawing.Color.Red;
                GetDrug gd = new GetDrug();
                gd.GetYTJDrug();
                button2.BackColor = System.Drawing.Color.Lime;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取英特吉数据出错" + ex.Message);
            }
        }
    }
}
