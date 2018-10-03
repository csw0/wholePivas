using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using ChargeInterface;
using PIVAsCommon.Helper;

namespace PivasIVRPrint
{
    public partial class ToHIS : Form
    {
        private List<string> p;
        private bool ok;
        private int i = 0;
        private string adds = string.Empty;
        private string userID = string.Empty;
        private int ind = 0;
        private DB_Help dbHelp = new DB_Help();

        public ToHIS(List<string> p, string userID)
        {
            this.p = p;
            this.userID = userID;
            int.TryParse(dbHelp.GetPivasAllSet("打印-打印计费-弹窗设置"), out ind);
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose(true);
            }
            else
            {
                label1.Text = "请稍候！！！";
                adds = (adds.Length < 3 ? adds + "." : string.Empty);
                if ((++i) >= ind * 2)
                {
                    button1.Visible = true;
                }
                label1.Text = label1.Text + adds;
            }
        }

        private void ToHIS_Load(object sender, EventArgs e)
        {
            try
            {
                Thread th = new Thread(() =>
                    {
                        #region csw修改，需要此exe下配置文件IMEQPIVAs.ini
                        ICharge charge = ChargeFactory.GetCharge(dbHelp.IniReadValuePivas("Charge", "HospitalType"));
                        #endregion
                        ok = charge.PrintCharge(p, userID);
                    });
                th.IsBackground = true;
                th.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
    }
}
