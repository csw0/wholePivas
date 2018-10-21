using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PIVAsCommon.Models
{
    /// <summary>
    /// 医院汉字首字母标识
    /// </summary>
    public static class HospitalType_Const
    {
        public const string ORIGIN = "ORIGIN";//默认值
        public const string QD = "QD";//启东医院
        public const string BDGJ = "BDGJ";//北大国际
        public const string SDFY = "SDFY";//苏大附一
        public const string HNZL = "HNZL";//河南肿瘤
        public const string TJXK = "TJXK";//天津胸科
        public const string NBDY = "NBDY";//宁波第一医院
        public const string SHLYD = "SHLYD";//上海六院东
        public const string GDHQ = "GDHQ";//广东华侨医院
        public const string CZ = "CZ";//CZ医院
    }

    /// <summary>
    /// 收到瓶签后去计费得到的结果
    /// </summary>
    public static class ChargeResult_Const
    {
        public const string RESCAN_CHARGE_SUCCESS = "重复扫描，已成功计过费";
        public const string CHARGE_SUCCESS = "计费成功";
        public const string PRESCRIPTION_STOP = "医嘱已停，计费失败";
        public const string MANUAL_CANCEL = "配置已取消";
        public const string LABELHANDLE_EXCEPTION = "瓶签处理异常，计费失败";
        public const string PHARMACIST_LOGOUT = "药师未登录，计费失败";
        public const string NOTCONFIG_SUCCESS = "配置文件设置不计费；认为计费成功";
        public const string CHARGE_EXCEPTION = "计费接口异常，计费失败";
        public const string PACKADVANCE_WARDCOMPOUND = "提前打包且已计费，在病区配置";
        public const string OTHERWAY_SUCCESS = "已通过其他流程计费，且计费成功";
    }
}
