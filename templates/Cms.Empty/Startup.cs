using System;
using System.IO;
#if externalAuthentication
using System.Security.Claims;
using System.Threading.Tasks;
#endif
using EPiServer.Cms.Shell;
#if localAuthentication
using EPiServer.Cms.UI.AspNetIdentity;
#endif
#if enableRESTWithExternalAuthentication
using EPiServer.ContentApi.Cms;
#endif
#if enableREST
using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.DependencyInjection;
#endif
#if enableRESTWithExternalAuthentication
using EPiServer.ContentDefinitionsApi;
#endif
#if enableRESTWithLocalAuthentication
using EPiServer.OpenIDConnect;
#endif
using EPiServer.Scheduler;
#if externalAuthentication
using EPiServer.Security;
#endif
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
#if externalAuthentication
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
#endif

namespace Cms.Empty
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_webHostingEnvironment.IsDevelopment())
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

                services.Configure<SchedulerOptions>(options => options.Enabled = false);
            }

            services
#if localAuthentication
                .AddCmsAspNetIdentity<ApplicationUser>()
#endif
                .AddCms()
                .AddAdminUserRegistration()
                .AddEmbeddedLocalization<Startup>();

#if externalAuthentication
            // see https://world.optimizely.com/documentation/developer-guides/CMS/security/integrate-azure-ad-using-openid-connect/
            var authenticationScheme = "oidc";
            var cookieScheme = "oidc-cookie";

            services
                .AddAuthentication(authenticationScheme)
#if enableREST
                .AddJwtBearer("jwt", options =>
                {
                    options.Authority = _configuration.GetValue<string>("Authentication:Authority");
                    options.MapInboundClaims = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.Email,
                        RoleClaimType = ClaimTypes.Role,
                        ValidAudiences = new[]
                        {
                            ContentDefinitionsApiOptionsDefaults.Scope,
                            ContentDeliveryApiOptionsDefaults.Scope
                        }
                    };
                })
#endif
                .AddCookie(cookieScheme, options =>
                {
                    options.Cookie.Name = cookieScheme;
                    options.Events.OnSignedIn = async ctx =>
                    {
                        await ServiceLocator.Current.GetInstance<ISynchronizingUserService>()
                            .SynchronizeAsync(ctx.Principal!.Identity as ClaimsIdentity);
                    };
                })
                .AddOpenIdConnect(authenticationScheme, options =>
                {
                    options.Authority = _configuration.GetValue<string>("Authentication:Authority");
                    options.ClientId = _configuration.GetValue<string>("Authentication:ClientId");

                    options.SignInScheme = cookieScheme;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.CallbackPath = "/signin-oidc";
                    options.UsePkce = true;

                    options.Scope.Clear();
                    options.Scope.Add(OpenIdConnectScope.OpenId);
                    options.Scope.Add(OpenIdConnectScope.OfflineAccess);
                    options.Scope.Add(OpenIdConnectScope.Email);
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.Email,
                        RoleClaimType = ClaimTypes.Role
                    };

                    options.Events.OnRedirectToIdentityProvider = ctx =>
                    {
                        // Prevent redirect loop
                        if (ctx.Response.StatusCode == 401)
                        {
                            ctx.HandleResponse();
                        }

                        return Task.CompletedTask;
                    };
                });
#endif

#if enableRESTWithLocalAuthentication
            // See https://world.optimizely.com/documentation/developer-guides/content-delivery-api/getting-started/api-authentication/
            services.AddOpenIDConnect<ApplicationUser>(
                useDevelopmentCertificate: true,
                signingCertificate: null,
                encryptionCertificate: null,
                createSchema: true,
                options =>
                {
                    // See appSettings.Development.json for example on
                    // how to add applications via settings.
                    options.Applications.Add(new OpenIDConnectApplication
                    {
                        ClientId = "web-app",
                        Scopes = { "openid", "offline_access", "profile", "email", "roles" },
                        RedirectUris = { new Uri("https://localhost:8080/login-callback"), },
                    });
                });
#endif

#if enableRESTWithLocalAuthentication
            // See https://world.optimizely.com/documentation/developer-guides/content-delivery-api/
            services.AddContentDeliveryApi(OpenIDConnectOptionsDefaults.AuthenticationScheme);
#elif enableRESTWithExternalAuthentication
            // See https://world.optimizely.com/documentation/developer-guides/content-delivery-api/
            services.AddContentDeliveryApi(authenticationScheme);
#endif

#if enableREST
            services
                .ConfigureForExternalTemplates()
                .Configure<ContentApiOptions>(options =>
                {
                    options.ExpandedBehavior = ExpandedLanguageBehavior.RequestedLanguage;
                    options.FlattenPropertyModel = true;
                    options.ForceAbsolute = true;
                });
#endif

#if enableRESTWithLocalAuthentication
            // See https://world.optimizely.com/documentation/developer-guides/content-definitions-api/
            services.AddContentDefinitionsApi(OpenIDConnectOptionsDefaults.AuthenticationScheme);
#elif enableRESTWithExternalAuthentication
            // See https://world.optimizely.com/documentation/developer-guides/content-definitions-api/
            services.AddContentDefinitionsApi(authenticationScheme);
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

#if enableREST
            app.UseCors(builder => builder
                .WithOrigins(new[] { "https://localhost:8080" })
                .WithExposedContentDeliveryApiHeaders()
                .WithExposedContentDefinitionApiHeaders()
                .WithHeaders("Authorization")
                .AllowAnyMethod()
                .AllowCredentials());
#endif

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapContent();
            });
        }
    }
}
