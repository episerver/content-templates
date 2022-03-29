using System.ComponentModel.DataAnnotations;
using Alloy.Mvc._1.Models.Pages;
using EPiServer.Core;
using EPiServer.Web;
using Microsoft.AspNetCore.Html;

namespace Alloy.Mvc._1.Models.ViewModels
{
    public class ContactBlockModel
    {
        [UIHint(UIHint.Image)]
        public ContentReference Image { get; set; }

        public string Heading { get; set; }

        public string LinkText { get; set; }

        public IHtmlContent LinkUrl { get; set; }

        public bool ShowLink { get; set; }

        public ContactPage ContactPage { get; set; }
    }
}
