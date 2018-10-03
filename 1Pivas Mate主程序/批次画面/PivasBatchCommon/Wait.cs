using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PIVAsCommon.Helper;

namespace PivasBatchCommon
{
    public partial class Wait : Form
    {
        InsertSql insert = new InsertSql();
        UpdateSql update = new UpdateSql();
        SelectSql select = new SelectSql();
        DateTime dt;
        DB_Help db = new DB_Help();
        //员工号
        string Empid = string.Empty;
        //Logid
        string ID = string.Empty;
        //病区CODE
        string ward = string.Empty;
        //病人CODE
        string pcode = string.Empty;
        /// <summary>
        ///i   0： 生成瓶签还 1：是重排
        /// </summary>
        int i = 0;

        public Wait(DateTime dt, string Emp)
        {
            InitializeComponent();
            this.dt = dt;
        }

        public Wait(DateTime dt, string Emp, string ward, string pcode, int i)
        {
           
            InitializeComponent();
            this.i = i;
            this.Empid = Emp;
            this.pcode = pcode;
            this.ward = ward;
            this.dt = dt;
        }

        /// <summary>
        /// 根据药单接受日期生成瓶签
        /// </summary>
        public void GenerationOrder()
        {
            try
            {
                DataSet dds = db.GetPIVAsDB(select.PBatchTemp());
                if (dds == null || dds.Tables[0].Rows.Count <= 0)
                {
                    //if(dds.Tables.Count<=0)
                    if (i == 0)
                    {
                        DataSet ds = db.GetPIVAsDB(insert.OrderLog(Empid, string.Empty, string.Empty, this.dt.ToString("yyyy-MM-dd")));
                        ID = ds.Tables[0].Rows[0]["LogID"].ToString().Trim();
                        ds.Dispose();
                    }
                    //db.SetPIVAsDB(update.GetIvRecord(ID, "", "", this.dt));

                    else
                    {
                        DataSet ds = db.GetPIVAsDB(insert.OrderLog(Empid, ward, pcode, this.dt.ToString("yyyy-MM-dd")));
                        ID = ds.Tables[0].Rows[0]["LogID"].ToString().Trim();
                        ds.Dispose();

                        db.SetPIVAsDB(update.IVRecordIsPatch(ward, pvb.datetime.ToString("yyyyMMdd"), pcode));
                        //db.SetPIVAsDB(update.GetIvRecord(ID, ward, pcode, this.dt));

                    }
                }
                else
                {
                    ID = dds.Tables[0].Rows[0]["logid"].ToString().Trim();
                    dds.Dispose();
                }
            }
            catch 
            {
                //MessageBox.Show(eee.ToString());
            }
        }


        //static Mutex myMutex = new Mutex();
        bool myMutex = false;
        string text = string.Empty;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //myMutex.WaitOne();
            if (!myMutex)
            {
                myMutex=true;
                try
                {
                    DataSet ds = db.GetPIVAsDB(select.OrderLog(ID));
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["DoCount"].ToString() == "结束" || ds.Tables[0].Rows[0]["DoCount"].ToString() == "重排结束"
                            || ds.Tables[0].Rows[0]["End"].Equals(true))
                        {
                            this.Close();
                        }
                        else
                        {

                            if (text == ds.Tables[0].Rows[0]["DoCount"].ToString().Trim())
                            {
                                if (Label_Order.Text.Contains("..."))
                                {
                                    Label_Order.Text = text;
                                }
                                else
                                {
                                    Label_Order.Text = Label_Order.Text + ".";
                                }
                            }
                            else
                            {
                                Label_Order.Text = text = ds.Tables[0].Rows[0]["DoCount"].ToString().Trim();
                            }
                            // if(text.)
                        }
                        pvb.operate = true;
                    }
                    else
                    {
                        Label_Order.Text = "没有找到同步记录，请重试";
                        button1.Enabled = true;
                    }
                }
                catch (Exception te)
                {
                    MessageBox.Show(te.ToString());
                }
                finally
                {
                    myMutex = false;
                }
            }
        }

        private void Wait_Load(object sender, EventArgs e)
        {
            if (i == 1)
            {
                Label_Order.Text = "正在重排批次";
            }
            else
            {
                Label_Order.Text = "正在生成瓶签";
            }
            PB_GetOrder.Minimum = 0;
            PB_GetOrder.Maximum = 0;
            Thread tr = new Thread(() => { GenerationOrder();});
            tr.IsBackground = true;
            tr.Name = "Pivas_Syn";
            tr.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
