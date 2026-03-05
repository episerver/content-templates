using EPiServer.Shell.Search;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc.Controllers;

[ApiController]
[Route("api/search")]
public class SearchApiController : ControllerBase
{
    private readonly ISearchProviderManager _searchProviderManager;
    private readonly UrlResolver _urlResolver;

    public SearchApiController(ISearchProviderManager searchProviderManager, UrlResolver urlResolver)
    {
        _searchProviderManager = searchProviderManager;
        _urlResolver = urlResolver;
    }

    [HttpGet]
    public IActionResult Search([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return Ok(Array.Empty<object>());
        }

        var query = new Query(q.Trim(), 20);
        var providers = _searchProviderManager.ListProviders(SearchAreaNames.PagesSearchAreaName, true);

        var results = new List<object>();

        foreach (var provider in providers)
        {
            foreach (var hit in provider.Search(query))
            {
                var url = ResolveUrl(hit);
                if (string.IsNullOrEmpty(url))
                {
                    continue;
                }

                results.Add(new
                {
                    title = hit.Title,
                    description = hit.PreviewText ?? "",
                    url,
                    type = provider.Area == SearchAreaNames.PagesSearchAreaName ? "page" : "content"
                });

                if (results.Count >= 20)
                {
                    return Ok(results);
                }
            }
        }

        return Ok(results);
    }

    private string ResolveUrl(SearchResult hit)
    {
        if (hit.Metadata.TryGetValue("Id", out var idStr) && ContentReference.TryParse(idStr, out var contentLink))
        {
            return _urlResolver.GetUrl(contentLink) ?? "";
        }

        return "";
    }
}
