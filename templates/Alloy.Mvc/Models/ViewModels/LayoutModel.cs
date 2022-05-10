using Alloy.Mvc._1.Models.Blocks;
using EPiServer.SpecializedProperties;
using Microsoft.AspNetCore.Html;

namespace Alloy.Mvc._1.Models.ViewModels
{
    public class LayoutModel
    {
        public SiteLogotypeBlock Logotype { get; set; }

        public IHtmlContent LogotypeLinkUrl { get; set; }

        public bool HideHeader { get; set; }

        public bool HideFooter { get; set; }

        public LinkItemCollection ProductPages { get; set; }

        public LinkItemCollection CompanyInformationPages { get; set; }

        public LinkItemCollection NewsPages { get; set; }

        public LinkItemCollection CustomerZonePages { get; set; }

        public bool LoggedIn { get; set; }

        public HtmlString LoginUrl { get; set; }

        public HtmlString LogOutUrl { get; set; }

        public HtmlString SearchActionUrl { get; set; }

        public bool IsInReadonlyMode { get; set; }
    }
}
