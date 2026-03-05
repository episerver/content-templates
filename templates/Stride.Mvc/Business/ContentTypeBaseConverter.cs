using System.Text.Json;
using System.Text.Json.Serialization;

namespace Stride.Mvc.Business;

public class ContentTypeBaseConverter : JsonConverter<ContentTypeBase?>
{
    public override ContentTypeBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringValue = reader.GetString();
        if (!string.IsNullOrEmpty(stringValue))
        {
            return new ContentTypeBase(stringValue);
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, ContentTypeBase? value, JsonSerializerOptions options)
        => writer.WriteStringValue(value?.ToString());
}
