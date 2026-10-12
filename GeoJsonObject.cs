using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Top level class representing all geojson types.
/// </summary>
public abstract class GeoJsonObject
{
    [JsonInclude]
    [JsonPropertyName("type")]
    internal abstract GeoJsonObjectTypes Type { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bbox")]
    public BoundingBox? Bbox { get; init; } = null;

    [JsonExtensionData]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, JsonElement>? ForeignMembers { get; init; } = null;

}
