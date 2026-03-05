using Stride.Mvc.Business.Rendering;
using Stride.Mvc.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc.Controllers;

public class EventListingPageController : ListingPageController<EventListingPage, EventPage>
{
    public EventListingPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader)
        : base(uiSignInManager, themeService, contentLoader) { }

    protected override IEnumerable<EventPage> OrderItems(IEnumerable<EventPage> items)
        => items.OrderBy(x => x.EventDate);
}
