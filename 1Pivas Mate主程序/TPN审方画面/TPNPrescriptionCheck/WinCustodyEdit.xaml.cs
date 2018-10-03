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
    interface ICustodyEdit
    {
        int EditID { get; set; }
        Action<bool> OnEnd { get; set; }

        void init(string _pcode, Action<bool> _OnEnd);
        void setObject(string _objType, string _objCode, string _objName, string _objValue, DateTime _valTime);
         
        string getObjectStr(string _sptChr="\r\n");
        string getDesc();

    }


    /// <summary>
    /// WinCustodyEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinCustodyEdit : Window, ICustodyEdit
    {
        public const string OBJTYP_TPN = "tpn";
        public const string OBJTYP_LIS = "lis";

        private string patientCode = ""; 


        public WinCustodyEdit()
        {
            InitializeComponent();

            txtCustodyer.Text = ""; 
        }

        public static bool IsIntervene { get; set; }
        //设置监护/干预对象(类型、编码、名称、值)
        public static Action<string, string, string, string, DateTime> OnSetObject { get; set; }

        public static bool chkWin(bool _isIntervene)
        {
            if (null != WinCustodyEdit.OnSetObject)
                if (WinCustodyEdit.IsIntervene != _isIntervene)
                {
                    BLPublic.Dialogs.Alert("正在" + (WinCustodyEdit.IsIntervene ? "干预" : "监护") + "操作");
                    return false;
                }

            return true;
        }

        public static bool TPNCustodyIntervene(string _pCode, bool _isIntervene, Action<int, string, string> _onOK)
        {
            return TPNCustodyIntervene(0, _pCode, _isIntervene, _onOK);
        }

        public static bool TPNCustodyIntervene(int _editID, bool _isIntervene, Action<int, string, string> _onOK)
        {
            return TPNCustodyIntervene(_editID, "", _isIntervene, _onOK);
        }

        /// <summary>
        /// 显示监护/干预界面
        /// </summary>
        /// <param name="_pCode"></param>
        /// <param name="_isIntervene"></param>
        /// <param name="_onOK"></param>
        /// <returns></returns>
        private static bool TPNCustodyIntervene(int _editID, string _pCode, bool _isIntervene, 
            Action<int, string, string> _onOK)
        {
            if (!chkWin(_isIntervene))
                return false;

            ICustodyEdit win = null;

            if (_isIntervene)
                win = new WinInterveneEdit();
            else
                win = new WinCustodyEdit();

            ((Window)win).Owner = AppConst.winMain;
            if (0 < _editID)
                win.EditID = _editID; 
            else 
                win.init(_pCode, null);

            win.OnEnd = (isOK) =>
                        {
                            WinCustodyEdit.OnSetObject = null;
                            if (isOK && (null != _onOK))
                                _onOK(win.EditID, win.getObjectStr(), win.getDesc());
                        };

            ((Window)win).Topmost = true;
            ((Window)win).Show();

            WinCustodyEdit.IsIntervene = _isIntervene;
            WinCustodyEdit.OnSetObject = (type, objcode, objname, objvalue, valtime) =>
            {
                win.setObject(type, objcode, objname, objvalue, valtime);
            };

            return true;
        }

        public int EditID { get; set; }
        public Action<bool> OnEnd { get; set; }

        public int getEditID()
        {
            return this.EditID;
        }

        /// <summary>
        /// 初始化监护对象
        /// </summary>
        /// <param name="_pcode"></param>
        /// <param name="_OnEnd">结束事件(bool 是否保存成功)</param> 
        public void init(string _pcode, Action<bool> _OnEnd)
        {
            this.patientCode = _pcode;
            this.OnEnd = _OnEnd;
        }
          
        /// <summary>
        /// 设置监护对象
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
        /// 获取监护对象字符串
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
        /// 获取监护描述
        /// </summary>
        /// <returns></returns>
        public string getDesc()
        {
            return txtCustodyDesc.Text.Trim();
        }

        public static string getObjectStr(int _custodyID, string _sptChr=",")
        {
            string rt = "";
            DataTable tbl = new DataTable();
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_CUSTODY_OBJ, _custodyID), ref tbl)) 
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
        /// 获取监护对象类型名称
        /// </summary>
        /// <param name="_objType"></param>
        /// <returns></returns>
        public static string getCustodyObjType(string _objType)
        {
            if (0 == string.Compare(_objType, OBJTYP_TPN, true))
                return "营养项目";
            else if (0 == string.Compare(_objType, OBJTYP_LIS, true))
                return "检查项目";
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

            else if (0 == string.Compare(_objType, OBJTYP_LIS, true))
                sql = string.Format(SQL.SEL_LISCHKNAME, this.patientCode, _objCode);
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
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_CUSTODY_BYID, this.EditID), ref tbl))
            {
                BLPublic.Dialogs.Error("加载监护失败:" + AppConst.db.Error);
                return;
            }

            txtCustodyer.Text = "";
            if (0 < tbl.Rows.Count)
            {
                txtCustodyDesc.Text = tbl.Rows[0]["CustodyDesc"].ToString();
                txtCustodyer.Text = tbl.Rows[0]["Custodyer"].ToString(); 
            }
            tbl.Clear();


            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_CUSTODY_OBJ, this.EditID), ref tbl))
            {
                BLPublic.Dialogs.Error("加载监护对象失败:" + AppConst.db.Error);
                return;
            }

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
            while (bldr.next())
                setObject(bldr.getString("ObjectType"), bldr.getString("ObjectCode"), bldr.getString("ObjectName"),
                    bldr.getString("ObjectValue"), bldr.getDateTime("ValueTime"));
            bldr.close();
            tbl.Clear();

            if (!string.IsNullOrWhiteSpace(txtCustodyer.Text))
                txtCustodyer.Text = ComClass.getEmpName(AppConst.db, txtCustodyer.Text); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (0 < this.EditID)
                loadCustody();
            else
                txtCustodyer.Text = ComClass.getEmpName(AppConst.db, AppConst.LoginEmpCode); 
        }

        private bool saveObjects(int _custodyID)
        {
            CustodyObject c = null;
            string sql = string.Format(SQL.DEL_CUSTODY_OBJ, _custodyID);
            foreach(Object item in lvObjects.Items)
            {
                c = (CustodyObject)item;
                sql += string.Format(SQL.ADD_CUSTODY_OBJ, _custodyID, c.TypeCode, c.ObjectCode, c.ObjectName, 
                                     c.ObjectValue, BLPublic.DBOperate.fmtDT(c.ValueTime));
            }

            if (!AppConst.db.ExecSQL(sql))
            {
                BLPublic.Dialogs.Error("保存监护失败:" + AppConst.db.Error);
                return false;
            } 

            return true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (0 == lvObjects.Items.Count)
            {
                BLPublic.Dialogs.Alert("请选择监护对象");
                lvObjects.Focus();
                return;
            }

            bool rt = false;
            if (0 < this.EditID)
            {
                if (AppConst.db.ExecSQL(string.Format(SQL.MOD_CUSTODY, this.EditID, txtCustodyDesc.Text.Trim())))
                    rt = true;
                else
                    BLPublic.Dialogs.Error("保存监护失败:" + AppConst.db.Error);
            }
            else
            {
                int ID = 0;
                if (AppConst.db.InsertAndGetId(string.Format(SQL.ADD_CUSTODY, this.patientCode, AppConst.LoginEmpCode,
                      txtCustodyDesc.Text.Trim()), out ID))
                {
                    this.EditID = ID;
                    rt = true;
                }
                else
                    BLPublic.Dialogs.Error("保存监护失败:" + AppConst.db.Error);
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinCustodyObj win = new WinCustodyObj();
            win.Owner = this;
            if (true == win.ShowDialog())
            {
                BLPublic.IDCodeNameItem sel = (BLPublic.IDCodeNameItem)win.cbbChkItem.SelectedItem;
                setObject(OBJTYP_LIS, sel.ID, sel.Name, win.txtChkValue.Text.Trim() + sel.Code, 
                          win.dpValDate.SelectedDate.Value);
            }
        }
    }

    class CustodyObject
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string ObjectCode { get; set; }
        public string ObjectName { get; set; }
        public string ObjectValue { get; set; }
        public DateTime ValueTime { get; set; }

    }
}
