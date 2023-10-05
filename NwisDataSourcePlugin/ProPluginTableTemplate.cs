using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using ArcGIS.Core.Internal.Geometry;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.Strtree;
using NwisApiClient;
using NwisApiClient.Models;
using NwisDataSourcePlugin.Attributes;
using NwisDataSourcePlugin.Extensions;
using NwisDataSourcePlugin.Models;
using Coordinate = ArcGIS.Core.Internal.Geometry.Coordinate;
using Envelope = NetTopologySuite.Geometries.Envelope;
using Geometry = ArcGIS.Core.Geometry.Geometry;

namespace NwisDataSourcePlugin
{
    internal interface IPluginRowProvider
    {
        PluginRow FindRow(int oid);
    }

    public class ProPluginTableTemplate : PluginTableTemplate, IPluginRowProvider
    {

        private readonly NwisModels _modelName;
        private readonly IList<SitePluginModel> _list;
        private readonly SortedDictionary<int, SitePluginModel> _bTree;
        private readonly STRtree<SitePluginModel> _rTree;

        public ProPluginTableTemplate(NwisModels modelName)
        {
            _modelName = modelName;
            var nwisApi = NwisApi.Create();
            var apiData = Task.Run( async () => await nwisApi.GetSites("tx")).Result;
            _list = apiData.Select((x, i) => new SitePluginModel
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
                    ? new Coordinate2D(x.Longitude.Value, x.Latitude.Value).ToMapPoint(SpatialReferenceBuilder.CreateSpatialReference(6318))
                    : null
            }).ToList();

            _bTree = new SortedDictionary<int, SitePluginModel>(_list.ToDictionary(x => x.ObjectId, x => x));

            _rTree = new STRtree<SitePluginModel>(_list.Count);
            _list
                .Where(x => x.Shape is not null)
                .Select(x => (site: x, envelope: new Envelope(new NetTopologySuite.Geometries.Coordinate(x.Longitude.Value, x.Latitude.Value))))
                .ToList()
                .ForEach(t => _rTree.Insert(t.envelope, t.site));
        }

        public override bool IsNativeRowCountSupported() => true;

        public override long GetNativeRowCount() => _bTree.Count;

        public override ArcGIS.Core.Geometry.Envelope GetExtent()
        {
            var topoEnvelope = _rTree.Root.Bounds;
            var minCoord = new Coordinate2D(topoEnvelope.MinX, topoEnvelope.MinY);
            var maxCoord = new Coordinate2D(topoEnvelope.MaxX, topoEnvelope.MaxY);
            return EnvelopeBuilderEx.CreateEnvelope(minCoord, maxCoord, SpatialReferenceBuilder.CreateSpatialReference(6318));
        }

        public override IReadOnlyList<PluginField> GetFields()
        {
            return _modelName switch
            {
                NwisModels.Sites => typeof(SitePluginModel).ToPluginFields(),
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

        public PluginRow FindRow(int oid)
        {
            var obj = _bTree[oid];
            var values = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Select(fi => fi.GetValue(obj))
                .ToList();
            return new PluginRow(values);
        }
    }
}
