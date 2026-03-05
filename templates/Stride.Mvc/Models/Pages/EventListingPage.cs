using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc.Models.Pages;

[SiteContentType(
    GUID = "9E6A3C2F-7D8B-4A5E-8C4F-2D9B3A7E6C5F",
    DisplayName = "Event Listing",
    Description = "Events listing page")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
[AvailableContentTypes(Include = new[] { typeof(EventPage) })]
public class EventListingPage : SitePageData
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Heading { get; set; }

}
