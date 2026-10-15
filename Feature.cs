using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a Feature geojson object type.
/// </summary>
/// <typeparam name="TGeometry">The type representing the Geometry property.</typeparam>
/// <typeparam name="TProperties">The type representing the Properties property.</typeparam>
public sealed class Feature<TGeometry, TProperties> : GeoJsonObject
    where TProperties : class?
    where TGeometry : GeometryGeoJsonObject?
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Feature;

    /// <summary>
    /// Represents the "geometry" property in a feature. This will contain a <see cref="GeometryGeoJsonObject"/> that maps to a supported geometry type listed in the rfc standard.
    /// </summary>
    [JsonPropertyName("geometry")]
    [JsonPropertyOrder(2)]
    public required TGeometry Geometry { get; init; }

    /// <summary>
    /// Represents the "properties" property in a feature. This will contain a user defined type that maps to the properties of the feature. This can be null if the feature has no properties.
    /// </summary>
    [JsonPropertyName("properties")]
    [JsonPropertyOrder(3)]
    public required TProperties Properties { get; init; }
}

/// <summary>
/// Reporesents a collection of Feature geojson objects.
/// </summary>
/// <typeparam name="TGeometry">The type used for each feature Geometry property.</typeparam>
/// <typeparam name="TFeatureProperties">The type used for each feature Properties property.</typeparam>
public sealed class FeatureCollection<TGeometry, TFeatureProperties> : GeoJsonObject
    where TFeatureProperties : class?
    where TGeometry : GeometryGeoJsonObject?
{
    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.FeatureCollection;

    /// <summary>
    /// Represents the "features" property in a feature collection. This will contain a list of <see cref="Feature{TGeometry, TFeatureProperties}"/> objects.
    /// </summary>
    [JsonPropertyName("features")]
    [JsonPropertyOrder(2)]
    public required IList<Feature<TGeometry, TFeatureProperties>> Features { get; init; }
}