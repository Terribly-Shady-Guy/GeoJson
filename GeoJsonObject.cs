using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Top level class representing all geojson types.
/// </summary>
[JsonConverter(typeof(GeoJsonObjectConverterFactory))]
public abstract class GeoJsonObject
{
    /// <summary>
    /// Represents the "type" property in a geojson object.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public abstract GeoJsonObjectTypes Type { get; }

    /// <summary>
    /// Represents the "bbox" property in a geojson object. 
    /// <see cref="BoundingBox"/> instances map to a json array in the order of [minLon, minLat, maxLon, maxLat] for 2D bounding boxes and [minLon, minLat, minAlt, maxLon, maxLat, maxAlt] for 3D bounding boxes.
    /// </summary>
    /// <remarks>Optional. When null, the property is omitted from JSON.</remarks>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bbox")]
    [JsonPropertyOrder(1)]
    public BoundingBox? Bbox { get; init; } = null;

    /// <summary>
    /// The geojson rfc standard allows for nonstandard properties to be included in geojson objects. This property will contain these properties or null if none exists.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, object?>? ForeignMembers { get; init; } = null;
}
