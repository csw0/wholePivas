using System;
using System.Drawing;
using System.Windows.Forms;
using PIVAsCommon;

namespace CommonUI.Controls
{
    //定义枚举
    public partial class Keyboard : Form
    {
        //小键盘隐藏事件,为了保留原设计；其实接口可以直接暴露，不必用事件
        private static event EventHandler HideForm;
        //键盘值接收事件
        public event EventHandler<PivasEventArgs<String>> KeyDataReceived;
        //键盘删除键接收事件
        public event EventHandler<PivasEventArgs<String>> KeyDeleteReceived;
        private Point mouse_offset;//实现窗体拖动

        public Keyboard()
        {
            InitializeComponent();          
        }

        private void keyboard_Load(object sender, EventArgs e)
        {
            HideForm += Keyboard_HideForm;
        }

        /// <summary>
        /// 隐藏小键盘时，触发的处理；主要供外部使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keyboard_HideForm(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("隐藏小键盘时，触发处理操作出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 调用关闭窗体事件
        /// </summary>
        public static void CloseFrom()
        {
            try
            {
                if (HideForm != null)
                {
                    HideForm(null,null);
                }
            }
            catch { }
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = (Image)CommonUI.Properties.Resources.ResourceManager.GetObject("关闭");
        }

        private void Panel_Close_MouseMove(object sender, MouseEventArgs e)
        {
            Panel_Close.BackgroundImage = (Image)CommonUI.Properties.Resources.ResourceManager.GetObject("关闭按下时");
        }

        #region 软键盘按键
        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击小键盘时，触发接收事件
        /// </summary>
        /// <param name="keyValue"></param>
        private void SetKeyData(string keyValue)
        {
            try
            {
                if (KeyDataReceived != null)
                {
                    KeyDataReceived(this, new PivasEventArgs<String>(keyValue));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("点击小键盘时，触发接收事件出错" + ex.Message);
            }
        }

        private void txt_1_Click(object sender, EventArgs e)
        {
            SetKeyData("1");
        }

        private void txt_2_Click(object sender, EventArgs e)
        {
            SetKeyData("2");
        }

        private void txt_3_Click(object sender, EventArgs e)
        {
            SetKeyData("3");
        }

        private void txt_4_Click(object sender, EventArgs e)
        {
            SetKeyData("4");
        }

        private void txt_5_Click(object sender, EventArgs e)
        {
            SetKeyData("5");
        }

        private void txt_6_Click(object sender, EventArgs e)
        {
            SetKeyData("6");
        }

        private void txt_7_Click(object sender, EventArgs e)
        {
            SetKeyData("7");
        }

        private void txt_8_Click(object sender, EventArgs e)
        {
            SetKeyData("8");
        }

        private void txt_9_Click(object sender, EventArgs e)
        {
            SetKeyData("9");
        }

        private void txt_0_Click(object sender, EventArgs e)
        {
            SetKeyData("0");
        }

        private void txt_q_Click(object sender, EventArgs e)
        {
            SetKeyData("q");
        }

        private void txt_w_Click(object sender, EventArgs e)
        {
            SetKeyData("w");
        }

        private void txt_e_Click(object sender, EventArgs e)
        {
            SetKeyData("e");
        }

        private void txt_r_Click(object sender, EventArgs e)
        {
            SetKeyData("r");
        }

        private void txt_t_Click(object sender, EventArgs e)
        {
            SetKeyData("t");
        }

        private void txt_y_Click(object sender, EventArgs e)
        {
            SetKeyData("y");
        }

        private void txt_u_Click(object sender, EventArgs e)
        {
            SetKeyData("u");
        }

        private void txt_i_Click(object sender, EventArgs e)
        {
            SetKeyData("i");
        }

        private void txt_o_Click(object sender, EventArgs e)
        {
            SetKeyData("o");
        }

        private void txt_a_Click(object sender, EventArgs e)
        {
            SetKeyData("a");
        }

        private void txt_s_Click(object sender, EventArgs e)
        {
            SetKeyData("s");
        }

        private void txt_d_Click(object sender, EventArgs e)
        {
            SetKeyData("d");
        }

        private void txt_f_Click(object sender, EventArgs e)
        {
            SetKeyData("f");
        }

        private void txt_g_Click(object sender, EventArgs e)
        {
            SetKeyData("g");
        }

        private void txt_h_Click(object sender, EventArgs e)
        {
            SetKeyData("h");
        }

        private void txt_j_Click(object sender, EventArgs e)
        {
            SetKeyData("j");
        }

        private void txt_k_Click(object sender, EventArgs e)
        {
            SetKeyData("k");
        }

        private void txt_l_Click(object sender, EventArgs e)
        {
            SetKeyData("l");
        }

        private void txt_p_Click(object sender, EventArgs e)
        {
            SetKeyData("p");
        }

        private void txt_z_Click(object sender, EventArgs e)
        {
            SetKeyData("z");
        }

        private void x_Click(object sender, EventArgs e)
        {
            SetKeyData("x");
        }

        private void txt_c_Click(object sender, EventArgs e)
        {
            SetKeyData("c");
        }

        private void txt_v_Click(object sender, EventArgs e)
        {
            SetKeyData("v");
        }

        private void txt_b_Click(object sender, EventArgs e)
        {
            SetKeyData("b");
        }

        private void txt_n_Click(object sender, EventArgs e)
        {
            SetKeyData("n");
        }

        private void txt_m_Click(object sender, EventArgs e)
        {
            SetKeyData("m");
        }

        private void txt_Enter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击键盘删除键时，触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_清空_Click(object sender, EventArgs e)
        {
            try
            {
                if (KeyDeleteReceived != null)
                {
                    KeyDeleteReceived(this, new PivasEventArgs<string>("DELETE"));
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("点击键盘删除键时，触发事件出错：" + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 主窗体里面第一级控件里面的子控件；属于主窗体二级控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mouse_Down(object sender, MouseEventArgs e)
        {
            Point keyLocation =((Control)sender).Location;
            mouse_offset = new Point(-(keyLocation.X + e.X), -(keyLocation.Y + e.Y));
        }

        /// <summary>
        /// 主窗体里面第一级控件
        /// </summary>
        private void Mouse_Down_First(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                Location = mousePos;
            }
        }
    }
}
