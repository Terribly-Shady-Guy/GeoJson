using System.Text.Json.Serialization;

namespace GeoJson.Geometries;

/// <summary>
/// A geometry GeoJSON type representing a Polygon.
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
    public PolygonGeometry(IList<IList<Position>> coordinates)
    {
        Coordinates = coordinates;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Polygon;

    /// <summary>
    /// Represents the "coordinates" property in a GeoJSON geometry object. For a "Polygon" type, it will contain an array of linear rings.
    /// </summary>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<IList<Position>> Coordinates { get; }
}
