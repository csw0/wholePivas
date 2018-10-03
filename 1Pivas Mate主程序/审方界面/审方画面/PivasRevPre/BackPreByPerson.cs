using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class BackPreByPerson : UserControl
    {
        public BackPreByPerson()
        {
            InitializeComponent();
        }

        public delegate void setDesc();
        public event setDesc setDescription;

        DB_Help DB = new DB_Help();
        public string CheckResultID = "";
        public string CheckRecID = "";
        public string Level = "";
        private int index;
     
        private DataTable dt=new DataTable();

        private void pnlClose_Click(object sender, EventArgs e)
        {
            ((BPConfirm)(this.Parent.Parent)).dataGridView1.Visible = false; 
            this.Dispose();
            
        }

        public void setBackInfo(string PreID,int index)
        {
            try
            {
                this.index = index;
                string sql = "select CD.CPRecordID,P.Level,PD.DrugName,DD.DrugCode  from PrescriptionDetail PD "+
                            "inner join Prescription P on P.PrescriptionID = PD.PrescriptionID "+
                            "inner join DDrug DD ON DD.DrugCode = PD.DrugCode "+
                            "left join CPRecord CD ON CD.PrescriptionID = P.PrescriptionID "+
                            "where P.PrescriptionID = '" + PreID + "'";

                DataSet ds = new DataSet();

                ds = DB.GetPIVAsDB(sql);

                if (ds!=null&&ds.Tables.Count>0&&ds.Tables[0].Rows.Count>0)
                {
                    CheckRecID = ds.Tables[0].Rows[0]["CPRecordID"].ToString();
                    //CheckResultID = ds.Tables[0].Rows[0]["CheckResultID"].ToString();
                    Level = ds.Tables[0].Rows[0]["Level"].ToString();

                    for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
                    {
                        BDrug D = new BDrug(ds.Tables[0].Rows[i]["DrugCode"].ToString(), ds.Tables[0].Rows[i]["DrugName"].ToString());

                        cbbDrugA.Items.Add(D);
                        cbbDrugB.Items.Add(D);
                    }
                }


                sql = "select distinct name from CPResultRGDict";
                ds = DB.GetPIVAsDB(sql);

                if (ds!=null&&ds.Tables.Count>0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count;i++ )
                    {
                        cbbCensorItem.Items.Add(ds.Tables[0].Rows[i]["name"].ToString());
                        
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        




        private void BackPreByPerson_Load(object sender, EventArgs e)
        {
            try
            {
                cbbCensorItem.Text = "审方结果";
                cbbDrugA.Text = "药品A";
                cbbDrugB.Text = "药品B";
                txtDescription.Text = "退方说明";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbDrugB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                setDescription();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtDescription_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == "退方说明")
            {
                txtDescription.SelectAll();
            }
            
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            setDescription();
            DataTable Newdt = dt.Copy();
            
            if (txtDescription.Text.Trim() == "退方说明")
            {
                txtDescription.ForeColor = Color.Gray;
                
            }
            else
            {
                txtDescription.ForeColor = Color.Black;

                Newdt.Rows.Clear();


                if (dt.Rows.Count <= 0)
                    return;

                DataRow[] DR = dt.Select("describe like '%" + txtDescription.Text + "%'");
               
                foreach (DataRow dr in DR)
                {
                    Newdt.ImportRow(dr);
                }
               
            }

            ((BPConfirm)(this.Parent.Parent)).dataGridView1.DataSource = Newdt;

            if (Newdt.Rows.Count > 0)
            {
                ((BPConfirm)(this.Parent.Parent)).dataGridView1.Visible = true;
                
                ((BPConfirm)(this.Parent.Parent)).dataGridView1.Tag = index;
                ((BPConfirm)(this.Parent.Parent)).dataGridView1.Location =
                   new Point(this.txtDescription.Location.X + 5, this.Parent.Location.Y + (this.txtDescription.Location.Y + 23) * (index + 1));
            }
            //else

            //    ((BPConfirm)(this.Parent.Parent)).dataGridView1.Visible = false;
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {                
                 setDescription();
                         
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbCensorItem_SelectedIndexChanged(object sender, EventArgs e)
        {
           DataSet ds = DB.GetPIVAsDB("select distinct describe from CPResultRGDictDetail where dictID in (select [ID] from CPResultRGDict where Name='" + cbbCensorItem.Text + "' )");
           if (ds != null)
             dt = ds.Tables[0];
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            if (!((BPConfirm)(this.Parent.Parent)).dataGridView1.Focused)
            ((BPConfirm)(this.Parent.Parent)).dataGridView1.Visible = false;
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtDescription.Focused&&e.KeyCode==Keys.Down) 
            {
                ((BPConfirm)(this.Parent.Parent)).dataGridView1.Focus();
            }
        }

        private void txtDescription_Enter(object sender, EventArgs e)
        {
            //if (txtDescription.Text == "退方说明" || txtDescription.Text == "")
            //{

                if (dt.Rows.Count > 0)
                {
                    ((BPConfirm)(this.Parent.Parent)).dataGridView1.DataSource = dt;
                    ((BPConfirm)(this.Parent.Parent)).dataGridView1.Visible = true;

                    ((BPConfirm)(this.Parent.Parent)).dataGridView1.Tag = index;
                    ((BPConfirm)(this.Parent.Parent)).dataGridView1.Location =
                       new Point(this.txtDescription.Location.X + 5, this.Parent.Location.Y + (this.txtDescription.Location.Y + 23) * (index + 1));
                }
            //}
        }

    }

    class BDrug
    {
        public string Code;
        public string Name;
        public BDrug(string DrugCode,string DrugName)
        {
            Name = DrugName;
            Code = DrugCode;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
