using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;
namespace PivasNurse
{
    public partial class TiQianDB : UserControl
    {
        public TiQianDB()
        {
            InitializeComponent();
        }
        public TiQianDB(string Employee, string WardCode) 
        {
            InitializeComponent();
            this.EmployeeID = Employee;
            this.WardCode = WardCode;
        }
        string WardCode, PrescriptionCode,  EmployeeID;
        SQL sqlstr = new SQL();
        DB_Help db = new DB_Help();
        int LabelOverFor;
        string IsCheck = "1";//默认需要确认

        private void TiQianDB_Load(object sender, EventArgs e)
        {
            IsCheck = db.GetPivasAllSet("护士站-提前打包-确认");
            LabelOverFor = NurseFormSet();         
            BangdingDvg();

        }
        private void BangdingDvg() //处方单
        {
            string str=sqlstr.ReturnPre(WardCode);
            DataSet ds=db.GetPIVAsDB(str);
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                dgvPre.ClearSelection();
                dgvPre.DataSource = ds.Tables[0];
                dgvPre.Columns["PatientCode"].Visible = false;
              
                label_count.Text = ds.Tables[0].Rows.Count.ToString();
            }
        }

        private void dgvPre_Click(object sender, EventArgs e)
        {
            if (dgvPre.Rows.Count <= 0)
            {
                ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
                return;
            }
            button2.Enabled = false;
            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            showlabel(PrescriptionCode);
            information21.SetInformation(PrescriptionCode);
            
        }
        private void showlabel(string PrescripionCode) //显示瓶签
        {
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            checkBox3.Checked = false;
            if (PrescriptionCode != null)
            {
                string str = sqlstr.ReturnRecord(PrescriptionCode, 0);
                DataSet ds = db.GetPIVAsDB(str);
                int IsKCancel = GetIsKCancel();
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        LabelNo ln = new LabelNo("TiQianDB", LabelOverFor);
                        ln.add(ds.Tables[0].Rows[i],true,IsKCancel);
                        ln.Top = 28 * i;
                        ln.Parent = panel1;
                        panel1.Controls.Add(ln);
                    }
                }
                str = sqlstr.ReturnRecord(PrescriptionCode, 1);
                ds = db.GetPIVAsDB(str);
                if (ds!=null&& ds.Tables[0].Rows.Count>0 )
                {
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        LabelNo ln = new LabelNo("TiQianDB", LabelOverFor);
                        ln.add(ds.Tables[0].Rows[i],false,IsKCancel);
                        ln.Top = 28 * i;
                        ln.Parent = panel2;
                        panel2.Controls.Add(ln);
                    }
                    panel3.Visible = true;             
                }
                else
                {
                    panel3.Visible = false;                 
                }
            }
        }


       
        private void checkBox3_Click(object sender, EventArgs e)//全选
        {
            if (checkBox3.Checked == true)
            {
                foreach (Control i in panel1.Controls)
                {
                    if (i is LabelNo&&i.Enabled==true)
                    {
                        ((LabelNo)i).checkBox1.Checked = true;
                    }
                }
                foreach (Control i in panel2.Controls)
                {
                    if (i is LabelNo&&i.Enabled==true)
                    {
                        ((LabelNo)i).checkBox1.Checked = true;
                    }
                }
            }
            else 
            {
                foreach (Control i in panel1.Controls)
                {
                    if (i is LabelNo)
                    {
                        ((LabelNo)i).checkBox1.Checked = false;
                    }
                }
                foreach (Control i in panel2.Controls)
                {
                    if (i is LabelNo)
                    {
                        ((LabelNo)i).checkBox1.Checked = false;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//刷新界面
        {
            int row = dgvPre.CurrentRow.Index;
            CheckCancelDrug crd = new CheckCancelDrug();
            CheckCancelDrug.CancelDrug += new CheckCancelDrug.NewDelegate(update);
            if (crd.ShowDialog() == DialogResult.OK)
            {
                dgvPre.Rows[row].Selected=true;
                showlabel(dgvPre.CurrentRow.Cells["PreCode"].Value.ToString());
                button2.Enabled = false;
            }

            
        }
        public void update(string Rid,DateTime Rtime ) //提前打包
        {
            string str = string.Empty;
            foreach (LabelNo i in panel1.Controls)
            {
                if (i.checkBox1.Checked == true)
                {
                    if (IsCheck == "0")
                    {
                        str = sqlstr.EarlyDB(i.checkBox1.Text, Rid, Rtime);
                    }
                    else
                    {
                        str = sqlstr.CancelInsert(i.checkBox1.Text, Rid, Rtime, 0);
                    }
                    db.SetPIVAsDB(str);
                }
            }
            foreach (LabelNo i in panel2.Controls)
            {
                if (i.checkBox1.Checked == true)
                {
                    if (IsCheck == "0")
                    {
                        str = sqlstr.EarlyDB(i.checkBox1.Text, Rid, Rtime);
                    }
                    else
                    {
                        str = sqlstr.CancelInsert(i.checkBox1.Text, Rid, Rtime, 0);
                    }
                    db.SetPIVAsDB(str);
                }
            }
        }

        private int NurseFormSet()
        {
            int m=0;
            string SqlSet = "Select * from PivasNurseFormSet where WardCode='" + WardCode + "'";
            DataTable dt = new DataTable();
            dt = db.GetPIVAsDB(SqlSet).Tables[0];
            if (dt.Rows.Count == 0)
            {
                //dt = db.GetPIVAsDB("select LabelOverFor,PackOverFor from PivasNurseFormSet where DateFrom='1'").Tables[0];
                //if (dt.Rows.Count == 1)
                //{
                //    int a = int.Parse(dt.Rows[0]["LabelOverFor"].ToString());
                //    int b = int.Parse(dt.Rows[0]["PackOverFor"].ToString());
                //    db.SetPIVAsDB(" insert into PivasNurseFormSet values('0','" + WardCode + "','" + a + "','" + b + "',null)");
                //}
                //dt = db.GetPIVAsDB(SqlSet).Tables[0];
                MessageBox.Show("病区未设置，请重新登录进行设置");
               return 100;
            }
            if (dt.Rows.Count > 0)
            {
                m = int.Parse(dt.Rows[0]["PackOverFor"].ToString());
            }
            return m;

        }
        /// <summary>
        /// 得到从那个环节不准提前打包
        /// </summary>
        /// <returns></returns>
        private int GetIsKCancel()
        {
            string sql = "select IsKCancel from PivasNurseFormSet where WardCode='" + WardCode + "' ";
            DataTable dt = db.GetPIVAsDB(sql).Tables[0];
            return int.Parse(dt.Rows[0][0].ToString() == "" ? "0" : dt.Rows[0][0].ToString());
        }

        private void dgvPre_SelectionChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            if (dgvPre.Rows.Count <= 0)
            {
                return;
            }
            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            showlabel(PrescriptionCode);
            information21.SetInformation(PrescriptionCode);
        }

      
    }
    
}
