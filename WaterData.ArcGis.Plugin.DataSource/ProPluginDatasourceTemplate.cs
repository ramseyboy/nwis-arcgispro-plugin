using System;
using System.Collections.Generic;
using System.Linq;
using ArcGIS.Core.Data.PluginDatastore;

namespace WaterData.ArcGis.Plugin.DataSource;

public class ProPluginDatasourceTemplate : PluginDatasourceTemplate
{
    private Uri _currentUri;

    private Dictionary<Uri, PluginTableTemplate> _tables;

    public override void Open(Uri connectionPath)
    {
        _currentUri = connectionPath;
        _tables = new Dictionary<Uri, PluginTableTemplate>();
    }

    public override void Close()
    {
        _tables.Clear();
    }

    public override PluginTableTemplate OpenTable(string name)
    {
        if (!_tables.Keys.Contains(_currentUri))
        {
            var modelName = Enum.Parse<NwisModels>(name);
            _tables[_currentUri] = new ProPluginTableTemplate(_currentUri, modelName);
        }

        return _tables[_currentUri];
    }

    public override IReadOnlyList<string> GetTableNames()
    {
        return Enum.GetNames<NwisModels>();
    }

    public override bool IsQueryLanguageSupported()
    {
        return false;
    }
}
