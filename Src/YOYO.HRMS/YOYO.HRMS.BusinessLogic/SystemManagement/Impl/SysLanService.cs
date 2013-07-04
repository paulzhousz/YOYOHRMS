using System;
using System.Collections.Generic;
using System.Globalization;
using YOYO.HRMS.DataAccess.SystemManagement;
using YOYO.HRMS.Models;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Core.Localization;

namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public class SysLanService:BaseService<SysLang>,ISysLanService
    {
       // private readonly long _currentCorporateId;
        private readonly IUnitOfWork _uow;
        private readonly ISyslangRepository _repository;

        public SysLanService(IUnitOfWork uow, ISyslangRepository repository)
            : base(uow, repository)
        {            
          //  _currentCorporateId = CurrentParemeter.GetCurrentCorporateId();
            _uow = uow;
            _repository = repository;
        }
        
        public CultureInfo GetCurrentLang(long corporateID,string cultureName)
        {
            try
            {
                var culture = new CultureInfo(cultureName);
                return _repository.Exists<SysLang>("where c_CorporateID=@corpID and C_LocaleId=@localeId",
                                             new { corpID = corporateID, localeID = culture.LCID }) ? culture : GetDefaultLang();
            }
            catch (Exception)
            {

                return GetDefaultLang();
            }
            
        }


        public CultureInfo GetDefaultLang()
        {
            return DefaultUICulture.Value;
        }

        /// <summary>
        /// 获取当前公司支持的语言列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysLang> GetLangList()
        {
            return _repository.GetLangs(currentCorporateId);
        }
    }
}