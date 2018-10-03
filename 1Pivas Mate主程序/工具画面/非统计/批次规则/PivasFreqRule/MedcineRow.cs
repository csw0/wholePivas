using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    /// <summary>
    /// 药品控件
    /// </summary>
    public partial class MedcineRow : UserControl
    {

        DB_Help DB = new DB_Help();
        public static Control Med;
        public static string MedName;
        public static string MedCode;
        public static string MedID;
        public static string SeqNo;
        public static string status;
        public Panel p;
        public String medCode = "";

        public MedcineRow(Panel P)
        {
            InitializeComponent();
            p = P;
        }

        /// <summary>
        /// 控件单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedcineName_Click(object sender, EventArgs e)
        {
            this.Focus();
            MedName = lblMedName.Text;
            MedCode = lblMedCode.Text;
            MedID = lblMedID.Text;//保存当前选中的ID号
            SeqNo = lblSeqNo.Text;
            Med = this;
            MedicineChoiceBgColor();//选中背景色
            MedicineUnChoiceBgColor();//未选中背景色
        }

        private void setMed()
        {
            Med = this;
        }

        /// <summary>
        /// 选中时修改背景色
        /// </summary>
        private void MedicineChoiceBgColor()
        {
            this.BackColor = Color.FromArgb(140, 140, 255);
        }

        /// <summary>
        /// 未选中的恢复颜色
        /// </summary>
        private void MedicineUnChoiceBgColor()
        {
            foreach (Control c in p.Controls)
            {
                if (c is MedcineRow)
                {
                    MedcineRow medicine = (MedcineRow)c;
                    if (medicine.medCode != medCode)
                    {
                        medicine.BackColor = Color.White;
                    }
                }
            }  
        }

        public void SetMedcine(DataRow row)
        {
            if (row["IsMedClass"].ToString() == "True")
                lblMedName.Text = "<类>" + row["MedName"].ToString();
            else
                lblMedName.Text = row["MedName"].ToString();
            lblMedID.Text = row["MPRuleID"].ToString();
            lblSeqNo.Text = row["SeqNo"].ToString();
            lblMedCode.Text = row["MedCode"].ToString();
            lblIsClass.Text = row["IsMedClass"].ToString();
            medCode = row["MedCode"].ToString();
        }

        /// <summary>
        /// 控件双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Medcine_DoubleClick(object sender, EventArgs e)
        {
            MedList list = new MedList();
            MedName = lblMedName.Text;
            MedCode = lblMedCode.Text;
            MedID = lblMedID.Text;
            SeqNo = lblSeqNo.Text;
            status = "change";
            Med = this;

            switch (list.ShowDialog())
            {
                case DialogResult.OK:
                    lblMedCode.Text = MedList.MeCode;
                    lblMedName.Text = MedList.MeName;
                    lblIsClass.Text = Convert.ToString(MedList.IsClass);
            	break;
                case DialogResult.Yes:
                    lblMedCode.Text = MedList.MeCode;
                    lblMedName.Text = MedList.MeName;
                    lblIsClass.Text = "0";
                break;
            }
        }

        /// <summary>
        /// 设置药品信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="MedCode"></param>
        /// <param name="MedName"></param>
        /// <param name="SeqNo"></param>
        /// <param name="IsClass"></param>
        public void SetMed(string ID, string MedCode, string MedName, string SeqNo,string IsClass)
        {
            lblMedID.Text=ID;
            lblIsClass.Text = IsClass;
            lblMedCode.Text = MedCode;
            medCode = MedCode;//当前药品编号的赋值
            if(IsClass != "0")
                lblMedName.Text = "<类>" + MedName;
            else
                lblMedName.Text = MedName;
            lblSeqNo.Text = SeqNo;
        }

        private void pnlDelete_Click(object sender, EventArgs e)
        {
            try
            {               

                string str = "delete from OrderMPRule where MPRuleID = " + lblMedID.Text + "; " +
                            "delete from OrderMPRuleSub where MPRuleID = " + lblMedID.Text;
                DB.SetPIVAsDB(str);

                this.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
