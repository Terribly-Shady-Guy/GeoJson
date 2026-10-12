using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A marker interface representing a geometry geojson type.
/// </summary>
public abstract class GeometryGeoJsonObject : GeoJsonObject;

/// <summary>
/// A geometry geojson type representing a single position.
/// </summary>
public sealed class PointGeometry(Position coordinates) : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Point;

    [JsonPropertyName("coordinates")]
    public Position Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of positions.
/// </summary>
public sealed class MultiPointGeometry(Position[] coordinates) : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPoint;

    [JsonPropertyName("coordinates")]
    public Position[] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a sequence of positions.
/// </summary>
public sealed class LineStringGeometry(Position[] coordinates) : GeometryGeoJsonObject  
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.LineString;
    [JsonPropertyName("coordinates")]
    public Position[] Coordinates { get; } = coordinates;
    
}

/// <summary>
/// A geometry geojson type representing a collection of LineStrings.
/// </summary>
public sealed class MultiLineStringGeometry(Position[][] coordinates) : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiLineString;
    [JsonPropertyName("coordinates")]
    public Position[][] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a Polygon.
/// </summary>
public sealed class PolygonGeometry(Position[][] coordinates) : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Polygon;
    [JsonPropertyName("coordinates")]
    public Position[][] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygonGeometry(Position[][][] coordinates) : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPolygon;
    [JsonPropertyName("coordinates")]
    public Position[][][] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of geometries.
/// </summary>
public sealed class GeometryCollection(GeometryGeoJsonObject[] geometries) : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.GeometryCollection;
    [JsonPropertyName("geometries")]
    public GeometryGeoJsonObject[] Geometries { get; } = geometries;
}