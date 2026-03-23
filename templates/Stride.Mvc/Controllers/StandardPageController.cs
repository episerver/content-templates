using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.Pages;
using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Shell.Security;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Controllers;

public class StandardPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader) : PageControllerBase<StandardPage>(uiSignInManager, themeService)
{
    public ViewResult Index(StandardPage currentPage)
    {
        var newsListings = contentLoader.GetChildren<NewsListingPage>(currentPage.ContentLink)
            .Select(listing => new StandardPageViewModel.NewsListingWithArticles(
                listing,
                [.. contentLoader.GetChildren<NewsArticlePage>(listing.ContentLink)
                    .OrderByDescending(n => n.PublishDate)
                    .Take(3)]))
            .Where(x => x.Articles.Count > 0)
            .ToList();

        var eventListings = contentLoader.GetChildren<EventListingPage>(currentPage.ContentLink)
            .Select(listing => new StandardPageViewModel.EventListingWithEvents(
                listing,
                [.. contentLoader.GetChildren<EventPage>(listing.ContentLink)
                    .OrderBy(e => e.EventDate)
                    .Take(2)]))
            .Where(x => x.Events.Count > 0)
            .ToList();

        var model = new StandardPageViewModel(currentPage)
        {
            NewsListings = newsListings,
            EventListings = eventListings
        };

        return View("~/Views/StandardPage/Index.cshtml", model);
    }
}
