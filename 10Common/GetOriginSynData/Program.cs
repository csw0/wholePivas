using ChargeInterface;
using Communication.DisplayTcp;
using Communication.PLCCom;
using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading;

namespace GetOriginSynData
{
    class Program
    {
        static event EventHandler<EventArgs> OnEvent;

        static void Main(string[] args)
        {
            InternalLogger.Log.Info("任意键，测试开始");
            Console.ReadLine();
            #region 测试体
            try
            {
                string DBName_DBOPre = "HISOriginData.dbo.";
                string DBName_SYSPre = "HISOriginData.sys.";
                //pivas.SDFY库中只有1到9；10和11没用
                for (int i = 1; i < 10; i++)
                {
                    string tempTabelName = GetTabelName(i);
                    if (tempTabelName == string.Empty)
                    {
                        InternalLogger.Log.Error("根据code未获取到临时表名");
                        return;
                    }
                    CreateEmptyTable(DBName_DBOPre + tempTabelName, tempTabelName, DBName_SYSPre);
                    //因部分类型转换会出错，先建表后copy数据
                    testCopyDataTableToDB(i, DBName_DBOPre + tempTabelName);
                }
            }
            catch (Exception wx)
            {
                Console.WriteLine("异常:" + wx.Message);
            }
            #endregion
            Console.WriteLine("任意键，测试结束");
            Console.ReadLine();
        }

        /// <summary>
        /// 创建空表，仅有表结构
        /// </summary>
        static void CreateEmptyTable(string hisTableName, string tempTableName,string preDBName)
        {
            DB_Help dbHelp = new DB_Help();
            int rtn = -1;

            DataSet ds = dbHelp.GetPIVAsDB("select 1 from " + preDBName + "sysobjects where id = object_id('"
                + hisTableName + "') and type = 'U'");
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            { rtn = 1;  }
            else
            {//不存在时，才创建表
                rtn = dbHelp.SetPIVAsDB("select * into " + hisTableName + " from " + tempTableName + " where 1=2");
            }
            
            if (rtn < 0)
            {
                InternalLogger.Log.Error("CREATE TABLE error");
            }
        }

