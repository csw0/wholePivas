using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class BPConfirm : Form
    {
        public BPConfirm()
        {
            InitializeComponent();
        }

        public BPConfirm(string formname, string model)
        {
            InitializeComponent();
            this.model = model;
            label4.Text = model;
            label1.Text = formname;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">“通过”，“不通过”</param>
        /// <param name="PreID">处方ID</param>
        public BPConfirm(string formname, string model, string PreID)
        {
            InitializeComponent();
            this.model = model;
            label4.Text = model;
            label1.Text = formname;
            this.PreID = PreID;
        }        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="model">“通过”，“不通过”</param>
        /// <param name="PreID">处方ID</param>
        /// <param name="DEmployeeID">员工ID</param>
        public BPConfirm(string formname, string model, string PreID,string DEmployeeID)
        {
            InitializeComponent();
            this.model = model;
            label4.Text = model;
            label1.Text = formname;
            this.PreID = PreID;
            this.DEmployeeID = DEmployeeID;
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

        string model;
        public int flpBackPreIndex=0;
        public string DoctorExplain;
        public string ecode = "", epass = "", eid = "";
        string PreID = "";
        string DEmployeeID = "";
        DB_Help DB = new DB_Help();
        string sql = "";

        private void btnOK_Click(object sender, EventArgs e)
        {
            ecode = txtCode.Text;
            epass = txtPass.Text;
            bool Sys = false;
            bool Per = false;

            for (int i = 0; i < dgvResult.RowCount;i++ )
            {                
                if (dgvResult.Rows[i].Cells["Column1"].Value.ToString()=="1")
                {
                    Sys = true;
                    break;
                }
            }
            if (flpBackPre.Controls.Count>0)
            {
                Per = true;
            }

            if (dgvResult.RowCount>0&&!Sys&&!Per)
            {
                MessageBox.Show("请勾选系统审方结果 或者 插入1条人工审核意见 ！");
                return;
            }
            else if (!Sys && !Per)
            {
                MessageBox.Show("请至少插入1条人工审核意见！");
                return;
            }

            if (flpBackPre.Controls.Count > 0)
            {
                if (((BackPreByPerson)(flpBackPre.Controls[flpBackPre.Controls.Count - 1])).cbbCensorItem.Text.Trim()=="" ||
                        ((BackPreByPerson)(flpBackPre.Controls[flpBackPre.Controls.Count - 1])).txtDescription.Text.Trim() == "退方说明" ||
                        ((BackPreByPerson)(flpBackPre.Controls[flpBackPre.Controls.Count - 1])).txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("人工审核信息不完整，如：退方说明是否填写；审方结果是否选择");
                    return;
                }
            }

            if (richTextBox1.Text.Trim() == "描述"||richTextBox1.Text.Trim() =="") 
            {
                MessageBox.Show("请填写退方描述（您可以双击1条系统审方，自动添加描述！）");
                richTextBox1.Focus();
                richTextBox1.SelectAll();
                return;
            }
            if (txtCode.Text.Trim() == "" && CheckPre.Confirmation == "1")
            {
                MessageBox.Show("退单，帐号不可为空 ！");
                return;
            }

            
            string s = "";

            try
            {
                string str = "";
                if (CheckPre.Confirmation == "1")
                {
                    str = "select * from DEmployee where AccountID = '" + ecode + "' and Pas = '" + epass + "'";
                }
                else
                {
                    str = "select * from DEmployee where DEmployeeID = '" + DEmployeeID + "'";
                }
                DataSet ds = DB.GetPIVAsDB(str);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    eid = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    DoctorExplain = richTextBox1.Text.Replace("'", "");
                    

                    for (int i = 0; i < dgvResult.Rows.Count;i++ )
                    {
                        
                        if (dgvResult.Rows[i].Cells["Column1"].Value.ToString()=="1")
                        {
                            s = "INSERT INTO CPResultRG values ('" + dgvResult.Rows[i].Cells["dgvCheckResultID"].Value.ToString() +
                                "','" + dgvResult.Rows[i].Cells["dgvCPRecordID"].Value.ToString() +
                                "','" + PreID +
                                "','" + dgvResult.Rows[i].Cells["dgvCensorItem"].Value.ToString() +
                                "','" + dgvResult.Rows[i].Cells["dgvDrugACode"].Value.ToString() +
                                "','" + dgvResult.Rows[i].Cells["dgvDrugBCode"].Value.ToString() +
                                "','" + dgvResult.Rows[i].Cells["dgvDescription"].Value.ToString() +
                                "','" + dgvResult.Rows[i].Cells["dgvLevel"].Value.ToString() +
                                "','" + dgvResult.Rows[i].Cells["dgvReferenName"].Value.ToString() + 
                                "',getdate(),0)";
                            DB.SetPIVAsDB(s);
                        }
                    }

                    for (int i = 0; i < flpBackPre.Controls.Count; i++)
                    {
                        string codeA = ((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugA.SelectedIndex >= 0 ? ((BDrug)(((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugA.SelectedItem)).Code : "";
                        string codeB = ((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugB.SelectedIndex >= 0 ? ((BDrug)(((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugB.SelectedItem)).Code : "";


                        s = "INSERT INTO CPResultRG values ('" + "0" +
                              "','" + ((BackPreByPerson)(flpBackPre.Controls[i])).CheckRecID +
                              "','" + PreID +
                              "','" + ((BackPreByPerson)(flpBackPre.Controls[i])).cbbCensorItem.Text +
                              "','" + codeA +
                              "','" + codeB +
                              "','" + ((BackPreByPerson)(flpBackPre.Controls[i])).txtDescription.Text.Trim() +
                              "','" + ((BackPreByPerson)(flpBackPre.Controls[i])).Level +
                              "','" + "人工退单" +
                              "',getdate(),1)";
                            DB.SetPIVAsDB(s);                    
                    }

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("用户不存在或密码错误");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + " "+ s);
            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void CPConfirm_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (CheckPre.Confirmation =="0" )
            {
                txtCode.Visible = false;
                txtPass.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                
            }

            txtCode.Focus();
            
            sql = "SELECT TOP 1 * FROM DEmployee WHERE DEmployeeID = '" + DEmployeeID + "'";
            ds = DB.GetPIVAsDB(sql);
            if (ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
            {
                label8.Text = ds.Tables[0].Rows[0]["DEmployeeName"].ToString();
            }

            sql = "SELECT 1 checked,CT.CensorItem,DD1.DrugName DrugAName,DD2.DrugName DrugBName,Description,ReferenName,"+
                "CT.CheckResultID,CD.CPRecordID,P.Level,DrugACode,DrugBCode  " +
                "FROM CPResult CT " +
                "INNER join CPRecord CD on CD.CPRecordID = CT.CheckRecID " +
                "INNER join Prescription P on P.PrescriptionID = CD.PrescriptionID " +
                "LEFT join DDrug DD1 ON DD1.DrugCode = DrugACode "+
                "LEFT join DDrug DD2 ON DD2.DrugCode = DrugBCode "+
                "where P.PrescriptionID = '" + PreID + "'";

            
            ds = DB.GetPIVAsDB(sql);
            if (ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count==0)
            {
                return;
            }

            dgvResult.DataSource = ds.Tables[0];
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {

                    string str = "select * from QRcodeLog where QRcode = '" + txtCode.Text + "' and DelDT IS NULL";
                    DataSet ds = DB.GetPIVAsDB(str);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ecode = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                        DoctorExplain = richTextBox1.Text.Replace("'", "");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        txtPass.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    ecode = txtCode.Text;
                    epass = txtPass.Text;
                    if (txtCode.Text.Trim() == "")
                    {
                        MessageBox.Show("帐号不可为空");
                        return;
                    }
                    string str = "select * from DEmployee where AccountID = '" + ecode + "' and Pas = '" + epass + "'";
                    DataSet ds = DB.GetPIVAsDB(str);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ecode = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                        DoctorExplain = richTextBox1.Text.Replace("'", "");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("用户不存在或密码错误");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddBackPre_Click(object sender, EventArgs e)
        {
            if (flpBackPre.Controls.Count > 0)
            {
                if (((BackPreByPerson)(flpBackPre.Controls[flpBackPre.Controls.Count - 1])).cbbCensorItem.Text.Trim() == "审方结果" ||
                        ((BackPreByPerson)(flpBackPre.Controls[flpBackPre.Controls.Count - 1])).txtDescription.Text.Trim() == "退方说明" || 
                        ((BackPreByPerson)(flpBackPre.Controls[flpBackPre.Controls.Count - 1])).txtDescription.Text.Trim() == "")
                {
                    return;
                }
                else
                {
                    BackPreByPerson B = new BackPreByPerson();
                    B.setBackInfo(PreID, flpBackPre.Controls.Count);
                    B.setDescription += new BackPreByPerson.setDesc(SetDescription);
                    flpBackPre.Controls.Add(B);
                }
            }
            else
            {
                BackPreByPerson B = new BackPreByPerson();
                B.setBackInfo(PreID,0);
                B.setDescription += new BackPreByPerson.setDesc(SetDescription);
                flpBackPre.Controls.Add(B);
                
            }
       }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Trim() == "描述")
            {
                richTextBox1.SelectAll();
            }
            richTextBox1.ForeColor = Color.Gray;
        }

        private void SetDescription()
        {
            try
            {
                string s = "";
                for (int i = 0; i < flpBackPre.Controls.Count;i++ )
                {
                    s += ((BackPreByPerson)(flpBackPre.Controls[i])).cbbCensorItem.Text + "_";

                    if(((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugA.SelectedIndex>=0)
                        s += ((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugA.Text + "_";
                    if (((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugB.SelectedIndex >= 0)
                        s += ((BackPreByPerson)(flpBackPre.Controls[i])).cbbDrugB.Text + "_";

                    s += ((BackPreByPerson)(flpBackPre.Controls[i])).txtDescription.Text.Trim() + "\r\n";
                }
                richTextBox1.Text = s;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BPConfirm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void dgvResult_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void flpBackPre_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < dgvResult.Rows.Count; i++)
                    {
                        dgvResult.Rows[i].Cells["Column1"].Value = "1";
                    }
                }
                else
                {
                    for (int i = 0; i < dgvResult.Rows.Count; i++)
                    {
                        dgvResult.Rows[i].Cells["Column1"].Value = "0";
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvResult_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.RowCount<=0)
                {
                    return;
                }
                richTextBox1.Text = dgvResult.CurrentRow.Cells["dgvCensorItem"].Value.ToString()+"\r\n";
                if (dgvResult.CurrentRow.Cells["dgvDrugAName"].Value.ToString()!="")
                {
                    richTextBox1.Text += dgvResult.CurrentRow.Cells["dgvDrugAName"].Value.ToString() + "\r\n";
                }
                if (dgvResult.CurrentRow.Cells["dgvDrugBName"].Value.ToString()!="")
                {
                    richTextBox1.Text += dgvResult.CurrentRow.Cells["dgvDrugBName"].Value.ToString() + "\r\n";
                }
                
                richTextBox1.Text += dgvResult.CurrentRow.Cells["dgvDescription"].Value.ToString() + "\r\n";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (DB.Limit(DEmployeeID, "refundwhy"))
            //{
                RGDict dict = new RGDict();
                dict.ShowDialog();
          //  }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
                return;
            ((BackPreByPerson)(flpBackPre.Controls[Convert.ToInt32( dataGridView1.Tag.ToString())])).txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            ((BackPreByPerson)(flpBackPre.Controls[Convert.ToInt32(dataGridView1.Tag.ToString())])).txtDescription.Focus();
            dataGridView1.Visible = false;

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
           // MessageBox.Show("fdfd");
            if (e.KeyCode == Keys.Enter)
            {
                ((BackPreByPerson)(flpBackPre.Controls[Convert.ToInt32(dataGridView1.Tag.ToString())])).txtDescription.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                ((BackPreByPerson)(flpBackPre.Controls[Convert.ToInt32(dataGridView1.Tag.ToString())])).txtDescription.Focus();
                dataGridView1.Visible = false;
                
                   // SendKeys.Send("{up}");
            }
        }


  
        

    
       

 
    }
}
