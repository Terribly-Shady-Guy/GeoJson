using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a Feature geojson object type.
/// </summary>
/// <typeparam name="TGeometry">The type representing the Geometry property.</typeparam>
/// <typeparam name="TProperties">The type representing the Properties property.</typeparam>
public sealed class Feature<TGeometry, TProperties>(TGeometry? geometry, TProperties properties) : GeoJsonObject
    where TProperties : class
    where TGeometry : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Feature;

    [JsonPropertyName("geometry")]
    [JsonPropertyOrder(2)]
    public TGeometry? Geometry { get; } = geometry;
    [JsonPropertyName("properties")]
    [JsonPropertyOrder(3)]
    public TProperties Properties { get; } = properties;
}

/// <summary>
/// Reporesents a collection of Feature geojson objects.
/// </summary>
/// <typeparam name="TGeometry">The type used for each feature Geometry property.</typeparam>
/// <typeparam name="TFeatureProperties">The type used for each feature Properties property.</typeparam>
public sealed class FeatureCollection<TGeometry, TFeatureProperties>(IList<Feature<TGeometry, TFeatureProperties>> features) : GeoJsonObject
    where TFeatureProperties : class
    where TGeometry : GeometryGeoJsonObject
{
    internal override GeoJsonObjectTypes Type => GeoJsonObjectTypes.FeatureCollection;

    [JsonPropertyName("features")]
    [JsonPropertyOrder(2)]
    public IList<Feature<TGeometry, TFeatureProperties>> Features { get; } = features;
}