using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class TimeRow : UserControl
    {
        public TimeRow()
        {
            InitializeComponent();

        }
        updatedb update;
        DateTime PreviousTime;
        seldb sel = new seldb();
        DataTable dt = new DataTable();
        int i=0;
        int k;
        private void TimeRow_Load(object sender, EventArgs e)
        {
            Start_Time.CustomFormat = "HH:mm";
            End_Time.CustomFormat = "HH:mm";
            
        }

        public void show(DataRow row)
        {
            k = sel.getTimeRule().Tables[0].Rows.Count;
            label1.Text = row[0].ToString();
            Start_Time.Text = row[2].ToString();
            End_Time.Text = row[3].ToString();
            if (row[4].ToString() == "True")
            {
                label2.Image = Properties.Resources.勾;
                label2.Tag = "1";
            }
            else
            {
                label2.Image = Properties.Resources.不选;
                label2.Tag = "0";
            }

            
            //if (label1.Text == "1")
            //{

            //}
            //else
            //{
            //   // PreviousTime = DateTime.Parse(sel.getHeadRule(label1.Text.ToString()).Tables[0].Rows[0][3].ToString());
            //    if (DateTime.Compare(Start_Time.Value, PreviousTime) < 0)
            //    {
            //        panel1.BackColor = Color.Yellow;
            //    }
            //}
            

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Start_Time.Visible = true;

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            End_Time.Visible = true;

        }

        private void Start_Time_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {


                    if (label1.Text == "1")
                    {
                        updatedb update = new updatedb();
                        update.Start_Time(label1.Text, Start_Time.Text.ToString(), false);
                    }
                    else
                    {
                        PreviousTime = DateTime.Parse(sel.getHeadRule(label1.Text.ToString()).Tables[0].Rows[0][3].ToString());
                        if (DateTime.Compare(Start_Time.Value, PreviousTime) > 0)
                        {
                            updatedb update = new updatedb();
                            update.Start_Time(label1.Text, Start_Time.Text.ToString(), false);
                            panel1.BackColor = Color.Transparent;
                        }
                        else
                        {
                           
                            panel1.BackColor = Color.Yellow;

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void End_Time_KeyPress(object sender, KeyPressEventArgs e)
        {
            updatedb update = new updatedb();
           
            dt = sel.getTimeRule().Tables[0];
            int last = dt.Rows.Count - 1;
            if (e.KeyChar == 13)
            {

                if (label1.Text.ToString() == dt.Rows[last][0].ToString() && label1.Text.ToString() != "1")
                {
                    DateTime First_time = DateTime.Parse(dt.Rows[0][2].ToString());
                    if (DateTime.Compare(End_Time.Value, First_time) >= 0)
                    {
                       
                        panel2.BackColor = Color.Yellow;
                    }
                    else
                    {
                        panel2.BackColor = Color.Transparent;
                        if (DateTime.Compare(End_Time.Value, Start_Time.Value) > 0)
                        {

                            update.End_Time(label1.Text.ToString(), End_Time.Text.ToString(), false);

                        }
                        else
                        {
                            update.End_Time(label1.Text.ToString(), End_Time.Text.ToString(), true);
                        }
                    }
                }
                else
                {
                    if (DateTime.Compare(End_Time.Value, Start_Time.Value) > 0)
                    {

                        update.End_Time(label1.Text.ToString(), End_Time.Text.ToString(), false);
                    }
                    else
                    {
                        update.End_Time(label1.Text.ToString(), End_Time.Text.ToString(), true);
                    }
                }
            }
        }






        private void label2_Click(object sender, EventArgs e)
        {
            update = new updatedb();
            if (label2.Tag.ToString() == "1")
            {
                label2.Image = Properties.Resources.不选;
                label2.Tag = "0";
                update.IsValid(label1.Text, false);
            }
            else
            {
                label2.Image = Properties.Resources.勾;
                label2.Tag = "1";
                update.IsValid(label1.Text, true);
            }
        }

        private void Start_Time_ValueChanged(object sender, EventArgs e)
        {
            i++;
            k = sel.getTimeRule().Tables[0].Rows.Count;
            if (i >=2)
            {
                
                if (DateTime.Compare(DateTime.Parse(Start_Time.Text), DateTime.Parse(End_Time.Text)) >= 0)
                {
                    panel1.BackColor = Color.Yellow;
                    panel2.BackColor = Color.Yellow;
                }
                else
                {
                    panel1.BackColor = Color.Transparent;
                    panel2.BackColor = Color.Transparent;
                }
                ((TimeRule)this.Parent.Parent).SelectLastPanel(int.Parse(label1.Text) - 2, Start_Time.Text);
            }
        }

        private void End_Time_ValueChanged(object sender, EventArgs e)
        {
            i++;
            k = sel.getTimeRule().Tables[0].Rows.Count;
            if(i>=3)
            { 
                
                if (DateTime.Compare(DateTime.Parse(Start_Time.Text), DateTime.Parse(End_Time.Text)) >=0)
                {
                    panel1.BackColor = Color.Yellow;
                    panel2.BackColor = Color.Yellow;
                }
                else
                {
                    panel1.BackColor = Color.Transparent;
                    panel2.BackColor = Color.Transparent;
                }
                ((TimeRule)this.Parent.Parent).SelectNextPanel(int.Parse(label1.Text), End_Time.Text);
               
            }
        }

    }
}
