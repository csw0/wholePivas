using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using Communication.PLCCom;
using Amib.Threading;
using PIVAsCommon.Helper;
using PIVAsCommon;

namespace PivasMcc
{
    public class Light:IDisposable
    {
        DB_Help DB = new DB_Help();
        DataSet ds = new DataSet();
        private string PlcType=string.Empty;
        private ActFxCpu Ax;
        //private static BL_PLCSerialPort bl_plc = new BL_PLCSerialPort();
        private static WinAPI_ServialPort WinApi_PLC = new WinAPI_ServialPort();

        //多个PLC都会接在同一类串口线上，实现并行。一个plc可控制输出多个灯
        //保存打包后的PLC指令,key是plc地址
        private static Dictionary<int, PLCCommand> dicSendWindow = new Dictionary<int, PLCCommand>();
        private DateTime baseDatatime = new DateTime(2017, 1, 1);
        private SmartThreadPool smartThreadPool = new SmartThreadPool();//线程池，可能有问题
        private static Thread threadHandlePackPLCComm = null;

        private const int SENDTOPLC_INTERVAL = 150;//向同一PLC发送间隔，毫秒
        private const int PLC_INTERRUPT_INTERVAL = 200;//同plc同输出口，中断间隔。

        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="COM">PLC 连接的COM口</param>
        /// <param name="Port">波特率</param>
        /// <param name="Port">plc类型</param>
        public Light(string COM, int Rate, string PlcType)
        {
            try
            {
                this.PlcType = PlcType;
                if (PlcType == "BL-PLC") 
                {
                    //bl_plc.CloseCom();
                    //bl_plc.OpenCom(COM, Rate);

                    if (WinApi_PLC.Opened)
                        WinApi_PLC.Close();
                    WinApi_PLC.PortNum = COM;
                    WinApi_PLC.BaudRate = Rate;
                    WinApi_PLC.ByteSize = 8;
                    WinApi_PLC.Open();
                }
                else if (PlcType == "SL-PLC") 
                {
                    Ax = new ActFxCpu();
                    Ax.axActFXCPU1.ActPortNumber = int.Parse(COM.Substring(3, 1));
                    int ret = Ax.axActFXCPU1.Open();
                    
                }
                string SQL = "SELECT * FROM MOXACon m INNER JOIN LightModel l on l.DeskNo=m.DeskNo ";
                ds = DB.GetPIVAsDB(SQL);

                smartThreadPool.Start();
                if (threadHandlePackPLCComm == null)
                {
                    threadHandlePackPLCComm = new Thread(HandlePackPLCComm);
                    threadHandlePackPLCComm.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接plc失败,请检查PLC配置是否正确!"+ex.Message);                
            }
        }

        #region 控制向plc发送间隔
        /// <summary>
        /// 打包PLC指令
        /// PLC首次，将plc地址保存，将<led地址,发送时刻>保存;指令打包并保存
        /// PLC非首次，但LED首次，将指令打包保存
        /// PLC非首次且LED非首次，判断间隔是否满足设定；（若满足，将指令打包保存;若不满足，则新线程等待设定时间后，打包保存)
        /// </summary>
        /// <param name="plcAddress">PLC地址号</param>
        /// <param name="lightAddress">PLC控制输出端口号(一般16个)</param>
        /// <param name="lightStatus">1开灯，0关灯</param>
        /// <param name="lightTime">灯开闭间隔</param>
        private void ControlPLCInterval(int plcAddress, int lightAddress, byte lightStatus,int lightTime)
        {
            try
            {
                double curTime = (DateTime.Now.ToUniversalTime() - baseDatatime).TotalMilliseconds;
                lock (dicSendWindow)
                {
                    PLCCommand command = null;
                    if (dicSendWindow.ContainsKey(plcAddress))
                    {
                        dicSendWindow.TryGetValue(plcAddress, out command);
                        if (command.DicLightSendTime.ContainsKey(lightAddress))//此灯已发送过
                        {
                            double preTime = 0;
                            command.DicLightSendTime.TryGetValue(lightAddress, out preTime);
                            double totalTime = curTime - preTime;
                            if (totalTime < lightTime)//时间间隔小于设定值
                            {
                                int sleepReply = (int)Math.Ceiling(lightTime - totalTime);
                                #region 在sleep前，需把本次的时间戳保存，防止后面还有指令进来
                                command.DicLightSendTime[lightAddress] = curTime + sleepReply;//更新发送时刻
                                dicSendWindow[plcAddress] = command;//更新
                                #endregion
                                smartThreadPool.QueueWorkItem(() =>
                                {
                                    //新线程，先sleep（lightTime-totalTime）后，再lock放入dicSendWindow
                                    Thread.Sleep(sleepReply);
                                    lock (dicSendWindow)
                                    {
                                        SetFrame(command, lightAddress, lightStatus);
                                        command.DicLightSendTime[lightAddress] = curTime + sleepReply;//更新发送时刻
                                        command.Send = false;
                                        dicSendWindow[plcAddress] = command;//更新
                                    }
                                });
                                return;//直接退出，不要删除
                            }
                            else//大于设定间隔
                                command.DicLightSendTime[lightAddress] = curTime;//更新发送时刻
                        }
                        else//此灯第一次发送
                            command.DicLightSendTime.Add(lightAddress, curTime);//将plc下灯地址发送时刻保存
                        
                        SetFrame(command, lightAddress, lightStatus);
                        command.Send = false;
                        dicSendWindow[plcAddress] = command;//更新
                    }
                    else
                    {
                        command = new PLCCommand();//command.Send = false;默认
                        SetFrame(command, lightAddress, lightStatus);
                        command.DicLightSendTime.Add(lightAddress, curTime);//将plc下灯地址发送时刻保存
                        dicSendWindow.Add(plcAddress, command);//新增
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("打包PLC指令出错：" + ex.Message);
            }
        }

        /// <summary>
        ///打包帧
        /// </summary>
        /// <param name="command"></param>
        /// <param name="lightAddress">从0开始到15</param>
        /// <param name="lightStatus"></param>
        void SetFrame(PLCCommand command,int lightAddress, byte lightStatus)
        {
            if (lightAddress >= 0 && lightAddress <= 7)
                command.ThirdFrame = SetBit(command.ThirdFrame, 8 - lightAddress, lightStatus == 1);//设置第三帧
            else
                command.FourthFrame = SetBit(command.FourthFrame, 16 - lightAddress, lightStatus == 1);//设置第四帧
        }

        /// <summary>
        /// 设置某一位的值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index">要设置的位， 值从低到高为 1-8</param>
        /// <param name="flag">要设置的值 true / false</param>
        /// <returns></returns>
        byte SetBit(byte data, int index, bool flag)
        {
            if (index <= 8 || index >= 1)
            {
                int v = index < 2 ? index : (2 << (index - 2));
                return flag ? (byte)(data | v) : (byte)(data & ~v);
            }
            return data;//大于255时，返回原值
        }

        private volatile int sleepTimes = 0;//对休眠次数计数
        /// <summary>
        /// 处理PLC打包指令，每SENDTOPLC_INTERVAL毫秒发一次，
        /// 同一PLC上次与本次间隔为PLC个数*SENDTOPLC_INTERVAL，此值过大，会造成灯指令覆盖
        /// </summary>
        void HandlePackPLCComm()
        {
            while (true)
            {
                try
                {
                    int[] strKey = null;
                    lock (dicSendWindow)
                    {
                        int dicCount = dicSendWindow.Keys.Count;
                        strKey = new int[dicCount];
                        dicSendWindow.Keys.CopyTo(strKey, 0);
                    }

                    int PLCCount = strKey.Length;
                    for (int i = 0; i < PLCCount; i++)
                    {
                        int key = strKey[i];
                        PLCCommand command = null;
                        lock (dicSendWindow)
                        {
                            command = dicSendWindow[strKey[i]];
                        }

                        if (!command.Send)
                        {
                            Interlocked.Add(ref sleepTimes, 1);//加1

                            WinApi_PLC.SendDataAllOut((byte)key, command.ThirdFrame, command.FourthFrame);
                            //bl_plc.SendDataAllOut((byte)key, command.ThirdFrame, command.FourthFrame);
                            command.Send = true;//更新为已发送
                            lock (dicSendWindow)
                            {
                                dicSendWindow[key] = command;
                            }
                            //每个包之间都要间隔200毫秒，不然plc接收会粘包，而且plc同地址会中断
                            Thread.Sleep(SENDTOPLC_INTERVAL);
                        }
                    }
                    //循环结束后，sleepTimes=0;休眠中断间隔，=1休眠剩余时间间隔
                    if (sleepTimes <= 1)
                    {
                        int sleepInterval = PLC_INTERRUPT_INTERVAL - sleepTimes * SENDTOPLC_INTERVAL;
                        Thread.Sleep(sleepInterval < 0 ? 0 : sleepInterval);
                    }
                    Interlocked.Add(ref sleepTimes, -sleepTimes);//0
                }
                catch (Exception ex)
                {
                    Thread.Sleep(PLC_INTERRUPT_INTERVAL);
                    InternalLogger.Log.Error("处理PLC打包指令出错：" + ex.Message);
                }
            }
        }
        #endregion

        /// <summary>
        /// 查询PLC地址和LED地址
        /// </summary>
        public void Lighting(ControlLightCommand command)
        {
            try
            {
                #region 实现对PLC地址和LED地址的查询
                DataRow dr;
                if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Select("MOXAIP='" + command.MoxaIP +
                    "' AND MOXAPort =" + command.MoxaPort).Length > 0)
                {
                    dr = ds.Tables[0].Select("MOXAIP='" + command.MoxaIP + "' AND MOXAPort =" + command.MoxaPort)[0];
                }
                else
                {
                    return;
                }

                int lightAddress = 0;//灯地址，一个PLC的地址对应多个灯地址；从数据库中拉取的对应关系
                if (command.LightColor == 1)
                {
                    lightAddress = Convert.ToInt32(dr["RedLight"].ToString());
                }
                else if (command.LightColor == 2)
                {
                    lightAddress = Convert.ToInt32(dr["GreenLight"].ToString());
                }
                else
                {
                    return;
                }

                string PLC = dr["PLC"].ToString();//PLC地址号
                int iPLC = Convert.ToInt32(PLC);
                #endregion

                //将指令打包
                ControlPLCInterval(iPLC, lightAddress, (byte)command.LightStatus, command.LightTime);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("查询PLC地址和LED地址出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 带超时时间的方法调用
        /// </summary>
        /// <param name="action"></param>
        /// <param name="timeoutMilliseconds"></param>
        void CallWithTimeout(System.Action action, int timeoutMilliseconds)
        {
            try
            {
                if (action == null)
                    return;

                Thread threadToKill = null;
                System.Action wrappedAction = () =>
                {
                    threadToKill = Thread.CurrentThread;
                    action();
                };

                IAsyncResult result = wrappedAction.BeginInvoke(null, null);
                if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
                {
                    wrappedAction.EndInvoke(result);
                }
                else
                {
                    threadToKill.Abort();
                    throw new TimeoutException();
                }
            }
            catch (TimeoutException)
            {
                //action.Method.Name获取方法名，匿名时采用<父函数名>b_0_0
                InternalLogger.Log.Error(String.Format("调用{0}方法超过{1}毫秒，请检查与设备通信链路。",action.Method.Name, timeoutMilliseconds));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("带超时时间的方法调用出错：" + ex.Message);
            }
        }

        #region IDispose机制
        /// <summary>
        /// 提供外部手动释放接口；gc不会用
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 标记是否被释放过(不管是系统还是手动)
        /// </summary>
        protected bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            this._disposed = true;

            if (disposing)
            {
                //释放托管资源,系统调用时不进来。用于主动释放
                ds = null;
                dicSendWindow = null;
                DB = null;
                try
                {
                    threadHandlePackPLCComm.Abort();
                }
                catch {}
            }
            //非托管对象释放，不管是手动还是系统都要执行；
            try
            {
                threadHandlePackPLCComm = null;
                smartThreadPool.Dispose();
                Ax.Dispose();
            }
            catch { }
        }
        /// <summary>
        /// GC使用
        /// </summary>
        ~Light()
        {
            this.Dispose(false);
        }
        #endregion
    }
}