using System.Collections.Generic;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public class SyslangRepository:PetaPocoRepository<SysLang>,ISyslangRepository
    {
        public SyslangRepository(IDBFactory dbFactory):base(dbFactory)
        {

        }
        /// <summary>
        /// 获取指定公司支持的语言列表
        /// </summary>
        /// <param name="corporateId"></param>
        /// <returns></returns>
        public IEnumerable<SysLang> GetLangs(long corporateId)
        {
            return Query<SysLang>("where c_CorporateID=@corpID order by c_LocaleID", new { corpID = corporateId });
        }
    }
}
