using GeoJson.Converters;
using System.Text.Json.Serialization;

namespace GeoJson.Geometry;

/// <summary>
/// Represents an individual point in a geometry.
/// </summary>
/// <param name="Longitude">The point's longitude. This will be the first element in a position array.</param>
/// <param name="Latitude">The point's latitude. This will be the second element in a position array.</param>
/// <param name="Altitude">An optional altitude. This will be the last element in a position array if used.</param>
[JsonConverter(typeof(PositionJsonConverter))]
public readonly record struct Position(double Latitude, double Longitude, double? Altitude = null)
{
    public bool Is3D => Altitude != null;

    public override string ToString()
    {
        return Altitude != null ? $"[{Longitude},{Latitude},{Altitude}]" : $"[{Longitude},{Latitude}]";
    }
}
