using NetTopologySuite.Geometries;

namespace WaterData.ArcGis.Abstractions.DataSource;

/// <summary>
/// <para>
/// This abstract class serves as one of the key extensibility points that comprise the <i>Plugin Datastore Framework</i>.
/// Specifically, each instance of concrete class that implements this abstraction acts as a conduit between
/// a data structure in a third-party data source and a <see cref="T:ArcGIS.Core.Data.Table" /> (or <see cref="T:ArcGIS.Core.Data.FeatureClass" />) in ArcGIS Pro.
/// </para>
/// <para>
/// A plug-in table does not necessarily correspond to a database table on the back end. It can be any data structure or format, but it <i>presented</i> to ArcGIS as a table.
/// </para>
/// <para>
/// If the list of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginField" />s returned by <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.GetFields" /> has a field whose type is <see cref="F:ArcGIS.Core.Data.FieldType.Geometry" />,
/// then this concrete implementation is considered a feature class; otherwise, a table.
/// </para>
/// </summary>
public interface ITable
{
    /// <summary>
    /// Gets the name of this currently opened plug-in table or feature class.
    /// </summary>
    /// <returns>
    /// The name of this currently opened plug-in table or feature class.
    /// </returns>
    /// <remarks>
    /// The returned name string should be one of the values in the <see cref="T:System.Collections.Generic.IReadOnlyList`1" />
    /// returned by <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.GetTableNames" />.
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public string GetName();

    /// <summary>
    /// Gets all of the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginField" />s for this currently opened plug-in table or feature class.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IReadOnlyList`1" /> of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginField" />s.
    /// </returns>
    /// <remarks>
    /// If the list of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginField" />s returned by this method has a field whose type is <see cref="F:ArcGIS.Core.Data.FieldType.Geometry" />,
    /// then this concrete implementation is considered a feature class; otherwise, a table.
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public IReadOnlyList<Field> GetFields();

    /// <summary>
    /// Performs a non-spatial search on this plug-in table that satisfies the criteria set in the <paramref name="queryFilter" />.
    /// </summary>
    /// <param name="queryFilter">
    /// A query filter that specifies the search criteria.
    /// </param>
    /// <returns>
    /// A concrete instance of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate" /> that encapsulates the retrieved rows.
    /// </returns>
    /// <remarks>
    /// Please see the additional constraints that may be imposed on this method in the <i>remarks</i> section of
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.IsQueryLanguageSupported" />.
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public ICursor Search(QueryFilter queryFilter);

    /// <summary>
    /// Performs a spatial search on this plug-in feature class that satisfies the criteria set in the <paramref name="spatialQueryFilter" />.
    /// </summary>
    /// <param name="spatialQueryFilter">
    /// A spatial query filter that specifies the search criteria.
    /// </param>
    /// <returns>
    /// A concrete instance of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate" /> that encapsulates the retrieved rows.
    /// </returns>
    /// <remarks>
    /// Please see the additional constraints that may be imposed on this method in the <i>remarks</i> section of
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.IsQueryLanguageSupported" />.
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public ICursor Search(SpatialQueryFilter spatialQueryFilter);

    /// <summary>
    /// Gets the <see cref="T:ArcGIS.Core.Geometry.GeometryType" /> of this plug-in table if it supports spatial functionality.
    /// </summary>
    /// <returns>
    /// The type of shape this plug-in table supports.  The default is <see cref="F:ArcGIS.Core.Geometry.GeometryType.Unknown" />
    /// (i.e., a non-spatial table).
    /// </returns>
    public GeometryType GetShapeType() => GeometryType.Unknown;

    /// <summary>
    /// Gets the <see cref="T:ArcGIS.Core.Geometry.Envelope" /> of this plug-in table if it supports spatial functionality.
    /// </summary>
    /// <returns>
    /// The extent of this plug-in table if it supports spatial functionality.  The default is <c>null</c> (i.e., a non-spatial table).
    /// </returns>
    public Envelope GetExtent() => (Envelope) null;

    /// <summary>
    /// Gets a value indicating whether the plug-in table or feature class supports row count natively.
    /// </summary>
    /// <returns>
    /// <b>true</b> if the plug-in table or feature class supports row count natively; otherwise, <b>false</b>.  The default is <b>false</b>.
    /// </returns>
    /// <remarks>
    /// The reason why concrete implementations of this abstraction should overwrite this method and <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.GetNativeRowCount" />
    /// (if the underlying data source supports native row count) is strictly for performance reasons.  If this method returns <b>false</b>
    /// and a row count is needed for the plug-in table, the framework will call the <i>Search</i> method on the entire plug-in table
    /// and then manually iterate through the cursor to determine the number of rows.
    /// </remarks>
    public bool IsNativeRowCountSupported() => false;

    /// <summary>
    /// Gets the count of how many rows are currently in this plug-in table or feature class.
    /// </summary>
    /// <returns>
    /// The number of rows in this plug-in table or feature class.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The reason why concrete implementations of this abstraction should overwrite this method and <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.IsNativeRowCountSupported" />
    /// (if the underlying data source supports native row count) is strictly for performance reasons.  If <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.IsNativeRowCountSupported" />
    /// returns <b>false</b> and a row count is needed for the plug-in table, the framework will call the <i>Search</i> method
    /// on the entire plug-in table and then manually iterate through the cursor to determine the number of rows.
    /// </para>
    /// <para>
    /// The framework will <b>not</b> call this method if <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.IsNativeRowCountSupported" /> return <b>false</b>.
    /// </para>
    /// </remarks>
    /// <exception caption="" cref="T:System.NotSupportedException">
    /// This plug-in implementation does not support a native row count.
    /// </exception>
    public long GetNativeRowCount() => throw new NotSupportedException("This plugin implementation does not support native row count.");

    /// <summary>Gets the last modified timestamp of the table.</summary>
    /// <returns>
    /// The timestamp of when the table was last modified. The default is the minimum possible date <see cref="F:System.DateTime.MinValue" />.
    /// </returns>
    public virtual DateTime GetLastModifiedTime() => DateTime.MinValue;
}
