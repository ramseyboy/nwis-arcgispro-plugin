using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using WaterData.ArcGis.Plugin.DataSource.Attributes;
using WaterData.Nwis.Models;

namespace WaterData.ArcGis.Plugin.DataSource.Models;

public class NwisSitePluginModel : INwisSite
{
    [ArcGisPluginField("OBJECTID", FieldType.OID)]
    public int ObjectId { get; set; }

    [ArcGisPluginField("SHAPE", FieldType.Geometry)]
    public Geometry Shape { get; set; }

    [ArcGisPluginField("agency_cd", FieldType.String)]
    public string AgencyCode { get; set; }

    [ArcGisPluginField("site_no", FieldType.BigInteger)]
    public long SiteNumber { get; set; }

    [ArcGisPluginField("station_nm", FieldType.String)]
    public string SiteName { get; set; }

    [ArcGisPluginField("site_tp_cd", FieldType.String)]
    public string SiteType { get; set; }

    [ArcGisPluginField("dec_lat_va", FieldType.Double)]
    public double? Latitude { get; set; }

    [ArcGisPluginField("dec_long_va", FieldType.Double)]
    public double? Longitude { get; set; }

    [ArcGisPluginField("coord_acy_cd", FieldType.String)]
    public string LatLongAccuracy { get; set; }

    [ArcGisPluginField("dec_coord_datum_cd", FieldType.String)]
    public string LatLongDatum { get; set; }

    [ArcGisPluginField("alt_va", FieldType.Double)]
    public double? Altitude { get; set; }

    [ArcGisPluginField("alt_acy_va", FieldType.Double)]
    public double? AltitudeAccuracy { get; set; }

    [ArcGisPluginField("alt_datum_cd", FieldType.String)]
    public string AltitudeDatum { get; set; }

    [ArcGisPluginField("huc_cd", FieldType.String)]
    public string HydrologicUnitCode { get; set; }
}