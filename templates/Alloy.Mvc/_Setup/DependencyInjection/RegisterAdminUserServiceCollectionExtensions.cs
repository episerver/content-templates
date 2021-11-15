using System;
using Alloy.Mvc.Setup;
using Alloy.Mvc.Setup.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring and enabling the admin user registration feature.
    /// </summary>
    public static class RegisterAdminUserServiceCollectionExtensions
    {
        /// <summary>
        /// Registers and configures services for the admin user registration feature.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configureOptions">Registers an action used to configure <see cref="RegisterAdminUserOptions"/>.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddAdminUserRegistration(this IServiceCollection services, Action<RegisterAdminUserOptions> configureOptions = null)
        {
            var builder = services.AddOptions<RegisterAdminUserOptions>();
            if (configureOptions is not null)
            {
                builder.Configure(configureOptions);
            }

            services.AddSingleton<RegisterAdminUserBehaviorEvaluator>();

            return services;
        }

        /// <summary>
        /// Enables the admin user registration feature.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns> A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseAdminUserRegistration(this IApplicationBuilder app)
        {
            app.UseMiddleware<RegisterAdminUserMiddleware>();

            return app;
        }

        /// <summary>
        /// Adds endpoints for the admin user registration feature.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/>.</param>
        /// <returns>An <see cref="ControllerActionEndpointConventionBuilder"/> for endpoints associated with the admin user registration feature..</returns>
        public static ControllerActionEndpointConventionBuilder MapAdminUserRegistration(this IEndpointRouteBuilder endpoints)
        {
            var builder = endpoints.MapControllerRoute("RegisterAdminUser", RegisterAdminUserDefaults.Path, new { controller = "RegisterAdminUser", action = "Index" });

            return builder;
        }
    }
}
