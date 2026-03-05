using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Pages;

/// <summary>
/// News article page — each article is a child page under NewsListingPage.
/// The listing page queries its children to build the news feed.
/// </summary>
[SiteContentType(
    GUID = "8A4D7C2E-9F6B-4A3D-8E5C-2D9B7A3C6F4E",
    DisplayName = "News Article",
    Description = "Individual news article")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-article.png")]
[AvailableContentTypes(Availability.None)]
public class NewsArticlePage : ArticlePageData
{
    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }

    [Display(
        Name = "Publish Date",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [Required]
    public virtual DateTime PublishDate { get; set; }

    [Display(
        Name = "Featured Image",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference FeaturedImage { get; set; }

    [Display(
        Name = "Summary",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Summary { get; set; }
}
