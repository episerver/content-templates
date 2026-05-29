using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "B2C4D6E8-1A3F-4B5D-9C7E-3F5A7B9D1E2C",
    DisplayName = "Product Hero Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ProductHeroSection : SectionData
{
    [Display(
        Name = "Background Image",
        Description = "Background image for the product hero",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference BackgroundImage { get; set; }

    [Display(
        Name = "Badge Label",
        Description = "Product badge text (e.g. Platform, Challenges, Business)",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string BadgeLabel { get; set; }

    [Display(
        Name = "Badge Icon",
        Description = "Lucide icon name for the badge (e.g. sparkles, flag, wallet)",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual string BadgeIcon { get; set; }
}
