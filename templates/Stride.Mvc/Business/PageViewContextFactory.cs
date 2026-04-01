using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Data;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

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
        return $"{optionMonitor.Get(IdentityConstants.ApplicationScheme)?.LoginPath ?? Globals.LoginPath}?ReturnUrl={urlResolver.GetUrl(returnToContentLink)}";
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
