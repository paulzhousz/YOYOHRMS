using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using YOYO.HRMS.Utility;
using YOYO.HRMS.MVC;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.Models;
using System.Text;

namespace YOYO.HRMS.Web.Controllers
{
    public class LanguageController : Controller
    {
        private static ILocalizedViewService _viewService;

        public LanguageController(ILocalizedViewService viewService)
        {
            _viewService = viewService;
        }
        //
        // GET: /Language/

        public JavaScriptResult Language()
        {
            string lanCode; 
            if (CurrentParemeter.GetCurrentLan() == string.Empty)
            {
                CurrentParemeter.SetCurrentLan(Request.UserLanguages[0]);
            }
            lanCode = CurrentParemeter.GetCurrentLan();
            return GetResourceScript(lanCode);
        }

        JavaScriptResult GetResourceScript(string lanCode)
        {
            ICacheStorage _cache=CacheFactory.CreateCacheFactory();
            var cacheName = string.Format("ResourceJavaScripter.{0}", lanCode);
            var value = _cache.Get(cacheName) as JavaScriptResult;
            if (value == null)
            {
            JavaScriptResult javaScriptResult = CreateResourceScript(lanCode);
            _cache.Insert(cacheName, javaScriptResult,TimeSpan.FromHours(24));
            return javaScriptResult;
            }
            return value;
        }

        private JavaScriptResult CreateResourceScript(string lanCode)
        {
            var sb = new StringBuilder("var Prompts={");
            IEnumerable<LocalizedView> prompts = _viewService.LoadAllCommonPrompts(lanCode);

            foreach (LocalizedView prompt in prompts)
            {
                var key=prompt.TextName;
                var value=prompt.TextValue;
                if (string.IsNullOrEmpty(value))
                {
                    value = string.Format("[{0}]{1}", lanCode, key);
                }
                sb.AppendFormat("\"{0}\":\"{1}\",", key, value);
            }

            string script = sb.ToString();
            if (!string.IsNullOrEmpty(script))
            {
                script = script.Remove(script.Length - 1);
            }
            script += "};";
            return new JavaScriptResult { Script = script };
        }
    }
}
