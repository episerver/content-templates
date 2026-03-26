namespace Stride.Mvc._1.Models.ViewModels;

/// <summary>
/// View model for listing pages that display children of a specific type.
/// </summary>
public class ListingPageViewModel<TPage, TChild>(TPage currentPage) : PageViewModel<TPage>(currentPage)
    where TPage : PageData
    where TChild : PageData
{
    public IReadOnlyList<TChild> Items { get; set; } = [];
}
