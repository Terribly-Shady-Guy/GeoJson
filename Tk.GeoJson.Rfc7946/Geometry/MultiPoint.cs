using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.CoordinateModels;

namespace Tk.GeoJson.Rfc7946.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a collection of positions.
/// </summary>
public sealed class MultiPoint : GeoJsonObject, IGeoJsonGeometry
{
    public MultiPoint(IEnumerable<Position> coordinates)
    {
        Coordinates = coordinates;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiPoint;

    /// <summary>
    /// Represents the "coordinates" property in a GeoJSON geometry object. For a "MultiPoint" type, it will contain an array of arbitrary position arrays.
    /// </summary>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IEnumerable<Position> Coordinates { get; }
}
