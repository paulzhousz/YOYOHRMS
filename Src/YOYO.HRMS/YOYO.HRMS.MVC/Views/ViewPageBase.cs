using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.MVC.Html;
using YOYO.HRMS.MVC.Html.SelectLists;
using YOYO.HRMS.BusinessLogic;


namespace YOYO.HRMS.MVC.Views
{
    /// <summary>
    /// View基类
    /// Author：Paul Zhou   Date：2012/11/15
    /// 说明：
    /// ViewPageBase作为项目中所有view的基类，用于各Areas独立mvc项目中注册js文件
    /// 使用规则：
    ///     1.在MVC项目中按照Views目录的相同结构建立ViewScripts目录，每一个View文件对应ViewScripts对应目录下的同名JS文件
    ///       用于存放view所用的js脚本。view加载时会自动注册此js文件。
    /// Update Log:
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ViewPageBase<TModel> : WebViewPage<TModel>
    {




        #region view's localization

        private ILocalizedViewService _viewLocalizer;

        /// <summary>
        /// Gets class used for the view localization
        /// </summary>
        protected virtual ILocalizedViewService ViewLocalizer
        {
            get {
                return _viewLocalizer ??
                       (_viewLocalizer = DependencyResolver.Current.GetService<ILocalizedViewService>());
            }
        }

        /// <summary>
        /// Gets the text helper.
        /// </summary>
        public TextHtmlHelper<TModel> Text { get; private set; }


        /// <summary>
        /// Gets the new HTML helpers.
        /// </summary>
        public FormHtmlHelper<TModel> Html2 { get; private set; }

        /// <summary>
        /// Gets select helper
        /// </summary>
        public SelectHelper Select
        {
            get { return new SelectHelper(); }
        }

        /// <summary>
        /// GetText inspired localization
        /// </summary>
        /// <param name="text"></param>
        /// <param name="formatterArguments">optional arguments if the string contains {} formatters</param>
        /// <returns></returns>
        public MvcHtmlString T(string text, bool commonText, params object[] formatterArguments)
        {
            string translated;
            if (!commonText)
            {
                translated = ViewLocalizer.Translate(CurrentParemeter.GetCurrentCorporateId(),
                    VirtualPath, ViewContext.RouteData, text);
            }
            else
            {
                translated = ViewLocalizer.LoadCommonPrompt(CurrentParemeter.GetCurrentCorporateId(), text);
            }
            return
                MvcHtmlString.Create(formatterArguments.Length == 0
                                         ? translated
                                         : string.Format(translated, formatterArguments));
        }


        /// <summary>
        /// Inits the helpers.
        /// </summary>
        public override void InitHelpers()
        {
            base.InitHelpers();
            Html2 = new FormHtmlHelper<TModel>(Html);
            Text = new TextHtmlHelper<TModel>(Html);
        }

        #endregion view's localization

        protected override void InitializePage()
        {
            base.InitializePage();
            AddScriptForView(VirtualPath);
        }

        #region js file register
        
        void AddScriptForView(string path)
        {
            //throw new NotImplementedException();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            var dctScripts = GetScriptsDictionary();
            if (!dctScripts.ContainsKey(path))
            {
                //Change path to the javascripts folder
                path = path.Replace("/Views/", "/ViewScripts/");
                //Change the extension to .js
                path = Path.ChangeExtension(path, "js");

                //Check if the file exists
                if (File.Exists(Server.MapPath(path)))
                {
                    //Build the javascript reference and keep it in the dictionary
                    var tagBuilder = new TagBuilder("script");
                    tagBuilder.Attributes.Add("type", "text/javascript");
                    tagBuilder.Attributes.Add("src", Url.Content(path));
                    dctScripts[path] = string.Format("{0}{1}",
                    tagBuilder.ToString(TagRenderMode.Normal), System.Environment.NewLine);
                }
            }
        }

        bool _isPrincipalView;
        const string ViewScriptsKey = "__ViewScripts";

        private Dictionary<string, string> GetScriptsDictionary()
        {
            //throw new NotImplementedException();
            var ctx = Context;
            Dictionary<string, string> dctScripts;
            if (!ctx.Items.Contains(ViewScriptsKey))
            {
                //If the dictionary is not created yet, then this view is the principal
                _isPrincipalView = true;
                dctScripts = new Dictionary<string, string>();
                ctx.Items[ViewScriptsKey] = dctScripts;

            }
            else
            {
                dctScripts = ctx.Items[ViewScriptsKey] as Dictionary<string, string>;
            }
            return dctScripts;
        }

        public override void ExecutePageHierarchy()
        {
            base.ExecutePageHierarchy();

            //This code will execute only for the principal view,
            //not for the layout view, start view, or partial views.
            if (_isPrincipalView)
            {
                var dctScripts = GetScriptsDictionary();
                var sw = Output as StringWriter;
                var sb = sw.GetStringBuilder();

                //add here the javascript files for layout and viewstart
                AddScriptForView(Layout);
                AddScriptForView("~/Views/_ViewStart.cshtml");

                //Insert the scripts
                foreach (var pair in dctScripts)
                {
                    sb.Insert(0, pair.Value);
                }
            }
        }
        #endregion js file register
    }

    public abstract class ViewPageBase : ViewPageBase<dynamic> { }
}
