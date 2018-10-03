using Communication.MOXATcp;
using PIVAsCommon;
using PIVAsCommon.Extensions;
using PIVAsCommon.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Communication.DisplayTcp
{
    public class DisplayTcpClient : IDisposable
    {
        #region 私有
        private AsyncSocketTcpClient client;
        List<byte> lstSinalDisplay = new List<byte>();//每个待发送的Display数据
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
            set { connectStatus = value; }
        }
        #endregion

        public DisplayTcpClient(string remoteIP, int remotePort, int bufferSize = 8192)
        {
            client = new AsyncSocketTcpClient(remoteIP, remotePort, bufferSize);
            client.Connected += Client_Connected;
            client.Disconnected += Client_Disconnected;
            client.DataReceived += Client_DataReceived;
            client.DataSent += Client_DataSent;
            client.ErrorOccurred += Client_ErrorOccurred;
            client.Started += Client_Started;
            client.Stopped += Client_Stopped;

            this.serverIp = remoteIP;
            this.serverPort = remotePort;
            //this.sessionId = client.SessionId; start后成功才能获取到sessionId
        }

        private void Client_DataSent(object sender, AsyncSocketSessionEventArgs e)
        {
            InternalLogger.Log.Info(String.Format("DisplayTcpClient({0}:{1}[{2}])发送数据{3}成功:",
                   this.serverIp, this.serverPort, this.sessionId,e.DataTransferred.BytesToHexString()));
        }


        /// <summary>
        /// 连接成功后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public event EventHandler<PivasEventArgs<TCPMessageEx>> Connected;
        protected void Client_Connected(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("MOXATcpClient({0}:{1}[{2}])已经连接成功:",
                    this.serverIp, this.serverPort, this.sessionId));
                connectStatus = true;
                if (Connected != null)
                {
                    Connected(sender, new PivasEventArgs<TCPMessageEx>(new TCPMessageEx(
                        this.serverIp, this.serverPort, null)));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理连接成功后事件出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 非客户端主动断开成功后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public event EventHandler<PivasEventArgs<TCPMessageEx>> Disconnected;
        protected void Client_Disconnected(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("MOXATcpClient({0}:{1}[{2}])已经断开连接:",
                    this.serverIp, this.serverPort, this.sessionId));
                connectStatus = false;

                if (Disconnected != null)
                {
                    Disconnected(sender, new PivasEventArgs<TCPMessageEx>(new TCPMessageEx(
                       this.serverIp, this.serverPort, null)));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理断开成功后事件出错：" + ex.Message);
            }
        }

        protected void Client_DataReceived(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                if (e.DataTransferred == null)
                {
                    InternalLogger.Log.Warn("接收到Display数据为空，不处理");
                    return;
                }
                InternalLogger.Log.Debug("接收到原始的display数据：" + e.DataTransferred.BytesToHexString());
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理数据接收事件出错：" + ex.Message);
            }
        }

        protected void Client_ErrorOccurred(object sender, AsyncSocketErrorEventArgs e)
        {
            InternalLogger.Log.Error(String.Format("DisplayTcpClient({0}:{1}[{2}])通信过程中出错:",
                this.serverIp, this.serverPort, this.sessionId) + e.ToString());
        }

        protected void Client_Started(object sender, EventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("DisplayTcpClient({0}:{1}[{2}])尝试去打开连接:",
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
                InternalLogger.Log.Info(String.Format("DisplayTcpClient({0}:{1}[{2}])尝试去断开连接:",
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

        public bool Send(byte[] displayData )
        {
            try
            {
                return client.Send(displayData);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(String.Format("{0}:{1}[SessionId={2}]发送数据出错{3}。",
                    this.ServerIp, this.ServerPort, this.SessionId, ex.Message));
            }
            return false;
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
                this.connectStatus = false;
                if (this.client != null)
                {
                    this.client.Dispose();
                    this.client = null;
                }
                this.Connected = null;
                this.Disconnected = null;
            }
            //非托管对象释放，不管是手动还是系统都要执行；
            Stop();
        }

        /// <summary>
        /// GC使用
        /// </summary>
        ~DisplayTcpClient()
        {
            this.Dispose(false);
        }
        #endregion
    }
}
