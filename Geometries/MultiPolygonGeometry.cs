using System.Text.Json.Serialization;

namespace GeoJson.Geometries;

/// <summary>
/// A geometry GeoJSON type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygonGeometry : GeometryGeoJsonObject
{
    public MultiPolygonGeometry(IList<IList<IList<Position>>> coordinates)
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
    public IList<IList<IList<Position>>> Coordinates { get; }
}
