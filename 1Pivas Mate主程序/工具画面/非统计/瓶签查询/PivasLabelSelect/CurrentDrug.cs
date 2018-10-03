using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasLabelSelect
{
    public partial class CurrentDrug : UserControl
    {
        string UniPreparationID;
        string labelno = string.Empty; //删除药品时使用      
        SqlStr sql = new SqlStr();   //删除插入药品时使用
        private  DB_Help db = new DB_Help();
        DataRow dr;
        public CurrentDrug(string labelno,DataRow dr)
        {
            InitializeComponent();
            this.labelno = labelno;
           
            this.dr = dr;
        }
       
        private void setDrug(DataRow R)
        {
            try
            {
                lblDrugName.Text = R["DrugName"].ToString().Trim();
                lblDrugName.Tag = R["DrugCode"].ToString();
                this.Tag = R["IVRecodedDetaiID"].ToString();
                if (R["PiShi"].ToString() == "True")
                {
                    lblPiShi.Text = "皮试";
                    lblPiShi.ForeColor = Color.Red;
                }
                else
                    lblPiShi.Text = "------";
                lblSpec.Text = R["Spec"].ToString();
                label2.Text =getstr( R["Dosage"].ToString()) + R["DosageUnit"].ToString();
                UniPreparationID = R["UniPreparationID"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string getstr(string str)
        {

            //return str.Replace(".0000", "");

          return   str.TrimEnd('0').TrimEnd ('.');
        
        }
        private void lblDrugName_Click(object sender, EventArgs e)
        {
            //this.Parent.Focus();
            if (string.IsNullOrEmpty(UniPreparationID))
            {
                MessageBox.Show("无匹配药品,请维护", "提示");
            }
            else
            {
                LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
                mf.UniPreparationID = UniPreparationID;
                mf.ShowDialog();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "由于处方审过之后，瓶签上的药品就已经固定。所以原则上瓶签上药品不可删除，若院方一定要删除药品，删除后出现的任何问题，本公司概不负责!";
            if (MessageBox.Show(msg, "警告！", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                User u = new User();
                if (u.ShowDialog() == DialogResult.OK)
                {
                    int a = db.SetPIVAsDB(sql.DeleteLabelDrug(u.Tag.ToString(), this.labelno, this.Tag.ToString()));
                    if (a == 0)
                    {
                        MessageBox.Show("删除失败！");
                    }
                    else
                    {
                        MessageBox.Show("删除成功！");
                        this.Enabled = false;
                    }
                }
            }
           
         
        }

        private void CurrentDrug_Load(object sender, EventArgs e)
        {        
            setDrug(this.dr);
            if (dr["iscancel"].ToString() == "0")
                this.Enabled = false;
            else
                this.Enabled = true;
        }
        
    }
}
