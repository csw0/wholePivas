using System;
using System.Collections.Generic;
using System.IO;
using System.Text; 

namespace synpivasdata
{
    class SynPivasData
    {
        private string appPath = "";
        private BLPublic.DBOperate db = null;
        private BLPublic.LogOperate logOp = null;

        public string Error { get; set; }

        public SynPivasData(string _appPath)
        {
            this.appPath = _appPath;
            this.logOp = new BLPublic.LogOperate(Path.Combine(this.appPath, "log\\"), "synpivasdata");
        }

        public bool synDrug()
        { 
            if (!connectDB())
                return false;

            this.db.ExecSQL("USE HospitalData");

            BLPublic.IniFile ini = new BLPublic.IniFile(Path.Combine(this.appPath, "synpivasdata.ini"));

            if (this.db.ExecSQL(ini.read("newdrug", "SQL")))
                this.logOp.log("导入新药品成功");
            else
            {
                this.Error = "导入新药品失败:" + this.db.Error;
                this.logOp.log(this.Error);
                return false;
            }


            if (this.db.ExecSQL(ini.read("updatedrug", "SQL")))
                this.logOp.log("更新药品成功");
            else
            {
                this.Error = "更新药品失败:" + this.db.Error;
                this.logOp.log(this.Error);
                return false;
            }

            return true;
        }

        public bool synPatient()
        {
            if (!connectDB())
                return false;

            this.db.ExecSQL("USE HospitalData");
            
            BLPublic.IniFile ini = new BLPublic.IniFile(Path.Combine(this.appPath, "synpivasdata.ini"));
             
            //患者出院
            if ("1".Equals(ini.read("outpatient", "IsUsed", "1")))
                if (this.db.ExecSQL(ini.read("outpatient", "SQL")))
                    this.logOp.log("更新患者出院成功");
                else
                {
                    this.Error = "更新患者出院失败:" + this.db.Error;
                    this.logOp.log(this.Error);
                    return false;
                }

            //新患者
            if ("1".Equals(ini.read("newpatient", "IsUsed", "1")))
                if (this.db.ExecSQL(ini.read("newpatient", "SQL")))
                    this.logOp.log("导入新患者成功");
                else
                {
                    this.Error = "导入新患者失败:" + this.db.Error;
                    this.logOp.log(this.Error);
                    return false;
                }

            //患者病区
            if ("1".Equals(ini.read("updatedept", "IsUsed", "1")))
                if (this.db.ExecSQL(ini.read("updatedept", "SQL")))
                    this.logOp.log("更新患者病区成功");
                else
                {
                    this.Error = "更新患者病区失败:" + this.db.Error;
                    this.logOp.log(this.Error);
                    return false;
                }

            //患者床位
            if ("1".Equals(ini.read("updatebedno", "IsUsed", "1")))
                if (this.db.ExecSQL(ini.read("updatebedno", "SQL")))
                    this.logOp.log("更新患者床位成功");
                else
                {
                    this.Error = "更新患者床位失败:" + this.db.Error;
                    this.logOp.log(this.Error);
                    return false;
                }

            return true;
        }


        private bool connectDB()
        {
            if (null == this.db)
                this.db = new BLPublic.DBOperate(this.appPath + @"\bl_server.lcf", "CPMATE");

            if (!this.db.IsConnected)
            { 
                this.Error = "连接服务器失败:" + this.db.Error;
                this.logOp.log(this.Error);
                return false;
            }

            return true;
        }
    }
}
