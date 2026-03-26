using Stride.Mvc._1.Business;
using Stride.Mvc._1.Models.ViewModels;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Framework.Web.Mvc;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc._1.Controllers;

[TemplateDescriptor(
    Inherited = true,
    TemplateTypeCategory = TemplateTypeCategories.MvcController,
    Tags = [RenderingTags.Preview, RenderingTags.Edit],
    AvailableWithoutTag = false)]
[VisitorGroupImpersonation]
[RequireClientResources]
public class PreviewController(
        IContentLoader contentLoader,
        TemplateResolver templateResolver,
        DisplayOptions displayOptions) : ActionControllerBase, IRenderTemplate<BlockData>, IModifyLayout
{
    public async Task<IActionResult> Index(IContent currentContent)
    {
        //As the layout requires a page for title etc we "borrow" the home page
        var homePage = contentLoader.Get<PageData>(ContentReference.StartPage);

        var model = new PreviewModel(homePage, currentContent);

        var supportedDisplayOptions = displayOptions
            .Select(x => new { x.Tag, x.Name, Supported = SupportsTag(currentContent, x.Tag) })
            .ToList();

        if (supportedDisplayOptions.Any(x => x.Supported))
        {
            foreach (var displayOption in supportedDisplayOptions)
            {
                var contentArea = new ContentArea();

                contentArea.Items.Add(new ContentAreaItem
                {
                    ContentLink = currentContent.ContentLink
                });

                var areaModel = new PreviewModel.PreviewArea
                {
                    Supported = displayOption.Supported,
                    AreaTag = displayOption.Tag,
                    AreaName = displayOption.Name,
                    ContentArea = contentArea
                };

                model.Areas.Add(areaModel);
            }
        }

        return View(model);
    }

    private bool SupportsTag(IContent content, string tag)
    {
        var templateModel = templateResolver.Resolve(
            HttpContext,
            content.GetOriginalType(),
            content,
            TemplateTypeCategories.MvcPartial,
            tag);

        return templateModel != null;
    }

    public void ModifyLayout(LayoutModel layoutModel)
    {
        layoutModel.HideHeader = true;
        layoutModel.HideFooter = true;
    }
}
