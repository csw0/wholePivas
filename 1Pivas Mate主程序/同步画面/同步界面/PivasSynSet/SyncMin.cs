using PIVAsCommon.Helper;
using PivasLimitDES;
using System;
using System.Windows.Forms;

namespace PivasSynSet
{
    public partial class SyncMin : UserControl
    {
        private string userid;//员工号
        private string SynCode;//同步code
        private DB_Help dbHelp = new DB_Help();

        public SyncMin(string user, string SynCode)
        {
            userid = user;
            this.SynCode = SynCode;
            InitializeComponent();
        }
        protected internal string getcode()
        {
            return SynCode;
        }

        private void panel1_Click(object sender, EventArgs e)//双击调用同步设置画面
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(userid, "01002"))
                {
                    if (!elementHost1.Visible)
                    {
                        new SetSyn.frmSetSyn(SynCode, userid).ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string insertNewSynLog()
        {
            string rt = string.Format("exec bl_InsertNewSynLog '{0}','{1}'", SynCode, userid).ToString();
            return rt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (GetPivasLimit.Instance.Limit(userid, "01003"))
            {
                new PivasSynLog.PivasSynLog(SynCode).ShowDialog();
            }
        }

        /// <summary>
        /// 插同步日志或更新日志为取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyn_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(userid, "01001"))
                {
                    if (elementHost1.Visible)
                    {
                        dbHelp.GetPIVAsDB(string.Format("UPDATE [dbo].[SynLog] SET [EndUPTime] = GETDATE(),[EndTime] = GETDATE(),[Success] = 1,[Schedule] = 500,[ScheduleTxt] = '手动取消同步' WHERE SynCode='{0}'", SynCode));
                    }
                    else
                    {
                        dbHelp.GetPIVAsDB(insertNewSynLog());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Parent.Focus();
        }
    }
}
