using System;
using System.Collections.Generic;
using System.Web.Mvc;
using YOYO.HRMS.BusinessLogic;

namespace YOYO.HRMS.MVC.Html.Generators
{
    /// <summary>
    /// Used to generate SELECT tags
    /// </summary>
    public class SelectGenerator : FormTagGenerator
    {
        protected override IEnumerable<NestedTagBuilder> GenerateTags()
        {
            if (typeof (Enum).IsAssignableFrom(Context.Metadata.ModelType))
                return GenerateForEnum(Context);

            var selectContext = Context as SelectContext;
            if (selectContext == null)
                throw new InvalidOperationException("Only SelectContext is supported.");

            var tagBuilder = CreatePrimaryTag("select");

            var value = GetValue();
            tagBuilder.AddChildren(GenerateOptions(selectContext.ListItems, value, selectContext.Formatter));
            return new[] {tagBuilder};
        }


        public virtual IEnumerable<NestedTagBuilder> GenerateForEnum(ITagBuilderContext context)
        {
            Setup(context);

            var selectTag = CreatePrimaryTag("select");
            if (!string.IsNullOrEmpty(Context.Metadata.Description))
                selectTag.MergeAttribute("title", Context.Metadata.Description);

            foreach (var enumName in Enum.GetNames(Context.Metadata.ModelType))
            {
                var tagBuilder = new NestedTagBuilder("option");
                tagBuilder.MergeAttribute("value", enumName, true);
                if (GetValue() == enumName)
                    tagBuilder.MergeAttribute("selected", "selected");

                long corporateID = CurrentParemeter.GetCurrentCorporateId();
                var title = LocalizedStringProvider.GetEnumString(corporateID,Context.Metadata.ModelType, enumName);
                tagBuilder.SetInnerText(title);

                selectTag.AddChild(tagBuilder);
            }

            return new[] {selectTag};
        }
    }
}