using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using NwisApiClient;
using NwisApiClient.Models;
using NwisDataSourcePlugin.Attributes;
using NwisDataSourcePlugin.Extensions;
using NwisDataSourcePlugin.Models;

namespace NwisDataSourcePlugin
{
    internal interface IPluginRowProvider
    {
        PluginRow FindRow(long oid);
    }

    public class ProPluginTableTemplate : PluginTableTemplate, IPluginRowProvider
    {

        private readonly NwisModels _modelName;
        private readonly IEnumerable<Site> _data;

        public ProPluginTableTemplate(NwisModels modelName)
        {
            _modelName = modelName;
            var nwisApi = NwisApi.Create();
            _data = Task.Run( async () => await nwisApi.GetSites("tx")).Result;
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
            var ids = _data.Select(x => x.SiteNumber!.Value).ToList();
            return new ProPluginCursorTemplate(this, ids);
        }

        public override PluginCursorTemplate Search(SpatialQueryFilter spatialQueryFilter)
        {
            var ids = _data.Select(x => x.SiteNumber!.Value).ToList();
            return new ProPluginCursorTemplate(this, ids);
        }

        public override GeometryType GetShapeType()
        {
            return GeometryType.Point;
        }

        public PluginRow FindRow(long oid)
        {
            var obj = _data.FirstOrDefault(x => x.SiteNumber == oid);
            var values = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Select(fi => fi.GetValue(obj))
                .ToList();
            return new PluginRow(values);
        }
    }
}
