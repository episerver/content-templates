using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc.Business;

public class FeatureSectionStyleSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return
        [
            new SelectItem { Value = "default", Text = "Default (Large Cards)" },
            new SelectItem { Value = "compact", Text = "Compact (Small Cards)" }
        ];
    }
}
