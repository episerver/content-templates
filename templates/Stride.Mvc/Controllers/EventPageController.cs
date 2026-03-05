using Stride.Mvc.Business.Rendering;
using Stride.Mvc.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc.Controllers;

public class EventPageController : DetailPageController<EventPage>
{
    protected override int RelatedItemsLimit => 2;

    public EventPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader)
        : base(uiSignInManager, themeService, contentLoader) { }

    protected override IEnumerable<EventPage> OrderRelated(IEnumerable<EventPage> items)
        => items.OrderBy(x => x.EventDate);
}
