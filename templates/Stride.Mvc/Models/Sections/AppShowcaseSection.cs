using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;
using EPiServer.Web;

namespace Stride.Mvc.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "A7D3E5B1-4C2F-4A8E-9D6B-3F1C8E5A2D7B",
    DisplayName = "App Showcase Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class AppShowcaseSection : SectionData
{
    [Display(
        Name = "Section Number",
        Description = "Section number label",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    public virtual string SectionNumber { get; set; }

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

    [Display(
        Name = "Screenshot 1",
        Description = "Left app screenshot image",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Screenshot1 { get; set; }

    [Display(
        Name = "Screenshot 2",
        Description = "Center app screenshot image",
        GroupName = SystemTabNames.Content,
        Order = 60)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Screenshot2 { get; set; }

    [Display(
        Name = "Screenshot 3",
        Description = "Right app screenshot image",
        GroupName = SystemTabNames.Content,
        Order = 70)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Screenshot3 { get; set; }
}
