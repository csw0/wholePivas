using PIVAsCommon.Helper;
using PivasLimitDES;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EmployeeManage
{
    public partial class FormEmployee : Form
    {
        private DB_Help db = new DB_Help();
        private DataSet ds = new DataSet();
       
        protected internal static string AccountID;
        protected internal static string Pas;
        protected internal static string ID;
        protected internal static string Position;
        protected internal static string DEmployeeName;
        protected internal static string DEmployeeCode;
        protected internal static string Isvalid;
        protected internal static string EType;
        protected internal static int CIndex;

        public FormEmployee()
        {
            InitializeComponent();
            UserID = "";
        }
        public FormEmployee(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private string UserID=string.Empty;
        #endregion
      
        public void Form1_Load(object sender, EventArgs e)
        {

            if (UserID == null || UserID =="")
            {
                MessageBox.Show("  请从正常途径登录本画面！ ");
                this.Dispose();
            }
            else if (GetPivasLimit.Instance.Limit(UserID, "PivasDEmployee"))
            {
                DataSet nm = new DataSet();
                label5.Text = "";
                string str1 = "select DEmployeeName from DEmployee where DEMployeeID = " + UserID + "";
                nm = db.GetPIVAsDB(str1);
                label5.Text = nm.Tables[0].Rows[0][0].ToString();
                addEmployee();
            }
            else
            {
              //  MessageBox.Show("您没有操作权限，请与管理员联系！");
                this.Dispose();
            }
        }
     
        private void panel1_click(object sender, EventArgs e)
        {
            this.Close();
        }      
      
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
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
        //刷新界面
        public void refresh() 
        {           
            addEmployee();
        }

        private void addEmployee()//初始化员工信息
        {
            try
            {
                string str = "select AccountID,pas,DEmployeeName,DEmployeeCode,Position,IsValid,'',DEmployeeID,type from DEmployee ";
                if (checkBox1.Checked)
                { str = str + "  where IsValid = 1  "; }
                ds = db.GetPIVAsDB(str);
                dgvEmployee.DataSource = ds.Tables[0];               
                dgvEmployee.Columns[2].DefaultCellStyle.NullValue = "**********";
                dgvEmployee.Columns[2].Width = 120;
                dgvEmployee.Columns[3].Width = 120;
                dgvEmployee.Columns[4].Visible = false; 
                dgvEmployee.Columns[5].Width = 120;
                dgvEmployee.Columns[6].Width = 120;
                dgvEmployee.Columns[7].Width = 120;
                dgvEmployee.Columns[8].Width = 65;
                dgvEmployee.Columns[9].Width = 25;
                dgvEmployee.Columns[10].Visible = false;
                dgvEmployee.Columns[0].Visible = true;
                dgvEmployee.Columns[1].Visible = false;
                dgvEmployee.RowsDefaultCellStyle.ForeColor = Color.Black;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)//显示增加员工信息对话框
        {
            new AddEmployee().ShowDialog(this);
        }
        private void dgvEmployee_Click(object sender, EventArgs e)//单击，主要是删除和员工状态的改变
        {
            try
            {
                string str, p;
                AccountID = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["AccountID"].Value.ToString();
                Position = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Position"].Value.ToString();
                DEmployeeCode = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeCode"].Value.ToString();
                DEmployeeName = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeName"].Value.ToString();
                ID = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeID"].Value.ToString();
                Isvalid = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["IsValid"].Value.ToString();
                EType = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Type"].Value.ToString();

                p = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Pas"].Value.ToString();
                if (p != "")
                    //Pas = db.Decrypt(p);
                    Pas = p;
                CIndex = dgvEmployee.CurrentCell.ColumnIndex;
                if (CIndex == 0)
                {
                    deleteEmployee DE = new deleteEmployee();
                    if (DE.ShowDialog(this) == DialogResult.OK)
                    {
                        str = "delete from DEmployee where DEmployeeID = " + ID;
                        db.SetPIVAsDB(str);
                        dgvEmployee.Rows.RemoveAt(dgvEmployee.CurrentRow.Index);
                    }
                }
                if (CIndex == 8)
                {
                    string v = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["IsValid"].Value.ToString();
                    switch (v)
                    {
                        case "True":
                            v = "False";
                            break;
                        case "False":
                            v = "True";
                            break;
                    }
                    str = "update DEmployee SET IsValid = '" + v + "' where DEmployeeID = " + ID;
                    dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["IsValid"].Value = v;
                    db.SetPIVAsDB(str);
                }
                /*   else
                   {
                       updateEmployee UE = new updateEmployee();
                       if (UE.ShowDialog() == DialogResult.OK)
                       {
                           dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["AccountID"].Value = updateEmployee.uAccountID;
                           dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Pas"].Value = updateEmployee.uPas;
                           dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Position"].Value = updateEmployee.uPosition;
                           dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeName"].Value = updateEmployee.uDEmployeeName;
                           dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeCode"].Value = updateEmployee.uDEmployeeCode;
                           dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["IsValid"].Value = updateEmployee.uIsvalid;
                       }
                   }*/
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEmployee_DoubleClick(object sender, EventArgs e)//弹出修改画面
        {
            try
            {
                string p;
                AccountID = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["AccountID"].Value.ToString();
                Position = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Position"].Value.ToString();
                DEmployeeCode = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeCode"].Value.ToString();
                DEmployeeName = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeName"].Value.ToString();
                ID = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeID"].Value.ToString();
                Isvalid = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["IsValid"].Value.ToString();
                EType = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Type"].Value.ToString();

                p = dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Pas"].Value.ToString();
                if (p != "")
                    // Pas = db.Decrypt(p);
                    Pas = p;
                CIndex = dgvEmployee.CurrentCell.ColumnIndex;
                if (CIndex > 0 && CIndex < 8)
                {
                    updateEmployee UE = new updateEmployee();
                    if (UE.ShowDialog() == DialogResult.OK)
                    {
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["AccountID"].Value = updateEmployee.uAccountID;
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Pas"].Value = updateEmployee.uPas;
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Position"].Value = updateEmployee.uPosition;
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeName"].Value = updateEmployee.uDEmployeeName;
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["DEmployeeCode"].Value = updateEmployee.uDEmployeeCode;
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["IsValid"].Value = updateEmployee.uIsvalid;
                        dgvEmployee.Rows[dgvEmployee.CurrentRow.Index].Cells["Type"].Value = updateEmployee.utype;
                    }
                }
                else
                {
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pnlMin_Click(object sender, EventArgs e)//最小化
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Panel_Close_Click(object sender, EventArgs e)//关闭
        {
            this.Dispose();
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent ;
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.Transparent;
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)//点住不放可移动窗体
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {            
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text.Trim()) || textBox1.Text.Trim() == "工号/姓名")
                    addEmployee();
                else
                    addEmployee(textBox1.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 查询加载员工信息
        /// </summary>
        /// <param name="s"></param>
        private void addEmployee(string s)//
        {
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append("select AccountID,pas,DEmployeeName,DEmployeeCode,Position,IsValid,'',DEmployeeID,Type from DEmployee ");
                str.Append("where (DEmployeeName like '%" + s + "%' or DEmployeeCode like '%" + s + "%')");
                if (checkBox1.Checked)
                {
                    str.Append(" and IsValid = 1 ");
                }
                ds = db.GetPIVAsDB(str.ToString());                
                dgvEmployee.DataSource = ds.Tables[0];
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (textBox1.Text.Trim() == "" || textBox1.Text.Trim() == "工号/姓名")
                        addEmployee();
                    else
                        addEmployee(textBox1.Text);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
            {
                addEmployee();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                textBox1.Text = "工号/姓名";
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "工号/姓名")
            {
                textBox1.SelectAll();
            }
        }

        private void lblAdd_MouseHover(object sender, EventArgs e)
        {
            lblAdd.ForeColor = Color.White;
        }

        private void lblAdd_MouseLeave(object sender, EventArgs e)
        {
            lblAdd.ForeColor = Color.Silver;
        }
    }
}
