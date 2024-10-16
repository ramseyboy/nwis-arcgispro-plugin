namespace WaterData.ArcGis.Abstractions.DataSource;

  /// <summary>Specifies the spatial relationship.</summary>
  /// <remarks>
  /// 	<list type="bullet">
  /// 		<item>Intersects - Returns a feature if any spatial relationship is found.</item>
  /// 		<item>EnvelopeIntersects - Returns a feature if the envelope of the two shapes intersect.</item>
  /// 		<item>Contains - Returns a feature if its shape is wholly contained within the search geometry. Valid of all shape type combinations.</item>
  /// 		<item>Crosses - Returns a feature if the intersection of the interiors of the two shapes is not empty and has a lower dimension that the maximum dimension of
  ///     the two shapes. Two lines that share an endpoint do not cross. Valid for polyline/polyline, polyline/Area, multipoint/Area, and multipoint/polyline shape type
  ///     combinations.</item>
  /// 		<item>IndexIntersects uses the underlying index grid of the target feature class which is faster than using the envelope of the features, and is often used
  ///     to return features for display purposes.</item>
  /// 		<item>Overlaps - Returns a feature if the intersection of the two shapes results in an object of the same dimension, but different from both of the shapes.
  ///     Applies to Area/Area, polyline/polyline, and multipoint/multipoint shape type combinations.</item>
  /// 		<item>Touches - Returns a feature if the two shapes share a common boundary. However, the intersection of the interiors of the two shapes must be empty. In
  ///     the point/polyline case, the point may touch an endpoint only of the polyline. Applies to all combinations except for point/point.</item>
  /// 		<item>Within - Returns a feature if its shape wholly contains the search geometry. Valid for all shape type combinations.</item>
  /// 	</list>
  /// </remarks>
  public enum SpatialRelationship
  {
    /// <summary>No defined spatial relationship.</summary>
    Undefined,
    /// <summary>
    /// The filter geometry intersects a feature from the target feature class.
    /// </summary>
    Intersects,
    /// <summary>
    /// The envelope of the filter geometry intersects with the envelope of a feature in the target feature class.
    /// </summary>
    EnvelopeIntersects,
    /// <summary>
    /// The envelope of the filter geometry intersects with the index entry for a feature in the target feature class.
    /// </summary>
    IndexIntersects,
    /// <summary>
    /// The filter geometry touches a feature in the target feature class.
    /// </summary>
    Touches,
    /// <summary>
    /// The filter geometry overlaps a feature in the target feature class.
    /// </summary>
    Overlaps,
    /// <summary>
    /// The filter geometry crosses a feature from the target feature class.
    /// </summary>
    Crosses,
    /// <summary>
    /// The filter geometry is within a feature in the target feature class.
    /// </summary>
    Within,
    /// <summary>
    /// The filter geometry wholly contains within it a feature from the target feature class.
    /// </summary>
    Contains,
    /// <summary>
    /// The filter geometry is involved in an interior-boundary-exterior relationship with a feature from the target feature class.
    /// </summary>
    Relation,
  }
