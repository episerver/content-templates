using EPiServer.ServiceLocation;

namespace Stride.Mvc._1.Business.Rendering;

/// <summary>
/// Service for managing theme-related CSS classes based on content categories
/// </summary>
[ServiceConfiguration(typeof(ThemeService), Lifecycle = ServiceInstanceScope.Scoped)]
public class ThemeService
{
    private readonly CategoryRepository _categoryRepository;

    public ThemeService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Returns the CSS classes associated with the themes of the content, as decided by its categories
    /// </summary>
    /// <param name="content">The categorizable content</param>
    public string[] GetThemeCssClassNames(ICategorizable content)
    {
        if (content?.Category == null)
        {
            return [];
        }

        var cssClasses = new HashSet<string>();

        foreach (var categoryName in content.Category.Select(category => _categoryRepository.Get(category).Name.ToLowerInvariant()))
        {
            switch (categoryName)
            {
                case "meet":
                    cssClasses.Add("theme1");
                    break;
                case "track":
                    cssClasses.Add("theme2");
                    break;
                case "plan":
                    cssClasses.Add("theme3");
                    break;
                default:
                    break;
            }
        }

        return [.. cssClasses];
    }
}
