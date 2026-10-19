using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A type to represent an id value. id can either be string or double.
/// </summary>
[JsonConverter(typeof(FeatureIdentifierJsonConverter))]
public readonly struct FeatureIdentifier
{
    private enum FeatureIdentifierType : byte
    {
        None,
        Double,
        String
    }

    private readonly double _doubleValue;
    private readonly string _stringValue;
    private readonly FeatureIdentifierType _tag;

    public FeatureIdentifier(double id)
    {
        _doubleValue = id;
        _stringValue = string.Empty;
        _tag = FeatureIdentifierType.Double;
    }

    public FeatureIdentifier(string id)
    {
        _doubleValue = default;
        _stringValue = id;
        _tag = FeatureIdentifierType.String;
    }

    public readonly object? Value => _tag switch
    {
        FeatureIdentifierType.Double => _doubleValue,
        FeatureIdentifierType.String => _stringValue,
        _ => null
    };

    public bool TryGetValue(out double id)
    {
        if (_tag != FeatureIdentifierType.Double)
        {
            id = default;
            return false;
        }

        id = _doubleValue;
        return true;
    }

    public bool TryGetValue([NotNullWhen(true)] out string? id)
    {
        if (_tag != FeatureIdentifierType.String)
        {
            id = default;
            return false;
        }

        id = _stringValue;
        return true;
    }

    public static implicit operator FeatureIdentifier(double id) => new(id);
    public static implicit operator FeatureIdentifier(string id) => new(id);
}

public sealed class FeatureIdentifierJsonConverter : JsonConverter<FeatureIdentifier>
{
    public override FeatureIdentifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out double doubleValue))
        {
            return doubleValue;
        }
        else if (reader.TokenType == JsonTokenType.String)
        {
            string stringValue = reader.GetString() ?? string.Empty;
            return stringValue;
        }

        throw new JsonException($"Unexpected token parsing FeatureIdentifier. Expected Number or String, got {reader.TokenType}.");
    }

    public override void Write(Utf8JsonWriter writer, FeatureIdentifier value, JsonSerializerOptions options)
    {
        if (value.TryGetValue(out double doubleValue))
        {
            writer.WriteNumberValue(doubleValue);
        }
        else if (value.TryGetValue(out string? stringValue))
        {
            writer.WriteStringValue(stringValue);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}