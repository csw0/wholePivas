using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasRevPre
{
    public partial class 控件 : UserControl
    {
        bool code;
        int nowid;
        public 控件()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="all"></param>
        /// <param name="now"></param>
        /// <param name="each"></param>
        /// <param name="most"></param>
        public void show(int all, int now, int each,int most) 
        {   
              if(all>each)
              {
                try 
                { 
                   if (each > (all / most)) 
                  {
                      most = all/each;
                  }
                
                  nowid = now / (all / most);
                }
                  catch{}

                for (int i =0; i < most; i++)
                {
                    if (i ==nowid)
                    {
                        code = true;
                    }
                    else
                    {
                        code = false;
                    }
                    dian dian = new dian(code);
                    dian.Name = i.ToString();
                    dian.Location = new Point(i * 18, 0);
                    panel1.Controls.Add(dian);
                }
                this.Width = most * 18;
                }
            }

        public void show1(int all, int now, int each)
        {
            int A = 2;
            if (all > each)
            {
                try
                {
                    A = all / each;
                    nowid = now / each;
                }
                catch { }

                for (int i = 0; i < A; i++)
                {
                    if (i == nowid)
                    {
                        code = true;
                    }
                    else
                    {
                        code = false;
                    }
                    dian dian = new dian(code);
                    dian.Name = i.ToString();
                    dian.Location = new Point(i * 18, 0);
                    panel1.Controls.Add(dian);
                }
                this.Width = A * 18;
            }
        }
    }
    
}
