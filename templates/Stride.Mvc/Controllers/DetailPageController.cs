using Stride.Mvc.Business.Rendering;
using Stride.Mvc.Models.ViewModels;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc.Controllers;

/// <summary>
/// Generic base controller for detail pages that display related sibling items.
/// </summary>
public abstract class DetailPageController<T> : PageControllerBase<T>
    where T : PageData
{
    protected readonly IContentLoader ContentLoader;

    protected virtual int RelatedItemsLimit => 3;

    protected DetailPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader)
        : base(uiSignInManager, themeService)
    {
        ContentLoader = contentLoader;
    }

    public virtual ViewResult Index(T currentPage)
    {
        var siblings = ContentLoader.GetChildren<T>(currentPage.ParentLink)
            .Where(p => !p.ContentLink.CompareToIgnoreWorkID(currentPage.ContentLink));

        var model = new DetailPageViewModel<T>(currentPage)
        {
            RelatedItems = [.. OrderRelated(siblings).Take(RelatedItemsLimit)]
        };
        return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", model);
    }

    /// <summary>
    /// Override to customize sort order.
    /// </summary>
    protected virtual IEnumerable<T> OrderRelated(IEnumerable<T> items)
        => items.OrderByDescending(x => x.Changed);
}
