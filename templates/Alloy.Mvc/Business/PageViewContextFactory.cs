using Alloy.Mvc._1.Models.ViewModels;
using AlloyMvc1;
using EPiServer.Data;
using EPiServer.ServiceLocation;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Alloy.Mvc._1.Business;

[ServiceConfiguration]
public class PageViewContextFactory
{
    private readonly IContentLoader _contentLoader;
    private readonly UrlResolver _urlResolver;
    private readonly IDatabaseMode _databaseMode;
    private readonly CookieAuthenticationOptions _cookieAuthenticationOptions;

    private readonly IContentGraphClient _contentGraphClient;
    private readonly Lazy<LocalesSerializer> _lazyLocaleSerializer = new(() => new LocalesSerializer());

    public PageViewContextFactory(
        IContentLoader contentLoader,
        UrlResolver urlResolver,
        IDatabaseMode databaseMode,
        IContentGraphClient contentGraphClient,
        IOptionsMonitor<CookieAuthenticationOptions> optionMonitor)
    {
        _contentLoader = contentLoader;
        _urlResolver = urlResolver;
        _databaseMode = databaseMode;
        _contentGraphClient = contentGraphClient;
        _cookieAuthenticationOptions = optionMonitor.Get(IdentityConstants.ApplicationScheme);
    }

    public virtual LayoutModel CreateLayoutModel(ContentReference currentContentLink, HttpContext httpContext)
    {
        var locale = _lazyLocaleSerializer.Value.Parse(httpContext.GetRequestedLanguage().Replace("-", "_"));
        var startPage = _contentGraphClient.StartPage.ExecuteAsync(locale, SiteDefinition.Current.StartPage.ID).GetAwaiter().GetResult().Data.StartPage.Items.FirstOrDefault();

        return new LayoutModel
        {
            Logotype = new Models.Blocks.SiteLogotypeBlock { Title = startPage.SiteLogotype.Title },
            LogotypeLinkUrl = new HtmlString(startPage.RelativePath),
            ProductPages = new LinkItemCollection(startPage.ProductPageLinks.Select(CreateLinkItem)),
            CompanyInformationPages = new LinkItemCollection(startPage.CompanyInformationPageLinks.Select(CreateLinkItem)),
            NewsPages = new LinkItemCollection(startPage.NewsPageLinks.Select(CreateLinkItem)),
            CustomerZonePages = new LinkItemCollection(startPage.CustomerZonePageLinks.Select(CreateLinkItem)),
            LoggedIn = httpContext.User.Identity.IsAuthenticated,
            LoginUrl = new HtmlString(GetLoginUrl(currentContentLink)),
            SearchActionUrl = new HtmlString(startPage.SearchPageLink.Url),
            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
        };
    }

    private LinkItem CreateLinkItem(ILinkItemNode linkItemNode)
    {
        return new LinkItem { Title = linkItemNode.Title, Href = linkItemNode.Href, Target = linkItemNode.Target, Text = linkItemNode.Text };
    }

    private string GetLoginUrl(ContentReference returnToContentLink)
    {
        return $"{_cookieAuthenticationOptions?.LoginPath.Value ?? Globals.LoginPath}?ReturnUrl={_urlResolver.GetUrl(returnToContentLink)}";
    }

    public virtual IContent GetSection(ContentReference contentLink)
    {
        var currentContent = _contentLoader.Get<IContent>(contentLink);

        static bool isSectionRoot(ContentReference contentReference) =>
           ContentReference.IsNullOrEmpty(contentReference) ||
           contentReference.Equals(SiteDefinition.Current.StartPage) ||
           contentReference.Equals(SiteDefinition.Current.RootPage);

        if (isSectionRoot(currentContent.ParentLink))
        {
            return currentContent;
        }

        return _contentLoader.GetAncestors(contentLink)
            .OfType<PageData>()
            .SkipWhile(x => !isSectionRoot(x.ParentLink))
            .FirstOrDefault();
    }
}