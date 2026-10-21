using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A marker class representing a geometry geojson type.
/// </summary>
/// <remarks>
/// You should not directly inherit this class. Inheriting this class directly implies extending the type property which is not supported in rfc 7946.
/// <para>This will be updated to be a closed type in a future version.</para>
/// </remarks>
[JsonConverter(typeof(GeometryGeoJsonConverter))]
public abstract class GeometryGeoJsonObject : GeoJsonObject;

/// <summary>
/// A geometry geojson type representing a single position.
/// </summary>
public sealed class PointGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Point;

    /// <summary>
    /// The object's coordinates as a Position.
    /// </summary>
    /// <remarks>Required; must be provided during object initialization.</remarks>
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

    /// <summary>
    /// Represents the "coordinates" property in a geojson geometry object. For a "MultiPoint" type, it will contain an array of arbitrary position arrays.
    /// </summary>
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

    /// <summary>
    /// Represents the "coordinates" property in a geojson geometry object. For a "LineString" type, it will contain an array of sequential position arrays.
    /// </summary>
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

    /// <summary>
    /// Represents the "coordinates" property in a geojson geometry object. For a "MultiLineString" type, it will contain an array of LineString arrays.
    /// </summary>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public required IList<IList<Position>> Coordinates { get; init; }
}

/// <summary>
/// A geometry geojson type representing a Polygon.
/// </summary>
/// <remarks>
/// A valid Polygon must follow these rules:
/// <list type="bullet">
/// <item>The first element in the array will represent the exterior ring, and subsequent elements will represent interior holes.</item>
/// <item>A linear ring's first and last positions must be identical, and have at least four positions.</item>
/// <item>A Polygon should follow the right-hand rule. The exterior ring should be oriented counterclockwise, and the interior rings should be oriented clockwise.</item>
/// </list>
/// </remarks>
/// 
public sealed class PolygonGeometry : GeometryGeoJsonObject
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Polygon;

    /// <summary>
    /// Represents the "coordinates" property in a geojson geometry object. For a "Polygon" type, it will contain an array of linear rings.
    /// </summary>
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

    /// <summary>
    /// Represents the "coordinates" property in a geojson geometry object. For a "MultiPolygon" type, it will contain an array of polygons.
    /// </summary>
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

    /// <summary>
    /// Represents the "geometries" property in a GeometryCollection geojson object. This will contain a list of other geometry geojson types.
    /// </summary>
    [JsonPropertyName("geometries")]
    [JsonPropertyOrder(2)]
    public required IList<GeometryGeoJsonObject> Geometries { get; init; }
}