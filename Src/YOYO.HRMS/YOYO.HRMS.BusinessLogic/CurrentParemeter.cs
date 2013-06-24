using YOYO.HRMS.Utility;
using System.Web.Mvc;
using YOYO.HRMS.Models;
using YOYO.HRMS.BusinessLogic.OrganizationManagement;
using YOYO.HRMS.BusinessLogic.SystemManagement;

namespace YOYO.HRMS.BusinessLogic
{
    public class CurrentParemeter
    {
        static ICorporateService _corpService = DependencyResolver.Current.GetService<ICorporateService>();
        static IUserService _userService = DependencyResolver.Current.GetService<IUserService>();
        /// <summary>
        /// 获取当前登录的公司ID
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentCorporateId()
        {
            var corp = SessionHelper.GetSession("currentCorp");
            Corporate _currentcorp;
            if (corp == null)
            {               
                _currentcorp = _corpService.GetEntity<Corporate>("");
                SessionHelper.SetSession("currentCorp", _currentcorp);
            }         
            else
            {
                _currentcorp=corp as Corporate;
            }
            return _currentcorp.CorporateID;
        }

        /// <summary>
        /// 获取当前登录的userID
        /// </summary>
        /// <returns></returns>
        public static long GetuserID()
        {
            var user = SessionHelper.GetSession("currentUser");
            if (user == null)
            {
                return -1;
            }
            else
            {
                return ((User)user).UserID;
            }                
        }

        /// <summary>
        /// 设置当前登录公司
        /// </summary>
        /// <param name="corp">公司实体</param>
        public static void SetCurrentCorp(Corporate corp)
        {
            SessionHelper.SetSession("currentCorp", corp);
        }

        /// <summary>
        /// 设置当前登录公司
        /// </summary>
        /// <param name="corporateID">公司ID</param>
        public static void SetCurrentCorp(long corporateID)
        {
            var _currentcorp = _corpService.GetEntity<Corporate>(corporateID);
            SessionHelper.SetSession("currentCorp", _currentcorp);
        }


        /// <summary>
        /// 设置当前登录用户
        /// </summary>
        /// <param name="user">user实体</param>
        public static void SetCurrentUser(User user)
        {
            SessionHelper.SetSession("currentUser",user);
        }

        /// <summary>
        /// 设置当前登录用户
        /// </summary>
        /// <param name="userCode">用户code</param>
        public static void SetCurrentUser(string userCode)
        {
            var _currentUser = _userService.GetEntity<User>("and c_userCode=@p_usercode", new { p_usercode = userCode });
            SessionHelper.SetSession("currentUser", _currentUser);

        }
    }
}
