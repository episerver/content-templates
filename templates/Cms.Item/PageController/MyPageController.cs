using System;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace MyAppNamespace
{
    [TemplateDescriptor(Inherited = true)]
    public class MyPageController : PageController<MyPageData>
    {
        public ActionResult Index(MyPageData currentPage)
        {
            // Implementation of action. You can create your own view model class that you pass to the view or
            // you can pass the page type model directly for simpler templates

            return View(currentPage);
        }
    }
}
