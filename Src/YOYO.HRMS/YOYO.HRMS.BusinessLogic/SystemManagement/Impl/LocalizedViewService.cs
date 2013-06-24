﻿using System;
using System.Web.Routing;
using System.Globalization;
using YOYO.HRMS.Core.Localization;
using YOYO.HRMS.Core.Repository;
using YOYO.HRMS.DataAccess.SystemManagement;
using YOYO.HRMS.Models;

namespace YOYO.HRMS.BusinessLogic.SystemManagement
{
    public class LocalizedViewService:BaseService<LocalizedView>,ILocalizedViewService
    {
        private readonly ILocalizedViewRepository _repository;
        private readonly IUnitOfWork _uow;

        public LocalizedViewService(IUnitOfWork uow, ILocalizedViewRepository repository):
            base(uow,repository)
        {
            if (uow == null) throw new ArgumentNullException("uow is null");
            if (repository == null) throw new ArgumentNullException("ILocalizedViewRepository is null");
            _uow = uow;
            _repository = repository;
        }

        /// <summary>
        /// Translate a text prompt
        /// </summary>
        /// <param name="routeData">Used to lookup the view location</param>
        /// <param name="text">Text to translate</param>
        /// <returns>
        /// String if found; otherwise null.
        /// </returns>
        [Obsolete("Use the overload with viewPath")]
        public virtual string Translate(long corporateId,RouteData routeData, string text)
        {
            if (routeData == null) throw new ArgumentNullException("routeData");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            if (!_repository.Exists(corporateId, CultureInfo.CurrentUICulture))
            {
                CloneDefaultCulture(corporateId, "", null);
            }

            var textToSay = "";
            var uri = ViewPromptKey.GetViewPath(routeData);
            var id = new ViewPromptKey(uri, text);
            var prompt = _repository.GetPrompt(corporateId, CultureInfo.CurrentUICulture, id);

            if (prompt == null)
            {
                textToSay = LoadCommonPrompt(corporateId,text);
                if (textToSay == null)
                    _repository.Insert(corporateId,CultureInfo.CurrentUICulture,uri,text,"");
            }
            else
                textToSay = prompt.TextValue;

            return string.IsNullOrEmpty(textToSay)
                       ? FormatMissingPrompt(text)
                       : textToSay;
        }

        /// <summary>
        /// Translate a text prompt
        /// </summary>
        /// <param name="viewPath">Virtual path to the view, so that we can identify layouts and request common prompts.</param>
        /// <param name="routeData">Used to lookup the view location</param>
        /// <param name="text">Text to translate</param>
        /// <returns>
        /// String if found; otherwise null.
        /// </returns>
        public virtual string Translate(long corporateId,string viewPath, RouteData routeData, string text)
        {
            if (routeData == null) throw new ArgumentNullException("routeData");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            
            if (!_repository.Exists(corporateId, CultureInfo.CurrentUICulture))
            {
                CloneDefaultCulture(corporateId, "", null);
            }

            var textToSay = "";
            var uri = ViewPromptKey.GetViewPath(viewPath,routeData);
            var id = new ViewPromptKey(uri, text);
            var prompt = _repository.GetPrompt(corporateId, CultureInfo.CurrentUICulture, id);

            if (prompt == null)
            {
                textToSay = LoadCommonPrompt(corporateId, text);
                if (textToSay == null)
                    _repository.Insert(corporateId, CultureInfo.CurrentUICulture, uri, text, "");
            }
            else
                textToSay = prompt.TextValue;

            return string.IsNullOrEmpty(textToSay)
                       ? FormatMissingPrompt(text)
                       : textToSay;
        }

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
        public virtual string FormatMissingPrompt(string text)
        {
            return (DefaultUICulture.LCID==CultureInfo.CurrentUICulture.LCID)
                ?text
                : string.Format("{1}:[{0}]", text, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Load a common prompt
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <returns>Translation if found; otherwise null</returns>
        /// <remarks>Used to avoid duplications of prompts.</remarks>
        public virtual string LoadCommonPrompt(long corporateId, string text)
        {
            var key = new ViewPromptKey("Common", text);
            var prompt = _repository.GetPrompt(corporateId, CultureInfo.CurrentUICulture, key);
            if (prompt == null)
            {
                _repository.Insert(corporateId, CultureInfo.CurrentUICulture, "Common", text, text);
            }
            return prompt == null ? text : prompt.TextValue;
        }

        /// <summary>
        /// Clone the default culture 
        /// </summary>
        /// <remarks>A translation for the current UI culture is missing. Clones the default culture into the current one</remarks>
        /// <seealso cref="DefaultUICulture" />
        public virtual void CloneDefaultCulture(long corporateId,string filterSql, params object[] args)
        {
            var phrases = _repository.GetAllPrompts(corporateId, DefaultUICulture.Value, filterSql, args);

            if (phrases != null)
            {
                try
                {
                    _uow.StartNew();
                    foreach (var phrase in phrases)
                    {
                        _repository.Insert(
                            phrase.CorporateID,
                            CultureInfo.CurrentUICulture,
                            phrase.ViewPath,
                            phrase.TextName,
                            phrase.TextValue
                            );
                    }
                    _uow.Commit();
                }
                catch (Exception ex)
                {
                    _uow.Rollback();
                    throw new Exception("CloneDefaultCulture");
                }
            }
        }

    }
}
