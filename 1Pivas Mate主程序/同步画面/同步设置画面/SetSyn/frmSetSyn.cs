using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SetSyn
{
    /// <summary>
    /// 同步参数设置窗体
    /// </summary>
    public partial class frmSetSyn : Form
    {
        private string EMCode = string.Empty;
        private string SynCode = string.Empty;
        private string ChosenTree = string.Empty;
        private SelectSQL select = new SelectSQL();
        private DB_Help db = new DB_Help();
        private DataSet ds = new DataSet();

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        #endregion

        public frmSetSyn(string Code, string ECode)
        {
            EMCode = ECode;
            SynCode = Code;
            Select(SynCode);
            InitializeComponent();
        }

        private void Select(string Code)
        {
            try
            {
                ds = db.GetPIVAsDB(select.Get_SynSet(Code));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetSynF_Load(object sender, EventArgs e)
        {
            Initialise();
            Panel_Cycle_Click(sender, e);
        }
        private void Initialise()
        {
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SynName"].ToString()))
            {
                Label_Head.Text = ds.Tables[0].Rows[0]["SynName"].ToString() + "同步配置";
                this.Text = ds.Tables[0].Rows[0]["SynName"].ToString() + "同步配置";
            }
            if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LogDBSave"].ToString())||ds.Tables[0].Rows[0]["LogDBSave"].ToString() == "False")
            {
                Pic_Save_ONOFF.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("开");
                Pic_Save_ONOFF.Tag = "0";
            }
            else
            {
                Pic_Save_ONOFF.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("关");
                Pic_Save_ONOFF.Tag = "1";
            }
            Label_Cycle.Tag = "0";
            Label_Repeat.Tag = "1";
            Label_SaveLog.Tag = "2";
            Label_SynSet.Tag = "4";
            Label_Other.Tag = "5";
        }

        private void Panel_Cycle_Click(object sender, EventArgs e)
        {
            try
            {
                Save(SynCode, Label_Cycle.Tag.ToString());
                Panel_U.Controls.Add(new USynMinSet(SynCode));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void Panel_SynSet_Click(object sender, EventArgs e)
        {
            try
            { 
                Save(SynCode, Label_SynSet.Tag.ToString());
                Panel_U.Controls.Add(new USynSet(SynCode));
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void Panel_Other_Click(object sender, EventArgs e)
        {
            Save(SynCode, Label_Other.Tag.ToString());
        }

        private void Pic_ONOFF_Click(object sender, EventArgs e)
        {
            Save(SynCode, Label_SaveLog.Tag.ToString());
            Panel_U.Controls.Add(new USaveLog(SynCode, this.Text.Replace("同步配置", "")));
            if (Equals(Pic_Save_ONOFF.Tag.ToString(), "0"))
            {
                Pic_Save_ONOFF.Tag = "1";
                Pic_Save_ONOFF.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("关");
            }
            else
            {
                Panel_U.Controls[0].Enabled = false;
                Pic_Save_ONOFF.Tag = "0";
                Pic_Save_ONOFF.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("开");
            }
        }

        private void Pic_R_ONOFF_Click(object sender, EventArgs e)
        {
            Save(SynCode, Label_Repeat.Tag.ToString());
            if (Equals(Pic_R_ONOFF.Tag.ToString(),"0"))
            {
                Pic_R_ONOFF.Tag = "1";
                Pic_R_ONOFF.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("关");
            }
            else
            {
                Pic_R_ONOFF.Tag = "0";
                Pic_R_ONOFF.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("开");
            }
        }

        private bool Save(string SyCode, string ChoseTrees)
        {
            bool tf = true;
            Panel_Cycle.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("周期");
            Panel_Repeat.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("失败重复");
            Panel_SaveLog.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("保存日志");
            Panel_SynSet.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("同步设置");
            panel3.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("同步设置");
            Panel_Other.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("其它");
            switch (ChoseTrees)
            {
                case "0":
                    Panel_Cycle.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("周期2");
                    break;
                case "1":
                    Panel_Repeat.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("失败重复2");
                    break;
                case "2":
                    Panel_SaveLog.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("保存日志2");
                    break;
                case "4":
                    Panel_SynSet.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("设置2");
                    break;
                case "5":
                    Panel_Other.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("其它2");
                    break;
                case "6":
                    panel3.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("设置2");
                    break;
                default:
                    break;
            }
            switch (ChosenTree)
            {
                case "0":
                    {
                        break;
                    }
                case "1":
                    {
                        break;
                    }
                case "2":
                    {
                        tf = ((USaveLog)Panel_U.Controls[0]).Update(SyCode, Convert.ToInt32(Pic_Save_ONOFF.Tag.ToString()));
                        break;
                    }
                case "3":
                    {
                        break;
                    }
                case "4":
                    {
                        tf = ((USynSet)Panel_U.Controls[0]).Update(SyCode);
                        break;
                    }
                default:
                    break;
            }
            Panel_U.Controls.Clear();
            ChosenTree = ChoseTrees;
            return tf;
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            Save(SynCode,"");
            this.Dispose();
        }

        private void Label_Head_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Label_SaveLog_Click(object sender, EventArgs e)
        {
            Save(SynCode,Label_SaveLog.Tag.ToString());
            Panel_U.Controls.Add(new USaveLog(SynCode, this.Text.Replace("同步配置", "")));
            if (Equals(Pic_Save_ONOFF.Tag.ToString(), "0"))
            {
                Panel_U.Controls[0].Enabled = false;
            }
        }
        private void Label_Repeat_Click(object sender, EventArgs e)
        {
            Save(SynCode, Label_Repeat.Tag.ToString());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                Save(SynCode, label1.Tag.ToString());
                Panel_U.Controls.Add(new DeleteTableNew());
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
    }
}
