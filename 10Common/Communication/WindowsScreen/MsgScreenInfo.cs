using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication.WindowsScreen
{
    /// <summary>
    /// 屏信息实体
    /// </summary>
    public class MsgScreenInfo : MsgBase
    {
        /// <summary>
        /// 患者所在病区名称
        /// </summary>
        public string WardName { get; set; }

        /// <summary>
        /// 患者名字
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 14位标准瓶签或长度不定的第三方瓶签
        /// </summary>
        public string LabelNo { get; set; }

        /// <summary>
        /// 显示时间，格式HH:mm:ss MM/dd 
        /// </summary>
        public string ShowTime { get; set; }

        /// <summary>
        /// 药师名字
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 该药师分配的配液数量
        /// </summary>
        public string MixCount { get; set; }

        List<MsgDrugRowInfo> drugs = new List<MsgDrugRowInfo>();
        public List<MsgDrugRowInfo> Drugs
        {
            get { return drugs; }
            set { drugs = value; }
        }

        List<string> methods = new List<string>();
        public List<string> Methods
        {
            get { return methods; }
            set { methods = value; }
        }
        /// <summary>
        /// 计费后的结果消息
        /// </summary>
        public string ChargeMessage { get; set; }
    }
}
