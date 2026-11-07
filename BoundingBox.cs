using GeoJson.Converters;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a bounding box for a GeoJSON type.
/// </summary>
[JsonConverter(typeof(BoundingBoxJsonConverter))]
public abstract record BoundingBox
{
    public required double West { get; init; }
    public required double South { get; init; }
    public required double East { get; init; }
    public required double North { get; init; }
}

public sealed record BoundingBox2D : BoundingBox
{
    public override string ToString()
    {
        return $"[{West},{South},{East},{North}]";
    }
}
/// <summary>
/// Represents a 3D bounding box for a GeoJSON type.
/// </summary>
public sealed record BoundingBox3D : BoundingBox
{
    public required double MinAltitude { get; init; }
    public required double MaxAltitude { get; init; }

    public override string ToString()
    {
        return $"[{West},{South},{MinAltitude},{East},{North},{MaxAltitude}]";
    }
}
