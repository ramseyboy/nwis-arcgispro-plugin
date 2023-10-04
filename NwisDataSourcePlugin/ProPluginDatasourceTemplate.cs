using System;
using System.Collections.Generic;
using ArcGIS.Core.Data.PluginDatastore;

namespace NwisDataSourcePlugin
{
    public class ProPluginDatasourceTemplate : PluginDatasourceTemplate
    {

        public override void Open(Uri connectionPath)
        {
            // nothing to open, stateless
        }

        public override void Close()
        {
            // nothing to close, stateless
        }

        public override PluginTableTemplate OpenTable(string name)
        {
            var modelName = Enum.Parse<NwisModels>(name);
            return new ProPluginTableTemplate(modelName);
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
}
