using System.ComponentModel.DataAnnotations;
using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "7A9E3C2F-6B4D-4E8A-9C5F-2D7B3A8E6C4F",
    DisplayName = "Feature Grid",
    Description = "Horizontal feature list block")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class FeatureGridBlock : SiteBlockData
{
    [Display(
        Name = "Style",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [SelectOne(SelectionFactoryType = typeof(Business.FeatureSectionStyleSelectionFactory))]
    public virtual string Style { get; set; } = "default";

    [Display(
        Name = "Features",
        Description = "Icon-based feature items",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(FeatureElement))]
    public virtual ContentArea Features { get; set; }
}
