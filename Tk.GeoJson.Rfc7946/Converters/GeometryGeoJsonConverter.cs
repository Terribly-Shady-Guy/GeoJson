using System.Text.Json;
using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Geometry;

namespace Tk.GeoJson.Rfc7946.Converters;

/// <summary>
/// A <see cref="JsonConverter"/> that is responsible for converting an <see cref="IGeoJsonGeometry"/> to and from a JSON object that is representing a geometry GeoJSON type.
/// </summary>
public sealed class GeometryGeoJsonConverter : JsonConverter<IGeoJsonGeometry>
{
    public override IGeoJsonGeometry? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected a JSON object for GeoJSON geometry.");
        }

        using var document = JsonDocument.ParseValue(ref reader);
        if (!document.RootElement.TryGetProperty("type", out JsonElement typeProperty))
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

    public override void Write(Utf8JsonWriter writer, IGeoJsonGeometry value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case Point point:
                JsonSerializer.Serialize(writer, point, options);
                break;
            case MultiPoint multiPoint:
                JsonSerializer.Serialize(writer, multiPoint, options);
                break;
            case LineString lineString:
                JsonSerializer.Serialize(writer, lineString, options);
                break;
            case MultiLineString multiLineString:
                JsonSerializer.Serialize(writer, multiLineString, options);
                break;
            case Polygon polygon:
                JsonSerializer.Serialize(writer, polygon, options);
                break;
            case MultiPolygon multiPolygon:
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

internal static class GeometryGeoJsonObjectFactory
{
    public static IGeoJsonGeometry? Create(JsonDocument document, GeoJsonObjectTypes type, JsonSerializerOptions options)
    {
        JsonElement root = document.RootElement;
        return type switch
        {
            GeoJsonObjectTypes.Point => root.Deserialize<Point>(options),
            GeoJsonObjectTypes.MultiPoint => root.Deserialize<MultiPoint>(options),
            GeoJsonObjectTypes.LineString => root.Deserialize<LineString>(options),
            GeoJsonObjectTypes.MultiLineString => root.Deserialize<MultiLineString>(options),
            GeoJsonObjectTypes.Polygon => root.Deserialize<Polygon>(options),
            GeoJsonObjectTypes.MultiPolygon => root.Deserialize<MultiPolygon>(options),
            GeoJsonObjectTypes.GeometryCollection => root.Deserialize<GeometryCollection>(options),
            _ => null
        };
    }
}