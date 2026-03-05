using System.ComponentModel.DataAnnotations;
using EPiServer.Web;

namespace Stride.Mvc.Models.Elements;

[ContentType(
    GUID = "2F7A9C4E-8B1D-4E3A-9C6F-7D5B2A8E4C3F",
    DisplayName = "Image Element",
    GroupName = "Visual Builder Elements",
    CompositionBehaviors = [CompositionBehavior.ElementEnabledKey])]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ImageElement : BlockData
{
    [Display(
        Name = "Image",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Image { get; set; }

    [Display(
        Name = "Alt Text",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    public virtual string AltText { get; set; }
}
