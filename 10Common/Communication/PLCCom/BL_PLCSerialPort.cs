using PIVAsCommon;
using PIVAsCommon.Extensions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

namespace Communication.PLCCom
{
    /// <summary>
    /// 博龙PLC串口通信类
    /// 长度为6字节，
    /// 第1字节：PLC地址（01~04）
    /// 第2字节：PLC控制模式（目前只用02标识控制单个灯）
    /// 第3字节：PLC控制输出口地址（00~FE）
    /// 第4字节：控制灯命令（0：关，1：开）
    /// 第5字节：前四个字节的CRC校验码
    /// 第6字节：CRC校验码右移8位。
    /// </summary>
    public class BL_PLCSerialPort
    {
        private SerialPort spPLC = null;
        //private AutoResetEvent SendSuccessAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// 打开Com口
        /// </summary>
        /// <param name="portName">com名</param>
        /// <param name="baudRate">波特率</param>
        public void OpenCom(string portName, int baudRate)
        {
            try
            {
                spPLC = new SerialPort(portName, baudRate,Parity.None,8,StopBits.One);
                //spPLC.DataReceived += SpPLC_DataReceived;
                spPLC.DataReceived += DataReceivedEx;
                spPLC.ErrorReceived += SpPLC_ErrorReceived;
                spPLC.Open();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("打开COM口失败："+ ex.Message);
            }
        }

        public void CloseCom()
        {
            try
            {
                if (spPLC != null)
                {
                    spPLC.Close();
                    spPLC = null;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("关闭COM口出错：" + ex.Message);
            }
        }
        private void SpPLC_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            try
            {
                String msg = String.Empty;
                switch (e.EventType)
                {
                    case SerialError.RXOver:
                        msg = "发生输入缓冲区溢出。输入缓冲区空间不足，或在文件尾 (EOF) 字符之后接收到字符";
                        break;
                    case SerialError.Overrun:
                        msg = "发生字符缓冲区溢出。下一个字符将丢失";
                        break;
                    case SerialError.RXParity:
                        msg = "硬件检测到奇偶校验错误";
                        break;
                    case SerialError.Frame:
                        msg = "硬件检测到一个组帧错误";
                        break;
                    case SerialError.TXFull:
                        msg = "应用程序尝试传输一个字符，但是输出缓冲区已满";
                        break;
                    default: break;
                }
                InternalLogger.Log.Error("与COM口通信出错：" + msg);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理COM口出错信息异常：" + ex.Message);
            }
        }

