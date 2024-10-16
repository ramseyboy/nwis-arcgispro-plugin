namespace WaterData.ArcGis.Abstractions.DataSource;

public enum DatasetType
{
    /// <summary>Unknown Dataset.</summary>
    Unknown = 1,
    /// <summary>Feature Dataset.</summary>
    FeatureDataset = 4,
    /// <summary>Feature Class.</summary>
    FeatureClass = 5,
    /// <summary>Topology.</summary>
    Topology = 8,
    /// <summary>Table Dataset.</summary>
    Table = 10, // 0x0000000A
    /// <summary>Relationship Class.</summary>
    RelationshipClass = 11, // 0x0000000B
    /// <summary>Raster Dataset.</summary>
    RasterDataset = 12, // 0x0000000C
    /// <summary>Raster Band.</summary>
    RasterBand = 13, // 0x0000000D
    /// <summary>CadDrawing Dataset.</summary>
    CadDrawing = 15, // 0x0000000F
    /// <summary>Mosaic Dataset.</summary>
    MosaicDataset = 29, // 0x0000001D
    /// <summary>Utility Network.</summary>
    UtilityNetwork = 33, // 0x00000021
    /// <summary>Parcel Fabric.</summary>
    ParcelFabric = 38, // 0x00000026
    /// <summary>Attributed Relationship Class.</summary>
    AttributedRelationshipClass = 99, // 0x00000063
}
