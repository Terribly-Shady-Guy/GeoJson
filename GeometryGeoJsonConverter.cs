using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeometryGeoJsonConverter : JsonConverter<GeometryGeoJsonObject>
{
    public override GeometryGeoJsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var document = JsonDocument.ParseValue(ref reader);
        JsonElement root = document.RootElement;

        JsonElement typeProperty = root.GetProperty("type");
        string? unparsedType = typeProperty.GetString();

        if (!Enum.TryParse<GeoJsonObjectTypes>(unparsedType, out var type))
        {
            throw new JsonException($"Invalid GeoJSON type: {unparsedType}");
        }

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

    public override void Write(Utf8JsonWriter writer, GeometryGeoJsonObject value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case PointGeometry point:
                JsonSerializer.Serialize(writer: writer, point, options);
                break;
            case MultiPointGeometry multiPoint:
                JsonSerializer.Serialize(writer: writer, multiPoint, options);
                break;
            case LineStringGeometry lineString:
                JsonSerializer.Serialize(writer: writer, lineString, options);
                break;
            case MultiLineStringGeometry multiLineString:
                JsonSerializer.Serialize(writer: writer, multiLineString, options);
                break;
            case PolygonGeometry polygon:
                JsonSerializer.Serialize(writer: writer, polygon, options);
                break;
            case MultiPolygonGeometry multiPolygon:
                JsonSerializer.Serialize(writer: writer, multiPolygon, options);
                break;
            case GeometryCollection geometryCollection:
                JsonSerializer.Serialize(writer: writer, geometryCollection, options);
                break;
            default:
                throw new NotSupportedException($"The geometry type '{value.GetType().Name}' is not supported for serialization.");
        }
    }
}
