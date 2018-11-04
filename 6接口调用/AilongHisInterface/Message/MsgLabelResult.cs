using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AilongHisInterface.Message
{
    /// <summary>
    /// 屏信息实体
    /// </summary>
    public class MsgLabelResult : MsgBase
    {
        public MsgLabelResult()
        {
            this.MsgType = (short)MsgConstantType.ScanResultInfo;
        }
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
        /// 药师名字
        /// </summary>
        public string EmployeeName { get; set; }

        List<MsgDrugRowInfo> drugs = new List<MsgDrugRowInfo>();
        public List<MsgDrugRowInfo> Drugs
        {
            get { return drugs; }
            set { drugs = value; }
        }

        /// <summary>
        /// 计费后的结果消息
        /// </summary>
        public string ChargeMessage { get; set; }
        /// <summary>
        /// 计费成功与否,0=失败；1=成功
        /// </summary>
        public short ChargeResult { get; set; }
    }
}
