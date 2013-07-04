using YOYO.HRMS.Core.Localization;
using System.Web.Routing;
using YOYO.HRMS.Models;
using System.Collections.Generic;


namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public interface ILocalizedViewService:IBaseService<LocalizedView>
    {
        /// <summary>
        /// Translate a text prompt
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="routeData">Used to lookup the view location</param>
        /// <param name="text">Text to translate</param>
        /// <returns>
        /// String if found; otherwise null.
        /// </returns>
        string Translate(long corporateId,RouteData routeData, string text);

        /// <summary>
        /// Translate a text prompt
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="viewPath">Virtual path to the view, so that we can identify layouts and request common prompts.</param>
        /// <param name="routeData">Used to lookup the view location</param>
        /// <param name="text">Text to translate</param>
        /// <returns>
        /// String if found; otherwise null.
        /// </returns>
        string Translate(long corporateId,string viewPath, RouteData routeData, string text);

        /// <summary>
        /// Format output for a missing prompt
        /// </summary>
        /// <param name="text">View text</param>
        /// <returns>Plain text if default culture; otherwise culture tagged text.</returns>
        /// <example>
        /// Default culture:
        /// <code>
        /// localizer.FormatMissingPrompt("Hello world"); // --> hello world
        /// </code>
        /// Another culture:
        /// <code>
        /// localizer.FormatMissingPrompt("Hello world"); // --> [sv-se: hello world]
        /// </code>
        /// </example>
        string FormatMissingPrompt(string text);

        /// <summary>
        /// Load a common prompt
        /// </summary>
        /// <param name="corporateId"></param>
        /// <param name="text">Text to translate</param>
        /// <returns>Translation if found; otherwise null</returns>
        /// <remarks>Used to avoid duplications of prompts.</remarks>
        string LoadCommonPrompt(long corporateId, string text);

        IEnumerable<LocalizedView> LoadAllCommonPrompts(string cultureName);

         /// <summary>
        /// Clone the default culture 
        /// </summary>
        /// <remarks>A translation for the current UI culture is missing. Clones the default culture into the current one</remarks>
        /// <seealso cref="DefaultUICulture" />
        void CloneDefaultCulture(long corporateId,string filterSql, params object[] args);
    }
}
