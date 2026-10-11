using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a Feature geojson object type.
/// </summary>
/// <typeparam name="TProperties">The type representing the Properties property.</typeparam>
public sealed class Feature<TProperties> : GeoJsonObject
    where TProperties : class
{
    [JsonPropertyName("geometry")]
    public IGeometry? Geometry { get; set; }
    [JsonPropertyName("properties")]
    public required TProperties Properties { get; set; }
}

/// <summary>
/// Reporesents a collection of Feature geojson objects.
/// </summary>
/// <typeparam name="TFeatureProperties">The type used for each feature Properties property.</typeparam>
public sealed class FeatureCollection<TFeatureProperties> : GeoJsonObject
    where TFeatureProperties : class
{
    [JsonPropertyName("features")]
    public Feature<TFeatureProperties>[] Features { get; set; } = [];
}