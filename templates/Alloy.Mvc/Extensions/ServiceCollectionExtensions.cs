using Alloy.Mvc.Business;
using Alloy.Mvc.Business.Channels;
using Alloy.Mvc.Business.Rendering;
using EPiServer.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Alloy.Mvc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAlloy(this IServiceCollection services)
        {
            services.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()));

            services.Configure<DisplayOptions>(displayOption =>
            {
                displayOption.Add("full", "/displayoptions/full", Global.ContentAreaTags.FullWidth, "", "epi-icon__layout--full");
                displayOption.Add("wide", "/displayoptions/wide", Global.ContentAreaTags.TwoThirdsWidth, "", "epi-icon__layout--two-thirds");
                displayOption.Add("narrow", "/displayoptions/narrow", Global.ContentAreaTags.OneThirdWidth, "", "epi-icon__layout--one-third");
            });

            services.Configure<MvcOptions>(options => options.Filters.Add<PageContextActionFilter>());

            services.AddDisplayResolutions();
            services.AddDetection();

            return services;
        }

        private static void AddDisplayResolutions(this IServiceCollection services)
        {
            services.AddSingleton<StandardResolution>();
            services.AddSingleton<IpadHorizontalResolution>();
            services.AddSingleton<IphoneVerticalResolution>();
            services.AddSingleton<AndroidVerticalResolution>();
        }
    }
}
