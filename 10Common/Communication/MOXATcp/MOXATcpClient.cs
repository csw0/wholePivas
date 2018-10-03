using PIVAsCommon;
using PIVAsCommon.Extensions;
using PIVAsCommon.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Communication.MOXATcp
{
    
    /// <summary>
    /// MOXATcpClient，AsyncSocketTcpClient属于非托管资源
    /// AsyncSocketTcpClient的所有事件都最好加try处理，不然都会触发异常事件，造成连接断开
    /// </summary>
    public class MOXATcpClient : IDisposable
    {
        #region 私有
        private AsyncSocketTcpClient client;
        volatile List<byte> lstSinalMoxta = new List<byte>();//每次接收的MOxa数据
        private double dSaveSinalMoxtaTime = 0;
        private DateTime baseDatatime = new DateTime(2017, 1, 1);
        private object objectSaveSinalMoxtaTime = new object();
        #endregion

        #region 属性
        private string serverIp = String.Empty;
        public string ServerIp { get { return serverIp; } }
        private int serverPort = -1;
        public int ServerPort { get { return serverPort; } }
        private int sessionId = -1;
        public int SessionId { get { return sessionId; } }
        private bool connectStatus = false;//连接状态
        public bool ConnectStatus
        {
            get { return connectStatus; }
        }
        #endregion

        public MOXATcpClient(string remoteIP, int remotePort, int bufferSize = 8192)
        {
            client = new AsyncSocketTcpClient(remoteIP, remotePort, bufferSize);
            client.Connected += Client_Connected;
            client.Disconnected += Client_Disconnected;
            client.DataReceived += Client_DataReceived;
            client.ErrorOccurred += Client_ErrorOccurred;
            client.Started += Client_Started;
            client.Stopped += Client_Stopped;

            this.serverIp = remoteIP;
            this.serverPort = remotePort;
            //this.sessionId = client.SessionId; start后成功才能获取到sessionId
            
            Thread t = new Thread(() =>
             {
                 ClearSinalMoxta();
             });
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 连接成功后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public event EventHandler<PivasEventArgs<TCPMessage>> Connected;
        protected void Client_Connected(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("MOXATcpClient({0}:{1}[{2}])已经连接成功:",
                    this.serverIp, this.serverPort, this.sessionId));
                connectStatus = true;
                if (Connected != null)
                {
                    Connected(sender, new PivasEventArgs<TCPMessage>(new TCPMessage(
                        this.serverIp, this.serverPort, String.Empty)));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理连接成功后事件出错："+ex.Message);
            }
        }

        void ClearSinalMoxta()
        {
            while (true)
            {
                try
                {
                    lock (objectSaveSinalMoxtaTime)
                    {
                        double curTime = (DateTime.Now.ToUniversalTime() - baseDatatime).TotalMilliseconds;
                        double totalTime = curTime - dSaveSinalMoxtaTime;
                        if (totalTime > 3000 && lstSinalMoxta.Count > 0)//3秒，3秒还不处理，将其清空
                        {
                            lstSinalMoxta.Clear();
                        }
                    }
                   
                    Thread.Sleep(3000);
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("3秒清理lstSinalMoxta出错：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 断开成功后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public event EventHandler<PivasEventArgs<TCPMessage>> Disconnected;
        protected void Client_Disconnected(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("MOXATcpClient({0}:{1}[{2}])已经断开连接:",
                    this.serverIp, this.serverPort, this.sessionId));
                connectStatus = false;
                if (Disconnected != null)
                {
                    Disconnected(sender, new PivasEventArgs<TCPMessage>(new TCPMessage(
                       this.serverIp, this.serverPort, String.Empty)));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理断开成功后事件出错：" + ex.Message);
            }
        }


        public event EventHandler<PivasEventArgs<TCPMessage>> DataReceived;
        protected void Client_DataReceived(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                if (e.DataTransferred == null)
                {
                    InternalLogger.Log.Warn("接收到Moxa数据为空，不处理");
                    return;
                }
                InternalLogger.Log.Debug("接收到原始的moxa数据：" + e.DataTransferred.BytesToHexString());

                PackMoxaData(e.DataTransferred);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理数据接收事件出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 将完整的moxa数据入队列
        /// </summary>
        /// <param name="strMoxaData"></param>
        void MoxaDataEnQueue(string strMoxaData)
        {
            if (DataReceived != null)
            {
                DataReceived(this, new PivasEventArgs<TCPMessage>(new TCPMessage(this.serverIp, this.serverPort, strMoxaData)));
            }
            InternalLogger.Log.Info("接收到已分包完成的moxa数据：" + strMoxaData);
        }

        /// <summary>
        /// 对moxa数据拆包;更好的方法，还可以将包分类去处理。目前因类型较少，先不分类
        /// 此种方式对扫错条码，将无法兼容
        /// </summary>
        /// <param name="arrayMoxaData"></param>
        void PackMoxaData(byte[] arrayMoxaData)
        {
            try
            {
                for (int i = 0; i < arrayMoxaData.Length; i++)
                {
                    if (arrayMoxaData[i] > 0x20 && arrayMoxaData[i] != 0x7F)//过滤非法字符
                    {
                        lock (objectSaveSinalMoxtaTime)
                        {
                            lstSinalMoxta.Add(arrayMoxaData[i]);
                            dSaveSinalMoxtaTime = (DateTime.Now.ToUniversalTime() - baseDatatime).TotalMilliseconds;
                        }

                        string outMoxaData;
                        if (CheckMoxaData(out outMoxaData))
                        {
                            InternalLogger.Log.Debug("Moxa数据入队："+outMoxaData);
                            MoxaDataEnQueue(outMoxaData);
                            lstSinalMoxta.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("递归分包Moxa数据时出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 检测是否是合法的瓶签（含登录手牌）
        /// </summary>
        /// <returns>true=合法</returns>
        bool CheckMoxaData(out string moxaData)
        {
            moxaData = string.Empty;
            try
            {
                //判断是不是手牌，长度为22且头四位为7777
                if (lstSinalMoxta.Count == 22)
                {
                    if (Encoding.UTF8.GetString(lstSinalMoxta.Take(4).ToArray()).Equals("7777"))
                    {
                        moxaData = Encoding.UTF8.GetString(lstSinalMoxta.Take(22).ToArray());
                        return true;
                    }
                }

                //判断是不是瓶签，长度为tagLabelLen；
                if (lstSinalMoxta.Count == tagLabelLen)
                {
                    string tempMoxa = Encoding.UTF8.GetString(lstSinalMoxta.Take(tagLabelLen).ToArray());
                    //包含标记且开头四位不是7777;可以确认是瓶签(含标准瓶签和第三方瓶签)。tagLabelMark标准瓶签时为empty
                    //加不是手牌的条件原因是，因为标准瓶签长度小于手牌长度时，会截取手牌前14位作为误认为瓶签
                    if (tempMoxa.Contains(tagLabelMark) && (tempMoxa.Length > 4 && !tempMoxa.Substring(0,4).Equals("7777")))
                    {
                        moxaData = tempMoxa;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("检测是否是合法的瓶签（含登录手牌）出错：" + ex.Message);
            }
            return false;
        }
        protected void Client_ErrorOccurred(object sender, AsyncSocketErrorEventArgs e)
        {
            InternalLogger.Log.Error(String.Format("MOXATcpClient({0}:{1}[{2}])通信过程中出错:",
                this.serverIp, this.serverPort, this.sessionId) + e.ToString());
        }

        protected void Client_Started(object sender, EventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("MOXATcpClient({0}:{1}[{2}])尝试去打开连接:",
                this.serverIp, this.serverPort, this.sessionId));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理开启连接事件出错：" + ex.Message);
            }
        }

        protected void Client_Stopped(object sender, EventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("MOXATcpClient({0}:{1}[{2}])尝试去断开连接:",
                this.serverIp, this.serverPort, this.sessionId));
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理断开连接事件出错：" + ex.Message);
            }
        }

        public bool Start(bool throwOnError = false)
        {
            try
            {
                if (this.client != null)
                {
                    if (client.Start(throwOnError))
                    {
                        this.sessionId = client.SessionId;
                        return true;
                    }
                }
            }
            catch 
            {
                InternalLogger.Log.Error(String.Format("因{0}:{1}[SessionId={2}]连接对象已释放掉，开启连接失败。",
                    this.ServerIp, this.ServerPort, this.SessionId));
            }
           
            return false;
        }

        /// <summary>
        /// 停止连接不释放
        /// </summary>
        /// <param name="throwOnError"></param>
        /// <returns></returns>
        public bool Stop(bool throwOnError = false)
        {
            try
            {
                connectStatus = false;//防止断开未成功情况时，connectStatus未赋值为false
                if (this.client != null)
                {
                    client.Stop(throwOnError);
                }
            }
            catch
            {
                InternalLogger.Log.Error(String.Format("因{0}:{1}[SessionId={2}]连接对象已释放掉，关闭连接失败。",
                    this.ServerIp, this.ServerPort, this.SessionId));
            }
           
            return false;
        }

        #region MyRegion临时属性，更好的方式是在硬件层面设置moxa的分隔符
        /// <summary>
        /// 瓶签长度，第三方瓶签时可配置，标准瓶签此值为14
        /// </summary>
        private static int tagLabelLen = 14;
        public static int TagLabelLen
        {
            set { tagLabelLen = value; }
        }
        /// <summary>
        /// 此值是瓶签的标识，第三方瓶签标识可配置，标准瓶签此值为empty
        /// </summary>
        private static string tagLabelMark = String.Empty;
        public static string TagLabelMark
        {
            set { tagLabelMark = value; }
        }
        #endregion

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
                this.connectStatus = false;
                if (this.client!= null)
                {
                    this.client.Dispose();
                    this.client = null;
                }
                this.DataReceived = null;
                this.Connected = null;
                this.Disconnected = null;
            }
            //非托管对象释放，不管是手动还是系统都要执行；
            Stop();
        }
        /// <summary>
        /// GC使用
        /// </summary>
        ~MOXATcpClient()
        {
            this.Dispose(false);
        }
        #endregion
    }
}
