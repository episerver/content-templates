using Microsoft.AspNetCore.Mvc.Razor;

namespace Stride.Mvc.Business.Rendering;

/// <summary>
/// View location expander for Experience types (following OptiAlloy pattern)
/// Adds ~/Views/Shared/Experiences/ as a search location for experience templates
/// </summary>
public class ExperienceViewEngineLocationExpander : IViewLocationExpander
{
    // Path arguments:
    // 0 - expanderContext.ViewName,
    // 1 - expanderContext.ControllerName,
    // 2 - expanderContext.AreaName
    private const string ExperienceViewFormat = TemplateCoordinator.ExperienceFolder + "{0}.cshtml";

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        var isExperienceController = string.Equals(context.ControllerName, "Experience", StringComparison.Ordinal);

        if (isExperienceController)
        {
            // Prioritize views from the experiences folder when request comes from the ExperienceController
            yield return ExperienceViewFormat;
        }

        foreach (var location in viewLocations)
        {
            yield return location;
        }

        if (!isExperienceController)
        {
            yield return ExperienceViewFormat;
        }
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
        // No additional values needed
    }
}
