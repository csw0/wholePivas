using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PivasLimitDES
{
    public sealed class GetPivasLimit
    {
        #region 单例
        private static GetPivasLimit instance = null;
        public static GetPivasLimit Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GetPivasLimit();
                }
                return instance;
            }
        }

        private GetPivasLimit()
        {
            dbHelp = new DB_Help();
        }
        #endregion

        private DB_Help dbHelp = null;
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID">用户名</param>
        /// <param name="LimitName">权限名</param>
        /// <returns></returns>
        public bool Limit(string DEmployeeID, string LimitName)
        {
            try
            {
                bool lt = false;//当前画面是否在该医院可用Check
                string str = "select * from ManageLimit where DEMployeeID = '" + DEmployeeID + "' and LimitName = '" + LimitName + "'    ";
                str = str + "  select DEmployeeName from DEmployee where DEmployeeID = '" + DEmployeeID + "'     ";
                DataSet ds = dbHelp.GetPIVAsDB(str);
                if (NotInFormLimit(LimitName.Trim()))//检测是否需要
                    lt = true;
                else
                    if (SynToolIsLatest())//如果是最新的则进一步判断是否有权限
                        lt = GetLimt(LimitName);

                if (!lt)
                {
                    MessageBox.Show("您没有此操作权限，请于系统管理员联系！！！");
                    ds.Dispose();
                    return false;
                }
                else if ((ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0 
                    && ds.Tables[1].Rows[0]["DEmployeeName"].ToString().Trim() == "LaennecSystem")
                    || (ds != null && ds.Tables[0].Rows.Count > 0))
                {
                    ds.Dispose();
                    return true;
                }
                else
                {
                    MessageBox.Show("您没有此操作权限，请于系统管理员联系！！！");
                    ds.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("判断用户权限时出错:" + ex.Message);
            }
            return false;
        }

        public bool LimitNoMsg(string DEmployeeID, string LimitName)
        {
            try
            {
                DataSet ds = new DataSet();
                bool lt = false;//当前画面是否在该医院可用Check
                string str = "select * from ManageLimit where DEMployeeID = '" + DEmployeeID + "' and LimitName = '" + LimitName + "'    ";
                str = str + "  select DEmployeeName from DEmployee where DEmployeeID = '" + DEmployeeID + "'     ";

                ds = dbHelp.GetPIVAsDB(str);

                if (NotInFormLimit(LimitName.Trim()))//检测是否需要
                {
                    lt = true;
                }
                else
                {
                    SynToolIsLatest();//作为同步程序的提示信息
                    lt = GetLimt(LimitName);
                }

                if (!lt)
                {
                    ds.Dispose();
                    return false;
                }
                else if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["DEmployeeName"].ToString().Trim() == "LaennecSystem")
                {
                    ds.Dispose();
                    return true;
                }
                else if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Dispose();
                    return true;
                }
                else
                {
                    ds.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 判断同步程序是不是最新的
        /// </summary>
        /// <returns></returns>
        private bool SynToolIsLatest()
        {
            try
            {
                StringBuilder sb = new StringBuilder(4096);
                sb.AppendLine(" if not exists (SELECT 1 FROM sys.all_objects where type='U' AND name='PivasDesPassWord') begin Create table [PivasDesPassWord](ID INT identity(1,1) PRIMARY key,MacSSID VARCHAR(250),PivasWord text,Dat datetime) end");
                sb.AppendLine(" if not exists (SELECT 1 FROM sys.all_objects where type='U' AND name='PivasDesSoftTAB') ");
                sb.AppendLine(" begin ");
                sb.AppendLine(" Create table [PivasDesSoftTAB](ID INT identity(1,1) PRIMARY key,SoftPW VARCHAR(512),Dat datetime) ");
                sb.AppendLine(" insert into [PivasDesSoftTAB] values('',getdate()) ");
                sb.AppendLine(" end ");
                sb.AppendLine(" SELECT SoftPW,Dat,Getdate() gs FROM [PivasDesSoftTAB] ");

                DataSet ds = dbHelp.GetPIVAsDB(sb.ToString());
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString().Trim())
                        && !string.IsNullOrEmpty(ds.Tables[0].Rows[0][1].ToString().Trim()))
                    {
                        DateTime val1 = DateTime.Now.AddDays(-1);
                        DateTime.TryParse(ds.Tables[0].Rows[0][1].ToString().Trim(), out val1);

                        string dat = PermitHelper.EncrypOrDecryp(ds.Tables[0].Rows[0][0].ToString().Trim(),
                            val1.ToString("yyyy-MM-dd HH:mm:ss"), false);
                        DateTime dt = DateTime.Now.AddDays(-1);
                        bool isok = DateTime.TryParse(dat, out dt);
                        DateTime.TryParse(ds.Tables[0].Rows[0][2].ToString().Trim(), out val1);
                        if (isok && val1 < dt.AddHours(1))
                            return true;
                        else
                            MessageBox.Show("同步程序不是最新的！！！请更新同步程序");
                    }
                    else
                        MessageBox.Show("请运行最新的同步程序！！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="Pro"></param>
        /// <returns></returns>
        private bool GetLimt(string Pro)
        {
            bool ret = false;
            try
            {
                DataSet ds = dbHelp.GetPIVAsDB(
                    "select [MacSSID],[PivasWord] from [PivasDesPassWord] where ID=(select max(ID) from [PivasDesPassWord])");

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    using (DataSet dt = new DataSet())
                    {
                        string val = PermitHelper.EncrypOrDecryp(ds.Tables[0].Rows[0][1].ToString().Trim(),
                            ds.Tables[0].Rows[0][0].ToString().Trim(), false);

                        if (!string.IsNullOrEmpty(val))
                        {
                            using (StringReader sr = new StringReader(val))
                                dt.ReadXml(sr, XmlReadMode.Auto);

                            if (dt != null && dt.Tables.Count > 0 && dt.Tables[0].Rows.Count > 0)
                                if (dt.Tables[0].Columns.Contains(Pro))
                                    bool.TryParse(dt.Tables[0].Rows[0][Pro].ToString(), out ret);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// 检查那些全选不要检查
        /// </summary>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        private bool NotInFormLimit(string LimitName)
        {
            bool FormCheck = false;

            if (LimitName == "" || LimitName == "01001" || LimitName == "01003" || LimitName == "01002" || LimitName == "")
            {
                FormCheck = true;
            }
            else if (LimitName == "02000" || LimitName == "02001" || LimitName == "02002" || LimitName == "02003" || LimitName == "02005" || LimitName == "02006" || LimitName == "refundwhy" || LimitName == "02004")
            {
                FormCheck = true;
            }
            else if (LimitName == "03000" || LimitName == "03001" || LimitName == "03002" || LimitName == "03003" || LimitName == "03004")
            {
                FormCheck = true;
            }
            else if (LimitName == "04000" || LimitName == "04001" || LimitName == "04002" || LimitName == "PrintBatchUpdate" || LimitName == "04003")
            {
                FormCheck = true;
            }
            else if (LimitName == "05000" || LimitName == "CheckSet" || LimitName == "" || LimitName == "" || LimitName == "")
            {
                FormCheck = true;
            }
            else if (LimitName == "06000" || LimitName == "PivasDEmployee" || LimitName == "PivasDDrug" || LimitName == "PivasFreqRule" || LimitName == "PivasDFreg")
            {
                FormCheck = true;
            }
            else if (LimitName == "PivasDMetric" || LimitName == "PivasDWard" || LimitName == "06001" || LimitName == "")
            {
                FormCheck = true;
            }
            else if (LimitName == "07000" || LimitName == "PivasWorkFlow" || LimitName == "PivasSafe" || LimitName == "PivasDrugFlow" || LimitName == "ConsumablesStatic")
            {
                FormCheck = true;
            }
            else if (LimitName == "08000" || LimitName == "PivasLabelSelect" || LimitName == "ScanPre" || LimitName == "LabelOver" || LimitName == "")
            {
                FormCheck = true;
            }
            else if (LimitName == "08001" || LimitName == "08002" || LimitName == "08003" || LimitName == "08004" || LimitName == "08005" || LimitName == "08006")
            {
                FormCheck = true;
            }
            else if (LimitName == "" || LimitName == "" || LimitName == "" || LimitName == "" || LimitName == "")
            {
                FormCheck = true;
            }
            else
            {
                FormCheck = false;
            }
            return FormCheck;
        }
    }
}
