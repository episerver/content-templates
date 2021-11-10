using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Alloy.Mvc.Setup.Internal
{
    /// <summary>
    /// Middleware responsible for redirecting request to the root
    /// depending on the configured behavior in <see cref="RegisterAdminUserOptions.Behaviors"/>.
    /// </summary>
    internal class RegisterAdminUserMiddleware
    {
        private static bool _isFirstRequest = true;

        private readonly RequestDelegate _next;
        private readonly RegisterAdminUserBehaviorEvaluator _registerAdminUserBehaviorEvaluator;
        private readonly ILogger<RegisterAdminUserMiddleware> _logger;

        /// <summary>
        /// Constructs a new <see cref="RegisterAdminUserMiddleware"/> instance with a
        /// given <see cref="RegisterAdminUserBehaviorEvaluator"/>.
        /// </summary>
        /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
        /// <param name="registerAdminUserBehaviorEvaluator">The <see cref="RegisterAdminUserBehaviorEvaluator"/>.</param>
        /// <param name="logger">The logger.</param>
        public RegisterAdminUserMiddleware(
            RequestDelegate next,
            RegisterAdminUserBehaviorEvaluator registerAdminUserBehaviorEvaluator,
            ILogger<RegisterAdminUserMiddleware> logger)
        {
            _next = next;
            _registerAdminUserBehaviorEvaluator = registerAdminUserBehaviorEvaluator;
            _logger = logger;
        }

        /// <summary>
        /// Evaluates the request using the <see cref="RegisterAdminUserBehaviorEvaluator"/> and
        /// redirects to the admin user registration if the admin user registration is enabled.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> instance.</param>
        /// <returns>A task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!_isFirstRequest || context.Request.Path != "/")
            {
                await _next(context);
                return;
            }

            _isFirstRequest = false;

            if (await _registerAdminUserBehaviorEvaluator.IsEnabledAsync(forceBehavior: RegisterAdminUserBehaviors.SingleUser))
            {
                _logger.LogInformation(
                    "This is the first request to '/'´, redirecting to '{0}'.",
                    RegisterAdminUserDefaults.Path);

                context.Response.Redirect(RegisterAdminUserDefaults.Path);
            }
            else
            {
                _logger.LogInformation(
                    "The admin user registration is disabled, no redirect to '{0}' will happen.",
                    RegisterAdminUserDefaults.Path);
            }

            await _next(context);
        }
    }
}
