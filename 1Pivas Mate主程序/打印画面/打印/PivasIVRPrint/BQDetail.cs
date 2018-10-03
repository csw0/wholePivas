using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace PivasIVRPrint
{
    public partial class BQDetail : UserControl
    {
        private UserControlPrint piv;
        private static DataSet ds;
        private point selectpot;
        private static Dictionary<point, List<string>> dic = new Dictionary<point, List<string>>();
        private static List<string> als = new List<string>();
        private int H;
        private int value;
        private Thread th;
        public BQDetail(UserControlPrint p, DataSet dss)
        {
            piv = p;
            ds = dss;
            InitializeComponent();
        }
        private void BQDetail_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width + 20;
            foreach (Control c in panel1.Controls)
            {
                c.Width = this.Width;
            }
        }
        private void BQDetail_Load(object sender, EventArgs e)
        {
            piv.prints = new Dictionary<string, bool>();
            List<string> al;
            point pot;
            int i = ds.Tables[0].Rows.Count;
            int j;
            H = value = 0;
            if (i > 100)
            {
                int t;
                int k;
                for (j = 0; j < (i % 10); j++)
                {
                    al = new List<string>();
                    for (t = j * (i / 10 + 1); t < (j + 1) * (i / 10 + 1); t++)
                    {
                        al.Add(ds.Tables[0].Rows[t]["瓶签号"].ToString());
                        piv.prints.Add(ds.Tables[0].Rows[t]["瓶签号"].ToString(), piv.checkBox2.Checked);
                    }
                    pot = new point(this, j * (i / 10 + 1));
                    flowLayoutPanel1.Controls.Add(pot);
                    dic.Add(pot, al);
                    if (flowLayoutPanel1.Controls.Count == 1)
                        pot.point_Click(sender, e);
                }
                for (k = 0; k < 10 - j; k++)
                {
                    al = new List<string>();
                    for (t = (i % 10) * (i / 10 + 1) + k * (i / 10); t < (i % 10) * (i / 10 + 1) + (k + 1) * (i / 10); t++)
                    {
                        al.Add(ds.Tables[0].Rows[t]["瓶签号"].ToString());
                        piv.prints.Add(ds.Tables[0].Rows[t]["瓶签号"].ToString(), piv.checkBox2.Checked);
                    }
                    pot = new point(this, (i % 10) * (i / 10 + 1) + k * (i / 10));
                    flowLayoutPanel1.Controls.Add(pot);
                    dic.Add(pot, al);
                    if (flowLayoutPanel1.Controls.Count == 1)
                        pot.point_Click(sender, e);
                }
            }
            else
            {
                al = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    al.Add(dr["瓶签号"].ToString());
                    piv.prints.Add(dr["瓶签号"].ToString(), piv.checkBox2.Checked);
                }
                als = al;
                al.AsReadOnly();
                flowLayoutPanel1.Visible = false;
            }
            timer1.Enabled = true;
        }


        private void run()
        {
            lock (this)
            {
                int Y = H - panel1.VerticalScroll.Value;
                if (!panel1.HasChildren || (Y - panel1.Height) < 300)
                {
                    if (als.Count != 0)
                    {
                        BQlabel bq = new BQlabel(piv, als[0]);
                        bq.Width = this.Width;
                        bq.Location = new Point(0, Y);
                        panel1.Controls.Add(bq);
                        H = H + bq.Height;
                        als.RemoveAt(0);
                    }
                    else if ((Y - panel1.Height) == 0)
                    {
                        int ind = flowLayoutPanel1.Controls.IndexOf(selectpot) + 1;
                        if (ind < flowLayoutPanel1.Controls.Count)
                        {
                            ((point)flowLayoutPanel1.Controls[ind]).point_Click(null, null);
                        }
                    }
                }
                else if (panel1.AutoScrollPosition.Y == 0 && H > panel1.Height)
                {

                    if (value < -360)
                    {
                        value = 0;
                        int ind = flowLayoutPanel1.Controls.IndexOf(selectpot) - 1;
                        if (ind >= 0)
                        {
                            ((point)flowLayoutPanel1.Controls[ind]).point_Click(null, null);
                        }
                    }
                    else if (th == null || !th.IsAlive)
                    {
                        th = new Thread(remv);
                        th.IsBackground = true;
                        th.Start();
                    }
                }
            }
        }
        public void resrun(point pi, int cout)
        {
            selectpot = pi;
            als = dic[pi].GetRange(0, dic[pi].Count);
            H = 0;
            panel1.Controls.Clear();
            if (!panel1.Focused)
                panel1.Focus();
            if (th != null)
                th.Abort();
        }
        private void remv()
        {
            panel1.Focus();
            panel1.MouseWheel += new MouseEventHandler(panel1_MouseWheel);
            while (true)
            {
                continue;
            }
        }
        void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            value = (panel1.AutoScrollPosition.Y == 0 && H > panel1.Height) ? (e.Delta > 0 ? value - e.Delta : 0) : 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            run();
        }
    }
}
