using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "7F3A2B8E-4D19-4C7B-9E5A-2F8D3C1B6A4E",
    DisplayName = "Hero Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class HeroSection : SectionData
{
    [Display(
        Name = "Background Media",
        Description = "Background image or video for the hero",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [UIHint(UIHint.MediaFile)]
    public virtual ContentReference HeroImage { get; set; }
}
