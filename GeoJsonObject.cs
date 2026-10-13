using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Top level class representing all geojson types.
/// </summary>
[JsonConverter(typeof(GeoJsonObjectConverterFactory))]
public abstract class GeoJsonObject
{
    [JsonInclude]
    [JsonPropertyName("type")]
    [JsonPropertyOrder(-100)]
    internal abstract GeoJsonObjectTypes Type { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bbox")]
    [JsonPropertyOrder(-99)]
    public BoundingBox? Bbox { get; init; } = null;

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ForeignMembers { get; init; } = null;

}
