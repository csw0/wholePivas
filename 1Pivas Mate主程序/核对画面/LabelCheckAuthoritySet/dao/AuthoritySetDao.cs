using System;
using System.Text;
using System.Data;
using LabelCheckAuthoritySet.entity;
using PIVAsCommon.Helper;

namespace LabelCheckAuthoritySet.dao
{
    public class AuthoritySetDao
    {
        #region 属性
        private DB_Help db = new DB_Help();
        #endregion

        #region 方法
        /// <summary>
        /// 加载频次下拉列表框的数据库操作
        /// </summary>
        public string LoadComboxBatchDao()
        {
            StringBuilder str = new StringBuilder()
            .Append(" select OrderID from DOrder order by OrderID ASC ");
            return str.ToString();
        }

        /// <summary>
        /// 获取下一个主键的sql
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        private int GetNextSEQNO(string TableName)
        {
            int seqno = 0;
            string sql = "select SEQNO from " + TableName + " order by SEQNO desc ";
            DataSet ds = new DataSet();
            ds = db.GetPIVAsDB(sql);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    seqno = 0;
                }
                else
                {
                    seqno = int.Parse(ds.Tables[0].Rows[0]["SEQNO"].ToString()) + 1;
                }
            }
            return seqno;
        }

        /// <summary>
        /// 保存主要权限的数据库操作
        /// </summary>
        /// <param name="name">权限名字</param>
        /// <param name="area">权限作用域</param>
        /// <param name="level">权限等级</param>
        public int SaveSetMain(string name, string area, string level)
        {
            int SEQNO = GetNextSEQNO("IVRecord_Authority");
            StringBuilder str = new StringBuilder()
            .Append(" insert into IVRecord_Authority(SEQNO,AuthorityName,AuthorityArea,AuthorityLevel) ")
            .Append(" values("+SEQNO+",'"+name+"','"+area+"','"+level+"')");
            int i = db.SetPIVAsDB(str.ToString());
            return i;
        }

        /// <summary>
        /// 修改主要权限的数据库操作
        /// </summary>
        /// <param name="seqno"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public int UpdateSetMain(int seqno, string name, string area, string level)
        {
            StringBuilder str = new StringBuilder()
            .Append(" update IVRecord_Authority set AuthorityName = '" + name + "' ,")
            .Append(" AuthorityArea='" + area + "', ")
            .Append(" AuthorityLevel='" + level + "' ")
            .Append(" where SEQNO = "+seqno+"");
            int i = db.SetPIVAsDB(str.ToString());
            return i;
        }

       /// <summary>
        /// 按照作用域查询权限
       /// </summary>
       /// <param name="area"></param>
       /// <param name="isall"></param>
       /// <returns></returns>
        public DataSet SelSetMainByArea(string area,string isall)
        {
            StringBuilder str = new StringBuilder()
            .Append(" select * from IVRecord_Authority where AuthorityArea = '" + area + "' ");
            if (isall != "")
            {
                str.Append(" and AuthorityName = 'All' ");
            }
            DataSet ds = new DataSet();
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        /// <summary>
        /// 保存其他配置信息
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public int SaveSetOther(Authority auth)
        {
            int SEQNO = GetNextSEQNO("IVRecord_Authority");
            StringBuilder str = new StringBuilder()
            .Append(" insert into IVRecord_Authority(SEQNO,AuthorityName,AuthorityArea,AuthorityLevel)  ")
            .Append(" values("+SEQNO+",'"+auth.AuthorityName+"','"+auth.AuthorityArea+"','"+auth.AUthorityLevel+"')");
            int i = db.SetPIVAsDB(str.ToString());
            return i;
        }

        /// <summary>
        /// 删除其他配置信息
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public int DeleteSetOther(Authority auth)
        {
            StringBuilder str = new StringBuilder()
            .Append(" delete from IVRecord_Authority where SEQNO = "+auth.SeqNo+"");
            int i = db.SetPIVAsDB(str.ToString());
            return i;
        }

        /// <summary>
        /// 修改其他配置信息
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public int UpdateSetOther(Authority auth)
        {
            StringBuilder str = new StringBuilder()
            .Append(" update IVRecord_Authority set AuthorityName = '" + auth.AuthorityName + "',")
            .Append(" AuthorityArea = '" + auth.AuthorityArea + "', ")
            .Append(" AuthorityLevel = '" + auth.AUthorityLevel + "' ")
            .Append(" where SEQNO = "+auth.SeqNo+"");
            int i = db.SetPIVAsDB(str.ToString());
            return i;
        }

        /// <summary>
        /// 更改InI文件
        /// </summary>
        public void SetInIFile()
        {
            
        }

        /// <summary>
        /// 读取InI文件
        /// </summary>
        public string ReadInIFile(string node,string nodeType)
        {
            string nodeValue = "";
            try
            {
                nodeValue = db.IniReadValuePivas(node, nodeType);
            }
            catch (Exception )
            {
                throw;
            }
            return nodeValue;
        }
        #endregion


        #region 其他配置信息（颜色，列数等）
        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="checkname"></param>
        /// <param name="content1"></param>
        /// <param name="content2"></param>
        /// <param name="content3"></param>
        /// <param name="content4"></param>
        /// <param name="content5"></param>
        /// <param name="content6"></param>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <param name="color3"></param>
        /// <param name="color4"></param>
        /// <param name="color5"></param>
        /// <param name="color6"></param>
        /// <param name="NextDay"></param>
        public bool updateCombox(string checkname, string content1, string content2, string content3, string content4, string content5, string content6, string color1, string color2, string color3, string color4, string color5, string color6, string NextDay, string WaitTime, string FreshTime)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("update  PivasCheckFormSet set Content1='" + content1 + "'");
            str.Append(" ,Content2='" + content2 + "'");
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
            str.Append(" ,NextDay='" + NextDay + "'");
            str.Append(" ,WaitTime='" + WaitTime + "'");
            str.Append(" ,FreshTime='" + FreshTime + "'");
            str.Append(" where CheckName='" + checkname + "'");
            int i = db.SetPIVAsDB(str.ToString());
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <returns></returns>
        public DataSet getCheckFormSet(string checkName)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from PivasCheckFormSet");
            str.Append(" where Show='1'   and CheckName = '"+checkName+"'");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }
        #endregion


    }
}
