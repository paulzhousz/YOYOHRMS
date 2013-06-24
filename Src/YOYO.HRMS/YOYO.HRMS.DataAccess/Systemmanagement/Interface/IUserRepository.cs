using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public interface IUserRepository:IRepository<User>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="newPassword">新密码(已加密)</param>
        /// <param name="operatorID">操作userID</param>
        /// <returns>修改成功：true；不成功：false</returns>
        bool ChangePassword(User user,string newPassword,string operatorID);
    }
}
