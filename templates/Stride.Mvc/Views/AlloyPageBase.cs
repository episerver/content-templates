using Stride.Mvc.Business.Rendering;
using EPiServer.ServiceLocation;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Stride.Mvc.Views;

public abstract class AlloyPageBase<TModel> : RazorPage<TModel> where TModel : class
{
    public abstract override Task ExecuteAsync();

    public AlloyPageBase()
    {
    }

    protected void OnItemRendered(ContentAreaItem contentAreaItem, TagHelperContext context, TagHelperOutput output)
    {
        var alloyContentAreaItemRenderer = ViewContext.HttpContext.RequestServices.GetInstance<AlloyContentAreaItemRenderer>();

        alloyContentAreaItemRenderer.RenderContentAreaItemCss(contentAreaItem, context, output);
    }
}
