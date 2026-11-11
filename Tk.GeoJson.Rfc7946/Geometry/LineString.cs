using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.CoordinateModels;

namespace Tk.GeoJson.Rfc7946.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a sequence of positions.
/// </summary>
public sealed class LineString : GeoJsonObject, IGeoJsonGeometry
{
    public LineString(LineStringCoordinates coordinates)
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
    public LineStringCoordinates Coordinates { get; }
}
