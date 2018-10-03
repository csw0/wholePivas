using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;

namespace ChargeInterface
{
    public abstract class AbstractCharge : ICharge
    {
        public DB_Help dbHelp = new DB_Help();

        public abstract string Charge(string labelno, string UserID, out string msg);
        public abstract string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg);
        public abstract string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg);
        public abstract bool PrintCharge(List<string> LabelNos, string DEmployeeID);

        #region 流程核对接口
        public abstract string ScanPY5(string LabelNo, string DEmployeeID, out string msg);
        public abstract string ScanJC7(string LabelNo, string DEmployeeID, out string msg);
        public abstract string ScanPZ9(string LabelNo, string DEmployeeID, out string msg);
        public abstract string ScanCC11(string LabelNo, string DEmployeeID, out string msg);
        public abstract string ScanDB13(string LabelNo, string DEmployeeID, out string msg);
        public abstract string ScanQS15(string LabelNo, string DEmployeeID, out string msg);
        #endregion

        #region 备用
        public virtual string SPARE1(string No, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
        public virtual string SPARE2(string No, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
        public virtual string SPARE3(string No, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
        public virtual string SPARE4(string No, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
        public virtual string SPARE5(string No, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
        #endregion

        public virtual void ChangeStatus(string LabelNo, string DEmployeeID, string MoxaIP, string port, int Status)
        {
            //db.GetPIVAsDB("update ScreenDetail set Result='1',Msg='核对成功' where MoxaIP='" + LvS[i].MoxaIP + "' and port='" + LvS[i].MoxaPort + "'");
        }

        public virtual int BackPre(string p_group_no, string prm_EXEC_DOCTOR, string prm_jujueyy, out string PRM_DATABUFFER, out string PRM_APPCODE)
        {
            PRM_DATABUFFER = String.Empty;
            PRM_APPCODE = String.Empty;
            return 0;
        }

        public virtual int WXCharge(string Groupno, string infusionDT, string UserCode, out string hismsg, out string hisret)
        {
            hismsg = String.Empty;
            hisret = String.Empty;
            return 0;
        }
    }
}
