using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc.Models.Pages;

[SiteContentType(
    GUID = "2F9A7C4E-8D3B-4A6D-9C5E-7B2F8A4D3C6E",
    DisplayName = "About Page",
    Description = "About/overview page")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
public class AboutPage : StandardPage
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Heading { get; set; }

    [Display(
        Name = "Intro Text",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString IntroText { get; set; }
}
