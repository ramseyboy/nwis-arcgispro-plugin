using System.ComponentModel;
using NetTopologySuite.Geometries;

namespace WaterData.ArcGis.Abstractions.DataSource;

/// <summary>
/// Represents the spatial component of a filter used when querying a <see cref="T:ArcGIS.Core.Data.FeatureClass" />.
/// </summary>
public sealed class SpatialQueryFilter : QueryFilter
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    private Geometry _filterGeometry;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private SpatialRelationship _spatialRelationship;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private SearchOrder _searchOrder;
    [EditorBrowsable(EditorBrowsableState.Never)]
    private string _spatialRelationshipDescription = string.Empty;

    /// <summary>
    /// Gets and sets the geometry to use for the spatial filter.
    /// </summary>
    /// <exception caption="" cref="T:ArcGIS.Core.Geometry.Exceptions.GeometryException">
    /// This feature's type of shape (i.e., geometry) is not supported.
    /// </exception>
    public Geometry FilterGeometry
    {
        get => this._filterGeometry;
        set => this._filterGeometry = value;
    }

    /// <summary>
    /// Gets and sets the SpatialRelationship to use for the spatial filter.
    /// </summary>
    public SpatialRelationship SpatialRelationship
    {
        get => this._spatialRelationship;
        set => this._spatialRelationship = value;
    }

    /// <summary>
    /// Gets and sets the SearchOrder to be used by the query.
    /// </summary>
    public SearchOrder SearchOrder
    {
        get => this._searchOrder;
        set => this._searchOrder = value;
    }

    /// <summary>
    /// The DE-9IM matrix relation encoded as a string.
    /// This property is only applicable if the SpatialRelationship is <see cref="F:ArcGIS.Core.Data.SpatialRelationship.Relation" />.
    /// </summary>
    public string SpatialRelationshipDescription
    {
        get => this._spatialRelationshipDescription;
        set => this._spatialRelationshipDescription = value;
    }
}
