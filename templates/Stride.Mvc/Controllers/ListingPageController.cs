using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Controllers;

/// <summary>
/// Generic base controller for listing pages that display children of a specific type.
/// </summary>
public abstract class ListingPageController<TPage, TChild> : PageControllerBase<TPage>
    where TPage : PageData
    where TChild : PageData
{
    protected readonly IContentLoader ContentLoader;

    protected virtual int PageSize => 12;

    protected ListingPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader)
        : base(uiSignInManager, themeService)
    {
        ContentLoader = contentLoader;
    }

    public virtual ViewResult Index(TPage currentPage)
    {
        var children = ContentLoader.GetChildren<TChild>(currentPage.ContentLink);
        var model = new ListingPageViewModel<TPage, TChild>(currentPage)
        {
            Items = OrderItems(children).Take(PageSize).ToList()
        };
        return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", model);
    }

    /// <summary>
    /// Override to customize sort order.
    /// </summary>
    protected virtual IEnumerable<TChild> OrderItems(IEnumerable<TChild> items)
        => items.OrderByDescending(x => x.Changed);
}
