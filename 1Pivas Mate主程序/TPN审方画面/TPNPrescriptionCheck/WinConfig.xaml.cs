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
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinConfig : Window
    {
        public WinConfig()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IDataReader dr = null;

            if (AppConst.db.GetRecordSet(SQL.SEL_CONFIG, ref dr))
            {
                while (dr.Read())
                {
                    if ("cal_all_capacity".Equals(dr["SettingItemCode"].ToString()))
                    {
                        cbCalOtherCapacity.IsChecked = !"0".Equals(dr["SettingValue"].ToString());
                    }
                    else if ("cal_min_capacity".Equals(dr["SettingItemCode"].ToString()))
                    {
                        txtCalMinCapacity.Text = dr["SettingValue"].ToString();
                    }
                }

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载系统设置失败:" + AppConst.db.Error); 
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCalMinCapacity.Text))
            {
                BLPublic.Dialogs.Alert("请输入最小计算量");
                txtCalMinCapacity.Focus();
                return;
            }
            
            if (!BLPublic.Utils.IsNumeric(txtCalMinCapacity.Text))
            {
                BLPublic.Dialogs.Alert("最小计算量只能是数字");
                txtCalMinCapacity.Focus();
                return;
            }

            if (!AppConst.db.ExecSQL(string.Format(SQL.SET_CONFIG, "cal_all_capacity", true == cbCalOtherCapacity.IsChecked ? "1": "0")))
            {
                BLPublic.Dialogs.Error("设置计算非溶媒注射液容积失败:" + AppConst.db.Error); 
                return;
            }

            if (!AppConst.db.ExecSQL(string.Format(SQL.SET_CONFIG, "cal_min_capacity", txtCalMinCapacity.Text)))
            {
                BLPublic.Dialogs.Error("设置最小计算量失败:" + AppConst.db.Error);
                return;
            }

            BLPublic.Dialogs.Info("设置成功");
            this.DialogResult = true;
            this.Close();
        }
    }
}
