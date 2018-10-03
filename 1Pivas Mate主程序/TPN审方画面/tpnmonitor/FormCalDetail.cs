using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace tpnmonitor
{
    public partial class frmCalDetail : Form
    {
        private bool isMockModel = false;  //是否模拟模式
        private BLPublic.DBOperate db = null;
        private TPNMonitor monitor = null;
        private DataTable tblDrus = null;
        private DataTable tblTPN = null;
        private string recipeID = null;
        private string itemCode = null;

        [DllImport("user32.dll", EntryPoint = "GetScrollPos")]
        public static extern int GetScrollPos(IntPtr hwnd, int nBar); 

        public frmCalDetail()
        {
            InitializeComponent();
             
            lblItem.Text = "";
            lblExpress.Text = "";
            lblResult.Text = "";
            lvDrugs.SelectedIndexChanged -= new EventHandler(lvDrugs_SelectedIndexChanged);
             
            //chrtParam.Series["Series1"]["PieLabelStyle"] = "Outside";//将文字移到外侧
            //chrtParam.Series["Series1"]["PieLineColor"]  = "Black";//绘制黑色的连线。
        }

        /// <summary>
        /// 初始化显示项目
        /// </summary>
        /// <param name="_db"></param>
        /// <param name="_recipeID"></param>
        /// <param name="_itemCode"></param>
        public void init(BLPublic.DBOperate _db, string _recipeID, string _itemCode)
        {
            this.db = _db;
            this.recipeID = _recipeID;
            this.itemCode = _itemCode;

            btnAddDrug.Hide();
            btnDelDrug.Hide();
            sptRight.Hide();
            lvTPN.Hide();
            this.Width -= sptRight.Width + lvTPN.Width;

            init();
        }

        /// <summary>
        /// 初始化处方模拟
        /// </summary>
        public void initMockRecipe(BLPublic.DBOperate _db, string _recipeID)
        { 
            this.db = _db;
            this.recipeID = _recipeID;
            this.isMockModel = true;
            lvDrugs.SelectedIndexChanged += new EventHandler(lvDrugs_SelectedIndexChanged);

            init();
        }

        private void init()
        { 
            if (null == this.db)
                return;

            this.monitor = new TPNMonitor();
            this.monitor.init(this.db, "system");
        }

        /// <summary>
        /// 加载医嘱药品
        /// </summary>
        private void loadDrug()
        { 
            lvDrugs.Items.Clear();

            if (null == this.tblDrus)
                this.tblDrus = new DataTable();
            else
                this.tblDrus.Clear();

            if (!this.db.GetRecordSet(string.Format(SQL.SEL_ORDERSPREP_WDRUG, this.recipeID), ref this.tblDrus))
            {
                lvDrugs.Items.Add("加载处方失败:" + this.db.Error);
                return;
            }

            ListViewItem item = null;

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(this.tblDrus.CreateDataReader());
            while (bldr.next())
            {
                item = lvDrugs.Items.Add(bldr.getString("DrugName").Trim());
                item.Tag = new BLPublic.IDCodeItem(bldr.getInt("UniPreparationID"), bldr.getString("DrugCode"));
                item.SubItems.Add(bldr.getString("DrugSpec").Trim());
                item.SubItems.Add(BLPublic.Utils.trimZero(bldr.getString("Dosage")) + ' ' +
                                  fillEnd(bldr.getString("DosageUnit").Trim(), 3));
                item.SubItems.Add("-");
                item.SubItems.Add("-"); 
            }
            bldr.close();

            this.tblDrus.PrimaryKey = new DataColumn[] { this.tblDrus.Columns["DrugCode"] }; 
        }

        /// <summary>
        /// 加载TPN参数
        /// </summary>
        private void loadTPN()
        { 
            lvTPN.Items.Clear();
             
            //TPN参数
            if (null == this.tblTPN) 
                this.tblTPN = new DataTable();
            else
                this.tblTPN.Clear();

            if (!this.db.GetRecordSet(SQL.SEL_TPNTEIM, ref this.tblTPN))
            {
                lvTPN.Items.Add("加载TPN参数失败:" + this.db.Error);
                return;
            }
            this.tblTPN.PrimaryKey = new DataColumn[] { this.tblTPN.Columns["TPNItemID"] };
            if (!this.tblTPN.Columns.Contains("Value"))
                this.tblTPN.Columns.Add("Value", typeof(string)); 
        }

        /// <summary>
        /// 获取通用审核参数
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, TPNItemMonitor> getComChkTPN()
        {
            //获取通用审核
            DataTable tblComChkTPN = new DataTable();
            if (!this.db.GetRecordSet(SQL.SEL_ALWAY_CHK, ref tblComChkTPN))
            {
                lvTPN.Items.Add("加载通用审方失败:" + this.db.Error);
                return null;
            }

            string prepIDs = "";
            foreach (ListViewItem i in lvDrugs.Items)
                if (null != i.Tag)
                    prepIDs += ((BLPublic.IDCodeItem)i.Tag).ID.ToString() + ",";

            if (string.IsNullOrWhiteSpace(prepIDs))
                return null;

            //与医嘱匹配的通用审核
            prepIDs = prepIDs.Substring(0, prepIDs.Length - 1);
            tblComChkTPN.DefaultView.RowFilter = "UniPreparationID IN(" + prepIDs + ")";
            if (0 == tblComChkTPN.DefaultView.Count)
                tblComChkTPN.DefaultView.RowFilter = "UniPreparationID=0 OR UniPreparationID IS NULL";

            tblComChkTPN.DefaultView.Sort = "SeqNo";
            tblComChkTPN = tblComChkTPN.DefaultView.ToTable();

            Dictionary<int, TPNItemMonitor> dicComTPN = new Dictionary<int, TPNItemMonitor>();
            for(int i = 0; i < tblComChkTPN.Rows.Count; i++)
                dicComTPN.Add(Convert.ToInt32(tblComChkTPN.Rows[i]["TPNItemID"].ToString()),
                              new TPNItemMonitor(0, Convert.ToInt32(tblComChkTPN.Rows[i]["SeqNo"].ToString()),
                                                 tblComChkTPN.Rows[i]["NormalValue"].ToString()));
            
            tblComChkTPN.Clear();

            return dicComTPN;
        }

        /// <summary>
        /// 显示TPN参数
        /// </summary>
        private void listTPNItem(Dictionary<int, TPNItemMonitor> _dicComChkTPN)
        {
            lvTPN.BeginUpdate();
            lvTPN.Items.Clear();

            ListViewItem item = null;

            if (null != _dicComChkTPN)
            {
                //先显示通用审核TPN参数
                DataRow row = null;
                foreach (KeyValuePair<int, TPNItemMonitor> kv in _dicComChkTPN)
                {
                    row = tblTPN.Rows.Find(kv.Key);
                    if (null != row)
                    {
                        item = lvTPN.Items.Add(row["ItemName"].ToString());
                        item.Tag = row["ItemCode"].ToString();
                        item.SubItems.Add(row["Value"].ToString() + row["Unit"].ToString());
                        item.SubItems.Add(kv.Value.Domain);

                        item.UseItemStyleForSubItems = false;
                        if (0 > kv.Value.DeviateValue)
                        {
                            item.SubItems.Add("↓");
                            item.SubItems[3].ForeColor = Color.Coral;
                        }
                        else if (0 < kv.Value.DeviateValue)
                        {
                            item.SubItems.Add("↑");
                            item.SubItems[3].ForeColor = Color.Coral;
                        }
                        else
                        {
                            item.SubItems.Add("合格");
                            item.SubItems[3].ForeColor = Color.FromArgb(128, 255, 128);
                        }

                         
                    } 
                }

                item = lvTPN.Items.Add("");
                item.Tag = null;
                item.SubItems.Add("");
            }

            tblTPN.DefaultView.Sort = "SeqNo DESC";
            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tblTPN.DefaultView.ToTable().CreateDataReader());
            while (bldr.next())
            {
                if (!_dicComChkTPN.ContainsKey(bldr.getInt("TPNItemID")))
                {
                    item = lvTPN.Items.Add(bldr.getString("ItemName"));
                    item.Tag = bldr.getString("ItemCode");
                    item.SubItems.Add(bldr.getString("Value") + bldr.getString("Unit"));
                    item.SubItems.Add(bldr.getString("NormalValue"));
                    item.SubItems.Add("");
                }
            }
            bldr.close();
            lvTPN.EndUpdate();
        }

        private bool calTPNItemByCheckDrug()
        {
            DataTable tblCalDrugs = this.tblDrus.Clone();

            DataRow row = null;
            string dosage = null;
            int p = 0;
            foreach (ListViewItem item in lvDrugs.Items)
            {
                if (null != item.Tag)
                {
                    row = this.tblDrus.Rows.Find(((BLPublic.IDCodeItem)item.Tag).Code);
                    if (null != row)
                    {
                        dosage = item.SubItems[2].Text.Trim();
                        p = dosage.IndexOf(' ');
                        if (0 < p)
                            row["Dosage"] = Convert.ToDouble(dosage.Substring(0, p).Trim());
                        else
                            row["Dosage"] = 0;

                        tblCalDrugs.ImportRow(row);
                    }
                }
            }

            if (0 == tblCalDrugs.Rows.Count)
            {
                lvTPN.BeginUpdate();
                foreach (ListViewItem item in lvTPN.Items)
                    item.SubItems[1].Text = "";

                lvTPN.EndUpdate();
                return false;
            }

            if (!this.monitor.calculatorDrugs(tblCalDrugs))
            {
                lblResult.Text = this.monitor.getError();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 计算TPN参数值
        /// </summary>
        private void calTPN()
        {
            if (!calTPNItemByCheckDrug())
                return;
             
            Dictionary<string, string> itemInfo = new Dictionary<string, string>(8);
            Dictionary<int, double> tpnValue = new Dictionary<int, double>();
            foreach (DataRow r in this.tblTPN.Rows)
            {
                itemInfo.Clear();
                this.monitor.getTPNItemInfo(r["ItemCode"].ToString(), itemInfo);
                if (0 < itemInfo.Count)
                {
                    r["Value"] = itemInfo["Value"];
                    tpnValue.Add(Convert.ToInt32(itemInfo["TPNItemID"]), Convert.ToDouble(itemInfo["Value"]));
                }
                else
                    r["Value"] = "";
            }

            chkComCheckTPN(tpnValue);
        }

        private void chkComCheckTPN(Dictionary<int, double> _tpnValue)
        {
            List<int> lstPrep = new List<int>();
            Dictionary<int, TPNItemMonitor> lstResult = new Dictionary<int, TPNItemMonitor>();

            foreach (ListViewItem i in lvDrugs.Items)
                if (null != i.Tag)
                    lstPrep.Add(((BLPublic.IDCodeItem)i.Tag).ID);

            if (this.monitor.checkComTPN(this.db, lstPrep, _tpnValue, lstResult))
                listTPNItem(lstResult);

            else
            {
                listTPNItem(null);
                BLPublic.Dialogs.Error("审核通用项目失败:" + this.monitor.getError());
            }
        }
         

        /// <summary>
        /// 获取列汇总
        /// </summary>
        /// <param name="_col"></param>
        /// <returns></returns>
        private double getColTotal(int _col)
        {
            if (4 > _col || (_col >= lvDrugs.Columns.Count))
                return 0;

            double rt = 0;
            string txt = "";
            int p = 0;
            for (int i = lvDrugs.Items.Count - 1; i >= 0; i--)
            {
                txt = lvDrugs.Items[i].SubItems[_col].Text;

                if (!string.IsNullOrWhiteSpace(txt) && !"-".Equals(txt))
                {
                    p = txt.IndexOf(' ');
                    if (0 <= p)
                        rt += Convert.ToDouble(txt.Substring(0, p));
                    else
                        rt += Convert.ToDouble(txt); 
                }
            }

            return rt;
        }

        /// <summary>
        /// 显示单个TPN项目信息
        /// </summary>
        private void showTPNItem()
        {
            showContent();
            showChart();
        }

        /// <summary>
        /// 显示参数值
        /// </summary>
        private void showContent()
        {
            if (string.IsNullOrWhiteSpace(this.itemCode))
            {
                lblItem.Text = "未指定TPN项目";
                return;
            }
            
            Dictionary<string, string> itemInfo = new Dictionary<string, string>(); 
            this.monitor.getTPNItemInfo(this.itemCode, itemInfo);
            if (0 == itemInfo.Count)
            {
                lblItem.Text = "项目" + this.itemCode + "信息丢失";
                return;
            } 
             
            Dictionary<string, string[]> expItems = new Dictionary<string, string[]>(); //计算项目信息[0]对应列下标, [1]单位 
            string express = "";
             
            lblItem.Text = itemInfo["ItemName"];
            express = itemInfo["Express"];
             
            ColumnHeader col;
            if (string.IsNullOrWhiteSpace(express))
            {
                lblExpress.Text = "<药品所含量累加>";
                col = lvDrugs.Columns.Add(itemInfo["ItemName"], 60, HorizontalAlignment.Right);
                col.Tag = this.itemCode;
                expItems.Add(this.itemCode, new string[] { col.Index.ToString(), itemInfo["Unit"] });
            }
            else
            {
                //替换计算表达式项目名称
                string expName = express; 
                List<string> items = new List<string>();

                TPNCalculator.getExpressItem(express, items); 
                foreach (string itemCode in items)
                {
                    if ("recipe.capacity".Equals(itemCode))
                    {
                        expItems.Add(itemCode, new string[] { "4", "ml" });
                        expName = expName.Replace(itemCode, "医嘱容积");
                    }
                    else if ("patient.weight".Equals(itemCode))
                    {
                        expItems.Add(itemCode, new string[] { "-1", "kg" });
                        expName = expName.Replace(itemCode, "体重");
                    }
                    else
                    {
                        itemInfo.Clear();
                        this.monitor.getTPNItemInfo(itemCode, itemInfo);
                        if (1 <= itemInfo.Count)
                        {
                            expName = expName.Replace(itemCode, itemInfo["ItemName"]);
                            if ("comp".Equals(itemInfo["ItemType"]) || "thing".Equals(itemInfo["ItemType"]))
                            {
                                col = lvDrugs.Columns.Add(itemInfo["ItemName"], 80, HorizontalAlignment.Right);
                                col.Tag = itemCode;

                                expItems.Add(itemCode, new string[] { col.Index.ToString(), itemInfo["Unit"] });
                            }
                            else
                                expItems.Add(itemCode, new string[] { "-1", itemInfo["Unit"] });
                        }
                    }
                }
                lblExpress.Text = expName;
            }

            //计算
            if (this.isMockModel)
            {
                if (!calTPNItemByCheckDrug())
                    return;
            }
            else if (!this.monitor.calculatorOrders(this.recipeID, this.itemCode))
            {
                lblResult.Text = this.monitor.getError();
                return;
            }

            //在医嘱里显示
            double totalCapacoty = 0;
            RecipePrep info = null;
            foreach (ListViewItem item in lvDrugs.Items)
                if (null != item.Tag)
                {
                    info = this.monitor.getPrepInfo(((BLPublic.IDCodeItem)item.Tag).ID);
                    if (null != info)
                    {
                        item.SubItems[3].Text = info.Quantity.ToString();
                        if (0 < info.Capacity)
                            item.SubItems[4].Text = info.Capacity.ToString() + " ml";
                        else
                            item.SubItems[4].Text = "-";

                        totalCapacoty += info.Capacity;

                        for (int i = lvDrugs.Columns.Count - 1; i >= 5; i--)
                            item.SubItems.Add("-");

                        string[] tpn;
                        foreach (string key in info.TPNValues.Keys) 
                            if (expItems.ContainsKey(key))
                            {
                                tpn = expItems[key];
                                item.SubItems[Convert.ToInt32(tpn[0])].Text = Math.Round(info.TPNValues[key], 2).ToString() + " " + tpn[1];
                            }  
                    }
                }


            if ("recipe.capacity".Equals(this.itemCode))
                lblResult.Text = totalCapacoty.ToString() + " ml";
            else if ("patient.weight".Equals(this.itemCode))
                lblResult.Text = "0 kg";
            else
            {
                this.monitor.getTPNItemInfo(this.itemCode, itemInfo);
                if (0 < itemInfo.Count)
                    lblResult.Text = itemInfo["Value"] + " " + itemInfo["Unit"];
            }

            //合计
            ListViewItem totalItem = lvDrugs.Items.Add("<合计>");
            for (int i = lvDrugs.Columns.Count - 1; i >= 1; i--)
                totalItem.SubItems.Add("");

            totalItem.SubItems[4].Text = totalCapacoty.ToString() + " ml";

            string expValue = express;
            double totalValue = 0; 
            foreach (KeyValuePair<string, string[]> item in expItems)
            {
                totalValue = 0;

                if ("patient.weight".Equals(item.Key))
                    totalValue = 0;

                else if ("recipe.capacity".Equals(item.Key))
                    totalValue = totalCapacoty;

                else if (0 <= Convert.ToInt32(item.Value[0]))
                { 
                    totalValue = getColTotal(Convert.ToInt32(item.Value[0]));
                    totalItem.SubItems[Convert.ToInt32(item.Value[0])].Text = totalValue.ToString() + " " + item.Value[1];
                }
                else if (!string.IsNullOrWhiteSpace(express)) //替换计算表达式项目值 
                {
                    this.monitor.getTPNItemInfo(item.Key, itemInfo);
                    if (0 < itemInfo.Count)
                        totalValue = Math.Round(Convert.ToDouble(itemInfo["Value"]), 2);
                }

                expValue = expValue.Replace('['+item.Key+']', totalValue.ToString()); 
            }

            lblExpress.Text += "\r\n";
            lblExpress.Text += expValue;

            lvDrugs.Items.Insert(lvDrugs.Items.Count - 1, "");
            lvDrugs.Items.Add("");
        }

        private void showDrugPrepInfo(int _prepID)
        {
            string txt = "";
            IDataReader idr = null;
            if (!this.db.GetRecordSet(string.Format(SQL.SEL_PREP_NAME, _prepID), ref idr))
            {
                BLPublic.Dialogs.Error("读取知识库药品失败:" + this.db.Error);
                return;
            }

            txt = "对应知识库单位制剂信息:\r\n";
            txt += "\r\n[ID]: " + _prepID.ToString();
            if (idr.Read())
            {
                txt += "\r\n[名称]: " + idr.GetString(0); 
                txt += "\r\n[规格]: " + idr.GetString(1);
            }
            else
                txt += "\r\n单位制剂: <信息丢失>";

            idr.Close();

            string compCodes = "";
            string thingCodes = "";
            string item = ""; 

            for(int i = lvDrugs.Columns.Count - 1; i >= 5; i--)
            {
                item = lvDrugs.Columns[i].Tag.ToString();
                if (item.Contains("comp.")) 
                    compCodes += "'" + item + "',";

                else
                    thingCodes += "'" + item + "',";
            }

            string sql = "";
            if (!string.IsNullOrWhiteSpace(compCodes))
            {
                compCodes = compCodes.Substring(0, compCodes.Length - 1);
                sql = string.Format(SQL.SEL_TPN_THING, _prepID, compCodes);
            }
            if (!string.IsNullOrWhiteSpace(thingCodes))
            {
                thingCodes = thingCodes.Substring(0, thingCodes.Length - 1);
                if (!string.IsNullOrWhiteSpace(sql))
                    sql += " UNION ALL ";

                sql += string.Format(SQL.SEL_TPN_COMP, _prepID, thingCodes);
            }

            if (this.db.GetRecordSet(sql, ref idr))
            {
                txt += "\r\n";
                while(idr.Read())
                    txt += "\r\n" + idr.GetString(0) + ": " + idr.GetFloat(1).ToString() + " " + idr.GetString(2);

                idr.Close();
            }
            else
                txt += "\r\n读取所含物失败:" + this.db.Error;

            BLPublic.Dialogs.Info(txt);
        }

        /// <summary>
        /// 获取括号里字符
        /// </summary>
        /// <returns></returns>
        private string getBracketStr(string _txt)
        {
            if (string.IsNullOrWhiteSpace(_txt))
                return "";

            int p = _txt.IndexOf('(');
            int p2 = _txt.IndexOf("（");
            if (0 > p)
                p = p2;
            else if ((0 < p2) && (p2 < p))
                p = p2;
             
            if (0 < p)
            {
                int e = _txt.IndexOf(')', p);
                int e2 = _txt.IndexOf("）", p);
                if (0 > e)
                    e = e2;
                else if ((0 < e2) && (e2 < e))
                    e = e2; 

                if (0 > e)
                    e = _txt.Length;

                p++;
                _txt = _txt.Substring(p, e - p);
            }

            return _txt;
        }

        /// <summary>
        /// 获取药品商品名或别名
        /// 通过()获取，去除规格说明
        /// </summary>
        /// <param name="_drugName"></param>
        /// <returns></returns>
        private string getDrugShortName(string _drugName)
        {
            string dn = getBracketStr(_drugName);

            if (string.IsNullOrWhiteSpace(dn))
                return _drugName;

            if (dn.Contains("塑") || dn.Contains("瓶") || dn.Contains("袋") || dn.Contains("%") || dn.Contains("计算"))
                dn = "";
            else
            {
                Regex rNum = new Regex("[0-9]+?");
                if (rNum.IsMatch(dn))
                    dn = "";
            }

            if (!string.IsNullOrWhiteSpace(dn))
                return dn.Trim();
            else
            {
                int p = _drugName.IndexOf('(');
                if (0 > p)
                    p = _drugName.IndexOf("（");

                if (0 < p)
                    return _drugName.Substring(0, p);
                else
                    return _drugName;
            } 
        }

        /// <summary>
        /// 项目为容积时图表
        /// </summary>
        private void showCapacityChart()
        {
            string text = "";
            string unit = "";
            int p = 0;
            double val = 0;

            List<double> yValues = new List<double>();
            List<string> xValues = new List<string>();

            //药品
            for (int i = 0; i < lvDrugs.Items.Count - 3; i++)
            {
                text = lvDrugs.Items[i].SubItems[4].Text;
                if (!string.IsNullOrWhiteSpace(text) && !"-".Equals(text))
                {
                    p = text.LastIndexOf(' ');
                    if (0 < p)
                    {
                        unit = text.Substring(p + 1);
                        val = Convert.ToDouble(text.Substring(0, p));
                    }
                    else
                        val = 0;

                    if (0 < val)
                    {
                        yValues.Add(val);
                        xValues.Add(getDrugShortName(lvDrugs.Items[i].Text));
                    }
                }
            }

            showSerie(chrtDrug, xValues, yValues, unit);   

            yValues.Clear();
            xValues.Clear();
            //参数
            text = lvDrugs.Items[lvDrugs.Items.Count - 2].SubItems[4].Text;
            p = text.LastIndexOf(' ');
            if (0 < p)
            {
                unit = text.Substring(p + 1);
                val = Convert.ToDouble(text.Substring(0, p));
            }
            else
                val = 0;

            if (0 < val)
            {
                yValues.Add(val);
                xValues.Add(lvDrugs.Columns[4].Text);
            }

            showSerie(chrtParam, xValues, yValues, unit);   
        }

        /// <summary>
        /// 显示TPN项目图表
        /// </summary>
        private void showChart()
        {
            chrtDrug.Series["Series1"].Points.Clear();
            chrtParam.Series["Series1"].Points.Clear(); 
             
            if ("tpnitem.1".Equals(this.itemCode)) //容积
            {
                showCapacityChart();
                return;
            }
            else if (5 >= lvDrugs.Columns.Count)
                return;


            string text = "";
            string unit = "";
            int p = 0;  
             
            List<double> yValues = new List<double>();
            List<string> xValues = new List<string>();
             
            ListViewItem item = null;
            MathParserTK.MathParser parser = new MathParserTK.MathParser();
            string express = "";
            string tmpExp = "";
            int hadVal = 0;
            p = lblExpress.Text.IndexOf("\r\n");
            if (0 < p && express.Contains("["))
                express = lblExpress.Text.Substring(0, p);
            else
                express = "0";

            //药品贡献率 
            for (int i = 0; i < lvDrugs.Items.Count - 3; i++)
            {
                tmpExp = express;
                hadVal = 0;
                for (int j = 5; j < lvDrugs.Columns.Count; j++)
                {
                    text = lvDrugs.Items[i].SubItems[j].Text;
                    if (!string.IsNullOrWhiteSpace(text) && !"-".Equals(text))
                    {
                        p = text.LastIndexOf(' ');
                        if (0 < p)
                        {
                            unit = text.Substring(p + 1);
                            text = text.Substring(0, p);
                            hadVal++;
                        }
                        else
                            text = "0";
                    }
                    else
                        text = "0";

                    if ("0".Equals(express))
                        tmpExp += "+" + text;
                    else
                        tmpExp = tmpExp.Replace("["+lvDrugs.Columns[j].Text + "]", text);
                }

                if (0 < hadVal)
                { 
                    yValues.Add(Convert.ToDouble(parser.Parse(tmpExp)));
                    xValues.Add(getDrugShortName(lvDrugs.Items[i].Text));
                }
            }

            showSerie(chrtDrug, xValues, yValues, unit);  

            yValues.Clear();
            xValues.Clear();

            //参数贡献率 
            double val = 0;
            item = lvDrugs.Items[lvDrugs.Items.Count - 2]; //合计行
            for (int i = 5; i < lvDrugs.Columns.Count; i++)
                {
                    text = item.SubItems[i].Text;
                    p = text.LastIndexOf(' ');
                    if (0 < p)
                    {
                        unit = text.Substring(p + 1);
                        val = Convert.ToDouble(text.Substring(0, p));
                    }
                    else
                        val = 0;

                    if (0 < val)
                    {
                        yValues.Add(val);
                        text = getDrugShortName(lvDrugs.Columns[i].Text);
                        if (string.IsNullOrWhiteSpace(text))
                            xValues.Add(lvDrugs.Columns[i].Text);
                        else
                            xValues.Add(text);
                    }
                }

            showSerie(chrtParam, xValues, yValues, unit); 
        }

        private void showSerie(System.Windows.Forms.DataVisualization.Charting.Chart _cht, 
            List<string> _xValues, List<double> _yValues, string _unit)
        {
            if (null == _cht)
                return;

            _cht.Series.Clear();
            _cht.Series.Add("Series1");
            if (_cht == chrtDrug)
                _cht.Series[0].Label = "#PERCENT{P}";
            else
                _cht.Series[0].Label = "#LEGENDTEXT\r\n(#PERCENT{P})";

            _cht.Series[0].LegendText = "#VALX";
            _cht.Series[0].ToolTip = "#LEGENDTEXT\r\n#VALY" + _unit + " (#PERCENT{P})";
            _cht.Series[0].Points.DataBindXY(_xValues, _yValues);
            _cht.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
        }

        /// <summary>
        /// 在字符串后面填充字符
        /// </summary>
        /// <param name="_txt"></param>
        /// <returns></returns>
        private string fillEnd(string _txt, int _maxLen)
        {
            if (null == _txt)
                _txt = "";

            int len = _txt.Length;

            if (len < _maxLen)
                _txt = _txt + new string(' ', _maxLen - len);
            
            return _txt;
        }

        private void frmCalDetail_Load(object sender, EventArgs e)
        {
            if (null == this.db)
            {
                lblItem.Text = "<未初始化>";
                return;
            } 
             
            loadDrug();

            if (!string.IsNullOrWhiteSpace(this.itemCode))
            {
                showTPNItem();
            }
            else
            {
                loadTPN();
                calTPN();
            }
        }

        private void lvDetail_DoubleClick(object sender, EventArgs e)
        {
            if ((0 == lvDrugs.SelectedItems.Count) || (null == lvDrugs.SelectedItems[0].Tag))
                return;

            showDrugPrepInfo(((BLPublic.IDCodeItem)lvDrugs.SelectedItems[0].Tag).ID);
        } 

        private void lvDrugs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((0 == lvDrugs.SelectedItems.Count) || (null == lvDrugs.SelectedItems[0].Tag))
            {
                nudDosage.Hide();
                return;
            }
            
            string dosage = lvDrugs.SelectedItems[0].SubItems[2].Text.Trim();
            int p = dosage.IndexOf(' ');
            if (0 < p)
                dosage = dosage.Substring(0, p).Trim();
            else
                return;

            int t = lvDrugs.Top + 18 + 1;

            for(int i = lvDrugs.SelectedItems[0].Index; i > lvDrugs.TopItem.Index; i--)
                t += imageList1.ImageSize.Height + 1;
              
            nudDosage.Width = lvDrugs.Columns[2].Width;
            nudDosage.Top = t;
            nudDosage.Left = lvDrugs.Left + lvDrugs.Columns[0].Width + lvDrugs.Columns[1].Width + 2 -
                             GetScrollPos(lvDrugs.Handle, 0);
            if (null == nudDosage.Tag)
                nudDosage.Tag = new BLPublic.IDCodeItem(lvDrugs.SelectedItems[0].Index, dosage);
            else
            {
                BLPublic.IDCodeItem ic = (BLPublic.IDCodeItem)nudDosage.Tag;
                ic.ID = lvDrugs.SelectedItems[0].Index;
                ic.Code = dosage;
            }
            nudDosage.Value = Convert.ToDecimal(dosage); 
            nudDosage.Show();
        }

        private void nudDosage_ValueChanged(object sender, EventArgs e)
        {
            if (null == nudDosage.Tag)
                return;

            BLPublic.IDCodeItem ic = (BLPublic.IDCodeItem)nudDosage.Tag;

            string dosageUnit = lvDrugs.Items[ic.ID].SubItems[2].Text;
            int p = dosageUnit.IndexOf(' ');
            if (0 < p)
                dosageUnit = dosageUnit.Substring(p + 1).Trim();
            else
                return;

            lvDrugs.Items[ic.ID].SubItems[2].Text = nudDosage.Value.ToString() + " " + dosageUnit;

            if (ic.Code.Equals(nudDosage.Value.ToString())) //值未变
                return; 

            nudDosage.Enabled = false;

            calTPN();

            nudDosage.Enabled = true;
        }

        private void lvTPN_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((0 == lvTPN.SelectedItems.Count) || (null == lvTPN.SelectedItems[0].Tag))
                return;

            this.lvTPN.Enabled = false;

            int colLen = lvDrugs.Columns.Count;
            for (int i = colLen - 1; i >= 5; i--)
                lvDrugs.Columns.RemoveAt(i);

            for (int i = lvDrugs.Items.Count - 1; i >= 0; i--)
            {
                if (null == lvDrugs.Items[i].Tag)
                    lvDrugs.Items.RemoveAt(i);

                else for (int j = colLen - 1; j >= 5; j--)
                        lvDrugs.Items[i].SubItems.RemoveAt(j);
            }

            this.itemCode = lvTPN.SelectedItems[0].Tag.ToString();
            
            showTPNItem();

            this.lvTPN.Enabled = true;
        }

        private void btnAddDrug_Click(object sender, EventArgs e)
        {
            frmDrugList frm = new frmDrugList();
            frm.init(this.db);
            if (DialogResult.OK == frm.ShowDialog())
            {
                if (null != this.tblDrus.Rows.Find(frm.SelDrug["CustomCode"].ToString()))
                { 
                    BLPublic.Dialogs.Alert("药品已经存在.");
                    return;
                }

                ListViewItem item = lvDrugs.Items.Add(frm.SelDrug["CustomName"].ToString());
                item.Tag = new BLPublic.IDCodeItem(Convert.ToInt32(frm.SelDrug["UniPreparationID"].ToString()),
                                                    frm.SelDrug["CustomCode"].ToString());
                item.SubItems.Add(frm.SelDrug["SpecDesc"].ToString());
                if (string.IsNullOrWhiteSpace(frm.SelDrug["Dosage"].ToString()))
                    item.SubItems.Add(BLPublic.Utils.trimZero(frm.SelDrug["Capacity"].ToString()) + ' ' +
                                      fillEnd(frm.SelDrug["CapacityUnit"].ToString(), 3));
                else
                    item.SubItems.Add(BLPublic.Utils.trimZero(frm.SelDrug["Dosage"].ToString()) + ' ' +
                                      fillEnd(frm.SelDrug["DosageUnit"].ToString(), 3));

                //, 
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                lvDrugs.Refresh();
                lvDrugs.Enabled = false; 

                calTPN();

                lvDrugs.Enabled = true;
            }
        }

        private void btnDelDrug_Click(object sender, EventArgs e)
        {
            if (0 == lvDrugs.SelectedItems.Count)
            {
                BLPublic.Dialogs.Alert("请选择要删除的药品.");
                lvDrugs.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("确定删除所选药品?"))
                return;

            lvDrugs.SelectedItems[0].Remove();
            lvDrugs.Refresh();

            lvDrugs.Enabled = false;

            calTPN();

            lvDrugs.Enabled = true;
        }
    }
}
