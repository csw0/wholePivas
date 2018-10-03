using System;
using System.Data;

namespace PivasBatchCommon
{
    public static class pvb
    {

        //public static string ward = string.Empty;
        /// <summary>
        /// 病区编码字符串：'code1','code2'
        /// </summary>
        public static string ward = "''";
        //页面显示的时间，在等待生成瓶签的界面会用到
        public static DateTime datetime;
        //显示模式
        /// <summary>
        /// 显示模式 
        /// </summary>
        public static string PreviewMode = "0";

        /// <summary>
        ///仅显示有数据病区 
        /// </summary>
        public static int WardIdle = 0;
        //仅显示开放病区
        /// <summary>
        /// 仅显示开放病区
        /// </summary>
        public static int WardOpen = 0;
        /// <summary>
        /// 进入批次界面是否自动生成瓶签
        /// </summary>
        public static int AutoGetOrder = 1;
        /// <summary>
        /// 按什么方式排序
        /// </summary>
        public static int LabelOrderBy = 0;
        /// <summary>
        /// 停留在此画面不操作
        /// </summary>
        public static int timeCount = 180;
        /// <summary>
        /// 是否计算空包。0为计算，1为不计算
        /// </summary>
        public static int IsPack = 1;
        /// <summary>
        /// 是否停留在此画面没操作
        /// </summary>
        public static bool operate = false;
        /// <summary>
        /// 当前员工ID
        /// </summary>
        public static string DEmployeeID = "8888";
        /// <summary>
        /// 当前员工帐号
        /// </summary>
        public static string AccountID = string.Empty;
        /// <summary>
        /// 当前员工姓名
        /// </summary>
        public static string DEmployeeName = string.Empty;
        /// <summary>
        /// 是否保存状态 0 未发送，1已发送，2已打印
        /// </summary>
        public static int IvBatchSaved;
        //病区总数
        public static string WardCount;
        //筛选（k、l）
        public static string Choose = "";
        //筛选中的筛选
        public static string Choose2 = "";
        //public static string  
        /// <summary>
        /// 存储当前登录人批次颜色配置
        /// </summary>
        public static DataSet ChangeColords = new DataSet();


        /// <summary>
        /// 列表模式快速修改：true，可快速修改，false，不可
        /// </summary>
        public static bool fastChange;

        /// <summary>
        /// OrderID，快速修改做判断用
        /// </summary>
        public static int[] OrderID = new int[24];

        /// <summary>
        /// 已发送的瓶签是否允许修改批次 0 不可以；1 可以
        /// </summary>
        public static string ChangeSendBatch = "0";
        /// <summary>
        /// 已打印的瓶签是否允许修改批次 0 不可以；1 可以
        /// </summary>
        public static string ChangePrintBatch = "0";

        /// <summary>
        /// 病区列表选择方式，0、默认不选；1、默认选第一个；2、默认全选
        /// </summary>
        public static string WardDefaultSelectMode = "1";

        public static string combineBatch = string.Empty;
    }
}
