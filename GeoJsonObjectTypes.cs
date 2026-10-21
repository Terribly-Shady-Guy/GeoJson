using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents the different GeoJSON types.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GeoJsonObjectTypes
{
    /// <summary>
    /// Value represents a Point GeoJSON type in the type property.
    /// </summary>
    Point = 0,
    /// <summary>
    /// Value represents a MultiPoint GeoJSON type in the type property.
    /// </summary>
    MultiPoint = 1,
    /// <summary>
    /// Value represents a LineString GeoJSON type in the type property.
    /// </summary>
    LineString = 2,
    /// <summary>
    /// Value represents a MultiLineString GeoJSON type in the type property.
    /// </summary>
    MultiLineString = 3,
    /// <summary>
    /// Value represents a Polygon GeoJSON type in the type property.
    /// </summary>
    Polygon = 4,
    /// <summary>
    /// Value represents a MultiPolygon GeoJSON type in the type property.
    /// </summary>
    MultiPolygon = 5,
    /// <summary>
    /// Value represents a GeometryCollection GeoJSON type in the type property.
    /// </summary>
    GeometryCollection = 6,
    /// <summary>
    /// Value represents a Feature GeoJSON type in the type property.
    /// </summary>
    Feature = 7,
    /// <summary>
    /// Value represents a FeatureCollection GeoJSON type in the type property.
    /// </summary>
    FeatureCollection = 8
}