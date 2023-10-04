using ArcGIS.Core.Data;
using NwisApiClient.Models;
using NwisDataSourcePlugin.Attributes;

namespace NwisDataSourcePlugin.Models;

public class SitePluginModel : ISite
{
    [ArcGisPluginField(alias: "agency_cd", fieldType: FieldType.String)]
    public string AgencyCode { get; set; }

    [ArcGisPluginField(alias: "site_no", fieldType: FieldType.BigInteger)]
    public long? SiteNumber { get; set; }

    [ArcGisPluginField(alias: "station_nm", fieldType: FieldType.String)]
    public string SiteName { get; set; }

    [ArcGisPluginField(alias: "site_tp_cd", fieldType: FieldType.String)]
    public string SiteType { get; set; }

    [ArcGisPluginField(alias: "dec_lat_va", fieldType: FieldType.Double)]
    public decimal? Latitude { get; set; }

    [ArcGisPluginField(alias: "dec_long_va", fieldType: FieldType.Double)]
    public decimal? Longitude { get; set; }

    /*[ArcGisFieldType(fieldType: FieldType.Geometry)]
    public decimal? LatLong { get; set; }*/

    [ArcGisPluginField(alias: "coord_acy_cd", fieldType: FieldType.String)]
    public string LatLongAccuracy { get; set; }

    [ArcGisPluginField(alias: "dec_coord_datum_cd", fieldType: FieldType.String)]
    public string LatLongDatum { get; set; }

    [ArcGisPluginField(alias: "alt_va", fieldType: FieldType.Double)]
    public decimal? Altitude { get; set; }

    [ArcGisPluginField(alias: "alt_acy_va", fieldType: FieldType.Double)]
    public decimal? AltitudeAccuracy { get; set; }

    [ArcGisPluginField(alias: "alt_datum_cd", fieldType: FieldType.String)]
    public string AltitudeDatum { get; set; }

    [ArcGisPluginField(alias: "huc_cd", fieldType: FieldType.String)]
    public string HydrologicUnitCode { get; set; }
}