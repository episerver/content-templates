using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "8D3F6A2C-9E5B-4D7A-8C4F-3B7D9A2E5C6F",
    DisplayName = "Team Member",
    Description = "Team member profile card",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class TeamMemberElement : SiteBlockData
{
    [Display(
        Name = "Name",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Name { get; set; }

    [Display(
        Name = "Bio",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Bio { get; set; }

    [Display(
        Name = "Email",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [EmailAddress]
    public virtual string Email { get; set; }

    [Display(
        Name = "Phone",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [Phone]
    public virtual string Phone { get; set; }

    [Display(
        Name = "Image",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }
}
