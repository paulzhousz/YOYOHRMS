using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Models;
using YOYO.HRMS.Utility;

namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public interface IUserService:IBaseService<User>
    {
        /// <summary>
        /// 验证用户是否有登陆公司权限
        /// </summary>
        /// <param name="corporateID">公司ID</param>
        /// <param name="userID">UserID</param>
        /// <returns></returns>
        bool UserCorporateVerify(long corporateID,long userID);

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="corporateID">登陆公司ID</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="passWord">用户密码</param>
        /// <returns></returns>
        UserMessage LoginVerify(long corporateID, string userCode, string passWord);
        
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        bool ChangePassword(long userID,string newPassword);
    }
}
