using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc.Business;

public class JourneyCardVariantSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return
        [
            new SelectItem { Value = "tile", Text = "Tile (Badge + Title + Description)" },
            new SelectItem { Value = "map", Text = "Map (Badge + Title + Map Visual)" },
            new SelectItem { Value = "metric", Text = "Metric (Badge + Title + Stats Grid)" },
            new SelectItem { Value = "list", Text = "List (Badge + Title + Items)" }
        ];
    }
}
