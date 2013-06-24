

namespace YOYO.HRMS.Core.Localization
{
    /// <summary>
    /// Provides validation messages for attributes from a specific data source.
    /// </summary>
    public interface IValidationMessageDataSource
    {
        /// <summary>
        /// Get a validation message
        /// </summary>
        /// <param name="context"></param>
        /// <returns>String if found; otherwise <c>null</c>.</returns>
        string GetMessage(IGetMessageContext context);

        /// <summary>
        /// 获取通用message
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetCommonMessage(string propertyName);
    }
}
