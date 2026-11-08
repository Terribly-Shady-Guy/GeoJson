using System.Text.Json.Serialization;

namespace Tk.GeoJson.Rfc7946.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a sequence of positions.
/// </summary>
public sealed class LineStringGeometry : GeoJsonObject, IGeoJsonGeometry
{
    public LineStringGeometry(IEnumerable<Position> coordinates)
    {
        Coordinates = coordinates;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.LineString;

    /// <summary>
    /// Represents the "coordinates" property in a GeoJSON geometry object. For a "LineString" type, it will contain an array of sequential position arrays.
    /// </summary>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IEnumerable<Position> Coordinates { get; }
}
