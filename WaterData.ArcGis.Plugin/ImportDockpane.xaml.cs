using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;
using WaterData.Request;
using Envelope = NetTopologySuite.Geometries.Envelope;

namespace WaterData.ArcGis.Plugin;

/// <summary>
///     Interaction logic for ImportDockpaneView.xaml
/// </summary>
public partial class ImportDockpaneView : UserControl
{
    public ImportDockpaneView()
    {
        InitializeComponent();
    }
}
