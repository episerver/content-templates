using Stride.Mvc.Business;
using Stride.Mvc.Business.Initialization;
using Stride.Mvc.Business.Rendering;
using EPiServer.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Stride.Mvc.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAlloy(this IServiceCollection services)
    {
        services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander());
            options.ViewLocationExpanders.Add(new ExperienceViewEngineLocationExpander());
        });

        services.Configure<DisplayOptions>(displayOption =>
        {
            displayOption.Add("full", "/displayoptions/full", Globals.ContentAreaTags.FullWidth, string.Empty, "epi-icon__layout--full");
            displayOption.Add("wide", "/displayoptions/wide", Globals.ContentAreaTags.WideWidth, string.Empty, "epi-icon__layout--wide");
            displayOption.Add("half", "/displayoptions/half", Globals.ContentAreaTags.HalfWidth, string.Empty, "epi-icon__layout--half");
            displayOption.Add("narrow", "/displayoptions/narrow", Globals.ContentAreaTags.NarrowWidth, string.Empty, "epi-icon__layout--narrow");
        });

        services.Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());

        return services;
    }
}
