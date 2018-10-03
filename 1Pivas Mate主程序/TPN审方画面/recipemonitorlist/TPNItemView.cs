using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Controls;

namespace recipemonitorlist
{
    public class TPNItemView
    {
        private BLPublic.DBOperate db = null;
        private ItemsControl icViewer = null;

        private bool isExpandTPN = false;
        private DataTable tblTPNItem = null;
        private DataTable tblComChkTPN = null;


        public TPNItemView(BLPublic.DBOperate _db, ItemsControl _viewer)
        {
            this.db = _db;
            this.icViewer = _viewer;

            loadTPNItems();
            loadComChkTPN(); 
        }

        /// <summary>
        /// 显示医嘱TPN项目
        /// </summary>
        /// <param name="_recipeID"></param>
        public void showRecipeTPN(string _recipeID)
        { 
            loadTPNValue(_recipeID);
            listTPNValue(this.isExpandTPN);
        }

        /// <summary>
        /// 显示TPN项目
        /// </summary>
        /// <param name="_isAll">是否显示全部</param>
        public void showTPN(bool _isAll)
        {
            listTPNValue(_isAll);
        }

        /// <summary>
        /// 获取TPN项目信息
        /// </summary>
        /// <param name="_itemID"></param>
        /// <returns></returns>
        public DataRow getTPNItem(int _itemID)
        {
            if (null == this.tblTPNItem)
                return null;
            else
                return this.tblTPNItem.Rows.Find(_itemID);
        }

        /// <summary>
        /// 清除医嘱相关信息
        /// </summary>
        public void clearView()
        {
            this.icViewer.Items.Clear();

            if (null != this.tblTPNItem)
                foreach (DataRow row in this.tblTPNItem.Rows)
                {
                    if (1000 <= Convert.ToInt32(row["SeqNo"].ToString()))
                        row["SeqNo"] = Convert.ToInt32(row["SeqNo"].ToString()) - 1000;

                    row["ItemValue"] = "";
                    row["ValueDiret"] = "";
                    row["ValueSubPer"] = "";
                    row["ResultOK"] = "";
                    row["NormalValue"] = row["OrgNormalValue"];
                }
        }
         
        /// <summary>
        /// 加载TPN项目
        /// </summary>
        private void loadTPNItems()
        {
            if (null == this.tblTPNItem)
                this.tblTPNItem = new DataTable();
            else
                this.tblTPNItem.Clear();

            if (this.db.GetRecordSet(SQL.SEL_TPNTEIM, ref this.tblTPNItem))
            {
                this.tblTPNItem.PrimaryKey = new DataColumn[] { this.tblTPNItem.Columns["TPNItemID"] };
                this.tblTPNItem.Columns.Add("ItemValue", typeof(string));
                this.tblTPNItem.Columns.Add("ValueDiret", typeof(string));
                this.tblTPNItem.Columns.Add("ValueSubPer", typeof(string));
                this.tblTPNItem.Columns.Add("ResultOK", typeof(string));
                this.tblTPNItem.Columns.Add("OrgNormalValue", typeof(string));

                foreach (DataRow r in this.tblTPNItem.Rows)
                    r["OrgNormalValue"] = r["NormalValue"];
            }
            else
                BLPublic.Dialogs.Error("加载TPN项目信息失败:" + this.db.Error);
        }
        
        /// <summary>
        /// 加载通用审核项目(无指定药品部分)，用于TPN项目列表显示顺序
        /// </summary>
        private void loadComChkTPN()
        {
            if (null == this.tblComChkTPN)
                this.tblComChkTPN = new DataTable();
            else
                this.tblComChkTPN.Clear();

            if (!this.db.GetRecordSet(SQL.SEL_ALWAY_CHK, ref this.tblComChkTPN))
            {
                //BLPublic.Dialogs.Error("加载通用审方失败:" + this.db.Error);
            }
        }

