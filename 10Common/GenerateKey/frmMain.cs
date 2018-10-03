using CommonUI.Froms;
using PIVAsCommon.Helper;
using System;
using System.Text;
using System.Windows.Forms;

namespace GenerateKey
{
    public partial class frmMain : Form
    {
        DB_Help dbHelp = new DB_Help();
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改pivas数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            DBSet dbSet = new DBSet(DatabaseType.PivasDB);
            dbSet.ShowDialog();
        }

        /// <summary>
        /// 修改His数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DBSet dbSet = new DBSet(DatabaseType.HISDB);
            dbSet.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string dat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string PW = PermitHelper.EncrypOrDecryp(dat, dat, true);
                StringBuilder sb = new StringBuilder(4096);
                #region 其他加密，暂时没用
                //sb.Append("IF OBJECT_ID('PivasDesPassWord') IS NULL ");
                //sb.Append(" begin ");
                //sb.Append(" Create table [PivasDesPassWord](ID INT identity(1,1) PRIMARY key,MacSSID VARCHAR(250),PivasWord text,Dat datetime) ");
                //sb.Append(" end ");
                //sb.Append(" else ");
                //sb.Append(" begin ");
                //sb.Append(string.Format(" UPDATE [PivasDesPassWord] SET [MacSSID] = '{0}' WHERE ID=(SELECT MAX(ID) FROM [PivasDesPassWord]) ", HDssid));
                //sb.Append(" end ");
                #endregion
                sb.Append("IF OBJECT_ID('PivasDesSoftTAB') IS NULL ");
                sb.Append(" begin ");
                sb.Append(" Create table [PivasDesSoftTAB](ID INT identity(1,1) PRIMARY key,SoftPW VARCHAR(512),Dat datetime) ");
                sb.Append(string.Format(" insert into [PivasDesSoftTAB] values('{0}','{1}') ", PW, dat));
                sb.Append(" end ");
                sb.Append(" else ");
                sb.Append(" begin ");
                sb.Append(" truncate table [PivasDesSoftTAB] ");
                sb.Append(string.Format(" insert into [PivasDesSoftTAB] values('{0}','{1}') ", PW, dat));
                sb.Append(" end ");
                dbHelp.SetPIVAsDB(sb.ToString());
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("设置同步程序最新出错" + ex.Message);
            }
        }
    }
}
