using System;
using System.Web;
using System.Web.Mvc;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.BusinessLogic;

namespace YOYO.HRMS.MVC.Provider
{
    class YoYoStringsProvider : IValidationMessageDataSource
    {
        private ILocalizedTypeService Provider
        {
            get
            {
                // ASP.NET doesn't honor the IoC scope of the provider
                // which means that we can't set the dependency one,
                // but need to resolve it per request
                var provider = HttpContext.Current.Items["ILocalizedTypeService"] as ILocalizedTypeService;
                if (provider == null)
                {
                    provider = DependencyResolver.Current.GetService<ILocalizedTypeService>();
                    HttpContext.Current.Items["ILocalizedTypeService"] = provider;
                }

                if (provider == null)
                    throw new InvalidOperationException(
                        "Failed to find an 'ILocalizedTypeService' implementation. Either include one in the LocalizedModelMetadataProvider constructor, or register an implementation in your Inversion Of Control container.");

                return provider;
            }
        }

        /// <summary>
        /// Get a validation message
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        /// String if found; otherwise <c>null</c>.
        /// </returns>
        public string GetMessage(IGetMessageContext context)
        {
            long corporateID = CurrentParemeter.GetCurrentCorporateId();
            return Provider.GetValidationString(corporateID,context.Attribute.GetType(), context.ContainerType, context.PropertyName) ??
                        Provider.GetValidationString(corporateID,context.Attribute.GetType());
        }

        public string GetCommonMessage(string propertyName)
        {
            long corporateID = CurrentParemeter.GetCurrentCorporateId();
            return Provider.GetCommonPrompts(corporateID, propertyName);
        }
    }
}
