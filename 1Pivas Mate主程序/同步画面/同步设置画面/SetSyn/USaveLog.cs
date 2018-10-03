using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class USaveLog : UserControl
    {
        private string syncode=string.Empty;
        private string Txt = string.Empty;

        private string Days = string.Empty;
        private DB_Help db = new DB_Help();
        private UpdateSQL update = new UpdateSQL();
        private SelectSQL select = new SelectSQL();
        private DataSet ds = new DataSet();

        public USaveLog(string code, string text)
        {
            Txt = text;
            syncode = code;
            Select(code);
            InitializeComponent();
        }



        private void Select(string Code)
        {
            ds = db.GetPIVAsDB(select.Get_SynSet(Code));
        }

        public bool Update(string syncode, int LogSave)
        {
            string LogName = string.Empty;
            int i = 0;
            if (LogSave == 1)
            {
                if (Label_Day.Text.Trim().Length == 0)
                {
                    Label_Day.Text = "10";
                }
                if (Pic_LogSave.Visible)
                {
                    LogSave = 1;
                    LogName = Label_LogName.Text;
                }
                else
                {
                    LogSave = 0;
                }
                i = db.SetPIVAsDB(update.Get_SynSet(syncode, LogName, LogSave, Convert.ToInt32(Label_Day.Text)));
            }
            else
            {
                i = db.SetPIVAsDB(update.Get_SynSet(syncode, LogSave));
            }
            //if (Label_LogName.Text != "")
            //{
            //    LogName = Label_LogName.Text;
            //}
            return (i > 0);
        }

        private void USaveLog_Load(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LogDirName"].ToString()))
                {
                    Label_LogName.Text = ds.Tables[0].Rows[0]["LogDirName"].ToString();
                    Pic_LogSave.Visible = true;     
                    Label_LogName.Visible = true;
                }
                else
                {
                    Pic_LogSave.Visible = false;     
                }
                if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["LogNumDays"].ToString()))
                {
                    Label_Day.Text = "10";
                }
                else
                {
                    Label_Day.Text = ds.Tables[0].Rows[0]["LogNumDays"].ToString();
                }
            }
            Label_LogName.Text = Label_LogName.Text.Replace("**", ds.Tables[0].Rows[0]["SynName"].ToString());
        }

        private void Pic_DropDown_Click(object sender, EventArgs e)
        {
            Panel_Days.Visible = !Panel_Days.Visible;
        }
      
        private void Label_1_Click(object sender, EventArgs e)
        {
            Panel_Days.Visible = false;
            Label_Day.Text = Days;
        }

        private void Label_1_MouseHover(object sender, EventArgs e)
        {
            Label_1.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days=Label_1.Text;
        }

        private void Label_1_MouseLeave(object sender, EventArgs e)
        {
            Label_1.Image = null;
        }

        private void Label_2_MouseHover(object sender, EventArgs e)
        {
            Label_2.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_2.Text;

        }

        private void Label_2_MouseLeave(object sender, EventArgs e)
        {
            Label_2.Image = null;
        }

        private void Label_3_MouseHover(object sender, EventArgs e)
        {
            Label_3.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_3.Text;
        }

        private void Label_3_MouseLeave(object sender, EventArgs e)
        {
            Label_3.Image = null;
        }


        private void Label_4_MouseHover(object sender, EventArgs e)
        {
            Label_4.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_4.Text;
        }

        private void Label_4_MouseLeave(object sender, EventArgs e)
        {
            Label_4.Image = null;
        }

        private void Label_5_MouseHover(object sender, EventArgs e)
        {
            Label_5.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_5.Text;
        }

        private void Label_5_MouseLeave(object sender, EventArgs e)
        {
            Label_5.Image = null;
        }

        private void Label_6_MouseHover(object sender, EventArgs e)
        {
            Label_6.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_6.Text;
        }

        private void Label_6_MouseLeave(object sender, EventArgs e)
        {
            Label_6.Image = null;
        }

        private void Label_7_MouseHover(object sender, EventArgs e)
        {
            Label_7.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_7.Text;

        }

        private void Label_7_MouseLeave(object sender, EventArgs e)
        {
            Label_7.Image = null;
        }

        private void Label_8_MouseHover(object sender, EventArgs e)
        {
            Label_8.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_8.Text;

        }

        private void Label_8_MouseLeave(object sender, EventArgs e)
        {
            Label_8.Image = null;
        }

        private void Label_9_MouseHover(object sender, EventArgs e)
        {
            Label_9.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_9.Text;
        }

        private void Label_9_MouseLeave(object sender, EventArgs e)
        {
            Label_9.Image = null;
        }

        private void Label_10_MouseHover(object sender, EventArgs e)
        {
            Label_10.Image = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("天数小边框");
            Days = Label_10.Text;
        }

        private void Label_10_MouseLeave(object sender, EventArgs e)
        {
            Label_10.Image = null;
        }

        private void Panel_LogSave_Click(object sender, EventArgs e)
        {
            if (!Pic_LogSave.Visible)
            {
                Pic_LogSave.Visible = true;
                Label_LogName.Visible = true;
                Label_LogName.Text = Label_LogName.Text.Replace("**", ds.Tables[0].Rows[0]["SynName"].ToString());
                Label_LogName.Focus();
            }
            else
            {
                Pic_LogSave.Visible = false;
                Label_LogName.Visible = false;
            }
        }

        private void Panel_LogDBSave_Click(object sender, EventArgs e)
        {
            Pic_LogDBSave.Visible = !Pic_LogDBSave.Visible;
        }

        private void USaveLog_EnabledChanged(object sender, EventArgs e)
        {
            Pic_LogDBSave.Visible = Label_LogName.Enabled;
            if (!Label_LogName.Enabled)
            {
                Pic_LogSave.Visible = false;
                Label_LogName.Visible = false;
                Label_Day.Enabled = false;
            }
        }
    }
}
