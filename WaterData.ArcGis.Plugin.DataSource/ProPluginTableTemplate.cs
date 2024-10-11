using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.Strtree;
using WaterData.ArcGis.Plugin.DataSource.Extensions;
using WaterData.ArcGis.Plugin.DataSource.Models;
using WaterData.Nwis;
using WaterData.Nwis.Models;
using Envelope = ArcGIS.Core.Geometry.Envelope;

namespace WaterData.ArcGis.Plugin.DataSource;

internal interface IPluginRowProvider
{
    PluginRow FindRow(int oid);
}

public class ProPluginTableTemplate : PluginTableTemplate, IPluginRowProvider
{
    private readonly SortedDictionary<int, NwisSitePluginModel> _bTree;
    private readonly IList<NwisSitePluginModel> _list;
    private readonly NwisModels _modelName;
    private readonly STRtree<NwisSitePluginModel> _rTree;

    public ProPluginTableTemplate(Uri parameterUri, NwisModels modelName)
    {
        _modelName = modelName;
        var apiData = Task.Run(async () =>
        {
            var request = new NwisHttpRequest<NwisSite>(parameterUri);
            return await request.GetAsync();
        }).Result;
        _list = apiData.Select((x, i) => new NwisSitePluginModel
        {
            ObjectId = i,
            AgencyCode = x.AgencyCode,
            SiteNumber = x.SiteNumber,
            SiteName = x.SiteName,
            SiteType = x.SiteType,
            Latitude = x.Latitude,
            Longitude = x.Longitude,
            LatLongAccuracy = x.LatLongAccuracy,
            LatLongDatum = x.LatLongDatum,
            Altitude = x.Altitude,
            AltitudeAccuracy = x.AltitudeAccuracy,
            AltitudeDatum = x.AltitudeDatum,
            HydrologicUnitCode = x.HydrologicUnitCode,
            Shape = x.Latitude is not null && x.Longitude is not null
                ? new Coordinate2D(x.Longitude.Value, x.Latitude.Value).ToMapPoint(
                    SpatialReferenceBuilder.CreateSpatialReference(6318))
                : null
        }).ToList();

        _bTree = new SortedDictionary<int, NwisSitePluginModel>(_list.ToDictionary(x => x.ObjectId, x => x));

        _rTree = new STRtree<NwisSitePluginModel>(_list.Count);
        _list
            .Where(x => x.Shape is not null)
            .Select(x => (site: x,
                envelope: new NetTopologySuite.Geometries.Envelope(
                    new Coordinate(x.Longitude.Value, x.Latitude.Value))))
            .ToList()
            .ForEach(t => _rTree.Insert(t.envelope, t.site));
    }

    public PluginRow FindRow(int oid)
    {
        var obj = _bTree[oid];
        var values = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            .Select(fi => fi.GetValue(obj))
            .ToList();
        return new PluginRow(values);
    }

    public override bool IsNativeRowCountSupported()
    {
        return true;
    }

    public override long GetNativeRowCount()
    {
        return _bTree.Count;
    }

    public override Envelope GetExtent()
    {
        var topoEnvelope = _rTree.Root.Bounds;
        var minCoord = new Coordinate2D(topoEnvelope.MinX, topoEnvelope.MinY);
        var maxCoord = new Coordinate2D(topoEnvelope.MaxX, topoEnvelope.MaxY);
        return EnvelopeBuilderEx.CreateEnvelope(minCoord, maxCoord,
            SpatialReferenceBuilder.CreateSpatialReference(6318));
    }

    public override IReadOnlyList<PluginField> GetFields()
    {
        return _modelName switch
        {
            NwisModels.Sites => typeof(NwisSitePluginModel).ToPluginFields(),
            _ => throw new ArgumentOutOfRangeException(nameof(_modelName))
        };
    }

    public override string GetName()
    {
        return _modelName.ToString();
    }

    public override PluginCursorTemplate Search(QueryFilter queryFilter)
    {
        var ids = _bTree.Keys.ToList();
        return new ProPluginCursorTemplate(this, ids);
    }

    public override PluginCursorTemplate Search(SpatialQueryFilter spatialQueryFilter)
    {
        var ids = _bTree.Keys.ToList();
        return new ProPluginCursorTemplate(this, ids);
    }

    /*private IList<long> Query(QueryFilter queryFilter)
    {
        var result = new List<long>();
        var emptyQuery = true;

        SpatialQueryFilter spatialQueryFilter = null;
        if (queryFilter is SpatialQueryFilter filter)
        {
            spatialQueryFilter = filter;
        }

        if (queryFilter.ObjectIDs.Count > 0)
        {
            emptyQuery = false;
            result = _bTree
                .Where(row => queryFilter.ObjectIDs.Contains(row.Key))
                .Select(row => row.Key).ToList();

            //anything selected?
            if (result.Count == 0)
            {
                //no - specifying a fidset trumps everything. The client
                //specified a fidset and nothing was selected so we are done
                return result;
            }
        }
    }*/

    public override GeometryType GetShapeType()
    {
        return GeometryType.Point;
    }
}