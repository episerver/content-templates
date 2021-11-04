using System.Collections.Generic;
using System.Threading.Tasks;
using Alloy.Mvc.Infrastructure;
using Alloy.Mvc.Models;
using EPiServer.Authorization;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Security;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc.Controllers
{
    /// <summary>
    /// Used to register a user for first time
    /// </summary>
    [RegisterFirstAdminWithLocalRequest]
    public class RegisterController : Controller
    {
        public const string ErrorKey = "CreateError";

        private readonly string _adminRoleName = Roles.WebAdmins;
        private readonly UIUserProvider _userProvider;
        private readonly UIRoleProvider _roleProvider;
        private readonly UISignInManager _signInManager;
        private readonly IContentSecurityRepository _contentSecurityRepository;

        public RegisterController(
            UIUserProvider userProvider,
            UIRoleProvider roleProvider,
            UISignInManager signInManager,
            IContentSecurityRepository contentSecurityRepository)
        {
            _userProvider = userProvider;
            _roleProvider = roleProvider;
            _signInManager = signInManager;
            _contentSecurityRepository = contentSecurityRepository;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryReleaseToken]
        public async Task<ActionResult> Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userProvider.CreateUserAsync(model.Username, model.Password, model.Email, null, null, true);
                if (result.Status == UIUserCreateStatus.Success)
                {
                    await _roleProvider.CreateRoleAsync(_adminRoleName);
                    await _roleProvider.AddUserToRolesAsync(result.User.Username, new string[] { _adminRoleName });

                    AdministratorRegistrationPageMiddleware.IsEnabled = false;
                    SetFullAccessToWebAdmin();

                    var resFromSignIn = await _signInManager.SignInAsync(model.Username, model.Password);
                    if (resFromSignIn)
                    {
                        return Redirect("/");
                    }
                }

                AddErrors(result.Errors);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void SetFullAccessToWebAdmin()
        {
            var permissions = _contentSecurityRepository.Get(ContentReference.RootPage).CreateWritableClone() as IContentSecurityDescriptor;
            permissions.AddEntry(new AccessControlEntry(_adminRoleName, AccessLevel.FullAccess));
            _contentSecurityRepository.Save(ContentReference.RootPage, permissions, SecuritySaveType.Replace);
        }

        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(ErrorKey, error);
            }
        }
    }
}
