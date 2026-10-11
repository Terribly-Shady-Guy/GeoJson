using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A marker interface representing a geometry geojson type.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(PointGeometry), nameof(GeoJsonObjectTypes.Point))]
[JsonDerivedType(typeof(MultiPointGeometry), nameof(GeoJsonObjectTypes.MultiPoint))]
[JsonDerivedType(typeof(LineStringGeometry), nameof(GeoJsonObjectTypes.LineString))]
[JsonDerivedType(typeof(MultiLineStringGeometry), nameof(GeoJsonObjectTypes.MultiLineString))]
[JsonDerivedType(typeof(PolygonGeometry), nameof(GeoJsonObjectTypes.Polygon))]
[JsonDerivedType(typeof(MultiPolygonGeometry), nameof(GeoJsonObjectTypes.MultiPolygon))]
[JsonDerivedType(typeof(GeometryCollection), nameof(GeoJsonObjectTypes.GeometryCollection))]
public interface IGeometry;

/// <summary>
/// A geometry geojson type representing a single position.
/// </summary>
public sealed class PointGeometry(Position coordinates) : GeoJsonObject, IGeometry
{
    [JsonPropertyName("coordinates")]
    public Position Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of positions.
/// </summary>
public sealed class MultiPointGeometry(Position[] coordinates) : GeoJsonObject, IGeometry
{
    [JsonPropertyName("coordinates")]
    public Position[] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a sequence of positions.
/// </summary>
public sealed class LineStringGeometry(Position[] coordinates) : GeoJsonObject, IGeometry   
{
    [JsonPropertyName("coordinates")]
    public Position[] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of LineStrings.
/// </summary>
public sealed class MultiLineStringGeometry(Position[][] coordinates) : GeoJsonObject, IGeometry
{
    [JsonPropertyName("coordinates")]
    public Position[][] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a Polygon.
/// </summary>
public sealed class PolygonGeometry(Position[][] coordinates) : GeoJsonObject, IGeometry
{
    [JsonPropertyName("coordinates")]
    public Position[][] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygonGeometry(Position[][][] coordinates) : GeoJsonObject, IGeometry
{
    [JsonPropertyName("coordinates")]
    public Position[][][] Coordinates { get; } = coordinates;
}

/// <summary>
/// A geometry geojson type representing a collection of geometries.
/// </summary>
public sealed class GeometryCollection(IGeometry[] geometries) : GeoJsonObject, IGeometry
{
    [JsonPropertyName("geometries")]
    public IGeometry[] Geometries { get; } = geometries;
}