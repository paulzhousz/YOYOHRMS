using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Models;
using YOYO.HRMS.DataAccess.SystemManagement;
using YOYO.HRMS.DataAccess.OrganizationManagement;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Utility;

namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly  IUserRepository _repository;
        private readonly IUserCorporatePrivRepository _userCorpPrivRepo;
        private readonly ICorporateRepository _corpRepo;

        public UserService(IUnitOfWork uow, 
            IUserRepository repository,
            ICorporateRepository corpRepo,
            IUserCorporatePrivRepository userCorpPrivRepo)
            : base(uow, repository)
        {            
            _uow = uow;
            _repository = repository;
            _corpRepo = corpRepo;
            _userCorpPrivRepo = userCorpPrivRepo;
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="corporateID">登陆公司ID</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="passWord">用户密码</param>
        /// <returns></returns>
        public UserMessage LoginVerify(long corporateID, string userCode, string passWord)
        {
            //throw new NotImplementedException();

            var md5Password = MD5Provider.Hash(passWord);
            var user = _repository.SingleOrDefault<User>("where c_UserCode=@p_usercode and c_UserPwd=@p_userpwd", new { p_usercode = userCode, p_userpwd = md5Password });

            //无法找到用户
            if (user.IsNullOrEmpty())
            {
                return UserMessage.UserLoginError;
            }
            //用户被锁定
            else if (!user.IsEnabled)
            {
                return UserMessage.IsLocked;
            }
            //用户无此公司登陆权限
            //else if (!UserCorporateVerify(corporateID,user.UserID))
            //{
                //TODO:增加用户是否有登陆相应公司权限判断
            //    return UserMessage.NotAccessCorporate;
            //}
            else
            {
                return UserMessage.UserLoginSuccess;
            }


        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public bool ChangePassword(long userID, string newPassword)
        {
            User user = _repository.SingleOrDefault<User>(userID);
            string MD5pwd = MD5Provider.Hash(newPassword);
            if (!user.IsNullOrEmpty())
            {
                return _repository.ChangePassword(user, MD5pwd, user.UserCode);

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证用户是否有登陆公司权限
        /// </summary>
        /// <param name="corporateID">公司ID</param>
        /// <param name="userID">UserID</param>
        /// <returns></returns>
        public bool UserCorporateVerify(long corporateID, long userID)
        {
            
            Corporate corp=_corpRepo.SingleOrDefault<Corporate>("where c_IsEnabled=1");
            //公司是否被禁用
            if (corp.IsNullOrEmpty()||!corp.IsEnabled)
            {
                return false;
            }
            else
            {
                if (!_userCorpPrivRepo.Exists<UserCorporatePriv>("where c_UserID=@p_userid and c_CorporateID=@p_corpID",
                    new { p_userid = userID, p_corpID = corporateID }))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
