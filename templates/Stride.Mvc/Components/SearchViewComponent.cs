using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Components;

public class SearchViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
