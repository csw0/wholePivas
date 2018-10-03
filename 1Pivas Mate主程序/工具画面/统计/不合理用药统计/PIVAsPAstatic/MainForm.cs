using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace PIVAsPAstatic
{
    public partial class MainForm : Form
    {
        DataBase db = new DataBase();
        DB_Help db1 = new DB_Help();
        private string AccountID = "0";
        //private string WardCode = "";
        public DataTable dtPrint; //存放打印的数据
        private string demploy = string.Empty;
        public MainForm(string demployId)
        {
            InitializeComponent();

            //listviewinit1();
            label4.Text = getInfor();
            this.demploy = demployId;
        }
        /// <summary>
        /// 获取当前日期时间段的sql
        /// </summary>
        /// <returns></returns>
        private string getDate()
        {
            string date = "BETWEEN '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000") + "' AND '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59.999") + "'";
            return date;
        }
        /// <summary>
        /// 按药师统计时的界面设置
        /// </summary>
        private void listviewinit1()
        {
            this.listView1.View = View.Details;
            this.listView1.GridLines = false;
            this.listView1.FullRowSelect = true;
            DataSet ds = getItem();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ColumnHeader header1 = new ColumnHeader();
                ColumnHeader header2 = new ColumnHeader();
                ColumnHeader header3 = new ColumnHeader();
                header1.Text = "";
                header2.Text = "医师";
                header3.Text = "总计";
                header1.Width = 1;
                header2.Width = 70;
                header3.Width = 70;
                this.listView1.Columns.Add(header1);
                this.listView1.Columns.Add(header2);
                this.listView1.Columns.Add(header3);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Text = ds.Tables[0].Rows[i][1].ToString();
                    header.Width = 75;
                    this.listView1.Columns.Add(header);
                }
            }
            else
            {
                MessageBox.Show("当前时间段没有数据！");
            }
            ImageList im = new ImageList();
            im.ImageSize = new Size(1, 20);
            this.listView1.SmallImageList = im;
          
        }

        private DataSet getItem()
        {
            DataSet ds = new DataSet();
            if (this.RGTSMI.Checked)
            {
                ds = db.getRGDataItem(getDate());
            }
            else if (this.SysTSMI.Checked)
            {
                ds = db.getSYSDataItem(getDate());
            }
            return ds;
        }

        /// <summary>
        /// 按药师统计结果
        /// </summary>
        /// <param name="type">0是人工审方统计1是系统审方</param>
        private void listviewCountByDoctor(string type)
        {
            DataSet ds1 =getItem();

            DataSet ds = db.getDataCountByDoctor(ds1, type, getDate());
            if (ds != null && ds.Tables.Count > 0)
            {
                this.dtPrint = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(ds.Tables[0].Rows[i][0].ToString().Trim());
                    for (int m = 1; m < ds.Tables[0].Columns.Count; m++)
                    {
                        lv.SubItems.Add(ds.Tables[0].Rows[i][m].ToString().Trim());
                    }
                    this.listView1.Items.Add(lv);
                }
            }
        }
        /// <summary>
        /// 按病区统计的界面设置
        /// </summary>
        private void listviewinit2()
        {
            this.listView1.View = View.Details;
            this.listView1.GridLines = false;
            this.listView1.FullRowSelect = true;
            DataSet ds = getItem();
            
            if (ds.Tables.Count>0&& ds.Tables[0].Rows.Count > 0)
            {
                ColumnHeader header1 = new ColumnHeader();
                ColumnHeader header2 = new ColumnHeader();
                ColumnHeader header3 = new ColumnHeader();
                header1.Text = "";
                header2.Text = "病区";
                header3.Text = "总计";
                header1.Width = 1;
                header2.Width = 140;
                header3.Width = 60;
                this.listView1.Columns.Add(header1);
                this.listView1.Columns.Add(header2);
                this.listView1.Columns.Add(header3);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ColumnHeader header = new ColumnHeader();
                    header.Text = ds.Tables[0].Rows[i][1].ToString();
                    header.Width =75;
                    this.listView1.Columns.Add(header);
                }
            }
            ImageList im = new ImageList();
            im.ImageSize = new Size(1, 20);
            this.listView1.SmallImageList = im;
          
        }
        /// <summary>
        /// 按病区统计的结果
        /// </summary>
        private void listviewCountByWard(string type)
        {
            //string date = getDate();
          
            DataSet ds1 =getItem();

            DataSet ds = db.getDataCountWard(ds1,type,getDate());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.dtPrint = ds.Tables[0];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ListViewItem lv = new ListViewItem(ds.Tables[0].Rows[i][0].ToString().Trim());
                    for (int m = 1; m < ds.Tables[0].Columns.Count; m++)
                    {
                        lv.SubItems.Add(ds.Tables[0].Rows[i][m].ToString().Trim());
                    }
                    this.listView1.Items.Add(lv);
                }
            }
        }

        /// <summary>
        /// 不合理处方明细
        /// </summary>
        private void flowlayoutinit()
        {
            int n = 0;
            string date = getDate();
            DataSet ds = null;
            if (this.demTSMI.Checked && this.RGTSMI.Checked)
                ds = db.getItemByDoctor(AccountID, "0", date);
            else if (this.demTSMI.Checked && this.SysTSMI.Checked)
                ds = db.getItemByDoctor(AccountID, "1", date);
            else if (this.wardTSMI.Checked && this.RGTSMI.Checked)
                ds = db.getItemByWard(AccountID, "0", date);
            else
                ds = db.getItemByWard(AccountID, "1", date);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Label lab = new Label();
                    lab.Text = ds.Tables[0].Rows[i][0].ToString();
                    lab.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    lab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    lab.BackColor = System.Drawing.Color.Yellow;
                    lab.Size = new System.Drawing.Size(this.flowLayoutPanel1.Width-10, 20);
                    this.flowLayoutPanel1.Controls.Add(lab);
                    DataSet das = null;
                    if (this.demTSMI.Checked&&this.RGTSMI.Checked)
                        das = db.getItemDetailByDoctor(AccountID, ds.Tables[0].Rows[i][0].ToString(), date,"0");
                    else if(this.demTSMI.Checked&&this.SysTSMI.Checked)
                        das = db.getItemDetailByDoctor(AccountID, ds.Tables[0].Rows[i][0].ToString(), date, "1");
                    else if(this.wardTSMI.Checked&&this.RGTSMI.Checked)
                        das = db.getItemDetailByWard(AccountID, ds.Tables[0].Rows[i][0].ToString(), date,"0");
                    else 
                        das = db.getItemDetailByWard(AccountID, ds.Tables[0].Rows[i][0].ToString(), date,"1");
                    ListView lv = new ListView();
                    //lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    //            | System.Windows.Forms.AnchorStyles.Left)
                    //            | System.Windows.Forms.AnchorStyles.Right)));
                    lv.View = View.Details;
                    lv.GridLines = true;
                    lv.FullRowSelect = true;
                    if (das.Tables[0].Rows.Count > 5)
                    {
                        n = 150;
                    }
                    else
                    {
                        n = das.Tables[0].Rows.Count * 20 + 50;
                    }
                    lv.Size = new System.Drawing.Size(this.flowLayoutPanel1.Width-10, n);
                    ColumnHeader header6 = new ColumnHeader();
                    ColumnHeader header1 = new ColumnHeader();
                    ColumnHeader header2 = new ColumnHeader();
                    ColumnHeader header3 = new ColumnHeader();
                    ColumnHeader header4 = new ColumnHeader();
                    ColumnHeader header5 = new ColumnHeader();
                    header6.Text = "处方号";
                    header1.Text = "药品";
                    header2.Text = "作用药品";
                    header3.Text = "描述";
                    header4.Text = "来源";
                    header5.Text = "退方人";
                    
                    header6.Width = 70;
                    header5.Width = 50;
                    header1.Width = 125;
                    header2.Width = 120;
                    header3.Width = 200;
                    header4.Width = 70;
                  
                    lv.Columns.Add(header6);
                    lv.Columns.Add(header1);
                    lv.Columns.Add(header2);
                    lv.Columns.Add(header3);
                    lv.Columns.Add(header4);
                    if (this.SysTSMI.Checked)
                    {

                        header1.Width = 135;
                    }
                    else
                    {
                        lv.Columns.Add(header5);
                    }
                  
                  
                    ImageList im = new ImageList();
                    im.ImageSize = new Size(1, 20);
                    lv.SmallImageList = im;
                    for (int m = 0; m < das.Tables[0].Rows.Count; m++)
                    {
                        ListViewItem lvi = new ListViewItem(das.Tables[0].Rows[m][0].ToString().Trim());
                        lvi.SubItems.Add(das.Tables[0].Rows[m][1].ToString().Trim());
                        lvi.SubItems.Add(das.Tables[0].Rows[m][2].ToString().Trim());
                        lvi.SubItems.Add(das.Tables[0].Rows[m][3].ToString().Trim());
                        lvi.SubItems.Add(das.Tables[0].Rows[m][4].ToString().Trim());
                        lv.Items.Add(lvi);
                    }
                    this.flowLayoutPanel1.Controls.Add(lv);
                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
            AccountID = this.listView1.SelectedItems[0].Text;
            flowlayoutinit();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (this.dateTimePicker2.Value < this.dateTimePicker1.Value)
                this.dateTimePicker1.Value = this.dateTimePicker2.Value;
            else
                return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listView1.Clear();
            this.flowLayoutPanel1.Controls.Clear();
            AccountID = "0";
            if (this.demTSMI.Checked)
            {
                listviewinit1();
                if (this.RGTSMI.Checked)
                {
                    listviewCountByDoctor("0");
                }
                else
                {
                    listviewCountByDoctor("1");
                }
            }
            else
            {
                listviewinit2();
                if (this.RGTSMI.Checked)
                {
                    listviewCountByWard("0");
                }
                else
                {
                    listviewCountByWard("1");
                }

            }
            this.flowLayoutPanel1.Controls.Clear();
            //flowlayoutinit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string beginDT = this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000");
            string endDT = this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59.999");
            DataChart dc = new DataChart(getDate());
            dc.Show();
            dc.WindowState = FormWindowState.Normal;
        }

    

        private void SysTSMI_Click(object sender, EventArgs e)
        {
            if (this.SysTSMI.Checked)
            {
                this.SysTSMI.Checked = false;
                this.RGTSMI.Checked =true;
            }
            else
            {
                this.SysTSMI.Checked =true;
                this.RGTSMI.Checked = false;

            }
            this.listView1.Items.Clear();
            this.flowLayoutPanel1.Controls.Clear();
            label4.Text = getInfor();
        }

        private void ygtjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.demTSMI.Checked)
            {
                this.demTSMI.Checked = false;
                this.wardTSMI.Checked = true;
            }
            else
            {
                this.demTSMI.Checked = true;
                this.wardTSMI.Checked = false;
            }
            this.listView1.Clear();
            //this.listView1.Items.Clear();
            this.flowLayoutPanel1.Controls.Clear();
            label4.Text = getInfor();
        }

        private void wdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Information inf = new Information();
            inf.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DgvToExcel dgvToExcel = new DgvToExcel();
            dgvToExcel.BindDgv_Excel(this.dtPrint);
  
          
        }

        private string getInfor()
        {
            string s = string.Empty;
            if (this.SysTSMI.Checked && this.demTSMI.Checked)
            {
                s = "系统审方不合理用药按医师统计";
            }
            else if (this.SysTSMI.Checked && this.wardTSMI.Checked)
            {
                s = "系统审方不合理用药按病区统计";
            }
            else if (this.RGTSMI.Checked && this.demTSMI.Checked)
            {
                s = "人工审方不合理用药按医师统计";
            }
            else
            {
                s = "人工审方不合理用药按病区统计";
            }
            return s;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            this.label5.BackColor = Color.Yellow;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            this.label5.BackColor = Color.YellowGreen;
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

        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GetPivasLimit.Instance.Limit(demploy, "PivasSafe"))
                {
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

     
      
    }
}
