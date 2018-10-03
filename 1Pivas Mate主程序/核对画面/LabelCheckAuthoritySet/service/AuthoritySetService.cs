using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using LabelCheckAuthoritySet.dao;
using LabelCheckAuthoritySet.entity;
using PIVAsCommon.Helper;

namespace LabelCheckAuthoritySet.service
{
    public class AuthoritySetService
    {
        #region 属性
        private AuthoritySetDao dao = new AuthoritySetDao();
        private DataSet ds = null;
        private DB_Help db = new DB_Help();
        #endregion

        /// <summary>
        /// 加载批次下拉框
        /// </summary>
        /// <returns></returns>
        public  DataSet LoadComboxBatch()
        {
            try
            {
                ds = db.GetPIVAsDB(dao.LoadComboxBatchDao());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            return ds;
        }

        /// <summary>
        /// 新增主要权限的服务(首次)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="level"></param>
        public bool SetMainService(string name,string area,string level)
        {
            int i = dao.SaveSetMain(name,area,level);
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
        /// 修改主要权限的服务
        /// </summary>
        /// <param name="seqno"></param>
        /// <param name="name"></param>
        /// <param name="area"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool UpdateMainService(int seqno, string name, string area, string level)
        {
            int i = dao.UpdateSetMain(seqno,name,area,level);
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
        /// 查询当前页的主要权限
        /// </summary>
        /// <param name="area"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public Authority FindMainAuthorityByArea(string area,string all)
        {
            Authority auth = new Authority();
            DataSet ds = new DataSet();
            ds = dao.SelSetMainByArea(area,all);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                auth.SeqNo = int.Parse(ds.Tables[0].Rows[0]["SEQNO"].ToString());
                auth.AuthorityName = ds.Tables[0].Rows[0]["AuthorityName"].ToString();
                auth.AuthorityArea = ds.Tables[0].Rows[0]["AuthorityArea"].ToString();
                auth.AUthorityLevel = ds.Tables[0].Rows[0]["AuthorityLevel"].ToString();
            }
            return auth;
        }

        /// <summary>
        /// 查询当前页面的所有权限
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public List<Authority> FindAllAuthorityByArea(string area)
        {
            List<Authority> authList = new List<Authority>();
            DataSet ds = new DataSet();
            ds = dao.SelSetMainByArea(area, "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Authority auth = null;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    auth = new Authority();
                    auth.SeqNo = int.Parse(ds.Tables[0].Rows[i]["SEQNO"].ToString());
                    auth.AuthorityName = ds.Tables[0].Rows[i]["AuthorityName"].ToString();
                    auth.AuthorityArea = ds.Tables[0].Rows[i]["AuthorityArea"].ToString();
                    auth.AUthorityLevel = ds.Tables[0].Rows[i]["AuthorityLevel"].ToString();
                    authList.Add(auth);
                }
            }
            return authList;
        }

        /// <summary>
        /// 保存其他配置信息
        /// </summary>
        /// <param name="auth">需要保存的配置对象</param>
        /// <returns></returns>
        public bool SetOtherService(Authority auth)
        {
            int i = dao.SaveSetOther(auth);
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
        /// 删除其他配置信息
        /// </summary>
        /// <param name="auth">需要删除的配置对象</param>
        /// <returns></returns>
        public bool DeleteOtherService(Authority auth)
        {
            int i = dao.DeleteSetOther(auth);
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
        /// 修改其他配置信息
        /// </summary>
        /// <param name="auth">需要修改的配置对象</param>
        /// <returns></returns>
        public bool UpdateOtherService(Authority auth)
        {
            int i = dao.UpdateSetOther(auth);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ReadInIFile(string node,string nodename)
        {
            string nodeValue = dao.ReadInIFile(node,nodename);
            return nodeValue;
        }
    }
}
