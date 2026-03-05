using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc._1.Controllers;

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
