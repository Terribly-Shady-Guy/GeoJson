using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a bounding box for a GeoJSON type.
/// </summary>
[JsonConverter(typeof(BoundingBoxConverter))]
public record BoundingBox
{
    public required double West { get; init; }
    public required double South { get; init; }
    public required double East { get; init; }
    public required double North { get; init; }

    public virtual double[] ToArray()
    {
        return [West, South, East, North];
    }

    public override string ToString()
    {
        return $"[{West},{South},{East},{North}]";
    }
}

/// <summary>
/// Represents a 3D bounding box for a GeoJSON type.
/// </summary>
public record BoundingBox3D : BoundingBox
{
    public required double MinAltitude { get; init; }
    public required double MaxAltitude { get; init; }

    public override double[] ToArray()
    {
        return [West, South, MinAltitude, East, North, MaxAltitude];
    }

    public override string ToString()
    {
        return $"[{West},{South},{MinAltitude},{East},{North},{MaxAltitude}]";
    }
}

public sealed class BoundingBoxConverter : JsonConverter<BoundingBox>
{
    public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
            4 => new BoundingBox
            {
                West = bboxBuffer[0],
                South = bboxBuffer[1],
                East = bboxBuffer[2],
                North = bboxBuffer[3],
            },
            6 => new BoundingBox3D
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

    public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
    {
        BoundingBox3D? box3D = value as BoundingBox3D;
        writer.WriteStartArray();

        writer.WriteNumberValue(value.West);
        writer.WriteNumberValue(value.South);

        if (box3D != null) writer.WriteNumberValue(box3D.MinAltitude);

        writer.WriteNumberValue(value.East);
        writer.WriteNumberValue(value.North);

        if (box3D != null) writer.WriteNumberValue(box3D.MaxAltitude);

        writer.WriteEndArray();
    }

    private static bool ShouldStillRead(ref Utf8JsonReader reader)
    {
        return reader.Read() && reader.TokenType != JsonTokenType.EndArray;
    }
}

