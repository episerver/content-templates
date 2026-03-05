using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc._1.Models.Elements;

[ContentType(
    GUID = "8E5A3C7F-2D9B-4E6A-9C3F-5D7B8A2E4C6F",
    DisplayName = "List Item Element",
    GroupName = "Visual Builder Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ListItemElement : BlockData
{
    [Display(
        Name = "Label",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Label { get; set; }

    [Display(
        Name = "Meta",
        Description = "Metadata (e.g., '7 km · Hiking')",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string Meta { get; set; }
}
