using System.Text.Json;
using System.Text.Json.Serialization;
using Tk.GeoJson.Rfc7946.Geometry;

namespace Tk.GeoJson.Rfc7946.Converters;

/// <summary>
/// A <see cref="JsonConverter"/> that is responsible for converting a <see cref="Position"/> to and from a JSON array.
/// </summary>
public sealed class PositionJsonConverter : JsonConverter<Position>
{
    private const int LatitudeIndex = 1;
    private const int LongitudeIndex = 0;
    private const int AltitudeIndex = 2;

    public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array for Position.");
        }

        int position = 0;

        double longitude = 0;
        double latitude = 0;
        double? altitude = null;

        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            switch (position)
            {
                case LongitudeIndex:
                    longitude = reader.GetDouble();
                    break;
                case LatitudeIndex:
                    latitude = reader.GetDouble();
                    break;
                case AltitudeIndex:
                    altitude = reader.GetDouble();
                    break;
                default:
                    throw new JsonException("Unexpected element in Position array.");
            }

            position++;
        }

        if (position < 2)
        {
            throw new JsonException("Expected at least two elements in Position array.");
        }

        return new Position(latitude, longitude, altitude);
    }

    public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteNumberValue(value.Longitude);
        writer.WriteNumberValue(value.Latitude);

        if (value.Altitude != null)
        {
            writer.WriteNumberValue(value.Altitude.Value);
        }

        writer.WriteEndArray();
    }
}
