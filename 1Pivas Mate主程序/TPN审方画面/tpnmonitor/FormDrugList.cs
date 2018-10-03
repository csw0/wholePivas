using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace tpnmonitor
{
    public partial class frmDrugList : Form
    {
        private BLPublic.DBOperate db = null;
        private DataTable tblDrug = null;

        public frmDrugList()
        {
            InitializeComponent();
        }

        public DataRow SelDrug { get; set; }

        public void init(BLPublic.DBOperate _db)
        {
            this.db = _db; 
        }

        /// <summary>
        /// 从接口加载员工
        /// </summary>
        private void loadDrug()
        {
            this.tblDrug = new DataTable();
            if (!this.db.GetRecordSet(SQL.SEL_DRUG, ref this.tblDrug))
            {
                BLPublic.Dialogs.Error("加载药品失败:" + this.db.Error);
                return;
            }

            listDrug();
        }

        private void listDrug(string _key="")
        {
            lvDrug.Items.Clear();
            ListViewItem item = null;
              
            if (!string.IsNullOrEmpty(_key))
                this.tblDrug.DefaultView.RowFilter = string.Format("CustomName LIKE '%{0}%' OR SpellCode LIKE '%{0}%'", _key);

            this.tblDrug.DefaultView.Sort = "CustomName";
            BLPublic.BLDataReader idr = new BLPublic.BLDataReader(this.tblDrug.DefaultView.ToTable().CreateDataReader());
            lvDrug.BeginUpdate();
            while(idr.next())
            {
                item = lvDrug.Items.Add(idr.getString("CustomName")); //, 
                item.SubItems.Add(idr.getString("SpecDesc"));
                item.Tag = idr.getString("CustomCode");
            }
            idr.close();
            lvDrug.EndUpdate();
        }

        private void selOK()
        {
            DataRow[] rows = this.tblDrug.Select("CustomCode='" + lvDrug.SelectedItems[0].Tag.ToString() + "'");
            if (0 < rows.Length)
                this.SelDrug = rows[0];

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }


        private void frmEmpList_Load(object sender, EventArgs e)
        {
            loadDrug();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            listDrug(txtKey.Text.Trim());
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (0 == lvDrug.SelectedItems.Count)
            {
                BLPublic.Dialogs.Alert("请选择药品.");
                lvDrug.Focus();
                return;
            }

            selOK();
        } 

        private void txtKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
                listDrug(txtKey.Text.Trim());
        }

        private void lvDrug_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (0 < lvDrug.SelectedItems.Count)
                selOK(); 
        }
    }
}
