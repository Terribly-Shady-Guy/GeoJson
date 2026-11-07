using GeoJson.Features;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson.Converters;

public sealed class FeatureIdentifierJsonConverter : JsonConverter<FeatureIdentifier>
{
    public override FeatureIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out double doubleValue))
        {
            return doubleValue;
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            string stringValue = reader.GetString() ?? throw new JsonException("String cannot be null.");
            return stringValue;
        }

        throw new JsonException($"Unexpected token parsing FeatureIdentifier. Expected Number or String, got {reader.TokenType}.");
    }

    public override void Write(Utf8JsonWriter writer, FeatureIdentifier value, JsonSerializerOptions options)
    {
        if (value.TryGetValue(out double doubleValue))
        {
            writer.WriteNumberValue(doubleValue);
        }
        else if (value.TryGetValue(out string? stringValue))
        {
            writer.WriteStringValue(stringValue);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}