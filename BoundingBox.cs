using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// Represents a bounding box for a geojson type.
/// </summary>
[JsonConverter(typeof(BoundingBoxConverter))]
public record BoundingBox
{
    public required double MinLongitude { get; init; }
    public required double MinLatitude { get; init; }
    public required double MaxLongitude { get; init; }
    public required double MaxLatitude { get; init; }
}

/// <summary>
/// Represents a 3D bounding box for a geojson type.
/// </summary>
public record BoundingBox3D : BoundingBox
{
    public required double MinAltitude { get; init; }
    public required double MaxAltitude { get; init; }
}

public class BoundingBoxConverter : JsonConverter<BoundingBox>
{
    public override BoundingBox Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("bbox must be an array.");
        }
        
        Span<double> bboxBuffer = stackalloc double[6];

        int count;
        for (count = 0; count < bboxBuffer.Length && ShouldStillRead(reader); count++)
        {
            bboxBuffer[count] = reader.GetDouble();
        }

        return count switch
        {
            4 => new BoundingBox
            {
                MinLongitude = bboxBuffer[0],
                MinLatitude = bboxBuffer[1],
                MaxLongitude = bboxBuffer[2],
                MaxLatitude = bboxBuffer[3],
            },
            6 => new BoundingBox3D
            {
                MinLongitude = bboxBuffer[0],
                MinLatitude = bboxBuffer[1],
                MinAltitude = bboxBuffer[2],
                MaxLongitude = bboxBuffer[3],
                MaxLatitude = bboxBuffer[4],
                MaxAltitude = bboxBuffer[5]
            },
            _ => throw new JsonException("A valid bbox must contain 4 or 6 elements.")
        };
    }

    public override void Write(Utf8JsonWriter writer, BoundingBox value, JsonSerializerOptions options)
    {
        BoundingBox3D? box3D = value as BoundingBox3D;
        writer.WriteStartArray();

        writer.WriteNumberValue(value.MinLongitude);
        writer.WriteNumberValue(value.MinLatitude);

        if (box3D != null) writer.WriteNumberValue(box3D.MinAltitude);

        writer.WriteNumberValue(value.MaxLongitude);
        writer.WriteNumberValue(value.MaxLatitude);

        if (box3D != null) writer.WriteNumberValue(box3D.MaxAltitude);

        writer.WriteEndArray();
    }

    private static bool ShouldStillRead(Utf8JsonReader reader)
    {
        return reader.Read() && reader.TokenType != JsonTokenType.EndArray;
    }
}

