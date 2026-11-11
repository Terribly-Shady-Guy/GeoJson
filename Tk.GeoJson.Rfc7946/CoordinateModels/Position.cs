using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Converters;

namespace Tk.GeoJson.Rfc7946.CoordinateModels;

/// <summary>
/// Represents an individual position in a geometry.
/// </summary>
/// <param name="Longitude">The position's longitude. This will be the first element in a position array.</param>
/// <param name="Latitude">The position's latitude. This will be the second element in a position array.</param>
/// <param name="Altitude">An optional altitude. This will be the last element in a position array if used.</param>
[JsonConverter(typeof(PositionJsonConverter))]
public readonly record struct Position(
    double Longitude,
    double Latitude,
    double? Altitude = null) : ICoordinates
{
    public int Dimensions => Altitude != null ? 3 : 2;

    public override string ToString()
    {
        return Altitude != null ? $"[{Longitude},{Latitude},{Altitude}]" : $"[{Longitude},{Latitude}]";
    }
}
