using EPiServer.VisualBuilder;
using EPiServer.VisualBuilder.Compositions;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Mvc.TagHelpers.Experience.Internal;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Stride.Mvc._1.Models;

#nullable enable

/// <summary>
/// Extension methods for working with Visual Builder compositions.
/// Based on OptiAlloy sample implementation.
/// </summary>
public static class CompositionExtensions
{
    /// <summary>
    /// Gets the current rendering node from ViewData.
    /// </summary>
    private static CompositionNode? GetCurrentRenderingNode(this ViewDataDictionary viewData) =>
        viewData.TryGetCompositionRenderingNode(out var compositionRenderingNode) ? compositionRenderingNode : null;

    /// <summary>
    /// Gets display settings for the current rendering node.
    /// </summary>
    private static IDictionary<string, string>? GetDisplaySettings(this ViewDataDictionary viewData) =>
        viewData.GetCurrentRenderingNode()?.DisplaySettings;

    /// <summary>
    /// Gets CSS class from component display settings using a mapping function.
    /// Usage: ViewData.GetComponentCss("level", value => value == "h1" ? "text-6xl" : "text-4xl")
    /// </summary>
    public static string GetComponentCss<TModel>(
        this ViewDataDictionary<TModel> viewData,
        string key,
        Func<string, string> mapFunction,
        string defaultCss = "")
    {
        var displaySettings = viewData.GetDisplaySettings();

        if (displaySettings is null || !displaySettings.TryGetValue(key, out var setting))
        {
            return defaultCss;
        }

        return mapFunction(setting);
    }

    /// <summary>
    /// Gets CSS class from component display settings using a dictionary map.
    /// Usage: ViewData.GetComponentCss("padding", new Dictionary { { "small", "p-2" }, { "large", "p-8" } })
    /// </summary>
    public static string GetComponentCss<TModel>(
        this ViewDataDictionary<TModel> viewData,
        string key,
        IReadOnlyDictionary<string, string> map,
        string defaultCss = "")
    {
        var displaySettings = viewData.GetDisplaySettings();

        if (displaySettings is not null && displaySettings.TryGetValue(key, out var setting))
        {
            if (map.TryGetValue(setting, out var css))
            {
                return css;
            }
        }

        return defaultCss;
    }

    /// <summary>
    /// Gets CSS class from composition node display settings.
    /// Usage: node.GetCss("margin", new Dictionary { { "top", "mt-3" }, { "bottom", "mb-3" } })
    /// </summary>
    public static string? GetCss(
        this CompositionNode node,
        string key,
        Dictionary<string, string> map,
        string? defaultCss = null)
    {
        if (node.DisplaySettings.TryGetValue(key, out var setting))
        {
            if (map.TryGetValue(setting, out var css))
            {
                return css;
            }
        }
        return defaultCss;
    }

    /// <summary>
    /// Adds edit mode attributes to HTML elements.
    /// Usage: @Html.EditAttributes(ViewData["Node"] as ComponentNode)
    /// </summary>
    public static IHtmlContent EditAttributes(this IHtmlHelper html, CompositionNode? node)
    {
        if (!string.IsNullOrWhiteSpace(node?.Key)
            && html.ViewContext.HttpContext.RequestServices.GetRequiredService<IContextModeResolver>().CurrentMode == ContextMode.Edit)
        {
            return new HtmlString($"data-epi-block-id=\"{node.Key}\"");
        }

        return HtmlString.Empty;
    }

    /// <summary>
    /// Renders a section or block from its template.
    /// Usage: await Html.RenderCompositionSectionOrBlock(section)
    /// </summary>
    public static Task RenderCompositionSectionOrBlock(this IHtmlHelper html, CompositionNode node)
    {
        return node switch
        {
            SectionNode sectionNode => html.RenderContentDataAsync(
                sectionNode.SectionData,
                false,
                [sectionNode.DisplayTemplateKey],
                new { Node = sectionNode }),
            ComponentNode componentNode => html.RenderContentDataAsync(
                componentNode.Component,
                false,
                [componentNode.DisplayTemplateKey],
                new { Node = componentNode }),
            _ => Task.CompletedTask
        };
    }
}
