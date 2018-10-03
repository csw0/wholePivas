using System.Collections.Generic;

namespace ChargeInterface
{
    public interface ICharge
    {
        /// <summary>
        /// 计费接口（舱内扫描程序调用）
        /// 0 计费不成功
        /// 1 计费成功
        /// msg 失败原因
        /// 调HIS接口前，判断LabelOver是否等于0，不等于0认为配置已取消，直接返回"0"；
        /// 调HIS接口前，判断PStatus是否等于4，等于0认为医嘱已停，直接返回"0"，同时更新IVRecord表的labelOver=-3；
	    /// 调HIS接口前，判断IVStatus是否大于等于9且remark3的值等于15，返回"1"，已记过费不再重复调用;
        /// 调用HIS计费接口,根据HIS计费接口的返回值，更新一些信息。
	    /// 计费成功，更新IVRecord表的Remark3=15（计费成功），保留计费痕迹。
	    /// 计费失败，若his确定此药病区不再需要，更新IVRecord表的LabelOver=-3（收费处取消）。
	    /// 计费失败，若his因模糊原因造成返回计费失败，更新IVRecord表的Remark3=12(计费失败)，保留计费痕迹，为工作量统计做基础支撑。
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string Charge(string labelno, string UserID, out string msg);

        /// <summary>
        /// 计费前，调用HIS存储过程，判断是否需计费(因HIS取消等原因)
        /// </summary>
        /// <param name="p_group_no"></param>
        /// <param name="prm_EXEC_DOCTOR"></param>
        /// <param name="prm_jujueyy"></param>
        /// <param name="PRM_DATABUFFER"></param>
        /// <param name="PRM_APPCODE"></param>
        /// <returns></returns>
        int BackPre(string p_group_no, string prm_EXEC_DOCTOR, string prm_jujueyy, out string PRM_DATABUFFER, out string PRM_APPCODE);

        /// <summary>
        /// 审方通过调用
        /// 0 不成功
        /// 1 成功
        /// 不管返回成功 不成功，程序都暂不做任何处理。只会将MSG报出来。
        /// </summary>
        /// <param name="GroupNo">处方组号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 审方不通过调用
        /// 0 不成功
        /// 1 成功
        /// 不管返回成功 不成功，程序都暂不做任何处理。只会将MSG报出来。
        /// </summary>
        /// <param name="GroupNo">处方组号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 打印计费按钮调用
        /// </summary>
        /// <param name="LabelNos"></param>
        /// <param name="DEmployeeID">计费人ID</param>
        /// <returns></returns>
        bool PrintCharge(List<string> LabelNos, string DEmployeeID);

        /// <summary>
        /// 排药核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string ScanPY5(string LabelNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 进仓核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string ScanJC7(string LabelNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 配置核对（可通过此方法调用舱内扫描计费接口）
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string ScanPZ9(string LabelNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 出仓核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string ScanCC11(string LabelNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 打包核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string ScanDB13(string LabelNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 病区签收
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string ScanQS15(string LabelNo, string DEmployeeID, out string msg);

        /// <summary>
        /// 备用方法1
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="No">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string SPARE1(string No, string DEmployeeID, out string msg);

        /// <summary>
        /// 备用方法2
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="No">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string SPARE2(string No, string DEmployeeID, out string msg);

        /// <summary>
        /// 备用方法3
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="No">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string SPARE3(string No, string DEmployeeID, out string msg);

        /// <summary>
        /// 备用方法4
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="No">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string SPARE4(string No, string DEmployeeID, out string msg);

        /// <summary>
        /// 备用方法5
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="No">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        string SPARE5(string No, string DEmployeeID, out string msg);

        /// <summary>
        /// 更改ScreenDeatils表的状态
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="Status">状态 0：未知；1：核对已成功</param>
        void ChangeStatus(string LabelNo, string DEmployeeID, string MoxaIP, string port, int Status);

        /// <summary>
        /// 调用HIS存储过程进行计费
        /// </summary>
        /// <param name="Groupno"></param>
        /// <param name="infusionDT"></param>
        /// <param name="UserCode"></param>
        /// <param name="hismsg"></param>
        /// <param name="hisret"></param>
        /// <returns></returns>
        int WXCharge(string Groupno, string infusionDT, string UserCode, out string hismsg, out string hisret);
    }    
}
