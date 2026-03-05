using Stride.Mvc.Models.ViewModels;
using EPiServer.Applications;
using EPiServer.Data;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Stride.Mvc.Business;

[ServiceConfiguration]
public class PageViewContextFactory
{
    private readonly IContentLoader _contentLoader;
    private readonly UrlResolver _urlResolver;
    private readonly IDatabaseMode _databaseMode;
    private readonly IApplicationResolver _applicationResolver;
    private readonly SystemDefinition _systemDefinition;
    private readonly CookieAuthenticationOptions _cookieAuthenticationOptions;

    public PageViewContextFactory(
        IContentLoader contentLoader,
        UrlResolver urlResolver,
        IDatabaseMode databaseMode,
        IApplicationResolver applicationResolver,
        SystemDefinition systemDefinition,
        IOptionsMonitor<CookieAuthenticationOptions> optionMonitor)
    {
        _contentLoader = contentLoader;
        _urlResolver = urlResolver;
        _databaseMode = databaseMode;
        _applicationResolver = applicationResolver;
        _systemDefinition = systemDefinition;
        _cookieAuthenticationOptions = optionMonitor.Get(IdentityConstants.ApplicationScheme);
    }

    public virtual LayoutModel CreateLayoutModel(ContentReference currentContentLink, HttpContext httpContext)
    {
        var routableApplication = _applicationResolver.GetByContext() as IRoutableApplication;
        var startPageContentLink = routableApplication?.EntryPoint;

        // Use the content link with version information when editing the startpage,
        // otherwise the published version will be used when rendering the props below.
        if (currentContentLink.CompareToIgnoreWorkID(startPageContentLink))
        {
            startPageContentLink = currentContentLink;
        }

        var layoutModel = new LayoutModel
        {
            LoggedIn = httpContext.User.Identity.IsAuthenticated,
            LoginUrl = new HtmlString(GetLoginUrl(currentContentLink)),
            IsInReadonlyMode = _databaseMode.DatabaseMode == DatabaseMode.ReadOnly
        };

        return layoutModel;
    }

    private string GetLoginUrl(ContentReference returnToContentLink)
    {
        return $"{_cookieAuthenticationOptions?.LoginPath.Value ?? Globals.LoginPath}?ReturnUrl={_urlResolver.GetUrl(returnToContentLink)}";
    }

    public virtual IContent GetSection(ContentReference contentLink)
    {
        var currentContent = _contentLoader.Get<IContent>(contentLink);
        var routableApplication = _applicationResolver.GetByContext() as IRoutableApplication;
        var currentStartPage = routableApplication?.EntryPoint;

        bool isSectionRoot(ContentReference contentReference) =>
            ContentReference.IsNullOrEmpty(contentReference) ||
            contentReference.Equals(currentStartPage) ||
            contentReference.Equals(_systemDefinition.RootPage);

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
