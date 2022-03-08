using System;
using EPiServer.Core;
using EPiServer.Web.Mvc;

namespace MyAppNamespace
{
    public class MyRazorPageModel : RazorPageModel<MyPageData>
    {
        public void OnGet()
        {
        }
    }
}
