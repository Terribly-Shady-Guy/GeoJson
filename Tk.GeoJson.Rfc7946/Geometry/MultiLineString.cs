using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.CoordinateModels;

namespace Tk.GeoJson.Rfc7946.Geometry;

/// <summary>
/// A geometry GeoJSON type representing a collection of LineStrings.
/// </summary>
public sealed class MultiLineString : GeoJsonObject, IGeoJsonGeometry
{
    public MultiLineString(IList<LineStringCoordinates> coordinates)
    {
        Coordinates = coordinates;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.MultiLineString;

    /// <summary>
    /// Represents the "coordinates" property in a GeoJSON geometry object. For a "MultiLineString" type, it will contain an array of LineString arrays.
    /// </summary>
    [JsonPropertyName("coordinates")]
    [JsonPropertyOrder(2)]
    public IList<LineStringCoordinates> Coordinates { get; }
}
