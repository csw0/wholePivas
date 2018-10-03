using PIVAsCommon;
using PIVAsCommon.Models;
using System;

namespace ChargeInterface
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class ChargeFactory
    {
        /// <summary>
        /// 使用反射获取计费接口
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ICharge GetCharge(string type)
        {
            ICharge iCharge = null;
            try
            {
                switch (type)
                {
                    case HospitalType_Const.ORIGIN:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeOrigin")) as ICharge;
                        break;
                    case HospitalType_Const.QD:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeQD")) as ICharge;
                        break;
                    case HospitalType_Const.BDGJ:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeBDGJ")) as ICharge;
                        break;
                    case HospitalType_Const.SDFY:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeSDFY")) as ICharge;
                        break;
                    case HospitalType_Const.HNZL:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeHNZL")) as ICharge;
                        break;
                    case HospitalType_Const.TJXK:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeTJXKService.ChargeTJXK")) as ICharge;
                        break;
                    case HospitalType_Const.NBDY:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeNBDYService.ChargeNBDY")) as ICharge;
                        break;
                    case HospitalType_Const.SHLYD:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeSHLYDService.ChareSHLYD")) as ICharge;
                        break;
                    case HospitalType_Const.GDHQ:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeGDHQService.ChargeGDHQ")) as ICharge;
                        break;
                    default:
                        iCharge = Activator.CreateInstance(Type.GetType("ChargeInterface.ChargeOrigin")) as ICharge;
                        break;
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("通过反射获取计费接口对象出错：" + ex.Message);
            }
            return iCharge;
        }
    }
}
