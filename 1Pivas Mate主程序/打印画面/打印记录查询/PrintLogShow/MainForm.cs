using PivasLimitDES;
using System;
using System.Windows.Forms;

namespace PrintLogShow
{
    public partial class MainForm : Form
    {
        private string userID;
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(string userID)
        {
            this.userID = userID;
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!GetPivasLimit.Instance.Limit(userID, "PrintLogShow"))
            {
                MessageBox.Show("您没有权限，请联系管理员！");
                this.FindForm().Dispose();
            }
            mainShow1.useid = userID;
        }
    }
}
