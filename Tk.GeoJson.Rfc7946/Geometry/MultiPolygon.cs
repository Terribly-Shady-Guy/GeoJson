using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.CoordinateModels;

namespace Tk.GeoJson.Rfc7946.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a collection of polygons.
/// </summary>
public sealed class MultiPolygon : GeoJsonObject, IGeoJsonGeometry
{
    public MultiPolygon(IList<PolygonCoordinates> coordinates)
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
    public IList<PolygonCoordinates> Coordinates { get; }
}
