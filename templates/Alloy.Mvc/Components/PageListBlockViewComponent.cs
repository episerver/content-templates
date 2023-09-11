using Alloy.Mvc._1.Models.Blocks;
using Alloy.Mvc._1.Models.ViewModels;
using AlloyMvc1.Business.OptiGraph;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Alloy.Mvc._1.Components;

public class PageListBlockViewComponent : BlockComponent<PageListBlock>
{
    private readonly PageListBlockHandler _pageListBlockHandler;

    public PageListBlockViewComponent(PageListBlockHandler pageListBlockHandler)
    {
        _pageListBlockHandler = pageListBlockHandler;
    }

    protected override IViewComponentResult InvokeComponent(PageListBlock currentContent)
    {
        var listResult = _pageListBlockHandler.FilterSitePageData(currentContent).GetAwaiter().GetResult();

        var model = new PageListModel(currentContent) { ListResult = listResult };

        ViewData.GetEditHints<PageListModel, PageListBlock>()
            .AddConnection(x => x.Heading, x => x.Heading);

        return View(model);
    }
}
