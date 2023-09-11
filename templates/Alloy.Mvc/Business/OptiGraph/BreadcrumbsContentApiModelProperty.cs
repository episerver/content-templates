using AlloyMvc1.Business.OptiGraph.GraphQL;
using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Optimizely.ContentGraph.Cms.Core.ContentApiModelProperties.Internal;

namespace AlloyMvc1.Business.OptiGraph
{
    [ServiceConfiguration(typeof(IContentApiModelProperty), Lifecycle = ServiceInstanceScope.Singleton)]
    public class BreadcrumbsContentApiModelProperty : IContentApiModelProperty
    {
        private readonly IContentLoader _contentLoader;
        private readonly UrlResolver _urlResolver;

        public BreadcrumbsContentApiModelProperty()
            : this(
                  ServiceLocator.Current.GetInstance<IContentLoader>(),
                  ServiceLocator.Current.GetInstance<UrlResolver>())
        {
        }

        public BreadcrumbsContentApiModelProperty(
            IContentLoader contentLoader,
            UrlResolver urlResolver)
        {
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }

        public string Name => "Breadcrumb";

        public object GetValue(ContentApiModel contentApiModel)
        {
            try
            {
                if (contentApiModel?.ContentLink?.Id == null || contentApiModel.ContentLink.Id == 0)
                {
                    return Enumerable.Empty<string>().ToList();
                }

                var contentReference = new ContentReference(contentApiModel.ContentLink.Id.Value, contentApiModel.ContentLink?.WorkId.GetValueOrDefault() ?? 0, contentApiModel.ContentLink?.ProviderName);
                var anchestors = _contentLoader.GetAncestors(contentReference);

                return anchestors.OfType<PageData>().Where(x => !string.IsNullOrEmpty(x.LinkURL)).Select(x => $"{GetVirtualPath(x)} | {x.Name}").ToList();
            }
            catch { }

            return Enumerable.Empty<string>().ToList();
        }

        public string GetVirtualPath(IContent content)
        {
            return _urlResolver.GetVirtualPath(content)?.VirtualPath;
        }
    }
}

