using System.Text.Json;
using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Features;

namespace Tk.GeoJson.Rfc7946.Converters;

/// <summary>
/// A <see cref="JsonConverter"/> that is responsible for converting a <see cref="FeatureIdentifier"/> to a string or number JSON value and vice versa.
/// </summary>
/// <remarks>
/// This converter will be removed in the future when <see cref="FeatureIdentifier"/> is converted to a union type in C# 15, and the <see cref="JsonConverter"/> will no longer be needed.
/// </remarks>
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