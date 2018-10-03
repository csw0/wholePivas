using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace ConsumablesStatic
{
    public partial class MainForm : Form
    {
        DB_Help db = new DB_Help();
        private string demploy="";
        private bool locked;
        private DataTable dtprint;
        public MainForm(string demployId)
        {
            InitializeComponent();
            this.demploy = demployId;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!GetPivasLimit.Instance.Limit(demploy, "ConsumablesStatic"))
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 比较日期大小
        /// </summary>
        private bool compareDate()
        {
            DateTime t1, t2;
            t1 = Convert.ToDateTime(dateTimePicker1.Text);
            t2 = Convert.ToDateTime(dateTimePicker2.Text);
            if (t1 > t2)
            {
                return false;

            }
            return true;

        }
        private void GetConsum()
        {
            string ischarge = string.Empty;
            if (string.IsNullOrEmpty(db.IniReadValuePivas("ConsumableIsCharge", "ChargeValue")))
            {
                db.IniWriteValuePivas("ConsumableIsCharge", "ChargeValue", "-1");
                ischarge = "-1";
            }
            else
            {
                ischarge = db.IniReadValuePivas("ConsumableIsCharge", "ChargeValue");
            }
            string sql = string.Format("exec bl_ConsumablesStatistics '{0}','{1}',{2}"
                , dateTimePicker1.Value.ToString("yyyy-MM-dd")+" 00:00:00", dateTimePicker2.Value.ToString("yyyy-MM-dd")+" 23:59:00",ischarge );
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                dtprint = ds.Tables[0];


                foreach (DataRow dr in dtprint.Rows)
                {
                    int a = 0;
                    for (int i = 3; i < dr.ItemArray.Length; i++)
                    {
                        a = a + Convert.ToInt32(dr[i]);
                    }
                    dr["总计"] = a;
                }

              
              
            }
            else
            {
                if (dtprint != null)
                {
                    dtprint.Rows.Clear();
                }
                MessageBox.Show("此段时间没有耗材数据!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!compareDate())
            {
                MessageBox.Show("请重新选择统计时间段");
                return;
            }
          
              GetConsum();
              panel3.Controls.Clear();
              consumTable ct = new consumTable(dtprint);
              panel3.Controls.Add(ct);
          
        }
        #region 移动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion 

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

       

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Plum;
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Set s = new Set();
            s.ShowDialog();
        }

        #region 拖动改变窗体大小
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero;//默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion

        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.Plum;
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.Transparent;
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState =FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            DataGridViewToExcel(dtprint);
        }

        private void DataGridViewToExcel(DataTable dgv)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "2003-2007 Execl files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = dlg.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                string columnTitle = "";
                try
                {
                    //写入列标题
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        if (i > 0)
                        {
                            columnTitle += "\t";
                        }
                        columnTitle += dgv.Columns[i].ColumnName;
                    }
                    sw.WriteLine(columnTitle);

                    //写入列内容
                    for (int j = 0; j < dgv.Rows.Count; j++)
                    {
                        string columnValue = "";
                        for (int k = 0; k < dgv.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                columnValue += "\t";
                            }
                            if (string.IsNullOrEmpty( dgv.Rows[j][k].ToString()))
                                columnValue += "";
                            else
                                columnValue += dgv.Rows[j][k].ToString().Trim();
                        }
                        sw.WriteLine(columnValue);
                    }
                    sw.Close();
                    myStream.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Controls.Clear();
            Print p = new Print(dtprint);
            p.Dock = DockStyle.Fill;
            panel3.Controls.Add(p);
        }
       
    }
}
