using System.Text.Json.Serialization;

namespace GeoJson.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygonGeometry : GeometryGeoJsonObject
{
    public MultiPolygonGeometry(IEnumerable<IEnumerable<IEnumerable<Position>>> coordinates)
    {
        Coordinates = coordinates;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPolygon;

    /// <summary>
    /// Represents the "coordinates" property in a GeoJSON geometry object. For a "MultiPolygon" type, it will contain an array of polygons.
    /// </summary>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IEnumerable<IEnumerable<IEnumerable<Position>>> Coordinates { get; }
}
