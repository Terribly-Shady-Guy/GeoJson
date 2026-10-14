using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeoJsonFeaturesConverter<TGeometry, TProperties> : JsonConverter<GeoJsonObject>
    where TProperties : class?
    where TGeometry : GeometryGeoJsonObject?
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
            GeoJsonObjectTypes.Feature => root.Deserialize<Feature<TGeometry, TProperties>>(options),
            GeoJsonObjectTypes.FeatureCollection => root.Deserialize<FeatureCollection<TGeometry, TProperties>>(options),
            _ => null
        };
    }
    public override void Write(Utf8JsonWriter writer, GeoJsonObject value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case Feature<TGeometry, TProperties> feature:
                JsonSerializer.Serialize(writer, feature, options);
                break;
            case FeatureCollection<TGeometry, TProperties> featureCollection:
                JsonSerializer.Serialize(writer, featureCollection, options);
                break;
            default:
                throw new NotSupportedException($"The GeoJSON type '{value.GetType().Name}' is not supported for serialization.");
        }
    }
}
