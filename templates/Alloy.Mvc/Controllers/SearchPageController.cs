using Alloy.Mvc._1.Models.Pages;
using Alloy.Mvc._1.Models.ViewModels;
using AlloyMvc1;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc._1.Controllers;

public class SearchPageController : PageControllerBase<SearchPage>
{
    private readonly IContentGraphClient _contentGraphClient;
    private readonly Lazy<LocalesSerializer> _lazyLocaleSerializer = new(() => new LocalesSerializer());

    public SearchPageController(IContentGraphClient contentGraphClient)
    {
        _contentGraphClient = contentGraphClient;
    }

    public ViewResult Index(SearchPage currentPage, string q)
    {
        var searchHits = new List<SearchContentModel.SearchHit>();
        var total = 0;

        if (q != null)
        {
            // GraphQL don't support - in enums. All languages in the Locale enum has will have - replaced with _ for example en_Gb.
            var locale = _lazyLocaleSerializer.Value.Parse(currentPage.Language.TwoLetterISOLanguageName.Replace("-", "_"));
            var result = _contentGraphClient.SearchContentByPhrase.ExecuteAsync(locale, q).GetAwaiter().GetResult();

            foreach (var item in result.Data.SitePageData.Items)
            {

                searchHits.Add(new SearchContentModel.SearchHit()
                {
                    Title = item.Name,
                    Url = item.RelativePath,
                    Excerpt = item.TeaserText
                });
            }

            total = result.Data.SitePageData.Total.GetValueOrDefault();
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
