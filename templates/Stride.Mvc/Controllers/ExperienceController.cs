using EPiServer.Framework.DataAnnotations;
using EPiServer.Shell.Security;
using EPiServer.VisualBuilder;
using Microsoft.AspNetCore.Mvc;
using Stride.Mvc.Models.ViewModels;
using Stride.Mvc.Business.Rendering;

namespace Stride.Mvc.Controllers;

/// <summary>
/// Controller for rendering ExperienceData pages.
/// </summary>
[TemplateDescriptor(Inherited = true, ModelType = typeof(ExperienceData))]
public sealed class ExperienceController : PageControllerBase<ExperienceData>
{
    public ExperienceController(UISignInManager signInManager, ThemeService themeService)
        : base(signInManager, themeService)
    {
    }

    public IActionResult Index(ExperienceData currentPage)
    {
        var model = CreateModel(currentPage);
        return View(model);
    }

    /// <summary>
    /// Creates a PageViewModel where the type parameter is the type of the experience.
    /// </summary>
    private static object CreateModel(ExperienceData currentPage)
    {
        var type = typeof(PageViewModel<>).MakeGenericType(currentPage.GetOriginalType());
        return Activator.CreateInstance(type, currentPage);
    }
}
