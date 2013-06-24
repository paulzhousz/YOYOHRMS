using System;
using System.Web;
using System.Web.Mvc;

namespace YOYO.HRMS.MVC.Html
{
    public static class LocalizationHelpers
    {
        public static IHtmlString MetaAcceptLanguage<TT>(this HtmlHelper<TT> html)
        {
            var acceptLanguage = HttpUtility.HtmlAttributeEncode(System.Threading.Thread.CurrentThread.CurrentUICulture.ToString());
            return new HtmlString(String.Format("<meta name=\"accept-language=\" content=\"{0}\">", acceptLanguage));
        }
    }
}