using Stride.Mvc.Business.Rendering;
using Stride.Mvc.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc.Controllers;

public class NewsArticlePageController : DetailPageController<NewsArticlePage>
{
    public NewsArticlePageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader)
        : base(uiSignInManager, themeService, contentLoader) { }

    protected override IEnumerable<NewsArticlePage> OrderRelated(IEnumerable<NewsArticlePage> items)
        => items.OrderByDescending(x => x.PublishDate);
}
