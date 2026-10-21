using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeoJsonObjectConverter : JsonConverter<GeoJsonObject>
{
    public override GeoJsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        if (!document.RootElement.TryGetProperty("type", out JsonElement typeProperty))
        {
            throw new JsonException("Missing 'type' property in GeoJSON object.");
        }

        string? unparsedType = typeProperty.GetString();
        if (!Enum.TryParse(unparsedType, out GeoJsonObjectTypes geoJsonType))
        {
            throw new JsonException($"Invalid GeoJSON type: {unparsedType}");
        }

        return geoJsonType switch
        {
            GeoJsonObjectTypes.Feature => document.RootElement
                .Deserialize<Feature<GeometryGeoJsonObject?, JsonObject?>>(options),
            GeoJsonObjectTypes.FeatureCollection => document.RootElement
                .Deserialize<FeatureCollection<GeometryGeoJsonObject?, JsonObject?>>(options),
            _ => GeometryGeoJsonObjectFactory.Create(document, geoJsonType, options),
        };
    }

    public override void Write(Utf8JsonWriter writer, GeoJsonObject value, JsonSerializerOptions options)
    {
        Type valueType = value.GetType();

        switch (value)
        {
            case { Type: GeoJsonObjectTypes.Feature }:
                Type feature = CreateGenericType(typeof(Feature<,>), valueType);
                JsonSerializer.Serialize(writer, value, feature, options);
                break;

            case { Type: GeoJsonObjectTypes.FeatureCollection }:
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
