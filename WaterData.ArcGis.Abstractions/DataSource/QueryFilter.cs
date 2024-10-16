using System.ComponentModel;
using NetTopologySuite.Geometries;

namespace WaterData.ArcGis.Abstractions.DataSource;

  /// <summary>
  /// Represents a filter for performing a query against a <see cref="T:ArcGIS.Core.Data.Table" />.
  /// </summary>
  public class QueryFilter
  {
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _whereClause = string.Empty;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _prefixClause = string.Empty;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _postfixClause = string.Empty;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _subFields = "*";
    [EditorBrowsable(EditorBrowsableState.Never)]
    private Geometry _spatialReference;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private IReadOnlyList<long> _objectIDs = (IReadOnlyList<long>) new List<long>(0);

    /// <summary>
    /// Gets or sets the where clause used to filter the rows returned.
    /// </summary>
    /// <remarks>
    /// <see cref="T:ArcGIS.Core.Data.Row" />s for which the where clause evaluates to True are returned by the filter.
    /// Those rows which evaluate to False or Unknown (Null) are not returned.
    /// </remarks>
    public string WhereClause
    {
      get => this._whereClause;
      set => this._whereClause = value != null ? value.Trim() : string.Empty;
    }

    /// <summary>Gets or sets the prefix clause used by the filter.</summary>
    /// <remarks>
    /// A clause that will be inserted between the SELECT keyword and the SELECT COLUMN LIST (subfields). Most commonly used for clauses like
    /// DISTINCT or ALL, e.g., SELECT <see cref="P:ArcGIS.Core.Data.QueryFilter.PrefixClause" /> <see cref="P:ArcGIS.Core.Data.QueryFilter.SubFields" />
    /// WHERE <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> <see cref="P:ArcGIS.Core.Data.QueryFilter.PostfixClause" />.
    /// </remarks>
    public string PrefixClause
    {
      get => this._prefixClause;
      set => this._prefixClause = value != null ? value.Trim() : string.Empty;
    }

    /// <summary>Gets or sets the postfix clause used by the filter.</summary>
    /// <remarks>
    /// A clause that will be appended to the SELECT statement, following the where clause. Most commonly used for clauses like ORDER BY.
    /// e.g., SELECT <see cref="P:ArcGIS.Core.Data.QueryFilter.PrefixClause" /> <see cref="P:ArcGIS.Core.Data.QueryFilter.SubFields" />
    /// WHERE <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> <see cref="P:ArcGIS.Core.Data.QueryFilter.PostfixClause" />.
    /// </remarks>
    public string PostfixClause
    {
      get => this._postfixClause;
      set => this._postfixClause = value != null ? value.Trim() : string.Empty;
    }

    /// <summary>
    /// Gets or sets a comma (,) delimited string containing the names of fields for which values should be returned by the query.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The subfield property requests a minimum set of fields to return values for, by restricting the set of fields for which values are returned
    /// you can optimize the performance of the query when using a query filter. Additional values may be returned for fields that the query requires
    /// (e.g., ObjectID or Shape fields).  Fields not included in the subfields list or required by the query are still present in the rows returned,
    /// but are not populated with values. Including all of the fields in the row ensures that the field index position is constant no matter how it was
    /// hydrated.
    /// </para>
    /// <para>
    /// The default setting for subfields is to request values to be returned for all fields. The strings of "*" or "" can be set to return the query
    /// to this default. The default of returning all fields should always be used if the intent of the operation is to alter the values of the row
    /// and store it.
    /// </para>
    /// <para>
    /// To set the subfields property to request the values to be returned for the "Name" and "Age" fields a string of "Name, Age" should be provided
    /// (white space is optional).
    /// </para>
    /// <para>
    /// It is not required to set the subfields property if the query filter is to be used in a context where no rows are returned (e.g.,
    /// <see cref="M:ArcGIS.Core.Data.Table.Select(ArcGIS.Core.Data.QueryFilter,ArcGIS.Core.Data.SelectionType,ArcGIS.Core.Data.SelectionOption)" />).
    /// </para>
    /// </remarks>
    public string SubFields
    {
      get => this._subFields;
      set => this._subFields = string.IsNullOrEmpty(value) ? "*" : value.Trim();
    }

    /// <summary>
    /// Gets or sets the spatial reference in which the features will be returned.
    /// </summary>
    public Geometry OutputSpatialReference
    {
      get => this._spatialReference;
      set => this._spatialReference = value;
    }

    /// <summary>
    /// Gets or sets the list of objectIDs used for filtering data in the underlying data store.
    /// </summary>
    /// <remarks>
    /// If <c>ObjectIDs</c> inputs are used in conjunction with <see cref="P:ArcGIS.Core.Data.QueryFilter.WhereClause" /> and/or
    /// <see cref="P:ArcGIS.Core.Data.SpatialQueryFilter.FilterGeometry" />, the resulting rows/features will be those whose objectIDs
    /// <b>intersect</b> with the inputs in <c>ObjectIDs</c> and objectsIDs corresponding to the input <c>WhereClause</c> and/or
    /// <c>FilterGeometry</c>.
    /// </remarks>
    /// <example>
    /// <code>
    /// QueryFilter queryFilter = new QueryFilter()
    /// {
    ///   ObjectIDs = new List&lt;long&gt;() { 1, 2, 3, 5, 6, 8 },
    ///   WhereClause = "OWNER_NAME = 'John Smith'" // The OID of 'John Smith' is 6.
    /// };
    ///
    /// using (RowCursor rowCursor = table.Search(queryFilter, false))
    /// {
    ///   List&lt;Row&gt; actualRows = new List&lt;Row&gt;();
    ///
    ///   try
    ///   {
    ///      while (rowCursor.MoveNext())
    ///      {
    ///        actualRows.Add(rowCursor.Current);
    ///      }
    ///
    ///      Assert.AreEqual(1, actualRows.Count, "There is only 1 OID (6) in ObjectsIDs that intersects with the result from the where clause.");
    ///      Assert.AreEqual(6, actualRows[0].GetObjectID(), "The intersecting OID corresponding to 'John Smith' should be 6.");
    ///   }
    ///   finally
    ///   {
    ///     foreach (Row row in actualRows)
    ///       row.Dispose();
    ///   }
    /// }
    /// </code>
    /// </example>
    public IReadOnlyList<long> ObjectIDs
    {
      get => this._objectIDs;
      set => this._objectIDs = value == null ? (IReadOnlyList<long>) new List<long>(0).AsReadOnly() : value;
    }
  }
