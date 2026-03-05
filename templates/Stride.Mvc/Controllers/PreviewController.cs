using Stride.Mvc.Business;
using Stride.Mvc.Models.ViewModels;
using EPiServer.Applications;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Framework.Web.Mvc;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Stride.Mvc.Controllers;

[TemplateDescriptor(
    Inherited = true,
    TemplateTypeCategory = TemplateTypeCategories.MvcController,
    Tags = [RenderingTags.Preview, RenderingTags.Edit],
    AvailableWithoutTag = false)]
[VisitorGroupImpersonation]
[RequireClientResources]
public class PreviewController : ActionControllerBase, IRenderTemplate<BlockData>, IModifyLayout
{
    private readonly IContentLoader _contentLoader;
    private readonly TemplateResolver _templateResolver;
    private readonly IApplicationResolver _applicationResolver;
    private readonly DisplayOptions _displayOptions;

    public PreviewController(
        IContentLoader contentLoader,
        TemplateResolver templateResolver,
        IApplicationResolver applicationResolver,
        DisplayOptions displayOptions)
    {
        _contentLoader = contentLoader;
        _templateResolver = templateResolver;
        _applicationResolver = applicationResolver;
        _displayOptions = displayOptions;
    }

    public async Task<IActionResult> Index(IContent currentContent, CancellationToken cancellationToken)
    {
        //As the layout requires a page for title etc we "borrow" the home page
        var application = await _applicationResolver.GetByContextAsync(cancellationToken);
        var routableApplication = application as IRoutableApplication;
        var homePage = _contentLoader.Get<PageData>(routableApplication?.EntryPoint ?? ContentReference.StartPage);

        var model = new PreviewModel(homePage, currentContent);

        var supportedDisplayOptions = _displayOptions
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
        var templateModel = _templateResolver.Resolve(
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
