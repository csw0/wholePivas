using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MccScreen
{
    /// <summary>
    /// win10系统屏，控制层
    /// </summary>
    public class ScreenController : IDisposable
    {
        private ScreenTcpServer screenTcpServer = null;
        private Thread checkServerStatus = null;
        private int failCount = 0;//检测失败次数
        private int FAIL_COUNT = 0;//检测失败次数通知阈值
        private DB_Help db = new DB_Help();//用于读取配置文件

        public ScreenController(ScreenTcpServer screenTcpServer)
        {
            this.screenTcpServer = screenTcpServer;

            FAIL_COUNT = Int32.Parse(db.IniReadValuePivas("SCREEN", "FailCount").Trim());

            checkServerStatus = new Thread(CheckServerStatus);
            checkServerStatus.IsBackground = true;
            checkServerStatus.Start();
        }

        public event EventHandler ListenStarted;//监控已开启
        public event EventHandler ListenStoped;//监听已停止
        private void CheckServerStatus()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(60000);//60s=检测周期
                    if (screenTcpServer != null)
                    {
                        if (screenTcpServer.IsListening)
                        {
                            if (ListenStarted != null && failCount > 0)
                                ListenStarted(null, null);//从失败中恢复时，触发通知事件
                            Interlocked.Add(ref failCount, -failCount);//置零，保证下次不再触发事件
                        }
                        else
                        {
                            Interlocked.Add(ref failCount, 1);//加1

                            if (ListenStoped != null && failCount == FAIL_COUNT)
                                ListenStoped(null, null);//只有等于3时，触发一次

                            screenTcpServer.Stop();
                            Thread.Sleep(100);//稍微暂停一下
                            screenTcpServer.Start();
                        }
                    }
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("检测屏服务监听状态出错" + ex.Message);
                }
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
                checkServerStatus = null;
                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~ScreenController() {
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
