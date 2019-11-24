using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ExaminationSystem.MvcCoreWebUI.TagHelpers
{
    [HtmlTargetElement("list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }

        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        [HtmlAttributeName("controller-name")]
        public string ControllerName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ul class='pagination'>");

            for (int i = 1; i <= PageCount; i++)
            {
                stringBuilder.AppendFormat("<li class='{0}'>", i == CurrentPage ? "Active" : "");
                stringBuilder.AppendFormat("<a href='/{0}/index?page={1}'>{2}</a>", ControllerName, i, i);
                stringBuilder.AppendFormat("</li>");
            }

            output.Content.SetHtmlContent(stringBuilder.ToString());

            base.Process(context, output);
        }
    }
}