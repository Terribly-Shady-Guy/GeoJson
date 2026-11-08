using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Converters;

namespace Tk.GeoJson.Rfc7946;

/// <summary>
/// Represents a bounding box for a GeoJSON type.
/// </summary>
[JsonConverter(typeof(BoundingBoxJsonConverter))]
public abstract class BoundingBox
{
    public required double West { get; set; }
    public required double South { get; set; }
    public required double East { get; set; }
    public required double North { get; set; }
}

/// <summary>
/// Represents a 2D bounding box for a GeoJson type.
/// </summary>
public sealed class BoundingBox2D : BoundingBox
{
    public override string ToString()
    {
        return $"[{West},{South},{East},{North}]";
    }
}
/// <summary>
/// Represents a 3D bounding box for a GeoJSON type.
/// </summary>
public sealed class BoundingBox3D : BoundingBox
{
    public required double MinAltitude { get; set; }
    public required double MaxAltitude { get; set; }

    public override string ToString()
    {
        return $"[{West},{South},{MinAltitude},{East},{North},{MaxAltitude}]";
    }
}
