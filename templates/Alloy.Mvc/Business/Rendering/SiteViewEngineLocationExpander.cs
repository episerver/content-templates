using Microsoft.AspNetCore.Mvc.Razor;
using System.Collections.Generic;

namespace Alloy.Mvc._1.Business.Rendering;

public class SiteViewEngineLocationExpander : IViewLocationExpander
{
    private static readonly string[] AdditionalPartialViewFormats = new[]
    {
        TemplateCoordinator.BlockFolder + "{0}.cshtml",
        TemplateCoordinator.PagePartialsFolder + "{0}.cshtml"
    };

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        foreach (var location in viewLocations)
        {
            yield return location;
        }

        for (var i = 0; i < AdditionalPartialViewFormats.Length; i++)
        {
            yield return AdditionalPartialViewFormats[i];
        }
    }

    public void PopulateValues(ViewLocationExpanderContext context) { }
}
