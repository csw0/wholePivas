using PIVAsCommon;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Communication.MOXATcp
{
    /// <summary>
    /// Moxa操作的控制类
    /// </summary>
    public class MOXAController: IDisposable
    {
        /// <summary>
        /// 保存所有moxa连接;开启在线检测线程中会变遍历
        /// 打开成功进行保存，后续不释放，只进行开闭操作
        /// </summary>
        private static List<MOXATcpClient> lstMoxaTcpClient = new List<MOXATcpClient>();
        private AutoResetEvent moxaOpenAutoResetEvent = new AutoResetEvent(false);
        /// <summary>
        /// 多个连接都放入此队列
        /// </summary>
        private static Queue<TCPMessage> moxaQueue = new Queue<TCPMessage>();
        private Thread threadMoxaStatus_check = null;
        private Thread threadMoxaRead = null;
        //Moxa实际端口与数据库配置的不一样，以4000往上加
        private const int MOXA_BASEPORT = 4000;
        public int GetMOXA_BASEPORT
        {
            get { return MOXA_BASEPORT; }
        }
            
        public MOXAController()
        {
            try
            {
                threadMoxaRead = new Thread(MoxaRead);
                threadMoxaRead.Start();
                threadMoxaStatus_check = new Thread(StartMoxaStatusCheck);
                threadMoxaStatus_check.Start();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("开启moxa检测线程出错"+ ex.Message);
            }
        }

        /// <summary>
        /// 打开moxa连接;存在就直接打开，不存在就新建再打开
        /// </summary>
        /// <param name="remoteIP">此端口需要变化增加4000</param>
        /// <param name="remotePort"></param>
        /// <returns>返回端口号;为了兼容</returns>
        public int MoxaOpen(string remoteIP, int remotePort)
        {
            MOXATcpClient moxaTcpClient = null;
            bool bContainMoxaTcpClient = ContainTcpClient(remoteIP, remotePort, out moxaTcpClient);
            if (!bContainMoxaTcpClient)
            {
                moxaTcpClient = new MOXATcpClient(remoteIP, remotePort + MOXA_BASEPORT);
                moxaTcpClient.DataReceived += MoxaTcpClient_DataReceived;
                moxaTcpClient.Connected += MoxaTcpClient_Connected;
                moxaTcpClient.Disconnected += MoxaTcpClient_Disconnected;
            }
            else
            {
                moxaTcpClient.Stop();//每次打开前，先关闭一下
            }
            
            if (moxaTcpClient.Start())
            {
                if (!bContainMoxaTcpClient)
                {
                    lstMoxaTcpClient.Add(moxaTcpClient);
                }
                try
                {
                    bool moxaOpenSuccess = moxaOpenAutoResetEvent.WaitOne(600);//(ping*3)*3
                    if (moxaOpenSuccess)
                    {
                        //保存连接
                        return remotePort;
                    }
                }
                catch (Exception)
                {
                }
            }
            return -1;//打开失败返回-1
        }

        private void MoxaTcpClient_Disconnected(object sender, PivasEventArgs<TCPMessage> e)
        {
        }

        private void MoxaTcpClient_Connected(object sender, PivasEventArgs<TCPMessage> e)
        {
            moxaOpenAutoResetEvent.Set();
        }

        private bool ContainTcpClient(string remoteIP, int remotePort,out MOXATcpClient moxaTcpClient)
        {
            try
            {
                for (int i = 0; i < lstMoxaTcpClient.Count; i++)
                {
                    int port = lstMoxaTcpClient[i].ServerPort - MOXA_BASEPORT;
                    if (lstMoxaTcpClient[i].ServerIp.Trim().Equals(remoteIP) &&
                        port == remotePort)
                    {
                        moxaTcpClient = lstMoxaTcpClient[i];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error(String.Format("判断Moxa连接表中是否包含{0}:{1}出错.{2}",
                    remoteIP, remotePort,ex.Message));
            }

            moxaTcpClient = null;
            return false;
        }

        public event EventHandler<PivasEventArgs<MOXATcpClient>> Disconnected;
        public event EventHandler<PivasEventArgs<TCPMessage>> DataReceived;

        /// <summary>
        /// 关闭端口
        /// </summary>
        /// <param name="remoteIP"></param>
        /// <param name="remotePort"></param>
        /// <returns>返回端口号;为了兼容</returns>
        public int MoxaClose(string remoteIP, int remotePort)
        {
            MOXATcpClient moxaTcpClient = null;
            bool bContainMoxaTcpClient = ContainTcpClient(remoteIP, remotePort, out moxaTcpClient);
            if (bContainMoxaTcpClient)
            {
                moxaTcpClient.Stop();
                return 0;//成功返回0
            }
            return -1;//失败返回-1
        }

        /// <summary>
        /// 读取moxa数据
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="buf"></param>
        /// <param name="frameMaxLen"></param>
        /// <returns></returns>
        void MoxaRead()
        {
            try
            {
                TCPMessage moxaMsgModel = null;
                while (true)
                {
                    moxaMsgModel = null;
                    Thread.Sleep(5);//为减少不必要的开销，值暂定50毫秒
                    if (moxaQueue.Count > 0)
                    {
                        lock (moxaQueue)
                        {
                            moxaMsgModel = moxaQueue.Dequeue();
                            moxaMsgModel.ServerPort -= MOXA_BASEPORT;//在外部用的时候减去基准
                        }
                    }

                    if (moxaMsgModel != null && DataReceived != null)//事件是单线程处理
                        DataReceived(null, new PivasEventArgs<TCPMessage>(moxaMsgModel));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("循环读取moxa数据出错："+ ex.Message);
            }
        }

        private void MoxaTcpClient_DataReceived(object sender, PivasEventArgs<TCPMessage> e)
        {
            try
            {
                //多个连接都会入队            
                lock (moxaQueue)
                {
                    moxaQueue.Enqueue(e.Value);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("MOXA数据入队出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 检测所有moxa的连接状态
        /// </summary>
        protected void StartMoxaStatusCheck()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(60000);//60s=检测所有moxa的连接周期
                    for (int i = 0; i < lstMoxaTcpClient.Count; i++)
                    {
                        var item = lstMoxaTcpClient[i];
                        if (!item.ConnectStatus)//断线了
                        {
                            if (Disconnected != null)
                            {
                                Disconnected(this, new PivasEventArgs<MOXATcpClient>(item));
                            }
                        }
                        Thread.Sleep(200);
                    }
                }
                catch (System.Exception ex)
                {
                    InternalLogger.Log.Error("检测所有moxa的连接状态出错" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 临时接口，更好的方式是在硬件层面设置moxa的分隔符
        /// </summary>
        public void SetThridLabelLen(int tagLabelLen, string tagLabelMark)
        {
            MOXATcpClient.TagLabelLen = tagLabelLen;
            MOXATcpClient.TagLabelMark = tagLabelMark;
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
                    try
                    {
                        foreach (var item in lstMoxaTcpClient)
                        {
                            item.Dispose();
                        }
                        lstMoxaTcpClient.Clear();
                        lstMoxaTcpClient = null;
                    }
                    catch
                    {
                    }
                    this.Disconnected = null;
                    moxaQueue.Clear();
                    moxaQueue = null;
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                try
                {
                    for (int i = 0; i < lstMoxaTcpClient.Count; i++)
                    {
                        lstMoxaTcpClient[i].Stop();
                    }
                    lstMoxaTcpClient.Clear();
                    lstMoxaTcpClient = null;
                }
                catch 
                {
                }
                threadMoxaRead = null;
                threadMoxaStatus_check = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~MOXAController()
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
