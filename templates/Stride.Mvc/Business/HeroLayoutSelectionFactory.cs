using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc._1.Business;

public class HeroLayoutSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return
        [
            new SelectItem { Value = "single-column", Text = "Single Column (Centered)" },
            new SelectItem { Value = "two-column", Text = "Two Column (Split)" }
        ];
    }
}
