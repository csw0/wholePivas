using TestAsyncSocketTcpServer.Sockets;
using System;
using System.Text;
using System.Threading;

namespace TestAsyncSocketTcpServer
{
    class Program
    {
        private static AsyncSocketTcpServer tcpServer;
        private static int sessionId = -1;//标记客户端的sessionId
        static  bool exit = false;//标记退出while

        static void Main(string[] args)
        {
            tcpServer = new AsyncSocketTcpServer(5003);
            tcpServer.DataReceived += tcpServer_DataReceived;
            tcpServer.Connected += tcpServer_Connected;
            tcpServer.DataSent += tcpServer_DataSent;
            tcpServer.Disconnected += tcpServer_Disconnected;
            tcpServer.ErrorOccurred += tcpServer_ErrorOccurred;
            tcpServer.Started += tcpServer_Started;
            tcpServer.Stopped += tcpServer_Stopped;
            tcpServer.Start(false);

            Console.WriteLine("请在30秒内，开启tcpclient测试端，并进行连接");
            (new Thread(() => 
            {
                Thread.Sleep(30000);
                exit = true;
            })).Start();

            do
            {
                Thread.Sleep(700);
                //如果有客户端连接成功，就向客户端发送一个瓶签号
                if (sessionId != -1)
                {
                    tcpServer.Send(sessionId, Encoding.Default.GetBytes("20181028100111"));
                }
            } while (sessionId == -1 && !exit);

            if (sessionId != -1 || exit)
            {
                if (exit)
                {
                    Console.WriteLine("没有任何客户端进行连接，输入输入任意键，关闭程序");
                }
                if (sessionId != -1)
                {
                    Console.WriteLine("服务端已向客户端发送瓶签号，请验证客户端是否收到。输入任意键，关闭程序");
                }
            }
            Console.ReadLine();
        }

        static void tcpServer_Stopped(object sender, EventArgs e)
        {
            Console.WriteLine("tcpServer_Started");
        }

        static void tcpServer_Started(object sender, EventArgs e)
        {
            Console.WriteLine("tcpServer_Started");
        }

        static void tcpServer_ErrorOccurred(object sender, AsyncSocketErrorEventArgs e)
        {
            Console.WriteLine("tcpServer_ErrorOccurred");
        }

        static void tcpServer_Disconnected(object sender, AsyncSocketSessionEventArgs e)
        {
            Console.WriteLine("tcpServer_Disconnected");
        }

        static void tcpServer_DataSent(object sender, AsyncSocketSessionEventArgs e)
        {
            Console.WriteLine("tcpServer_DataSent");
        }

        static void tcpServer_Connected(object sender, AsyncSocketSessionEventArgs e)
        {
            Console.WriteLine("tcpServer_Connected");
            GetClientInfo();//成功后，获取客户端的信息
        }

        static void tcpServer_DataReceived(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                //接收到的数据是e.DataTransferred，转化字符串显示
                string result = Encoding.Default.GetString(e.DataTransferred);
                if (!string.IsNullOrEmpty(result))
                {
                    if (Int32.Parse(result) == 0)
                    {
                        Console.WriteLine("计费失败");
                    }
                    else
                    {
                        Console.WriteLine("计费成功");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 获取已连接成功的客户端信息
        /// </summary>
        static void GetClientInfo()
        {
            try
            {
                if (tcpServer != null)
                {
                    foreach (var item in tcpServer.GetSessionCollection().Keys)
                    {
                        sessionId = item;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
