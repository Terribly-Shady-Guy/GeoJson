using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeoJsonObjectConverter : JsonConverter<GeoJsonObject>
{
    public override GeoJsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        JsonElement root = document.RootElement;

        if (!root.TryGetProperty("type", out JsonElement typeProperty))
        {
            throw new JsonException("Missing 'type' property in GeoJSON object.");
        }

        string? unparsedType = typeProperty.GetString();

        if (!Enum.TryParse(unparsedType, out GeoJsonObjectTypes geoJsonType))
        {
            throw new JsonException($"Invalid GeoJSON type: {unparsedType}");
        }

        switch (geoJsonType)
        {
            case GeoJsonObjectTypes.Feature:
                Type[] genericArgs = typeToConvert.GetGenericArguments();
                Type feature = typeof(Feature<,>).MakeGenericType(genericArgs);
                return (GeoJsonObject?)root.Deserialize(feature, options);
            case GeoJsonObjectTypes.FeatureCollection:
                genericArgs = typeToConvert.GetGenericArguments();
                Type featureCollection = typeof(FeatureCollection<,>).MakeGenericType(genericArgs);
                return (GeoJsonObject?)root.Deserialize(featureCollection, options);
            default:
                GeometryGeoJsonConverter converter = new();
                return converter.Read(ref reader, typeToConvert, options);
        }
    }
    public override void Write(Utf8JsonWriter writer, GeoJsonObject value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case { Type: GeoJsonObjectTypes.Feature}:
                Type[] genericArgs = value.GetType().GetGenericArguments();
                Type feature = typeof(Feature<,>).MakeGenericType(genericArgs);
                JsonSerializer.Serialize(writer, value, feature,  options);
                break;
            case { Type: GeoJsonObjectTypes.FeatureCollection}:
                genericArgs = value.GetType().GetGenericArguments();
                Type featureCollection = typeof(FeatureCollection<,>).MakeGenericType(genericArgs);
                JsonSerializer.Serialize(writer, value, featureCollection, options);
                break;
            case GeometryGeoJsonObject geometry:
                GeometryGeoJsonConverter converter = new();
                converter.Write(writer, geometry, options);
                break;
            default:
                throw new NotSupportedException($"The GeoJSON type '{value.GetType().Name}' is not supported for serialization.");
        }
    }
}
