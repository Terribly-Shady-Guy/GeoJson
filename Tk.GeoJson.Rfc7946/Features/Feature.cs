using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Geometry;

namespace Tk.GeoJson.Rfc7946.Features;

public readonly union FeatureIdentifier(string, double);

/// <summary>
/// Represents a Feature GeoJSON object type.
/// </summary>
/// <typeparam name="TGeometry">The type representing the Geometry property. Can be nullable.</typeparam>
/// <typeparam name="TProperties">The type representing the Properties property. Can be nullable.</typeparam>
public sealed class Feature<TGeometry, TProperties> : GeoJsonObject
    where TGeometry : IGeoJsonGeometry?
    where TProperties : class?
{
    public Feature(TGeometry geometry, TProperties properties)
    {
        Geometry = geometry;
        Properties = properties;
    }

    [JsonPropertyName("type")]
    public override GeoJsonObjectTypes Type => GeoJsonObjectTypes.Feature;

    /// <summary>
    /// Represents the "id" property in a feature. This will contain a unique identifier for the feature.
    /// </summary>
    /// <remarks>
    /// This field is optional and can be null. If null, this property will be omitted from the JSON output. 
    /// </remarks>
    [JsonPropertyName("id")]
    [JsonPropertyOrder(-1)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public FeatureIdentifier? Id { get; set; } = null;

    /// <summary>
    /// Represents the "geometry" property in a feature. This will contain a <see cref="GeometryGeoJsonObject"/> that maps to a supported geometry type listed in the rfc standard.
    /// </summary>
    [JsonPropertyName("geometry")]
    [JsonPropertyOrder(2)]
    public TGeometry Geometry { get; }

    /// <summary>
    /// Represents the "properties" property in a feature. This will contain a user defined type that maps to the properties of the feature. This can be null if the feature has no properties.
    /// </summary>
    [JsonPropertyName("properties")]
    [JsonPropertyOrder(3)]
    public TProperties Properties { get; }
}
