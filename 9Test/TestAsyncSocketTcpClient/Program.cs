using PIVAsCommon;
using PIVAsCommon.Sockets;
using System;
using System.Text;
using System.Threading;

namespace TestAsyncSocketTcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //AsyncSocketTcpClient tcpClient = new AsyncSocketTcpClient("192.168.23.200", 9999);
            //tcpClient.DataReceived += tcpClient_DataReceived;
            //tcpClient.Connected += tcpClient_Connected;
            //tcpClient.DataSent += tcpClient_DataSent;
            //tcpClient.Disconnected += tcpClient_Disconnected;
            //tcpClient.ErrorOccurred += tcpClient_ErrorOccurred;
            //tcpClient.Started += tcpClient_Started;
            //tcpClient.Stopped += tcpClient_Stopped;
            //tcpClient.Start(false);
            //Thread.Sleep(1000);
            //tcpClient.Send(Encoding.Default.GetBytes(DateTime.Now.ToString()));
            //Thread.Sleep(600000);
            string cmd = Console.ReadLine();
            byte[] bb = new byte[4];
            try
            {
                byte[] b = new byte[] { 0x31 , 0x30, 0x6D, 0x6C, 0x3A, 0x30, 0x2E, 0x34, 0x67, 0x2A, 0x36, 0xD6, 0xA7 };
                string dd =Encoding.GetEncoding(20936).GetString(b);
                Console.WriteLine(dd);
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
           
            Console.WriteLine("程序关闭");
            string cm2d = Console.ReadLine();
        }

        static void tcpClient_Stopped(object sender, EventArgs e)
        {
            Console.WriteLine("tcpClient_Started");
        }

        static void tcpClient_Started(object sender, EventArgs e)
        {
            Console.WriteLine("tcpClient_Started");
        }

        static void tcpClient_ErrorOccurred(object sender, AsyncSocketErrorEventArgs e)
        {
            Console.WriteLine("tcpClient_ErrorOccurred");
        }

        static void tcpClient_Disconnected(object sender, AsyncSocketSessionEventArgs e)
        {
            Console.WriteLine("tcpClient_Disconnected");
        }

        static void tcpClient_DataSent(object sender, AsyncSocketSessionEventArgs e)
        {
            Console.WriteLine("tcpClient_DataSent");
        }

        static void tcpClient_Connected(object sender, AsyncSocketSessionEventArgs e)
        {
            Console.WriteLine("tcpClient_Connected");
        }

        static void tcpClient_DataReceived(object sender, AsyncSocketSessionEventArgs e)
        {
            try
            {
                Console.WriteLine(Encoding.Default.GetString(e.DataTransferred));
                string data = string.Format("Size:{},Data:{1},Local:{2},Remote:{3}"
                              , e.BytesTransferred
                              , Encoding.Default.GetString(e.DataTransferred)
                              , e.SessionIPEndPoint.ToString()
                              , ""
                              );
                InternalLogger.Log.Info(data);
            }
            catch (Exception)
            {
            }
        }
    }
}
