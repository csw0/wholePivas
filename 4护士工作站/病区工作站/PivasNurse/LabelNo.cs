using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class LabelNo : UserControl
    {
        public LabelNo()
        {
            InitializeComponent();
        }
        public LabelNo(string type, int LabelOverFor)
        {
            InitializeComponent();
            this.type = type;
            this.LabelOverFor = LabelOverFor;
        }
        
        string type;
        int LabelOverFor;
        string CancelDrugStatus=null;
        public void add(DataRow row,bool today,int IsKCancel) 
        {
            DB_Help db=new DB_Help();
            checkBox1.Text = row["LabelNo"].ToString();
            label1.Text = row["Batch"].ToString();
            label3.Text = row["DrugName"].ToString();
            Boolean HaveK= label1.Text.Contains("K");
            if (!HaveK)//若是空包，无论什么状态都可以退药。
            {
                HaveK = label1.Text.Contains("k"); 
            }
            if (row["WardAct"].ToString() == "1")
            {
                CancelDrugStatus += "(配置取消已提交";
                this.Enabled = false;
            }
            else if (row["WardAct"].ToString() == "0")
            {
                CancelDrugStatus += "(提前打包已提交";
                this.Enabled = false;
            }
            if (row["CenterAct"].ToString() == "0")
            {
                CancelDrugStatus += "未确认)";
            }
            else if (row["CenterAct"].ToString() == "1")
            {
                CancelDrugStatus = null;
            }
            else if (row["CenterAct"].ToString() == "2")
            {
                CancelDrugStatus += "被忽略)";
            }
            int status = int.Parse(row["IVStatus"].ToString());
            switch (status)
            {
                case 0:label2.Text="等待打印"+CancelDrugStatus;
                    break;
                case 1: label2.Text = "等待打印" + CancelDrugStatus;
                    break;
                case 3: label2.Text = "已打印" + CancelDrugStatus;
                    break;
                case 5: label2.Text = "已排药" + CancelDrugStatus;
                    break;
                case 7: label2.Text = "已进仓" + CancelDrugStatus;
                    break;
                case 9: label2.Text = "配置完成" + CancelDrugStatus;
                    break;
                case 11: label2.Text = "已出仓" + CancelDrugStatus;
                    break;
                case 13: label2.Text = "已打包" + CancelDrugStatus;
                    break;
                case 15: label2.Text = "病区已签收" + CancelDrugStatus;
                    break;
                default:
                    break;
            }

            if (status >= LabelOverFor)
            {
                this.Enabled = false; 
            }
            try
            {
                if (today)
                {
                    string mm = label1.Text.Substring(0, 1);
                    int OrderID = Convert.ToInt32(mm);
                    string RetreadNot = db.GetPIVAsDB(" select RetreadNot from DOrder where OrderID='" + OrderID + "'").Tables[0].Rows[0][0].ToString();
                    if (RetreadNot != "")
                    {
                        if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(RetreadNot)) > 0)
                            this.Enabled = false;
                    }
                }
            }
            catch { }
            #region 空包任何时候都能退药(张家港用)
            if (IsKCancel == 1 && HaveK)
                this.Enabled = true;
            #endregion

            if (int.Parse(row["LabelOver"].ToString()) < 0 && row["WardRetreat"].ToString()=="0")
            {
                label2.Text = "配置取消";
                this.Enabled = false;
            }
            else if (row["WardRetreat"].ToString() == "1" || row["WardRetreat"].ToString() == "2") 
            {
                label2.Text = "已退药";
                this.Enabled = false;
            }
            if (row["PackTime"].ToString()!= "")
            {
                label2.Text = "已打包";
                this.Enabled = false;
            }
            //if (type == "DXTuiYao" && status < 3)
            //    this.Enabled = false;
            else if (type == "TiQianDB"&&HaveK)
                this.Enabled = false;
            if (label2.Text.Contains("未确认"))
            {
                this.Enabled = false;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (type == "DXTuiYao")
            {
                if (checkBox1.Checked == true&&checkBox1.Enabled==true)
                {
                    ((DXTuiYao)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent).button2.Enabled = true;
                }
                else
                {
                    ((DXTuiYao)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent).button2.Enabled =false;
                }
            }
            else if (type == "TiQianDB"&&checkBox1.Enabled==true) 
            {
                if (checkBox1.Checked == true)
                {
                    ((TiQianDB)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent).button2.Enabled = true;
                }
                else
                {
                    ((TiQianDB)this.Parent.Parent.Parent.Parent.Parent.Parent.Parent).button2.Enabled = false   ;
                }
            }
        }

       
    }
}
