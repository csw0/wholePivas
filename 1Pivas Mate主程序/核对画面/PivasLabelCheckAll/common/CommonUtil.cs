using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PivasLabelCheckAll.dao;
using PivasLabelCheckAll.entity;
using System.Data;

namespace PivasLabelCheckAll.common
{
    /// <summary>
    /// 公共方法类
    /// </summary>
    public class CommonUtil
    {
        #region 属性
        private seldb sel = new seldb();
        #endregion

        /// <summary>
        /// 设置登陆panel居中显示的公共方法
        /// </summary>
        /// <param name="p">需要居中显示的panel</param>
        public void SetLocation(Panel pnl)
        {
            //获取当前屏幕的长宽(分辨率)
            int SH = Screen.PrimaryScreen.Bounds.Height;
            int SW = Screen.PrimaryScreen.Bounds.Width;
            //获取控件居中显示的坐标
            int locationX = (SW - pnl.Width) / 2;
            int locationY = (SH - pnl.Height) / 2;
            Point p = new Point(locationX, locationY);
            //设置控件的坐标
            pnl.Location = p;
        }

        /// <summary>
        /// 登陆方法
        /// </summary>
        /// <param name="barCode">二维码或者员工编号</param>
        /// <param name="password">登陆密码</param>
        /// <returns></returns>
        public Demployee login(string barCode,string password)
        {
            Demployee employee = null;
            if (barCode != "" && barCode.Length > 0)
            {
                if (barCode.Substring(0, 4) == "7777" && barCode.Length >= 22)//扫描的是员工二维码
                {
                    DataSet ds = new DataSet();
                    ds = sel.UserLogin(barCode,password);
                    if (ds.Tables[0].Rows.Count > 0)//用户存在的情况
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        employee = new Demployee();
                        employee.DEmployeeID = dr["DEmployeeID"].ToString();
                        employee.QRcode = dr["QRcode"].ToString();
                        employee.QRcodeDT = dr["QRcodeDT"].ToString();
                        employee.DelDT = dr["DelDT"].ToString();
                        employee.AccountID = dr["AccountID"].ToString();
                        employee.Pas = dr["Pas"].ToString();
                        employee.Position = dr["Position"].ToString();
                        employee.DEmployeeCode = dr["DEmployeeCode"].ToString();
                        employee.DemployeeName = dr["DemployeeName"].ToString();
                        employee.IsValid = dr["IsValid"].ToString();
                        employee.Type = dr["Type"].ToString();

                        return employee;
                    }
                    else
                    {
                        //用户不存在的情况
                        return null;
                    }
                }
                else //输入的是员工编号和密码的情况
                {
                    DataSet ds = new DataSet();
                    ds = sel.UserLogin(barCode, password);
                    if (ds.Tables[0].Rows.Count > 0)//用户存在的情况
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        employee = new Demployee();
                        employee.DEmployeeID = dr["DEmployeeID"].ToString();
                        employee.AccountID = dr["AccountID"].ToString();
                        employee.Pas = dr["Pas"].ToString();
                        employee.Position = dr["Position"].ToString();
                        employee.DEmployeeCode = dr["DEmployeeCode"].ToString();
                        employee.DemployeeName = dr["DemployeeName"].ToString();
                        employee.IsValid = dr["IsValid"].ToString();
                        employee.Type = dr["Type"].ToString();

                        return employee;
                    }
                    else
                    {
                        //用户不存在的情况
                        return null;
                    }
                }
            }
            else //扫描数据不对的情况
            {
                return null;
            }
            
        }
    }
}
