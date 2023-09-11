using Alloy.Mvc._1.Extensions;
using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using EPiServer.DependencyInjection;
using AlloyMvc1.Business.OptiGraph;
using AlloyMvc1;

namespace Alloy.Mvc._1;

public class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;

    public Startup(IWebHostEnvironment webHostingEnvironment)
    {
        _webHostingEnvironment = webHostingEnvironment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (_webHostingEnvironment.IsDevelopment())
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

            services.Configure<SchedulerOptions>(options => options.Enabled = false);
        }

        services
            .AddCmsAspNetIdentity<ApplicationUser>()
            .AddCms()
            .AddAlloy()
            .AddAdminUserRegistration()
            .AddEmbeddedLocalization<Startup>();

        // Required by Wangkanai.Detection
        services.AddDetection();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Opti Graph
        services.AddContentDeliveryApi();
        services.AddContentGraph();
        services.AddSingleton<SearchHandler>();
        services.AddSingleton<LocalesSerializer>();
        services.AddSingleton<PageListBlockHandler>();
        services
            .AddContentGraphClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://cg.optimizely.com/content/v2?auth=eBrGunULiC5TziTCtiOLEmov2LijBf30obh0KmhcBlyTktGZ"));

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Required by Wangkanai.Detection
        app.UseDetection();
        app.UseSession();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapContent();
        });
    }
}
