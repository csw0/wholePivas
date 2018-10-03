using System;
using System.Data;
using System.Collections.Generic;
using PIVAsCommon.Helper;
using PIVAsCommon;

namespace PivasMcc
{
    /// <summary>
    /// 结构体，存储PLC信息
    /// </summary>
    public struct TPLCDevicePromptDefaultSet
    {
        public string SuccessPromptType;
        public string SuccessPromptMS;
        public string SuccessPromptTime;
        public string BackedPromptType;
        public string BackedPromptMS;
        public string BackedPromptTime;
    }

    public class MOXA
    {
        DB_Help DB = new DB_Help();
        public DataSet DSMOXA = new DataSet();
        public DataSet DSDesk = new DataSet();
        public DataSet DSLight = new DataSet();
        public MOXA(string Group)
        {
            try
            {
                if (string.IsNullOrEmpty(Group))//列出所有
                {
                    DSMOXA = DB.GetPIVAsDB("select distinct MOXAIP,MOXANo,MOXAModel,[Group] FROM MOXACon order by MOXANo ");
                    DSDesk = DB.GetPIVAsDB("Select [ID],[MOXAIP],[MOXAModel],[MOXAPort],[DeskNo],[DeskDesc],[PLC] ," +
                        "[RedLight],[GreenLight],[MOXANo],[ScreenIP],[Group] from MOXACon");
                }
                else
                {
                    DSMOXA = DB.GetPIVAsDB("select distinct MOXAIP,MOXANo,MOXAModel,[Group] FROM MOXACon where [Group]='"
                        + Group + "' order by MOXANo ");
                    DSDesk = DB.GetPIVAsDB("Select [ID],[MOXAIP],[MOXAModel],[MOXAPort],[DeskNo],[DeskDesc],[PLC] ," +
                        "[RedLight],[GreenLight],[MOXANo],[ScreenIP],[Group] from MOXACon where [Group]='" + Group + "'");
                }
            }
            catch (System.Exception ex)
            {
                InternalLogger.Log.Error("构造MOXA对象出错:" + ex.Message);
            }
        }
    }

    public enum TPromptType { ptLong, ptAlternation };

    /// <summary>
    /// 控制灯的类型
    /// 1=记账成功（S）；2=退药（B）；3=记账失败（F）
    /// </summary>
    public enum ControlLightType { ChargeSuccess = 1, BackDrug=2,ChargeFail=3 };

    public class TPort
    {
        public int FGPid;
        public string FIP;
        public int FPortNo;
        public string FDesk;
        public string FDeskDesc;
        public string FSDeviceName;
        public TPromptType FSPromptType;
        public string FSPromptMS;
        public string FSPromptTime;
        public string FBDeviceName;
        public TPromptType FBPromptType;
        public string FBPromptMS;// Word;
        public string FBPromptTime;
        public string FStat;
        public string FECode;
        public string FEName;
        public string FELoginTime;
        public bool FReading;
        public int FFinishCount;
        public string FLabelNo;
        public string FConfectResult;
        public string FWard;
        public string FTestStr;

        public TPort(string IP, int Port, string Desk, string DeskDesc, string SDeviceName, TPromptType SPromptType, string SPromptMS,
                    string SPromptTime, string BDeviceName, TPromptType BPromptType, string BPromptMS, string BPromptTime)
        {
            FGPid = -102; // NSIO_NOT_ALIVE (defined in IPSerial.pas)
            FIP = IP;
            FPortNo = Port;
            FDesk = Desk;
            FDeskDesc = DeskDesc;

            FSDeviceName = SDeviceName;
            FSPromptType = SPromptType;
            FSPromptMS = SPromptMS;
            FSPromptTime = SPromptTime;
            FBDeviceName = BDeviceName;
            FBPromptType = BPromptType;
            FBPromptMS = BPromptMS;
            FBPromptTime = BPromptTime;

            FStat = "关闭";
            FECode = "--";
            FEName = "--";
            FELoginTime = "--";
            FReading = false;
            FFinishCount = 0;
            FLabelNo = "--";
            FConfectResult = "未登录";
            FTestStr = "";
        }
    }

    public class ListviewMain_Model
    {
        private string _MoxaIP;
        private string _MoxaPort;
        private string _DeskNo;
        private string _DeskDesc;
        private string _PLC;
        private string _RedLight;
        private string _GreenLight;
        private string _ScreenIP;
        private int _ScreenPort;

        public string MoxaPort
        {
            get { return _MoxaPort; }
            set { _MoxaPort = value; }
        }


        public string DeskNO
        {
            get { return _DeskNo; }
            set { _DeskNo = value; }
        }

        public string DeskDesc
        {
            get { return _DeskDesc; }
            set { _DeskDesc = value; }
        }

        public string PLC
        {
            get { return _PLC; }
            set { _PLC = value; }
        }

        public string RedLight
        {
            get { return _RedLight; }
            set { _RedLight = value; }
        }


        public string GreenLight
        {
            get { return _GreenLight; }
            set { _GreenLight = value; }
        }

        public string MoxaIP
        {
            get { return _MoxaIP; }
            set { _MoxaIP = value; }
        }

        public string ScreenIP
        {
            get { return _ScreenIP; }
            set { _ScreenIP = value; }
        }
        public int ScreenPort
        {
            get { return _ScreenPort; }
            set { _ScreenPort = value; }
        }
    }

    public class ControlLightCommand
    {
        private string _MoxaIP = String.Empty;
        private string _MoxaPort = String.Empty;
        private int _LightColor = 0;//1=红灯；2=绿灯
        private int _LightStatus = 0;//不等于1=关；1=开
        private int _LightTime = 0;//此灯亮持续时间，或灭持续时间

        public ControlLightCommand(string moxaIP, string moxaPort, int lightColor, int lightStatus, int lightTime)
        {
            this._MoxaIP = moxaIP;
            this._MoxaPort = moxaPort;
            this._LightColor = lightColor;
            this._LightStatus = lightStatus;
            this._LightTime = lightTime;
        }

        public ControlLightCommand()
        {
        }

        public string MoxaIP
        {
            get { return _MoxaIP; }
            set { _MoxaIP = value; }
        }
        public string MoxaPort
        {
            get { return _MoxaPort; }
            set { _MoxaPort = value; }
        }

        public int LightColor
        {
            get { return _LightColor; }
            set { _LightColor = value; }
        }

        public int LightStatus
        {
            get { return _LightStatus; }
            set { _LightStatus = value; }
        }

        public int LightTime
        {
            get { return _LightTime; }
            set { _LightTime = value; }
        }
    }

    public class PLCCommand
    {
        private bool _Send = false;
        private byte _ThirdFrame= 0;//第三帧，plc1到8端口
        private byte _FourthFrame = 0;//第四帧，plc9到16端口
        //key是led地址号，value是发送时刻
        private Dictionary<int, double> _DicLightSendTime = new Dictionary<int, double>();

        public PLCCommand(bool send, byte thirdFrame, byte fourthFrame)
        {
            this._Send = send;
            this._ThirdFrame = thirdFrame;
            this._FourthFrame = fourthFrame;
        }

        public PLCCommand()
        {
        }

        public bool Send
        {
            get { return _Send; }
            set { _Send = value; }
        }
        public byte ThirdFrame
        {
            get { return _ThirdFrame; }
            set { _ThirdFrame = value; }
        }

        public byte FourthFrame
        {
            get { return _FourthFrame; }
            set { _FourthFrame = value; }
        }

        public Dictionary<int, double> DicLightSendTime
        {
            get { return _DicLightSendTime; }
            set { _DicLightSendTime = value; }
        }
    }
}