using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using WaterData.Request;
using Envelope = NetTopologySuite.Geometries.Envelope;

namespace WaterData.ArcGis.Abstractions.Esri;

public class EsriMapSession: IMapSession
{
    /// <inheritdoc />
    public Envelope CurrentMapBounds()
    {
        var mapView = MapView.Active;
        if (mapView == null)
        {
            throw new InvalidOperationException("No map extent loaded yet");
        }

        var extent = mapView.Extent;
        var projectedEnvelope = GeometryEngine.Instance.Project(extent, SpatialReferences.WGS84).Extent;

        return new Envelope(projectedEnvelope.XMin,
            projectedEnvelope.XMax, projectedEnvelope.YMin, projectedEnvelope.YMax);
    }

    /// <inheritdoc />
    public void Render<T>(IWaterDataHttpRequest<T> request)
    {
        var uri = request.Uri;
        using var plugin = new PluginDatastore(
            new PluginDatasourceConnectionPath("WaterData.ArcGis.Plugin.DataSource_Datasource", uri));

        foreach (var table_name in plugin.GetTableNames())
        {
            using var table = plugin.OpenTable(table_name);
            //StandaloneTableFactory.Instance.CreateStandaloneTable(new StandaloneTableCreationParams(table), MapView.Active.Map);
            LayerFactory.Instance.CreateLayer<FeatureLayer>(
                new FeatureLayerCreationParams(table as FeatureClass), MapView.Active.Map);
        }
    }
}
