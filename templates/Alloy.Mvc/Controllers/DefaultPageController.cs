using Alloy.Mvc._1.Models.Pages;
using Alloy.Mvc._1.Models.ViewModels;
using EPiServer.Framework.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc._1.Controllers;

/// <summary>
/// Concrete controller that handles all page types that don't have their own specific controllers.
/// </summary>
/// <remarks>
/// Note that as the view file name is hard coded it won't work with DisplayModes (ie Index.mobile.cshtml).
/// For page types requiring such views add specific controllers for them. Alternatively the Index action
/// could be modified to set ControllerContext.RouteData.Values["controller"] to type name of the currentPage
/// argument. That may however have side effects.
/// </remarks>
[TemplateDescriptor(Inherited = true)]
public class DefaultPageController : PageControllerBase<SitePageData>
{
    public ViewResult Index(SitePageData currentPage)
    {
        var model = CreateModel(currentPage);
        return View($"~/Views/{currentPage.GetOriginalType().Name}/Index.cshtml", model);
    }

    /// <summary>
    /// Creates a PageViewModel where the type parameter is the type of the page.
    /// </summary>
    /// <remarks>
    /// Used to create models of a specific type without the calling method having to know that type.
    /// </remarks>
    private static IPageViewModel<SitePageData> CreateModel(SitePageData page)
    {
        var type = typeof(PageViewModel<>).MakeGenericType(page.GetOriginalType());
        return Activator.CreateInstance(type, page) as IPageViewModel<SitePageData>;
    }
}
