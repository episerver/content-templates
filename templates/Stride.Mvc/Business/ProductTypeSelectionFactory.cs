using EPiServer.Shell.ObjectEditing;

namespace Stride.Mvc._1.Business;

public class ProductTypeSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return new[]
        {
            new SelectItem { Value = "features", Text = "Features" },
            new SelectItem { Value = "challenges", Text = "Challenges" },
            new SelectItem { Value = "subscriptions", Text = "Subscriptions" }
        };
    }
}
