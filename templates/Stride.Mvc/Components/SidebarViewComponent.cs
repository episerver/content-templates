using EPiServer.Applications;
using Stride.Mvc._1.Models.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Components;

public class SidebarViewComponent(IContentLoader contentLoader, IApplicationResolver applicationResolver) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var newsArticles = new List<NewsArticlePage>();
        var events = new List<EventPage>();

        if (!ContentReference.IsNullOrEmpty(ContentReference.RootPage))
        {
            foreach (var childRef in contentLoader.GetDescendents(ContentReference.RootPage).Take(500))
            {
                if (contentLoader.TryGet<NewsArticlePage>(childRef, out var newsPage))
                {
                    newsArticles.Add(newsPage);
                }
                else if (contentLoader.TryGet<EventPage>(childRef, out var eventPage))
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
