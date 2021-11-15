using System.Threading.Tasks;
using Alloy.Mvc.Setup.Internal;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Security;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alloy.Mvc.Setup
{
    /// <summary>
    /// Used to register a first-time administrator user.
    /// The availability of these endpoint depends on the configuration
    /// in <see cref="RegisterAdminUserOptions.Behavior"/>.
    /// </summary>
    public class RegisterAdminUserController : Controller
    {
        private readonly UIUserProvider _userProvider;
        private readonly UIRoleProvider _roleProvider;
        private readonly UISignInManager _signInManager;
        private readonly RegisterAdminUserBehaviorEvaluator _registerAdminUserBehaviorEvaluator;
        private readonly RegisterAdminUserOptions _registerAdminUserOptions;
        private readonly IContentSecurityRepository _contentSecurityRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<RegisterAdminUserController> _logger;

        /// <summary>
        /// Constructs a new <see cref="RegisterAdminUserMiddleware"/> instance.
        /// </summary>
        /// <param name="userProvider">The <see cref="UIUserProvider"/>.</param>
        /// <param name="roleProvider">The <see cref="UIRoleProvider"/>.</param>
        /// <param name="signInManager">The <see cref="UISignInManager"/>.</param>
        /// <param name="registerAdminUserBehaviorEvaluator">The <see cref="RegisterAdminUserBehaviorEvaluator"/>.</param>
        /// <param name="registerAdminUserOptions">The <see cref="IOptions{RegisterAdminUserOptions}"/>.</param>
        /// <param name="webHostEnvironment">The <see cref="IWebHostEnvironment"/>.</param>
        /// <param name="contentSecurityRepository">The <see cref="IContentSecurityRepository"/>.</param>
        public RegisterAdminUserController(
            UIUserProvider userProvider,
            UIRoleProvider roleProvider,
            UISignInManager signInManager,
            RegisterAdminUserBehaviorEvaluator registerAdminUserBehaviorEvaluator,
            IOptions<RegisterAdminUserOptions> registerAdminUserOptions,
            IContentSecurityRepository contentSecurityRepository,
            IWebHostEnvironment webHostEnvironment,
            ILogger<RegisterAdminUserController> logger)
        {
            _userProvider = userProvider;
            _roleProvider = roleProvider;
            _signInManager = signInManager;
            _registerAdminUserBehaviorEvaluator = registerAdminUserBehaviorEvaluator;
            _registerAdminUserOptions = registerAdminUserOptions.Value;
            _contentSecurityRepository = contentSecurityRepository;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        /// <summary>
        /// The GET endpoint.
        /// </summary>
        public IActionResult Index() => View(RegisterAdminUserDefaults.ViewName);

        /// <summary>
        /// The POST endpoint
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryReleaseToken]
        public async Task<ActionResult> Index(RegisterAdminUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createResult = await _userProvider.CreateUserAsync(model.Username, model.Password, model.Email, null, null, true);
                if (createResult.Status == UIUserCreateStatus.Success)
                {
                    await CreateRolesAndAssignToUser(createResult.User.Username);

                    SetFullAccessForRoles();

                    if (await _signInManager.SignInAsync(model.Username, model.Password))
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }

            return View(RegisterAdminUserDefaults.ViewName, model);
        }

        /// <summary>
        /// Makes sure the endpoints are only available when then admin user registration
        /// feature is enabled.
        /// </summary>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (await _registerAdminUserBehaviorEvaluator.IsEnabledAsync())
            {
                if (!_webHostEnvironment.IsDevelopment())
                {
                    _logger.LogCritical(
                        "Access to the admin user registration is enabled. Following behaviors were evaluated '{0}'.",
                        _registerAdminUserOptions.Behavior);
                }
                else
                {
                    _logger.LogWarning(
                        "Access to the admin user registration is enabled. Following behaviors were evaluated '{0}'.",
                        _registerAdminUserOptions.Behavior);
                }

                await base.OnActionExecutionAsync(context, next);
            }
            else
            {
                _logger.LogWarning(
                    "Access to the admin user registration was denied, the feature is disabled. Following behaviors were evaluated '{0}'.",
                    _registerAdminUserOptions.Behavior);

                context.Result = new NotFoundResult();

                return;
            }
        }

        private async Task CreateRolesAndAssignToUser(string username)
        {
            foreach (var role in _registerAdminUserOptions.Roles)
            {
                await _roleProvider.CreateRoleAsync(role);
            }

            await _roleProvider.AddUserToRolesAsync(username, _registerAdminUserOptions.Roles);
        }

        private void SetFullAccessForRoles()
        {
            var permissions = (IContentSecurityDescriptor)_contentSecurityRepository.Get(ContentReference.RootPage).CreateWritableClone();

            foreach (var role in _registerAdminUserOptions.Roles)
            {
                permissions.AddEntry(new AccessControlEntry(role, AccessLevel.FullAccess));
            }

            _contentSecurityRepository.Save(ContentReference.RootPage, permissions, SecuritySaveType.MergeChildPermissions);
        }
    }
}
