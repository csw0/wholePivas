using System;
using System.Web.Services;

namespace PivasWebServiceInterface
{
    /// <summary>
    /// PivasWebService,访问his，最直接的接口
    /// </summary>
    [WebService(Namespace = "http://192.168.10.29/PivasWebService.asmx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PivasWebService : WebService
    {
        [WebMethod(Description = "计费服务")]
        public string Charge(String DemployeeName,string xml,out string msg)
        {
            msg = "";
            ChargeService cs = new ChargeService();
            return cs.charge(DemployeeName,xml ,out msg);
        }

        #region 启东反编译
        [WebMethod(Description = " 艾隆计费")]
        public string Charge(string Labelno, string UserCode, out string hismsg, out string hisret)
        {
            new ChargeService().ChargeQD(Labelno, UserCode, out hismsg, out hisret);
            return "1";
        }

        [WebMethod(Description = "退单")]
        public string BackPre(string p_group_no, string prm_EXEC_DOCTOR, string prm_jujueyy, out string PRM_DATABUFFER, out string PRM_APPCODE)
        {
            new BackPreService().BackPre(p_group_no, prm_EXEC_DOCTOR, prm_jujueyy, out PRM_DATABUFFER, out PRM_APPCODE);
            return "1";
        }

        [WebMethod(Description = "计费")]
        public string Charge(string Groupno, string infusionDT, string UserCode, out string hismsg, out string hisret)
        {
            new ChargeService().WXChargeQD(Groupno, infusionDT, UserCode, out hismsg, out hisret);
            return "1";
        }
        #endregion
    }
}

