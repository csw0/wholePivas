using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace DWardManage
{
    /// <summary>
    /// 病区字典表维护画面
    /// 陆卓春  2013-08-20  新规
    /// 顾  甡  2013-08-22  改修
    /// </summary>
    public partial class PivasDWard : Form
    { 
        private seldb seldb = new seldb();
        private DataSet DWard;
        private DataTable dt;
        private bool isalter;
        private string gz = "";
        private int sy = 4;
        private string UserID = string.Empty;
        private DB_Help db = new DB_Help();    
        public PivasDWard()
        {
            InitializeComponent();
        }

        public PivasDWard(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        /// <summary>
        /// 画面拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

        #region ********************************自动获取药品拼音码***************************************
        /// <summary>
        /// 获取首字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            { myStr += getSpell(strText.Substring(i, 1)); }
            return myStr;
        }

        public string getSpell(string cnChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25)
                        max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                } return "*";
            }
            else return cnChar;
        }
        #endregion  ********************************自动获取药品拼音码***************************************


        /// <summary>
        /// 画面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PivasDWard_Load(object sender, EventArgs e)
        {

            Pic_Min.Parent = this.panel3;
            Panel_Close.Parent = this.panel3;
            if (UserID == null || UserID == "")
            {
                UserID = "00000000000000000000000000000000000000";
            }
            if (GetPivasLimit.Instance.Limit(UserID, "PivasDWard"))
            {

                DataSet nm = new DataSet();
                label9.Text = "";
                DataSet dss = db.GetPIVAsDB("SELECT * FROM [DWard]");
                if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                {
                    if (dss.Tables[0].Columns.Contains("Spellcode"))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [DWard] where Spellcode is null or Spellcode =''", db.DatebasePIVAsInfo());
                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                dr["Spellcode"] = GetChineseSpell(dr["WardName"].ToString());
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataTable dt = ds.Tables[0].GetChanges(DataRowState.Modified);
                                adapter.Update(dt);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("本数据库中没有病区简拼字段");
                    }
                }

                isalter = true;
                addcontrols();
                Load_panel4();

            }
            else
            {
                MessageBox.Show("您没有操作权限，请与管理员联系！");
                this.Dispose();
            }

        }

        ///// <summary>
        ///// 权限
        ///// </summary>
        ///// <param name="DEmployeeID"></param>
        ///// <param name="LimitName"></param>
        ///// <returns></returns>
        //public bool Limit(string DEmployeeID, string LimitName)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        string str = "select * from ManageLimit where DEMployeeID = " + DEmployeeID + " and LimitName = '" + LimitName + "'";
        //        //MessageBox.Show(str);
        //        ds = db.GetPIVAsDB(str);

        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            return true;
        //        }
        //        ds.Dispose();
        //        return false;
               

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 界面初始化
        /// </summary>
        private void addcontrols()
        {
            string kf = "";
            if (label4.Text == "开放病区   " + comboBox2.Text)
            {
                kf = "True";
            }
            else if (label4.Text == "未开放病区   " + comboBox2.Text)
            {
                kf = "False";
            }
            
            string fz = comboBox2.Text;
            // sy = comboBox1.SelectedIndex;
            switch(sy)
            {
                case  0 :
                    gz = "Wardcode"; break;
                case  1 :
                    gz = "WardSeqNo"; break;
                case  2 :
                    gz = "WardArea"; break;
                default:
                    gz = "";
                    break;
            }
            if (label10.Text == "")
            {
                DWard = new DataSet();
                DWard = seldb.getDWard(kf, fz, gz);
                showdata(DWard);
            }
            else 
            {
                 DWard = new DataSet();
                 DWard = seldb.getVagueSelect(label10.Text.ToString(),fz);
                 showdata(DWard);
            }
        }
  
        /// <summary>
        /// 加载用户控件
        /// </summary>
        /// <param name="DWard"></param>
        private void showdata(DataSet DWard)
        {
            if (DWard == null || DWard.Tables.Count == 0)
                return;
            DataTable dt = DWard.Tables[0];
            dgvWard.DataSource = dt;

            //title title=new title();
            //panel5.Controls.Add(title);
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public void refresh() 
        {
            //panel5.Controls.Clear();
            addcontrols();
            this.ActiveControl = dgvWard;
        }

        /// <summary>
        /// 排序（暂不使用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refresh();
            //panel5.Focus();
        }
        
        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (panel4.Visible == true)
            {
                panel4.Visible = false;
            }
            else
            {
                panel4.Visible = true;
            }

        }

        /// <summary>
        /// 筛选保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            label4.Text = string.Empty;
            if (radioButton1.Checked == true)
            {
                label4.Text =  label4.Text +"开放病区";
            }
            if (radioButton2.Checked == true)
            {
                label4.Text = label4.Text + "未开放病区";
            }

            label4.Text = label4.Text + "   " + comboBox2.Text;
            panel4.Visible = false;
            label10.Text = textBox1.Text;
            refresh();
        }

      
        private void Load_panel4() 
        {
            dt = new DataTable();
            dt = seldb.getWardArea().Tables[0];
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "WardArea";
            comboBox2.SelectedIndex = -1; 
        }
 
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* private void button3_Click(object sender, EventArgs e)
        {
            refresh();
        }*/


        /// <summary>
        /// 刷新单行
        /// </summary>
        /// <param name="i"></param>
        /// <param name="k"></param>
        public void ddrefresh(string i,string k) 
        {
            //foreach(Control c in panel5.Controls ) 
            //{
            //    if (c.Name == k.ToString()) 
            //    { 
            //    panel5.Controls.Remove(c);
            //    Point p = c.Location;
            //    dt = seldb.getDWardid(i.ToString()).Tables[0];
            //    int I = Convert.ToInt32(k);
            //    rowDWard row;
            //    row = new rowDWard();
            //    row.add(dt.Rows[0], isalter, k);
            //    row.Top = p.Y;
            //    row.Name = k.ToString();
            //    panel5.Controls.Add(row);
            //    this.ActiveControl = this.panel5;
            //    }
            //} 
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        //private void panel4_Paint(object sender, PaintEventArgs e)
        //{
        //    ControlPaint.DrawBorder(e.Graphics,
        //                                panel4.ClientRectangle,
        //                                Color.FromArgb(19, 179, 253),
        //                                1,
        //                                ButtonBorderStyle.Solid,
        //                                Color.FromArgb(19, 179, 253),
        //                                1,
        //                                ButtonBorderStyle.Solid,
        //                                Color.FromArgb(19, 179, 253),
        //                                1,
        //                                ButtonBorderStyle.Solid,
        //                                Color.FromArgb(19, 179, 253),
        //                                1,
        //                                ButtonBorderStyle.Solid);
        //}

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.Transparent;
         
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor =  Color.Transparent;
     
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                sy = 0;
                pictureBox2.Visible = true;
                pictureBox1.Visible = false;
                refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        { 
                sy = 4;
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
                refresh();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(255, 255, 192);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.FromArgb(179, 218, 91);
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(255, 255, 192);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(179, 218, 91);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = string.Empty;
            if (radioButton1.Checked == true)
            {
                label4.Text = label4.Text + "开放病区";
            }
            else if (radioButton2.Checked == true)
            {
                label4.Text = label4.Text + "未开放病区";
            }
            else
            {
                label4.Text = string.Empty;
            }
            label4.Text = label4.Text + "   " + comboBox2.Text;
            label10.Text = textBox1.Text;
            refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                textBox1.Text = "";
                label4.Text = string.Empty;
                label4.Text = label4.Text + "开放病区";
                label4.Text = label4.Text + "   " + comboBox2.Text;
                label10.Text = textBox1.Text;
                refresh();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                 textBox1.Text = "";
                label4.Text = string.Empty;
                label4.Text = label4.Text + "未开放病区";
                label4.Text = label4.Text + "   " + comboBox2.Text;
                label10.Text = textBox1.Text;
                refresh();
            }
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    //label4.Text = label4.Text + "   " + comboBox2.Text;
        //    //label10.Text = textBox1.Text;
        //    //refresh();
        //     label4.Text = string.Empty;
        //     radioButton1.Checked = false;
        //     radioButton2.Checked = false;
        //    //if (radioButton1.Checked == true)
        //    //{
        //    //    label4.Text = label4.Text + "开放病区";
        //    //}
        //    //else if (radioButton2.Checked == true)
        //    //{
        //    //    label4.Text = label4.Text + "未开放病区";
        //    //}
        //    //else
        //    //{
        //    //    label4.Text = string.Empty;
        //    //}
        //    label4.Text = label4.Text + "   " + comboBox2.Text;
        //    label10.Text = textBox1.Text;
        //    refresh();
            
        //}

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                label4.Text = string.Empty;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                label4.Text = label4.Text + "   " + comboBox2.Text;
                label10.Text = textBox1.Text;
                refresh();
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                label4.Text = comboBox2.Text;
                label10.Text = textBox1.Text;
                refresh();
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

     

        private void dgvWard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x=e.RowIndex;
            int y=e.ColumnIndex;
            Point p = new Point(Control.MousePosition.X, Control.MousePosition.Y);

            if (x <0)
                return;

            switch(y)
            {
                case 3:UpdatePxh update = new UpdatePxh(dgvWard.Rows[x]); //排序
                        update.Location = p;
                        update.ShowDialog(this);
                        break;
                case 2: UpdateJC update2 = new UpdateJC(dgvWard.Rows[x], y);//简称
                        update2.Location = p;
                        update2.ShowDialog(this);
                        break;
                case 6: UpdateJC update6 = new UpdateJC(dgvWard.Rows[x], y);//简拼
                        update6.Location = p;
                        update6.ShowDialog(this);
                        break;
                case 4: UpdateFZ update4 = new UpdateFZ(dgvWard.Rows[x]);//分组
                        update4.Location = p;
                        update4.ShowDialog(this);
                        break;
                case 5: dgvWard.Rows[x].Cells[y].Value = dgvWard.Rows[x].Cells[y].Value.ToString()!="True" ? true : false;
                        db.SetPIVAsDB("update DWard set IsOpen='" + dgvWard.Rows[x].Cells[y].Value.ToString() + "' where WardCode='" + dgvWard.Rows[x].Cells["WardCode"].Value.ToString() + "' ");
                        break;
                case 7: UpdateLM update5 = new UpdateLM(dgvWard.Rows[x]);
                        update5.Location = p;
                        update5.Size = new Size(80, 36);
                        update5.ShowDialog(this);
                        break;
                default: break;
            }
          
            
        }
    }
}
