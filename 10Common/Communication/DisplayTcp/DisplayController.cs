using PIVAsCommon;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Communication.DisplayTcp
{
    /// <summary>
    /// 舱内屏操作的控制类
    /// </summary>
    public class DisplayController : IDisposable
    {
        /// <summary>
        /// 保存所有display连接;开启在线检测线程中会变遍历
        /// 打开成功进行保存，后续不释放，只进行开闭操作
        /// </summary>
        private static List<DisplayTcpClient> lstDisplayTcpClient = new List<DisplayTcpClient>();
        /// <summary>
        /// 多个屏的信息都放入此队列
        /// </summary>
        private static Queue<TCPMessageEx> displayQueue = new Queue<TCPMessageEx>();
        private Thread threadDisplayStatus_check = null;//屏连接状态检测
        private Thread threadDisplaySend = null;//发送到屏数据的处理线程
        private AutoResetEvent moxaOpenAutoResetEvent = new AutoResetEvent(false);

        public DisplayController()
        {
            try
            {
                threadDisplaySend = new Thread(DisplayDataSend);
                threadDisplaySend.Start();
                threadDisplayStatus_check = new Thread(StartDisplayStatusCheck);
                threadDisplayStatus_check.Start();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("开启display检测线程出错" + ex.Message);
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
        public void DisplayClose(string remoteIP, int remotePort)
        {
            DisplayTcpClient displayTcpClient = null;
            bool bContainDisplayTcpClient = ContainTcpClient(remoteIP, remotePort, out displayTcpClient);
            if (bContainDisplayTcpClient)
            {
                displayTcpClient.Stop();
            }
        }

        /// <summary>
        /// 打开display连接;存在就直接打开，不存在就新建再打开
        /// </summary>
        /// <param name="remoteIP">ip</param>
        /// <param name="remotePort">端口号</param>
        /// <returns>返回端口号;为了兼容</returns>
        public bool DisplayOpen(string remoteIP, int remotePort)
        {
            DisplayTcpClient displayTcpClient = null;
            bool bContainDisplayTcpClient = ContainTcpClient(remoteIP, remotePort, out displayTcpClient);
            if (!bContainDisplayTcpClient)
            {
                displayTcpClient = new DisplayTcpClient(remoteIP, remotePort);
                displayTcpClient.Connected += DisplayTcpClient_Connected;
                displayTcpClient.Disconnected += DisplayTcpClient_Disconnected;
            }
            else
            {
                displayTcpClient.Stop();//每次打开前，先关闭一下
            }

            if (displayTcpClient.Start())
            {
                if (!bContainDisplayTcpClient)
                {
                    lstDisplayTcpClient.Add(displayTcpClient);
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
        /// 将待发送到屏的信息，放入队列
        /// 数据长度采用固定长度60，方便屏解析；不足部分用0x20（ASCII码的空格）填充
        /// </summary>
        /// <param name="e"></param>
        public void DisplayDataEnqueue(PivasEventArgs<TCPMessageEx> e)
        {
            try
            {
                //多个连接都会入队            
                lock (displayQueue)
                {
                    displayQueue.Enqueue(e.Value);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("Display数据入队出错:" + ex.Message);
            }
        }
        #endregion

        #region TCPClient暴露出来的事件，用于UI更新
        public event EventHandler<PivasEventArgs<TCPMessageEx>> Disconnected; 
        private void DisplayTcpClient_Disconnected(object sender, PivasEventArgs<TCPMessageEx> e)
        {
            if (Disconnected != null)
            {
                Disconnected(sender, e);
            }
        }

        public event EventHandler<PivasEventArgs<TCPMessageEx>> Connected;
        private void DisplayTcpClient_Connected(object sender, PivasEventArgs<TCPMessageEx> e)
        {
            moxaOpenAutoResetEvent.Set();
            if (Connected != null)
            {
                Connected(sender, e);
            }
        }
        #endregion

        #region Controller内部处理的方法和线程
        private bool ContainTcpClient(string remoteIP, int remotePort, out DisplayTcpClient displayTcpClient)
        {
            try
            {
                for (int i = 0; i < lstDisplayTcpClient.Count; i++)
                {
                    int port = lstDisplayTcpClient[i].ServerPort;
                    if (lstDisplayTcpClient[i].ServerIp.Trim().Equals(remoteIP) &&
                        port == remotePort)
                    {
                        displayTcpClient = lstDisplayTcpClient[i];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(String.Format("判断Display连接表中是否包含{0}:{1}出错.{2}",
                    remoteIP, remotePort, ex.Message));
            }

            displayTcpClient = null;
            return false;
        }

        /// <summary>
        /// 从队列中读取display数据，然后发送到屏
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="buf"></param>
        /// <param name="frameMaxLen"></param>
        /// <returns></returns>
        void DisplayDataSend()
        {
            TCPMessageEx displayMsgModel = null;
            while (true)
            {
                try
                {
                    displayMsgModel = null;
                    Thread.Sleep(6);//屏硬件要求，发完一帧要间隔6毫秒左右(一般5即可，这里做预留)
                    if (displayQueue.Count > 0)
                    {
                        lock (displayQueue)
                        {
                            displayMsgModel = displayQueue.Dequeue();
                        }
                    }

                    //从连接列表中找到tcp对象，调用send方法，发送
                    DisplayTcpClient displayTcpClient = null;
                    if (displayMsgModel != null)
                    {
                        bool bContainDisplayTcpClient = ContainTcpClient(displayMsgModel.ServerIp,
                        displayMsgModel.ServerPort, out displayTcpClient);

                        if (bContainDisplayTcpClient)
                        {
                            displayTcpClient.Send(displayMsgModel.TcpData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("循环读取display数据出错：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 检测所有display的连接状态
        /// </summary>
        protected void StartDisplayStatusCheck()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(60000);//60s=检测所有display的连接周期
                    for (int i = 0; i < lstDisplayTcpClient.Count; i++)
                    {
                        var item = lstDisplayTcpClient[i];
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
                    InternalLogger.Log.Error("检测所有display的连接状态出错" + ex.Message);
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
                        foreach (var item in lstDisplayTcpClient)
                        {
                            item.Dispose();
                        }
                        lstDisplayTcpClient.Clear();
                        lstDisplayTcpClient = null;
                    }
                    catch
                    {
                    }
                    this.Disconnected = null;
                    displayQueue.Clear();
                    displayQueue = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                try
                {
                    for (int i = 0; i < lstDisplayTcpClient.Count; i++)
                    {
                        lstDisplayTcpClient[i].Stop();
                    }
                    lstDisplayTcpClient.Clear();
                    lstDisplayTcpClient = null;
                }
                catch
                {
                }
                threadDisplaySend = null;
                threadDisplayStatus_check = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~DisplayController()
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
