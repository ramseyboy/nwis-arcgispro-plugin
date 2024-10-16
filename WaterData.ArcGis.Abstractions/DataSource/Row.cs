namespace WaterData.ArcGis.Abstractions.DataSource;

/// <summary>Represents a row in a plug-in table.</summary>
public sealed class Row
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginRow" /> class.
    /// </summary>
    public Row()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginRow" /> class with a list of field values that
    /// represent a specific row in a plug-in table.
    /// </summary>
    /// <remarks>
    /// A plug-in table does not necessarily correspond to a database table on the back end. It can be any data structure or format, but it <i>presented</i> to ArcGIS as a table.
    /// Similarly, the PluginRow represents an entity that appears to be a row in ArcGIS, but may not correspond to a row in a database management system.
    /// </remarks>
    /// <param name="values">
    /// The number of values and the type of each value should match those returned by
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.GetFields" />.
    /// </param>
    /// <remarks>
    /// The constructor delegates to the <see cref="P:ArcGIS.Core.Data.PluginDatastore.PluginRow.Values" /> property to assign <paramref name="values" />.
    /// </remarks>
    public Row(IEnumerable<object> values) => this.Values = (IReadOnlyList<object>) values.ToList<object>().AsReadOnly();

    /// <summary>
    /// Gets or sets the values representing a specific row in a plug-in table.
    /// </summary>
    /// <value>
    /// The number of values in the list and the type of each value should match those returned by
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.GetFields" />.
    /// </value>
    public IReadOnlyList<object> Values { get; set; }
}
