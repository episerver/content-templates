using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc._1.Controllers;

public class NewsListingPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader) : ListingPageController<NewsListingPage, NewsArticlePage>(uiSignInManager, themeService, contentLoader)
{
    protected override IEnumerable<NewsArticlePage> OrderItems(IEnumerable<NewsArticlePage> items)
        => items.OrderByDescending(x => x.PublishDate);
}
