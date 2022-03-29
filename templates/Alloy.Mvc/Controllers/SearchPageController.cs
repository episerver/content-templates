using System.Linq;
using Alloy.Mvc._1.Models.Pages;
using Alloy.Mvc._1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc._1.Controllers
{
    public class SearchPageController : PageControllerBase<SearchPage>
    {
        public ViewResult Index(SearchPage currentPage, string q)
        {
            var model = new SearchContentModel(currentPage)
            {
                Hits = Enumerable.Empty<SearchContentModel.SearchHit>(),
                NumberOfHits = 0,
                SearchServiceDisabled = true,
                SearchedQuery = q
            };

            return View(model);
        }
    }
}
