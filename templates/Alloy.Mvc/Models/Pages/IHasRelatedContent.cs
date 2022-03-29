using EPiServer.Core;

namespace Alloy.Mvc._1.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
