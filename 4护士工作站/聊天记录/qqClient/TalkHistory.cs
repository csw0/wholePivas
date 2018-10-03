using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace qqClient
{
    public partial class TalkHistory : Form
    {
        SQL sql = new SQL();
        DB_Help db = new DB_Help(); 
        private string TalkName = string.Empty;
        private string login = string.Empty;
        private string Demployeeid = string.Empty;
        private string WardCode = string.Empty;
        private string DemployName = string.Empty;
        public TalkHistory(string wardcode, string demployId,string demployName, string loginType, string talkName)
        {
            InitializeComponent();
            this.TalkName = talkName;
            this.login = loginType;
            this.WardCode = wardcode;
            this.DemployName = demployName;
        }
        private void TalkHistory_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddDays(-6) ;
            AddInfor();
        }


        private void AddInfor()
        {
          

                if (TalkName == "AllWard")
                {
                    Talk1(1);
                }
                else
                {
                    Talk1(0);
                }
           
        }
        /// <summary>
        /// 获得群聊天记录
        /// </summary>
        private void Talk1(int type)
        {
            flp.Controls.Clear();
            DataSet ds = new DataSet();
            if (type == 1)
            {
                ds = db.GetPIVAsDB(sql.GetWardTalk(WardCode, "0", true,dateTimePicker1.Value,dateTimePicker2.Value));
            }
            else
            {
                //配置中心
                if (login == "PivasMate")
                {
                    ds = db.GetPIVAsDB(sql.GetWardTalk(TalkName, "1", true, dateTimePicker1.Value, dateTimePicker2.Value));
                }
                //护士工作站内
                else if (login == "PivasNurse" && TalkName== WardCode)
                {
                    ds = db.GetPIVAsDB(sql.GetWardTalk(TalkName, "2", true, dateTimePicker1.Value, dateTimePicker2.Value));
                }
                else if (login == "PivasNurse" && TalkName == "PivasMate")
                {
                    ds = db.GetPIVAsDB(sql.GetWardTalk(WardCode, "1", true, dateTimePicker1.Value, dateTimePicker2.Value));
                }


            }
            ShowTalkLog(ds);
            
        }

        private void ShowTalkLog(DataSet ds)
        {
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string de = ds.Tables[0].Rows[i]["DEmployeeName"].ToString();
                    string ins = ds.Tables[0].Rows[i]["InsertTime"].ToString();
                    string cont = ds.Tables[0].Rows[i]["Content"].ToString();
                    string type = ds.Tables[0].Rows[i]["stype"].ToString();
                    string wardName = ds.Tables[0].Rows[i]["wardName"].ToString();
                    JudgeShow(de, ins, cont, type, wardName);
                }
                flp.ScrollControlIntoView(flp.Controls[ds.Tables[0].Rows.Count - 1]);
              
            }
            else
            {
                flp.Controls.Clear();
            }
        }


        /// <summary>
        /// 显示内容
        /// </summary>
        /// <param name="de">发送人</param>
        /// <param name="ins">发送时间</param>
        /// <param name="cont">发送内容</param>
        /// <param name="type">登陆人类别</param>
        /// <param name="wardName">发送人病区</param>
        private void JudgeShow(string de, string ins, string cont, string type, string wardName)
        {
            if (type == "1")
            {
                Notice n = new Notice(de, ins, cont);
                flp.Controls.Add(n);

            }
            else
            {
                MyTalkLog mtl = new MyTalkLog(de, ins, cont, wardName, DemployName, TalkName, login);
                flp.Controls.Add(mtl);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

   

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
        }

        private void flp_Click(object sender, EventArgs e)
        {
            this.flp.Focus();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            AddInfor();
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            AddInfor();
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

       

    }
}
