using System.Data;
using System.Text;
using System;
using System.Windows.Forms;

namespace EDA
{
    class selDB
    {
        private DataBase  DB = new DataBase();
        
        public DataSet getIVRecordDetail(string code) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select a.*");
            str.Append("  ,b.BedNo as OBedNo,b.WardCode as OWardeCode  ,b.WardName as OWardName");
            str.Append(" from V_IVRecordandDetail a ");
            str.Append(" left join");
            str.Append(" (select pt.patcode,pt.WardCode,PT.BedNo,wardname from Patient PT,DWard D where PT.WardCode=D.WardCode)b on a.PatCode=b.PatCode");
            str.Append(" where LabelNo='" + code + "'");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
        public DataSet getCheckFormSet() 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from PivasCheckFormSet");
            str.Append(" where Show='1'");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
        public DataSet getCheckOpen()
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from PivasCheckFormSet");
            str.Append(" where IsOpen='1'");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }

        public DataSet count(string record,string CheckName,string DWard,string WardArea,string SelectRule,string Status)
        {
            try
            {
                int m = 0;
                DataSet ds = new DataSet();
                StringBuilder str = new StringBuilder();
                DataTable dt = CheckMateId(CheckName).Tables[0];
                str.Length = 0;
                str.Append("select a.全部病区");
                if (ds != null &&ds.Tables.Count>0&& ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 6; i < 12; i++)
                    {
                        if (dt.Rows[0][i].ToString() == "已核对")
                        {
                            str.Append(",isnull(b.已核对,0)已核对");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "打印")
                        {
                            str.Append(",isnull(c.已打印,0)已打印");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已排药")
                        {
                            str.Append(",isnull(d.已排药,0)已排药");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已进仓")
                        {
                            str.Append(",isnull(e.已进仓,0)已进仓");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已配置")
                        {
                            str.Append(",isnull(f.已配置,0)已配置");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已出仓")
                        {
                            str.Append(",isnull(g.已出仓,0)已出仓");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已打包")
                        {
                            str.Append(",isnull(h.已打包,0)已打包");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "总共")
                        {
                            str.Append(",a.总数");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "未核对")
                        {
                            str.Append(",isnull(n.未核对,0) 未核对");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "空包")
                        {
                            str.Append(",isnull(i.空包,0)空包");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "临时")
                        {
                            str.Append(",isnull(j.临时,0)临时 ");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已退药")
                        {
                            str.Append(",isnull(k.已退药,0)已退药 ");
                            m++;
                        }
                        else if (dt.Rows[0][i].ToString() == "已签收")
                        {
                            str.Append(",isnull(m.已签收,0)已签收 ");
                            m++;
                        }

                    }
                }
                for (int i = 0; i < 6 - m; i++)
                {
                    str.Append(",null");
                }
                str.Append(" from (select WardName 全部病区,COUNT(WardCode)总数,WardCode from IVRecord where 1=1 ");
                str.Append(" " + SelectRule + " ");
                if (DWard != "")
                    str.Append(" and WardCode<>'" + DWard + "'");
                if (WardArea != "")
                    str.Append(" and  WardCode not in (select WardCode from DWard where WardArea='" + WardArea + "') ");
                if (Status != "")
                {
                    if (Status == "已核对")
                        str.Append(" and LabelNo in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                    else if (Status == "未核对")
                        str.Append(" and LabelNo not in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                    else if (Status == "已退药")
                        str.Append(" and (WardRetreat=1 or LabelOver<0) ");
                }
                str.Append("group by WardName,WardCode)a");
                str.Append("  left join");
                str.Append(" (select WardName 全部病区,COUNT(WardCode)已核对 from IVRecord a," + record + " b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + "  group by WardName)b ");
                str.Append("  on a.全部病区=b.全部病区");
                str.Append(" left join  ");
                str.Append(" (select WardName 全部病区,COUNT(WardCode)未核对 from IVRecord a where 1=1 " + SelectRule + "");
                str.Append(" and LabelNo not in(select IVRecordID from " + record + " where  ScanCount='0' and Invalid is null " + SelectRule + ")group by WardName) n");
                str.Append(" on a.全部病区=n.全部病区");
                str.Append(" left join ");
                str.Append(" (select WardName 全部病区,COUNT(WardCode)已打印 from IVRecord where IVStatus='3' " + SelectRule + "  group by WardName)c on a.全部病区= c.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已排药 from IVRecord a,IVRecord_PY b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)d ");
                str.Append("  on a.全部病区=d.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已进仓 from IVRecord a,IVRecord_JC b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)e ");
                str.Append("  on a.全部病区=e.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已配置 from IVRecord a,IVRecord_PZ b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + "  group by WardName)f ");
                str.Append("  on a.全部病区=f.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已出仓 from IVRecord a,IVRecord_CC b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + "  group by WardName)g ");
                str.Append("  on a.全部病区=g.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已打包 from IVRecord a,IVRecord_DB b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)h ");
                str.Append("  on a.全部病区=h.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已签收 from IVRecord a,IVRecord_QS b ");
                str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)m ");
                str.Append("  on a.全部病区=m.全部病区");
                str.Append(" left join (select WardName 全部病区,COUNT(WardCode)空包 from IVRecord  where Batch like '%K%'  " + SelectRule + " Group by WardName)i on i.全部病区=a.全部病区");
                str.Append(" left join(select WardName 全部病区,COUNT(WardCode)临时 from IVRecord  where JustOne='1' " + SelectRule + " Group by WardName)j on j.全部病区=a.全部病区");
                str.Append(" left join (select WardName 全部病区,COUNT(WardCode)已退药 from IVRecord where WardRetreat=1 " + SelectRule + " or LabelOver<0  " + SelectRule + " group by WardName)k on a.全部病区=k.全部病区");
                str.Append(" order by a.WardCode");
                ds = DB.GetDataset(str.ToString());
                return ds;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        
        public DataSet getDWard(string LabelNo) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select WardCode from IVRecord");
            str.Append(" where LabelNo='"+LabelNo+"'");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
        public DataSet getWardArea(string LabelNo) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select WardArea from DWard a ,IVRecord b");
            str.Append(" where LabelNo='" + LabelNo + "'and a.WardCode=b.WardCode");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
        
        public DataSet countid(string Dward, string record, string LastCheckName,string CheckName,string SelectRule,string Status)
        {
            int m = 0;
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            DataTable dt = CheckMateId(CheckName).Tables[0];
            str.Length = 0;
            str.Append("select a.锁定病区");
            for (int i = 6; i < 12; i++)
            {
                if (dt.Rows[0][i].ToString() == "已核对")
                {
                    str.Append(",isnull(b.已核对,0)已核对");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "打印")
                {
                    str.Append(",isnull(c.已打印,0)已打印");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已排药")
                {
                    str.Append(",isnull(d.已排药,0)已排药");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已进仓")
                {
                    str.Append(",isnull(e.已进仓,0)已进仓");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已配置")
                {
                    str.Append(",isnull(f.已配置,0)已配置");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已出仓")
                {
                    str.Append(",isnull(g.已出仓,0)已出仓");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已打包")
                {
                    str.Append(",isnull(h.已打包,0)已打包");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "总共")
                {
                    str.Append(",a.总数");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "未核对")
                {
                    str.Append(",isnull(n.未核对,0) 未核对");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "空包")
                {
                    str.Append(",isnull(i.空包,0)空包");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "临时")
                {
                    str.Append(",isnull(j.临时,0)临时 ");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已退药")
                {
                    str.Append(",isnull(k.已退药,0)已退药 ");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已签收")
                {
                    str.Append(",isnull(m.已签收,0)已签收 ");
                    m++;
                }

            }
            for (int i = 0; i < 6 - m; i++)
            {
                str.Append(",null");
            }
            str.Append("  from(select WardName 锁定病区,COUNT(WardCode)总数,WardCode"); 
            str.Append("  from IVRecord where WardCode='"+Dward+"'");
            str.Append("  "+SelectRule+"");
            if (Status != "")
            {
                if (Status == "已核对")
                    str.Append(" and LabelNo in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                else if (Status == "未核对")
                    str.Append(" and LabelNo not in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                else if (Status == "已退药")
                    str.Append(" and (WardRetreat=1 or LabelOver<0) ");
            }
            str.Append("  group by WardName,WardCode)a left join");
            str.Append(" (select WardName 锁定病区,COUNT(WardCode)已核对 from IVRecord a,"+record+" b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0'and WardCode='"+Dward+"' ");
            str.Append(" and Invalid is null " + SelectRule + " group by WardName)b ");
            str.Append(" on a.锁定病区=b.锁定病区");
            str.Append(" left join  ");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)未核对 from IVRecord a where 1=1 " + SelectRule + "");
            str.Append(" and LabelNo not in(select IVRecordID from " + record + " where  ScanCount='0' and Invalid is null " + SelectRule + ")group by WardName) n");
            str.Append(" on a.锁定病区=n.全部病区");
            str.Append(" left join ");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)已打印 from IVRecord where IVStatus='1' " + SelectRule + " group by WardName)c on a.锁定病区= c.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已排药 from IVRecord a,IVRecord_PY b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)d ");
            str.Append("  on a.锁定病区=d.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已进仓 from IVRecord a,IVRecord_JC b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)e ");
            str.Append("  on a.锁定病区=e.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已配置 from IVRecord a,IVRecord_PZ b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + "  group by WardName)f ");
            str.Append("  on a.锁定病区=f.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已出仓 from IVRecord a,IVRecord_CC b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)g ");
            str.Append("  on a.锁定病区=g.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已打包 from IVRecord a,IVRecord_DB b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)h ");
            str.Append("  on a.锁定病区=h.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已签收 from IVRecord a,IVRecord_QS b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)m ");
            str.Append("  on a.锁定病区=m.全部病区");
            str.Append(" left join (select WardName 全部病区,COUNT(WardCode)空包 from IVRecord  where Batch like '%K%' " + SelectRule + " Group by WardName)i on i.全部病区=a.锁定病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)临时 from IVRecord  where JustOne='1' " + SelectRule + "  Group by WardName)j on j.全部病区=a.锁定病区");
            str.Append(" left join (select WardName 全部病区,COUNT(WardCode)已退药 from IVRecord where WardRetreat=1 " + SelectRule + " or LabelOver<0 " + SelectRule + " group by WardName)k on a.锁定病区=k.全部病区");
            str.Append(" order by a.WardCode");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }


        public DataSet countID(string WardArea, string record, string LastCheckName, string CheckName, string SelectRule, string Status)
        {
            int m = 0;
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            DataTable dt = CheckMateId(CheckName).Tables[0];
            str.Length = 0;
            str.Append("select a.锁定病区");
            for (int i = 6; i < 12; i++)
            {
                if(dt.Rows[0][i].ToString() == "已核对")
                {
                    str.Append(",isnull(b.已核对,0)已核对");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "打印")
                {
                    str.Append(",isnull(c.已打印,0)已打印");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已排药")
                {
                    str.Append(",isnull(d.已排药,0)已排药");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已进仓")
                {
                    str.Append(",isnull(e.已进仓,0)已进仓");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已配置")
                {
                    str.Append(",isnull(f.已配置,0)已配置");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已出仓")
                {
                    str.Append(",isnull(g.已出仓,0)已出仓");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已打包")
                {
                    str.Append(",isnull(h.已打包,0)已打包");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "总共")
                {
                    str.Append(",a.总数");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "未核对")
                {
                    str.Append(",isnull(n.未核对,0) 未核对");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "空包")
                {
                    str.Append(",isnull(i.空包,0)空包");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "临时")
                {
                    str.Append(",isnull(j.临时,0)临时 ");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已退药")
                {
                    str.Append(",isnull(k.已退药,0)已退药 ");
                    m++;
                }
                else if (dt.Rows[0][i].ToString() == "已签收")
                {
                    str.Append(",isnull(m.已签收,0)已签收 ");
                    m++;
                }

            }
            for (int i = 0; i < 6 - m; i++)
            {
                str.Append(",null");
            }
            str.Append("  ");
            str.Append(" from ( select WardName 锁定病区,COUNT(WardCode)总数,WardCode  from IVRecord  where WardCode in  "); 
            str.Append(" (select WardCode from DWard where WardArea='"+WardArea+"')    ");
            str.Append(" "+SelectRule+" ");
            if (Status != "")
            {
                if (Status == "已核对")
                    str.Append(" and LabelNo in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                else if (Status == "未核对")
                    str.Append(" and LabelNo not in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                else if (Status == "已退药")
                    str.Append(" and (WardRetreat=1 or LabelOver<0)");
            }
            str.Append(" group by WardName,WardCode  )a");
            str.Append(" left join(select WardName 锁定病区,COUNT(WardCode)已核对 from IVRecord a," + record + " b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0'and  Invalid is null " + SelectRule + " and WardCode in");
            str.Append(" (select WardCode from DWard where WardArea='"+WardArea+"')  group by WardName)b   ");
            str.Append(" on a.锁定病区=b.锁定病区");
            str.Append(" left join  ");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)未核对 from IVRecord a where 1=1 " + SelectRule + "");
            str.Append(" and LabelNo not in(select IVRecordID from " + record + " where  ScanCount='0' and Invalid is null " + SelectRule + ")group by WardName) n");
            str.Append(" on a.锁定病区=n.全部病区");
            str.Append(" left join ");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)已打印 from IVRecord where IVStatus='1' " + SelectRule + "  group by WardName)c on a.锁定病区= c.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已排药 from IVRecord a,IVRecord_PY b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)d ");
            str.Append("  on a.锁定病区=d.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已进仓 from IVRecord a,IVRecord_JC b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)e ");
            str.Append("  on a.锁定病区=e.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已配置 from IVRecord a,IVRecord_PZ b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)f ");
            str.Append("  on a.锁定病区=f.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已出仓 from IVRecord a,IVRecord_CC b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)g ");
            str.Append("  on a.锁定病区=g.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已打包 from IVRecord a,IVRecord_DB b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)h ");
            str.Append("  on a.锁定病区=h.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已签收 from IVRecord a,IVRecord_QS b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)m ");
            str.Append("  on a.锁定病区=m.全部病区");
            str.Append(" left join (select WardName 全部病区,COUNT(WardCode)空包 from IVRecord  where Batch like '%K%' " + SelectRule + " Group by WardName)i on i.全部病区=a.锁定病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)临时 from IVRecord  where JustOne='1' " + SelectRule + " Group by WardName)j on j.全部病区=a.锁定病区");
            str.Append(" left join (select WardName 全部病区,COUNT(WardCode)已退药 from IVRecord where WardRetreat=1 " + SelectRule + " or LabelOver<0 " + SelectRule + " group by WardName)k on a.锁定病区=k.全部病区");
            str.Append(" order by a.WardCode");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }

        public DataSet CheckMate() 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from PivasCheckFormSet");
            str.Append(" where Show='1' and IsOpen='1' and Vesting='1' ");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
        public DataSet CheckMateId(string CheckName) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from PivasCheckFormSet");
            str.Append(" where CheckName='"+CheckName+"' ");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
        public void updateCheckFormSet(bool isopen, string checkname)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("update  PivasCheckFormSet set IsOpen='" + isopen + "'");
            str.Append(" where CheckName='" + checkname + "'");
            DB.ExecSql(str.ToString());

        }
        public void updateCombox(string checkname, string content1, string content2, string content3, string content4,string content5,string content6, string color1, string color2, string color3, string color4, string color5,string color6,string NextDay) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("update  PivasCheckFormSet set Content1='" + content1 + "'");
            str.Append(" ,Content2='"+content2+"'");
            str.Append(" ,Content3='" + content3 + "'");
            str.Append(" ,Content4='" + content4 + "'");
            str.Append(" ,Content5='" + content5 + "'");
            str.Append(" ,Content6='" + content6 + "'");
            str.Append(" ,Color1='" + color1 + "'");
            str.Append(" ,Color2='" + color2 + "'");
            str.Append(" ,Color3='" + color3 + "'");
            str.Append(" ,Color4='" + color4 + "'");
            str.Append(" ,Color5='" + color5 + "'");
            str.Append(" ,Color6='" + color6 + "'");
            str.Append(" ,NextDay='"+NextDay+"'");
            str.Append(" where CheckName='" + checkname + "'");
            DB.ExecSql(str.ToString());
        }

        public  DataSet IVRecord(string record,string type,string DWard,string Date,string Batch,string Sx,string Status) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select a.LabelNo,a.WardName ,a.BedNo,a.PatName ,a.Batch,b.Pcode,b.time,a.WardRetreat,LabelOver from IVRecord a");  
            str.Append(" left join ");
            str.Append(" (select IVRecordID," + type + " time ,b.DEmployeeName as Pcode from " + record + " a,DEmployee b where ScanCount='0' and a.PCode=b.DEmployeeID  )b");
            str.Append("  on a.LabelNo=b.IVRecordID");
            str.Append(" where 1=1");
            if (DWard != "")
                str.Append(" and WardCode='" + DWard + "' ");
            if (Date != "")
                str.Append(" and DATEDIFF(DD,InfusionDT,'"+Date+"')=0 ");
            if (Sx != "")
                str.Append(" and Batch like '%" + Sx + "%'");
            if (Batch != "")
            {
                Batch = Batch.Replace("长期:", "");
                Batch = Batch.Replace("#", "");
                str.Append("and Batch like '%" + Batch + "%' ");
            }
            if (Status != "") 
            {
                if (Status == "已核对")
                    str.Append(" and LabelNo in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                else if (Status == "未核对")
                    str.Append(" and LabelNo not in (select IVRecordID from " + record + " where ScanCount='0' and Invalid is null)");
                else if (Status == "已退药")
                    str.Append(" and (WardRetreat=1 or LabelOver<0) ");
            }
            ds = DB.GetDataset(str.ToString());
            return ds;
        }

        public DataSet IVRecordPrint(string Code,string Select) 
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct IVRecord_Print.LabelNo from IVRecord_Print");
            str.Append(" inner join IVRecord on IVRecord_Print.LabelNo=IVRecord.LabelNo");
            str.Append("  where (DrugQRCode='" + Code + "'or OrderQRCode='" + Code + "')");
            str.Append(" "+ Select+" ");
            ds = DB.GetDataset(str.ToString());
            return ds;
        }
       
    }
}
