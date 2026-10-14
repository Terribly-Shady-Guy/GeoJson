using System.Text.Json;
using System.Text.Json.Serialization;

namespace GeoJson;

public sealed class GeoJsonObjectConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(GeoJsonObject).IsAssignableFrom(typeToConvert);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert.IsAssignableFrom(typeof(GeometryGeoJsonObject)))
        {
            return new GeometryGeoJsonObjectJsonConverter();
        }
        
        if (typeToConvert.IsGenericType)
        {
            Type genericDef = typeToConvert.GetGenericTypeDefinition();
            if (genericDef == typeof(Feature<,>) || genericDef == typeof(FeatureCollection<,>))
            {
                Type[] genericArgs = typeToConvert.GetGenericArguments();
                Type closedConverterType = typeof(GeoJsonFeaturesConverter<,>).MakeGenericType(genericArgs);
                return (JsonConverter?)Activator.CreateInstance(closedConverterType);
            }
        }

        return null;
    }
}