using EPiServer.Core;

namespace Alloy.Mvc.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
