using System;
using ArcGIS.Core.Data;

namespace NwisDataSourcePlugin.Attributes;

public class ArcGisPluginFieldAttribute: Attribute
{
    public FieldType FieldType { get; private set; }
    public string Alias { get; private set; }

    public ArcGisPluginFieldAttribute(string alias, FieldType fieldType)
    {
        Alias = alias;
        FieldType = fieldType;
    }
}