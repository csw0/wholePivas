using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace PivasDDrug
{
    public partial class PivasDDrug : Form
    {
        public PivasDDrug()
        {
            InitializeComponent();
        }

        public PivasDDrug(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        DB_Help db = new DB_Help();
        Select select = new Select();
        Update update = new Update();
        public static string DrugCode;
        private string UserID = string.Empty;


        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        //public static DataTable DT = new DataTable();
        //public static SqlDataAdapter SDA = new SqlDataAdapter();

        private void PivasDDrug_Load(object sender, EventArgs e)
        {

            if (UserID == null || UserID == "")
            {
                UserID = "1";
            }
            if (GetPivasLimit.Instance.Limit(UserID, "PivasDDrug"))
            {
                DataSet nm = new DataSet();
                label9.Text = "";
                string str1 = "select DEmployeeName from DEmployee where DEMployeeID = " + UserID + "";
                nm = db.GetPIVAsDB(str1);
                label9.Text = (nm.Tables[0].Rows.Count > 0 ? nm.Tables[0].Rows[0][0].ToString() : "LaennecSystem");
                show();
            }
            else
            {
                this.Dispose();
            }
        }

        private string getDrugType(string _type)
        {
            if ("2".Equals(_type))
                return "抗生素";
            else if ("3".Equals(_type))
                return "化疗药";
            else if ("4".Equals(_type))
                return "营养药";
            else if ("5".Equals(_type))
                return "中成药";
            else
                return "普通药"; 
        }

        private void show()
        {
            try
            {
                Dgv_Druglist.DataSource = null;
                int check;
                DataSet ds = new DataSet();
                if (Cb_IsVaildCheck.Checked)
                {
                    check = 1;
                }
                else
                {
                    check = 0;
                }

                ds = db.GetPIVAsDB(select.DDurg(check, Txt_SName.Text.ToString().Trim()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //3、药品名称 //4、药品简称;//5、规格//6、剂量//7、剂量单位  //9 速查码 //10 货柜号 //11 单位制剂//12 药品耗材
                    Dgv_Druglist.DataSource = ds.Tables[0];
                    Dgv_Druglist.Columns["可见"].Width = 35;   
                    //dataGridView1.Columns[2].Width = 150;
                    Dgv_Druglist.Columns[1].Width = 70; 
                    Dgv_Druglist.Columns[2].Width = 290; 
                    Dgv_Druglist.Columns[3].Visible = false; 
                    Dgv_Druglist.Columns[4].Width = 140;
                    Dgv_Druglist.Columns[5].Width = 60; 
                    Dgv_Druglist.Columns[6].Width = 60; 
                    Dgv_Druglist.Columns[7].Width = 60;
                    Dgv_Druglist.Columns[8].Width = 60;
                    Dgv_Druglist.Columns[10].Width = 110;
                    Dgv_Druglist.Columns[11].Width = 400;
                    Dgv_Druglist.Columns[12].Width = 200;
                    Dgv_Druglist.Columns[13].Width = 100;
                    foreach (DataGridViewRow dgvr in Dgv_Druglist.Rows)
                    {
                        dgvr.Cells[13].Tag = dgvr.Cells[13].Value;
                        dgvr.Cells[13].Value = getDrugType(dgvr.Cells[13].Value.ToString());//.Replace("1", "普通药").Replace("2", "抗生素").Replace("3", "化疗药").Replace("4", "营养药");
                    }
                }
            }
            catch { }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                Panel_Max_None.BackgroundImage = global::PivasDDrug.Properties.Resources.还原;
            
            }
            else
            {
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Panel_Max_None.BackgroundImage = global::PivasDDrug.Properties.Resources.最大化;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //e.RowIndex > -1否则点击header也是叫一列 
            if (Dgv_Druglist.Rows.Count > 0 && Dgv_Druglist.CurrentRow.Index > -1)
            {
                try
                {
                    DrugCode = Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["药品编码"].Value.ToString();
                    UpdateDrug UE = new UpdateDrug();
                   
                    UE.DrugCode = DrugCode;
                    UE.show();
                    //if (Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["单位制剂"].Value != null && Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["单位制剂"].Value != "")
                    //{
                    //    UE.Txt_UniPreparation.Text = Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["单位制剂"].Value.ToString();

                    //}
                    //else
                    //{
                    //    UE.Txt_UniPreparation.Text = "";
                    //} 
                    UE.DG_UniPreparation.Visible = false;
                    DialogResult Dr = UE.ShowDialog();
                    if (Dr == DialogResult.Yes)
                    {
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["单位制剂"].Value = UE.Preparation;
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["剂量"].Value = string.Format("{0:0.##}", UE.Dosage);
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["剂量单位"].Value = UE.DosageUnit;
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["主药剂量"].Value = string.Format("{0:0.##}", UE.Dosage) + UE.DosageUnit;
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["溶媒剂量"].Value = string.Format("{0:0.##}", UE.Capacity) + UE.CapacityUnit;
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["货柜号"].Value = UE.Txt_PortNo.Text;
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["药品耗材"].Value = UE.txt_Consumables.Text;
                        Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["药品属性"].Value = UE.TheDrugType;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Cb_IsVaildCheck_CheckedChanged(object sender, EventArgs e)
        {
              show();
        }
        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.dDrugTableAdapter.Fill(this.pivas2013DataSet.DDrug);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void Dgv_Druglist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
              if (this.Dgv_Druglist.CurrentCell.ColumnIndex == 0)
            {
                //获取DataGridView中CheckBox的Cell
                DataGridViewCheckBoxCell dgvCheck = (DataGridViewCheckBoxCell)(this.Dgv_Druglist.Rows[this.Dgv_Druglist.CurrentCell.RowIndex].Cells[0]);
                DrugCode = DrugCode = Dgv_Druglist.Rows[Dgv_Druglist.CurrentRow.Index].Cells["药品编码"].Value.ToString();
               //根据单击时，Cell的值进行处理。EditedFormattedValue和Value均可以
              
                    //若单击时，CheckBox没有被勾上
                if (Convert.ToBoolean(dgvCheck.EditedFormattedValue) == false)
                {
                    int i = db.SetPIVAsDB(update.DDurg(1, DrugCode));
                    //通过程序完成CheckBox是否勾上的控制
                    dgvCheck.Value = true;
                    //show();
                }
                //若单击时，CheckBox已经被勾上
                else
                {
                    int i = db.SetPIVAsDB(update.DDurg(0, DrugCode));
                    //通过程序完成CheckBox是否勾上的控制
                    dgvCheck.Value = false;
                    //this.Dgv_Druglist.Rows[this.Dgv_Druglist.CurrentCell.RowIndex].Cells[0]);
                    if (Cb_IsVaildCheck.Checked == true)
                    {
                      
                        Dgv_Druglist.Rows.Remove(Dgv_Druglist.Rows[this.Dgv_Druglist.CurrentCell.RowIndex]);
                        //show();
                    }
                }
              
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            //keyboard.CloseFrom();
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);           
        }

        private void label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            //Panel pl = (Panel)sender;
            //pl.BackColor = Color.FromArgb(84, 199, 253);
            Pic_Min.BackgroundImage = global::PivasDDrug.Properties.Resources.最小化按下时按钮;
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackgroundImage = null;
            Pic_Min.BackColor = Color.Transparent ;
            Pic_Min.BackgroundImage = global::PivasDDrug.Properties.Resources.最小化;
        }

        private void Panel_Max_None_MouseHover(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                Panel_Max_None.BackgroundImage = global::PivasDDrug.Properties.Resources.最大化按下时按钮;
            }
            else
            {
                Panel_Max_None.BackgroundImage = global::PivasDDrug.Properties.Resources.还原按下时按钮;
            }
        }

        private void Panel_Max_None_MouseLeave(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                Panel_Max_None.BackgroundImage = global::PivasDDrug.Properties.Resources.最大化;
            }
            else
            {
                Panel_Max_None.BackgroundImage = global::PivasDDrug.Properties.Resources.还原;
            }
            Panel_Max_None.BackColor = Color.Transparent ;
           // Panel_Close.BackgroundImage = global::PivasDDrug.Properties.Resources.最大化;
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = global::PivasDDrug.Properties.Resources.关闭按下时按钮;

        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = null;
            Panel_Close.BackColor = Color.Transparent ;
            Panel_Close.BackgroundImage = global::PivasDDrug.Properties.Resources.关闭;
        }

        private void Txt_SName_TextChanged(object sender, EventArgs e)
        {
            show();
        }
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID"></param>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        //public bool Limit(string DEmployeeID, string LimitName)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        string str = "select * from ManageLimit where DEMployeeID = '" + DEmployeeID + "' and LimitName = '" + LimitName + "'";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
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




    }
}
