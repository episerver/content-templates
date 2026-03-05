using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Elements;

[ContentType(
    GUID = "4D9B6E2A-7C3F-4A8D-B5E9-8A2C6F3D7B4E",
    DisplayName = "Highlight Element",
    GroupName = "Visual Builder Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class HighlightElement : BlockData
{
    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }

    [Display(
        Name = "Body",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Body { get; set; }
}
