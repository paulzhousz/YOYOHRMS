using System;
using System.Globalization;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.DataAccess.SystemManagement;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public class LocalizedTypeService:BaseService<LocalizedType>,ILocalizedTypeService
    {
        private readonly ILocalizedTypeRepository _repository;
        private readonly IUnitOfWork _uow;

        public LocalizedTypeService(IUnitOfWork uow, ILocalizedTypeRepository repository):
            base(uow,repository)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (repository == null) throw new ArgumentNullException("repository");
            _uow = uow;
            _repository = repository;
        }


        public string GetModelString(long corporateId, Type model, string propertyName)
        {
            //throw new NotImplementedException();
            return Translate(corporateId, model, propertyName);
        }

        public string GetModelString(long corporateId, Type model, string propertyName, string metadataName)
        {
            //throw new NotImplementedException();
            return Translate(corporateId, model, propertyName + "_" + metadataName);
        }

        public string GetValidationString(long corporateId, Type attributeType)
        {
            //throw new NotImplementedException();
            return Translate(corporateId, attributeType, "class");
        }

        public string GetValidationString(long corporateId, Type attributeType, Type modelType, string propertyName)
        {
            //throw new NotImplementedException();
            return Translate(corporateId,modelType,propertyName+"_"+attributeType.Name);
        }

        public string GetEnumString(long corporateId, Type enumType, string name)
        {
            //throw new NotImplementedException();
            return Translate(corporateId, enumType, name);
        }

        protected virtual string Translate(long corporateId,Type type, string name)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (name == null) throw new ArgumentNullException("name");

            //if (!string.IsNullOrEmpty(type.Namespace) && (type.Namespace.StartsWith("Griffin.MvcContrib") && !type.Namespace.Contains("TestProject")))
            //{
            //    return null;
            //}

            var key = new TypePromptKey(type.FullName, name);
            var prompt = _repository.GetPrompt(corporateId,CultureInfo.CurrentUICulture,key) ??
                         _repository.GetPrompt(corporateId, CultureInfo.CurrentUICulture,
                                               new TypePromptKey(typeof(CommonPrompts).FullName, name));

            if (prompt == null)
            {
                _repository.Insert(corporateId,CultureInfo.CurrentUICulture, type.FullName, name, "");
            }
            else
            {
                if (!string.IsNullOrEmpty(prompt.TextValue))
                    return prompt.TextValue;
            }

            return name.EndsWith("NullDisplayText") ? "" : null;

        }


        public string GetCommonPrompts(long corporateId, string name)
        {
            return GetModelString(corporateId, typeof (CommonPrompts), name);
        }
    }
}
