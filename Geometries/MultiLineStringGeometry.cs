using System.Text.Json.Serialization;

namespace GeoJson.Geometries;

/// <summary>
/// A geometry GeoJSON type representing a collection of LineStrings.
/// </summary>
public sealed class MultiLineStringGeometry : GeometryGeoJsonObject
{
    public MultiLineStringGeometry(IList<IList<Position>> coordinates)
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
    public IList<IList<Position>> Coordinates { get; }
}
