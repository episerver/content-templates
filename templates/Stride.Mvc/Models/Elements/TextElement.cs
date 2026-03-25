using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc._1.Models.Elements;

[ContentType(
    GUID = "4A9C7E2F-1D8B-4F3A-B5C6-8E2D9A4F7C1B",
    DisplayName = "Text Element",
    GroupName = "Visual Builder Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class TextElement : BlockData
{
    [Display(
        Name = "Text",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [UIHint(UIHint.Textarea)]
    public virtual string Text { get; set; }
}
