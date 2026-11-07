using GeoJson.Geometries;
using System.Text.Json.Serialization;

namespace GeoJson.Features;

/// <summary>
/// Reporesents a collection of Feature GeoJSON objects.
/// </summary>
/// <typeparam name="TFeatureGeometry">The type used for each feature Geometry property. Can be nullable.</typeparam>
/// <typeparam name="TFeatureProperties">The type used for each feature Properties property. Can be nullable.</typeparam>
public sealed class FeatureCollection<TFeatureGeometry, TFeatureProperties> : GeoJsonObject
    where TFeatureGeometry : GeometryGeoJsonObject?
    where TFeatureProperties : class?
{
    public FeatureCollection(IEnumerable<Feature<TFeatureGeometry, TFeatureProperties>> features)
    {
        Features = features;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.FeatureCollection;

    /// <summary>
    /// Represents the "features" property in a feature collection. This will contain a list of <see cref="Feature{TGeometry, TFeatureProperties}"/> objects.
    /// </summary>
    [JsonPropertyName("features")]
    [JsonPropertyOrder(2)]
    public IEnumerable<Feature<TFeatureGeometry, TFeatureProperties>> Features { get; }
}