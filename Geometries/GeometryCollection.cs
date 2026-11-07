using System.Text.Json.Serialization;

namespace GeoJson.Geometries;

/// <summary>
/// A geometry GeoJSON type representing a collection of geometries.
/// </summary>
public sealed class GeometryCollection : GeometryGeoJsonObject
{
    public GeometryCollection(IEnumerable<GeometryGeoJsonObject> geometries)
    {
        Geometries = geometries;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.GeometryCollection;

    /// <summary>
    /// Represents the "geometries" property in a GeometryCollection GeoJSON object. This will contain a list of other geometry GeoJSON types.
    /// </summary>
    [JsonPropertyName("geometries")]
    [JsonPropertyOrder(2)]
    public IEnumerable<GeometryGeoJsonObject> Geometries { get; }
}