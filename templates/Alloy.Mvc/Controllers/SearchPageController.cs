using Alloy.Mvc._1.Models.Pages;
using Alloy.Mvc._1.Models.ViewModels;
using AlloyMvc1.Business.OptiGraph;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc._1.Controllers;

public class SearchPageController : PageControllerBase<SearchPage>
{
    private readonly SearchHandler _searchHandler;

    public SearchPageController(SearchHandler searchHandler)
    {
        _searchHandler = searchHandler;
    }

    public ViewResult Index(SearchPage currentPage, string q)
    {
        var searchHits = new List<SearchContentModel.SearchHit>();
        var total = 0;

        if (q != null)
        {
            var result = _searchHandler.SearchSitePageData(q, currentPage.Language).GetAwaiter().GetResult();
            foreach (var item in result.Items)
            {

                searchHits.Add(new SearchContentModel.SearchHit()
                {
                    Title = item.Name,
                    Url = item.RelativePath,
                    Excerpt = item.TeaserText
                });
            }

            total = result.Total.GetValueOrDefault();
        }

        var model = new SearchContentModel(currentPage)
        {
            Hits = searchHits,
            NumberOfHits = total,
            SearchServiceDisabled = false,
            SearchedQuery = q
        };

        return View(model);
    }
}
