using Communication.WindowsScreen;
using Newtonsoft.Json.Linq;
using PIVAsCommon;
using PIVAsCommon.Extensions;
using PIVAsCommon.Sockets;
using System;
using System.Net;
using System.Text;

namespace MccScreen
{
    public class ScreenTcpServer : IDisposable
    {
        private AsyncSocketTcpServer server;
        private int serverPort = -1;
        private int sessionId = -1;
        private IPEndPoint currentClient = null;

        /// <summary>
        /// 用于端口关闭后重启监听
        /// </summary>
        public bool IsListening  { get { return IsListening; }   }

        public ScreenTcpServer(int port, int bufferSize = 8192, bool useIPv6 = false)
        {
            server = new AsyncSocketTcpServer(port, bufferSize, useIPv6);
            server.Connected += Server_Connected;
            server.Disconnected += Server_Disconnected;
            server.DataReceived += Server_DataReceived;
            server.DataSent += Server_DataSent;
            server.ErrorOccurred += Server_ErrorOccurred;
            server.Started += Server_Started;
            server.Stopped += Server_Stopped;
            this.serverPort = port;
        }

        /// <summary>
        /// 获取已连接成功的客户端信息
        /// </summary>
        private void GetClientInfo()
        {
            try
            {
                if (server != null)
                {
                    InternalLogger.Log.Info("已连接成功的客户端个数为" + server.GetSessionCollection().Count);
                    foreach (var item in server.GetSessionCollection().Keys)
                    {
                        this.sessionId = item;
                        server.GetSessionCollection().TryGetValue(item, out this.currentClient);
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("获取已连接成功的客户端信息出错：" + ex.Message);
            }
        }

        //由Controller层显示在界面上提示用户监听状态
        public event EventHandler Stopped;
        private void Server_Stopped(object sender, EventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("ScreenTcpServer在端口{0}尝试去断开监听,sessionId={1}",
                this.serverPort, this.sessionId));
                if (Stopped != null)
                {
                    Stopped(sender, null);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理断开监听事件出错：" + ex.Message);
            }
        }

        //由Controller层显示在界面上提示用户监听状态
        public event EventHandler Started;
        private void Server_Started(object sender, EventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("ScreenTcpServerz在端口{0}尝试去打开监听",this.serverPort));
                if (Started != null)
                {
                    Started(sender, null);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理开启监听事件出错：" + ex.Message);
            }
        }

        //由Controller层显示在界面上提示用户
        public event EventHandler<PivasEventArgs<String>> ErrorOccurred;
        private void Server_ErrorOccurred(object sender, AsyncSocketErrorEventArgs e)
        {
            if (ErrorOccurred != null)
            {
                string errorInfo = String.Format("sessionId={0},在端口{1}监听出错:{2}", this.sessionId, this.serverPort, e.ToString());
                InternalLogger.Log.Error(errorInfo);
                ErrorOccurred(sender, new PivasEventArgs<String>(errorInfo));
            }
        }

        //基本不向客户端发消息
        private void Server_DataSent(object sender, AsyncSocketSessionEventArgs e)
        {
            InternalLogger.Log.Info(String.Format("ScreenTcpServer向{0}:{1}[{2}]发送数据{3}成功:",
                   this.currentClient == null ? null : currentClient.Address,
                   this.currentClient == null ? 0 : currentClient.Port,
                   this.sessionId, e.DataTransferred.BytesToHexString()));
        }

        private void Server_DataReceived(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                if (e.DataTransferred == null)
                {
                    InternalLogger.Log.Warn("接收到需要显示的数据为空，不处理");
                    return;
                }
                string data = Encoding.Default.GetString(e.DataTransferred);
                InternalLogger.Log.Debug("接收到需要显示的数据：" + data);
                ParseData(data);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理数据接收事件出错：" + ex.Message);
            }
        }

        //断开成功后，在界面提示用户
        public event EventHandler Disconnected;
        private void Server_Disconnected(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                InternalLogger.Log.Info(String.Format("ScreenTcpServer与({0}:{1}[{2}])已经断开连接:",
                    this.currentClient == null ? null : currentClient.Address,
                    this.currentClient == null ? 0 : currentClient.Port, 
                    this.sessionId));

                if (Disconnected != null)
                    Disconnected(sender, null);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理断开成功后事件出错：" + ex.Message);
            }
        }

        //连接成功后，在界面提示用户
        public event EventHandler Connected;
        private void Server_Connected(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                GetClientInfo();//成功后，获取客户端的信息
                InternalLogger.Log.Info(String.Format("ScreenTcpServer与({0}:{1}[{2}])已经断开连接:",
                   this.currentClient == null ? null : currentClient.Address,
                   this.currentClient == null ? 0 : currentClient.Port,
                   this.sessionId));
                if (Connected != null)
                    Connected(sender,null);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理连接成功后事件出错：" + ex.Message);
            }
        }

        public bool Start(bool throwOnError = false)
        {
            try
            {
                if (this.server != null)
                {
                    if (server.Start(throwOnError))
                        return true;
                }
            }
            catch(Exception ex)
            {
                InternalLogger.Log.Error("开启服务端监听出错"+ex.Message);
            }

            return false;
        }

        public bool Stop(bool throwOnError = false)
        {
            try
            {
                if (this.server != null)
                {
                    server.Stop(throwOnError);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("停止服务端监听出错" + ex.Message);
            }

            return false;
        }


        public event EventHandler<PivasEventArgs<MsgLoginStatus>> EventLogin;
        public event EventHandler<PivasEventArgs<MsgScreenInfo>> EventScreenInfo;
        private void ParseData(string data)
        {
            JObject message = JObject.Parse(data);
            string msgType = message["MsgType"].ToString().Trim();
            switch (short.Parse(msgType))
            {
                case (short)MsgType.LoginStatus:
                    if (EventLogin != null)
                        EventLogin(null, new PivasEventArgs<MsgLoginStatus>(message.ToObject<MsgLoginStatus>()));
                    break;
                case (short)MsgType.ScrennInfo:
                    if (EventScreenInfo != null)
                        EventScreenInfo(null, new PivasEventArgs<MsgScreenInfo>(message.ToObject<MsgScreenInfo>()));
                    break;
                default:
                    InternalLogger.Log.Warn("非法消息发送的屏MsgType=" + msgType);
                    break;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ScreenTcpServer() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
