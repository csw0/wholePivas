using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace recipemonitor
{
    class SilenceMonitor
    {
        private const string SEL_ORDERS_NOCHK = "SELECT p.PatientCode, p.Birthday, p.Sex, r.RecipeID " +
              "FROM hospitaldata.dbo.Orders r INNER JOIN hospitaldata.dbo.Patient p ON r.PatientCode=p.PatientCode " +
              "WHERE p.IsInHospital=1 AND r.OrdersLabel=4 " +
              "AND NOT EXISTS(SELECT 1 FROM tpn.dbo.TPNCheckRecord cr WHERE cr.RecipeID=r.RecipeID) " +
              "ORDER BY p.PatientCode";

        private string appPath = "";
        private BLPublic.DBOperate db = null;
        private tpnmonitor.TPNMonitor tpnMnt = null;
        private BLPublic.LogOperate logOp = null;

        public SilenceMonitor(string _appPath)
        {
            this.appPath = _appPath; 
        }

        public void monitor()
        {
            this.logOp = new BLPublic.LogOperate(this.appPath + @"\log\", "tpnmonitorlog");
            this.db = new BLPublic.DBOperate(this.appPath + @"\bl_server.lcf", "CPMATE");
            if (!this.db.IsConnected)
            {
                this.logOp.log("连接服务器失败:" + this.db.Error);
                return;
            }

            DataTable tblPatient = null;
            if (!this.db.GetRecordSet(SEL_ORDERS_NOCHK, ref tblPatient))
            {
                this.logOp.log("加载未审方医嘱失败:" + this.db.Error);
                return;
            }

            if ((null == tblPatient) || (0 == tblPatient.Rows.Count))
            {
                this.logOp.log("审方结束");
            }

            this.tpnMnt = new tpnmonitor.TPNMonitor();
            this.tpnMnt.init(this.db, "9999");
             
            int month = 0;  
            DateTime birthday;
            bool rt = false;

            foreach (DataRow row in tblPatient.Rows)
            {
                if (!Convert.IsDBNull(row["Birthday"]))
                {
                    birthday = Convert.ToDateTime(row["Birthday"].ToString());

                    month = DateTime.Now.Month - birthday.Month;
                    month = (DateTime.Now.Year - birthday.Year) * 12 + month;
                    if (DateTime.Now.Day - birthday.Day < -15)
                        month--;
                    else if (DateTime.Now.Day - birthday.Day > 14)
                        month++;
                }
                else
                    month = 0;

                rt = this.tpnMnt.Monitor(row["PatientCode"].ToString(), month,
                                         "f".Equals(row["Sex"].ToString()) ? "女" : "男",
                                         row["RecipeID"].ToString());
                if (rt)
                    this.tpnMnt.saveTPNValue();
                else
                    this.logOp.log("审核医嘱'" + row["RecipeID"].ToString() + "'失败:" + this.tpnMnt.getError());
            } 
             
            tblPatient.Clear();
            this.logOp.log("审方结束");

            endMonitor();
        }

        private void endMonitor()
        {
            if (null != this.db) this.db.Close();
            if (null != this.tpnMnt) this.tpnMnt.clear();
        }
    }
}
