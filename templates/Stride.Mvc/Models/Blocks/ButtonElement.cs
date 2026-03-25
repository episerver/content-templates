using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc._1.Models.Blocks;

[SiteContentType(
    GUID = "47028CD8-9651-4272-BA8D-49D3A4F24F91",
    DisplayName = "Button",
    Description = "CTA button element",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ButtonElement : SiteBlockData
{
    [Display(
        Name = "Text",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Text { get; set; }

    [Display(
        Name = "Link",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(PageData))]
    public virtual ContentReference Link { get; set; }
}
