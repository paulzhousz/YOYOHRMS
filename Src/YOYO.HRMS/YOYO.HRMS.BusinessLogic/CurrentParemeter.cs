using YOYO.HRMS.Utility;
using System.Web.Mvc;
using YOYO.HRMS.Models;
using YOYO.HRMS.BusinessLogic.OrganizationManagement;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.Core.Localization;
using System.Threading;

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
            var corp = GetCurrentCorporate();
            Corporate _currentcorp;
            if (corp == null)
            {
                _currentcorp = _corpService.GetEntity<Corporate>("and c_isEnabled=1");
                SessionHelper.SetSession("currentCorp", _currentcorp);
            }
            else
            {
                _currentcorp = corp;
            }
            return _currentcorp.CorporateID;
        }

        /// <summary>
        /// 获取当前登录的userID
        /// </summary>
        /// <returns></returns>
        public static long GetuserID()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return -1;
            }
            else
            {
                return user.UserID;
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
            SessionHelper.SetSession("currentUser", user);
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

        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public static User GetCurrentUser()
        {
            var user = SessionHelper.GetSession("currentUser");
            return (User)user;
        }

        /// <summary>
        /// 获取当前登录公司
        /// </summary>
        /// <returns></returns>
        public static Corporate GetCurrentCorporate()
        {
            var corp = SessionHelper.GetSession("currentCorp");
            return (Corporate)corp;
        }

        /// <summary>
        /// 设置当前用户语言
        /// </summary>
        /// <param name="languageName">语言名称（like zh-CN，zh-TW，en-US）</param>
        public static void SetCurrentLan(string languageName)
        {
            DefaultUICulture.Set(languageName);
            CookieHelper.SetCookie("YoYoLanguage", DefaultUICulture.Value.Name);

        }

        /// <summary>
        /// 获取当前用户语言
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentLan()
        {
            var _currentLan = CookieHelper.GetCookieValue("YoYoLanguage");
            //if (_currentLan == string.Empty)
            //{
            //    _currentLan = DefaultUICulture.Value.Name;
            //}
            return _currentLan;
        }

        /// <summary>
        /// 设置当前Theme
        /// </summary>
        /// <param name="themeName"></param>
        public static void SetCurrentTheme(string themeName)
        {
            CookieHelper.SetCookie("YoYoThemeName", themeName);
        }

        /// <summary>
        /// 获取当前Theme
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTheme()
        {
            var _currentTheme = CookieHelper.GetCookieValue("YoYoThemeName");
            return _currentTheme;
        }
    }
}
