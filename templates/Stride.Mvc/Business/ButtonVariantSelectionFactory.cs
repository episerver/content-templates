using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc._1.Business;

public class ButtonVariantSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return [
            new SelectItem { Value = "primary", Text = "Primary (Filled)" },
            new SelectItem { Value = "outline", Text = "Outline (Border Only)" }
        ];
    }
}
