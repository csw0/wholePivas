using Amib.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestSmartThreadPool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //for (int i = 1; i < 10; i++)
            //{
            //    lst.Add(new model(i,"test"));
            //}
            smartThreadPool.Start();
        }

        SmartThreadPool smartThreadPool = new SmartThreadPool();
        List<model> lst = new List<model>();
        public void test()
        {
            for (int i = 0; i < lst.Count; i++)
            {
                model m = lst[i];
                handler("befer"+m.GetInfo()+"\r\n");
                lst.RemoveAt(i);
                handler("after" + m.GetInfo() + "\r\n");
            }
        }
        void handler(string s)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new System.Action(()=> {
                    handler1(s);
                }));               
            }
            else
            {
                handler1(s);
            }
        }

        void handler1(string s)
        {
            richTextBox1.Text += (s + "\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test();
        }

        System.Threading.AutoResetEvent autoEvent;
        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(test2));
            t.Start();
        }
        void DoWork()
        {
            handler(" Thread 1");
            autoEvent.WaitOne();
            handler("Thread 2");
        }

        void test2()
        {
            autoEvent = new System.Threading.AutoResetEvent(true);

            handler("main thread 1");
            System.Threading.Thread.Sleep(5000);
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(DoWork));
            t.Start();
            System.Threading.Thread.Sleep(5000);

            handler("main thread 2");
            System.Threading.Thread.Sleep(5000);

            handler("main thread 3");
            System.Threading.Thread.Sleep(5000);
            autoEvent.Set();
            handler("main thread 4");
            
            Console.ReadLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                smartThreadPool.QueueWorkItem(() =>
                {
                    //模拟计算较长时间
                    Thread.Sleep(1000);
                    handler(DateTime.Now.ToString());
                });
            }
        }
    }
}
