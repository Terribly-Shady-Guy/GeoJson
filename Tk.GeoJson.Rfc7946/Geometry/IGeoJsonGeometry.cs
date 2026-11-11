using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Converters;

namespace Tk.GeoJson.Rfc7946.Geometry;

/// <summary>
/// An interface representing a geometry GeoJSON type.
/// </summary>
/// <remarks>
/// You should not directly implement this interface. Implementing this interface directly implies extending the type property which is not supported in rfc 7946.
/// Attempting to serialize or deserialize a a custom <see cref="IGeoJsonGeometry"/> type will result in <see cref="NotSupportedException"/> being thrown.
/// </remarks>
[JsonConverter(typeof(GeometryGeoJsonConverter))]
public interface IGeoJsonGeometry
{
    /// <summary>
    /// Represents the "type" property in a geometry GeoJSON object.
    /// </summary>
    [JsonPropertyName("type")]
    public GeoJsonObjectTypes Type { get; }
}