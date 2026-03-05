using Stride.Mvc._1.Models.Pages;
using EPiServer.VisualBuilder;

namespace Stride.Mvc._1.Models.Experiences;

[ContentType(
    GUID = "2D8F9A4E-7C3B-4A6D-9E5C-8B2F7A3D6C4E",
    DisplayName = "Home Experience",
    Description = "Homepage experience",
    GroupName = Globals.GroupNames.Specialized)]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "page-type-thumbnail-home.png")]
[AvailableContentTypes(
    Availability.Specific,
    Include =
    [
        typeof(ProductExperience),
        typeof(StandardPage),
        typeof(AboutPage),
        typeof(ContactPage),
        typeof(ManagementPage),
        typeof(NewsListingPage),
        typeof(EventListingPage),
        typeof(ContentFolder)
    ])]
public class HomeExperience : ExperienceData
{
}
