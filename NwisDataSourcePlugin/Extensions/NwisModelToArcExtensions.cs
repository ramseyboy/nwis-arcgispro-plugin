using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using CsvHelper.Configuration.Attributes;
using NwisApiClient.Models;
using NwisDataSourcePlugin.Attributes;
using NwisDataSourcePlugin.Models;

namespace NwisDataSourcePlugin.Extensions;

public static class NwisModelToArcExtensions
{
    public static IReadOnlyList<PluginField> ToPluginFields(this Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            .Where(pi => Attribute.IsDefined(pi, typeof(ArcGisPluginFieldAttribute)))
            .Select(pi => (pi.GetCustomAttribute(typeof(ArcGisPluginFieldAttribute)) as ArcGisPluginFieldAttribute).ToPluginField(pi))
            .ToArray();
    }

    private static PluginField ToPluginField(this ArcGisPluginFieldAttribute attr, MemberInfo propertyInfo)
    {
        return new PluginField(propertyInfo.Name, attr.Alias, attr.FieldType);
    }
}