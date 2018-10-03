using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CSW.Common;

namespace PivasMcc
{
    class NewMoxa
    {
        public List<Socket> listSocket=new List<Socket> ();
        public List<string[]> Port=new List<string[]> ();
        public List<string> stringdata = new List<string>();
        public Socket newclient;
        public bool Connected;
        public List<Thread> myThread=new List<Thread> ();
        public delegate void MyInvoke(string str);

        public int SocketOpen(string ip, int port)
        {
            byte[] data = new byte[1024];

            newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string ipadd = ip;
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), port);
            try
            {
                newclient.Connect(ie);
                Connected = true;
                listSocket.Add(newclient);
                string[] s = { ip, port.ToString() };
                Port.Add(s);
                stringdata.Add("");
            }
            catch (SocketException e)
            {
                InternalLogger.Log.Error("SocketOpen FUNC 出错" + e.Message);
                return -1;
            }
            Thread th= new Thread(ReceiveMsg);
            myThread.Add (th);
            object[] obj=new object[2]{ip,port.ToString()};
            th.Start(obj);
            return port;
        }

        public void  ReceiveMsg(object obj)
        {
            while (true)
            {
                object[] NewObj = obj as Object[];
                string ip = (string)NewObj[0];
                string port = (string)NewObj[1];
                for (int n = 0; n < Port.Count; n++)
                {
                    if (Port[n][0].ToString() == ip && Port[n][1].ToString() == port)
                    {
                        try
                        {
                            byte[] data = new byte[1024];
                            int recv = listSocket[n].Receive(data);
                            stringdata[n] += (Encoding.UTF8.GetString(data, 0, recv));
                        }
                        catch (Exception e)
                        {
                            InternalLogger.Log.Error("ReceiveMsg function:" + e.Message);
                        }
                    }
                }
            }
        }

        public int SocketRead(string ip,int port, StringBuilder buf, int i)
        {
            for (int n = 0; n < Port.Count; n++)
            {
                if (Port[n][0] == ip && Port[n][1] == port.ToString())
                {
                    try
                    {
                        i = stringdata[n].Length > i ? i : stringdata[n].Length;
                        buf.Append(stringdata[n].Substring(0, i));
                        stringdata[n] = stringdata[n].Remove(0, i);
                        return buf.Length;
                    }
                    catch (Exception ex)
                    {
                        InternalLogger.Log.Error("SocketRead Function 出错:" + ex.Message);
                    }
                }
            }
            return 0;
        }

        public int SocketClose(string ip,int port)
        {
            for (int i = 0; i < Port.Count; i++)
            {
                if (Port[i][0] == ip && Port[i][1] == port.ToString())
                {
                    myThread[i].Abort();
                    myThread.Remove(myThread[i]);
                    listSocket[i].Close();
                    listSocket.Remove(listSocket[i]);
                    Port.Remove(Port[i]);
                    stringdata.Remove(stringdata[i]);
                    return 0;
                }
            }
            return port;
        }
    }
}