        static string GetTabelName(int SynCode)
        {
            string tempTableName = string.Empty;
            switch (SynCode)
            {
                case 1://药品
                    tempTableName = "DDrugTemp";
                    break;
                case 2://病区
                    tempTableName = "DWardTemp";
                    break;
                case 3://员工
                    tempTableName = "DEmployeeTemp";
                    break;
                case 4://剂量
                    tempTableName = "DMetricTemp";
                    break;
                case 5://频次
                    tempTableName = "DFreqTemp";
                    break;
                case 6://病人
                    tempTableName = "PatientTemp";
                    break;
                case 7://医嘱
                    tempTableName = "PrescriptionTemp";
                    break;
                case 8://药单
                    tempTableName = "UseDrugListTemp";
                    break;
                case 9://病人身高体重
                    tempTableName = "PatientTempHTandWT";
                    break;
                case 10://诊断
                    tempTableName = "DiagnosisTemp";
                    break;
                case 11://药单数量，目前似乎没用
                    tempTableName = "HISDrugListCountTemp";
                    break;
                default: break;
            }
            return tempTableName;
        }
        /// <summary>
        /// 这个方法很有用，用于将从his获取的数据，插入一个临时表TempTest。从表中分析原始数据是否有问题。
        /// </summary>
        static void testCopyDataTableToDB(int SynCode, string tempTableName)
        {
            try
            {
                DB_Help dbHelp = new DB_Help();
                DataSet dsSynData = dbHelp.GetPIVAsDB("SELECT TOP 100 SynID,EndTime,SynData FROM " +
                    "SynLog  where SynCode = " + SynCode.ToString() + " order by SynID desc");

                //获取表结构
                DataSet dsHis = dbHelp.GetPIVAsDB("select top 1 * from " + tempTableName);
                foreach (DataRow item in dsSynData.Tables[0].Rows)
                {
                    string str =item["SynData"].ToString();
                    DataSet ds = XmlSerializeHelper.XmlToDataTable(str);
                    if (ds.Tables !=null && ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            foreach (DataColumn dc in dsHis.Tables[0].Columns)//dsHis肯定有表结构
                            {
                                try
                                {
                                    if (dc.Caption.ToLower().Equals("id") || dc.Caption.ToLower().Equals("usedrugid"))
                                        continue;//列不存在时跳出本次

                                    if (!ds.Tables[0].Columns.Contains(dc.Caption))
                                    {
                                        ds.Tables[0].Columns.Add(dc.Caption, dc.DataType);
                                        continue;//列不存在时添加，然后跳出本次
                                    }

                                    if (!dc.DataType.Name.ToLower().Equals("string"))
                                    {
                                        if (string.IsNullOrEmpty(dr[dc.Caption].ToString()))
                                        {
                                            dr[dc.Caption] = DBNull.Value;
                                        }
                                        else if (dc.DataType.Name.ToLower().Equals("boolean"))
                                        {
                                            bool b = dr[dc.Caption].ToString().Trim().Equals("0") ? false : true;
                                            dr[dc.Caption] = b;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    InternalLogger.Log.Error("1234567890：" + ex.Message);
                                }
                            }
                        }
                        dbHelp.CopyDataTableToDB(ds.Tables[0], tempTableName);
                        Console.WriteLine("一次同步"+ SynCode.ToString()+"结束，同步时间:" 
                            + item["EndTime"].ToString());
                    }
                }
                Console.WriteLine("全部同步"+ SynCode.ToString()+"结束");
            }
            catch (Exception EX)
            {
                InternalLogger.Log.Error("从xmlcopy数据到数据库表出错：" + EX.Message);
            }
        }

        static  void testSizeOf()
        {
            MsgChareResRow t = new MsgChareResRow();
            Console.WriteLine("MsgChareResRow(50):"+ Marshal.SizeOf(t).ToString());

            MsgDEmployeeRow t1 = new MsgDEmployeeRow();
            Console.WriteLine("MsgDEmployeeRow(38):" + Marshal.SizeOf(t1).ToString());

            MsgDrugRow t2 = new MsgDrugRow();
            Console.WriteLine("MsgDrugRow(116):" + Marshal.SizeOf(t2).ToString());

            MsgPatientRow t3 = new MsgPatientRow();
            Console.WriteLine("MsgPatientRow(92):" + Marshal.SizeOf(t3).ToString());

            MsgGeneral t4 = new MsgGeneral();
            Console.WriteLine("MsgGeneral(4):" + Marshal.SizeOf(t4).ToString());
        }

        private static void Program_OnEvent(object sender, EventArgs e)
        {
            string str = DateTime.Now.Second.ToString() + ";"+((int)sender).ToString();
            InternalLogger.Log.Debug(str);
            Thread.Sleep(1000);
        }

        static void TestEvent()
        {
            for (int i = 0; i < 10000; i++)
            {
                Thread.Sleep(10);
                if (OnEvent != null)
                    OnEvent(i, null);
            }
        }
        static void TestSerialPort()
        {
            BL_PLCSerialPort bl_plc = new BL_PLCSerialPort();
            bl_plc.OpenCom("COM4", 9600);
            bl_plc.SendDataSinalOut(4, 10, 1);
        }
        
        static void TestCharge()
        {
            ChargeBDGJ charge = new ChargeBDGJ();//计费接口
            string labelno = "";
            string UserID = "";
            string msg = String.Empty;
            charge.ChargeEX(labelno, UserID,out msg);

        }

        static void TestContains()
        {
            string s = "uuufff";
            string ww = String.Empty;
            if (s.Contains(ww))
            {
                Console.WriteLine("Contains");
            }
            else
            {
                Console.WriteLine("not Contains");
            }
        }
        static void ff()
        {
            Dictionary<int, int> d = new
                 Dictionary<int, int>();
            for (int i = 0; i < 10; i++)
            {
                d.Add(i, i);
            }

            for (int i = 0; i < 10; i++)
            {
                d[i] = 5;
            }
            foreach (var item in d)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }
        }
        static void Test1()
        {
            int maxCount = 100000;
            try
            {
                List<HotSpotEntities> collection = new List<HotSpotEntities>();
                for (int i = 0; i < maxCount; i++)
                {
                    Console.WriteLine(string.Format("成功创建连接对象{0}", i));
                    var db = new HotSpotEntities();
                    if (db.Connection.Open())
                    {
                        collection.Add(db);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        static string strConn = "Data Source=.;Initial Catalog=pivas.LY;User ID=sa;Password=123456";
        static string strSQL ="SELECT * FROM[pivas.LY].[dbo].[DEmployee] where[AccountID] ='9999'";
        static string strUpdateSQL = "update  [pivas.LY].[dbo].[DEmployee] set [Pas] ='123456' where [AccountID]= '9999'";
        static void test2()
        {
            for (int error = 0; error < 100; error++)
            {
                DataSet DS = new DataSet();
                DataTable dt = new DataTable();
                try
                {
                    for (int i = 0; i < 100; i++)
                    {
                        using (SqlCommand command = new SqlCommand(strSQL, new SqlConnection(strConn)))
                        {
                            command.CommandTimeout = 3600;//s
                            using (SqlDataAdapter da = new SqlDataAdapter(command))
                            {
                                //da.Fill(DS);
                                da.Fill(dt);
                            }
                        }
                        if (DS.Tables != null && DS.Tables.Count >= 1 && DS.Tables[0].Rows != null && DS.Tables[0].Rows.Count >= 1)
                        {
                            Console.WriteLine("序号：" + i.ToString() + ";账号ID：" + DS.Tables[0].Rows[0][1].ToString());
                        }
                        else if (dt.Rows != null && dt.Rows.Count >= 1)
                        {
                            Console.WriteLine("序号：" + i.ToString() + ";账号ID：" + dt.Rows[0][1].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("数据库读取错误" + ex.ToString());
                    throw ex;
                }
            }
        }

        static void test3()
        {
            for(int error=0;error<100;error++)
            {
                try
                {
                    int a = -1;
                    for (int i = 0; i < 100; i++)
                    {
                        using (SqlCommand Command = new SqlCommand(strUpdateSQL, new SqlConnection(strConn)))
                        {
                            Command.CommandTimeout = 3600;
                            Command.Connection.Open();
                            a = Command.ExecuteNonQuery();
                            Command.Connection.Close();
                        }
                        Console.WriteLine("序号：" + i.ToString() + ",返回值：" + a.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("错误号：" + error.ToString() + "数据库写入错误" + ex.ToString());
                    throw ex;
                }
            }
        }

        static void test4()
        {
            DataSet DS = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                for (int i = 0; i < 100000; i++)
                {
                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库读取错误" + ex.ToString());
            }
        }
    }
}
