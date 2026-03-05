using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "6C9F3A2E-8B5D-4A7C-9E3F-2D8B5A7C4E6F",
    DisplayName = "Highlight",
    Description = "Highlight box element",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class HighlightElement : SiteBlockData
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
