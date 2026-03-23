using Stride.Mvc._1.Business;
using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.Pages;
using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Shell.Security;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Stride.Mvc._1.Controllers;

/// <summary>
/// All controllers that renders pages should inherit from this class so that we can
/// apply action filters, such as for output caching site wide, should we want to.
/// </summary>
public abstract class PageControllerBase<T>(UISignInManager uISignInManager, ThemeService themeService) : PageController<T>, IModifyLayout
    where T : PageData
{

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        // Apply theme CSS classes to ViewBag for categorizable content
        if (context.Result is ViewResult && PageContext.Content is ICategorizable categorizableContent)
        {
            var themeCssClasses = themeService.GetThemeCssClassNames(categorizableContent);
            ViewBag.ThemeCssClasses = string.Join(" ", themeCssClasses);
        }
    }

    /// <summary>
    /// Signs out the current user and redirects to the Index action of the same controller.
    /// </summary>
    /// <remarks>
    /// There's a log out link in the footer which should redirect the user to the same page.
    /// As we don't have a specific user/account/login controller but rely on the login URL for
    /// forms authentication for login functionality we add an action for logging out to all
    /// controllers inheriting from this class.
    /// </remarks>
    public async Task<IActionResult> Logout()
    {
        await uISignInManager.SignOutAsync();
        return Redirect(HttpContext.RequestServices.GetService<UrlResolver>().GetUrl(PageContext.ContentLink, PageContext.LanguageID));
    }

    public virtual void ModifyLayout(LayoutModel layoutModel)
    {
        if (PageContext.Content is SitePageData page)
        {
            layoutModel.HideHeader = page.HideSiteHeader;
            layoutModel.HideFooter = page.HideSiteFooter;
        }
    }
}
