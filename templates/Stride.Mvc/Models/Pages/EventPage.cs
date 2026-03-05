using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Pages;

/// <summary>
/// Event detail page — each event is a child page under EventListingPage.
/// The listing page queries its children to build the events grid.
/// Each event gets its own URL for linking and sharing.
/// </summary>
[SiteContentType(
    GUID = "5C9E3A2F-7D8B-4A6C-9E4F-2D8B3A7C5E6F",
    DisplayName = "Event",
    Description = "Event detail page")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-article.png")]
[AvailableContentTypes(Availability.None)]
public class EventPage : ArticlePageData
{
    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }

    [Display(
        Name = "Event Date",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [Required]
    public virtual DateTime EventDate { get; set; }

    [Display(
        Name = "Location",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual string Location { get; set; }

    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Description { get; set; }
}
