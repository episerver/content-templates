using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc._1.Business;

public class LayoutFormatSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return [
            new SelectItem { Value = "twoCols", Text = "Two Columns" },
            new SelectItem { Value = "stacked", Text = "Stacked" }
        ];
    }
}
