using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace synpivasdata
{
    public partial class frmMain : Form
    {
        private SynPivasData synPva = null;


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblInfo.Text = "";
        }
         

        private void showInfo(string _txt)
        {
            lblInfo.Text = _txt;
            this.Refresh();
            Application.DoEvents();
        }

        private void btnSynDrug_Click(object sender, EventArgs e)
        {
            if (null == this.synPva)
                this.synPva = new SynPivasData(Application.StartupPath);

            btnSynDrug.Enabled = false;
            showInfo("开始同步药品...");

            if (this.synPva.synDrug())
                showInfo("同步药品结束");
            else
                showInfo(this.synPva.Error);
            btnSynDrug.Enabled = true;
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            if (null == this.synPva)
                this.synPva = new SynPivasData(Application.StartupPath);

            btnPatient.Enabled = false;
            showInfo("开始患者...");

            if (this.synPva.synPatient())
                showInfo("同步患者结束");
            else
                showInfo(this.synPva.Error);

            btnPatient.Enabled = true;
        }
    }
}
