using System.ComponentModel.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "3D7F9A2E-8C5B-4D6A-9E3C-5B8A2F7D4C6E",
    DisplayName = "Product Card",
    Description = "Product category card with image and link",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ProductCardElement : SiteBlockData
{
    [Display(
        Name = "Icon",
        Description = "Lucide icon name",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Icon { get; set; }

    [Display(
        Name = "Product Type",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual string ProductType { get; set; }

    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }

    [Display(
        Name = "Image",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }

    [Display(
        Name = "Link",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [Required]
    public virtual Url Link { get; set; }
}
