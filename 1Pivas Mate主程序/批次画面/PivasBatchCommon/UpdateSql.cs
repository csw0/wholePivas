using System;
using System.Collections.Generic;
using System.Text;

namespace PivasBatchCommon
{
    public class UpdateSql
    {
        /// <summary>
        /// 批次修改
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="Btach">批次字符串</param>
        /// <param name="TeamNumber">批次</param>
        /// <param name="Ecode">修改人</param>
        /// <returns></returns>
        public string IVRecordBatch(string LabelNo, string oldBatch, string newBatch, int TeamNumber, string Ecode, ref string batchrule)
        {
            StringBuilder str = new StringBuilder();
            string ss = "修改人：" + Ecode + ",\r\n由" + oldBatch + "修改为" + newBatch + ",\r\n修改时间为：" + DateTime.Now.ToString();

            batchrule = ss;
            str.Append(" update IVRecord set ");
            str.Append(" batch='" + newBatch + "',");
            str.Append(" TeamNumber=" + TeamNumber + ",");
            str.Append(" BatchRule='" + ss + "'+CONVERT(varchar(100), GETDATE(), 20)");
            str.Append(" where LabelNo='" + LabelNo + "'");
            //str.Append(" and  IVStatus=0");
            return str.ToString();
        }

        public string IVRecordIsPatch(string WardCode, string DateTime, string Pcode)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" Update IVRecord set IsBatch=0 ");
            str.Append(" where WardCode =  '" + WardCode + "'");
            str.Append(" and LabelNo like '" + DateTime + "%'");
            str.Append(" and BatchSaved=0");
            if (Pcode.Length > 0)
            {
                str.Append(" and PatCode='" + Pcode + "'");
            }
            str.Append(" and IsBatch=1");
            return str.ToString();
        }
        /// <summary>
        /// 批次浏览设置
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <param name="PreviewMode">浏览模式</param>
        /// <param name="WardIdle">是否只显示有数据病区</param>
        /// <param name="WardOpen">是否只显示开放病区</param>
        /// <param name="AutoGetOrder">进入批次页面自动生成瓶签</param>
        /// <param name="LabelOrderBy">根据什么排序显示</param>
        /// <param name="NextDay">几点几分后自动显示为第二天</param>
        /// <param name="TimeCount">停在此界面不操作几分钟后自动刷新页面</param>
        /// <param name="AllChange">是否对所有帐号统一设置</param>
        /// <param name="AllChange">是否计算空包。，1是计算，0是不计算</param>
        /// <returns></returns>
        public string OrderFormSet(string Uid, int PreviewMode, int WardIdle, int WardOpen, int AutoGetOrder, int LabelOrderBy, string NextDay, int TimeCount, int IsPack, bool AllChange, int refresh)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update OrderFormSet set PreviewMode='" + PreviewMode + "',");
            str.Append(" WardIdle='" + WardIdle + "',WardOpen='" + WardOpen + "',");
            str.Append(" AutoGetOrder='" + AutoGetOrder + "',");
            str.Append(" LabelOrderBy=" + LabelOrderBy);
            str.Append(" ,NextDay='" + NextDay + "'");
            str.Append(" ,TimeCount=" + TimeCount);
            str.Append(" ,IsPack=" + IsPack);
            str.Append(",Refresh=" + refresh);
            if (!AllChange)
            {
                str.Append(" where DEmployeeID='" + Uid + "'");
            }
            return str.ToString();
        }

        /// <summary>
        /// 有改动设置
        /// </summary>
        /// <param name="Uid">用户ID</param>
        /// <param name="ChangeColor">颜色</param>
        /// <param name="AllChange">是否对所有帐号统一设置</param>
        /// <returns></returns>
        public string OrderFormSet(string Uid, string ChangeColor, bool AllChange)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update OrderFormSet set ");
            str.Append(" ChangeColor='" + ChangeColor + "'");
            if (!AllChange)
            {
                str.Append(" where DEmployeeID='" + Uid + "'");
            }
            return str.ToString();
        }

        /// <summary>
        /// 批次颜色设置
        /// </summary>
        /// <param name="OrderID">批次ID</param>
        /// <param name="OrderColor">批次颜色</param>
        /// <param name="OrderTColor">字体颜色</param>
        /// <returns></returns>
        public string OrderColor(Int16 OrderID, string OrderColor, string OrderTColor)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update OrderColor set OrderColor='" + OrderColor + "',OrderTColor='" + OrderTColor + "'");
            str.Append(" where OrderID='" + OrderID + "'");
            return str.ToString();
        }

        /// <summary>
        /// 病区列表显示
        /// </summary>
        /// <param name="DEmployeeID">用户ID</param>
        /// <param name="Name">字段名</param>
        /// <param name="Color">颜色</param>
        /// <param name="AllChange">是否对所有帐号统一设置</param>
        /// <returns></returns>
        public string OrderColor(string DEmployeeID, string Name, string Color, bool AllChange)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update OrderFormSet set " + Name + "='" + Color + "'");
            if (!AllChange)
            {
                str.Append(" where DEmployeeID='" + DEmployeeID + "'");
            }
            return str.ToString();
        }


        /// <summary>
        /// 病区发送
        /// </summary>
        /// <param name="dt">瓶签前十位</param>
        /// <param name="SelectText">查询的床号或者名字</param>
        /// <param name="Wardcode">病区</param>
        /// <param name="Choose">筛选条件。K，#</param>
        /// <returns></returns>
        public string IVRecordBatchSaved(string dt, string SelectText, string Wardcode, string Choose)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update IVRecord set ");
            str.Append(" BatchSaved=1,");
            str.Append(" BatchSavedDT=CONVERT(varchar(100), GETDATE(), 25)");
            str.Append(" where LabelNo like '" + dt + "%'");
            str.Append(" and IVStatus=0 and LabelOver>=0");
            if (SelectText != "")
            {

                str.Append(" and (BedNo like '%" + SelectText + "%' or PatName '%" + SelectText + "%' )");
            }
            else
            {
                if (!Wardcode.Equals("0"))
                {
                    str.Append(" and WardCode in (" + Wardcode + ") ");
                }
            }
            //if (Choose.Equals("#/K/L"))
            //{
            //    str.Append("and Batch like '%" + Choose + "%'");
            //}

            if (pvb.Choose2.Trim().Length > 0 && !pvb.Choose.Equals("#/K/L"))
            {
                str.Append(" " + pvb.Choose2);
                //  str1.Append("  " + pvb.Choose2);

            }
            else
            {
                if (!pvb.Choose.Equals("#/K/L"))
                {
                    str.Append(" and Batch like '%" + pvb.Choose + "%' ");
                    //  str1.Append(" and Batch like '%" + pvb.Choose + "%'  ");
                }
            }
            return str.ToString();
        }

        /// <summary>
        /// 批量发送，更新瓶签BatchSaved
        /// </summary>
        /// <param name="labelNo">瓶签号</param>
        /// <returns></returns>
        public string IVRecordBatchSaved(string labelNo, string S1, string SK, string LS1, string LSK)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  update IVRecord set  BatchSaved=1, ");
            str.Append(" Remark3=case when Batch like '%#' and Batch not like 'L%' then '");
            str.Append(S1);
            str.Append("' when Batch like '%K'and Batch not like 'L%' then '");
            str.Append(SK);
            str.Append("' when Batch like 'L%' and Batch like '%#' then  '");
            str.Append(LS1);
            str.Append("'  when Batch like 'L%' and Batch like '%K' then '");
            str.Append(LSK);
            str.Append("' end ,");
            str.Append("BatchSavedDT=CONVERT(varchar(100), GETDATE(), 25)  ");
            //str.Append(" where LabelNo = '" + labelNo + "'");
            str.Append(" where LabelNo in (" + labelNo + ") and IVStatus=0  and LabelOver>=0  ");

            return str.ToString();
        }

        /// <summary>
        /// 批量发送，更新瓶签BatchSaved
        /// </summary>
        /// <param name="labelNo">患者code</param>
        /// <param name="str">瓶签前八位</param>
        /// <returns></returns>
        public string IVRecordBatchSaved(string Patcode, string timestr, string S1, string SK, string LS1, string LSK)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update IVRecord set ");
            str.Append(" BatchSaved=1,");
            str.Append(" Remark3=case when Batch like '%#' and Batch not like 'L%' then '");
            str.Append(S1);
            str.Append("' when Batch like '%K'and Batch not like 'L%' then '");
            str.Append(SK);
            str.Append("' when Batch like 'L%' and Batch like '%#' then  '");
            str.Append(LS1);
            str.Append("'  when Batch like 'L%' and Batch like '%K' then '");
            str.Append(LSK);
            str.Append("' end ,");
            str.Append(" BatchSavedDT=CONVERT(varchar(100), GETDATE(), 25)");

            //str.Append(" where LabelNo = '" + labelNo + "'");
            str.Append(" where PatCode in (" + Patcode + ")");
            str.Append(" and IVStatus=0  and LabelOver>=0 and LabelNo like '%" + timestr + "%'");

            return str.ToString();
        }

        /// <summary>
        /// 单个病人发送
        /// </summary>
        /// <param name="dt">瓶签前十位</param>
        /// <param name="Patcode">病人代码</param>
        /// <param name="Wardcode">病区代码</param>
        /// <returns></returns>
        public string IVRecordBatchSaved_OnePat(string dt, string Patcode, string Wardcode, string S1, string SK, string LS1, string LSK)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update IVRecord set ");
            str.Append(" BatchSaved=1,");
            str.Append(" Remark3=case when Batch like '%#' and Batch not like 'L%' then '");
            str.Append(S1);
            str.Append("' when Batch like '%K'and Batch not like 'L%' then '");
            str.Append(SK);
            str.Append("' when Batch like 'L%' and Batch like '%#' then  '");
            str.Append(LS1);
            str.Append("'  when Batch like 'L%' and Batch like '%K' then '");
            str.Append(LSK);
            str.Append("' end ,");
            str.Append(" BatchSavedDT=CONVERT(varchar(100), GETDATE(), 25)");
            str.Append("where labelNo in (" + dt + ")");
            str.Append(" and IVStatus=0  and LabelOver>=0");
            str.Append(" and Patcode='" + Patcode + "'");
            return str.ToString();
        }


        //@LogId varchar(64), --员工号
        //@wardcode varchar(50)='',--病区编码
        //@patCode varchar(50)='',--病人编码
        //@dt 生成日期
        public string GetIvRecord(string LogId, string wardcode, string patCode, DateTime dt)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" exec autoivorder '" + LogId + "','" + wardcode + "',");
            str.Append(" '" + patCode + "','" + dt.Date.ToString("yyyy-MM-dd") + "'");
            //System.Windows.Forms.MessageBox.Show(str.ToString());
            return str.ToString();
        }


        /// <summary>
        /// 增加修改批次记录
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="EmpId"></param>
        /// <param name="EmpName"></param>
        /// <param name="old"></param>
        /// <param name="New"></param>
        /// <returns></returns>
        public string OrderChangelog(string ID, string EmpId, string EmpName, string old, string New, string IVStatus, string Reason, string ReasonDetail)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" insert  into OrderChangelog");
            str.Append(" values(");
            str.Append(ID + ",");
            str.Append("GETDATE(),");
            str.Append("'" + EmpId + "',");
            str.Append("'" + EmpName + "',");
            str.Append("'" + old + "',");
            str.Append("'" + New + "',");
            str.Append("'" + IVStatus + "',");
            str.Append("'" + Reason + "','");
            str.Append(ReasonDetail + "')");
            return str.ToString();
        }
    }
}

