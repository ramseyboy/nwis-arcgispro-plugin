namespace WaterData.ArcGis.Abstractions.DataSource;

public enum FieldType
{
    /// <summary>16-bit Integer.</summary>
    SmallInteger,
    /// <summary>32-bit Integer.</summary>
    Integer,
    /// <summary>Single-precision floating-point number.</summary>
    Single,
    /// <summary>Double-precision floating-point number.</summary>
    Double,
    /// <summary>Character string.</summary>
    String,
    /// <summary>Date.</summary>
    Date,
    /// <summary>32-bit Integer representing an object identifier.</summary>
    OID,
    /// <summary>Geometry.</summary>
    Geometry,
    /// <summary>Binary Large Object.</summary>
    Blob,
    /// <summary>Raster.</summary>
    Raster,
    /// <summary>Globally Unique Identifier.</summary>
    GUID,
    /// <summary>Esri Global ID.</summary>
    GlobalID,
    /// <summary>XML Document.</summary>
    XML,
    /// <summary>64-bit Integer.Reserved for future use.</summary>
    BigInteger,
    /// <summary>Date Only. Reserved for future use.</summary>
    DateOnly,
    /// <summary>Time Only. Reserved for future use.</summary>
    TimeOnly,
    /// <summary>Timestamp Offset. Reserved for future use.</summary>
    TimestampOffset,
}
