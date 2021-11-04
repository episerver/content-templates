using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alloy.Mvc.Business.Rendering
{
    /// <summary>
    /// Extends the default <see cref="ContentAreaRenderer"/> to apply custom CSS classes to each <see cref="ContentFragment"/>.
    /// </summary>
    public class AlloyContentAreaRenderer : ContentAreaRenderer
    {
        protected override string GetContentAreaItemCssClass(IHtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
        {
            var baseItemClass = base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);
            var tag = GetContentAreaItemTemplateTag(htmlHelper, contentAreaItem);

            return $"block {baseItemClass} {GetTypeSpecificCssClasses(contentAreaItem)} {GetCssClassForTag(tag)} {tag}";
        }

        /// <summary>
        /// Gets a CSS class used for styling based on a tag name (ie a Bootstrap class name)
        /// </summary>
        /// <param name="tagName">Any tag name available, see <see cref="Global.ContentAreaTags"/></param>
        private static string GetCssClassForTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
            {
                return string.Empty;
            }

            return tagName.ToLowerInvariant() switch
            {
                "span12" => "full",
                "span8" => "wide",
                "span6" => "half",
                _ => string.Empty,
            };
        }

        private static string GetTypeSpecificCssClasses(ContentAreaItem contentAreaItem)
        {
            var content = contentAreaItem.GetContent();
            var cssClass = content == null ? string.Empty : content.GetOriginalType().Name.ToLowerInvariant();

            if (content is ICustomCssInContentArea customClassContent &&
                !string.IsNullOrWhiteSpace(customClassContent.ContentAreaCssClass))
            {
                cssClass += string.Format(" {0}", customClassContent.ContentAreaCssClass);
            }

            return cssClass;
        }
    }
}
