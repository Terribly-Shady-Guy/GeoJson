using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace GeoJson;

/// <summary>
/// A <see cref="JsonSerializerContext"/> that provides metadata for the geojson object types and a default for <see cref="Feature{TGeometry, TProperties}"/> and <see cref="FeatureCollection{TFeatureGeometry, TFeatureProperties}"/>.
/// Use this if you are using NativeAot or prefer to use source generation.
/// </summary>
/// <remarks>
/// You will have to handle any <see cref="Feature{TGeometry, TProperties}"/> or <see cref="FeatureCollection{TFeatureGeometry, TFeatureProperties}"/> types that don't use <see cref="GeoJson.GeometryGeoJsonObject"/>? and <see cref="System.Text.Json.Nodes.JsonObject"/>? in your own <see cref="JsonSerializerContext"/>.
/// Combine this with your json serializer context by using <see cref="JsonTypeInfoResolver.Combine(ReadOnlySpan{IJsonTypeInfoResolver?})"/> or add to <see cref="JsonSerializerOptions.TypeInfoResolverChain"/>.
/// </remarks>
[JsonSerializable(typeof(GeoJsonObject))]
[JsonSerializable(typeof(GeometryGeoJsonObject))]
[JsonSerializable(typeof(Feature<GeometryGeoJsonObject?, JsonObject?>))]
[JsonSerializable(typeof(FeatureCollection<GeometryGeoJsonObject?, JsonObject?>))]
[JsonSerializable(typeof(PointGeometry))]
[JsonSerializable(typeof(MultiPointGeometry))]
[JsonSerializable(typeof(LineStringGeometry))]
[JsonSerializable(typeof(MultiLineStringGeometry))]
[JsonSerializable(typeof(PolygonGeometry))]
[JsonSerializable(typeof(MultiPolygonGeometry))]
[JsonSerializable(typeof(GeometryCollection))]
public sealed partial class GeoJsonSerializerContext : JsonSerializerContext;
