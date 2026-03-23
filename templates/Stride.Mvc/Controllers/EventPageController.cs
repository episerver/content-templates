using Stride.Mvc._1.Business.Rendering;
using Stride.Mvc._1.Models.Pages;
using EPiServer.Shell.Security;

namespace Stride.Mvc._1.Controllers;

public class EventPageController(
        UISignInManager uiSignInManager,
        ThemeService themeService,
        IContentLoader contentLoader) : DetailPageController<EventPage>(uiSignInManager, themeService, contentLoader)
{
    protected override int RelatedItemsLimit => 2;

    protected override IEnumerable<EventPage> OrderRelated(IEnumerable<EventPage> items)
        => items.OrderBy(x => x.EventDate);
}
