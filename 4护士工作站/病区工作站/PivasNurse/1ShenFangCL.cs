using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class ShenFangCL : UserControl
    {
        public ShenFangCL()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            information1.clear();
        }
        public ShenFangCL(string Employee, string WardCode)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 2;

            information1.clear();
            EmployeeID = Employee;
            this.WardCode = WardCode;
        }


        DB_Help DB = new DB_Help();
        SQL SQLStr = new SQL();
        string WardCode, PrescriptionCode, RecID, EmployeeID;
        int tag = 0;

        private void ShenFangCL_Load(object sender, EventArgs e)
        {    
            label2.Text = DateTime.Now.ToString();
            tag = 60;          
            timer1.Enabled = true;
            if (DB.GetPivasAllSet("护士站-系统审方-强制执行") == "0")
            {
                comboBox1.Items.RemoveAt(5);               
            }
            if (DB.GetPivasAllSet("护士站-系统审方处理") == "0")
            {
                button2.Visible = false;
            }
            else
            {
                button2.Visible = true;
            }
            LoadPrescription(WardCode, "2");

        }

        private void LoadPrescription(string WardCord, string operate)
        {
            try
            {
                information1.clear();           
                string str = SQLStr.NurseLoadPre(WardCord,operate);
                DataSet ds = DB.GetPIVAsDB(str);
                
                ds.Tables[0].Columns.Add("state", Type.GetType("System.String"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Level"].ToString().Trim() == "0" && ds.Tables[0].Rows[i]["PStatus"].ToString().Trim() != "3")
                    {
                        ds.Tables[0].Rows[i]["state"] = "系统审核已通过";
                       
                    }
                    else if (ds.Tables[0].Rows[i]["Level"].ToString().Trim() != "0" && ds.Tables[0].Rows[i]["DoctorOperate"].ToString().Trim() == "0" || ds.Tables[0].Rows[i]["DoctorOperate"].ToString().Trim() == "" && ds.Tables[0].Rows[i]["PStatus"].ToString().Trim() != "3")
                    {
                        ds.Tables[0].Rows[i]["state"] = "系统审核未通过";
                    }
                    else if (ds.Tables[0].Rows[i]["DoctorOperate"].ToString().Trim() == "1" && ds.Tables[0].Rows[i]["PStatus"].ToString().Trim() != "3")
                    {
                        ds.Tables[0].Rows[i]["state"] = "系统审核未通过强制执行";
                    }
                    else if (ds.Tables[0].Rows[i]["DoctorOperate"].ToString().Trim() == "2" && ds.Tables[0].Rows[i]["PStatus"].ToString().Trim() != "3")
                    {
                        ds.Tables[0].Rows[i]["state"] = "系统审核未通过已同意退单";
                    }
                    else if (ds.Tables[0].Rows[i]["PStatus"].ToString().Trim() == "3")
                    {
                        ds.Tables[0].Rows[i]["state"] = "人工退单";
                    }
                }
                tag = 60;
                button1.Enabled = false;
                dgvPre.DataSource = ds.Tables[0];
                dgvPre.Columns["Level"].Visible = false;
                dgvPre.Columns["DoctorOperate"].Visible = false;
                dgvPre.Columns["PStatus"].Visible = false;
                dgvPre.ClearSelection();
              
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        private void dgvPre_Click(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (dgvPre.Rows.Count <= 0)
            {
                return;
            }
            if (dgvPre.CurrentRow.Cells["state"].Value.ToString() == "系统审核未通过"|| dgvPre.CurrentRow.Cells["state"].Value.ToString() == "人工退单")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
            RecID = dgvPre.CurrentRow.Cells["CPRecordID"].Value.ToString();
            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            tag = 60;
            information1.SetInformation(PrescriptionCode);
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tag = 60;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    //label17.Text = "查看 全部 的医嘱";
                    LoadPrescription(WardCode, "0");
                    break;
                case 1:
                    //label17.Text = "查看 系统审核已通过 的医嘱";
                    LoadPrescription(WardCode, "1");
                    break;
                case 2:
                    //label17.Text = "查看 系统审核未通过人工未审 的医嘱";
                    LoadPrescription(WardCode, "2");
                    break;
                case 3:
                    //label17.Text = "查看 系统审核未通过人工强制执行 的医嘱";
                    LoadPrescription(WardCode, "3");
                    break;
                case 4:
                    //label17.Text = "查看 系统审核未通过人工强制执行 的医嘱";
                    LoadPrescription(WardCode, "4");
                    break;
                case 5:
                    LoadPrescription(WardCode, "5");  //人工退单，要求强制执行
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tag = 60;
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            DoctorOpinion DO = new DoctorOpinion(EmployeeID, RecID,PrescriptionCode);
            if (DO.ShowDialog() == DialogResult.OK)
            {
                int I = comboBox1.SelectedIndex;
                switch (I)
                {
                    case 0:
                        LoadPrescription(WardCode, "0");
                        button1.Enabled = false;
                        break;
                    case 1:
                        LoadPrescription(WardCode, "1");
                        button1.Enabled = false;
                        break;
                    case 2:
                        LoadPrescription(WardCode, "2");
                        button1.Enabled = true;
                        break;
                    case 3:
                        LoadPrescription(WardCode, "3");
                        button1.Enabled = false;
                        break;
                    case 4:
                        LoadPrescription(WardCode, "4");
                        button1.Enabled = false;
                        break;
                    case 5:
                        LoadPrescription(WardCode, "5");  //人工退单，要求强制执行
                        button1.Enabled = true;
                        break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            if (tag > 0)
            {
                tag--;
            }
            else
            {
                int I = comboBox1.SelectedIndex;
                switch (I)
                {
                    case 0:
                        LoadPrescription(WardCode, "0");
                        button1.Enabled = false;
                        break;
                    case 1:
                        LoadPrescription(WardCode, "1");
                        button1.Enabled = false;
                        break;
                    case 2:
                        LoadPrescription(WardCode, "2");
                        button1.Enabled = true;
                        break;
                    case 3:
                        LoadPrescription(WardCode, "3");
                        button1.Enabled = false;
                        break;
                    case 4:
                        LoadPrescription(WardCode, "4");
                        button1.Enabled = false;
                        break;

                    case 5:
                        LoadPrescription(WardCode, "5");  //人工退单，要求强制执行
                        button1.Enabled = true;
                        break;
                }
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tag = 60;
            Form1 f = new Form1(EmployeeID, WardCode);
            f.ShowDialog() ;
        }

       
       
        /// <summary>
        /// 上下键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        private void dgvPre_SelectionChanged(object sender, EventArgs e)
        {
            tag = 60;
            if (dgvPre.CurrentRow.Cells["state"].Value.ToString() == "系统审核未通过" || dgvPre.CurrentRow.Cells["state"].Value.ToString() == "人工退单")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }

            RecID = dgvPre.CurrentRow.Cells["CPRecordID"].Value.ToString();
            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();

            information1.SetInformation(PrescriptionCode);
        }
       
    }
}
