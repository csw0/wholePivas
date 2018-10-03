using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using PIVAsDBhelp;
using System.Xml;
using System.IO;

namespace EDAWebServices
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]

    public class Service1 : System.Web.Services.WebService
    {
        //属性
        #region


        XmlDocument xml = new XmlDocument();
        DB_Help db = new DB_Help();
        EDAWebServices.seldb sel = new seldb();
        protected static string conn = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
        SqlConnection con = new SqlConnection(conn);
        public string _IsCharge = string.Empty;
        public string _OnlyToday = string.Empty;


        #endregion


        //接口
        #region
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="Employeid">员工号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [WebMethod(Description = "登陆")]
        public string Login(string Employeid, string pwd)
        {
            string a = string.Empty;
            if (TestDB())
            {
                if (Employeid == "")
                {
                    //a = "请输入登录用户名";
                    a = "[{\"reasult\": \"0\",\"DEmployeeID\": \"\"}]";
                }
                else if(Employeid.Substring (0,4)=="7777"&&Employeid .Length>=22)//扫描的是二维码
                {
                    string sql = "  select DEmployeeID,DelDT from QRcodeLog where QRcode ='" + Employeid + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["DelDT"].ToString() == "" || ds.Tables[0].Rows[0]["DelDT"].ToString() == string.Empty)//删除时间不为空，账号不存在
                        {
                            a = "[{\"reasult\": \"4\",\"DEmployeeID\": \"\"}]";
                            return a;
                        }
                        else//账号存在
                        {
                            a = "[{\"reasult\":\"2\",\"DEmployeeID\":\"" + ds.Tables[0].Rows[0]["DEmployeeID"].ToString() + "\"}]";
                        }
                    }
                    else//账号错误
                    {
                        a = "[{\"reasult\": \"1\",\"DEmployeeID\": \"\"}]";
                    }
                }
                else
                {
                    string sql = "select top 1 DEmployeeID,AccountID,Pas,DEmployeeCode,DEmployeeName from DEmployee where AccountID='" + Employeid + "' and Pas='" + pwd + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        //a = "登录失败，输入账号或密码错误";
                        a = "[{\"reasult\": \"1\",\"DEmployeeID\": \"\"}]";
                    }
                    else if (ds.Tables[0].Rows.Count > 0)
                    {
                        //a = "登陆成功";
                        a = "[{\"reasult\":\"2\",\"DEmployeeID\":\"" + ds.Tables[0].Rows[0]["DEmployeeID"].ToString() + "\"}]";
                        //a = "2" + ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    }
                }
            }
            else
            {
                //a = " 连接失败  ！！！";
                a = "[{\"reasult\": \"3\",\"DEmployeeID\": \"\"}]";
            }
            return a;
        }

        /// <summary>
        /// 扫描单个瓶签
        /// </summary>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="CheckPro">扫描类型</param>
        /// <returns>返回sql语句执行次数 大于0成功，小于等于0失败</returns>
        [WebMethod(Description = "扫描单个瓶签")]
        public int QRLabelNo(string DEmployeeID, string LabelNo, string CheckPro)
        {
            int QR = 0;
            string str = "";
            if (CheckPro == "bl_Label_QS")
            {
                str = "exec " + CheckPro +"'"+ DateTime.Now.ToShortDateString() + "', '" + DEmployeeID + "','" + LabelNo + "','1','1'";
            }
            else
            {
                str = "exec " + CheckPro + "'" + DEmployeeID + "','" + LabelNo + "','','" + null + "','" + '1' + "','" + '1' + "','1'";
            }
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            int a = cmd.ExecuteNonQuery();//a：执行sql语句的次数

            QR = a <= 0 ? 0 : 1;//a大于0则返回a，否则0
            con.Close();
            return QR;
        }

        /// <summary>
        /// 扫描总瓶签
        /// </summary>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="LabelNos">总瓶签号</param>
        /// <param name="CheckPro">扫描类型</param>
        /// <param name="ivs">瓶签状态</param>
        /// <returns>返回扫描成功次数 m</returns>
        [WebMethod(Description = "扫描总瓶签")]
        public int SigleLableNo(string DEmployeeID, string LabelNos, string CheckPro, int ivs)
        {
            int s = 0;
            string str = "";
            DataTable PrintDt = sel.IVRecordPrint(LabelNos).Tables[0];

            int m = 0;
            if (PrintDt.Rows.Count > 0)
            {
                for (int i = 0; i < PrintDt.Rows.Count; i++)
                {
                    if (ivs == sel.Iseffective(PrintDt.Rows[i][0].ToString(), ivs))  //判断是否可核对
                    {

                        if (CheckPro == "bl_Label_QS")
                        {
                            str = "exec " + CheckPro + "'" + DateTime.Now.ToShortDateString() + "', '" + DEmployeeID + "','" + PrintDt.Rows[i][0].ToString() + "','1','1'";
                        }
                        else
                        {
                            str = "exec " + CheckPro + "'" + DEmployeeID + "','" + PrintDt.Rows[i][0].ToString() + "','','" + null + "','" + '1' + "','" + '1' + "','2'";
                        }
                        SqlCommand cmd = new SqlCommand(str, con);
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0)
                        {
                            m = m + 1;
                        }
                        con.Close();
                    }
                }
            }
            s = m <= 0 ? 0 : m; //m:数量，大于0则返回m
            return s;
        }


        /// <summary>
        /// 核对
        /// </summary>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="LabelNo">瓶签号</param>
        /// <returns>返回string 扫描结果 “扫描成功”等...</returns>
        [WebMethod(Description = "溶剂核对")]
        public string Rongji(string DEmployeeID, string LabelNo)
        {
            string Rongji = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                Rongji = "瓶签不能为空";
                return Rongji;
            }

            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~

            int s = sel.Iseffective(label, 4);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_YP_Solvent", 4);
            }
            else //单个瓶签
            {
                if (s == 4) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_YP_Solvent");

                    Rongji = result > 0 ? result + "个瓶签摆药（溶剂）完成" : "溶剂核对失败";
                }
                else
                {
                    Rongji = getivstatus(s);
                }
            }
            return Rongji;
        }

        [WebMethod(Description = "溶媒核对")]
        public string RongMei(string DEmployeeID, string LabelNo)
        {
            string RongMei = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                RongMei = "瓶签不能为空";
                return RongMei;
            }

            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~

            int s = sel.Iseffective(label, 4);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_YP_Menstruum", 4);
            }
            else
            {

                if (s == 4) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_YP_Menstruum");

                    RongMei = result > 0 ? result + "个瓶签摆药（溶媒）完成" : "溶媒核对失败";
                }
                else
                {
                    RongMei = getivstatus(s);
                }
            }
            return RongMei;
        }

        [WebMethod(Description = "排药核对")]
        public string PaiYao(string DEmployeeID, string LabelNo)
        {
            string paiYao = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                paiYao = "瓶签不能为空";
                return paiYao;
            }
            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~
            int s = sel.Iseffective(LabelNo, 5);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_PY", 5);
            }
            else
            {

                if (s == 5) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_PY");

                    paiYao = result > 0 ? result + "个瓶签排药完成" : "排药核对失败";
                }
                else
                {
                    paiYao = getivstatus(s);
                }
            }
            return paiYao;
        }

        [WebMethod(Description = "配置核对")]
        public string PeiZhi(string DEmployeeID, string LabelNo)
        {
            string peiZhi = string.Empty;
            string label = string.Empty;
            if (LabelNo == "")
            {
                peiZhi = "瓶签不能为空";
                return peiZhi;
            }
            getXml();
             label=sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~

            int result = 0;
            int s = sel.Iseffective(label, 9);

            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_PZ", 9);
            }
            else
            {
               
                if (_OnlyToday == "1")
                {
                    if (sel.IsToday(label) == 0)
                    {
                        return "非当天瓶签或瓶签不存在！";
                    }
                }
                if (s == 9) //扫描瓶签状态
                {

                    try
                    {
                        if (_IsCharge == "1")//需要计费
                        {
                            ChargeInterface.Charge charge = new ChargeInterface.Charge();
                            string msg = string.Empty;
                            string Msg = charge.ScanPZ9(label, DEmployeeID, out msg);
                            //peiZhi = Msg;
                            if (Msg == "1")//计费成功
                            {
                                result = QRLabelNo(DEmployeeID, label, "bl_Label_PZ");
                                peiZhi = result > 0 ? result.ToString() + "个瓶签配置完成,计费成功" : "配置核对失败,计费成功";
                            }
                            else
                            {
                                peiZhi = "计费失败:"+msg.ToString();
                            }
                        }
                        else//不调用计费接口
                        {
                            result = QRLabelNo(DEmployeeID, label, "bl_Label_PZ");
                            peiZhi = "成功配置" + result + "个瓶签";
                        }
                    }
                    catch (Exception ex)
                    {
                        peiZhi = ex.Message;
                    }
                }
                else
                {
                    peiZhi = getivstatus(s);
                }

            }
            return peiZhi;
        }

        [WebMethod(Description = "打包核对")]
        public string DaBao(string DEmployeeID, string LabelNo)
        {
            string daBao = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                daBao = "瓶签不能为空";
            }
            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~
            int s = sel.Iseffective(label, 13);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_DB", 13);
            }
            else
            {
                if (s == 13) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_DB");

                    daBao = result > 0 ? result + "个瓶签打包完成" : "打包核对失败";
                }
                else
                {
                    daBao = getivstatus(s);
                }
            }
            return daBao;
        }

        [WebMethod(Description = "进仓扫描")]
        public string JinCang(string DEmployeeID, string LabelNo)
        {
            string jinCang = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                jinCang = "瓶签不能为空";
                return jinCang;
            }

            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~


            int s = sel.Iseffective(label, 7);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_JC", 7);
            }
            else
            {
                if (s == 7) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_JC");

                    jinCang = result > 0 ? result + "个瓶签进仓完成" : "进仓核对失败";
                }
                else
                {
                    jinCang = getivstatus(s);
                }
            }
            return jinCang;
        }

        [WebMethod(Description = "出仓扫描")]
        public string ChuCang(string DEmployeeID, string LabelNo)
        {
            string chuCang = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                chuCang = "瓶签不能为空";
                return chuCang;
            }
            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~

            int s = sel.Iseffective(label, 11);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_CC", 11);
            }
            else
            {
                if (s == 11) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_CC");

                    chuCang = result > 0 ? result + "个瓶签出仓完成" : "出仓核对失败";
                }
                else
                {
                    chuCang = getivstatus(s);
                }
            }
            return chuCang;
        }

        [WebMethod(Description = "病区签收")]
        public string Qianshou(string DEmployeeID, string LabelNo)
        {
            string qianshou = string.Empty;
            string label = string.Empty;
            int result = 0;
            if (LabelNo == "")
            {
                qianshou = "瓶签不能为空";
                return qianshou;
            }
            label = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~

            int s = sel.Iseffective(label, 15);
            if (label.Length < 4) { return ""; }

            if ((label.Substring(0, 4) == "8888" || label.Substring(0, 4) == "9999") && label.Length >= 22)//扫描的是总瓶签
            {
                result = SigleLableNo(DEmployeeID, label, "bl_Label_QS", 15);
            }
            else
            {
                if (s == 15) //扫描瓶签状态
                {
                    result = QRLabelNo(DEmployeeID, label, "bl_Label_QS");

                    qianshou = result > 0 ? result + "个瓶签签收完成" : "签收失败";
                }
                else
                {
                    qianshou = getivstatus(s);
                }
            }
            return qianshou;
        }


        /// <summary>
        /// 扫描瓶签号，总瓶签号，或者处方号
        /// </summary>
        /// <param name="LabelNo">瓶签号，总瓶签号，或者处方号</param>
        /// <param name="OtherCondition">模糊查询的条件 </param>
        /// <returns>返回瓶签信息，由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "处方号/（总)瓶签信息")]
        public string LableInfor(string LabelNo,string OtherCondition)
        {
            DataSet ds=new DataSet ();
            string label = string.Empty;
            if (LabelNo != string.Empty && LabelNo != "") //LabelNo 不为空
            {
                if (LabelNo.Length < 4) { return ""; }
                string realno = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;//查看是否是第三方瓶签，是就取，不是用瓶签号~~
                if (realno.Substring(0, 4) == "8888" || realno.Substring(0, 4) == "9999")
                { ds = sel.GetQRCodeInfor(realno); } //获取总瓶签信息
                else
                { ds = sel.GetLabelInfor(realno, ""); }//获取单个瓶签信息
            }
            else if (OtherCondition != string.Empty && OtherCondition != "")//labelNo 为空，OtherCondition 不为空
            {
                try
                {
                    ds = sel.GetLabelInfor("", OtherCondition);
                }
                catch
                {
                    return "";
                }
            }
            else //两者全为空
            {
                return "";
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                label = Serialize(ds.Tables[0]);
            }
            return label;
        }

        /// <summary>
        /// 获取药瓶信息
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <returns>返回药品信息，由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "药品信息")]
        public string DrugInfor(string LabelNo)
        {
            string drug = string.Empty;
            string realno = sel.CheckIsHisLabel(LabelNo) != "" ? sel.CheckIsHisLabel(LabelNo) : LabelNo;
            DataSet ds = sel.GetDrugInfor(realno);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                drug = Serialize(ds.Tables[0]);
            }
            return drug;
        }

        /// <summary>
        /// 瓶签内药品瓶、支的数量
        /// </summary>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="Checkpro">扫描类型</param>
        /// <returns>返回瓶签号和对应数量，由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "瓶签瓶支数量")]
        public string AllDrugNum(string DEmployeeID, string Checkpro)
        {
            string Num = string.Empty;
            if (Checkpro != string.Empty)
            {
                DataSet ds = sel.GetDrugNum(DEmployeeID, Checkpro);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Num = Serialize(ds.Tables[0]);
                }
            }

            return Num;
        }

        /// <summary>
        /// 汇总信息
        /// </summary>
        /// <param name="demployeeid">员工号</param>
        /// <param name="checkpro">扫描类型</param>
        /// <param name="date">时间，string</param>
        /// <returns>返回指定时间指定扫描类型下，各病区的总数，核对，未核对；由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "汇总信息")]
        public string DwardInfor(string demployeeid, string checkpro, string date)
        {
            string all = string.Empty;
            DataSet ds = sel.GetAllInfor(demployeeid, checkpro, date);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                all = Serialize(ds.Tables[0]);
            }
            return all;
        }

        /// <summary>
        /// 病区信息
        /// </summary>
        /// <param name="checkpro">扫描类型</param>
        /// <param name="date">时间</param>
        /// <param name="wardname">病区名</param>
        /// <returns>返回该病区下，指定时间的所有瓶签号和瓶签状态；由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "病区信息")]
        public string InforByDward(string checkpro,string date,string wardname)
        {
            string dward = string.Empty;
            DataSet ds = sel.InforByDward( checkpro, date,wardname);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dward = Serialize(ds.Tables[0]);
            }
            return dward;
        }

        /// <summary>
        /// 单个瓶签的所有操作信息
        /// </summary>
        /// <param name="labelno">瓶签号</param>
        /// <returns>返回指定瓶签的所有操作步奏，操作人，操作时间，和对应步奏状态；由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "单个瓶签的所有操作信息")]
        public string SelectStepDetail(string labelno)
        {
            string step = string.Empty;
            DataSet ds = sel.SelectStepDetail(labelno);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                step = Serialize(ds.Tables[0]);
            }
            return step;
        }

        /// <summary>
        /// 差错类型
        /// </summary>
        /// <param name="errorstep">扫描类型，此处为产生差错的扫描步骤</param>
        /// <returns>返回该环节下的错误类型；由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "返回差错类型")]
        public string ReturnErrorType(string errorstep)
        {
            string etype = string.Empty;
            DataSet ds = sel.SelectErrorRule(errorstep);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                etype = Serialize(ds.Tables[0]);
            }
            return etype;
        }

        /// <summary>
        /// 添加差错记录
        /// </summary>
        /// <param name="labelno">瓶签号</param>
        /// <param name="errstep">差错环节，即扫描类型</param>
        /// <param name="errDid">错误人的员工号</param>
        /// <param name="fid">发现人的员工号</param>
        /// <param name="etime">错误的时间</param>
        /// <param name="etype">错误环节下的错误类型</param>
        /// <returns>返回“true”插入成功，“false”插入失败；String</returns>
        [WebMethod(Description = "添加差错记录")]
        public string AddErrorRecord(string labelno,string errstep,string errDid,string fid ,string etime ,string etype)
        {
            try
            {
                //insert into errorrecord values ('ErrorCode','labelno','EDA插入','errorstep','findstep','errorDeid','fDEid','eTime','ftime','eType')
                string type = string.Empty;
                sel.AddErrorRecord(labelno, errstep, errDid, fid, etime, etype);
                return "true";
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// 显示病区
        /// </summary>
        /// <returns>加载病区列表；由datatable转成Json 格式 返回一串String</returns>
        [WebMethod(Description = "显示病区")]
        public string ShowDward()
        {
                string dward = string.Empty;
                DataSet ds = sel.SelectDward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dward = Serialize(ds.Tables[0]);
                }
                return dward;
        }
        #endregion


        //方法
        #region

        /// <summary>
        /// 瓶签核对查询方法
        /// </summary>
        /// <param name="labelCode"></param>
        public void labelCheck(string CheckPro, string labelCode, string demployeeid)
        {
            string res = string.Empty;
            // string date = dtpTime.Value.Date.ToString();
            if (labelCode != "" && labelCode.Length >= 4)//瓶签标准符合要求的情况，正确的瓶签号
            {
                if ((labelCode.Substring(0, 4) == "8888" || labelCode.Substring(0, 4) == "9999") && labelCode.Length >= 22)//扫描的是总瓶签
                {
                    //string userName = employee.DemployeeName;
                    string barCode = labelCode.Substring(0, 22);
                    using (DataTable PrintDt = sel.IVRecordPrint(barCode).Tables[0])
                    {
                        int m = 0;
                        if (PrintDt.Rows.Count > 0)
                        {
                            for (int i = 0; i < PrintDt.Rows.Count; i++)
                            {
                                string str = "exec " + CheckPro + " '" + demployeeid + "','" + PrintDt.Rows[i][0] + "','','" + null + "','" + '1' + "','" + '1' + "','1'";
                                int a = db.SetPIVAsDB(str);
                                if (a > 0)
                                {
                                    m = m + 1;
                                }
                            }
                        }

                        if (m <= 0)
                        {
                            res = "[{\"reasult\": \"0\"}]";  //核对未成功
                        }
                        else
                        {
                            res = "[{\"reasult\": \"2\"，\"num\": \"" + m + "\"}]";//核对成功，m：成功瓶签数量
                        }
                    }
                }
                else if (labelCode != "" && labelCode.Length >= 14)//扫描的是单个瓶签
                {
                    res = CheckOneLabel(CheckPro, labelCode, demployeeid);
                }
            }
            else
            {
                res = "[{\"reasult\": \"1\"}]"; //不是瓶签
            }
        }


        /// <summary>
        /// 扫描单个瓶签的方法
        /// </summary>
        /// <param name="labelCode"></param>
        private string CheckOneLabel(string CheckPro, string labelCode, string demployeeid)
        {
            string re = string.Empty;
            string barCode = labelCode.Substring(0, 14);
            DataSet ds = sel.GetLabelInfor(barCode,"");
            //DataSet ds1 = sel.GetDrugInfor(barCode);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string str = "exec " + CheckPro + " '" + demployeeid + "','" + barCode + "','" + ds.Tables[0].Rows[0]["WardCode"].ToString() + "','" + null + "','" + '1' + "','" + '1' + "','1'";
                int a = db.SetPIVAsDB(str);
                if (a > 0)
                {
                    re = "[{\"reasult\": \"2\"}]";//核对成功
                }
                else
                {
                    re = "[{\"reasult\": \"0\"}]";  //核对未成功
                }
            }
            else
            {
                re = "[{\"reasult\": \"1\"}]"; //不是瓶签
            }

            return re;
        }


        /// <summary>
        /// 返回瓶签状态
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string getivstatus(int i)
        {
            string ivs = string.Empty;
            switch (i)
            {
                case 3: ivs = "已打印"; break;
                case 4: ivs = "已摆药"; break;
                case 5: ivs = "已排药"; break;
                case 7: ivs = "已进仓"; break;
                case 9: ivs = "已配置"; break;
                case 11: ivs = "已出仓"; break;
                case 13: ivs = "已打包"; break;
                case 15: ivs = "已签收"; break;
                default: ivs = "不可核对"; break;
            }
            return ivs;
        }


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private Boolean TestDB()
        {

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("select  top 1 AccountID  from DEmployee", conn);
                DataSet DS = new DataSet();
                sda.Fill(DS);

                if (DS != null)
                {
                    return true;
                }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 讲DataTable转换为Json格式
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string Serialize(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return serializer.Serialize(list);
        }


        /// <summary>
        /// 获取XML信息，IsCharge
        /// </summary>
        /// <returns></returns>
        private void getXml()
        {
            try
            {
                DirectoryInfo Myxml = new DirectoryInfo(Server.MapPath("~\\bin"));//查看XML是否存在，存在：读取，不存在：创建，读取；
                //DirectoryInfo Myxml = new DirectoryInfo("E:\\EDAWebService.xml");
                if (Myxml.Exists)
                {
                   // FileInfo[] files=Myxml.GetFiles();
                    xml.Load(Myxml.FullName+"\\EDAWebService.xml");

                    XmlNode node = xml.SelectSingleNode("EDAWebService");
                    _IsCharge = node.SelectSingleNode("IsCharge").InnerText;
                    _OnlyToday = node.SelectSingleNode("OnlyToday").InnerText;
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    doc.AppendChild(dec);
                    //创建一个根节点（一级）
                    XmlElement root = doc.CreateElement("EDAWebService");
                    doc.AppendChild(root);
                    //创建节点（二级）
                    XmlElement element1 = doc.CreateElement("IsCharge");
                    element1.InnerText = "1";
                    root.AppendChild(element1);

                    XmlElement element2 = doc.CreateElement("OnlyToday");
                    element2.InnerText = "1";
                    root.AppendChild(element2);


                    doc.Save(Myxml.FullName + "\\EDAWebService.xml");
                    Console.Write(doc.OuterXml);

                    xml.Load(Myxml.FullName + "\\EDAWebService.xml");

                    XmlNode node = xml.SelectSingleNode("EDAWebService");
                    _IsCharge = node.SelectSingleNode("IsCharge").InnerText;
                    _OnlyToday = node.SelectSingleNode("OnlyToday").InnerText;

                } 
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
