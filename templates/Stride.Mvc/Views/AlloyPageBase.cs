using Stride.Mvc._1.Business.Rendering;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Stride.Mvc._1.Views;

public abstract class AlloyPageBase<TModel> : RazorPage<TModel> where TModel : class
{
    public abstract override Task ExecuteAsync();

    public AlloyPageBase()
    {
    }

    protected void OnItemRendered(ContentAreaItem contentAreaItem, TagHelperOutput output)
    {
        var alloyContentAreaItemRenderer = ViewContext.HttpContext.RequestServices.GetInstance<AlloyContentAreaItemRenderer>();

        alloyContentAreaItemRenderer.RenderContentAreaItemCss(contentAreaItem, output);
    }
}
