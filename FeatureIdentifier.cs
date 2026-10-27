using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

/// <summary>
/// A type to represent an id value. Id can either be string or double.
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
    private readonly FeatureIdentifierType _discriminator;

    /// <summary>
    /// Initializes an instance of <see cref="FeatureIdentifier"/> as a <see cref="double"/>.
    /// </summary>
    /// <param name="value">The double value to initialize with.</param>
    public FeatureIdentifier(double value)
    {
        _doubleValue = value;
        _stringValue = string.Empty;
        _discriminator = FeatureIdentifierType.Double;
    }

    /// <summary>
    /// Initializes an instance of <see cref="FeatureIdentifier"/> as a <see cref="string"/>.
    /// </summary>
    /// <param name="value">The string to initialize with.</param>
    public FeatureIdentifier(string value)
    {
        _doubleValue = default;
        _stringValue = value;
        _discriminator = FeatureIdentifierType.String;
    }

    /// <summary>
    /// Gets the underlying value of the instance.
    /// </summary>
    public readonly object? Value => _discriminator switch
    {
        FeatureIdentifierType.Double => _doubleValue,
        FeatureIdentifierType.String => _stringValue,
        _ => null
    };

    /// <summary>
    /// Indicates that the <see cref="FeatureIdentifier"/> instance contains a value.
    /// </summary>
    public bool HasValue => _discriminator != FeatureIdentifierType.None;

    /// <summary>
    /// Attempts to unwrap the <see cref="FeatureIdentifier"/> to its underlying <see cref="double"/>.
    /// </summary>
    /// <param name="value">The double value to unwrap.</param>
    /// <returns>True if the unwrap was successful; otherwise, false.</returns>
    public bool TryGetValue(out double value)
    {
        bool isDouble = _discriminator == FeatureIdentifierType.Double;
        value = isDouble ? _doubleValue : default;
        return isDouble;
    }

    /// <summary>
    /// Attempts to unwrap the <see cref="FeatureIdentifier"/> to its underlying <see cref="string"/>.
    /// </summary>
    /// <param name="value">The string value to unwrap.</param>
    /// <returns>True if the unwrap was successful; otherwise, false.</returns>
    public bool TryGetValue([NotNullWhen(true)] out string? value)
    {
        bool isString = _discriminator == FeatureIdentifierType.String;
        value = isString ? _stringValue : null;
        return isString;
    }

    public static implicit operator FeatureIdentifier(double value) => new(value);
    public static implicit operator FeatureIdentifier(string value) => new(value);
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
            string stringValue = reader.GetString() ?? throw new JsonException("String cannot be null.");
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