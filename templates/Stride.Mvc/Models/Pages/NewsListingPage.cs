using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc._1.Models.Pages;

[SiteContentType(
    GUID = "7F2D9A4E-8C6B-4A3E-9D5C-2F8B7A3D6E4C",
    DisplayName = "News Listing",
    Description = "News articles listing page")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
[AvailableContentTypes(Include = new[] { typeof(NewsArticlePage) })]
public class NewsListingPage : SitePageData
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Heading { get; set; }

}
