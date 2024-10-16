namespace WaterData.ArcGis.Abstractions.DataSource;

/// <summary>
/// Represents a column in a plug-in table or feature class.
/// </summary>
/// <remarks>
/// A plug-in table does not necessarily correspond to a database table on the back end. It can be any data structure or format, but it is <i>presented</i> to ArcGIS as a table.
/// Similarly, the third-party data structure exposed through the Plugin Datasource does not necessarily contains <i>fields</i>, but they are exposed to ArcGIS in this fashion.
/// </remarks>
public sealed class Field
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginField" /> class.
    /// </summary>
    public Field()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginField" /> class.
    /// </summary>
    /// <param name="name">The name of the field.</param>
    /// <param name="aliasName">The alias name of the field.</param>
    /// <param name="fieldType">
    /// The <see cref="T:ArcGIS.Core.Data.FieldType" /> of the field.
    /// </param>
    public Field(string name, string aliasName, FieldType fieldType)
    {
        this.Name = name;
        this.AliasName = aliasName;
        this.FieldType = fieldType;
    }

    /// <summary>Gets or sets the name of the field.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the alias name of the field.</summary>
    public string AliasName { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:ArcGIS.Core.Data.FieldType" /> of the field.
    /// </summary>
    public FieldType FieldType { get; set; }

    /// <summary>Gets or sets the length of a text field.</summary>
    public int Length { get; set; }
}
