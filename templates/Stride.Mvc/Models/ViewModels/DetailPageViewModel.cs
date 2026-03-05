namespace Stride.Mvc._1.Models.ViewModels;

/// <summary>
/// View model for detail pages that display related sibling items.
/// </summary>
public class DetailPageViewModel<T> : PageViewModel<T>
    where T : PageData
{
    public DetailPageViewModel(T currentPage) : base(currentPage) { }

    public IReadOnlyList<T> RelatedItems { get; set; } = [];
}
