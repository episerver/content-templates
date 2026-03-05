using System.ComponentModel.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "9B6E4C2A-7D8F-4A3E-8C9F-2D5B7A3E6C4F",
    DisplayName = "Journey Card",
    Description = "Multi-variant showcase card")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class JourneyCardBlock : SiteBlockData
{
    [Display(
        Name = "Badge",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Badge { get; set; }

    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }

    [Display(
        Name = "Variant",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(Business.JourneyCardVariantSelectionFactory))]
    [Required]
    public virtual string Variant { get; set; } = "tile";

    [Display(
        Name = "Copy",
        Description = "Description text (for Tile variant)",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Copy { get; set; }

    [Display(
        Name = "Stats",
        Description = "Statistics (for Metric variant)",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [AllowedTypes(typeof(StatElement))]
    public virtual ContentArea Stats { get; set; }

    [Display(
        Name = "Items",
        Description = "List items (for List variant)",
        GroupName = SystemTabNames.Content,
        Order = 60)]
    [AllowedTypes(typeof(ListItemElement))]
    public virtual ContentArea Items { get; set; }
}
