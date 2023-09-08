using Alloy.Mvc._1.Models.Blocks;
using Alloy.Mvc._1.Models.ViewModels;
using AlloyMvc1;
using EPiServer.Filters;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc._1.Components;

public class PageListBlockViewComponent : BlockComponent<PageListBlock>
{
    private readonly IContentLoader _contentLoader;

    private readonly IContentGraphClient _contentGraphClient;
    private readonly Lazy<LocalesSerializer> _lazyLocaleSerializer = new(() => new LocalesSerializer());

    public PageListBlockViewComponent(IContentGraphClient contentGraphClient, IContentLoader contentLoader)
    {
        _contentGraphClient = contentGraphClient;
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(PageListBlock currentContent)
    {
        var locale = currentContent is not ILocalizable localizableContent ? Locales.All : _lazyLocaleSerializer.Value.Parse(localizableContent.Language.TwoLetterISOLanguageName.Replace("-", "_"));

        var rootGuid = _contentLoader.Get<IContent>(currentContent.Root).ContentGuid.ToString();
        var pageTypes = new string[] { currentContent.PageTypeFilter.Name };
        var categories = currentContent?.CategoryFilter?.Select(x => (int?)x).ToArray();
        var sortOrder = GetSortOrder(currentContent.SortOrder);

        var listResult = FilterPages(currentContent, locale, pageTypes, categories, sortOrder);

        var model = new PageListModel(currentContent) { ListResult = listResult };

        ViewData.GetEditHints<PageListModel, PageListBlock>()
            .AddConnection(x => x.Heading, x => x.Heading);

        return View(model);
    }

    private IPageListBlockQuery_SitePageData FilterPages(PageListBlock currentContent, Locales locale, string[] pageTypes, int?[] categories, SitePageDataOrderByInput sortOrder)
    {
        var andFilter = new List<SitePageDataWhereInput>
        {
            new SitePageDataWhereInput { ContentType = new StringFilterInput { In = pageTypes } },
            new SitePageDataWhereInput { Category = new CategoryModelWhereInput { Id = new IntFilterInput { In = categories } } }
        };

        if (currentContent.Recursive)
        {
            var rootGuid = GetContentGuid(currentContent);
            andFilter.Add(new SitePageDataWhereInput { Ancestors = new StringFilterInput { Eq = rootGuid } });
        }
        else
        {
            andFilter.Add(new SitePageDataWhereInput { ParentLink = new ContentModelReferenceWhereInput { Id = new IntFilterInput { Eq = currentContent.Root.ID } } });
        }

        var listResult = _contentGraphClient.PageListBlockQuery.ExecuteAsync(
            locale,
            where: new SitePageDataWhereInput { _and = andFilter },
            currentContent.Count,
            currentContent.IncludePublishDate,
            currentContent.IncludeIntroduction,
            sortOrder)
            .GetAwaiter().GetResult();

        return listResult.Data.SitePageData;
    }

    private string GetContentGuid(PageListBlock currentContent)
    {
        return _contentLoader.Get<IContent>(currentContent.Root).ContentGuid.ToString();
    }

    private static SitePageDataOrderByInput GetSortOrder(FilterSortOrder sortOrder)
    {
        switch (sortOrder)
        {
            case FilterSortOrder.ChangedDescending:
            {
                return new SitePageDataOrderByInput { Changed = OrderBy.Desc };
            }
            case FilterSortOrder.CreatedAscending:
            {
                return new SitePageDataOrderByInput { Created = OrderBy.Asc };
            }
            case FilterSortOrder.CreatedDescending:
            {
                return new SitePageDataOrderByInput { Created = OrderBy.Desc };
            }
            case FilterSortOrder.Index:
            {
                return new SitePageDataOrderByInput { _ranking = Ranking.Relevance };
            }
            case FilterSortOrder.PublishedAscending:
            {
                return new SitePageDataOrderByInput { StartPublish = OrderBy.Asc };
            }
            case FilterSortOrder.PublishedDescending:
            {
                return new SitePageDataOrderByInput { StartPublish = OrderBy.Desc };
            }
            default:
            {
                return new SitePageDataOrderByInput { Name = OrderBy.Asc };
            }
        }
    }
}
