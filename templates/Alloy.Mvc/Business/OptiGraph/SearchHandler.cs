using System;
using System.Globalization;

namespace AlloyMvc1.Business.OptiGraph
{
    public class SearchHandler
    {
        private readonly IContentGraphClient _contentGraphClient;
        private readonly LocalesSerializer _localeSerializer;

        public SearchHandler(IContentGraphClient contentGraphClient, LocalesSerializer localeSerializer)
        {
            _contentGraphClient = contentGraphClient;
            _localeSerializer = localeSerializer;
        }

        public async Task<ISearchContentByPhrase_SitePageData> SearchSitePageData(string query, CultureInfo language)
        {
            // GraphQL don't support - in enums. All languages in the Locale enum has will have - replaced with _ for example en_Gb.
            var locale = _localeSerializer.Parse(language.TwoLetterISOLanguageName.Replace("-", "_"));
            var result = await _contentGraphClient.SearchContentByPhrase.ExecuteAsync(locale, query);

            return result.Data.SitePageData;
        }
    }
}