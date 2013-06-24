using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.DataAccess.OrganizationManagement;
using YOYO.HRMS.Models;
using YOYO.HRMS.Utility;

namespace YOYO.HRMS.BusinessLogic.OrganizationManagement
{
    public class CorporateService:BaseService<Corporate>,ICorporateService
    {
        private readonly long _currentCorporateId;
        private readonly long _currentUserID;
        private readonly IUnitOfWork _uow;
        private readonly  ICorporateRepository _repository;

        public CorporateService(IUnitOfWork uow, ICorporateRepository repository)
            : base(uow, repository)
        {            
            //_currentCorporateId = CurrentParemeter.GetCurrentCorporateId();
            //_currentUserID = CurrentParemeter.GetuserID();
            _uow = uow;
            _repository = repository;
        }


        /// <summary>
        /// 获取所有可以登入的公司列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Corporate> GetAllCorporate()
        {
            
            return Query<Corporate>("and c_IsEnabled=1 order by c_CorporateCode");
        }
    }
}
