using Communication.WindowsScreen;
using PIVAsCommon;
using PIVAsCommon.BarcodeScanner;
using PIVAsCommon.Extensions;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MccScreen
{
    public partial class MainFrm : Form
    {
        #region 扫描枪键盘钩子
        private ScanerHook listener = new ScanerHook();
        private ScreenController controller = null;
        private ScreenTcpServer tcpServer = null;
        private DB_Help db = new DB_Help();//用于读取配置文件

        public MainFrm()
        {
            InitializeComponent();
            listener.ScanerEvent += Listener_ScanerEvent;
        }

        private void Listener_ScanerEvent(ScanerCodes codes)
        {
            this.SafeAction(() =>
            {
                tbLabel.Text = codes.Result;
            });

            new Thread(() => 
            {
                //发送瓶签号给客户端去处理
                if (tcpServer != null)
                {
                    tcpServer.Send(codes.Result.Trim());
                }
                else
                {
                    string str = "TCP链路断开，请重启";
                    InternalLogger.Log.Warn(str);
                    UpdateLabelResult(str);
                }
            }).Start();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            try
            {
                HideShow(true);
                listener.Start();

                int port = Int32.Parse(db.IniReadValuePivas("SCREEN", "ServerPort").Trim());
                tcpServer = new ScreenTcpServer(port);
                tcpServer.Connected += TcpServer_Connected;
                tcpServer.Disconnected += TcpServer_Disconnected;
                tcpServer.ErrorOccurred += TcpServer_ErrorOccurred;
                tcpServer.EventLogin += TcpServer_EventLogin;
                tcpServer.EventScreenInfo += TcpServer_EventScreenInfo;
                tcpServer.Started += TcpServer_Started;
                tcpServer.Stopped += TcpServer_Stopped;
                tcpServer.Start();

                controller = new ScreenController(tcpServer);
                controller.ListenStarted += TcpServer_Started;
                controller.ListenStoped += TcpServer_Stopped;

                InitListview();
                ShowTipCenter();
                this.lblLoginResult.Invalidate(true);
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("主窗体加载失败"+ex.Message);
            }
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listener.Stop();
        }
        #endregion 扫描枪键盘钩子

        #region 更新UI
        /// <summary>
        /// 更新结果行
        /// </summary>
        /// <param name="text"></param>
        private void UpdateLabelResult(string text)
        {
            this.SafeAction(() =>
            {
                if (panelLogin.Visible)
                {
                    lblLoginResult.Text = text;
                }
                else
                {
                    lblResult.Text = text;
                }
            });
        }

        /// <summary>
        /// 更新药品行
        /// </summary>
        /// <param name="list"></param>
        private void UpdateListviewDrug(List<MsgDrugRowInfo> list)
        {
            this.listViewDrug.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            string emptyEnd = " ".PadRight(5);
            foreach (var drugRow in list)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = drugRow.DrugIndex + emptyEnd;
                lvi.SubItems.Add(drugRow.DrugName + emptyEnd);
                lvi.SubItems.Add(drugRow.DrugSpec + emptyEnd);
                lvi.SubItems.Add(drugRow.DrugDose + emptyEnd);
                lvi.SubItems.Add(drugRow.DrugCount + emptyEnd);
                this.listViewDrug.Items.Add(lvi);
            }
            foreach (ColumnHeader ch in listViewDrug.Columns) { ch.Width = -1; }
            this.listViewDrug.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        /// <summary>
        /// 更新登录页面
        /// </summary>
        /// <param name="panelLoginShow"></param>
        private void HideShow(bool panelLoginShow)
        {
            if (panelLoginShow)
            {
                panelLogin.Visible = true;
                panelMain.Visible = false;
                panelLogin.Dock = DockStyle.Fill;
            }
            else
            {
                panelMain.Visible = true;
                panelLogin.Visible = false;
                panelMain.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// 将提示信息居中显示
        /// </summary>
        private void ShowTipCenter()
        {
            Point tipPoint = new Point();
            tipPoint.X = this.panelLogin.Width / 2 - lblLoginResult.Width / 2;
            tipPoint.Y = this.panelLogin.Height / 5;
            lblLoginResult.Location = tipPoint;
        }

        /// <summary>
        /// 更新配置方法行
        /// </summary>
        private void UpdateListMethod(List<string> list)
        {
            this.listViewConfigMethod.BeginUpdate();
            foreach (var item in list)
            {
                ListViewItem lvi1 = new ListViewItem();
                lvi1.Text = item;
                this.listViewConfigMethod.Items.Add(lvi1);
            }
            foreach (ColumnHeader ch in listViewConfigMethod.Columns) { ch.Width = -1; }
            this.listViewConfigMethod.EndUpdate();
        }
        #endregion 更新UI

        #region TcpServer和Controller事件

        private void TcpServer_Stopped(object sender, EventArgs e)
        {
            UpdateLabelResult("端口监听已停止");
        }

        private void TcpServer_Started(object sender, EventArgs e)
        {
            UpdateLabelResult("端口监听已开启");
        }

        private void TcpServer_EventScreenInfo(object sender, PivasEventArgs<MsgScreenInfo> e)
        {
            try
            {
                this.SafeAction(() =>
                {
                    //更换结果颜色
                    lblResult.ForeColor = e.Value.ChargeResult == StaticDictionary.CHARGE_RESULT_SUCCESS ? Color.Green : Color.Red;
                    UpdateLabelResult(e.Value.ChargeMessage);

                    this.lblWardName.Text = e.Value.WardName;
                    this.lblPatientName.Text = e.Value.PatientName;
                    this.lblLabelNo.Text = e.Value.LabelNo;

                    this.lblDoctor.Text = e.Value.EmployeeName;
                    this.lblConfigCount.Text = e.Value.MixCount;
                    this.lblTime.Text = e.Value.ShowTime;

                    UpdateListviewDrug(e.Value.Drugs);
                    UpdateListMethod(e.Value.Methods);
                });
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("接收到数据处理出错:"+ex.Message);
            }
        }

        private void TcpServer_EventLogin(object sender, PivasEventArgs<MsgLoginResult> e)
        {
            //0未登录状态，1登录状态
            if (e.Value.Status == StaticDictionary.DOCTOR_STATUS_FALSE)//未登录
            {
                HideShow(true);
                UpdateLabelResult("请扫描登录");
            }
            else
            {
                HideShow(false);
            }
        }

        private void TcpServer_ErrorOccurred(object sender, PivasEventArgs<string> e)
        {
            UpdateLabelResult(e.Value);
        }

        private void TcpServer_Disconnected(object sender, EventArgs e)
        {
            UpdateLabelResult("客户端已断开连接");
        }

        private void TcpServer_Connected(object sender, EventArgs e)
        {
            UpdateLabelResult("客户端已连接成功");
        }
        #endregion TcpServer和Controller事件

        #region 临时测试
        private void InitListview()
        {
            string[] indexArray = new string[5] { "A", "B", "C", "D", "E" };
            string[] drugArray = new string[5]
            {
                "J葡萄糖酸钙注射液",
                "多种微量元素注射液[18]-5443a",
                "甘油磷酸钠注射液[18]-4605gba",
                "J硫酸镁注射液[10]-1808",
                "J胰岛素注射液[18]-4217gb"
            };
            string[] specArray = new string[5]
            {
                "10ml:1g*5支", "10ml*10支", "10ml*10支", "10ml:2.5g*5支", "10ml:400单位"
            };
            string[] dosageArray = new string[5]
            {
                "2g", "10ml", "10ml", "2.5g", "30单位"
            };
            string[] countArray = new string[5]
            {
                "2", "1", "7", "8", "20"
            };

            string[] methodArray = new string[5]
            {
                "1、将A加入L中，摇匀，静置3-4分钟至待透明",
                "2、分别将B、C、D加入",
                "3、将D和E混合加入A中，摇匀",
                "4、再将H加入，摇匀，静置",
                "5、M加入后，观察是否与气泡，待无气泡后与A、B、C、D、E反复摇15秒后，静置"
            };

            this.listViewDrug.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度    
            string emptyEnd = " ".PadRight(5);
            for (int i = 0; i < 5; i++)   //添加10行数据  
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = indexArray[i] + emptyEnd;
                lvi.SubItems.Add(drugArray[i] + emptyEnd);
                lvi.SubItems.Add(specArray[i] + emptyEnd);
                lvi.SubItems.Add(dosageArray[i] + emptyEnd);
                lvi.SubItems.Add(countArray[i] + emptyEnd);
                this.listViewDrug.Items.Add(lvi);
            }
            foreach (ColumnHeader ch in listViewDrug.Columns) { ch.Width = -1; }
            this.listViewDrug.EndUpdate();  //结束数据处理，UI界面一次性绘制。

            this.listViewConfigMethod.BeginUpdate();
            for (int i = 0; i < 5; i++)
            {
                ListViewItem lvi1 = new ListViewItem();
                lvi1.Text = methodArray[i];
                this.listViewConfigMethod.Items.Add(lvi1);
            }
            foreach (ColumnHeader ch in listViewConfigMethod.Columns) { ch.Width = -1; }
            this.listViewConfigMethod.EndUpdate();
        }
        #endregion 临时测试

        private void panelLoginClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void panelMainClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
