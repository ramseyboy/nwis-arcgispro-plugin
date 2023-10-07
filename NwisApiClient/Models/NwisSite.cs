using CsvHelper.Configuration.Attributes;

namespace NwisApiClient.Models;

public interface INwisSite
{
    public string? AgencyCode { get; set; }

    public long SiteNumber { get; set; }

    public string? SiteName { get; set; }

    public string? SiteType { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? LatLongAccuracy { get; set; }

    public string? LatLongDatum { get; set; }

    public double? Altitude { get; set; }

    public double? AltitudeAccuracy { get; set; }

    public string? AltitudeDatum { get; set; }

    public string? HydrologicUnitCode { get; set; }
}

public sealed class NwisSite: INwisSite
{

    /*
    #  agency_cd       -- Agency
    #  site_no         -- Site identification number
    #  station_nm      -- Site name
    #  site_tp_cd      -- Site type
    #  dec_lat_va      -- Decimal latitude
    #  dec_long_va     -- Decimal longitude
    #  coord_acy_cd    -- Latitude-longitude accuracy
    #  dec_coord_datum_cd -- Decimal Latitude-longitude datum
    #  alt_va          -- Altitude of Gage/land surface
    #  alt_acy_va      -- Altitude accuracy
    #  alt_datum_cd    -- Altitude datum
    #  huc_cd          -- Hydrologic unit code
    */

    [Name("agency_cd")]
    public string? AgencyCode { get; set; }

    [Name("site_no")]
    public long SiteNumber { get; set; }

    [Name("station_nm")]
    public string? SiteName { get; set; }

    [Name("site_tp_cd")]
    public string? SiteType { get; set; }

    [Name("dec_lat_va")]
    public double? Latitude { get; set; }

    [Name("dec_long_va")]
    public double? Longitude { get; set; }

    [Name("coord_acy_cd")]
    public string? LatLongAccuracy { get; set; }

    [Name("dec_coord_datum_cd")]
    public string? LatLongDatum { get; set; }

    [Name("alt_va")]
    public double? Altitude { get; set; }

    [Name("alt_acy_va")]
    public double? AltitudeAccuracy { get; set; }

    [Name("alt_datum_cd")]
    public string? AltitudeDatum { get; set; }

    [Name("huc_cd")]
    public string? HydrologicUnitCode { get; set; }

}