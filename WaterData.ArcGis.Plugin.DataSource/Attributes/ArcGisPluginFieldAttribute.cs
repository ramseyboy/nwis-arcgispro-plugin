using System;
using ArcGIS.Core.Data;

namespace WaterData.ArcGis.Plugin.DataSource.Attributes;

public class ArcGisPluginFieldAttribute : Attribute
{
    public ArcGisPluginFieldAttribute(string alias, FieldType fieldType)
    {
        Alias = alias;
        FieldType = fieldType;
    }

    public FieldType FieldType { get; private set; }
    public string Alias { get; private set; }
}