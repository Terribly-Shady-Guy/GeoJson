namespace Tk.GeoJson.Rfc7946;

/// <summary>
/// Represents a 3D bounding box for a GeoJSON type.
/// </summary>
public sealed class ThreeDimensionalBoundingBox : IBoundingBox
{
    public required double MinAltitude { get; set; }
    public required double MaxAltitude { get; set; }
    public required double West { get; set; }
    public required double South { get; set; }
    public required double East { get; set; }
    public required double North { get; set; }

    public override string ToString()
    {
        return $"[{West},{South},{MinAltitude},{East},{North},{MaxAltitude}]";
    }
}
