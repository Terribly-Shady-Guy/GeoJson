using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Converters;

namespace Tk.GeoJson.Rfc7946;

/// <summary>
/// Represents a bounding box for a GeoJSON type.
/// </summary>
[JsonConverter(typeof(BoundingBoxJsonConverter))]
public interface IBoundingBox
{
    public double West { get; set; }
    public double South { get; set; }
    public double East { get; set; }
    public double North { get; set; }
}
