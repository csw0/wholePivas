using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class YiZhuCX : UserControl
    {
        public YiZhuCX()
        {
            InitializeComponent();
        }
        public YiZhuCX(string Employee, string WardCode) 
        {
            InitializeComponent();
            this.WardCode = WardCode;
            this.EmployeeID = Employee;
        }
        DB_Help db = new DB_Help();
        DataSet ds = new DataSet();
        SQL sql=new SQL();
        string str;
        string WardCode,  EmployeeID;

        private void YiZhuCX_Load(object sender, EventArgs e)
        {
            BangdingPre(comboBox2.Text, textBox1.Text);
            dgvDrug.Rows.Clear();
        }
        /// <summary>
        /// 病人信息
        /// </summary>
        /// <param name="status"></param>
        /// <param name="name"></param>
        /// <param name="bed"></param>
        private void BangdingPre(string status,string search)
        {
            dgvPre.Rows.Clear();
            if (cb1.Checked)
            {
                str = sql.AllPre( status, search, WardCode, 1);
            }
            else
            {
                str = sql.AllPre(status, search, WardCode, 0);
            }
            ds = db.GetPIVAsDB(str);
            if (ds != null&&ds.Tables[0].Rows.Count>0) 
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvPre.Rows.Add(1);
                    dgvPre.Rows[i].Cells["BedNo"].Value = ds.Tables[0].Rows[i]["BedNo"].ToString();
                    dgvPre.Rows[i].Cells["PatName"].Value = ds.Tables[0].Rows[i]["PatName"].ToString();
                    string sex = ds.Tables[0].Rows[i]["Sex"].ToString().ToLower().Trim();
                    if ("2" == sex)
                        dgvPre.Rows[i].Cells["Sex"].Value = "女";
                    else if ("1" == sex)
                        dgvPre.Rows[i].Cells["Sex"].Value = "男";
                    else
                        dgvPre.Rows[i].Cells["Sex"].Value = "";
                    dgvPre.Rows[i].Cells["Age"].Value = ds.Tables[0].Rows[i]["Age"].ToString();
                    dgvPre.Rows[i].Cells["PatientCode"].Value = ds.Tables[0].Rows[i]["PatientCode"].ToString();
                }
                //dgvPre.ClearSelection();
                if (dgvPre.Rows.Count > 0)
                    dgvPre_Click(dgvDrug.CurrentCell, null);
            }
        }

      
        private void dgvPre_Click(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (dgvPre.Rows.Count>0)
                ShowDreg(dgvPre.CurrentRow.Cells["PatientCode"].Value.ToString());
        }
        /// <summary>
        /// 处方信息
        /// </summary>
        /// <param name="PCode"></param>
        private void ShowDreg(string PCode)
        {
            string LastPre="";
            Color LastColor=Color.White;
            dgvDrug.Rows.Clear();
            if (cb1.Checked)
            {
                str = sql.DDrug(PCode, comboBox2.Text,1);
            }
            else
            {
                str = sql.DDrug(PCode, comboBox2.Text,0);
            }
            //str = sql.DDrug(PCode,comboBox2.Text);
            ds = db.GetPIVAsDB(str);
            if (ds != null&&ds.Tables[0].Rows.Count >0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvDrug.Rows.Add(1);
                    if (LastPre == ds.Tables[0].Rows[i]["PrescriptionID"].ToString().Trim())
                    {
                        dgvDrug.Rows[i].DefaultCellStyle.BackColor =LastColor;
                    }
                    else
                    {
                        if(LastColor==Color.White)
                            dgvDrug.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(232, 239, 246);
                        else
                            dgvDrug.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dgvDrug.Rows[i].Cells["UniPreparationID"].Value = ds.Tables[0].Rows[i]["UniPreparationID"].ToString().Trim();
                    dgvDrug.Rows[i].Cells["PrescriptionID"].Value = ds.Tables[0].Rows[i]["PrescriptionID"].ToString().Trim();
                    dgvDrug.Rows[i].Cells["FreqName"].Value = ds.Tables[0].Rows[i]["FreqName"].ToString().Trim();
                    dgvDrug.Rows[i].Cells["DrugName"].Value = ds.Tables[0].Rows[i]["DrugName"].ToString().Trim();
                    dgvDrug.Rows[i].Cells["PStatus"].Value = ds.Tables[0].Rows[i]["PStatus"].ToString().Trim();
                    dgvDrug.Rows[i].Cells["Spec"].Value = ds.Tables[0].Rows[i]["Spec"].ToString().Trim();
                    dgvDrug.Rows[i].Cells["Dosage"].Value = ds.Tables[0].Rows[i]["Dosage"].ToString().Trim();
                    switch(int.Parse( ds.Tables[0].Rows[i]["PStatus"].ToString()))
                    {
                        case 0:dgvDrug.Rows[i].Cells["PStatus"].Value="执行中";
                            break;
                        case 1: dgvDrug.Rows[i].Cells["PStatus"].Value = "执行中";
                            break;
                        case 2:dgvDrug.Rows[i].Cells["PStatus"].Value="执行中";
                            break;
                        case 3: dgvDrug.Rows[i].Cells["PStatus"].Value = "已退方";
                            break;
                        case 4: dgvDrug.Rows[i].Cells["PStatus"].Value = "已停方";
                            break;
                    }
                    if (ds.Tables[0].Rows[i]["PPause"].ToString() == "True")
                        dgvDrug.Rows[i].Cells["PStatus"].Value = "已暂停";
                    LastPre = ds.Tables[0].Rows[i]["PrescriptionID"].ToString().Trim();
                    LastColor = dgvDrug.Rows[i].DefaultCellStyle.BackColor;
                }
                dgvDrug.ClearSelection();
            }
        }

     

        private void dgvDrug_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDrug.CurrentCell == dgvDrug.CurrentRow.Cells["DrugName"])
            {
                LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
                string UniPreID =dgvDrug.CurrentRow.Cells["UniPreparationID"].Value.ToString();
                mf.UniPreparationID = UniPreID;//UniPreID 是Drug表中的UniPreparationID 字段
                mf.ShowDialog();
            }
            else
            {
                ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
                PreForLabel lb = new PreForLabel(dgvDrug.CurrentRow.Cells["PrescriptionID"].Value.ToString());
                lb.ShowDialog();
            }
        }

      
        private void dgvDrug_Click(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();

        }

      
        private void dgvPre_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPre.Rows.Count >1)
                ShowDreg(dgvPre.CurrentRow.Cells["PatientCode"].Value.ToString());
        }

        private void cb1_Click(object sender, EventArgs e)
        {
            BangdingPre(comboBox2.Text, textBox1.Text);
            dgvDrug.Rows.Clear();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "姓名/床号")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "姓名/床号";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BangdingPre(comboBox2.Text, textBox1.Text);
            dgvDrug.Rows.Clear();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BangdingPre(comboBox2.SelectedItem.ToString(), textBox1.Text);
            dgvDrug.Rows.Clear();
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            BangdingPre(comboBox2.Text, textBox1.Text);
            dgvDrug.Rows.Clear();
        }

    }
}