        private List<byte> buffer = new List<byte>(4096);
        private void DataReceivedEx(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] readBuffer = new byte[6];
                int n = spPLC.BytesToRead;
                byte[] buf = new byte[n];
                spPLC.Read(buf, 0, n);
                //1.缓存数据           
                buffer.AddRange(buf);
                //2.完整性判断         
                while (buffer.Count >= 6)
                {
                    buffer.CopyTo(0, readBuffer, 0, 6);
                    //从缓冲区中清除
                    buffer.RemoveRange(0, 6);
                    //触发外部处理接收消息事件
                    InternalLogger.Log.Info("从COM接收到数据:" + readBuffer.BytesToHexString());
                    new Thread(() =>
                    {
                        if (eventDataReceive != null)
                            eventDataReceive(this, new PivasEventArgs<byte[]>(readBuffer));
                    }).Start();
                }
            }
            catch
            {
                InternalLogger.Log.Error("DataReceivedEx出错");
            }
        }

        private const int BUFF_LEN = 6;//收发数组长度
        private volatile byte[] bufSend = null;
        private volatile byte[] recvBytes = new byte[BUFF_LEN];
        private volatile int bufIndex = 0;

        public event EventHandler<PivasEventArgs<byte[]>> eventDataReceive = null;

        private void SpPLC_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            #region csw Mcc程序中不用
            string RcvData = string.Empty;
            try
            {
                for (int i = 0; i < spPLC.BytesToRead; i++)
                {
                    byte b = (byte)spPLC.ReadByte();

                    InternalLogger.Log.Info("从COM接收到字节:" + b);

                    if (bufIndex < 6)//最大为5
                    {
                        recvBytes[bufIndex] = b;
                        if (bufIndex == 5)//出口
                        {
                            Interlocked.Add(ref bufIndex, -bufIndex);//值0
                            InternalLogger.Log.Info("从COM接收到数据:" + recvBytes.BytesToHexString());
                            new Thread(() =>
                            {
                                if (eventDataReceive != null)
                                    eventDataReceive(this, new PivasEventArgs<byte[]>(recvBytes));
                            }).Start();

                            //if (ReceiveReply(recvBytes))
                            //{
                            //    //SendSuccessAutoResetEvent.Set();
                            //    InternalLogger.Log.Info("从COM接收到数据:" + recvBytes.BytesToHexString());
                            //}
                        }
                        else
                            Interlocked.Add(ref bufIndex, 1);
                    }
                    else 
                        InternalLogger.Log.Error("处理COM读取的数据出错");
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("从COM读取数据出错：" + ex.Message);
            }
            //InternalLogger.Log.Info("从COM接收到数据:" + RcvData);
            #endregion
        }

        /// <summary>
        /// 第二种模式，只控制单个端口
        /// </summary>
        /// <param name="plcAddress">PLC地址</param>
        /// <param name="plcOutPort">PLC输出控制端口地址</param>
        /// <param name="lightStatus">控制灯命令（0：关，1：开）</param>
        public void SendDataSinalOut(byte plcAddress, byte plcOutPort, byte lightStatus)
        {
            try
            {
                lock (spPLC)
                {
                    if (spPLC.IsOpen)
                    {
                        byte[] buf = new byte[BUFF_LEN];
                        buf[0] = plcAddress;
                        buf[1] = 2;
                        buf[2] = plcOutPort;
                        buf[3] = lightStatus;
                        int crcCode = CRC8(buf, 4);
                        buf[4] = (byte)crcCode;
                        buf[5] = (byte)(crcCode >> 8);

                        spPLC.Write(buf, 0, buf.Length);
                        InternalLogger.Log.Info("发送数据(" + buf.BytesToHexString() + ")到PLC=" + plcAddress.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送数据到Com口出错：" + ex.Message);
            }
        }

        
        /// <summary>
        /// 第一种模式，所有端口一起发
        /// </summary>
        /// <param name="plcAddress">PLC地址</param>
        /// <param name="thirdFrame">1~8端口;一个bit一个端口</param>
        /// <param name="fourthFrame">9~16端口；一个bit一个端口</param>
        /// <returns></returns>
        public void SendDataAllOut(byte plcAddress, byte thirdFrame, byte fourthFrame)
        {
            try
            {
                if (spPLC.IsOpen)
                {
                    bufSend = new byte[BUFF_LEN];
                    bufSend[0] = plcAddress;
                    bufSend[1] = 1;//第一种模式，所有端口一起发
                    bufSend[2] = thirdFrame;
                    bufSend[3] = fourthFrame;
                    int crcCode = CRC8(bufSend, 4);
                    bufSend[4] = (byte)crcCode;
                    bufSend[5] = (byte)(crcCode >> 8);                    
                    
                    spPLC.Write(bufSend, 0, bufSend.Length);
                    spPLC.BaseStream.Flush();
                    //SendSuccessAutoResetEvent.WaitOne(150);//等待接收到帧

                    InternalLogger.Log.Info("发送数据(" + bufSend.BytesToHexString() + ")到PLC=" + plcAddress.ToString());
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送数据到Com口出错：" + ex.Message);
            }
        }

        /// 
        /// memcmp API
        /// 
        /// 字节数组1
        /// 字节数组2
        /// 如果两个数组相同，返回0；如果数组1小于数组2，返回小于0的值；如果数组1大于数组2，返回大于0的值。
        [DllImport("msvcrt.dll")]
        private static extern IntPtr memcmp(byte[] b1, byte[] b2, IntPtr count);

        private bool ReceiveReply(byte[] receiveBuf)
        {
            try
            {
                if (bufSend != null)
                {
                    IntPtr retval = memcmp(receiveBuf, bufSend, new IntPtr(receiveBuf.Length));
                    if (retval.ToInt32() == 0)
                        return true;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("比较发送与接收是否相等出错：" + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// CRC校验算法
        /// </summary>
        /// <param name="ptr">字节数组</param>
        /// <param name="len">需生成校验码的数组长度，此值需小于等于ptr的长度</param>
        /// <returns></returns>
        private int CRC8(byte[] ptr, int len)
        {
            int crc = 0xFFFF;
            for (int j = 0; j < len; j++)
            {
                crc ^= ptr[j];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x01) > 0)
                    {
                        crc = (crc >> 1) ^ 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }
    }
}
