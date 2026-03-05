using System.ComponentModel.DataAnnotations;

namespace Stride.Mvc.Models.Elements;

[ContentType(
    GUID = "6B8D3F2A-9E5C-4D1B-A7F9-2C6E4B8A3D5F",
    DisplayName = "Heading Element",
    GroupName = "Visual Builder Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class HeadingElement : BlockData
{
    [Display(
        Name = "Text",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    public virtual string Text { get; set; }

    [Display(
        Name = "Level",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    public virtual string Level { get; set; } = "h2";
}
