using System;
using System.Collections.Generic;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.Models;
using System.Globalization;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public class LocalizedTypeRepository:PetaPocoRepository<LocalizedType>,ILocalizedTypeRepository
    {
        public LocalizedTypeRepository(IDBFactory dbFactory) : base(dbFactory) { }

        public IEnumerable<LocalizedType> GetAllPrompts(long corporateId, CultureInfo cultureInfo, string filterSql, params object[] args)
        {
            //throw new NotImplementedException();
            var strSql = string.Format("where c_CorporateID={0} and C_LocaleId={1} ", corporateId, cultureInfo.LCID) + filterSql;
            IEnumerable<LocalizedType> prompts = Query<LocalizedType>(strSql, args);
            return prompts;
        }

        public bool Exists(long corporateId, CultureInfo cultureInfo)
        {
            //throw new NotImplementedException();
            const string sql = " c_CorporateID=@corpID and C_LocaleId=@localeID";
            return Exists<LocalizedType>(sql, new { corpID = corporateId, localeID = cultureInfo.LCID });
        }

        public bool Exists(long corporateId, CultureInfo cultureInfo, string textKey)
        {
            //throw new NotImplementedException();
            const string sql = " c_CorporateID=@corpID and C_LocaleId=@localeID and c_key=@key";
            return Exists<LocalizedType>(sql, new { corpID = corporateId, localeID = cultureInfo.LCID, key = textKey });
        }

        public LocalizedType GetPrompt(long corporateId, CultureInfo cultureInfo, TypePromptKey key)
        {
            //throw new NotImplementedException();
            var strSql = string.Format("where c_CorporateID={0} and C_LocaleId={1} and c_key='{2}'", corporateId, cultureInfo.LCID, key.ToString());
            return SingleOrDefault<LocalizedType>(strSql);
        }

        public LocalizedType GetPrompt(long primaryKey)
        {
            //throw new NotImplementedException();
            return SingleOrDefault<LocalizedType>(primaryKey);
        }

        public int Insert(long corporaeId, CultureInfo cultureInfo, string typeName, string textName, string translatedText)
        {
            //throw new NotImplementedException();
            if (cultureInfo == null) throw new ArgumentNullException("cultureInfo is null!");
            if (string.IsNullOrEmpty(typeName)) throw new ArgumentNullException("viewPath is null!");
            if (string.IsNullOrEmpty(textName)) throw new ArgumentNullException("textName is null!");

            var pathKey = new TypePromptKey(typeName, textName);

            var view = new LocalizedType
            {
                CorporateID = corporaeId,
                LocaleId = cultureInfo.LCID,
                Key = pathKey.ToString(),
                TypeName = typeName,
                TextName = textName,
                TextValue = translatedText
            };
            return Insert(view);
        }
    }
}
