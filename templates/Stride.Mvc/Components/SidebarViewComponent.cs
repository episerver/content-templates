using EPiServer.Applications;
using Stride.Mvc._1.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Components;

public class SidebarViewComponent : ViewComponent
{
    private readonly IContentLoader _contentLoader;
    private readonly IApplicationResolver _applicationResolver;

    public SidebarViewComponent(IContentLoader contentLoader, IApplicationResolver applicationResolver)
    {
        _contentLoader = contentLoader;
        _applicationResolver = applicationResolver;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var searchRoot = ContentReference.RootPage;
        try
        {
            var application = await _applicationResolver.GetByContextAsync(CancellationToken.None);
            if (application is IRoutableApplication routableApplication && !ContentReference.IsNullOrEmpty(routableApplication.EntryPoint))
            {
                searchRoot = routableApplication.EntryPoint;
            }
        }
        catch
        {
        }

        var newsArticles = new List<NewsArticlePage>();
        var events = new List<EventPage>();

        if (!ContentReference.IsNullOrEmpty(searchRoot))
        {
            foreach (var childRef in _contentLoader.GetDescendents(searchRoot).Take(500))
            {
                if (_contentLoader.TryGet<NewsArticlePage>(childRef, out var newsPage))
                {
                    newsArticles.Add(newsPage);
                }
                else if (_contentLoader.TryGet<EventPage>(childRef, out var eventPage))
                {
                    events.Add(eventPage);
                }
            }
        }

        var model = new SidebarViewModel
        {
            RecentNews = [.. newsArticles.OrderByDescending(n => n.PublishDate).Take(2)],
            UpcomingEvents = [.. events.OrderBy(e => e.EventDate).Take(2)]
        };

        return View(model);
    }
}

public class SidebarViewModel
{
    public IReadOnlyList<NewsArticlePage> RecentNews { get; set; } = [];
    public IReadOnlyList<EventPage> UpcomingEvents { get; set; } = [];
}
