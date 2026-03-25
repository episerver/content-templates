using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "A7D3E5B1-4C2F-4A8E-9D6B-3F1C8E5A2D7B",
    DisplayName = "App Showcase Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class AppShowcaseSection : SectionData
{
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
