using PIVAsCommon.Helper;
using System.Data;
using System.Text;

namespace PivasLabelCheckAll.dao
{
    /// <summary>
    /// 数据库sql集合
    /// </summary>
    public class seldb
    {
        /// <summary>
        /// 初始化数据库连接方案
        /// </summary>
        private  DB_Help db = new DB_Help();
        /// <summary>
        /// 数据保存的dataset
        /// </summary>
        private DataSet ds = null;

        /// <summary>
        /// 加载下拉列表框
        /// </summary>
        /// <returns></returns>
        public DataSet getCombox(string date)
        {
            ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct TeamNumber ");
            sql.Append(" from IVRecord IVR  ");
            sql.Append(" where IVR.IVStatus>=3 order by TeamNumber");
            ds = db.GetPIVAsDB(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取所有的病区组
        /// </summary>
        /// <returns>一般为空表</returns>
        public DataSet getWardareas()
        {
            StringBuilder sql = new StringBuilder()
                .Append(" select distinct WardArea  from Dward where WardArea <>'' and WardArea is Not Null order by WardArea");
            return db.GetPIVAsDB(sql.ToString());
        }

        /// <summary>
        /// 获取所有病区的方法
        /// </summary>
        /// <returns></returns>
        public DataSet getAllDward(string date, string sql2, string sql3, string batch1, string drugtype1, string wardarea)
        {
            ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select  WardCode ,WardName,WardSimName,WardArea,Spellcode,WardSeqNo,LabelWardCount");
            //sql.Append(",PackAdvanceCount ");提前打包数量
            sql.Append(" from ");
            sql.Append(" (");
            sql.Append("select  ");
            sql.Append("WardCode ,WardName,WardSimName,WardArea,Spellcode,WardSeqNo,");
            sql.Append(" (");
            sql.Append("  select count(1) from IVRecord IVR left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  left join Prescription P on P.PrescriptionID = IVR.PrescriptionID   where 1=1   ");
            sql.Append("  and IVRP.PrintCount = '0' and  IVR.WardRetreat='0' and IVR.LabelOver>=0 ");
            sql.Append(" ");
            sql.Append("and DATEDIFF(DD,IVR.InfusionDT,'" + date + "')=0    ");
            if (sql3 != null && sql3 != "")
            {
                sql.Append(sql3);
            }
            if (batch1 != "" && batch1 != null)
            {
                sql.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            if (sql2 != "" && sql2 != null)
            {
                sql.Append(sql2);
            }
            if (drugtype1 != "" && drugtype1 != null)
            {
                sql.Append(" and p.DrugType IN " + drugtype1 + "");
            }

            sql.Append(" and IVR.WardCode = DWard.WardCode");
            sql.Append(") as LabelWardCount ");

           #region //每个病区按批次统计提前打包数量
            //sql.Append(" ,(");
            //sql.Append("  select count(PackAdvance) from IVRecord IVR left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  left join Prescription P on P.PrescriptionID = IVR.PrescriptionID  where PackAdvance=1   ");
            //sql.Append("  and IVRP.PrintCount = '0' and  IVR.WardRetreat='0' and IVR.LabelOver>=0  ");
            //sql.Append(" and DATEDIFF(DD,IVR.InfusionDT,'" + date + "')=0    ");
            //if (sql3 != null && sql3 != "")
            //{
            //    sql.Append(sql3);
            //}
            //if (batch1 != "" && batch1 != null)
            //{
            //    sql.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            //}
            //if (sql2 != "" && sql2 != null)
            //{
            //    sql.Append(sql2);
            //}
            //sql.Append(" and IVR.WardCode = DWard.WardCode");
            //sql.Append(") as PackAdvanceCount ");
           #endregion

            sql.Append("from DWard  ");
            sql.Append("where   IsOpen = 1  ");
            if (wardarea != null && wardarea != "" && wardarea != "全部病区组")
            {
                sql.Append("  and WardArea = '" + wardarea + "'");
            }
            sql.Append("  ) as a ");
            sql.Append("order by WardArea , WardSeqNo;");
            ds = db.GetPIVAsDB(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 查询有数据的病区
        /// </summary>
        /// <param name="date"></param>
        /// <param name="sql2"></param>
        /// <param name="sql3"></param>
        /// <param name="batch1"></param>
        /// <returns></returns>
        public DataSet getDwardHaveData(string date, string sql2, string sql3, string batch1,string drugtype1)
        {
            ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select  WardCode ,WardName,WardSimName,WardArea,Spellcode,WardSeqNo,LabelWardCount");
            //sql.Append(",PackAdvanceCount ");提前打包数量
            sql.Append(" from ");
            sql.Append(" (");
            sql.Append("select  ");
            sql.Append("WardCode ,WardName,WardSimName,WardArea,Spellcode,WardSeqNo,");
            sql.Append(" (");
            sql.Append("  select count(1) from IVRecord IVR left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  left join Prescription P on P.PrescriptionID = IVR.PrescriptionID  where 1=1   ");
            sql.Append("  and IVRP.PrintCount = '0' and  IVR.WardRetreat='0' and IVR.LabelOver>=0  ");
            sql.Append(" and DATEDIFF(DD,IVR.InfusionDT,'" + date + "')=0    ");
            if (sql3 != null && sql3 != "")
            {
                sql.Append(sql3);
            }
            if (batch1 != "" && batch1 != null)
            {
                sql.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            if (sql2 != "" && sql2 != null)
            {
                sql.Append( sql2 );
            }

            if (drugtype1!= "" && drugtype1!=null)
            {
                sql.Append(" and p.DrugType IN " + drugtype1 + "");
            }

            
            sql.Append(" and IVR.WardCode = DWard.WardCode");
            sql.Append(") as LabelWardCount ");

            #region//每个病区按批次统计提前打包数量
            //sql.Append(" ,(");
            //sql.Append("  select count(PackAdvance) from IVRecord IVR left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  left join Prescription P on P.PrescriptionID = IVR.PrescriptionID  where PackAdvance=1   ");
            //sql.Append("  and IVRP.PrintCount = '0' and  IVR.WardRetreat='0' and IVR.LabelOver>=0  ");
            //sql.Append(" and DATEDIFF(DD,IVR.InfusionDT,'" + date + "')=0    ");
            //if (sql3 != null && sql3 != "")
            //{
            //    sql.Append(sql3);
            //}
            //if (batch1 != "" && batch1 != null)
            //{
            //    sql.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            //}
            //if (sql2 != "" && sql2 != null)
            //{
            //    sql.Append(sql2);
            //}
            //sql.Append(" and IVR.WardCode = DWard.WardCode");
            //sql.Append(") as PackAdvanceCount ");
            #endregion

            sql.Append("from DWard ");
            sql.Append("where   IsOpen = 1  ");
            sql.Append("  ) as a  where  LabelWardCount>0 ");
            sql.Append(" order by WardArea , WardSeqNo;");
            ds = db.GetPIVAsDB(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 用户登陆调用的sql
        /// </summary>
        /// <param name="barCode">二维码或者员工编号</param>
        /// <param name="password">登陆密码</param>
        /// <returns></returns>
        public DataSet UserLogin(string barCode,string password)
        {
            ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            if (barCode.Substring(0, 4) == "7777" && barCode.Length >= 22)
            {
                sql.Append("select *  ");
                sql.Append("from QRcodeLog a ");
                sql.Append("inner join DEmployee b ");
                sql.Append("on a.DEmployeeID=b.DEmployeeID ");
                sql.Append("where a.QRcode= '" + barCode + "' ");
                sql.Append(" and b.IsValid=1 ");
            }
            else
            {
                sql.Append("select * ");
                sql.Append("from DEmployee emp ");
                sql.Append("where AccountID= '" + barCode + "' ");
                sql.Append("and Pas = '" + password + "'");
            }
            ds = db.GetPIVAsDB(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 扫描总瓶签
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Select"></param>
        /// <returns></returns>
        public DataSet IVRecordPrint(string Code)
        {
            ds = new DataSet();
            StringBuilder sql = new StringBuilder();
            sql.Append("select distinct IVRecord_Print.LabelNo from IVRecord_Print ");
            sql.Append("inner join IVRecord on IVRecord_Print.LabelNo=IVRecord.LabelNo ");
            sql.Append("  where (DrugQRCode='" + Code + "'or OrderQRCode='" + Code + "')");
            ds = db.GetPIVAsDB(sql.ToString());
            return ds;
        }

        /// <summary>
        /// 查询瓶签信息
        /// </summary>
        /// <param name="record"></param>
        /// <param name="type"></param>
        /// <param name="DWard">病区</param>
        /// <param name="Date">日期</param>
        /// <param name="Batch">批次</param>
        /// <param name="Sx">筛选</param>
        /// <param name="Status">瓶签状态(已核对，未核对，已退药)</param>
        /// <returns></returns>
        public DataSet IVRecord(string record, string type, string DWard, string Date, string sql1, string Batch, string Sx, string Status, int checkMode, string labelno,string drugtype1)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();

            str.Append("select IVR.LabelNo,IVR.WardName,IVR.WardCode ,IVR.BedNo,IVR.PatName ,IVR.Batch,b.Pcode,b.time,IVR.WardRetreat,IVR.LabelOver,IVR.IVStatus from IVRecord IVR");
            str.Append(" left join ");
            str.Append(" (select IVRecordID," + type + " time ,b.DEmployeeName as Pcode from " + record + " IVR,DEmployee b where ScanCount='0' and IVR.PCode=b.DEmployeeID  )b");
            str.Append("  on IVR.LabelNo=b.IVRecordID");
            str.Append(" left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  ");
            str.Append(" left join Prescription P on P.PrescriptionID = IVR.PrescriptionID ");
            str.Append(" where 1=1");
            str.Append(" and ivstatus>=3 and IVRP.PrintCount = '0' ");
            if (DWard != "" && DWard != null)
            {
                if (checkMode == 1)
                {
                    str.Append(" and IVR.WardCode in " + DWard + " ");
                }
                else
                {
                    str.Append(" and (IVRP.DrugQRCode in " + DWard + "  or IVRP.OrderQRCode in " + DWard + "  )");
                }
            }
            else
            {
                str.Append(" and WardCode in ('') ");
            }
            if (Date != "")
                str.Append(" and DATEDIFF(DD,InfusionDT,'" + Date + "')=0 ");
            if (Sx != "")
                str.Append(" and Batch like '%" + Sx + "%'");
            if (sql1 != "")
            {
                if (sql1.Trim() != "all")
                {
                    str.Append(sql1);
                }
                else
                {
                    str.Append("");
                }
            }
           
            if (Batch != "" && Batch != null)//批次
            {
                Batch = Batch.Replace("长期:", "");
                Batch = Batch.Replace("#", "");
                str.Append("and IVR.TeamNumber in (" + Batch + ") ");
            }

            if (drugtype1 != "" && drugtype1 != null)
            {
                str.Append(" and P.DrugType in " + drugtype1 + "");
            }

            if (Status == "已核对")
            {
                str.Append(" and WardRetreat!=1 and WardRetreat!=2 and LabelOver>=0 ");
                str.Append(" and LabelNo in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
            }
            else if (Status == "未核对")
            {
                str.Append(" and WardRetreat!=1 and WardRetreat!=2 and LabelOver>=0 ");
                str.Append(" and LabelNo not in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
            }
            else if (Status == "已退药")
            {
                str.Append(" and (WardRetreat=1 or WardRetreat=2 or LabelOver<0) ");
            }
            else
            {
                str.Append(" and WardRetreat!=1 and WardRetreat!=2 and LabelOver>=0 ");
            }

            str.Append(" and IVR.LabelNo = '" + labelno + "'");

            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 判断当前的瓶签是否符合核对条件()
        /// </summary>
        /// <param name="wards"></param>
        /// <param name="date"></param>
        /// <param name="sql1"></param>
        /// <param name="sql3"></param>
        /// <param name="batch1"></param>
        /// <param name="LabelKindFlag"></param>
        /// <param name="labelnum"></param>
        /// <returns></returns>
        public DataSet CheckLabelNumber(string wards, string date, string sql1, string sql3, string batch1, int LabelKindFlag, string labelnum, int CheckMode,string checktable,string checkdt)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select  ");
            str.Append("IVR.LabelNo as '瓶签号',  ");
            str.Append("IVR.WardCode as '病区编号',  ");
            str.Append("ward.WardName as '病区',  ");
            str.Append("IVR.BedNo as '床号',  ");
            str.Append("IVR.PatName as '患者姓名',  ");
            str.Append("IVR.Batch as '批次',  ");
            str.Append("employee.DEmployeeName as '扫描人',  ");
            str.Append("db." + checkdt + " as '扫描时间' ");
            str.Append("from IVRecord IVR ");
            str.Append("left join DWard ward on ward.WardCode = IVR.WardCode  ");
            str.Append("left join " + checktable + " db on db.IVRecordID = IVR.LabelNo  ");
            str.Append("left join DEmployee employee on employee.DEmployeeCode = db.PCode ");
            str.Append("left join Prescription P on P.PrescriptionID = IVR.PrescriptionID ");
            str.Append("left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo ");
            str.Append(" where 1=1");
            str.Append(" and  IVR.LabelOver>=0 ");

            if (sql3 != "" && sql3 != null)
            {
                str.Append(sql3);
            }

            if (CheckMode == 1)//病区模式
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and IVR.WardCode in " + wards + " ");
                }
                else
                {
                    str.Append(" and IVR.WardCode in ('') ");
                }
            }
            else//总瓶签模式
            {

                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and (IVRP.DrugQRCode in " + wards + "  or IVRP.OrderQRCode in " + wards + "  )");
                }
                else
                {
                    str.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                }
            }

            if (date != "")//时间
            {
                str.Append(" and DATEDIFF(DD,InfusionDT,'" + date + "')=0 ");
            }

            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            }

            if (LabelKindFlag == 0)//未扫描过的
            {
                str.Append(" and db." + checkdt + " is null ");
            }
            else if (LabelKindFlag == 1)//已经扫描过的
            {
                str.Append(" and   Len(db." + checkdt + ")>0 ");
            }
            else//显示全部瓶签，包括已经扫描和未扫描的
            {
                //str.Append(" ");
            }

            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }

            if (labelnum != "")
            {
                str.Append("  and IVR.LabelNo = '"+labelnum+"' ");
            }

            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 查询该瓶签号原来的病区和床号和现在的病区床号
        /// </summary>
        /// <returns></returns>
        public DataSet FindDwardAndBedOLD(string label)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append(" select ");
            str.Append("p.PatCode as PCode,  ");
            str.Append("p.PatName,  ");
            str.Append(" r.WardCode  as PWardOld, ");
            str.Append("p.WardCode as pWardNew, ");
            str.Append("r.WardName as PWardNameOld, ");
            str.Append("ward.WardName as PWardNameNew, ");
            str.Append("r.BedNo as PbedOld, ");
            str.Append("p.BedNo as PBedNew ");
            str.Append("from Patient p ");
            str.Append("left join IVRecord R on r.PatCode = p.PatCode  ");
            str.Append("left join Dward ward on ward.WardCode = p.WardCode ");
            str.Append("where R.LabelNo = '"+label+"' ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 统计当前病区所有瓶签
        /// </summary>
        /// <param name="wards">病区</param>
        /// <param name="date">瓶签日期</param>
        /// <param name="batch">批次</param>
        /// <returns></returns>
        public DataSet CountLabel(string wards, string date, string sql1, string sql3, string batch1,string drugtype1, int LabelKindFlag, int checkModeFlag,string checktable,string checkdt)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            if (checkModeFlag == 1)
            {
                str.Append("select  ");
                str.Append("distinct db.LabelNo as '瓶签号',  ");
                str.Append("db.WardCode as '病区编号',  ");
                str.Append("ward.WardName as '病区',  ");
                str.Append("db.BedNo as '床号',  ");
                str.Append(" P.CaseID as '住院号' ,");
                str.Append("db.PatName as '患者姓名',  ");
                str.Append("db.Batch as '批次',  ");
                //str.Append(" DRUG ");
                str.Append(" DRUGA.DrugName as '主药' ,");
                str.Append(" DRUGB.DrugName as '溶媒' ,");
                str.Append(" (convert(varchar,convert(decimal(18,1),IVRD.Dosage))+convert(varchar,IVRD.DosageUnit)) as '用量',PackAdvance as '提前打包' ");
                //str.Append(",employee.DEmployeeName as '扫描人' ");
                //str.Append(",db." + checkdt + " as '扫描时间' ");
                str.Append(" from ");
                str.Append(" (select ivr.LabelNo ,ivr.IVRecordID,ivr.PrescriptionID,db1.PCode,ivr.WardCode,ivr.MarjorDrug,ivr.Menstruum,ivr.PatName,ivr.BedNo,ivr.Batch,db1." + checkdt + ",ivr.PackAdvance ");
                str.Append(" from IVRecord ivr  ");
                str.Append(" left join IVRecord_Print IVRP on ivr.LabelNo=IVRP.LabelNo ");
                str.Append(" left join Prescription P on ivr.PrescriptionID=p.PrescriptionID ");
                str.Append(" left join " + checktable + " db1 on ivr.LabelNo=db1.IVRecordID where 1=1 and  IVR.LabelOver>=0 and  IVR.WardRetreat = '0' and IVR.IVStatus >='3'");
            }
            else
            {
                str.Append("select  ");
                str.Append("distinct db.LabelNo as '瓶签号',  ");
                str.Append("db.WardCode as '病区编号',  ");
                str.Append("ward.WardName as '病区',  ");
                str.Append("db.BedNo as '床号',  ");
                str.Append(" P.CaseID as '住院号' ,");
                str.Append("db.PatName as '患者姓名',  ");
                str.Append("db.Batch as '批次',  ");
                str.Append(" DRUGA.DrugName as '主药' ,");
                str.Append(" DRUGB.DrugName as '溶媒' ,");
                str.Append(" (convert(varchar,convert(decimal(18,1),IVRD.Dosage))+convert(varchar,IVRD.DosageUnit)) as '用量',PackAdvance as '提前打包' ");
                //str.Append(" ,employee.DEmployeeName as '扫描人'  ");
                //str.Append(" ,db." + checkdt + " as '扫描时间' ");
                str.Append(" from ");
                str.Append(" (select ivr.LabelNo ,ivr.IVRecordID,ivr.PrescriptionID,db1.PCode,ivr.WardCode,ivr.MarjorDrug,ivr.Menstruum,ivr.PatName,ivr.BedNo,ivr.Batch,db1." + checkdt + ",ivr.PackAdvance ");
                str.Append(" from IVRecord ivr  ");
                str.Append(" left join IVRecord_Print IVRP on ivr.LabelNo=IVRP.LabelNo ");
                str.Append(" left join Prescription P on ivr.PrescriptionID=p.PrescriptionID ");
                str.Append(" left join " + checktable + " db1 on ivr.LabelNo=db1.IVRecordID where 1=1 and  IVR.LabelOver>=0 and  IVR.WardRetreat = '0'and IVR.IVStatus >='3'");
            }
            if (LabelKindFlag == 0)//未扫描过的
            {
                str.Append(" and db1.IVRecordID is null ");
            }
            else if (LabelKindFlag == 1)//已经扫描过的
            {
                str.Append(" and   Len(db1.IVRecordID)>0  ");
            }
            else//显示全部瓶签，包括已经扫描和未扫描的
            {
                //str.Append(" ");
            }
            if (sql3 != "" && sql3 != null)
            {
                str.Append(sql3);
            }

            if (checkModeFlag == 1)
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and IVR.WardCode in " + wards + " ");
                }
                else
                {
                    str.Append(" and IVR.WardCode in ('') ");
                }
            }
            else
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and (IVRP.DrugQRCode in " + wards + "  or IVRP.OrderQRCode in " + wards + "  )");
                }
                else
                {
                    str.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                }
            }

            if (date != "")//时间
            {
                str.Append(" and DATEDIFF(DD,InfusionDT,'" + date + "')=0 ");
            }

            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            }

            if (drugtype1 != "" && drugtype1 != null)
            {
                str.Append(" and P.DrugType in " + drugtype1 + "");
            }

            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }
            str.Append(" ) db ");
            
            str.Append("left join DWard ward on ward.WardCode = db.WardCode  ");
            str.Append("left join IVRecordDetail IVRD on IVRD.IVRecordID = db.IVRecordID  and db.MarjorDrug = IVRD.DrugCode ");
            str.Append("left join DDrug DRUGA on db.MarjorDrug = DRUGA.DrugCode   ");
            str.Append("left join DDrug DRUGB on db.Menstruum = DRUGB.DrugCode   ");
            str.Append("left join DEmployee employee on employee.DEmployeeID = db.PCode ");
            str.Append("left join Prescription P on P.PrescriptionID = db.PrescriptionID ");
            
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 查询显示列信息,核对设置表
        /// </summary>
        /// <param name="CheckName"></param>
        /// <returns></returns>
        public DataSet CheckMateId(string CheckName)
        {
            return db.GetPIVAsDB("select * from PivasCheckFormSet where CheckName='" + CheckName + "' ");
        }

        /// <summary>
        /// 统计所有
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet CountAll(string wards, string date, string sql2, string sql3, string batch1,string drugtype1, string record, DataTable countSet, int checkMode)
        {
            StringBuilder str1 = new StringBuilder();
            sql3 += "   and DATEDIFF(DD,InfusionDT,'" + date + "')=0 and  IVR.WardRetreat='0' and IVR.LabelOver>=0  ";
            str1.Append(sql3);

            if (checkMode == 1)
            {
                if (wards != "" && wards != null)//病区
                {
                    str1.Append(" and IVR.WardCode in " + wards + " ");
                }
                else
                {
                    str1.Append(" and IVR.WardCode in ('') ");
                }
            }
            else
            {
                if (wards != "" && wards != null)//病区
                {
                    str1.Append(" and (IVRP.DrugQRCode in " + wards + "  or IVRP.OrderQRCode in " + wards + "  )");
                }
                else
                {
                    str1.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                }

            }
            if (batch1 != "" && batch1 != null)
            {
                str1.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str1.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            }

            if (sql2 == "")
            {
                str1.Append(" ");
            }
            else
            {
                if (sql2 == "LingK")//临时空包
                {
                    str1.Append("  and IVR.Batch like ' %L%' and IVR.Batch like '%K%' ");
                }
                else if (sql2 == "LingA")//临时#包
                {
                    str1.Append("  and IVR.Batch like '%L%' and IVR.Batch not  like '%K%' ");
                }
                else if (sql2 == "LingKA")//所有临时包
                {
                    str1.Append("  and IVR.Batch like '%L%'  ");
                }
                else if (sql2 == "LongK")//长期空包
                {
                    str1.Append("  and IVR.Batch not  like '%L%' and IVR.Batch like '%K%' ");
                }
                else if (sql2 == "LongA")//长期#包
                {
                    str1.Append("  and IVR.Batch not  like '%L%' and IVR.Batch not  like '%K%' ");
                }
                else if (sql2 == "LongKA")//所有长期包
                {
                    str1.Append("  and IVR.Batch not  like '%L%'  ");
                }
                else if (sql2 == "LLK")
                {
                    str1.Append("  and IVR.Batch like '%K%'  ");
                }
                else if (sql2 == "LLA")
                {
                    str1.Append("  and IVR.Batch not like '%K%'  ");
                }
            }

            if (drugtype1 != "" && drugtype1 != null)
            {
                str1.Append(" and P.Drugtype in " + drugtype1 + "");
            }


            sql3 = str1.ToString();
            DataTable dt = countSet;

            int m = 1;
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();

            str.Length = 0;
            str.Append("select ivr.wardName 病区");
            for (int i = 6; i < 12; i++)
            {
                if (dt.Rows.Count > 0)
                {
                    switch (dt.Rows[0][i].ToString())
                    {
                        case "已摆药":
                            str.Append(",isnull(count(yp.LabelNo),0)已摆药");
                            m++;
                            break;
                        case "打印":
                            str.Append(",isnull(c.已打印,0)已打印");
                            m++;
                            break;
                        case "已排药":
                            str.Append(",isnull(count(py.IVRecordID),0)已排药");
                            m++;
                            break;
                        case "已进仓":
                            str.Append(",isnull(count(jc.IVRecordID),0)已进仓");
                            m++;
                            break;
                        case "已配置":
                            str.Append(",isnull(count(pz.IVRecordID),0)已配置");
                            m++;
                            break;
                        case "已出仓":
                            str.Append(",isnull(count(cc.IVRecordID),0)已出仓");
                            m++;
                            break;
                        case "已打包":
                            str.Append(",isnull(count(db.IVRecordID),0)已打包");
                            m++;
                            break;
                        case "总共":
                            str.Append(",COUNT(IVR.WardCode)总数");
                            m++;
                            break;
                        case "未核对":
                            str.Append(",isnull(COUNT(IVR.WardCode)-isnull(count(b.IVRecordID),0),0) 未核对");
                            m++;
                            break;
                        case "空包":
                            str.Append(",isnull(i.空包,0)空包");
                            m++;
                            break;
                        case "临时":
                            str.Append(",isnull(j.临时,0)临时 ");
                            m++;
                            break;
                        case "已退药":
                            str.Append(",isnull(k.已退药,0)已退药 ");
                            m++;
                            break;
                        case "已签收":
                            str.Append(",isnull(count(qs.IVRecordID),0)已签收 ");
                            m++;
                            break;
                        case "提前打包":
                            str.Append(",isnull(h.提前打包,0)提前打包 ");
                            m++;
                            break;
                    }
                }
            }
            str.Append(" from IVRecord ivr");
            str.Append(" left join IVRecord_Print ivrp on ivr.LabelNo=ivrp.LabelNo and IVRP.PrintCount = '0'  ");
            str.Append(" left join Prescription P on ivr.PrescriptionID=P.PrescriptionID  ");
            for (int i = 6; i < 12; i++)
            {
                switch (dt.Rows[0][i].ToString())
                {
                    case "已摆药":
                        str.Append(" left join IVRecod_YP_Check yp on IVR.LabelNo=yp.LabelNo   ");
                        m++;
                        break;
                    case "打印":
                        str.Append(" (select IVR.WardName 全部病区,COUNT(IVR.WardCode)已打印 from IVRecord IVR left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  where 1=1 and IVStatus='3' " + sql3 + "  group by IVR.WardName) as c on ivr.wardname= c.全部病区");
                        m++;
                        break;
                    case "已排药":
                        str.Append("  left join IVRecord_PY py on  IVR.LabelNo=py.IVrecordID and py.ScanCount='0' ");
                        m++;
                        break;
                    case "已出仓":
                        str.Append("  left join IVRecord_CC cc on  IVR.LabelNo=cc.IVrecordID and cc.ScanCount='0' ");
                        m++;
                        break;
                    case "已配置":
                        str.Append("  left join IVRecord_PZ pz on  IVR.LabelNo=pz.IVrecordID  and pz.ScanCount='0' ");
                        m++;
                        break;
                    case "已进仓":
                        str.Append("  left join IVRecord_JC jc on  IVR.LabelNo=jc.IVrecordID  and jc.ScanCount='0' ");
                        m++;
                        break;
                    case "已打包":
                        str.Append("  left join IVRecord_DB db on  IVR.LabelNo=db.IVrecordID and db.ScanCount='0' ");
                        m++;
                        break;
                    case "未核对":
                        str.Append("  left join " + record + " b on  IVR.LabelNo=b.IVrecordID and b.ScanCount='0' ");
                        m++;
                        break;
                    case "空包":
                        str.Append(" left join (select IVR.WardName 全部病区,COUNT(IVR.WardCode)空包 from IVRecord IVR left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo left join Prescription P on ivr.PrescriptionID=P.PrescriptionID   where IVR.Batch like '%K%'  " + sql3 + " Group by IVR.WardName) as i on i.全部病区=ivr.wardname");
                        m++;
                        break;
                    case "临时":
                        str.Append(" left join(select IVR.WardName 全部病区,COUNT(IVR.WardCode)临时 from IVRecord IVR  left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo left join Prescription P on ivr.PrescriptionID=P.PrescriptionID   where IVR.JustOne='1' " + sql3 + " Group by IVR.WardName) as j on j.全部病区=ivr.wardname");
                        m++;
                        break;
                    case "已退药":
                        str.Append(" left join (select distinct IVR.WardName 全部病区,COUNT(IVR.WardCode)已退药 from IVRecord IVR  left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo and PrintCount='0'  where (LabelOver<0 or WardRetreat!=0) and DATEDIFF(DD,InfusionDT,'" + date + "')=0 group by IVR.WardName) as k on ivr.wardname=k.全部病区");
                        m++;
                        break;
                    case "已签收":
                        str.Append(" left join IVRecord_QS qs on  IVR.LabelNo=qs.IVrecordID and qs.ScanCount='0' ");
                        m++;
                        break;
                    case "提前打包":
                        str.Append(" left join (select distinct IVR.WardName 全部病区,COUNT(IVR.WardCode)提前打包 from IVRecord IVR  left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo and PrintCount='0' left join Prescription P on ivr.PrescriptionID=P.PrescriptionID  where IVR.PackAdvance='1' " + sql3 + " Group by IVR.WardName) as h on ivr.wardname=h.全部病区");
                        m++;
                        break;
                }
            }

            str.Append("  where 1=1 and IVR.IVStatus >='3' " + sql3 + "");

            str.Append("  group by ivr.WardName,ivr.WardCode");
            for (int i = 6; i < 12; i++)
            {
                switch (dt.Rows[0][i].ToString())
                {
                    case "打印":
                        str.Append(" ,c.打印 ");
                        m++;
                        break;
                    case "空包":
                        str.Append(" , i.空包 ");
                        m++;
                        break;
                    case "临时":
                        str.Append("  ,j.临时 ");
                        m++;
                        break;
                    case "已退药":
                        str.Append(" ,k .已退药 ");
                        m++;
                        break;
                    case "提前打包":
                        str.Append(" ,h .提前打包 ");
                        m++;
                        break;
                    default: break;
                }
            }
            str.Append("  order by ivr.WardCode");

            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 统计按钮上显示的统计数量总数
        /// </summary>
        /// <returns></returns>
        public DataSet CountShowAll(string wards, string date, string sql1, string sql3, string batch1, int checkModeFlag,string checktable)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select ");
            str.Append("a.总数, ");
            str.Append("isnull(h.已核对,0) as 已核对,(a.总数-已核对) as 未核对 ");
            str.Append("from ");
            str.Append("(select COUNT(IVR.WardCode)总数 from IVRecord IVR  left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  where 1=1  ");

            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            } str.Append("and DATEDIFF(DD,IVR.InfusionDT,'" + date + "')=0  ");
            if (sql3 != null && sql3 != "")
            {
                str.Append(sql3);
            }

            if (checkModeFlag == 1)
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and IVR.WardCode in " + wards + " ");
                }
                else
                {
                    str.Append(" and IVR.WardCode in ('') ");
                }
            }
            else
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and (IVRP.DrugQRCode in " + wards + "  or IVRP.OrderQRCode in " + wards + "  )");
                }
                else
                {
                    str.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                }
            }
            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }
            str.Append("  and IVR.WardRetreat='0' and IVR.LabelOver>=0 and IVRP.PrintCount = '0' )as a ");
            str.Append(",  ");

            //二重
            str.Append("(select COUNT(WardCode)已核对 from IVRecord IVR ");
            str.Append(" inner join " + checktable + " b on IVR.LabelNo=b.IVrecordID ");
            str.Append("  left join IVRecord_Print ivrp on IVR.LabelNo=ivrp.LabelNo ");
            str.Append("where  ScanCount='0' and Invalid is null  ");
            str.Append ("and ivrp.PrintCount='0' ");



            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            } 
            str.Append("and DATEDIFF(DD,InfusionDT,'" + date + "')=0 and IVStatus>='3' and WardRetreat='0' and LabelOver>=0 "); 
            //str.Append("and IVR.LabelNo in (select IVR.LabelNo from IVRecord IVR left join Prescription P on P.PrescriptionID = IVR.PrescriptionID left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo  where 1=1  and DATEDIFF(DD,IVR.InfusionDT,'" + date + "')=0  ");
            if (sql3 != null && sql3 != "")
            {
                str.Append(sql3);
            }
            if (checkModeFlag == 1)
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and IVR.WardCode in " + wards + " ");
                }
                else
                {
                    str.Append(" and IVR.WardCode in ('') ");
                }
            }
            else
            {
                if (wards != "" && wards != null)//病区
                {
                    str.Append(" and (IVRP.DrugQRCode in " + wards + "  or IVRP.OrderQRCode in " + wards + "  )");
                }
                else
                {
                    str.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                }
            }
            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }
            str.Append("   and IVR.WardRetreat='0' and IVR.LabelOver>=0 ) ");
            str.Append(" as h   ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 统计当前的病区有没有扫描完毕
        /// </summary>
        /// <param name="date"></param>
        /// <param name="wards"></param>
        /// <returns></returns>
        public DataSet CountWardsLabels(string date,string wardName,string teamNumber,string checksim)
        {
            string num=string.Empty;
            switch (checksim)
            {
                case "PY": num = "5";
                    break;
                case "JC": num = "7";
                    break;
                case "PZ": num = "9";
                    break;
                case "CC": num = "11";
                    break;
                case "DB": num = "13";
                    break;
            }
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from IVRecord IVR  ");
            //str.Append("where WardCode ='" + wardName + "'  and ((IVR.IVStatus >=3 and IVR.IVStatus <13 and IVR.Batch like '%K%') or (IVR.IVStatus >=9 and IVR.IVStatus <13 and IVR.Batch like '%#%' )) ");
            str.Append("where WardCode ='" + wardName + "'  and  IVR.IVStatus >=3 and IVR.IVStatus <" + num + "  and IVR.LabelOver >=0 ");
            str.Append(" and DATEDIFF(DD,InfusionDT,'" + date + "')=0  ");
            str.Append(" and IVR.TeamNumber = "+teamNumber+" ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 根据编号查询病区
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public DataSet selectWardCodeByLabel(string label)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select  WardCode ,WardName,TeamNumber  from  IVRecord  where  LabelNo = '"+label+"'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        public DataSet selectAllAuthority(string area)
        {
            StringBuilder str = new StringBuilder()
            .Append("select * from IVRecord_Authority where AuthorityArea = '" + area + "'");
            return db.GetPIVAsDB(str.ToString());
        }

        /// <summary>
        /// 显示总瓶签
        /// </summary>
        /// <param name="wards"></param>
        /// <param name="date"></param>
        /// <param name="sql1"></param>
        /// <param name="sql3"></param>
        /// <param name="batch1"></param>
        /// <param name="LabelKindFlag"></param>
        /// <param name="checkModeFlag"></param>
        /// <returns></returns>
        public DataSet selectAllLabelGroups(string date, string sql1, string sql2, string sql3, string batch1, string drugtype1)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            //.Append("select distinct DrugQRCode,OrderQRCode ,PrintDT,PrintCode ,employee.DEmployeeName ")
            //.Append("  from IVRecord_Print IVRP")
            //.Append("  left join DEmployee employee on employee.DEmployeeCode = IVRP.PrintCode ");
            //ds = db.GetPIVAsDB(str.ToString());
            //return ds;

            str.Append("select distinct DrugQRCode,OrderQRCode,PrintDT,PrintCode,PrintName from ");
            str.Append("(select  ");
            str.Append("IVR.LabelNo as '瓶签号',  ");
            str.Append("IVR.WardCode as '病区编号',  ");
            str.Append("IVR.BedNo as '床号',  ");
            str.Append("IVR.PatName as '患者姓名',  ");
            str.Append("IVR.Batch as '批次',  ");
            str.Append("IVRP.DrugQRCode ,IVRP.OrderQRCode,IVRP.PrintDT,IVRP.PrintCode,employee.DEmployeeName as PrintName ");
            str.Append("from IVRecord IVR ");

            str.Append("left join Prescription P on P.PrescriptionID = IVR.PrescriptionID ");
            str.Append("left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo ");
            str.Append("left join DEmployee employee on employee.DEmployeeID = IVRP.PrintCode ");

            str.Append(" where 1=1");
            str.Append(" and  IVR.LabelOver>=0 and  IVR.WardRetreat = '0'  and IVRP.PrintCount = '0'  ");


            if (sql3 != "" && sql3 != null)
            {
                str.Append(sql3);
            }
            //else
            //{
            //    str.Append(" and IVR.WardCode in ('') ");
            //}

            if (date != "")//时间
            {
                str.Append(" and DATEDIFF(DD,InfusionDT,'" + date + "')=0 ");
            }

            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            }

            if (drugtype1 != "" && drugtype1 != null)
            {
                str.Append(" and p.DrugType IN " + drugtype1 + "");
            }

            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }

            str.Append(") as a  where a.DrugQRCode is not null and a.OrderQRCode is not null ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;

        }

        /// <summary>
        /// 扫描单个瓶签
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataSet getIVRecordDetail(string code)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select a.*");
            str.Append("  ,b.BedNo as OBedNo,b.WardCode as OWardeCode  ,b.WardName as OWardName");
            str.Append(" from IVRecordDetail a ");
            str.Append(" left join");
            str.Append(" IVRecord b  on a.IVRecordID=b.IVRecordID");
            str.Append(" where b.LabelNo='" + code + "'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 统计当前登陆扫描数
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="logindate"></param>
        /// <returns></returns>
        public DataSet getCountLabelCheckLogin(string userid, string logindate,string checktable,string checkdt)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select  COUNT(1) as CheckCount  from " + checktable + " YP  left join DEmployee employee  on YP.PCode = employee.DEmployeeID  where 1=1 ");
            str.Append(" and employee.AccountID= '" + userid + "' ");
            str.Append(" and DATEDIFF(SS," + checkdt + ",'" + logindate + "')<30 ");
            str.Append("  ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 扫描是否是第三方瓶签，返回对应labelNo
        /// </summary>
        /// <param name="HisLabelNo"></param>
        public string CheckIsHisLabel(string HisLabelNo)
        {
            string labelno = string.Empty;
            DataSet ds = db.GetPIVAsDB("Select labelNo from IVRecord where Remark6 = '" + HisLabelNo + "'");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                labelno = ds.Tables[0].Rows[0][0].ToString();
            return labelno;
        }

        /// <summary>
        /// 查询步奏ID
        /// </summary>
        /// <param name="checkkind"></param>
        /// <returns></returns>
        public string SelectCheckID(string checkkind)
        {
            StringBuilder str = new StringBuilder();
            str.Append("Select CheckID from PivasCheckFormSet where CheckName = '" + checkkind + "' ");
            DataSet ds = db.GetPIVAsDB(str.ToString());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0][0].ToString();

            return string.Empty;
        }

        public string SelectErrorChargeInfor(string labelno)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct DrugQRCode   from IVRecord_Print where LabelNo ='"+labelno+"'");

            DataSet ds = db.GetPIVAsDB(str.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        public string GetLabelDetailInfo(string labelno,string checktable,string checkdt)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct IVRecordID as '瓶签号',ScanCount as'扫描次序',employee.DEmployeeName as '扫描人',db." + checkdt + " 扫描时间 ");
            str.Append("from "+checktable+" db ");
            str.Append("left join DEmployee employee on employee.DEmployeeID = db.PCode ");
            str.Append("where IVRecordID='" + labelno + "'");

            return str.ToString();
            
        }

        /// <summary>
        /// 获取总瓶签下瓶签集
        /// </summary>
        /// <param name="wards">总瓶签号；病区号</param>
        /// <param name="date"></param>
        /// <param name="sql1"></param>
        /// <param name="sql3"></param>
        /// <param name="batch1"></param>
        /// <param name="CheckMode">1：总瓶签模式</param>
        /// <param name="checktable"></param>
        /// <returns></returns>
        public DataSet LabelNoCollection(string wards, string date, string sql1, string sql3, string batch1, int CheckMode, string checktable)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("    select  ");
            str.Append("    IVR.LabelNo as '瓶签号',isnull(db.ScanCount+1,0) as 扫描次数  ");
            str.Append("    from IVRecord IVR ");
            str.Append("    left join " + checktable + " db on db.IVRecordID = IVR.LabelNo  ");
            str.Append("    left join Prescription P on P.PrescriptionID = IVR.PrescriptionID ");
            str.Append("    left join IVRecord_Print IVRP on IVRP.LabelNo = IVR.LabelNo ");
            str.Append("    where 1=1");
            str.Append("    and  IVR.LabelOver>=0 ");

            if (sql3 != "" && sql3 != null)
            {
                str.Append(sql3);
            }

            if (CheckMode == 1)//总瓶签模式
            {
                if (wards != "" && wards != null)//总瓶签号
                {
                    str.Append(" and (IVRP.DrugQRCode = '" + wards + "'  or IVRP.OrderQRCode = '" + wards + "'  )");
                }
                else
                {
                    str.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                }
            }

            if (date != "")//时间
            {
                str.Append(" and DATEDIFF(DD,InfusionDT,'" + date + "')=0 ");
            }

            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            }

            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }

            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
        public DataSet AllLabel(string wards, string date, string sql1, string sql3, string batch1,string drugtype1, int LabelKindFlag, int checkModeFlag, string checktable, string checkdt)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            if (checkModeFlag == 1)
            {
                str.Append(" select ivr.LabelNo as 瓶签号,ivr.Batch as 批次,ivr.PatName as 病人,ivr.WardName as 病区,ivr.BedNo as 床号,ivr.MarjorDrug as 主药,dc.DrugColor as 主药颜色,ivr.Menstruum as 溶媒,dc1.DrugColor as 溶媒颜色,ivr.IVStatus,ivr.LabelOver,ivr.WardRetreat ,case ivr.PackAdvance when 1 then '提前打包' else '否' end as 提前打包,dw.WardName as 新病区 ,p.BedNo as 新床号,tb." + checkdt + " as 记录时间 ");
                str.Append(" from IVRecord ivr  ");
                str.Append(" left join IVRecord_Print IVRP on ivr.LabelNo=ivrp.LabelNo  ");
                str.Append(" left join Patient p on p.PatCode=ivr.PatCode  ");
                str.Append(" left join DWard dw on dw .WardCode=p.WardCode ");
                str.Append(" left join Prescription Pr on Pr.PrescriptionID = IVR.PrescriptionID   ");
                str.Append(" left join " + checktable + " tb on tb.IVRecordID=ivr.LabelNo and tb.ScanCount = '0' ");
                str.Append(" left join DDrugColor dc on dc.DrugCode=ivr.MarjorDrug ");
                str.Append(" left join DDrugColor dc1 on dc1.DrugCode=ivr.Menstruum ");
                str.Append("  where 1=1  and IVRP.PrintCount='0' ");
            }
            else
            {
                str.Append(" select ivr.LabelNo as 瓶签号,ivr.Batch as 批次,ivr.PatName as 病人,ivr.WardName as 病区,ivr.BedNo as 床号,ivr.MarjorDrug as 主药,dc.DrugColor as 主药颜色,ivr.Menstruum as 溶媒,dc1.DrugColor as 溶媒颜色,ivr.IVStatus,ivr.LabelOver,ivr.WardRetreat ,case ivr.PackAdvance when 1 then '提前打包' else '否' end as 提前打包,dw.WardName as 新病区 ,p.BedNo as 新床号,tb." + checkdt + " as 记录时间 ");
                str.Append(" from IVRecord ivr  ");
                str.Append(" left join IVRecord_Print IVRP on ivr.LabelNo=ivrp.LabelNo  ");
                str.Append(" left join Patient p on p.PatCode=ivr.PatCode  ");
                str.Append(" left join DWard dw on dw .WardCode=p.WardCode ");
                str.Append(" left join Prescription Pr on Pr.PrescriptionID = IVR.PrescriptionID   ");
                str.Append(" left join " + checktable + " tb on tb.IVRecordID = ivr.LabelNo and tb.ScanCount = '0' ");
                str.Append(" left join DDrugColor dc on dc.DrugCode=ivr.MarjorDrug ");
                str.Append(" left join DDrugColor dc1 on dc1.DrugCode=ivr.Menstruum ");
                str.Append("  where 1=1  and IVRP.PrintCount='0' ");
            }
            if (sql3 != "" && sql3 != null)
            {
                str.Append(sql3);
            }

            if (wards != "" && wards != string.Empty)
            {
                if (checkModeFlag == 1)
                {
                    if (wards != "" && wards != null)//病区
                    {
                        str.Append(" and IVR.WardCode in " + wards + " ");
                    }
                    else
                    {
                        str.Append(" and IVR.WardCode in ('') ");
                    }
                }
                else
                {
                    if (wards != "" && wards != null)//病区
                    {
                        str.Append(" and (IVRP.DrugQRCode in " + wards + "  or IVRP.OrderQRCode in " + wards + "  )");
                    }
                    else
                    {
                        str.Append(" and IVRP.DrugQRCode in ('') or  IVRP.OrderQRCode in ('')  ");
                    }
                }
            }

            if (date != "")//时间
            {
                str.Append(" and DATEDIFF(DD,InfusionDT,'" + date + "')=0 ");
            }

            if (batch1 != "" && batch1 != null)
            {
                str.Append(" and IVR.TeamNumber in (" + batch1 + ") ");
            }
            else
            {
                str.Append(" and IVR.TeamNumber in ('0') ");//批次条件为空的情况
            }

            if (drugtype1 != "" && drugtype1 != null)
            {
                str.Append(" and Pr.DrugType IN " + drugtype1 + "");
            }

            if (sql1 != "")
            {
                if (sql1 == "all")
                {
                    str.Append(" ");
                }
                else
                {
                    str.Append(sql1);
                }
            }
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
    }
}
