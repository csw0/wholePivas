using CommonUI.Controls;
using CommonUI.Froms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestTimer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 非ui线程更新ui不一定会出错；但不建议，后期出现问题将非常难查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //this.timer1.Enabled = true;
            Thread tt = new Thread(test1);
            tt.Start();

            new Thread(() =>
            {
                for (int i = 0; i < 700; i++)
                {
                    listView1.Items.Insert(0, i.ToString() + ",选好");
                    Thread.Sleep(30);
                }
                
            }).Start();
            
        }

        private AutoResetEvent autoResetEvent1 = new AutoResetEvent(false);
        void test1()
        {
            Thread.Sleep(5000);
            autoResetEvent1.Set();


            for (int i = 0; i < 10; i++)
            {
                autoResetEvent1.WaitOne();
                Thread ww = new Thread(()=> 
                {
                    Thread.Sleep(5000);
                    label1.Text = DateTime.Now.ToString("hh:mm:ss");
                    autoResetEvent1.Set();
                });
                ww.IsBackground = true;
                ww.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            //label1.Invoke(new Action(() =>
            //{
                label1.Text = DateTime.Now.ToString("hh:mm:ss");
            //}));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Keyboard form = new Keyboard();
            form.KeyDataReceived += Form_KeyDataReceived;
            form.KeyDeleteReceived += Form_KeyDeleteReceived; ;
            form.Show();
        }

        private void Form_KeyDeleteReceived(object sender, global::PIVAsCommon.PivasEventArgs<string> e)
        {
            label1.Text = e.Value;
        }

        private void Form_KeyDataReceived(object sender, global::PIVAsCommon.PivasEventArgs<string> e)
        {
            label1.Text = e.Value;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Keyboard.CloseFrom();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DBSet form = new DBSet(DatabaseType.PivasDB);
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(f);
            t.IsBackground = true;
            t.Start();
        }

        void f()
        {
            while (true)
            {
                Thread t2 = new Thread(() =>
                {
                    Thread.Sleep(1000);
                    label1.Invoke(new Action(() =>
                    {
                        label1.Text = DateTime.Now.ToString("hh:mm:ss");
                    }));
                });
                t2.IsBackground = true;
                t2.Start();
                Thread.Sleep(5000);
            }
        }
    }
}
