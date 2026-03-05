using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc._1.Models.Blocks;

[SiteContentType(
    GUID = "5A8C2F7E-9D4B-4A6E-8C3F-2D7B5A9E4C6F",
    DisplayName = "Stat",
    Description = "Statistic item",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class StatElement : SiteBlockData
{
    [Display(
        Name = "Label",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Label { get; set; }

    [Display(
        Name = "Value",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual string Value { get; set; }

    [Display(
        Name = "Detail",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual string Detail { get; set; }
}
