using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasLabelSelect
{
    public partial class CancelMessage : UserControl
    {
        public CancelMessage()
        {
            InitializeComponent();
        }

        public void setmsg(string Type,DataRow row) 
        {
          
            if (Type=="配置取消") 
            {
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
            }
            else if (Type == "退药")
            {
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
            }
            else if (Type == "核对")
            {    
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
            }
        }
    }
}
