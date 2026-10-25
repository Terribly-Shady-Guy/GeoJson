using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeometryGeoJsonConverter : JsonConverter<GeometryGeoJsonObject>
{
    public override GeometryGeoJsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        JsonElement root = document.RootElement;

        if (!root.TryGetProperty("type", out JsonElement typeProperty))
        {
            throw new JsonException("Missing 'type' property in GeoJSON object.");
        }

        string? unparsedType = typeProperty.GetString();

        if (!Enum.TryParse(unparsedType, out GeoJsonObjectTypes type))
        {
            throw new JsonException($"Invalid GeoJSON type: {unparsedType}");
        }

        return GeometryGeoJsonObjectFactory.Create(document, type, options);
    }

    public override void Write(Utf8JsonWriter writer, GeometryGeoJsonObject value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case PointGeometry point:
                JsonSerializer.Serialize(writer, point, options);
                break;
            case MultiPointGeometry multiPoint:
                JsonSerializer.Serialize(writer, multiPoint, options);
                break;
            case LineStringGeometry lineString:
                JsonSerializer.Serialize(writer, lineString, options);
                break;
            case MultiLineStringGeometry multiLineString:
                JsonSerializer.Serialize(writer, multiLineString, options);
                break;
            case PolygonGeometry polygon:
                JsonSerializer.Serialize(writer, polygon, options);
                break;
            case MultiPolygonGeometry multiPolygon:
                JsonSerializer.Serialize(writer, multiPolygon, options);
                break;
            case GeometryCollection geometryCollection:
                JsonSerializer.Serialize(writer, geometryCollection, options);
                break;
            default:
                throw new NotSupportedException($"The geometry type '{value.GetType().Name}' is not supported for serialization.");
        }
    }
}

public static class GeometryGeoJsonObjectFactory
{
    public static GeometryGeoJsonObject? Create(JsonDocument document, GeoJsonObjectTypes type, JsonSerializerOptions options)
    {
        JsonElement root = document.RootElement;
        return type switch
        {
            GeoJsonObjectTypes.Point => root.Deserialize<PointGeometry>(options),
            GeoJsonObjectTypes.MultiPoint => root.Deserialize<MultiPointGeometry>(options),
            GeoJsonObjectTypes.LineString => root.Deserialize<LineStringGeometry>(options),
            GeoJsonObjectTypes.MultiLineString => root.Deserialize<MultiLineStringGeometry>(options),
            GeoJsonObjectTypes.Polygon => root.Deserialize<PolygonGeometry>(options),
            GeoJsonObjectTypes.MultiPolygon => root.Deserialize<MultiPolygonGeometry>(options),
            GeoJsonObjectTypes.GeometryCollection => root.Deserialize<GeometryCollection>(options),
            _ => null
        };
    }
}