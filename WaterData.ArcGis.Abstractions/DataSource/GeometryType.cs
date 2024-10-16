namespace WaterData.ArcGis.Abstractions.DataSource;

/// <summary>Describes the different types of geometry.</summary>
public enum GeometryType
{
    /// <summary>
    /// Unknown type. There will be no Geometry instance existing with this type.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Point geometry. See <see cref="T:ArcGIS.Core.Geometry.MapPoint" />.
    /// </summary>
    Point = 513, // 0x00000201
    /// <summary>
    /// Envelope geometry. See <see cref="T:ArcGIS.Core.Geometry.Envelope" />.
    /// </summary>
    Envelope = 3077, // 0x00000C05
    /// <summary>
    /// Bag of geometries.  See <see cref="T:ArcGIS.Core.Geometry.GeometryBag" />.
    /// </summary>
    GeometryBag = 3594, // 0x00000E0A
    /// <summary>
    /// Multipoint geometry. See <see cref="T:ArcGIS.Core.Geometry.Multipoint" />.
    /// </summary>
    Multipoint = 8710, // 0x00002206
    /// <summary>
    /// Polyline geometry. See <see cref="T:ArcGIS.Core.Geometry.Polyline" />.
    /// </summary>
    Polyline = 25607, // 0x00006407
    /// <summary>
    /// Polygon geometry. See <see cref="T:ArcGIS.Core.Geometry.Polygon" />.
    /// </summary>
    Polygon = 27656, // 0x00006C08
    /// <summary>
    ///  MultiPatch 3D surface. See <see cref="T:ArcGIS.Core.Geometry.Multipatch" />.
    /// </summary>
    Multipatch = 32777, // 0x00008009
}
