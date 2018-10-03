using System;
using System.Windows.Forms;
using System.Threading;

namespace ConsumablesStatic
{
    public partial class WaitForm : Form
    {
        private delegate void SetPos(int ipos);
        private int i = 0;
        public WaitForm()
        {
            InitializeComponent();
        }
        private void SetTextMessage(int ipos)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos});
            }
            else
            {
                //this.label1.Text = label1.Text + "：" + ipos.ToString() + "/100";
                this.progressBar1.Value = Convert.ToInt32(ipos);
             }
         }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = i++ < 13 ? label1.Text + "." : "正在查询和计算,请稍后";
            //SetTextMessage(100 * i / 500);
            //int i = 100;
            //progressBar1.Value = progressBar1.Value + 1;
            //i = 100 - progressBar1.Value;
            ////textBox1.Text = i.ToString();
            //threadchange();
            //if (i == 0)
            //{
            //    timer1.Enabled = false;
            //}
        }

        private void change()
        {
            for (int i = 1; i <= progressBar1.Value; i++)
            {
                progressBar1.PerformStep();
                Application.DoEvents(); //让系统在百忙之中来响应其他事件
            }
        }
        public void threadchange() //通过委托处理，MSDN上又很详细用法的说明
        {
            MethodInvoker In = new MethodInvoker(change);
            this.BeginInvoke(In);
        }

        private void WaitForm_Load(object sender, EventArgs e)
        {
            //Thread cha = new Thread(new ThreadStart(threadchange));
            //cha.Start();
        }
    }
}
