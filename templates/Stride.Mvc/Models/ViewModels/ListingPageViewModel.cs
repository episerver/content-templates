namespace Stride.Mvc._1.Models.ViewModels;

/// <summary>
/// View model for listing pages that display children of a specific type.
/// </summary>
public class ListingPageViewModel<TPage, TChild> : PageViewModel<TPage>
    where TPage : PageData
    where TChild : PageData
{
    public ListingPageViewModel(TPage currentPage) : base(currentPage) { }

    public IReadOnlyList<TChild> Items { get; set; } = [];
}
