using System;
using System.Windows.Forms;
using PivasLabelCheckAll.dao;
using PIVAsCommon.Helper;

namespace PivasLabelCheckAll
{
    public partial class ChargeError : Form
    {
        #region 属性
        private seldb sel = new seldb();
        private DB_Help db = new DB_Help();
        private string Labelno;
        private string DrugQRCode;
        private string ErrorInfor;
        private string Errordt;
        private string DemployeeID;
        public bool noInfor = true;

        #endregion


        public ChargeError()
        {
            InitializeComponent();

        }


        private void ChargeError_Load(object sender, EventArgs e)
        {

        }

        public string labelno
        {
            get { return Labelno; }
            set { Labelno = value; }
        }

        public string drugQRCode
        {
            get { return DrugQRCode; }
            set { DrugQRCode = value; }
        }

        public string errorInfor
        {
            get { return ErrorInfor; }
            set { ErrorInfor = value; }
        }

        public string demployeeid
        {
            get { return DemployeeID; }
            set { DemployeeID = value; }
        }

        public string errordt
        {
            get { return Errordt; }
            set { Errordt = value; }
        }

        public void Showdgv()
        {
            dataGridView1.Rows.Add(Labelno, DrugQRCode,DemployeeID, ErrorInfor, Errordt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}
