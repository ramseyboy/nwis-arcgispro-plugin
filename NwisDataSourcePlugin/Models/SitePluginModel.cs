using ArcGIS.Core.Data;
using NetTopologySuite.Geometries;
using NwisApiClient.Models;
using NwisDataSourcePlugin.Attributes;
using Geometry = ArcGIS.Core.Geometry.Geometry;

namespace NwisDataSourcePlugin.Models;

public class SitePluginModel : ISite
{
    [ArcGisPluginField(alias: "OBJECTID", fieldType: FieldType.OID)]
    public int ObjectId { get; set; }

    [ArcGisPluginField(alias: "SHAPE", fieldType: FieldType.Geometry)]
    public Geometry Shape { get; set; }

    [ArcGisPluginField(alias: "agency_cd", fieldType: FieldType.String)]
    public string AgencyCode { get; set; }

    [ArcGisPluginField(alias: "site_no", fieldType: FieldType.BigInteger)]
    public long SiteNumber { get; set; }

    [ArcGisPluginField(alias: "station_nm", fieldType: FieldType.String)]
    public string SiteName { get; set; }

    [ArcGisPluginField(alias: "site_tp_cd", fieldType: FieldType.String)]
    public string SiteType { get; set; }

    [ArcGisPluginField(alias: "dec_lat_va", fieldType: FieldType.Double)]
    public double? Latitude { get; set; }

    [ArcGisPluginField(alias: "dec_long_va", fieldType: FieldType.Double)]
    public double? Longitude { get; set; }

    [ArcGisPluginField(alias: "coord_acy_cd", fieldType: FieldType.String)]
    public string LatLongAccuracy { get; set; }

    [ArcGisPluginField(alias: "dec_coord_datum_cd", fieldType: FieldType.String)]
    public string LatLongDatum { get; set; }

    [ArcGisPluginField(alias: "alt_va", fieldType: FieldType.Double)]
    public double? Altitude { get; set; }

    [ArcGisPluginField(alias: "alt_acy_va", fieldType: FieldType.Double)]
    public double? AltitudeAccuracy { get; set; }

    [ArcGisPluginField(alias: "alt_datum_cd", fieldType: FieldType.String)]
    public string AltitudeDatum { get; set; }

    [ArcGisPluginField(alias: "huc_cd", fieldType: FieldType.String)]
    public string HydrologicUnitCode { get; set; }
}