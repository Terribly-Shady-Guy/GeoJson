using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Top level class representing all GeoJSON types.
/// </summary>
/// <remarks>
/// You should not directly inherit this class. Inheriting this class directly implies extending the type property which is not supported in rfc 7946.
/// <para> Deserializing a Feature will create a <see cref="Feature{TGeometry, TProperties}"/>, where TGeometry is <see cref="GeometryGeoJsonObject"/>? and TProperties is <see cref="System.Text.Json.Nodes.JsonObject"/>?. 
/// Deserializing a FeatureCollection will create a <see cref="FeatureCollection{TGeometry, TFeatureProperties}"/>, where TGeometry is <see cref="GeometryGeoJsonObject"/>? and TFeatureProperties is <see cref="System.Text.Json.Nodes.JsonObject"/>?.</para>
/// <para>This will be updated to be a closed type in a future version.</para>
/// </remarks>
[JsonConverter(typeof(GeoJsonObjectConverter))]
public abstract class GeoJsonObject
{
    /// <summary>
    /// Represents the "type" property in a GeoJSON object.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public abstract GeoJsonObjectTypes Type { get; }

    /// <summary>
    /// Represents the "bbox" property in a GeoJSON object. 
    /// <see cref="BoundingBox"/> instances map to a json array in the order of [minLon, minLat, maxLon, maxLat] for 2D bounding boxes and [minLon, minLat, minAlt, maxLon, maxLat, maxAlt] for 3D bounding boxes.
    /// </summary>
    /// <remarks>Optional. When null, the property is omitted from JSON.</remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bbox")]
    [JsonPropertyOrder(1)]
    public BoundingBox? BBox { get; init; } = null;

    /// <summary>
    /// The GeoJSON rfc standard allows for nonstandard properties to be included in GeoJSON objects. This property will contain these properties or null if none exists.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object?>? ForeignMembers { get; init; } = null;
}
