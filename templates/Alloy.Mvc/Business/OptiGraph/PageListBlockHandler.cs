using Alloy.Mvc._1.Models.Blocks;
using EPiServer.Filters;
using StrawberryShake;

namespace AlloyMvc1.Business.OptiGraph
{
    public class PageListBlockHandler
    {
        private readonly IContentGraphClient _contentGraphClient;
        private readonly LocalesSerializer _localeSerializer;
        private readonly IContentLoader _contentLoader;

        public PageListBlockHandler(IContentGraphClient contentGraphClient, LocalesSerializer localeSerializer, IContentLoader contentLoader)
        {
            _contentGraphClient = contentGraphClient;
            _localeSerializer = localeSerializer;
            _contentLoader = contentLoader;
        }

        public async Task<IPageListBlockQuery_SitePageData> FilterSitePageData(PageListBlock currentContent)
        {
            var locale = currentContent is not ILocalizable localizableContent ? Locales.All : _localeSerializer.Parse(localizableContent.Language.TwoLetterISOLanguageName.Replace("-", "_"));

            var rootGuid = _contentLoader.Get<IContent>(currentContent.Root).ContentGuid.ToString();
            var pageTypes = new string[] { currentContent.PageTypeFilter.Name };
            var categories = currentContent?.CategoryFilter?.Select(x => (int?)x).ToArray();
            var sortOrder = GetSortOrder(currentContent.SortOrder);

            var sitePageDataResult = await FilterSitePageData(currentContent, locale, pageTypes, categories, sortOrder);
            return sitePageDataResult.Data.SitePageData;
        }

        private async Task<IOperationResult<IPageListBlockQueryResult>> FilterSitePageData(PageListBlock currentContent, Locales locale, string[] pageTypes, int?[] categories, SitePageDataOrderByInput sortOrder)
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

            return await _contentGraphClient.PageListBlockQuery.ExecuteAsync(
                locale,
                where: new SitePageDataWhereInput { _and = andFilter },
                currentContent.Count,
                currentContent.IncludePublishDate,
                currentContent.IncludeIntroduction,
                sortOrder);
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
}

