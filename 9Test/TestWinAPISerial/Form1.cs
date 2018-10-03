using Communication.PLCCom;
using PIVAsCommon;
using System;
using System.Threading;
using System.Windows.Forms;
using UseWebServer;

namespace TestWinAPISerial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static WinAPI_ServialPort WinApi_PLC = new WinAPI_ServialPort();
        private static BL_PLCSerialPort bl_plc = new BL_PLCSerialPort();
        private void button1_Click(object sender, EventArgs e)
        {
            if (WinApi_PLC.Opened)
            {
                WinApi_PLC.Close();
            }

            WinApi_PLC.PortNum = textBox2.Text.Trim();
            WinApi_PLC.BaudRate = 9600;
            WinApi_PLC.Parity = 0;
            WinApi_PLC.ByteSize = 8;
            WinApi_PLC.BytesToWrite = 6;
            WinApi_PLC.BytesToRead = 24;

            WinApi_PLC.Open();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InternalLogger.Log.Info("窗体加载成功");
            Thread t = new Thread(SendData);
            t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WinApi_PLC.SendDataAllOut(1,0xff,0xff);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bSendAPI = !bSendAPI;
        }
        private bool bSendAPI = false;
        private bool bSendNET = false;
        void SendData()
        {
            while (true)
            {
                if (bSendAPI)
                {
                    WinApi_PLC.SendDataAllOut(2, 0xff, 0xff);
                }
                if (bSendNET)
                {
                    bl_plc.SendDataAllOut(3, 0xff, 0xff);
                }
                Thread.Sleep(Int32.Parse(textBox1.Text.Trim()));
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bSendNET = !bSendNET;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bl_plc.CloseCom();
            bl_plc.OpenCom(textBox2.Text.Trim(), 9600);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            TestService t = new TestService();
            button6.Text =  t.testHelloW();
        }
    }
}
