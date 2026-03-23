using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Controllers;

/// <summary>
/// Generic base controller for listing pages that display children of a specific type.
/// </summary>
public abstract class ListingPageController<TPage, TChild>(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader) : PageControllerBase<TPage>(uiSignInManager, themeService)
    where TPage : PageData
    where TChild : PageData
{
    protected virtual int PageSize => 12;

    public virtual ViewResult Index(TPage currentPage)
    {
        var children = contentLoader.GetChildren<TChild>(currentPage.ContentLink);
        var model = new ListingPageViewModel<TPage, TChild>(currentPage)
        {
            Items = [.. OrderItems(children).Take(PageSize)]
        };
        return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", model);
    }

    /// <summary>
    /// Override to customize sort order.
    /// </summary>
    protected virtual IEnumerable<TChild> OrderItems(IEnumerable<TChild> items)
        => items.OrderByDescending(x => x.Changed);
}
