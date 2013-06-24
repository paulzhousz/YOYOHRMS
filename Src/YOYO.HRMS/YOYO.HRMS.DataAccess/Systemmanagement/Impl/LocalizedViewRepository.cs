using System;
using System.Collections.Generic;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.Models;
using System.Globalization;

namespace YOYO.HRMS.DataAccess.SystemManagement
{
    public class LocalizedViewRepository:PetaPocoRepository<LocalizedView>,ILocalizedViewRepository
    {

        public LocalizedViewRepository(IDBFactory dbFactory):base(dbFactory){}

        /// <summary>
        /// 获取View多语言信息
        /// </summary>
        /// <param name="corporateId">公司ID</param>
        /// <param name="localeID">语言LocaleID</param>
        /// <param name="filterSql">过滤条件</param>
        /// <param name="args">过滤条件参数</param>
        /// <returns></returns>
        public IEnumerable<LocalizedView> GetAllPrompts(long corporateId, CultureInfo cultureInfo, string filterSql, params object[] args)
        {
            string strSql = string.Format("where c_CorporateID={0} and C_LocaleId={1} ", corporateId, cultureInfo.LCID)+filterSql;
            IEnumerable<LocalizedView> prompts = Query<LocalizedView>(strSql, args);
            return prompts;
        }

        public LocalizedView GetPrompt(long corporateId, CultureInfo cultureInfo, ViewPromptKey key)
        {
            //throw new NotImplementedException();
            string strSql = string.Format("where c_CorporateID={0} and C_LocaleId={1} and c_key='{2}'", corporateId, cultureInfo.LCID,key.ToString());
            return SingleOrDefault<LocalizedView>(strSql);
        }

        public LocalizedView GetPrompt(long primaryKey)
        {
            return SingleOrDefault<LocalizedView>(primaryKey);
        }

        public int Insert(long corporaeId, CultureInfo cultureInfo, string viewPath, string textName, string translatedText)
        {
            //throw new NotImplementedException();

            if (cultureInfo==null) throw new ArgumentNullException("cultureInfo is null!");
            if (string.IsNullOrEmpty(viewPath)) throw new ArgumentNullException("viewPath is null!");
            if (string.IsNullOrEmpty(textName)) throw new ArgumentNullException("textName is null!");

            ViewPromptKey pathKey=new ViewPromptKey (viewPath,textName);

            LocalizedView view = new LocalizedView
            {
                CorporateID = corporaeId,
                LocaleId=cultureInfo.LCID,
                Key = pathKey.ToString(),
                ViewPath = viewPath,
                TextName = textName,
                TextValue = translatedText
            };
            return Insert(view);
        }

        public bool Exists(long corporateId, CultureInfo cultureInfo)
        {
            //throw new NotImplementedException();
            string sql = " c_CorporateID=@corpID and C_LocaleId=@localeID";
            return Exists<LocalizedView>(sql, new { corpID = corporateId, localeID = cultureInfo.LCID });
        }

        public bool Exists(long corporateId, CultureInfo cultureInfo, string textKey)
        {
            string sql = " c_CorporateID=@corpID and C_LocaleId=@localeID and c_key=@key";
            return Exists<LocalizedView>(sql, new { corpID = corporateId, localeID = cultureInfo.LCID, key = textKey });
            //throw new NotImplementedException();
        }
    }
}
