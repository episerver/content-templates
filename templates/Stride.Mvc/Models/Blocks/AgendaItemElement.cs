using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc.Models.Blocks;

[SiteContentType(
    GUID = "47028CD8-9221-4272-BA8D-49D3A4F24A34",
    DisplayName = "Agenda Item",
    Description = "Event agenda item",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class AgendaItemElement : SiteBlockData
{
    [Display(
        Name = "Time",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Time { get; set; }

    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual string Description { get; set; }
}
