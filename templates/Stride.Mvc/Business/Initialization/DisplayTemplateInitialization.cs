using System.Text.Json;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace Stride.Mvc.Business.Initialization;

[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class DisplayTemplateInitialization : IInitializableModule
{
    private static readonly Dictionary<string, string> ContentTypeResolutions = new(StringComparer.OrdinalIgnoreCase)
    {
        { "defaultButton", "ButtonElement" }
    };

    public void Initialize(InitializationEngine context)
    {
        var displayTemplateRepository = context.Services.GetRequiredService<IDisplayTemplateRepository>();
        var contentTypeRepository = context.Services.GetRequiredService<IContentTypeRepository>();

        var options = new JsonSerializerOptions();
        options.Converters.Add(new ContentTypeBaseConverter());

        var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "displaytemplates.json");

        if (!File.Exists(jsonPath))
        {
            return;
        }

        using var file = File.OpenRead(jsonPath);
        using var stream = new StreamReader(file);

        var json = stream.ReadToEnd();
        var templates = JsonSerializer.Deserialize<List<DisplayTemplate>>(json, options);

        if (templates == null)
        {
            return;
        }

        ResolveContentTypeIds(templates, contentTypeRepository);

        var existingTemplates = displayTemplateRepository.List()
            .ToDictionary(t => t.Key, StringComparer.OrdinalIgnoreCase);

        foreach (var template in templates)
        {
            if (existingTemplates.TryGetValue(template.Key, out var existing))
            {
                if (!string.Equals(existing.NodeType, template.NodeType, StringComparison.OrdinalIgnoreCase) ||
                    existing.BaseType != template.BaseType ||
                    existing.ContentTypeID != template.ContentTypeID ||
                    existing.IsDefault != template.IsDefault)
                {
                    var writable = existing.CreateWritableClone();
                    writable.NodeType = template.NodeType;
                    writable.BaseType = template.BaseType;
                    writable.ContentTypeID = template.ContentTypeID;
                    writable.IsDefault = template.IsDefault;
                    displayTemplateRepository.Save(writable);
                }
            }
            else
            {
                displayTemplateRepository.Save(template);
            }
        }
    }

    public void Uninitialize(InitializationEngine context)
    {
    }

    private static void ResolveContentTypeIds(List<DisplayTemplate> templates, IContentTypeRepository contentTypeRepository)
    {
        foreach (var template in templates)
        {
            if (ContentTypeResolutions.TryGetValue(template.Key, out var contentTypeName))
            {
                var contentType = contentTypeRepository.Load(contentTypeName);
                if (contentType != null)
                {
                    template.ContentTypeID = contentType.ID;
                    template.NodeType = null;
                }
            }
        }
    }
}
