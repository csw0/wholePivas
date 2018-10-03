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
    /// WinCustomCheck.xaml 的交互逻辑
    /// </summary>
    public partial class WinCustomCheck : Window
    {
        private string patientCode = "";
        private string recipeID = "";
        private int editID = 0;

        public WinCustomCheck()
        {
            InitializeComponent();
        }

        public int EditID
        {
            get { return this.editID; }
            set
            {
                this.editID = value;

                IDataReader dr = null;

                //显示自定义审方结果
                if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_CUSTOM_CHK_BYID, this.editID), ref dr))
                {
                    if (dr.Read())
                    {
                        txtDesc.Text = dr["ResultDesc"].ToString();
                        if ("5".Equals(dr["ResultLevel"].ToString()))
                            rdo5Degress.IsChecked = true;
                        else
                            rdo3Degress.IsChecked = true; 
                    }

                    dr.Close();
                }
                else
                    txtDesc.Text = "加载失败:" + AppConst.db.Error;
            }
        }

        public void init(string _patientCode, string _recipeID)
        {
            this.patientCode = _patientCode;
            this.recipeID = _recipeID;
        } 

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDesc.Text))
            {
                BLPublic.Dialogs.Alert("请输入审方内容.");
                txtDesc.Focus();
                return;
            }

            bool rt = false;
            if (0 < this.editID)
                rt = AppConst.db.ExecSQL(string.Format(SQL.MOD_CUSTOM_CHK, this.editID, txtDesc.Text.Trim(),
                                    true == rdo5Degress.IsChecked ? 5 : 3));
            else
                rt = AppConst.db.InsertAndGetId(string.Format(SQL.ADD_CUSTOM_CHK, this.patientCode, this.recipeID, txtDesc.Text.Trim(),
                                    true == rdo5Degress.IsChecked ? 5: 3, AppConst.LoginEmpCode), out this.editID);

            if (rt)
            {
                BLPublic.Dialogs.Info("保存成功");
                this.DialogResult = true;
                this.Close();
            }
            else
                BLPublic.Dialogs.Error("保存失败:" + AppConst.db.Error);
        }

    }
}
