using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc._1.Models.Blocks;

[SiteContentType(
    GUID = "7E9B3C2A-8D6F-4A5E-9C7F-2D8A3B6E5C4F",
    DisplayName = "List Item",
    Description = "List item with label and metadata",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ListItemElement : SiteBlockData
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
        Description = "Metadata",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string Meta { get; set; }
}
