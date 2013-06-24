using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.DataAccess.SystemManagement.Impl
{
    class UserRepository:PetaPocoRepository<User>,IUserRepository
    {
        public UserRepository(IDBFactory dbFactory):base(dbFactory){}

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="newPassword">新密码（已加密）</param>
        /// <param name="operatorID">操作userID</param>
        /// <returns>修改成功：true；不成功：false</returns>
        public bool ChangePassword(User user, string newPassword,string operatorID)
        {
            user.UserPwd = newPassword;
            user.Updated = operatorID;
            user.UpdateDate = DateTime.Now;
            var count = Update(user);
            return count > 0;
        }

    }
}
