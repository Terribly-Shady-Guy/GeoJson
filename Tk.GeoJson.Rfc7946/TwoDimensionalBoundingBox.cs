namespace Tk.GeoJson.Rfc7946;

/// <summary>
/// Represents a 2D bounding box for a GeoJson type.
/// </summary>
public sealed class TwoDimensionalBoundingBox : IBoundingBox
{
    public required double West { get; set; }
    public required double South { get; set; }
    public required double East { get; set; }
    public required double North { get; set; }

    public override string ToString()
    {
        return $"[{West},{South},{East},{North}]";
    }
}
