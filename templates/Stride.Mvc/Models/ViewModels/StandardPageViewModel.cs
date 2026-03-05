using Stride.Mvc.Models.Pages;

namespace Stride.Mvc.Models.ViewModels;

/// <summary>
/// View model for StandardPage when used as a News &amp; Events overview container.
/// </summary>
public class StandardPageViewModel : PageViewModel<StandardPage>
{
    public StandardPageViewModel(StandardPage currentPage) : base(currentPage) { }

    public record NewsListingWithArticles(NewsListingPage Listing, IReadOnlyList<NewsArticlePage> Articles);
    public record EventListingWithEvents(EventListingPage Listing, IReadOnlyList<EventPage> Events);

    public IReadOnlyList<NewsListingWithArticles> NewsListings { get; set; } = [];
    public IReadOnlyList<EventListingWithEvents> EventListings { get; set; } = [];
    public bool HasListingChildren => NewsListings.Count > 0 || EventListings.Count > 0;
}
