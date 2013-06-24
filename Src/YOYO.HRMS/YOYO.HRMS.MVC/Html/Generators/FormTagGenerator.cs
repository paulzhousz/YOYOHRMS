
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using YOYO.HRMS.BusinessLogic;
using YOYO.HRMS.BusinessLogic.SystemManagement;

namespace YOYO.HRMS.MVC.Html.Generators
{
    /// <summary>
    /// Base class for all FORM tag generators
    /// </summary>
    /// <remarks>
    /// Tag generators are used in MVC views to generate HTML tags with the help of html helpers.
    /// </remarks>
    public abstract class FormTagGenerator : ITagBuilder
    {
        private ILocalizedTypeService _languageProvider;

        /// <summary>
        /// Gets generator context
        /// </summary>
        /// <remarks>Contains information used to generate tags such as ModelMetaData.</remarks>
        protected ITagBuilderContext Context { get; private set; }

        /// <summary>
        /// Gets provider used to load localized strings from any source
        /// </summary>
        protected ILocalizedTypeService LocalizedStringProvider
        {
            get
            {
                if (_languageProvider==null)
                    _languageProvider = DependencyResolver.Current.GetService<ILocalizedTypeService>();
                return _languageProvider; 
            }
        }

        /// <summary>
        /// Generate options
        /// </summary>
        /// <param name="items">Items used to generate options</param>
        /// <param name="selectedValue">Selected value</param>
        /// <param name="formatter">Formatter used to find label/value. May be null if the list contains <c>SelectListItem</c></param>
        /// <returns>Generated <c>option</c> tags</returns>
        protected IEnumerable<NestedTagBuilder> GenerateOptions(IEnumerable items, string selectedValue,
                                                                ISelectItemFormatter formatter)
        {
            if (formatter == null)
            {
                if (!(items is IEnumerable<SelectListItem>))
                    throw new InvalidOperationException("No formatter was specified and the list do not contain SelectListItems");

                return GenerateOptions((IEnumerable<SelectListItem>)items, selectedValue);
            }

            var listItems = new List<NestedTagBuilder>();
            foreach (var item in items)
            {
                var tag = new NestedTagBuilder("option");
                var listItem = formatter.Generate(item);
                tag.MergeAttribute("value", listItem.Value);
                if (listItem.Value == selectedValue || listItem.Selected)
                    tag.MergeAttribute("selected", "selected");
                tag.SetInnerText(listItem.Text);
                listItems.Add(tag);
            }

            return listItems;
        }


        /// <summary>
        /// Generate options from a a list of items
        /// </summary>
        /// <param name="items">Items to generate option tags for</param>
        /// <param name="selectedValue">Selected value</param>
        /// <returns>Generated option tags</returns>
        protected virtual IEnumerable<NestedTagBuilder> GenerateOptions(IEnumerable<SelectListItem> items, string selectedValue)
        {
            var listItems = new List<NestedTagBuilder>();
            foreach (SelectListItem listItem in items)
            {
                var tag = new NestedTagBuilder("option");
                tag.MergeAttribute("value", listItem.Value);
                if (listItem.Value == selectedValue || listItem.Selected)
                    tag.MergeAttribute("selected", "selected");
                tag.SetInnerText(listItem.Text);
                listItems.Add(tag);
            }

            return listItems;
        }

        /// <summary>
        /// I know, really. Setup/Init methods are a pain and spawn of satan and all that. But I couldn't figure out a  better solution.
        /// </summary>
        /// <param name="context"></param>
        protected virtual void Setup(ITagBuilderContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Generate HTML tags for a property
        /// </summary>
        /// <param name="context">Context specific information</param>
        /// <returns>Generated HTML tags</returns>
        public IEnumerable<NestedTagBuilder> Generate(ITagBuilderContext context)
        {
            Setup(context);
            return GenerateTags();
        }

        /// <summary>
        /// Generated all used tags.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<NestedTagBuilder> GenerateTags();


        /// <summary>
        /// Generates the primary/root tag (which will contain child tags if required)
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>Tag builder for the tag</returns>
        /// <remarks>Adds all HTML attributes which has been specified in the Context, the name and the ID. Will
        /// also add any validation state members.</remarks>
        protected NestedTagBuilder CreatePrimaryTag(string tagName)
        {
            var tagBuilder = new NestedTagBuilder(tagName);
            tagBuilder.MergeAttributes(Context.HtmlAttributes);
            tagBuilder.MergeAttribute("name", Context.FullName, true);
            tagBuilder.GenerateId(Context.FullName);
            SetValidationState(tagBuilder, Context.FullName);
            return tagBuilder;
        }

        /// <summary>
        /// Get value for the model
        /// </summary>
        /// <returns>Model value</returns>
        /// <remarks>Value will either be the one from the previous POST or the one assigned in the model.</remarks>
        protected virtual string GetValue()
        {
            ModelState modelState;
            if (Context.ViewContext.ViewData.ModelState.TryGetValue(Context.Name, out modelState) && modelState.Value != null)
            {
                return Convert.ToString(modelState.Value, CultureInfo.CurrentUICulture);
            }

            return !string.IsNullOrEmpty(Context.Metadata.EditFormatString)
                       ? string.Format(Context.Metadata.EditFormatString, Context.Metadata.Model)
                       : Convert.ToString(Context.Metadata.Model, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        /// Gets the full name of the HTML field.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>dot notated field name</returns>
        protected string GetFullHtmlFieldName(string name)
        {
            return Context.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
        }

        private void SetValidationState(NestedTagBuilder tagBuilder, string fullName)
        {
            ModelState modelState;
            if (Context.ViewContext.ViewData.ModelState.TryGetValue(fullName, out modelState) && modelState.Errors.Count > 0)
            {
                tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            }
        }
    }
}