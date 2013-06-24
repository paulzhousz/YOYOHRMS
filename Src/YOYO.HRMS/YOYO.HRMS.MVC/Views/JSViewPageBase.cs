using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;

namespace YOYO.HRMS.MVC.Views
{
    /// <summary>
    /// View基类
    /// Author：Paul Zhou   Date：2012/11/15
    /// 说明：
    /// JSViewPageBase作为项目中所有view的基类，用于各Areas独立mvc项目中注册js文件
    /// 使用规则：
    ///     1.在MVC项目中按照Views目录的相同结构建立ViewScripts目录，每一个View文件对应ViewScripts对应目录下的同名JS文件
    ///       用于存放view所用的js脚本。view加载时会自动注册此js文件。
    /// Update Log:
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class JSViewPageBase<TModel> : WebViewPage<TModel>
    {
        protected override void InitializePage()
        {
            base.InitializePage();
            AddScriptForView(this.VirtualPath);
        }

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

        bool isPrincipalView = false;
        const string _viewScriptsKey = "__ViewScripts";

        private Dictionary<string, string> GetScriptsDictionary()
        {
            //throw new NotImplementedException();
            var ctx = Context;
            var dctScripts = default(Dictionary<string, string>);
            if (!ctx.Items.Contains(_viewScriptsKey))
            {
                //If the dictionary is not created yet, then this view is the principal
                isPrincipalView = true;
                dctScripts = new Dictionary<string, string>();
                ctx.Items[_viewScriptsKey] = dctScripts;

            }
            else
            {
                dctScripts = ctx.Items[_viewScriptsKey] as Dictionary<string, string>;
            }
            return dctScripts;
        }

        public override void ExecutePageHierarchy()
        {
            base.ExecutePageHierarchy();

            //This code will execute only for the principal view,
            //not for the layout view, start view, or partial views.
            if (isPrincipalView)
            {
                var dctScripts = GetScriptsDictionary();
                var sw = this.Output as StringWriter;
                var sb = sw.GetStringBuilder();

                //add here the javascript files for layout and viewstart
                AddScriptForView(this.Layout);
                AddScriptForView("~/Views/_ViewStart.cshtml");

                //Insert the scripts
                foreach (var pair in dctScripts)
                {
                    sb.Insert(0, pair.Value);
                }
            }
        }
    }

    public abstract class JSViewPageBase : JSViewPageBase<dynamic> { }
}
