using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Applications;
using EPiServer.Data;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Options;

namespace Stride.Mvc._1.Business;

[ServiceConfiguration]
public class PageViewContextFactory(
        IContentLoader contentLoader,
        UrlResolver urlResolver,
        IDatabaseMode databaseMode,
        IOptionsMonitor<CookieAuthenticationOptions> optionMonitor)
{
    public virtual LayoutModel CreateLayoutModel(ContentReference currentContentLink, HttpContext httpContext)
    {
        var startPageContentLink = ContentReference.StartPage;

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
            IsInReadonlyMode = databaseMode.DatabaseMode == DatabaseMode.ReadOnly
        };

        return layoutModel;
    }

    private string GetLoginUrl(ContentReference returnToContentLink)
    {
        return $"{optionMonitor?.CurrentValue.LoginPath ?? Globals.LoginPath}?ReturnUrl={urlResolver.GetUrl(returnToContentLink)}";
    }

    public virtual IContent GetSection(ContentReference contentLink)
    {
        var currentContent = contentLoader.Get<IContent>(contentLink);

        static bool isSectionRoot(ContentReference contentReference) =>
            ContentReference.IsNullOrEmpty(contentReference) ||
            contentReference.Equals(ContentReference.StartPage) ||
            contentReference.Equals(ContentReference.RootPage);

        if (isSectionRoot(currentContent.ParentLink))
        {
            return currentContent;
        }

        return contentLoader.GetAncestors(contentLink)
            .OfType<PageData>()
            .SkipWhile(x => !isSectionRoot(x.ParentLink))
            .FirstOrDefault();
    }
}
