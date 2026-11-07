using GeoJson.Converters;
using System.Text.Json.Serialization;

namespace GeoJson.Geometries;

/// <summary>
/// A marker class representing a geometry GeoJSON type.
/// </summary>
/// <remarks>
/// You should not directly inherit this class. Inheriting this class directly implies extending the type property which is not supported in rfc 7946.
/// Attempting to serialize or deserialize a a custom <see cref="GeometryGeoJsonObject"/> type will result in <see cref="NotSupportedException"/> being thrown.
/// <para>This will be updated to be a closed type in a future version.</para>
/// </remarks>
[JsonConverter(typeof(GeometryGeoJsonConverter))]
public abstract class GeometryGeoJsonObject : GeoJsonObject;