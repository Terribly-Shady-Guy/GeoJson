using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Top level class representing all geojson types.
/// </summary>
[JsonConverter(typeof(GeoJsonObjectConverterFactory))]
public abstract class GeoJsonObject
{
    [JsonPropertyName("type")]
    [JsonPropertyOrder(0)]
    public abstract GeoJsonObjectTypes Type { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bbox")]
    [JsonPropertyOrder(1)]
    public BoundingBox? Bbox { get; init; } = null;

    [JsonExtensionData]
    public IDictionary<string, JsonElement>? ForeignMembers { get; init; } = null;
}
