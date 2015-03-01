using System.Web.Mvc;
using CircleOfFunk.Extentions;

namespace CircleOfFunk.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ExternalLink(this HtmlHelper htmlHelper, string linkText, string externalUrl, string cssClass, string identity = "")
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.Attributes["href"] = externalUrl;
            tagBuilder.Attributes["target"] = "_blank";
            tagBuilder.AddClass(cssClass);
            tagBuilder.AddIdentity(identity);
            tagBuilder.InnerHtml = linkText;

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString Div(this HtmlHelper htmlHelper, string text, string cssClass, string identity = "")
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddClass(cssClass);
            tagBuilder.AddIdentity(identity);
            tagBuilder.InnerHtml = text;

            return new MvcHtmlString(tagBuilder.ToString());
        }

        public static MvcHtmlString Paragraph(this HtmlHelper htmlHelper, string text)
        {
            var tagBuilder = new TagBuilder("p")
                {
                    InnerHtml = text
                };

            return new MvcHtmlString(tagBuilder.ToString());
        }
    }
}