using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc.Models.Pages;

/// <summary>
/// Base class for content-heavy pages like news articles and events
/// </summary>
public abstract class ArticlePageData : SitePageData
{
    [Display(
        Name = "Main Content",
        GroupName = SystemTabNames.Content,
        Order = 310)]
    [CultureSpecific]
    public virtual XhtmlString MainContent { get; set; }
}
