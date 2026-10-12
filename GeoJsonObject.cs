using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Top level class representing all geojson types.
/// </summary>
public abstract class GeoJsonObject
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bbox")]
    public BoundingBox? Bbox { get; init; } = null;

    [JsonExtensionData]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, JsonNode>? ForeignMembers { get; init; } = null;
}
