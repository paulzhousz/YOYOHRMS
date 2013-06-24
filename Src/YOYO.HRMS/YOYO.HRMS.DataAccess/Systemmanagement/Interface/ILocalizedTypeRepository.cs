using System.Collections.Generic;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.Models;
using System.Globalization;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public interface ILocalizedTypeRepository : IRepository<LocalizedType>
    {
        /// <summary>
        /// 获取Type多语言信息
        /// </summary>
        /// <param name="corporateId">公司ID</param>
        /// <param name="filterSql"></param>
        /// <param name="args">过滤条件参数</param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        IEnumerable<LocalizedType> GetAllPrompts(long corporateId, CultureInfo cultureInfo, string filterSql, params object[] args);

        bool Exists(long corporateId, CultureInfo cultureInfo);

        bool Exists(long corporateId, CultureInfo cultureInfo, string textKey);

        LocalizedType GetPrompt(long corporateId, CultureInfo cultureInfo, TypePromptKey key);

        LocalizedType GetPrompt(long primaryKey);

        int Insert(long corporaeId, CultureInfo cultureInfo, string typeName, string textName, string translatedText);


    }
}
