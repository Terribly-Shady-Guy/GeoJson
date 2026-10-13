using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeometryGeoJsonObjectJsonConverter : JsonConverter<GeometryGeoJsonObject>
{
    public override GeometryGeoJsonObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        JsonElement root = jsonDocument.RootElement;

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
            case PointGeometry:
                JsonSerializer.Serialize(writer: writer, (PointGeometry)value, options);
                break;
            case MultiPointGeometry:
                JsonSerializer.Serialize(writer: writer, (MultiPointGeometry)value, options);
                break;
            case LineStringGeometry:
                JsonSerializer.Serialize(writer: writer, (LineStringGeometry)value, options);
                break;
            case MultiLineStringGeometry:
                JsonSerializer.Serialize(writer: writer, (MultiLineStringGeometry)value, options);
                break;
            case PolygonGeometry:
                JsonSerializer.Serialize(writer: writer, (PolygonGeometry)value, options);
                break;
            case MultiPolygonGeometry:
                JsonSerializer.Serialize(writer: writer, (MultiPolygonGeometry)value, options);
                break;
            case GeometryCollection:
                JsonSerializer.Serialize(writer: writer, (GeometryCollection)value, options);
                break;
            default:
                throw new NotSupportedException($"The geometry type '{value.GetType().Name}' is not supported for serialization.");
        }
    }
}
