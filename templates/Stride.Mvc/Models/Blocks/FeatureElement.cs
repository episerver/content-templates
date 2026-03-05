using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Blocks;

[SiteContentType(
    GUID = "4F8A2C7E-9D3B-4E6A-8C5F-7B2D9A4E3C6F",
    DisplayName = "Feature element",
    Description = "Icon-based feature item",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class FeatureElement : SiteBlockData
{
    [Display(
        Name = "Icon",
        Description = "Lucide icon name (e.g., 'check', 'globe', 'zap')",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Icon { get; set; }

    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual string Heading { get; set; }

    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Description { get; set; }
}
