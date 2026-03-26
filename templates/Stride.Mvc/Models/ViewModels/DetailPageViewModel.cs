namespace Stride.Mvc._1.Models.ViewModels;

/// <summary>
/// View model for detail pages that display related sibling items.
/// </summary>
public class DetailPageViewModel<T>(T currentPage) : PageViewModel<T>(currentPage)
    where T : PageData
{
    public IReadOnlyList<T> RelatedItems { get; set; } = [];
}
