using System.ComponentModel.DataAnnotations;
using EPiServer.VisualBuilder;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "D5E9A2C7-8B3F-4E1D-A6C9-7B4E8A2D5F1C",
    DisplayName = "Journey Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class JourneySection : SectionData
{
    [Display(
        Name = "Eyebrow",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    public virtual string Eyebrow { get; set; }

    [Display(
        Name = "Title",
        Description = "Main section title",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    public virtual string Title { get; set; }

    [Display(
        Name = "Description",
        Description = "Section description text",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [UIHint(UIHint.Textarea)]
    public virtual string Description { get; set; }
}
