using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class DXTuiYao : UserControl
    {
        
        public DXTuiYao()
        {
            InitializeComponent();
        }
        public DXTuiYao(string Employee, string WardCode) 
        {
            InitializeComponent();
            this.EmployeeID = Employee;
            this.WardCode = WardCode;
        }
        string WardCode, PrescriptionCode, EmployeeID; //病区id，处方id，员工id
        SQL sqlstr = new SQL();
        DB_Help db = new DB_Help();
        int LabelOverFor;
        string IsCheck = "1";//默认需要确认

        private void DXTuiYao_Load(object sender, EventArgs e)
        {
            IsCheck= db.GetPivasAllSet("护士站-单项退药-确认");
            LabelOverFor = NurseFormSet();
            BangdingDvg();
            
        }
        //加载处方列表
        private void BangdingDvg()
        {
            string str=sqlstr.ReturnPre(WardCode);
            DataSet ds=db.GetPIVAsDB(str);
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                dgvPre.DataSource = ds.Tables[0];
                dgvPre.Columns["PatientCode"].Visible = false;              
                if (dgvPre.Rows.Count > 0) 
                {
                    dgvPre_Click(dgvPre.CurrentCell, null);
                }
                dgvPre.ClearSelection();
                label_count.Text = " - 总共 " + ds.Tables[0].Rows.Count.ToString() + " 个输液单正在执行";
            }
        }

        //点击处方事件
        private void dgvPre_Click(object sender, EventArgs e)
        {
            if (dgvPre.Rows.Count <= 0)
            {
                ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
                return;
            }
          
            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            showlabel(PrescriptionCode);//显示瓶签
            information21.SetInformation(PrescriptionCode);//显示病人信息
            
        }
        //显示瓶签
        private void showlabel(string PrescripionCode)
        {
            checkBox3.Checked = false;
            if (PrescriptionCode != null)
            {
                string str = sqlstr.ReturnRecord(PrescriptionCode, 0);
                DataSet ds = db.GetPIVAsDB(str);
                int IsKCancel = GetIsKCancel();
                if (ds != null)
                {
                    checkBox3.Enabled = true;
                    panel1.Controls.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        LabelNo ln = new LabelNo("DXTuiYao", LabelOverFor);
                        ln.add(ds.Tables[0].Rows[i],true,IsKCancel);
                        ln.Top = 28 * i;
                        ln.Parent = panel1;
                        panel1.Controls.Add(ln);
                    }
                }
                str = sqlstr.ReturnRecord(PrescriptionCode, 1);
                ds = db.GetPIVAsDB(str);
                if (ds!=null&&ds.Tables[0].Rows.Count>0 )
                {
                    checkBox3.Enabled = true;
                    panel2.Controls.Clear();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        LabelNo ln = new LabelNo("DXTuiYao", LabelOverFor);
                        ln.add(ds.Tables[0].Rows[i],false,IsKCancel);
                        ln.Top = 28 * i;
                        ln.Parent = panel2;
                        panel2.Controls.Add(ln);
                    }
                    panel3.Visible = true;
                    //panel4.Height = 80;
                    //panel3.Top = 119;
                }
                else
                {
                    panel3.Visible = false;
                    //panel3.Top = 215;
                    //panel4.Height = 160;
                }
            }
        }

        //全选按钮
        private void checkBox3_Click(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
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
                    if (i is LabelNo && i.Enabled == true)
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

        //刷新界面
        private void button2_Click(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            int row = dgvPre.CurrentRow.Index;
            CheckCancelDrug crd = new CheckCancelDrug();
            CheckCancelDrug.CancelDrug+=new CheckCancelDrug.NewDelegate(update);
            if (crd.ShowDialog() == DialogResult.OK)
            {
                dgvPre.ClearSelection();
                dgvPre.Rows[row].Selected = true;
                showlabel(dgvPre.CurrentRow.Cells["PreCode"].Value.ToString());
                button2.Enabled = false;
            }
            
        }

        //退药
        public void update(string Rid,DateTime Rtime )
        {
            string str=string.Empty;
            foreach (LabelNo i in panel1.Controls) //退今天
            {
                if (i.checkBox1.Checked == true)
                {
                    if (IsCheck == "0")
                        str= sqlstr.CancelDrug(i.checkBox1.Text, Rid, Rtime);
                    else
                        str = sqlstr.CancelInsert(i.checkBox1.Text, Rid, Rtime, 1);
                    db.SetPIVAsDB(str);
                }
            }
            foreach (LabelNo i in panel2.Controls)//退明天
            {
                if (i.checkBox1.Checked == true)
                {
                    if (IsCheck == "0")
                        str = sqlstr.CancelDrug(i.checkBox1.Text, Rid, Rtime);
                    else
                        str = sqlstr.CancelInsert(i.checkBox1.Text, Rid, Rtime, 1);
                    db.SetPIVAsDB(str);
                }
            }
        }

        
        private int NurseFormSet() 
        {int m=0;
            string SqlSet = "Select * from PivasNurseFormSet where WardCode='"+WardCode+"'";
            DataTable dt = new DataTable();
            dt = db.GetPIVAsDB(SqlSet).Tables[0];
            if (dt.Rows.Count==0)
            {
                //dt = db.GetPIVAsDB("select LabelOverFor,PackOverFor from PivasNurseFormSet where DateFrom='1'").Tables[0];
                //if (dt.Rows.Count == 1) 
                //{
                //    int a =int.Parse( dt.Rows[0]["LabelOverFor"].ToString());
                //    int b = int.Parse(dt.Rows[0]["PackOverFor"].ToString());
                //    db.SetPIVAsDB(" insert into PivasNurseFormSet values('0','" + WardCode + "','" + a + "','" + b + "',null)");
                //}
                //dt = db.GetPIVAsDB(SqlSet).Tables[0];
                MessageBox.Show("病区未设置，请重新登录进行设置");
                return 100;
            }
            if (dt.Rows.Count > 0)
            {
               m= int.Parse(dt.Rows[0]["LabelOverFor"].ToString());
            }
            return m;
        }

        private int GetIsKCancel() 
        {
            string sql = "select IsKCancel from PivasNurseFormSet where WardCode='" + WardCode + "' ";
            DataTable dt = db.GetPIVAsDB(sql).Tables[0];
            return int.Parse(dt.Rows[0][0].ToString() == ""?"0" : dt.Rows[0][0].ToString());
        }

        private void dgvPre_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPre.Rows.Count <= 0)
            {
                return;
            }

            PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            showlabel(PrescriptionCode);//显示瓶签
            information21.SetInformation(PrescriptionCode);//显示病人信息

        }
      
    }
}
