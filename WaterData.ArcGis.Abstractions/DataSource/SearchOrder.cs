namespace WaterData.ArcGis.Abstractions.DataSource;

public enum SearchOrder
{
    /// <summary>Search using the spatial aspect of the filter first.</summary>
    Spatial,
    /// <summary>
    /// Search using the attribute aspect of the filter first.
    /// </summary>
    Attribute,
}
