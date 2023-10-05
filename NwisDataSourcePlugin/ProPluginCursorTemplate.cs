using System;
using System.Collections.Generic;
using ArcGIS.Core.Data.PluginDatastore;

namespace NwisDataSourcePlugin
{
    public class ProPluginCursorTemplate : PluginCursorTemplate
    {
        private readonly Queue<int> _oids;
        private int _current = -1;
        private readonly IPluginRowProvider _rowProvider;

        internal ProPluginCursorTemplate(IPluginRowProvider rowProvider, IEnumerable<int> oids)
        {
            _rowProvider = rowProvider;
            _oids = new Queue<int>(oids);
        }

        public override PluginRow GetCurrentRow()
        {
            return _rowProvider.FindRow(_current);
        }

        public override bool MoveNext()
        {
            if (_oids.Count == 0)
            {
                return false;
            }

            _current = _oids.Dequeue();

            return true;
        }
    }
}
