using System;
using EPiServer.Core;

namespace AlloyMvc1.Business.OptiGraph.GraphQL
{
    public class BreadcrumbHandler
    {
        private readonly IContentGraphClient _contentGraphClient;
        private readonly LocalesSerializer _localeSerializer;

        public BreadcrumbHandler(IContentGraphClient contentGraphClient, LocalesSerializer localeSerializer)
        {
            _contentGraphClient = contentGraphClient;
            _localeSerializer = localeSerializer;
        }

        public async Task<IEnumerable<BreadcrumbModel>> GetOrderedBreadcrumbs(IContent content)
        {
            var locale = content is not ILocalizable localizableContent ? Locales.All : _localeSerializer.Parse(localizableContent.Language.TwoLetterISOLanguageName.Replace("-", "_"));

            //var queryResult = await _contentGraphClient.BreadcrumbQuery.ExecuteAsync(locale, content.ContentLink.ID);

            var breadcrumbs = new List<BreadcrumbModel>();


            return breadcrumbs;
        }
    }
}

