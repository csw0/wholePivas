
using PIVAsCommon.Helper;

namespace PivasHisInterface
{
    /// <summary>
    /// pivas与his之间的通信抽象类
    /// </summary>
    public abstract class AbstractPivasHisComm : IPivasHisCommunication
    {
        public DB_Help dbHelp = new DB_Help();
    }
}
