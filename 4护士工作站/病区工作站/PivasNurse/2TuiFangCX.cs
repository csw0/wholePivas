using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class TuiFangCX : UserControl
    {
        public TuiFangCX()
        {
            InitializeComponent();
        }

        DB_Help DB = new DB_Help();
        SQL SQLStr = new SQL();
      
        string WardCode, PrescriptionCode, EmployeeID;
        int tag = 0;
        int currntindex;
        public TuiFangCX(string Employee, string WardCode)
        {
            InitializeComponent();
            EmployeeID = Employee;
            this.WardCode = WardCode;
        }
        
        private void TuiFangCX_Load(object sender, EventArgs e)
        {
            cb1.SelectedIndex =0;
          
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            LoadBPRecord(WardCode);          
        }

        private void LoadBPRecord(string w)
        {
            string str=null;
            DataSet ds = new DataSet();
            if (checkBox2.Checked == true && checkBox1.Checked == true)
            {
                str = SQLStr.BPRecore(w, this.dateTimePicker1.Value.ToString(), 3,cb1.SelectedIndex);
              
            }
            else if (checkBox1.Checked == true)
            {
                str = SQLStr.BPRecore(w, this.dateTimePicker1.Value.ToString(), 0,cb1.SelectedIndex);
            }
            else if (checkBox2.Checked == true)
            {
                str = SQLStr.BPRecore(w, this.dateTimePicker1.Value.ToString(), 1, cb1.SelectedIndex);
            }
           
            ds = DB.GetPIVAsDB(str);                
           if(ds!=null)
           {
               ds.Tables[0].Columns.Add("Read", Type.GetType("System.String"));
               for(int i=0;i<ds.Tables[0].Rows.Count;i++)
               {
                   if (ds.Tables[0].Rows[i]["BPIsRead"].ToString() == "False" || ds.Tables[0].Rows[i]["BPIsRead"].ToString()=="")
                   {
                       ds.Tables[0].Rows[i]["Read"] = "未阅读";
                   }
                   else
                   {
                       ds.Tables[0].Rows[i]["Read"] = "已阅读";
                   }
               }
               cancelDrugInfo1.clear();
                dgvPre.DataSource = ds.Tables[0];
                dgvPre.Columns["BPDT"].Visible = false;
                dgvPre.Columns["WardCode"].Visible = false;
                dgvPre.Columns["BPIsRead"].Visible = false;
                tag = 60;
                timer1.Enabled = true;
                for (int i = 0; i < dgvPre.Rows.Count; i++)
                {
                    //if (dgvPre.Rows[i].Cells["BPIsRead"].Value.ToString() == "True")
                    //{
                    //    dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    //}
                    if (dgvPre.Rows[i].Cells["PStatus"].Value.ToString() == "人工退方")
                    {
                        dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
               }
        }


        private string BPrecord(string BPRecord )
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" UPDATE bPRecord SET BPIsRead = 1");
            str.Append(" WHERE ");
            //str.Append(" DATEDIFF(dd, BPDT,'" + dateTimePicker1.Value + "') = 0");
            str.Append(" BPRecordID =");
            str.Append(BPRecord);
            return str.ToString();
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tag > 0)
            {
                tag--;
            }
            else
            {
                LoadBPRecord(WardCode);
            }
         
        }

      

        private void dgvPre_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPre.Rows.Count > 0)
            {
                currntindex = dgvPre.CurrentRow.Index;
                PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
                if (cb1.SelectedIndex == 1 || dgvPre.CurrentRow.Cells["PStatus"].Value.ToString() == "人工退方")
                {
                    cancelDrugInfo1.SetInformation(PrescriptionCode, 0);
                }
                else
                {
                    cancelDrugInfo1.SetInformation(PrescriptionCode,1);
                }
            }
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            try
            {
                tag = 60; 
                LoadBPRecord(WardCode);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void dgvPre_DoubleClick(object sender, EventArgs e)
        {
            if (dgvPre.Rows.Count > 0)
            {
                string BPRecord = dgvPre.CurrentRow.Cells["BPRecordID"].Value.ToString();
                DB.SetPIVAsDB(BPrecord(BPRecord));

                LoadBPRecord(WardCode);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tag = 60; 
            LoadBPRecord(WardCode); 
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            tag = 60; 
      LoadBPRecord(WardCode);
            
        }
        private void dgvPre_Sorted(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPre.Rows.Count; i++)
            {
                if (dgvPre.Rows[i].Cells["BPIsRead"].Value.ToString() == "True")
                {
                    dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }

        }

        private void cb1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            tag =60;
            if (cb1.SelectedIndex == 0 || cb1.SelectedIndex == 2)
            {
             
                checkBox1.Visible = false;
                checkBox2.Visible = false;
            }
            else
            {               
                checkBox1.Visible = true;
                checkBox2.Visible = true;
            }
            LoadBPRecord(WardCode);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ((Form1)(this.Parent.Parent)).m();
            //this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ((Form1)(this.Parent.Parent)).n();
        }

        private void information1_MouseDown(object sender, MouseEventArgs e)
        {

            ((Form1)(this.Parent.Parent)).n();
        }

     
    }
}
