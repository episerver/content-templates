using System.Net;
using System.Threading.Tasks;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Alloy.Mvc.Setup.Internal
{
    /// <summary>
    /// Evaluates whether the admin user registration feature should be enabled.
    /// </summary>
    public class RegisterAdminUserBehaviorEvaluator
    {
        private readonly UIUserProvider _userProvider;
        private readonly RegisterAdminUserOptions _registerAdminUserOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterAdminUserBehaviorEvaluator(
            UIUserProvider userProvider,
            IOptions<RegisterAdminUserOptions> registerAdminUserOptions,
            IHttpContextAccessor httpContextAccessor)
        {
            _userProvider = userProvider;
            _registerAdminUserOptions = registerAdminUserOptions.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Evaluates whether the admin user registration feature is enabled.
        /// </summary>
        /// <param name="forceBehavior">The behaviors to enforce when evaluating.</param>
        /// <returns>True if the admin user registration feature is enabled</returns>
        public async Task<bool> IsEnabledAsync(RegisterAdminUserBehaviors? forceBehavior = null)
        {
            var enabled = false;

            if (EvaluateBehavior(RegisterAdminUserBehaviors.Disabled))
            {
                return false;
            }

            if (EvaluateBehavior(RegisterAdminUserBehaviors.Enabled))
            {
                enabled = true;
            }

            if (EvaluateBehavior(RegisterAdminUserBehaviors.LocalRequestsOnly))
            {
                enabled = RequestIsLocal();
            }

            if (EvaluateBehavior(RegisterAdminUserBehaviors.SingleUser))
            {
                enabled = await UserDatabaseIsEmpty();
            }

            bool EvaluateBehavior(RegisterAdminUserBehaviors behavior)
            {
                var force = forceBehavior is not null && forceBehavior.Value.HasFlag(behavior);

                return force || _registerAdminUserOptions.Behaviors.HasFlag(behavior);
            }

            return enabled;
        }

        private bool RequestIsLocal()
        {
            var connection = _httpContextAccessor.HttpContext.Connection;

            static bool IsSet(IPAddress address) => address != null && address.ToString() != "::1";

            return !IsSet(connection.RemoteIpAddress) || IsSet(connection.LocalIpAddress)
                ? connection.LocalIpAddress.Equals(connection.RemoteIpAddress) // Is local same as remote, then we are local.
                : IPAddress.IsLoopback(connection.RemoteIpAddress); // Else we are remote if the remote IP address is not a loopback address.
        }

        private async Task<bool> UserDatabaseIsEmpty()
        {
            await foreach (var user in _userProvider.GetAllUsersAsync(0, 1))
            {
                return false;
            }

            return true;
        }
    }
}
