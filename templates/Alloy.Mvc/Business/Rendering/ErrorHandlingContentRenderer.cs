using System.Diagnostics;
using Alloy.Mvc._1.Helpers;
using Alloy.Mvc._1.Models.ViewModels;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Alloy.Mvc._1.Business.Rendering;

/// <summary>
/// Wraps an MvcContentRenderer and adds error handling to ensure that blocks and other content
/// rendered as parts of pages won't crash the entire page if a non-critical exception occurs while rendering it.
/// </summary>
/// <remarks>
/// Prints an error message for editors so that they can easily report errors to developers.
/// </remarks>
public class ErrorHandlingContentRenderer : IContentRenderer
{
    private readonly MvcContentRenderer _mvcRenderer;

    public ErrorHandlingContentRenderer(MvcContentRenderer mvcRenderer)
    {
        _mvcRenderer = mvcRenderer;
    }

    /// <summary>
    /// Renders the contentData using the wrapped renderer and catches common, non-critical exceptions.
    /// </summary>
    public async Task RenderAsync(IHtmlHelper helper, IContentData contentData, TemplateModel templateModel)
    {
        try
        {
            await _mvcRenderer.RenderAsync(helper, contentData, templateModel);
        }
        catch (Exception ex) when (!Debugger.IsAttached)
        {
            switch (ex)
            {
                case NullReferenceException:
                case ArgumentException:
                case ApplicationException:
                case InvalidOperationException:
                case NotImplementedException:
                case IOException:
                case EPiServerException:
                    HandlerError(helper, contentData, ex);
                    break;
                default:
                    throw;
            }
        }
    }

    private static void HandlerError(IHtmlHelper helper, IContentData contentData, Exception renderingException)
    {
        if (helper.ViewContext.IsInEditMode())
        {
            var errorModel = new ContentRenderingErrorModel(contentData, renderingException);
            helper.RenderPartialAsync("TemplateError", errorModel).GetAwaiter().GetResult();
        }
    }
}
