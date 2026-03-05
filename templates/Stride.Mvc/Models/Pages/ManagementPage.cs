using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc.Models.Pages;

[SiteContentType(
    GUID = "3E7C9A2F-8D4B-4A6E-9C3F-5D7B2A8E4C6F",
    DisplayName = "Management Page",
    Description = "Team management/profiles page")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-standard.png")]
public class ManagementPage : SitePageData
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
        Name = "Team Members",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [AllowedTypes(typeof(Blocks.TeamMemberElement))]
    public virtual ContentArea TeamMembers { get; set; }
}
