using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasSynSet
{
    public partial class Sync : UserControl, IMenuManager
    {
        private string userid = string.Empty;//员工ID
        private DB_Help db= new DB_Help();
        private bool locked = false;

        public Sync(string user, string str1, string str2)
        {
            userid = user;
            InitializeComponent();
        }
        private void Sync_Load(object sender, EventArgs e) { }
        private string getSynSetDate()
        {
            return "SELECT SynCode,SYnName,[SynID],StartTime,EndTime,Schedule,[ScheduleTxt],DEmployeeName FROM [dbo].[V_Sync] order by orderby";
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            foreach (SyncMin c in flowLayoutPanel1.Controls)
            {
                c.Width = this.Width;
            }
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!locked)
            {
                locked = true;
                bool HasSyn = false;
                try
                {
                    using (DataSet ds = db.GetPIVAsDB(getSynSetDate()))
                    {
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            foreach (SyncMin sm in flowLayoutPanel1.Controls)
                            {
                                DataRow[] drs = ds.Tables[0].Select(string.Format("SynCode='{0}'", sm.getcode()));
                                if (drs.Length > 0)
                                {
                                    DataRow dr = drs[0];
                                    if (string.IsNullOrEmpty(dr["EndTime"].ToString()) && !string.IsNullOrEmpty(dr["SynID"].ToString()))
                                    {
                                        sm.elementHost1.Visible = true;
                                        int Schedule = 0;
                                        //Schedule进度0~500若处方与药单则0~400数据插入;存储过程执行400~500
                                        int.TryParse(dr["Schedule"].ToString().Trim(), out Schedule);
                                        sm.syn_ProgressBar1.progressbar.Value = Schedule;
                                        //1:等待同步 2：同步开始 3：数据插入 4：拆分处方（审方）5拆分药单 6：同步完成
                                        sm.label2.Text = dr["ScheduleTxt"].ToString() + " , 总进度已完成:" + (Schedule > 499 ? 100 : Convert.ToInt32(Schedule / 5)) + "%    操作员工：" + dr["DEmployeeName"].ToString();
                                    }
                                    else
                                    {
                                        if (sm.elementHost1.Visible)
                                        {
                                            sm.syn_ProgressBar1.progressbar.Value = 500;
                                            sm.elementHost1.Visible = false;
                                            sm.syn_ProgressBar1.progressbar.Value = 0;
                                        }
                                        sm.label2.Text = "上次同步时间：" + dr["EndTime"].ToString() + "    同步结果：" + dr["ScheduleTxt"].ToString() + "    操作员工：" + dr["DEmployeeName"].ToString();
                                    }
                                }
                                if (sm.elementHost1.Visible)
                                {
                                    HasSyn = true;
                                }
                                timer.Interval = HasSyn ? 500 : 1000;
                            }
                        }
                    }
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);
                }
                finally
                {
                    locked = false;
                }
            }
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            try
            {
                using (DataSet ds = db.GetPIVAsDB(getSynSetDate()))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        flowLayoutPanel1.Controls.Clear();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SyncMin sm = new SyncMin(userid, dr["SynCode"].ToString());
                            sm.Width = this.Width;
                            sm.label1.Text = dr["SYnName"].ToString();
                            flowLayoutPanel1.Controls.Add(sm);
                        }
                        flowLayoutPanel1.Focus();
                        timer.Enabled = true;
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion
    }
}
