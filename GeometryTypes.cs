using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A marker interface representing a geometry geojson type.
/// </summary>
[JsonConverter(typeof(GeometryGeoJsonConverter))]
public abstract class GeometryGeoJsonObject : GeoJsonObject;

/// <summary>
/// A geometry geojson type representing a single position.
/// </summary>
public sealed class PointGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Point;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required Position Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a collection of positions.
/// </summary>
public sealed class MultiPointGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPoint;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required IList<Position> Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a sequence of positions.
/// </summary>
public sealed class LineStringGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.LineString;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required IList<Position> Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a collection of LineStrings.
/// </summary>
public sealed class MultiLineStringGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiLineString;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required IList<IList<Position>> Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a Polygon.
/// </summary>
public sealed class PolygonGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Polygon;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required IList<IList<Position>> Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygonGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPolygon;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required IList<IList<IList<Position>>> Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a collection of geometries.
/// </summary>
public sealed class GeometryCollection : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.GeometryCollection;

    [JsonPropertyName("geometries")]
    [JsonPropertyOrder(2)]
    public required IList<GeometryGeoJsonObject> Geometries { get; init; }
}