using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;
using System.Diagnostics;

namespace PivasSynSet
{
    public partial class SyncMin : UserControl
    {
        public string SynCode = "";//同步code
        public DB_Help DB;
        public DataSet ds = new DataSet();
        public string endtime = "";//同步结束时间
        public string userid = "";//员工号
        public Process myprocess;
        public SyncMin()
        {
            InitializeComponent();
        }
        public SyncMin(string SynCode, string userid)
        {
            this.userid = userid;
            this.SynCode = SynCode;
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                int Schedule;//、= Convert.ToInt32(ds.Tables[0].Rows[0]["Schedule"].ToString().Trim().Equals("") ? "0" : ds.Tables[0].Rows[0]["Schedule"].ToString());
                if (!SynCode.Trim().Equals(""))
                {
                    ds = DB.GetPIVAsDB(getThisSynDate());
                    endtime = ds.Tables[0].Rows[0]["EndTime"].ToString();
                    if (endtime.Trim().Equals(""))
                    {
                        run(Schedule);
                        label1.Text = ds.Tables[0].Rows[0]["SYnName"].ToString() + " (正在同步)";
                        label2.Text = label1.Text;
                        panel2.Size = new Size(panel1.Size.Width * Schedule / 500, panel1.Size.Height);
                    }
                    else
                    {
                        label1.Text = ds.Tables[0].Rows[0]["SYnName"].ToString() + "(" + ds.Tables[0].Rows[0]["EndTime"].ToString() + ")";
                        label2.Text = label1.Text;
                        panel2.Size = new Size(0, 0);
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void SyncMin_Load(object sender, EventArgs e)
        {
            DB = new DB_Help();
            timer1_Tick(sender,e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SynCode.Trim().Equals(""))
                {
                    if (!endtime.Trim().Equals(""))
                    {
                        DB.GetPIVAsDB(insertNewSynDate());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                myprocess = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo("", SynCode+" "+userid); 
                myprocess.StartInfo = startInfo;
                myprocess.StartInfo.UseShellExecute = false;
                myprocess.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string getThisSynDate()
        {
            return string.Format("SELECT * FROM [dbo].[V_Sync] where SynCode ='{0}'", SynCode);
        }

        private string insertNewSynDate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[SynLog] ");
            sb.Append("([SynCode] ,StartTime,[SynAct]) ");
            sb.Append("VALUES('{0}',GETDATE(),'{1}') ");
            return string.Format(sb.ToString(), SynCode, userid);
        }


        private void run(int Schedule)
        {
            
            if (Schedule < 500)
            {
                DB.SetPIVAsDB(string.Format("UPDATE [dbo].[SynLog] SET [Schedule] = '{0}' WHERE [SynCode]='{1}'and [RandomID] ='{2}'", Schedule + 2, SynCode, ds.Tables[0].Rows[0]["RandomID"].ToString()));
            }
            else 
            {
                DB.SetPIVAsDB(string.Format("UPDATE [dbo].[SynLog] SET [EndTime] = GETDATE(),[Success] = 1 WHERE [SynCode]='{0}'and [RandomID] ='{1}'", SynCode, ds.Tables[0].Rows[0]["RandomID"].ToString()));
            }
        }


    }
}
