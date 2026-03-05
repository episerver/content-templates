using System.ComponentModel.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.VisualBuilder;
using Stride.Mvc._1.Models.Pages;

namespace Stride.Mvc._1.Models.Experiences;

[ContentType(
    GUID = "A1B2C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C5D",
    DisplayName = "Product Experience",
    Description = "Product showcase experience",
    GroupName = Globals.GroupNames.Specialized)]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-product.png")]
[AvailableContentTypes(
    Availability.Specific,
    Include =
    [
        typeof(StandardPage),
        typeof(AboutPage),
        typeof(ContactPage),
        typeof(ManagementPage),
        typeof(NewsListingPage),
        typeof(EventListingPage),
        typeof(ContentFolder)
    ])]
public class ProductExperience : ExperienceData
{
    [Display(
        Name = "Product Type",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [SelectOne(SelectionFactoryType = typeof(Business.ProductTypeSelectionFactory))]
    [Required]
    public virtual string ProductType { get; set; }

    [Display(
        Name = "Badge Label",
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
