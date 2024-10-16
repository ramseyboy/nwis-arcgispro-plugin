namespace WaterData.ArcGis.Abstractions.DataSource;

  /// <summary>
  /// <para>
  /// This abstract class serves as one of the key extensibility points that comprise the <i>Plugin Datastore Framework</i>.
  /// Specifically, each instance of a concrete class that implements this abstraction acts as a conduit between
  /// a third-party data source and ArcGIS Pro via the deployment of a plug-in data source add-in.
  /// </para>
  /// <para>
  /// Currently, the framework only supports <see cref="F:ArcGIS.Core.Data.DatasetType.Table" />s and
  /// <see cref="F:ArcGIS.Core.Data.DatasetType.FeatureClass" />s in a read-only manner.
  /// </para>
  /// </summary>
  public interface IDatasource
  {
    /// <summary>
    /// Formally opens the Plugin Datasource in order for the new data format to be integrated into ArcGIS Pro.
    /// </summary>
    /// <param name="connectionPath">
    /// The connection path to the actual data source.
    /// </param>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// When the Plugin Datasource framework calls this method, the concrete implementation is expected to perform any necessary
    /// instantiation and initialization to commence the data integration process.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// When a Plugin Datasource add-in is loaded into the system at runtime, the framework may instantiate multiple instances of
    /// the concrete PluginDatasourceTemplate implementation.  In particular, when a plug-in data source with a specific
    /// <see cref="P:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceConnectionPath.PluginIdentifier" /> is used to open a specific <paramref name="connectionPath" />,
    /// the framework will create a new instance of this concrete implementation followed by calling this <c>Open</c> method.
    /// If there are pertinent data that must be reflected across all instances of this concrete implementation, e.g., if the value of
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.IsQueryLanguageSupported" /> depends on a given data source associated with the <paramref name="connectionPath" />
    /// and if said value must be reflected across all instances in order for the framework to behave properly, then it is the
    /// responsibility of this concrete implementation to ensure the data are properly shared and/or updated.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the Framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public void Open(Uri connectionPath);

    /// <summary>
    /// Formally closes the Plugin Datasource.  This operation is the opposite of <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.Open(System.Uri)" />.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// When the framework calls this method, the concrete implementation is expected to perform any necessary
    /// housekeeping and cleanup operation to close the plug-in data source.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The framework will ignore any exception that is raised by the concrete implementation.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    public void Close();

    /// <summary>
    /// Gets an instance of concrete class that implements the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate" />
    /// abstraction associated with <paramref name="name" /> in the plug-in data source.
    /// </summary>
    /// <param name="name">
    /// The name of a data structure entity that is exposed to ArcGIS as a table or feature class via the Plugin Datasource.
    /// </param>
    /// <returns>
    /// An instance of the concrete class that implements the <see cref="T:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate" /> abstraction.
    /// </returns>
    /// <remarks>
    /// Any input <paramref name="name" /> argument for this method should be one of the returned values in <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.GetTableNames" />.
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public ITable OpenTable(string name);

    /// <summary>
    /// Gets the name of all the tables and feature classes that exist in the currently opened plug-in data source.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IReadOnlyList`1" /> of table names.
    /// </returns>
    /// <remarks>
    /// <para>
    /// These plug-in tables and feature classes do not necessarily correspond to database tables on the back end. They can be any data
    /// structure or format in the third-party data source, but are presented to ArcGIS as a table via the Plugin Datasource.
    /// </para>
    /// <para>
    /// The concrete implementation will decide whether the returned table names are fully qualified or unqualified as long as
    /// the names can be successfully used as arguments to <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginDatasourceTemplate.OpenTable(System.String)" />.
    /// </para>
    /// </remarks>
    /// <exception caption="" cref="T:System.Exception">
    /// Signals to the framework that an exception derived from <see cref="T:System.Exception" /> has occurred.
    /// </exception>
    public IReadOnlyList<string> GetTableNames();

    /// <summary>
    /// Gets a value indicating whether the underlying data source supports a query language (e.g., SQL).
    /// </summary>
    /// <returns>
    /// <b>true</b> if the underlying data source supports a query language; otherwise, <b>false</b>.  The default is <b>false</b>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If the underlying data source supports a query language (e.g., SQL) by returning <b>true</b>,
    /// the framework will exhibit the following behavior:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// If <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> is specified in the argument to <see cref="M:ArcGIS.Core.Data.Table.Search(ArcGIS.Core.Data.QueryFilter,System.Boolean)" />,
    /// the framework will relay said where clause by setting <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> with the same value
    /// when it calls either <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.Search(ArcGIS.Core.Data.QueryFilter)" /> or
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.Search(ArcGIS.Core.Data.SpatialQueryFilter)" />.
    /// </description>
    /// </item>
    /// </list>
    /// <para>
    /// If the underlying data source does <b>not</b> support a query language (e.g., SQL) by returning <b>false</b>,
    /// the framework will exhibit the following behavior:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// The framework will <b>not</b> relay the <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> regardless of whether or not a where clause
    /// is specified by the user.  In other words, the <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> is an empty string when the framework
    /// calls either <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.Search(ArcGIS.Core.Data.QueryFilter)" /> or
    /// <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.Search(ArcGIS.Core.Data.SpatialQueryFilter)" />.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <para>
    /// However, if a user-specified <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> exists and said where clause has a subexpression involving
    /// a <see cref="T:ArcGIS.Core.Data.Field" /> of type <see cref="F:ArcGIS.Core.Data.FieldType.OID" />, the framework will transform the objectID value(s)
    /// into a list for <see cref="P:ArcGIS.Core.Data.QueryFilter.ObjectIDs" />.  For example:
    /// </para>
    /// <para>
    /// <code>
    /// QueryFilter queryFilter = new QueryFilter()
    /// {
    ///   WhereClause = String.Format(“{0} = {1}”, “OBJECTID”, 2888)  // the OBJECTID field is of type FieldType.OID.
    /// };
    ///
    /// using (RowCursor rowCursor = featureClass.Search(queryFilter))
    /// {}
    /// </code>
    /// </para>
    /// If <see cref="P:ArcGIS.Core.Data.QueryFilter.ObjectIDs" /> is specified by the user in their <see cref="T:ArcGIS.Core.Data.QueryFilter" />, the framework will relay said ObjectIDs by setting
    /// <see cref="P:ArcGIS.Core.Data.QueryFilter.ObjectIDs" /> in the <see cref="T:ArcGIS.Core.Data.QueryFilter" /> that is passed to <see cref="M:ArcGIS.Core.Data.PluginDatastore.PluginTableTemplate.Search(ArcGIS.Core.Data.QueryFilter)" />.
    /// <para>
    /// </para>
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    public bool IsQueryLanguageSupported() => false;

    /// <summary>
    /// Gets a string description for <paramref name="datasetType" />.
    /// </summary>
    /// <param name="datasetType">The data set type.</param>
    /// <returns>
    /// A string description for <paramref name="datasetType" />.  The default is <c>datasetType.ToString()</c>.
    /// </returns>
    /// <remarks>
    /// Currently, the framework only supports <see cref="F:ArcGIS.Core.Data.DatasetType.Table" /> and
    /// <see cref="F:ArcGIS.Core.Data.DatasetType.FeatureClass" /> in a read-only manner.
    /// </remarks>
    public string GetDatasetDescription(DatasetType datasetType) => datasetType.ToString();

    /// <summary>
    /// Gets a string description for the plug-in data source depending on <paramref name="inPluralForm" />.
    /// </summary>
    /// <param name="inPluralForm">
    /// A value indicating whether the description should be in singular or plural form.
    /// </param>
    /// <returns>
    /// A name description for the plug-in data source.  The default is <i>Plugin Datasources</i> if <paramref name="inPluralForm" />
    /// is <b>true</b>, <i>Plugin Datasource</i> if <b>false</b>.
    /// </returns>
    public string GetDatasourceDescription(bool inPluralForm) => !inPluralForm ? "Plugin Datasource" : "Plugin Datasources";

    /// <summary>
    /// Gets a value indicating whether the data source associated with <paramref name="connectionPath" /> can be opened
    /// by this concrete implementation.
    /// </summary>
    /// <param name="connectionPath">
    /// The connection path to the actual data source.
    /// </param>
    /// <returns>
    /// <b>true</b> if the data source associated with <paramref name="connectionPath" /> can be opened; otherwise, <b>false</b>.
    /// The default is <b>false</b>.
    /// </returns>
    /// <remarks>
    /// From time to time, some subsystems in ArcGIS Pro may need to query this method to determine whether a path to a data source
    /// can be opened by a data store, plugin or otherwise.  For example, the geoprocessing subsystem may call this method as part
    /// of its "post tool execution".  If this method returns true, geoprocessing may proceed to instantiate this plug-in data
    /// source in order to perform some system-specific tasks.
    /// </remarks>
    public bool CanOpen(Uri connectionPath) => false;
  }
