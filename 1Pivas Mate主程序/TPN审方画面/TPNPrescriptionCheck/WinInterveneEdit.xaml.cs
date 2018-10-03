using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TPNReview
{
    /// <summary>
    /// WinInterveneEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinInterveneEdit : Window, ICustodyEdit
    {
        public const string OBJTYP_TPN = "tpn";
        public const string OBJTYP_DRUG = "drug";

        private string patientCode = ""; 


        public WinInterveneEdit()
        {
            InitializeComponent();

            txtIntervener.Text = ""; 
        }

        public int EditID { get; set; }

        public Action<bool> OnEnd { get; set; }

        public int getEditID()
        {
            return this.EditID;
        }

        /// <summary>
        /// 初始化干预对象
        /// </summary>
        /// <param name="_pcode"></param>
        /// <param name="_OnEnd">结束事件(bool 是否保存成功)</param> 
        public void init(string _pcode, Action<bool> _OnEnd)
        {
            this.patientCode = _pcode;
            this.OnEnd = _OnEnd;
        }
         
        /// <summary>
        /// 设置干预对象
        /// </summary>
        /// <param name="_objType"></param>
        /// <param name="_objCode"></param>
        /// <param name="_objName"></param>
        /// <param name="_objValue"></param>
        public void setObject(string _objType, string _objCode, string _objName, string _objValue, DateTime _valTime)
        {
            lvObjects.Items.Add(new CustodyObject()
                {
                    TypeCode = _objType,
                    TypeName = getCustodyObjType(_objType),
                    ObjectCode = _objCode,
                    ObjectName = _objName,
                    ObjectValue = _objValue,
                    ValueTime = _valTime
                }
            );
        }

        /// <summary>
        /// 获取干预对象描述
        /// </summary>
        /// <returns></returns>
        public string getObjectStr(string _sptChr = ",")
        {
            string rt = "";
            CustodyObject c = null; 
            foreach (Object item in lvObjects.Items)
            {
                c = (CustodyObject)item;

                if (!string.IsNullOrWhiteSpace(rt))
                    rt += _sptChr;

                rt += c.ValueTime.ToString("[yy.MM.dd] ") + c.ObjectName;
                if (!string.IsNullOrWhiteSpace(c.ObjectValue))
                    rt += " " + c.ObjectValue;
            }

            return rt;
        }

        /// <summary>
        /// 获取干预计划
        /// </summary>
        /// <returns></returns>
        public string getDesc()
        {
            return txtInterveneDesc.Text.Trim();
        }

        public static string getObjectStr(int _interveneID, string _sptChr = ",")
        {
            string rt = "";
            DataTable tbl = new DataTable();
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_INTERVENE_OBJ, _interveneID), ref tbl)) 
                return ""; 

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
            while (bldr.next())
            {
                if (!string.IsNullOrWhiteSpace(rt))
                    rt += _sptChr;

                rt += bldr.getDateTime("ValueTime").ToString("[yy.MM.dd] ") + bldr.getString("ObjectName");
                if (!string.IsNullOrWhiteSpace(bldr.getString("ObjectValue")))
                    rt += " " + bldr.getString("ObjectValue");
            } 
            bldr.close();
            tbl.Clear();

            return rt;
        }

        /// <summary>
        /// 获取干预对象类型名称
        /// </summary>
        /// <param name="_objType"></param>
        /// <returns></returns>
        public static string getCustodyObjType(string _objType)
        {
            if (0 == string.Compare(_objType, OBJTYP_TPN, true))
                return "营养项目";
            else if (0 == string.Compare(_objType, OBJTYP_DRUG, true))
                return "药品";
            else
                return "";
        }
         
        private string getCustodyObjName(string _objType, string _objCode)
        {
            if (string.IsNullOrWhiteSpace(_objType) || string.IsNullOrWhiteSpace(_objCode))
                return "";

            string sql = "";
            if (0 == string.Compare(_objType, OBJTYP_TPN, true))
                sql = string.Format(SQL.SEL_TPNTEIM_NAME, _objCode);

            else if (0 == string.Compare(_objType, OBJTYP_DRUG, true))
                sql = string.Format(SQL.SEL_DRUG_NAME, _objCode);
            else
                return "";

            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(sql, ref dr))
            {
                string rt = "";
                if (dr.Read())
                {
                    rt = dr.GetString(0);
                    dr.Close();
                }

                return rt;
            } 

            return "";
        }

        private void loadCustody()
        {
            DataTable tbl = new DataTable();
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_INTERVENE_BYID, this.EditID), ref tbl))
            {
                BLPublic.Dialogs.Error("加载干预失败:" + AppConst.db.Error);
                return;
            }

            txtIntervener.Text = "";
            if (0 < tbl.Rows.Count)
            {
                txtInterveneDesc.Text = tbl.Rows[0]["IntervenePlan"].ToString();
                txtIntervener.Text = tbl.Rows[0]["Intervener"].ToString(); 
            }
            tbl.Clear();


            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_INTERVENE_OBJ, this.EditID), ref tbl))
            {
                BLPublic.Dialogs.Error("加载干预对象失败:" + AppConst.db.Error);
                return;
            }

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
            while (bldr.next())
                setObject(bldr.getString("ObjectType"), bldr.getString("ObjectCode"), bldr.getString("ObjectName"),
                    bldr.getString("ObjectValue"), bldr.getDateTime("ValueTime"));
            bldr.close();
            tbl.Clear();

            if (!string.IsNullOrWhiteSpace(txtIntervener.Text))
                txtIntervener.Text = ComClass.getEmpName(AppConst.db, txtIntervener.Text); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (0 < this.EditID)
                loadCustody();
            else
                txtIntervener.Text = ComClass.getEmpName(AppConst.db, AppConst.LoginEmpCode); 
        }

        private bool saveObjects(int _custodyID)
        {
            CustodyObject c = null;
            string sql = string.Format(SQL.DEL_INTERVENE_OBJ, _custodyID);
            foreach(Object item in lvObjects.Items)
            {
                c = (CustodyObject)item;
                sql += string.Format(SQL.ADD_INTERVENE_OBJ, _custodyID, c.TypeCode, c.ObjectCode, c.ObjectName,
                                     c.ObjectValue, BLPublic.DBOperate.fmtDT(c.ValueTime));
            }

            if (!AppConst.db.ExecSQL(sql))
            {
                BLPublic.Dialogs.Error("保存干预失败:" + AppConst.db.Error);
                return false;
            } 

            return true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (0 == lvObjects.Items.Count)
            {
                BLPublic.Dialogs.Alert("请选择干预对象");
                lvObjects.Focus();
                return;
            }

            bool rt = false;
            if (0 < this.EditID)
            {
                if (AppConst.db.ExecSQL(string.Format(SQL.MOD_INTERVENE, this.EditID, txtInterveneDesc.Text.Trim())))
                    rt = true;
                else
                    BLPublic.Dialogs.Error("保存干预失败:" + AppConst.db.Error);
            }
            else
            {
                int ID = 0;
                if (AppConst.db.InsertAndGetId(string.Format(SQL.ADD_INTERVENE, this.patientCode, AppConst.LoginEmpCode,
                      txtInterveneDesc.Text.Trim()), out ID))
                {
                    this.EditID = ID;
                    rt = true;
                }
                else
                    BLPublic.Dialogs.Error("保存干预失败:" + AppConst.db.Error);
            }


            if (rt)
                rt = saveObjects(this.EditID);

            if (rt)
            {
                BLPublic.Dialogs.Info("保存成功");

                if (null != this.OnEnd)
                {
                    this.OnEnd(true);
                    this.OnEnd = null;
                }

                this.Close();
            }
        }

        private void btnAddObj_Click(object sender, RoutedEventArgs e)
        {
            /*Button btn = (Button)sender; 
            btn.ContextMenu.IsOpen = true; */
            BLPublic.Dialogs.Info(((Button)sender).ToolTip.ToString());
        }

        private void btnDelObj_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvObjects.SelectedItem)
            {
                BLPublic.Dialogs.Alert("选择要删除的对象.");
                lvObjects.Focus();
            }
            else if (BLPublic.Dialogs.Yes == BLPublic.Dialogs.Ask("确定删除所选对象."))
                lvObjects.Items.Remove(lvObjects.SelectedItem);
        } 

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Topmost = false;
            if (null != this.OnEnd)
                this.OnEnd(false);

            AppConst.winMain.Activate();
        }
    } 
}
