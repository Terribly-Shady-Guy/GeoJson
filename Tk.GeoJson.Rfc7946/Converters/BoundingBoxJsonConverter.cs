using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tk.GeoJson.Rfc7946.Converters;

/// <summary>
/// A <see cref="JsonConverter"/> that is responsible for converting an <see cref="IBoundingBox"/> to and from a JSON array.
/// </summary>
public sealed class BoundingBoxJsonConverter : JsonConverter<IBoundingBox>
{
    public override IBoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("bbox must be an array.");
        }

        Span<double> bboxBuffer = stackalloc double[6];

        int count = 0;
        while (count < bboxBuffer.Length && ShouldStillRead(ref reader))
        {
            bboxBuffer[count] = reader.GetDouble();
            count++;
        }

        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new JsonException("bbox must not contain more than 6 elements.");
        }

        return count switch
        {
            4 => new TwoDimensionalBoundingBox
            {
                West = bboxBuffer[0],
                South = bboxBuffer[1],
                East = bboxBuffer[2],
                North = bboxBuffer[3],
            },
            6 => new ThreeDimensionalBoundingBox
            {
                West = bboxBuffer[0],
                South = bboxBuffer[1],
                MinAltitude = bboxBuffer[2],
                East = bboxBuffer[3],
                North = bboxBuffer[4],
                MaxAltitude = bboxBuffer[5]
            },
            _ => throw new JsonException("A valid bbox must contain 4 or 6 elements.")
        };
    }

    public override void Write(Utf8JsonWriter writer, IBoundingBox value, JsonSerializerOptions options)
    {
        ThreeDimensionalBoundingBox? box3D = value as ThreeDimensionalBoundingBox;
        writer.WriteStartArray();

        writer.WriteNumberValue(value.West);
        writer.WriteNumberValue(value.South);

        if (box3D != null)
        {
            writer.WriteNumberValue(box3D.MinAltitude);
        }

        writer.WriteNumberValue(value.East);
        writer.WriteNumberValue(value.North);

        if (box3D != null)
        {
            writer.WriteNumberValue(box3D.MaxAltitude);
        }

        writer.WriteEndArray();
    }

    private static bool ShouldStillRead(ref Utf8JsonReader reader)
    {
        return reader.Read() && reader.TokenType != JsonTokenType.EndArray;
    }
}
