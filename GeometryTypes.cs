using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A marker interface representing a geometry geojson type.
/// </summary>
[JsonConverter(typeof(GeometryGeoJsonObjectJsonConverter))]
public abstract class GeometryGeoJsonObject : GeoJsonObject;

/// <summary>
/// A geometry geojson type representing a single position.
/// </summary>
public sealed class PointGeometry(Position coordinates) : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Point;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public Position Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of positions.
/// </summary>
public sealed class MultiPointGeometry(IList<Position> coordinates) : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPoint;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<Position> Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a sequence of positions.
/// </summary>
public sealed class LineStringGeometry(IList<Position> coordinates) : GeometryGeoJsonObject  
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.LineString;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<Position> Coordinates { get; } = coordinates;
    
}

/// <summary>
/// A geometry geojson type representing a collection of LineStrings.
/// </summary>
public sealed class MultiLineStringGeometry(IList<IList<Position>> coordinates) : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiLineString;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<IList<Position>> Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a Polygon.
/// </summary>
public sealed class PolygonGeometry(IList<IList<Position>> coordinates) : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Polygon;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<IList<Position>> Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygonGeometry(IList<IList<IList<Position>>> coordinates) : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPolygon;

    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<IList<IList<Position>>> Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of geometries.
/// </summary>
public sealed class GeometryCollection(IList<GeometryGeoJsonObject> geometries) : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.GeometryCollection;

    [JsonPropertyName("geometries")]
    [JsonPropertyOrder(2)]
    public IList<GeometryGeoJsonObject> Geometries { get; } = geometries;
}