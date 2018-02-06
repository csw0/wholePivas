using Communication.DisplayTcp;
using PIVAsCommon;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Communication.screenTcp
{
    /// <summary>
    /// 舱内Windows系统屏操作的控制类
    /// </summary>
    public class ScreenClientController : IDisposable
    {
        /// <summary>
        /// 保存所有屏连接;开启在线检测线程中会变遍历
        /// 打开成功进行保存，后续不释放，只进行开闭操作
        /// </summary>
        private static List<ScreenTcpClient> lstScreenTcpClient = new List<ScreenTcpClient>();
        //发送对列和接收队列
        private static Queue<TCPMessage> sendQueue = new Queue<TCPMessage>();
        private static Queue<TCPMessage> receiveQueue = new Queue<TCPMessage>();

        private Thread threadscreenStatus_check = null;//屏连接状态检测
        private Thread threadscreenSend = null;//发送到屏数据的处理线程
        private Thread threadscreenReceive = null;//接收屏数据的处理线程
        private AutoResetEvent moxaOpenAutoResetEvent = new AutoResetEvent(false);

        public ScreenClientController()
        {
            try
            {
                threadscreenSend = new Thread(ScreenDataSend);
                threadscreenSend.Start();
                threadscreenReceive = new Thread(ScreenDataReceive);
                threadscreenReceive.Start();
                threadscreenStatus_check = new Thread(StartscreenStatusCheck);
                threadscreenStatus_check.Start();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("开启Screen检测线程出错" + ex.Message);
            }
        }

        private bool _ReConnnect = false;//默认是不需要重连的
        public bool ReConnnect { set { _ReConnnect = value; } }

        #region Controller提供外部调用的方法
        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="remoteIP"></param>
        /// <param name="remotePort"></param>
        /// <returns>返回端口号;为了兼容</returns>
        public void ScreenClose(string remoteIP, int remotePort)
        {
            ScreenTcpClient ScreenTcpClient = null;
            bool bContainScreenTcpClient = ContainTcpClient(remoteIP, remotePort, out ScreenTcpClient);
            if (bContainScreenTcpClient)
            {
                ScreenTcpClient.Stop();
            }
        }

        /// <summary>
        /// 打开screen连接;存在就直接打开，不存在就新建再打开
        /// </summary>
        /// <param name="remoteIP">ip</param>
        /// <param name="remotePort">端口号</param>
        /// <returns>返回端口号;为了兼容</returns>
        public bool ScreenOpen(string remoteIP, int remotePort)
        {
            ScreenTcpClient screenTcpClient = null;
            bool bContainScreenTcpClient = ContainTcpClient(remoteIP, remotePort, out screenTcpClient);
            if (!bContainScreenTcpClient)
            {
                screenTcpClient = new ScreenTcpClient(remoteIP, remotePort);
                screenTcpClient.Connected += ScreenTcpClient_Connected;
                screenTcpClient.Disconnected += ScreenTcpClient_Disconnected;
                screenTcpClient.DataReceived += ScreenTcpClient_DataReceived;
            }
            else
            {
                screenTcpClient.Stop();//每次打开前，先关闭一下
            }

            if (screenTcpClient.Start())
            {
                if (!bContainScreenTcpClient)
                {
                    lstScreenTcpClient.Add(screenTcpClient);
                }
            }

            try
            {
                if (moxaOpenAutoResetEvent.WaitOne(600))
                    return true;
            }
            catch {}

            return false;
        }

        /// <summary>
        /// 接收到瓶签数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenTcpClient_DataReceived(object sender, PivasEventArgs<TCPMessage> e)
        {
            try
            {
                lock (receiveQueue)
                {
                    receiveQueue.Enqueue(e.Value);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("将瓶签号入队出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 将待发送到屏的信息，放入队列
        /// </summary>
        /// <param name="e"></param>
        public void sendDataEnqueue(PivasEventArgs<TCPMessage> e)
        {
            try
            {
                //多个连接都会入队            
                lock (sendQueue)
                {
                    sendQueue.Enqueue(e.Value);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("发送到屏的数据入队出错:" + ex.Message);
            }
        }
        #endregion

        #region TCPClient暴露出来的事件，用于UI更新
        public event EventHandler<PivasEventArgs<TCPMessage>> Disconnected; 
        private void ScreenTcpClient_Disconnected(object sender, PivasEventArgs<TCPMessage> e)
        {
            if (Disconnected != null)
            {
                Disconnected(sender, e);
            }
        }

        public event EventHandler<PivasEventArgs<TCPMessage>> Connected;
        private void ScreenTcpClient_Connected(object sender, PivasEventArgs<TCPMessage> e)
        {
            moxaOpenAutoResetEvent.Set();
            if (Connected != null)
            {
                Connected(sender, e);
            }
        }
        #endregion

        #region Controller内部处理的方法和线程
        private bool ContainTcpClient(string remoteIP, int remotePort, out ScreenTcpClient ScreenTcpClient)
        {
            try
            {
                for (int i = 0; i < lstScreenTcpClient.Count; i++)
                {
                    int port = lstScreenTcpClient[i].ServerPort;
                    if (lstScreenTcpClient[i].ServerIp.Trim().Equals(remoteIP) &&
                        port == remotePort)
                    {
                        ScreenTcpClient = lstScreenTcpClient[i];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(String.Format("判断screen连接表中是否包含{0}:{1}出错.{2}",
                    remoteIP, remotePort, ex.Message));
            }

            ScreenTcpClient = null;
            return false;
        }

        /// <summary>
        /// 从队列中读取Screen数据，然后发送到屏
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="buf"></param>
        /// <param name="frameMaxLen"></param>
        /// <returns></returns>
        void ScreenDataSend()
        {
            TCPMessage screenMsgModel = null;
            while (true)
            {
                try
                {
                    screenMsgModel = null;
                    Thread.Sleep(5);//稍作停留
                    if (sendQueue.Count > 0)
                    {
                        lock (sendQueue)
                        {
                            screenMsgModel = sendQueue.Dequeue();
                        }
                    }

                    //从连接列表中找到tcp对象，调用send方法，发送
                    ScreenTcpClient ScreenTcpClient = null;
                    if (screenMsgModel != null)
                    {
                        bool bContainScreenTcpClient = ContainTcpClient(screenMsgModel.ServerIp,
                        screenMsgModel.ServerPort, out ScreenTcpClient);

                        if (bContainScreenTcpClient)
                        {
                            ScreenTcpClient.Send(screenMsgModel.TcpData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("循环读取发送队列并发送数据到屏端出错：" + ex.Message);
                }
            }
        }

        public event EventHandler<PivasEventArgs<TCPMessage>> LabelNoReceived;
        /// <summary>
        /// 从队列中读取瓶签信息，然后发送到屏
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="buf"></param>
        /// <param name="frameMaxLen"></param>
        /// <returns></returns>
        void ScreenDataReceive()
        {
            TCPMessage labelNo = null;
            while (true)
            {
                try
                {
                    labelNo = null;
                    Thread.Sleep(5);//(一般5毫秒即可)
                    if (receiveQueue.Count > 0)
                    {
                        lock (receiveQueue)
                        {
                            labelNo = receiveQueue.Dequeue();
                        }
                    }
                    if (labelNo != null && LabelNoReceived != null)//事件是单线程处理
                    {
                        LabelNoReceived(null, new PivasEventArgs<TCPMessage>(labelNo));
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("循环从队列中读取瓶签号数据出错：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 检测所有screen的连接状态
        /// </summary>
        protected void StartscreenStatusCheck()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(60000);//60s=检测所有screen的连接周期
                    for (int i = 0; i < lstScreenTcpClient.Count; i++)
                    {
                        var item = lstScreenTcpClient[i];
                        if (!item.ConnectStatus && _ReConnnect)//断线了且需要重连
                        {
                            item.Stop(); //需要先关闭，再重连；关闭结果不重要
                            if (!item.Start())
                            {
                                InternalLogger.Log.Warn(String.Format("屏 {0}:{1}重连失败。会再次等待重连",
                                    item.ServerIp, item.ServerPort));
                            }
                            else
                            {
                                InternalLogger.Log.Warn(String.Format("屏 {0}:{1}重连成功",
                                      item.ServerIp, item.ServerPort));
                            }
                        }
                        Thread.Sleep(200);
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("检测所有screen的连接状态出错" + ex.Message);
                }
            }
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    try
                    {
                        foreach (var item in lstScreenTcpClient)
                        {
                            item.Dispose();
                        }
                        lstScreenTcpClient.Clear();
                        lstScreenTcpClient = null;
                    }
                    catch
                    {
                    }
                    this.Disconnected = null;
                    sendQueue.Clear();
                    sendQueue = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                try
                {
                    for (int i = 0; i < lstScreenTcpClient.Count; i++)
                    {
                        lstScreenTcpClient[i].Stop();
                    }
                    lstScreenTcpClient.Clear();
                    lstScreenTcpClient = null;
                }
                catch
                {
                }
                threadscreenSend = null;
                threadscreenReceive = null;
                threadscreenStatus_check = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~ScreenClientController()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
