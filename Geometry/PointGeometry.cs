using System.Text.Json.Serialization;

namespace GeoJson.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a single position.
/// </summary>
public sealed class PointGeometry : GeometryGeoJsonObject
{
    public PointGeometry(Position coordinates)
    {
        Coordinates = coordinates;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Point;

    /// <summary>
    /// The object's coordinates as a Position.
    /// </summary>
    /// <remarks>Required; must be provided during object initialization.</remarks>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public Position Coordinates { get; }
}
