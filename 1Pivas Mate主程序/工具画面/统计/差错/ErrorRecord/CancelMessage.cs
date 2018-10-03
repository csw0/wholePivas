using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasLabelSelect
{
    public partial class CancelMessage : UserControl
    {
        private ErrorRecord.ErrorAddOthers ea;
        DB_Help DB = new DB_Help();
        public CancelMessage()
        {
            InitializeComponent();
            checkBox1.Visible = false;
            
        }

        public CancelMessage(ErrorRecord.ErrorAddOthers ea)
        {
            InitializeComponent();
            this.ea=ea;
        }

        public void setmsg(string Type,DataRow row) 
        {
          
            if (Type=="配置取消") 
            {
                checkBox1.Name = row["LabelNo"].ToString();
                lb_name.Text = "配置取消 :";
                lb_name.ForeColor = Color.Red;
                if (row["LabelOverName"].ToString().Trim() == "" && row["LabelOverID"].ToString().Trim() == "")
                {
                    User.Text = "系统自动";
                }
                else if (row["LabelOverName"].ToString().Trim() == "" && row["LabelOverID"].ToString().Trim() != "")
                {
                    User.Text = row["LabelOverID"].ToString();
                }
                else if (row["LabelOverName"].ToString().Trim() != "")
                {
                    User.Text = row["LabelOverName"].ToString() + "(" + row["LabelOverDEmpCode"].ToString() + ")";
                }
                lb_time.Text = row["LabelOverTime"].ToString();
                lb_Type.Text = "";
                lb_Location.Text = "";
                //label2.Text = row["EID"].ToString();
            }
            else if (Type == "退药")
            {
                checkBox1.Name = row["LabelNo"].ToString();
                lb_name.ForeColor = Color.Red;
                if (row["WardRetreat"].ToString().Trim() == "1")
                { lb_name.Text = "退药 :"; }
                else if(row["WardRetreat"].ToString().Trim() == "2")
                { lb_name.Text = "退药 :"; }

                if (row["WardRName"].ToString().Trim() == "" && row["WardRID"].ToString().Trim() == "")
                {
                    User.Text = "未知";
                }
                else if (row["WardRName"].ToString().Trim() == "" && row["WardRID"].ToString().Trim() != "")
                {
                    User.Text = row["WardRID"].ToString();
                }
                else if (row["WardRName"].ToString().Trim() != "")
                {
                    User.Text = row["WardRName"].ToString() + "(" + row["WardRetreatDEmpCode"].ToString() + ")";
                }

                lb_time.Text =  row["WardRTime"].ToString();
                lb_Type.Text = "";
                lb_Location.Text = "";
                //label2.Text = row["EID"].ToString();
            }
            else if (Type == "核对")
            {
                checkBox1.Name = row["瓶签号"].ToString();
                lb_name.Text = row["核对"].ToString()+" :";
                if (row["核对"].ToString() == "拿药" || row["核对"].ToString() == "贴签")
                { lb_name.ForeColor = Color.Red; }
                if (row["姓名"].ToString().Trim() == "")
                { User.Text = "(" + row["EID"].ToString() + ")"; }
                else
                { User.Text = row["姓名"].ToString() + "(" + row["ID"].ToString() + ")"; }
                lb_time.Text = row["时间"].ToString();

                if (row["TP"].ToString().Trim()=="1")
                { lb_Type.Text  = "总瓶签"; }
                else if (row["TP"].ToString().Trim() == "0")
                { lb_Type.Text = "瓶签"; }
                else
                { lb_Type.Text = ""; }
                lb_Location.Text  = row["Location"].ToString();
                //label2.Text =  row["EID"].ToString() ;
            }
            else if (Type == "审方")
            {
                checkBox1.Name = row["医嘱号"].ToString();
                lb_name.Text = row["审方"].ToString() + " :";
                if (row["审方"].ToString() == "审方")
                { lb_name.ForeColor = Color.Red; }
                if (row["姓名"].ToString().Trim() == "")
                { User.Text = "(" + row["EID"].ToString() + ")"; }
                else
                { User.Text = row["姓名"].ToString() + "(" + row["ID"].ToString() + ")"; }
                lb_time.Text = row["时间"].ToString();

                if (row["TP"].ToString().Trim() == "1")
                { lb_Type.Text = "通过"; }
                else if (row["TP"].ToString().Trim() == "0")
                { lb_Type.Text = "未通过"; }
                else
                { lb_Type.Text = ""; }
                lb_Location.Text = row["Location"].ToString();
                //label2.Text = row["EID"].ToString();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string s = lb_name.Text;
            ea.stext = s.Substring(0, s.LastIndexOf(":"));
            ea.sname = User.Text;
            ea.stime = lb_time.Text;
            ea.sEid = label2.Text;
            if (checkBox1.Checked)
            {
                ea.c = ea.c + 1;
                //ea.b = 30;
                string strtype = "select distinct TypeCode,TypeName from ErrorRule where StatusName='" + ea.stext + "'";
                DataTable dtype = DB.GetPIVAsDB(strtype).Tables[0];
                if (dtype.Rows.Count == 0)
                {
                    checkBox1.Checked = false;
                    MessageBox.Show("没有找到相应类型，请维护规则！");
                }
                else
                {
                    comboBox1.DataSource = dtype;
                    comboBox1.ValueMember = "TypeCode";
                    comboBox1.DisplayMember = "TypeName";
                }
                ea.Topchange();
            }
            else
            {
                ea.c = ea.c - 1;
                ea.b = 0;
                ea.Topchange();
            }
        }
    }
}
