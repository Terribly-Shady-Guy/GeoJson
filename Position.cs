using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents an individual point in a geometry.
/// </summary>
/// <param name="Longitude">The point's longitude. This will be the first element in a position array.</param>
/// <param name="Latitude">The point's latitude. This will be the second element in a position array.</param>
/// <param name="Altitude">An optional altitude. This will be the last element in a position array if used.</param>
[JsonConverter(typeof(PositionJsonConverter))]
public readonly record struct Position(double Longitude, double Latitude, double? Altitude = null);

public sealed class PositionJsonConverter : JsonConverter<Position>
{
    public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Value must be an array of numbers.");
        }

        double latitude = 0;
        double longitude = 0;
        double? altitude = null;

        int position = 0;
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            if (position > 2)
            {
                ThrowJsonExceptionForInvalidSize();
            }

            switch (position)
            {
                case 0:
                    longitude = reader.GetDouble();
                    break;
                case 1:
                    latitude = reader.GetDouble();
                    break;
                case 2:
                    altitude = reader.GetDouble();
                    break;
            }

            position++;
        }

        if (position < 1)
        {
            ThrowJsonExceptionForInvalidSize();
        }

        return new Position(longitude, latitude, altitude);
    }

    public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        writer.WriteNumberValue(value.Longitude);
        writer.WriteNumberValue(value.Latitude);
        if (value.Altitude != null)
        {
            writer.WriteNumberValue((double)value.Altitude);
        }

        writer.WriteEndArray();
    }

    private static void ThrowJsonExceptionForInvalidSize()
    {
        throw new JsonException("A valid position array must contain 2 to 3 elements.");
    }
}