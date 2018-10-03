using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace recipemonitor
{
    public partial class FormMain : Form
    {
        private const string SEL_ORDERS = "SELECT p.PatientCode, d.DeptName, p.BedNo, p.HospitalNo, " +
            "p.PatientName, p.Birthday, p.Sex, r.RecipeID, r.StartTime, r.UsageCode, r.FreqCode, " +
            "tcr.CheckLevel FROM hospitaldata.dbo.Orders r " +
            "INNER JOIN hospitaldata.dbo.Patient p ON r.PatientCode=p.PatientCode " +
            "LEFT JOIN tpn.dbo.TPNCheckRecord tcr ON (tcr.RecipeID=r.RecipeID AND tcr.IsValid=1) " +
            "LEFT JOIN hospitaldata.dbo.DDept d ON p.DeptCode=d.DeptCode " +
            "WHERE p.IsInHospital=1 AND r.OrdersLabel=4 AND (r.StopTime IS NULL OR r.StopTime>=GETDATE()) ";
        private const string SEL_ORDERS_NOCHK = SEL_ORDERS +
            "AND NOT EXISTS(SELECT 1 FROM tpn.dbo.TPNCheckRecord cr WHERE cr.RecipeID=r.RecipeID) " +
            "ORDER BY d.DeptName, p.BedNo";
        private const string SEL_ORDERS_ALL = SEL_ORDERS + " ORDER BY d.DeptName, p.BedNo";
        private const string SEL_ORDERSDETAIL = "SELECT DrugCode, DrugName, DrugSpec, Dosage, DosageUnit, " +
            "Quantity FROM hospitaldata.dbo.OrdersDetail WHERE RecipeID='{0}' ";
        private const string SEL_ORDERS_CHKLVL = "SELECT RecipeID, CheckLevel FROM tpn.dbo.TPNCheckRecord " +
            "WHERE RecipeID IN({0}) AND IsValid=1";
        private const string DEL_TPNMNT_BYRCP = "DELETE FROM tpn.dbo.TPNCheckUseItem WHERE TPNCheckResultID " +
            "IN(SELECT TPNCheckResultID FROM tpn.dbo.TPNCheckResult cr WHERE cr.RecipeID IN({0})); " +
            "DELETE FROM tpn.dbo.TPNCheckResult WHERE RecipeID IN({0}); " +
            "DELETE FROM tpn.dbo.TPNAlwayChkResult WHERE RecipeID IN({0}); ";


        private BLPublic.DBOperate db = null;
        private tpnmonitor.TPNMonitor tpnMnt = null;


        public FormMain()
        {
            InitializeComponent();

            lblNum.Text = "";
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.db = new BLPublic.DBOperate("bl_server.lcf", "CPMATE");
            if (!this.db.IsConnected)
                BLPublic.Dialogs.Error("连接服务器失败:" + this.db.Error);
                 
            else
                loadOrdres(); 
        }

        /// <summary>
        /// 设置医嘱行背景颜色
        /// </summary>
        /// <param name="_item"></param>
        private void setItemBack(ListViewItem _item)
        {
            if (null == _item)
                return;

            string lvl = _item.SubItems[9].Text;
            if ("5".Equals(lvl))
                _item.BackColor = Color.FromArgb(0xFF, 0xC0, 0xCB);
            else if ("3".Equals(lvl))
                _item.BackColor = Color.FromArgb(0xFF, 0xFF, 0x80);
            else if ("0".Equals(lvl))
                _item.BackColor = Color.FromArgb(0x98, 0xFF, 0x98);
            else
                _item.BackColor = lvOrders.BackColor;
        }

        private void loadOrdres()
        {
            lvOrders.Items.Clear();

            string sql = "";
            if (cbAll.Checked)
                sql = SEL_ORDERS_ALL;
            else
                sql = SEL_ORDERS_NOCHK;

            IDataReader dr = null;
            if (!this.db.GetRecordSet(sql, ref dr))
            {
                BLPublic.Dialogs.Error("加载医嘱失败:" + this.db.Error);
                return;
            }

            string ageStr = "";
            int ageMonths = 0;
            int bdIndx = dr.GetOrdinal("Birthday");
            int lvl = 0;
            ListViewItem item = null;

            while (dr.Read())
            {
                if (dr.IsDBNull(bdIndx))
                    ageStr = "";
                else
                    ageStr = BLPublic.Utils.getAge(dr.GetDateTime(bdIndx), out ageMonths);

                if (dr.IsDBNull(dr.GetOrdinal("CheckLevel")))
                    lvl = -1;
                else 
                    lvl = Convert.ToInt16(dr["CheckLevel"].ToString());

                item = lvOrders.Items.Add(dr.GetDateTime(dr.GetOrdinal("StartTime")).ToString("yyyy-MM-dd HH:mm"));
                item.SubItems.Add(dr["DeptName"].ToString().Trim());
                item.SubItems.Add(dr["BedNo"].ToString());
                item.SubItems.Add(dr["PatientName"].ToString().Trim());
                item.SubItems.Add(ageStr);
                item.SubItems.Add("f".Equals(dr["Sex"].ToString()) ? "女" : "男");
                item.SubItems.Add(dr["RecipeID"].ToString());
                item.SubItems.Add(dr["UsageCode"].ToString());
                item.SubItems.Add(dr["FreqCode"].ToString());
                item.SubItems.Add(lvl.ToString());
                item.Tag = new OrdersModel(dr["PatientCode"].ToString(), item.SubItems[5].Text, ageMonths, 
                                           dr["RecipeID"].ToString());

                setItemBack(item); 
            }

            dr.Close();

            lblNum.Text = lvOrders.Items.Count.ToString() + "条";
        }

        /// <summary>
        /// 更新审方结果
        /// </summary>
        private void refCheckLevel(string _rcpIDs)
        {
            if (string.IsNullOrWhiteSpace(_rcpIDs))
                return;

            if (',' == _rcpIDs[_rcpIDs.Length - 1])
                _rcpIDs = _rcpIDs.Substring(0, _rcpIDs.Length - 1);


            IDataReader dr = null;
            Dictionary<string, int> lstChk = new Dictionary<string,int>();
            string rcpID = "";
            int lvl = 0;
            if (this.db.GetRecordSet(string.Format(SEL_ORDERS_CHKLVL, _rcpIDs), ref dr))
            {
                while (dr.Read())
                {
                    rcpID = dr["RecipeID"].ToString();
                    if (dr.IsDBNull(1))
                        lvl = 0;
                    else
                        lvl = Convert.ToInt32(dr["CheckLevel"].ToString());

                    if (lstChk.ContainsKey(rcpID))
                    {
                        if (lstChk[rcpID] < lvl)
                            lstChk[rcpID] = lvl;
                    }
                    else
                        lstChk.Add(rcpID, lvl);
                }

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载审方结果失败:" + this.db.Error);

            if (0 == lstChk.Count)
                return;
             
            foreach(ListViewItem item in lvOrders.Items)
                if (lstChk.ContainsKey(item.SubItems[6].Text))
                { 
                    item.SubItems[9].Text = lstChk[item.SubItems[6].Text].ToString();
                    setItemBack(item);  
                }

            lvOrders.Refresh();
        }

        private void lvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvDetail.Items.Clear();

            if (0 == lvOrders.SelectedItems.Count)
                return;

            OrdersModel odr = (OrdersModel)lvOrders.SelectedItems[0].Tag;

            IDataReader dr = null;
            if (this.db.GetRecordSet(string.Format(SEL_ORDERSDETAIL, odr.RecipeID), ref dr))
            {
                ListViewItem item = null;
                while (dr.Read())
                {
                    item = lvDetail.Items.Add(dr["DrugName"].ToString());
                    item.SubItems.Add(dr["DrugSpec"].ToString());
                    item.SubItems.Add(BLPublic.Utils.trimZero(dr["Dosage"].ToString()) + dr["DosageUnit"].ToString());
                    item.SubItems.Add(BLPublic.Utils.trimZero(dr["DrugSpec"].ToString()));
                }

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载医嘱药品失败:" + this.db.Error);
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            cbSelAll.Checked = false;
            lvDetail.Items.Clear();
            loadOrdres(); 
        }

        private void cbSelAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach(ListViewItem item in lvOrders.Items)
            {
                item.Checked = cbSelAll.Checked;
            } 
        }

        private void btnMonitor_Click(object sender, EventArgs e)
        {
            if (null != btnMonitor.Tag)
            {
                btnMonitor.Tag = null;
                btnMonitor.Enabled = false;
            }

            txtInfo.Text = "";
            bool hadChk = false;
            string delRtRcp = "";

            foreach(ListViewItem item in lvOrders.Items) 
                if (item.Checked)
                {
                    hadChk = true;
                    if (!"-1".Equals(item.SubItems[9].Text))
                        delRtRcp += "'" + item.SubItems[6].Text + "',";
                }
            
            if (!hadChk)
            {
                BLPublic.Dialogs.Alert("选择要审方的医嘱.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(delRtRcp))
            {
                if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("当前选医嘱中有已经审方的医嘱，重审将删除原来的审方记录了。\r\n是否继续?"))
                    return;

                delRtRcp = delRtRcp.Substring(0, delRtRcp.Length - 1);
                if (!this.db.ExecSQL(string.Format(DEL_TPNMNT_BYRCP, delRtRcp)))
                {
                    BLPublic.Dialogs.Alert("删除TPN审方记录失败:" + this.db.Error); 
                    return;
                } 
            }


            if (null == this.tpnMnt)
            {
                this.tpnMnt = new tpnmonitor.TPNMonitor();
                this.tpnMnt.init(this.db, "9999");
            }

            string mntRcps = "";
            btnMonitor.Tag = 1;
            btnMonitor.Text = "取消";
            btnMonitor.Refresh();
            Application.DoEvents();

            foreach(ListViewItem item in lvOrders.Items)
                if (item.Checked)
                {
                    if (null == btnMonitor.Tag)
                        break;

                    OrdersModel odr = (OrdersModel)item.Tag;

                    bool rt = this.tpnMnt.Monitor(odr.PatientCode, odr.AgeMonth, odr.Sex, odr.RecipeID);
                    if (rt)
                    {
                        this.tpnMnt.saveTPNValue();
                        mntRcps += "'" + odr.RecipeID + "',";
                        item.Checked = false;

                        txtInfo.Text += "[" + odr.RecipeID + "]:成功." + this.tpnMnt.getError() + "\r\n";
                    }
                    else
                        txtInfo.Text += "[" + odr.RecipeID + "]:" + this.tpnMnt.getError() + "\r\n";

                    Application.DoEvents();
                }

            if (!string.IsNullOrWhiteSpace(mntRcps))
                refCheckLevel(mntRcps);

            btnMonitor.Enabled = true;
            btnMonitor.Tag = null;
            btnMonitor.Text = "审方";
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (null != this.db) this.db.Close();
            if (null != this.tpnMnt) this.tpnMnt.clear();
        }
    }

    public class OrdersModel
    {
        public OrdersModel(string _pcode, string _sex, int _ageMonth, string _rcpID)
        {
            this.PatientCode = _pcode;
            this.Sex = _sex;
            this.AgeMonth = _ageMonth;
            this.RecipeID = _rcpID;
        }

        public string PatientCode { get; set; }
        public string Sex { get; set; } 
        public int AgeMonth { get; set; }
        public string RecipeID { get; set; } 
    }
}
