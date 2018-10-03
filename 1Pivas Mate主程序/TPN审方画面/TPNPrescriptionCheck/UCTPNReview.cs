using System.Windows.Forms;
using System.Windows;
using PIVAsCommon;

namespace TPNReview
{
    public partial class UCTPNReview : UserControl, IMenuManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="empCode"></param>
        /// <param name="empRole"></param>
        /// <param name="empTeam"></param>
        public UCTPNReview(string empCode,string empRole,string empTeam)
        {
            //写在前面，赋值
            AppConst.LoginEmpCode = empCode;
            AppConst.LoginEmpRole = empRole;
            AppConst.DEmployeeID = empTeam;

            InitializeComponent();
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            ((IMenuManager)this.mainWindow1).menuBeforeSelect();
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion

    }
}
