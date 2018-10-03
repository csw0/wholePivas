using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class VolumeLimit : UserControl
    {
        DB_Help DB = new DB_Help();

        public VolumeLimit()
        {
            InitializeComponent();
        }
        public static Control LimitRow;

        public void Show(DataRow row)
        {
            lblID.Text = row["MPRule2ID"].ToString();
            batch.Text = row["OrderID"].ToString();
            lblLow.Text = row["LowMetric"].ToString();
            lblUp.Text = row["HighMetric"].ToString();
        }

        private void VolumeLimit_Click(object sender, EventArgs e)
        {
            LimitRow = this;
        }

        private void lblLow_Click(object sender, EventArgs e)
        {
            txtLow.Text = lblLow.Text;
            txtLow.Visible = true;
            lblLow.Visible = false;
            txtLow.Focus();
        }

        private void txtLow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (Convert.ToDouble(txtLow.Text) < Convert.ToDouble(lblUp.Text))
                    {
                        string str = "update OrderMCPRule2 set LowMetric = " + Convert.ToDouble(txtLow.Text) +
                            " where MPRule2ID = " + lblID.Text;
                        DB.SetPIVAsDB(str);
                        txtLow.Visible = false;
                        lblLow.Text = txtLow.Text;
                        lblLow.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("下限不可以大于上限！");
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void lblUp_Click(object sender, EventArgs e)
        {
            txtUp.Text = lblUp.Text;
            txtUp.Visible = true;
            lblUp.Visible = false;
            txtUp.Focus();
        }

        private void txtUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (Convert.ToDouble(lblLow.Text) < Convert.ToDouble(txtUp.Text))
                    {
                        string str = "update OrderMCPRule2 set HighMetric = " + Convert.ToDouble(txtUp.Text)+
                            " where MPRule2ID = " + lblID.Text;
                        DB.SetPIVAsDB(str);
                        txtUp.Visible = false;
                        lblUp.Text = txtUp.Text;
                        lblUp.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("下限不可以大于上限！");
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtLow_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtLow.Text) < Convert.ToDouble(lblUp.Text))
                {
                    string str = "update OrderMCPRule2 set LowMetric = " + Convert.ToDouble(txtLow.Text) +
                        " where MPRule2ID = " + lblID.Text;
                    DB.SetPIVAsDB(str);
                    txtLow.Visible = false;
                    lblLow.Text = txtLow.Text;
                    lblLow.Visible = true;
                }
                else
                {
                    MessageBox.Show("下限不可以大于上限！");
                    return;
                }
                txtLow.Visible = false;
                lblLow.Visible = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtUp_Leave(object sender, EventArgs e)
        {
            try
            {
            
                if (Convert.ToDouble(lblLow.Text) < Convert.ToDouble(txtUp.Text))
                {
                    string str = "update OrderMCPRule2 set HighMetric = " + Convert.ToDouble(txtUp.Text)+
                        " where MPRule2ID = " + lblID.Text;
                    DB.SetPIVAsDB(str);
                    txtUp.Visible = false;
                    lblUp.Text = txtUp.Text;
                    lblUp.Visible = true;
                }
                else
                {
                    MessageBox.Show("下限不可以大于上限！");
                    return;
                }
            
                txtUp.Visible = false;
                lblUp.Visible = true;
            }
                    catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VolumeLimit_Leave(object sender, EventArgs e)
        {
            txtUp.Visible = false;
            lblUp.Visible = true;
            txtLow.Visible = false;
            lblLow.Visible = true;
        }

   
    }
}
