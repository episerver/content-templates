using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Blocks;

[SiteContentType(
    GUID = "5C2F8A4E-9D7B-4A3C-8E6F-3B5D9A7C2E4F",
    DisplayName = "Journey",
    Description = "Major feature showcase section with highlights and cards")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class JourneyBlock : SiteBlockData
{
    [Display(
        Name = "Eyebrow",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Eyebrow { get; set; }

    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }

    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual XhtmlString Description { get; set; }

    [Display(
        Name = "Highlights",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [AllowedTypes(typeof(HighlightElement))]
    public virtual ContentArea Highlights { get; set; }

    [Display(
        Name = "Journey Cards",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [AllowedTypes(typeof(JourneyCardBlock))]
    public virtual ContentArea JourneyCards { get; set; }

    [Display(
        Name = "Reverse Layout",
        GroupName = SystemTabNames.Content,
        Order = 60)]
    public virtual bool ReverseLayout { get; set; }
}
