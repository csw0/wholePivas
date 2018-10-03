using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class UDFreqRule : UserControl
    {
        public UDFreqRule()
        {
            InitializeComponent();
        }
        updatedb  update = new updatedb();
        seldb sel;
        string OrderID ="0";
        string WardCode = string.Empty;
        public void rowFreqRule(DataRow row,string wardcode) 
        {
            OrderShow();
            label1.Text = row["FreqSubCode"].ToString();
            label2.Text = row["FreqCode"].ToString();
            Comb_Order.Text = row["OrderID"].ToString();
            OrderID = Comb_Order.Text.Trim();
            dtp.Text = row["UseTime"].ToString();
            //label5.Text= row["OrderID"].ToString();
            label6.Text = row["FreqSubName"].ToString();
            WardCode = wardcode;
            if (row["IfCompound"].ToString() == "True")
            {
                label3.Image=Properties.Resources.勾;
                label3.Tag = "1";
            }
            else 
            { 
                label3.Tag = "0";
                label3.Image = Properties.Resources.不选;
            }

            if (row["IfFixed"].ToString() == "True")
            {
                label4.Image = Properties.Resources.勾;
                label4.Tag = "1";
            }
            else
            {
                label4.Tag = "0";
                label4.Image = Properties.Resources.不选;
            }

            if (row["IsCrossDay"].ToString() == "True")
            {
                label5.Image = Properties.Resources.勾;
                label5.Tag = "1";
            }
            else
            {
                label5.Tag = "0";
                label5.Image = Properties.Resources.不选;
            }
            this.dtp.ValueChanged += new System.EventHandler(this.dtp_ValueChanged);
        }


        private void OrderShow()
        {
            sel = new seldb();
            Comb_Order.DataSource=sel.getOrder().Tables[0];
            Comb_Order.DisplayMember = "OrderID";
        }


        private void Comb_Order_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!OrderID.Trim().Equals(Comb_Order.Text.ToString().Trim()) && !OrderID.Equals("0"))
            {
                string code = label1.Text;           
                string orderID = Comb_Order.Text.ToString().Trim();
                if (WardCode.Length > 0)
                {
                    if (update.updateOrderID(code, orderID, 1, WardCode) > 0)
                    {
                        OrderID = orderID;
                    }
                }
                else
                {
                   if(update.updateOrderID(code, orderID, 0, "")>0)
                    {
                        OrderID = orderID;
                    }
                }
            }
        }
 
        private void panel2_Click(object sender, EventArgs e)
        {
          
            if (label4.Tag.ToString() == "1")
            {
                label4.Image = Properties.Resources.不选;
                label4.Tag = "0";
                update.updateIfFixed(label1.Text.ToString(), "False",WardCode);
            }
            else
            {
                label4.Image = Properties.Resources.勾 ;
                label4.Tag = "1";
                update.updateIfFixed(label1.Text.ToString(), "True",WardCode);
            }
        }

       

        private void label5_Click(object sender, EventArgs e)
        {        
            if (label5.Tag.ToString() == "1")
            {
                label5.Image = Properties.Resources.不选;
                label5.Tag = "0";
                update.updateIsCrossDay(label1.Text.ToString(), "False", WardCode);
            }
            else
            {
                label5.Image = Properties.Resources.勾;
                label5.Tag = "1";
                update.updateIsCrossDay(label1.Text.ToString(), "True", WardCode);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (label3.Tag.ToString() == "1")
            {
                label3.Image = Properties.Resources.不选;
                label3.Tag = "0";
                update.updateIfCompound(label1.Text.ToString(), "False", WardCode);
            }
            else
            {
                label3.Image = Properties.Resources.勾;
                label3.Tag = "1";
                update.updateIfCompound(label1.Text.ToString(), "True", WardCode);
            }
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            update.updateUseTime(label1.Text.ToString(), dtp.Value.ToString("HH:mm"), WardCode);
        }

    

     
    }
}
