using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "E8C4A2D7-9F3B-4E6A-8D5C-7B2F9A4E346D",
    DisplayName = "CTA Block",
    Description = "Call-to-action Block")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class CtaBlock : SiteBlockData
{
    [Display(
        Name = "Puff Cards",
        Description = "Two CTA cards",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [AllowedTypes(typeof(PuffElement))]
    public virtual ContentArea PuffCards { get; set; }
}
