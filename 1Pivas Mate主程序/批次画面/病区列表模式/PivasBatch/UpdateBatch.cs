using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatch
{
    public delegate void DelegateChangeTextValS(string TextVal, int tags, int Issame, string Labelno, string batch,string batchrule);
    public partial class UpdateBatch : Form
    {
        public UpdateBatch()
        {
            InitializeComponent();
        }

        public UpdateBatch(string NowStatus)
        {
            InitializeComponent();
            this.NowStatus = NowStatus;
        }
        
        //状态 已发送，未发送，以打印 三种
        string NowStatus = "";
        SelectSql sql = new SelectSql();
        DB_Help db = new DB_Help();
        public string Groupno = "";
       // public string BatchTags = string.Empty;
        //瓶签号
        string LabelNo;
        //批次
        string K_U = "";
        //批次（1，2，3，4）
        string Team = "";
        //string Pcode;
        //批次(1#,1-K)
        string Batch;
        //病人信息
        public string patcode;
        public int tags, IsSame;
        UpdateSql update = new UpdateSql();
        InsertSql insert = new InsertSql();
        int ChangeRow = 0;        

        /// <summary>
        /// 改批次原因LIST
        /// </summary>
        List<string> Data = new List<string>();

        bool reasonclick = false;

        private string reason = string.Empty;
        string reasonDetail = string.Empty;
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        // 定义委托事件  
        public event DelegateChangeTextValS ChangeTextVal;
        public void sert(string Groupno)
        {
            try
            {
                OFF = false;
                //鼠标相对于屏幕的坐标
                this.Groupno = Groupno;
                Rectangle s = Screen.PrimaryScreen.WorkingArea;
                int x = 0;
                int y = 0;
                if (Control.MousePosition.X + this.Width < s.Width)
                {
                    x = Control.MousePosition.X;
                }
                else
                {
                    x = Control.MousePosition.X - this.Width;
                }
                if (Control.MousePosition.Y + this.Height < s.Height)
                {
                    y = Control.MousePosition.Y;
                }
                else
                {
                    y = Control.MousePosition.Y - this.Height;
                }
                this.Location = new Point(x, y);
                
                DataSet ds = db.GetPIVAsDB(sql.INFO(Groupno, pvb.datetime.ToString("yyyyMMdd")));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //根据瓶签号排序
                    DataTable dd = ds.Tables[0];
                    DataView dw = dd.DefaultView;
                    dw.Sort = " labelNo ";

                    
                    Dgv_Info.DataSource = dw;

                    //显示已打印未打印
                    for (int i = 0; i < Dgv_Info.Rows.Count; i++)
                    {
                        if (Dgv_Info.Rows[i].Cells["IVstatus"].Value.ToString().Trim().Equals("1"))
                        {
                            Dgv_Info.Rows[i].Cells[1].Tag = "1";

                            Dgv_Info["图片列", i].Value = PivasBatch.Properties.Resources.打印1;
                            enbaled = false;
                        }
                        else
                        {
                            Dgv_Info.Rows[i].Cells[1].Tag = "0";
                            enbaled = true;
                        }
                    }
                }

                Dgv_Info.Columns[1].Width = 5;
                Dgv_Info.Columns["StatasString"].Width = 50;
                Dgv_Info.Columns["FreqName"].Width = 100;
                Dgv_Info.Columns["labelNo"].Width = 120;
                Dgv_Info.Columns["IVstatus"].Visible = false;
                Dgv_Info.Columns["BatchSaved"].Visible = false;
                Batch = Dgv_Info.Rows[0].Cells["batch"].Value.ToString().Trim();
                DataSet dds = db.GetPIVAsDB(sql.DOrder());
                //加载批次
                for (int i = 0; i < Fpanel_set.Controls.Count; i++)
                {
                    string labeltext = ((Label)(Fpanel_set.Controls[i])).Text.Trim();
                    if (dds.Tables[0].Select(" OrderID='" + labeltext + "'").Length <= 0)
                    {
                        Fpanel_set.Controls.Remove(Fpanel_set.Controls[i]);
                        i--;
                    }
                }
                Label_BorderShow();
                X = this.Location.X;
                y = this.Location.Y;
                OFF = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        bool enbaled = true;
        /// <summary>
        /// 加载批次的边框
        /// </summary>
        private void Label_BorderShow()
        {

         
            //if (pvb.IvBatchSaved == 0)
            //{
            //    enbaled = true;
            //}
            //else
            //{
            //    enbaled = false;
            //}

            //加载批次的边框
            foreach (Label la in Fpanel_set.Controls)
            {
                if (Batch.IndexOf(la.Text) >= 0)
                {
                    la.BackgroundImage = (Image)PivasBatch.Properties.Resources.ResourceManager.GetObject("K_L");
                }
                else
                {
                    la.BackgroundImage = null;
                }
                la.Enabled = enbaled;
            }

            foreach (Label la in Panel_K_L.Controls)
            {
                if (Batch.IndexOf(la.Text) >= 0)
                {
                    la.BackgroundImage = (Image)PivasBatch.Properties.Resources.ResourceManager.GetObject("K_L");
                }
                else
                {
                    la.BackgroundImage = null;
                }

                la.Enabled = enbaled;
            }
            if (Label_L.BackgroundImage != null)
            {
                Label_L.BackgroundImage = null;
                Label_L.BackColor = Color.Gold;
            }
            else
            {
                Label_L.BackColor = Color.PaleGoldenrod;
            }
        }


        /// <summary>
        /// 点击K，#
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                if (NowStatus == "已打印")
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("此瓶签为已打印瓶签，确定修改批次？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        timer1.Enabled = true;
                        return;
                    }
                }
                else if (NowStatus == "已发送")
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("此瓶签为已发送瓶签，确定修改批次？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        timer1.Enabled = true;
                        return;
                    }
                }

             
                string oldBatch = "";
                string newBatch = string.Empty;
                //if (Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString().Equals("0"))
                //{
                    pvb.operate = true;
                    if (Dgv_Info.SelectedRows != null && Dgv_Info.SelectedRows.Count > 0)
                    {
                        Team = Dgv_Info.SelectedRows[0].Cells["TeamNumber"].Value.ToString();
                        oldBatch = Dgv_Info.SelectedRows[0].Cells["batch"].Value.ToString();
                        LabelNo = Dgv_Info.SelectedRows[0].Cells["LabelNo"].Value.ToString();
                    }
                    else
                    {
                        Team = Dgv_Info.Rows[0].Cells["TeamNumber"].Value.ToString();
                        oldBatch = Dgv_Info.Rows[0].Cells["batch"].Value.ToString();
                        LabelNo = Dgv_Info.Rows[0].Cells["LabelNo"].Value.ToString();
                    }
                    Label Label_UKL = (Label)sender;
                    K_U = Label_UKL.Text;
                    string empName = pvb.DEmployeeName;
                    string emp = pvb.DEmployeeID;

                    string batchrule = "";
                    if (!K_U.Trim().Equals("L"))
                    {
                        newBatch = oldBatch.Substring(0, oldBatch.IndexOf(Team) + 1) + (K_U.Equals("K") ? "-K" : "#");
                    }
                    else
                    {
                        newBatch = oldBatch.IndexOf("L-") >= 0 ? oldBatch.Replace("L-", "") : "L-" + newBatch;
                    }

                    if (textBox2.Visible == true && (textBox2.Text == "" || textBox2.Text == "修改原因"))
                    {
                        MessageBox.Show("请填写修改原因");
                        return;
                    }

                    if (comboBox1.Visible == true && comboBox1.SelectedItem.ToString() == "请选择修改原因")
                    {
                        reason = "未选择修改原因";
                    }
                    else if (comboBox1.Visible == true && comboBox1.SelectedItem.ToString() != "请选择修改原因")
                    {
                        reason = comboBox1.SelectedItem.ToString();
                    }
                    else
                    {
                        reason = textBox2.Text;
                    }
                    if (textBox1.Text != "原因说明" && textBox1.Text.ToString() != "")
                    {
                        reasonDetail = textBox2.Text;
                    }
                    else
                    {
                        reasonDetail = null;
                    }
                    int i = db.SetPIVAsDB(update.IVRecordBatch(LabelNo, oldBatch, newBatch, int.Parse(Team), empName,ref batchrule));
                    if (i > 0)
                    {
                        db.SetPIVAsDB(insert.OrderChangelog(LabelNo, emp, empName, oldBatch, newBatch, Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString(),reason,reasonDetail));
                        this.DialogResult = DialogResult.Yes;
                        Team = "";
                        ChangeTextVal(patcode, tags, IsSame, LabelNo, newBatch,batchrule);
                        this.Close();
                    }
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        
        private void Label_TeamNumber_MouseMove(object sender, MouseEventArgs e)
        {
            Label Label_UKL = (Label)sender;
            if (!Label_UKL.Text.Equals("L"))
            {
                if (Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString().Equals("0"))
                {

                    Label_UKL.BackgroundImage = (Image)PivasBatch.Properties.Resources.ResourceManager.GetObject("K_L");
                }
            }
            else
            {
                if (Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString().Equals("0"))
                {
                    Label_UKL.BackColor = Color.Gold;
                }
            }
        }

        private void Label_TeamNumber_MouseLeave(object sender, EventArgs e)
        {   
            Label Label_UKL = (Label)sender;
             if (!Label_UKL.Text.Equals("L"))
            {
                if (Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString().Equals("0"))
                {

                    if (Batch.IndexOf(Label_UKL.Text) < 0)
                    {
                      Label_UKL.BackgroundImage = null;
                    }
                }
            }
            else
            {
                if (Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString().Equals("0"))
                {
                    Label_UKL.BackColor = Color.PaleGoldenrod;
                }
            }

        }

        /// <summary>
        /// 选中瓶签修改批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                pvb.operate = true;
                Label_BorderShow();
                Team = Dgv_Info.Rows[e.RowIndex].Cells["TeamNumber"].ToString();
                LabelNo = Dgv_Info.Rows[e.RowIndex].Cells["LabelNo"].Value.ToString();
                Batch = Dgv_Info.Rows[e.RowIndex].Cells["batch"].Value.ToString();
                Label_BorderShow();
                ChangeRow = e.RowIndex;

            }
        }

        /// <summary>
        /// 点击批次。（1.2.3）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label10_Click(object sender, EventArgs e)
        {
            try
            {
                if (NowStatus == "已打印")
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("此瓶签为 <已打印> 瓶签，确定修改批次？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        timer1.Enabled = true;
                        return;
                    }
                }
                else if (NowStatus == "已发送")
                {
                    timer1.Enabled = false;
                    if (MessageBox.Show("此瓶签为 <已发送> 瓶签，确定修改批次？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        timer1.Enabled = true;
                        return;
                    }
                }

                //if (Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString().Equals("0"))
                //{
                    pvb.operate = true;
                  
                    string oldBatch = "";
                    string newBatch = "";
                    if (Dgv_Info.SelectedRows != null && Dgv_Info.SelectedRows.Count > 0)
                    {
                        oldBatch = Dgv_Info.SelectedRows[0].Cells["batch"].Value.ToString();
                        Team = Dgv_Info.SelectedRows[0].Cells["TeamNumber"].Value.ToString();
                        LabelNo = Dgv_Info.SelectedRows[0].Cells["LabelNo"].Value.ToString();
                    }
                    else
                    {
                        oldBatch = Dgv_Info.Rows[0].Cells["batch"].Value.ToString();
                        Team = Dgv_Info.Rows[0].Cells["TeamNumber"].Value.ToString();
                        LabelNo = Dgv_Info.Rows[0].Cells["LabelNo"].Value.ToString();
                    }

                    Label Label_UKL = (Label)sender;
                    K_U = Label_UKL.Text;
                    string empName = pvb.DEmployeeName;
                    string emp = pvb.DEmployeeID;
                    string batchrule = "";

                    string L = "";
                    string K = "";

                    if (oldBatch.IndexOf("L-") < 0)
                    {
                        L = "";
                    }
                    else
                    {
                        L = "L-";
                    }
                    if (oldBatch.IndexOf("-K") < 0)
                    {
                        K = "#";
                    }
                    else
                    {
                        K = "-K";
                    }

                    newBatch = L + K_U + K;

                    if (textBox2.Visible == true && (textBox2.Text == "" || textBox2.Text == "修改原因"))
                    {
                        MessageBox.Show("请填写修改原因");
                        return;
                    }

                    if (comboBox1.Visible == true && comboBox1.SelectedItem.ToString() == "请选择修改原因")
                    {
                        reason = "未选择修改原因";
                    }
                    else if (comboBox1.Visible == true && comboBox1.SelectedItem.ToString() != "请选择修改原因")
                    {
                        reason = comboBox1.SelectedItem.ToString();
                    }
                    else
                    {
                        reason = textBox2.Text;
                    }
                    if (textBox1.Text != "原因说明" && textBox1.Text.ToString() != "")
                    {
                        reasonDetail = textBox2.Text;
                    }
                    else
                    {
                        reasonDetail = null;
                    }
                    //Batch = Batch.Replace(Team,K_U);
                    int i = db.SetPIVAsDB(update.IVRecordBatch(LabelNo, oldBatch, newBatch, int.Parse(K_U), empName, ref batchrule));
                    if (i > 0)
                    {
                        db.SetPIVAsDB(insert.OrderChangelog(LabelNo, emp, empName, oldBatch, newBatch, Dgv_Info.Rows[ChangeRow].Cells["IVstatus"].Value.ToString(),reason,reasonDetail));
                        this.DialogResult = DialogResult.Yes;
                        Team = "";
                        ChangeTextVal(patcode, tags, IsSame, LabelNo, newBatch,batchrule);
                        this.Close();
                    }
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("1");
            }
        }
        int X;
        int Y;
        bool OFF = false;
        /// <summary>
        /// 过时间关掉此界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //使用
            //myMutex.WaitOne();
            if (OFF)
            {
                OFF = false;
                if ((Control.MousePosition.X < X || Control.MousePosition.Y < Y))
                {
                    //if (cbbReason.DroppedDown)
                    //{
                    //    return;
                    //}
                    this.Close();
                }
                OFF = true;
            }
            //myMutex.ReleaseMutex();
        }

        private void UpdateBatch_MouseDown(object sender, MouseEventArgs e)
        {
            OFF = false;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            X = this.Location.X;
            Y = this.Location.Y;
            OFF = true;
        }        
        

        

        private void textBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateBatch_Load(object sender, EventArgs e)
        {
            comboBox1.IntegralHeight = false;
            textBox2.Visible = false;
            AddReason();
        }
        /// <summary>
        /// 添加原因类
        /// </summary>
        private void AddReason()
        {
            string sql = " select distinct  Reason  from OrderChangeLog  where Reason not like '%打印画面修改%'";
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string reason = ds.Tables[0].Rows[i]["Reason"].ToString();
                 
                    comboBox1.Items.Add(reason);
                }
               
            }
            comboBox1.SelectedIndex = 0;
        }

        



        private void textBox1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "修改原因")
            {
                textBox1.SelectAll();
            }
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            reasonclick = false;
        }

      

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true)
            {
                comboBox1.Visible = false;
                textBox2.Visible = true;
            }
            else
            {
                comboBox1.Visible = true;
                textBox2.Visible = false;
            }

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "原因说明")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "原因说明";
                textBox1.ForeColor = Color.Gray;

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "修改原因";
                textBox2.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "修改原因")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;

            }

        }

    }
}
