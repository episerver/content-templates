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
        Name = "Layout Format",
        Description = "Header layout: two-column or stacked",
        GroupName = SystemTabNames.Content,
        Order = 5)]
    [EPiServer.Shell.ObjectEditing.SelectOne(SelectionFactoryType = typeof(Business.LayoutFormatSelectionFactory))]
    public virtual string LayoutFormat { get; set; } = "twoCols";

    [Display(
        Name = "Side Image",
        Description = "Optional image displayed alongside the header (stacked layout only)",
        GroupName = SystemTabNames.Content,
        Order = 6)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference SideImage { get; set; }

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
}
