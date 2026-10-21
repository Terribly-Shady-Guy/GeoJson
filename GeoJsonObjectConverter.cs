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
                Type feature = CreateGenericType(typeof(Feature<,>), typeToConvert);
                return (GeoJsonObject?)root.Deserialize(feature, options);

            case GeoJsonObjectTypes.FeatureCollection:
                Type featureCollection = CreateGenericType(typeof(FeatureCollection<,>), typeToConvert);
                return (GeoJsonObject?)root.Deserialize(featureCollection, options);

            default:
                GeometryGeoJsonConverter converter = new();
                return converter.Read(ref reader, typeToConvert, options);
        }
    }
    public override void Write(Utf8JsonWriter writer, GeoJsonObject value, JsonSerializerOptions options)
    {
        Type valueType = value.GetType();

        switch (value)
        {
            case { Type: GeoJsonObjectTypes.Feature}:
                Type feature = CreateGenericType(typeof(Feature<,>), valueType);
                JsonSerializer.Serialize(writer, value, feature,  options);
                break;

            case { Type: GeoJsonObjectTypes.FeatureCollection}:
                Type featureCollection = CreateGenericType(typeof(FeatureCollection<,>), valueType);
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

    private static Type CreateGenericType(Type generic, Type abstractType)
    {
        if (!abstractType.IsGenericType || abstractType.GetGenericTypeDefinition() != generic)
        {
            throw new InvalidOperationException($"Type {abstractType.Name} is not generic type.");
        }

        Type[] args = abstractType.GetGenericArguments();
        return generic.MakeGenericType(args);
    }
}
