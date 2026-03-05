using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc.Components;

public class SearchViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
