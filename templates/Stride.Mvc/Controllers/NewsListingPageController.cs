using Stride.Mvc.Business.Rendering;
using Stride.Mvc.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc.Controllers;

public class NewsListingPageController : ListingPageController<NewsListingPage, NewsArticlePage>
{
    public NewsListingPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader)
        : base(uiSignInManager, themeService, contentLoader) { }

    protected override IEnumerable<NewsArticlePage> OrderItems(IEnumerable<NewsArticlePage> items)
        => items.OrderByDescending(x => x.PublishDate);
}
