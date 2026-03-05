using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Pages;

[SiteContentType(
    GUID = "6D8B3F2A-9E5C-4D7A-8C4F-2D9B7A3E6C5F",
    DisplayName = "Contact Page",
    Description = "Contact information page")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
public class ContactPage : SitePageData
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

    [Display(
        Name = "Support Email",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [EmailAddress]
    public virtual string SupportEmail { get; set; }

    [Display(
        Name = "Press Email",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [EmailAddress]
    public virtual string PressEmail { get; set; }

    [Display(
        Name = "Phone Number",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [Phone]
    public virtual string PhoneNumber { get; set; }
}
