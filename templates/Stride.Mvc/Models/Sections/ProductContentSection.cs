using EPiServer.VisualBuilder;

namespace Stride.Mvc.Models.Sections;

[ContentType(
    GroupName = "Sections",
    GUID = "A4D2E8F1-3B7C-4A5E-9D6F-1C8B5A2E7D3F",
    DisplayName = "Product Content Section")]
[SiteImageUrl(Globals.StaticGraphicsFolderPath + "block-type-thumbnail.png")]
public class ProductContentSection : SectionData
{
}
