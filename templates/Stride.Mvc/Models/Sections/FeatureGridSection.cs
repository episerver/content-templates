using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "3C8F5D2A-6E1B-4A9D-8C7F-5E3A9B2D4C6F",
    DisplayName = "Feature Grid Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class FeatureGridSection : SectionData
{
    [Display(
        Name = "Image",
        Description = "Optional featured image for the section header",
        GroupName = SystemTabNames.Content,
        Order = 6)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }

    [Display(
        Name = "Section Label",
        Description = "Section label text",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    public virtual string SectionLabel { get; set; }

    [Display(
        Name = "Heading",
        Description = "Main section heading",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual string Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "Section description text",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [UIHint(UIHint.Textarea)]
    public virtual string Description { get; set; }
}
