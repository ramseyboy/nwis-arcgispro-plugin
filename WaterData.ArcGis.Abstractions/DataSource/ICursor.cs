namespace WaterData.ArcGis.Abstractions.DataSource;

/// <summary>
/// <para>
/// This abstract class serves as one of the key extensibility points that comprise the <i>Plugin Datastore Framework</i>.
/// Specifically, each instance of concrete class that implements this abstraction acts as a conduit between
/// a cursor traversing a data structure in a third-party data source and a <see cref="T:ArcGIS.Core.Data.RowCursor" /> object in ArcGIS Pro.
/// </para>
/// </summary>
public interface ICursor
{
    /// <summary>
    /// Advances to the next <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginRow" /> in this concrete instance of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate" />.
    /// </summary>
    /// <returns>
    /// <b>true</b> if the cursor has successfully advanced to the next row; otherwise, <b>false</b>.
    /// </returns>
    /// <remarks>
    /// If <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate.MoveNext" /> returns <b>false</b>, the framework will <b>not</b> call <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate.GetCurrentRow" />.
    /// </remarks>
    public bool MoveNext();

    /// <summary>
    /// Gets the current <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginRow" /> in this concrete instance of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate" />.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginRow" /> if the cursor has not advanced past the end of the table; otherwise, <b>null</b>.
    /// </returns>
    /// <remarks>
    /// If <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate.MoveNext" /> returns <b>false</b>, the framework will <b>not</b> call <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginCursorTemplate.GetCurrentRow" />.
    /// </remarks>
    public Row GetCurrentRow();
}
