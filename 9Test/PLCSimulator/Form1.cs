using Communication.PLCCom;
using PIVAsCommon;
using PIVAsCommon.Extensions;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace PLCSimulator
{
    public partial class Form1 : Form
    {
        private BL_PLCSerialPort communicate = new BL_PLCSerialPort();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //10个PLC；每个PLC16个输出口（1~16）,对应button编号
            communicate.eventDataReceive += Communicate_eventDataReceive1; ;
        }

        private void Communicate_eventDataReceive1(object sender, PivasEventArgs<byte[]> e)
        {
            //已保证是6个字节
            try
            {
                byte plcNo = e.Value[0];
                byte[] out_1_16 = new byte[2];
                out_1_16[0] =  e.Value[2];
                out_1_16[1] = e.Value[3];
                InternalLogger.Log.Error(string.Format("PLCNo={0},send1={1},send2={2}", plcNo, e.Value[2], e.Value[3]));
                GetLights(plcNo, out_1_16);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("处理接收到控制指令出错："+ex.Message);
            }
        }

        private void GetLights(byte plcNo, byte[] out_1_16)
        {
            BitArray bitArray = new BitArray(out_1_16);
            for (int i = 0; i < bitArray.Count; i++)
            {
                Color c = Color.Black;//默认黑
                if (bitArray.Get(i))//1
                    c = Color.Red;
                else c = Color.Black;

                ControlLight(GetControl(plcNo, i+1), c);
            }
        }
        private Control GetControl(byte plcNo,int linghtNo)
        {
            string btnName = "button" + ((plcNo - 1) * 16 + linghtNo).ToString();
            return ((Button)(this.Controls.Find(btnName, false)[0]));
        }
        /// <summary>
        /// 控制button背景色是什么颜色
        /// </summary>
        /// <param name="control">控件名</param>
        /// <param name="color">颜色</param>
        private void ControlLight(Control control, Color color)
        {
            control.SafeAction(() =>
            {
                control.BackColor = color;
            });
        }

        private void button161_Click(object sender, EventArgs e)
        {
            communicate.CloseCom();
            communicate.OpenCom(textBox2.Text.Trim(), 9600);
        }
    }
}
