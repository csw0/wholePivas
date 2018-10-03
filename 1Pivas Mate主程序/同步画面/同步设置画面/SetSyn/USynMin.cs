using PIVAsCommon.Helper;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class USynMin : UserControl
    {
        protected internal bool open;
        private DB_Help db;

        public USynMin()
        {
            InitializeComponent();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in this.Parent.Controls)
                {
                    if (c.Tag != null)
                    {
                        USynMin us = c as USynMin;
                        us.pictureBox2.Visible = false;
                        us.label1.ForeColor = Color.Silver;
                        us.label2.ForeColor = Color.Silver;
                    }
                }
                pictureBox2.Visible = true;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                db.SetPIVAsDB(string.Format("UPDATE [dbo].[SynSet] SET [SynTimeCode] = '{0}' WHERE [SynCode]='{1}'", Tag.ToString(), USynMinSet.SynCode).ToString());
                USynMinSet.SynTimeCode = Tag.ToString();
                switch (Tag.ToString())
                {
                    case "1":
                        {
                            label1.Text = "一次";
                            label2.Text = "每天定时执行一次同步";
                            label3.Text = "在" + (USynMinSet.begin.Contains(",") ? USynMinSet.begin.Split(',')[0] : USynMinSet.begin) + "点执行";
                            break;
                        }
                    case "2":
                        {
                            label1.Text = "时段";
                            label2.Text = "每天的一个时间段内间隔执行同步";
                            label3.Text = "间隔" + USynMinSet.space + "分钟执行";
                            break;
                        }
                    case "3":
                        {
                            label1.Text = "全天";
                            label2.Text = "全天间隔执行同步";
                            label3.Text = "间隔" + USynMinSet.space + "分钟执行";
                            break;
                        }
                    case "4":
                        {
                            label1.Text = "手动";
                            pictureBox1.Visible = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox2.Visible)
                {
                    USynMinSetOpen usso = this.Parent.Controls["usso"] as USynMinSetOpen;
                    if (open)
                    {
                        open = false;
                        usso.dataGridView1.EndEdit();
                        usso.save();
                        db.SetPIVAsDB(string.Format("UPDATE [dbo].[SynSet] SET [SynStarTime] = '{0}' ,[SynEndTime] = '{1}',[SyncSpaceTime] = '{2}' WHERE [SynCode]='{3}'", USynMinSet.begin, USynMinSet.end, USynMinSet.space, USynMinSet.SynCode).ToString());
                        pictureBox1.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("mulv14");
                        foreach (Control c in this.Parent.Controls)
                        {
                            c.Enabled = true;
                        }
                        usso.Visible = false;
                        switch (Tag.ToString())
                        {
                            case "1":
                                {
                                    label1.Text = "一次";
                                    label2.Text = "每天定时执行一次同步";
                                    label3.Text = "在" + (USynMinSet.begin.Contains(",") ? USynMinSet.begin.Split(',')[0] : USynMinSet.begin) + "点执行";
                                    break;
                                }
                            case "2":
                                {
                                    label1.Text = "时段";
                                    label2.Text = "每天的一个时间段内间隔执行同步";
                                    label3.Text = "间隔" + USynMinSet.space + "分钟执行";
                                    break;
                                }
                            case "3":
                                {
                                    label1.Text = "全天";
                                    label2.Text = "全天间隔执行同步";
                                    label3.Text = "间隔" + USynMinSet.space + "分钟执行";
                                    break;
                                }
                            case "4":
                                {
                                    label1.Text = "手动";
                                    pictureBox1.Visible = false;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        open = true;
                        pictureBox1.BackgroundImage = (Image)SetSyn.Properties.Resources.ResourceManager.GetObject("mulv11");
                        if (Tag.ToString() != "4")
                        {
                            usso.usm = this;
                            usso.BringToFront();
                            usso.Location = new Point(usso.Location.X, this.Location.Y + this.Size.Height - 2);
                            usso.USynMinSetOpen_Load(sender, e);
                            usso.Visible = true;
                            usso.Refresh();
                        }
                        foreach (Control c in this.Parent.Controls)
                        {
                            c.Enabled = false;
                        }
                        usso.Enabled = true;
                        this.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void USynMin_Load(object sender, EventArgs e)
        {
            db = new DB_Help();
            if (USynMinSet.SynTimeCode == Tag.ToString())
            {
                pictureBox2.Visible = true;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
            }
            else
            {
                pictureBox2.Visible = false;
                label1.ForeColor = Color.Silver;
                label2.ForeColor = Color.Silver;
            }
            if (Tag != null)
            {
                switch (Tag.ToString())
                {
                    case "1":
                        {
                            label1.Text = "一次";
                            label2.Text = "每天定时执行一次同步";
                            label3.Text = "在" + (USynMinSet.begin.Contains(",") ? USynMinSet.begin.Split(',')[0] : USynMinSet.begin) + "点执行";
                            break;
                        }
                    case "2":
                        {
                            label1.Text = "时段";
                            label2.Text = "每天的一个时间段内间隔执行同步";
                            label3.Text = "间隔" + USynMinSet.space + "分钟执行";
                            break;
                        }
                    case "3":
                        {
                            label1.Text = "全天";
                            label2.Text = "全天间隔执行同步";
                            label3.Text = "间隔" + USynMinSet.space + "分钟执行";
                            break;
                        }
                    case "4":
                        {
                            label1.Text = "手动";
                            pictureBox1.Visible = false;
                            break;
                        }
                }
            }
        }
    }
}

