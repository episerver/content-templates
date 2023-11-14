using System;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace MyAppNamespace;

public class MyContentComponent : PartialContentComponent<MyBlockData>
{
    protected override IViewComponentResult InvokeComponent(MyBlockData currentContent)
    {
        return View(currentContent);
    }
}
