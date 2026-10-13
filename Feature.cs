using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a Feature geojson object type.
/// </summary>
/// <typeparam name="TProperties">The type representing the Properties property.</typeparam>
public sealed class Feature<TProperties>(GeometryGeoJsonObject? geometry, TProperties properties) : GeoJsonObject
    where TProperties : class
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Feature;

    [JsonPropertyName("geometry")]
    public GeometryGeoJsonObject? Geometry { get; } = geometry;
    [JsonPropertyName("properties")]
    public TProperties Properties { get; } = properties;
}

/// <summary>
/// Reporesents a collection of Feature geojson objects.
/// </summary>
/// <typeparam name="TFeatureProperties">The type used for each feature Properties property.</typeparam>
public sealed class FeatureCollection<TFeatureProperties>(IList<Feature<TFeatureProperties>> features) : GeoJsonObject
    where TFeatureProperties : class
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.FeatureCollection;

    [JsonPropertyName("features")]
    public IList<Feature<TFeatureProperties>> Features { get; } = features;
}