using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasRevPre
{
    public partial class PersonInfo : UserControl
    {
        public PersonInfo()
        {
            InitializeComponent();
        }
        public string PatCode;
        public void SetDetailInfo(DataTable dt)
        {
            lblAge.Text = dt.Rows[0]["Birthday"].ToString();
            lblBedNo.Text = dt.Rows[0]["BedNo"].ToString();
            lblCaseID.Text = dt.Rows[0]["CaseID"].ToString();
            PatCode = dt.Rows[0]["PatCode"].ToString();            
            lblPatient.Text = dt.Rows[0]["PatName"].ToString();
            lblWeight.Text = dt.Rows[0]["Weight"].ToString();

            string sex=dt.Rows[0]["Sex"].ToString().ToLower().Trim();
            if ("m" == sex)
                lblSex.Text = "男";
            else if ("f" == sex)
                lblSex.Text = "女";
            lblWard.Text = dt.Rows[0]["WardName"].ToString();
        }

        public void Clear()
        {
            lblAge.Text = "";
            lblBedNo.Text = "";
            lblCaseID.Text = "";
            PatCode = "";            
            lblPatient.Text = "";
            lblWeight.Text = "";
            lblSex.Text = "";
            lblWard.Text = "";
        }
    }
}