        /// <summary>
        /// 加载医嘱TPN项目值,审方通用项目
        /// </summary>
        /// <param name="_recipeID"></param>
        private void loadTPNValue(string _recipeID)
        {
            IDataReader dr = null;
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_ORDERSTPNVAL, _recipeID), ref dr))
            {
                BLPublic.Dialogs.Error("加载医嘱TPN项目失败:" + this.db.Error);
                return;
            }

            Dictionary<int, double> lstValuet = new Dictionary<int, double>(16);
            DataRow row = null;
            while (dr.Read())
            {
                row = this.tblTPNItem.Rows.Find(dr.GetInt32(0).ToString());
                if (null != row)
                {
                    if (dr.IsDBNull(1))
                        row["ItemValue"] = "";
                    else
                    {
                        row["ItemValue"] = dr.GetString(1);
                        if (BLPublic.Utils.IsNumeric(dr.GetString(1)))
                            lstValuet.Add(dr.GetInt32(0), Convert.ToDouble(dr.GetString(1)));
                    }
                }
            }
            dr.Close();

            //医嘱药品制剂
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_COMCHK_RT, _recipeID), ref dr))
            {
                BLPublic.Dialogs.Error("读取医嘱通用审核失败:" + this.db.Error);
                return;
            }

            bool hadResult = false;
            string valDire = "";
            string valSub = "";
            double per = 0;
            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
            while (bldr.next())
            {
                valDire = "";
                valSub = "";
                per = bldr.getFloat("DeviatePer");
                if (0 > per)
                {
                    valDire = "↓";
                    valSub = "- " + Math.Abs(per).ToString("p2");
                }
                else if (0 < per)
                {
                    valDire = "↑";
                    valSub = "+ " + per.ToString("p2");
                }

                row = this.tblTPNItem.Rows.Find(bldr.getInt("TPNItemID"));
                if (null != row)
                {
                    row["ValueDiret"] = valDire;
                    row["ValueSubPer"] = valSub;
                    row["ResultOK"] = string.IsNullOrWhiteSpace(valDire) ? "合格" : "不合格";
                    row["NormalValue"] = bldr.getString("NormalValue");
                    row["SeqNo"] = 1000 + bldr.getInt("SeqNo");
                }

                hadResult = true;
            }
            dr.Close();

            if (!hadResult)
                setComChk(_recipeID);
        }

        /// <summary>
        /// 设置通用审核项目
        /// </summary>
        private void setComChk(string _recipeID)
        {
            if (null == this.tblComChkTPN)
                return;

            DataTable tbl = new DataTable();
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_ORDERSPREP, _recipeID), ref tbl))
            {
                BLPublic.Dialogs.Error("读取医嘱匹配信息失败:" + this.db.Error);
                return;
            }

            List<DataRow> lstRow = new List<DataRow>();
            BLPublic.BLDataReader dr = new BLPublic.BLDataReader(tbl.CreateDataReader());
            while (dr.next())
            {
                DataRow[] rows = this.tblComChkTPN.Select("UniPreparationID=" + dr.getString("UniPreparationID"));
                if (null != rows)
                    foreach (DataRow r in rows)
                        lstRow.Add(r);
            }
            dr.close();

            if (0 == lstRow.Count)
            {
                DataRow[] rows = this.tblComChkTPN.Select("UniPreparationID=0 OR UniPreparationID IS NULL");
                if (null != rows)
                    foreach (DataRow r in rows)
                        lstRow.Add(r);
            }

            if (0 == lstRow.Count)
                return;

            DataRow row = null;
            foreach (DataRow r in lstRow)
            {
                row = this.tblTPNItem.Rows.Find(Convert.ToInt32(r["TPNItemID"].ToString()));
                if (null != row)
                {
                    row["NormalValue"] = r["NormalValue"];
                    row["SeqNo"] = 1000 + Convert.ToInt32(r["SeqNo"].ToString());
                }
            }
        }


        /// <summary>
        /// 显示TPN项目值
        /// </summary>
        /// <param name="_isAll"></param>
        private void listTPNValue(bool _isAll)
        {
            this.isExpandTPN = _isAll;
            this.icViewer.Items.Clear();

            TPNItemModel md = null;
            this.tblTPNItem.DefaultView.Sort = "SeqNo";

            this.tblTPNItem.DefaultView.RowFilter = "SeqNo>=1000";
            foreach (DataRow row in this.tblTPNItem.DefaultView.ToTable().Rows)
            {
                md = new TPNItemModel(Convert.ToInt32(row["TPNItemID"].ToString()),
                            row["ItemCode"].ToString(), row["ItemName"].ToString(), row["ItemValue"].ToString(),
                            row["Unit"].ToString());
                md.NormalValue = row["NormalValue"].ToString();
                md.ResultOK = row["ResultOK"].ToString();
                md.ValueDiret = row["ValueDiret"].ToString();
                md.ValueSubPer = row["ValueSubPer"].ToString();
                this.icViewer.Items.Add(md);
            }

            if (0 < this.icViewer.Items.Count)
                this.icViewer.Items.Add(new TPNItemModel(0, "", "", "", ""));

            this.tblTPNItem.DefaultView.RowFilter = "0<=SeqNo AND SeqNo<1000";
            foreach (DataRow row in this.tblTPNItem.DefaultView.ToTable().Rows)
            {
                md = new TPNItemModel(Convert.ToInt32(row["TPNItemID"].ToString()),
                            row["ItemCode"].ToString(), row["ItemName"].ToString(), row["ItemValue"].ToString(),
                            row["Unit"].ToString());

                md.ResultOK = row["ResultOK"].ToString();
                this.icViewer.Items.Add(md);
            }

            if (this.isExpandTPN)
            {
                this.tblTPNItem.DefaultView.RowFilter = "SeqNo<0";
                foreach (DataRow row in this.tblTPNItem.DefaultView.ToTable().Rows)
                {
                    md = new TPNItemModel(Convert.ToInt32(row["TPNItemID"].ToString()),
                                row["ItemCode"].ToString(), row["ItemName"].ToString(), row["ItemValue"].ToString(),
                                row["Unit"].ToString());
                    md.ResultOK = row["ResultOK"].ToString();
                    this.icViewer.Items.Add(md);
                }
                this.icViewer.Items.Add(new TPNItemModel(0, "_COLLAPSED_", " ↑[收缩]", "", ""));
            }
            else
                this.icViewer.Items.Add(new TPNItemModel(0, "_EXPAND_", " ↓[展开]", "", ""));
        }
         
    }


    /// <summary>
    /// 患者相关项目（检查项目）
    /// </summary>
    public class TPNItemModel : tpnmonitor.TPNItem
    {
        public TPNItemModel(int _id, string _code, string _name, string _value, string _unit) :
            base(_id, "", _code, _name, _value, _unit, 0)
        {
            this.IsSelected = false;
        }

        public TPNItemModel(tpnmonitor.TPNItem _item) :
            base(_item.ID, _item.Type, _item.Code, _item.Name, _item.Value, _item.Unit, 0)
        {
            this.IsSelected = false;
        }

        public bool IsSelected { get; set; }
        public string ResultOK { get; set; }
        public string ValueDiret { get; set; }
        public string ValueSubPer { get; set; }
    }
}
