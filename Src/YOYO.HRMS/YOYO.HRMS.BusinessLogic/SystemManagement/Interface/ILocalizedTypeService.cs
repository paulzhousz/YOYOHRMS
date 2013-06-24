using System;
using System.Web.Mvc;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public interface ILocalizedTypeService:IBaseService<LocalizedType>
    {
        /// <summary>
        /// Get a localized string for a model property
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="model">Model being localized</param>
        /// <param name="propertyName">Property to get string for</param>
        /// <returns>Translated string if found; otherwise null.</returns>
        string GetModelString(long corporateId,Type model, string propertyName);


        /// <summary>
        /// Get a localized metadata for a model property
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="model">Model being localized</param>
        /// <param name="propertyName">Property to get string for</param>
        /// <param name="metadataName">Valid names are: Watermark, Description, NullDisplayText, ShortDisplayText.</param>
        /// <returns>Translated string if found; otherwise null.</returns>
        /// <remarks>
        /// Look at <see cref="ModelMetadata"/> to know more about the meta data
        /// </remarks>
        string GetModelString(long corporateId,Type model, string propertyName, string metadataName);


        /// <summary>
        /// Get a translated string for a validation attribute
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="attributeType">Type of attribute</param>
        /// <returns>Translated validation message if found; otherwise null.</returns>
        /// <remarks>
        /// Used to get localized error messages for the DataAnnotation attributes. The returned string 
        /// should have the same format as the built in messages, such as "{0} is required.".
        /// </remarks>
        string GetValidationString(long corporateId,Type attributeType);

        /// <summary>
        /// Get a translated string for a validation attribute
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="attributeType">Type of attribute</param>
        /// <param name="modelType">Your view model</param>
        /// <param name="propertyName">Property in your view model</param>
        /// <returns>Translated validation message if found; otherwise null.</returns>
        /// <remarks>
        /// Tries to get a validation string which is specific for a view model property.
        /// </remarks>
        string GetValidationString(long corporateId,Type attributeType, Type modelType, string propertyName);

        /// <summary>
        /// Gets a enum string
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="enumType">Type of enum</param>
        /// <param name="name">Name of the value to translation for</param>
        /// <returns>Translated name if found; otherwise null.</returns>
        string GetEnumString(long corporateId,Type enumType, string name);

        /// <summary>
        /// 获取通用信息
        /// </summary>
        /// <param name="corporateId">公司ID</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        string GetCommonPrompts(long corporateId, string name);
    }
}
