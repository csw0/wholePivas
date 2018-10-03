using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Threading;

namespace SynServer
{
    /// <summary>
    /// 执行pivas存储过程，实现将基础数据从临时表到正式表
    /// </summary>
    internal sealed class ExecProcedures
    {
        private DB_Help dbHelp = new DB_Help();
        CommonFunc db = new CommonFunc();

        /// <summary>
        /// 执行存储过程，同步数据
        /// </summary>
        /// <param name="SynCode"></param>
        /// <param name="SynID"></param>
        internal void SyncRun(string SynCode, string SynID)
        {
            try
            {
                dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [ScheduleTxt]='开始同步',[StartUPTime]=GETDATE(),[RandomID]='{0}',[Schedule]='0' where [SynID]='{1}'", Guid.NewGuid().ToString(), SynID));
                using (DataSet ds = db.GetBaseInfoByHIS(SynCode))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [ScheduleTxt]='插入数据'," +
                            "[SynData]='{0}' where [SynID]='{1}'", XmlSerializeHelper.SerializeDataTableXml(
                                ds.Tables[0], SynCode).Replace("'", ""), SynID));
                        switch (SynCode)
                        {
                            case "1":
                                {
                                    //将his返回的最新信息存入临时表，然后执行存储过程更新到正式表
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "DDrugTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_syndruginfo ");
                                    break;
                                }
                            case "2":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "DWardTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_syndwardinfo ");
                                    break;
                                }
                            case "3":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "DEmployeeTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_synemployeeinfo ");
                                    break;
                                }
                            case "4":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "DMetricTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_syndmetricinfo ");
                                    break;
                                }
                            case "5":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "DFreqTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_synfreqinfo ");
                                    break;
                                }
                            case "6":
                                {
                                    dbHelp.SetPIVAsDB("EXEC bl_synpatientinfoBefore");
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "PatientTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_synpatientinfo ");
                                    dbHelp.SetPIVAsDB("EXEC bl_synpatientinfoAfter");
                                    break;
                                }
                            case "7":
                                {
                                    DateTime tt = DateTime.Now;
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "PrescriptionTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='100',[EndUPTime]=GETDATE(),[ScheduleTxt]='拆分处方' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_fetchrecipes6");//csw bl_fetchrecipes '6'修改成bl_fetchrecipes6
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='200',[ScheduleTxt]='开始审方' where [SynID]='{0}'", SynID));
                                    int k = 0;
                                    Thread th1 = new Thread(() =>
                                    {
                                        try
                                        {
                                            dbHelp.SetPIVAsDB("EXEC bl_Remonitor ''");
                                        }
                                        catch (Exception ex)
                                        {
                                            InternalLogger.Log.Error(SynCode + "执行审方出错_1" + ex.Message);
                                        }
                                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='{0}' where [SynID]='{1}'", (200 + (k + 1) * 50), SynID));
                                    });
                                    th1.IsBackground = true;
                                    th1.Start();
                                    Thread th2 = new Thread(() =>
                                    {
                                        try
                                        {
                                            dbHelp.SetPIVAsDB("EXEC bl_Remonitor2");
                                        }
                                        catch (Exception ex)
                                        {
                                            InternalLogger.Log.Error(SynCode + "执行审方出错_2" + ex.Message);
                                        }
                                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='{0}' where [SynID]='{1}'", (200 + (k + 1) * 50), SynID));
                                    });
                                    th2.IsBackground = true;
                                    th2.Start();
                                    Thread th3 = new Thread(() =>
                                    {
                                        try
                                        {
                                            dbHelp.SetPIVAsDB("EXEC bl_Remonitor3");
                                        }
                                        catch (Exception ex)
                                        {
                                            InternalLogger.Log.Error(SynCode + "执行审方出错_3" + ex.Message);
                                        }
                                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='{0}' where [SynID]='{1}'", (200 + (k + 1) * 50), SynID));
                                    });
                                    th3.IsBackground = true;
                                    th3.Start();
                                    Thread th4 = new Thread(() =>
                                    {
                                        try
                                        {
                                            dbHelp.SetPIVAsDB("EXEC bl_Remonitor4");
                                        }
                                        catch (Exception ex)
                                        {
                                            InternalLogger.Log.Error(SynCode + "执行审方出错_4" + ex.Message);
                                        }
                                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='{0}' where [SynID]='{1}'", (200 + (k + 1) * 50), SynID));
                                    });
                                    th4.IsBackground = true;
                                    th4.Start();
                                    while (th1.IsAlive || th2.IsAlive || th3.IsAlive || th4.IsAlive)
                                    {
                                        if (DateTime.Now > tt.AddMinutes(7))
                                        {
                                            try
                                            {
                                                if (th1.IsAlive)
                                                    th1.Abort();
                                                if (th2.IsAlive)
                                                    th2.Abort();
                                                if (th3.IsAlive)
                                                    th3.Abort();
                                                if (th4.IsAlive)
                                                    th4.Abort();
                                            }
                                            catch { }
                                        }
                                    }
                                    break;
                                }
                            case "8":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "UseDrugListTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='拆分药单' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_fetchdrugList6 ");
                                    break;
                                }
                            case "9":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "PatientTempHTandWT");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_synpatientinfoAfter ");
                                    break;
                                }
                            case "10":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "DiagnosisTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_synDiagnosisInfo ");
                                    break;
                                }
                            case "11":
                                {
                                    dbHelp.CopyDataTableToDB(ds.Tables[0], "HISDrugListCountTemp");
                                    dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Schedule]='300',[EndUPTime]=GETDATE(),[ScheduleTxt]='信息更新' where [SynID]='{0}'", SynID));
                                    dbHelp.SetPIVAsDB("EXEC bl_SynHISDrugListCount ");
                                    break;
                                }
                        }
                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Success]='1',[Schedule]='500' ,"+
                            "[EndTime]=GETDATE(),[ScheduleTxt]='同步成功' where [SynID]='{0}'", SynID));
                    }
                    else
                    {
                        dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Success]='0',[Schedule]='500',[EndUPTime]="+
                            "GETDATE() ,[EndTime]=GETDATE(),[ScheduleTxt]='没有数据' where [SynID]='{0}'", SynID));
                    }
                }
            }
            catch (Exception ex)
            {
                dbHelp.SetPIVAsDB(string.Format("update [SynLog] set [Success]='-1',[Schedule]='500',[EndUPTime]="+
                    "GETDATE() ,[EndTime]=GETDATE(),[ScheduleTxt]='同步异常' where [SynID]='{0}'", SynID));
                InternalLogger.Log.Error(SynCode + "执行同步出错" + ex.Message);
            }
        }
    }
}
