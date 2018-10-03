using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PivasLabelCheckAll.common;

namespace PivasLabelCheckAll
{
    public partial class frmMessage : Form
    {
        //private ShowVoice speak = new ShowVoice();//读中文语音的公共类

        public frmMessage(string wname,string batch)
        {
            InitializeComponent();
            lblMsg.Text = wname;
            label2.Text = batch;
            //speak.Speak(spk);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
