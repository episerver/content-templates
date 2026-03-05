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
        Name = "Hero Image",
        Description = "Background hero",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [UIHint(UIHint.MediaFile)]
    public virtual ContentReference HeroImage { get; set; }

    [Display(
        Name = "Side Image",
        Description = "Side image (two-column layout only)",
        GroupName = SystemTabNames.Content,
        Order = 15)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference SideImage { get; set; }

    [Display(
        Name = "Layout Style",
        Description = "Choose hero layout style",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [EPiServer.Shell.ObjectEditing.SelectOne(SelectionFactoryType = typeof(Business.HeroLayoutSelectionFactory))]
    public virtual string LayoutStyle { get; set; } = "single-column";

    [Display(
        Name = "Heading",
        Description = "Main hero heading",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual string Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "Supporting text below the heading",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [UIHint(UIHint.Textarea)]
    public virtual string Description { get; set; }

    [Display(
        Name = "Primary Button Text",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    public virtual string PrimaryButtonText { get; set; }

    [Display(
        Name = "Primary Button Link",
        GroupName = SystemTabNames.Content,
        Order = 55)]
    public virtual Url PrimaryButtonLink { get; set; }

    [Display(
        Name = "Secondary Button Text",
        GroupName = SystemTabNames.Content,
        Order = 60)]
    public virtual string SecondaryButtonText { get; set; }

    [Display(
        Name = "Secondary Button Link",
        GroupName = SystemTabNames.Content,
        Order = 65)]
    public virtual Url SecondaryButtonLink { get; set; }
}
