using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using YOYO.HRMS.BusinessLogic;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.Core.Localization;

namespace YOYO.HRMS.MVC.Provider
{
    public class LocalizedModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private static ILocalizedTypeService Provider
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
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        /// The metadata for the property.
        /// </returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            if (containerType == null || propertyName == null)
                return metadata;

            if (metadata.DisplayName == null)
            {
                metadata.DisplayName = Translate(containerType, propertyName);
                if (!DefaultUICulture.IsActive && metadata.DisplayName == null)
                    metadata.DisplayName = string.Format("[{0}: {1}]", CultureInfo.CurrentUICulture.Name, propertyName);
            }

            //if (metadata.Watermark == null)
            //    metadata.Watermark = Translate(containerType, propertyName, "Watermark");

            //if (metadata.Description == null)
            //    metadata.Description = Translate(containerType, propertyName, "Description");

            //if (metadata.NullDisplayText == null)
            //    metadata.NullDisplayText = Translate(containerType, propertyName, "NullDisplayText");

            //if (metadata.ShortDisplayName == null)
            //    metadata.ShortDisplayName = Translate(containerType, propertyName, "ShortDisplayName");

            return metadata;
        }

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">mode type</param>
        /// <param name="propertyName">Property name to translate</param>
        /// <returns>Translated string</returns>
        protected virtual string Translate(Type type, string propertyName)
        {
            var corporateId = CurrentParemeter.GetCurrentCorporateId();
            return Provider.GetModelString(corporateId,type, propertyName);
        }

        /// <summary>
        /// Translate a string
        /// </summary>
        /// <param name="type">Model type</param>
        /// <param name="propertyName">Property name</param>
        /// <param name="metadataName">Meta data name</param>
        /// <returns>Translated string</returns>
        protected virtual string Translate(Type type, string propertyName, string metadataName)
        {
            var corporateId = CurrentParemeter.GetCurrentCorporateId();
            return Provider.GetModelString(corporateId, type, propertyName, metadataName);
        }
    }
}
