using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeoJsonFeaturesConverter<TProperties> : JsonConverter<GeoJsonObject>
    where TProperties : class
{
    public override GeoJsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        JsonElement root = document.RootElement;

        JsonElement typeProperty = root.GetProperty("type");
        string? unparsedType = typeProperty.GetString();

        if (!Enum.TryParse<GeoJsonObjectTypes>(unparsedType, out var geoJsonType))
        {
            throw new JsonException($"Invalid GeoJSON type: {unparsedType}");
        }

        return geoJsonType switch
        {
            GeoJsonObjectTypes.Feature => root.Deserialize<Feature<TProperties>>(options),
            GeoJsonObjectTypes.FeatureCollection => root.Deserialize<FeatureCollection<TProperties>>(options),
            _ => null
        };
    }
    public override void Write(Utf8JsonWriter writer, GeoJsonObject value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case Feature<TProperties> feature:
                JsonSerializer.Serialize(writer, feature, options);
                break;
            case FeatureCollection<TProperties> featureCollection:
                JsonSerializer.Serialize(writer, featureCollection, options);
                break;
            default:
                throw new NotSupportedException($"The GeoJSON type '{value.GetType().Name}' is not supported for serialization.");
        }
    }
}
