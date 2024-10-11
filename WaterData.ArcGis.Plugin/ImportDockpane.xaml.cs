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
    private ComboBoxViewModel? _selectedCounty;
    private ComboBoxViewModel? _selectedHuc;
    private ComboBoxViewModel? _selectedState;

    public ImportDockpaneView()
    {
        InitializeComponent();

        StateSelect.ItemsSource = BuildComboBoxViewModels(NwisRequestBuilder
            .Builder()
            .StateCodes()
            .BuildRequest());

        CountySelect.ItemsSource = BuildComboBoxViewModels(NwisRequestBuilder
            .Builder()
            .CountyCodes()
            .BuildRequest());

        HucSelect.ItemsSource = BuildComboBoxViewModels(NwisRequestBuilder
            .Builder()
            .HydrologicUnitCodes()
            .BuildRequest());
    }

    private List<ComboBoxViewModel> BuildComboBoxViewModels<T>(IWaterDataEnumerableRequest<T> request) where T : NwisCode
    {
        var data = request.GetAsync().Result;

        var codes = data
            .Select(s => new ComboBoxViewModel(s))
            .ToList();
        return codes;
    }

    private void StateSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selectedState = StateSelect.SelectedItem as ComboBoxViewModel;
    }

    private void CountySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selectedCounty = CountySelect.SelectedItem as ComboBoxViewModel;
    }

    private void HucSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _selectedHuc = HucSelect.SelectedItem as ComboBoxViewModel;
    }

    public async void ImportButton_Click(object sender, RoutedEventArgs e)
    {
        await QueuedTask.Run(() =>
        {
            var mapView = MapView.Active;
            if (mapView == null)
            {
                throw new InvalidOperationException("No map extent loaded yet");
            }

            var extent = mapView.Extent;
            var projectedEnvelope = GeometryEngine.Instance.Project(extent, SpatialReferences.WGS84).Extent;

            var boundingBox = new Envelope(projectedEnvelope.XMin,
                projectedEnvelope.XMax, projectedEnvelope.YMin, projectedEnvelope.YMax);

            var builder = NwisRequestBuilder
                .Builder()
                .Sites();

            if (_selectedState?.Code is not null)
            {
                builder.StateCode(_selectedState.Code);
            }

            if (_selectedCounty?.Code is not null)
            {
                builder.CountyCode(_selectedCounty.Code);
            }

            if (_selectedHuc?.Code is not null)
            {
                builder.HydrologicUnitCode(_selectedHuc.Code);
            }

            builder.BoundingBox(boundingBox);

            var request = builder.BuildRequest();

            var uri = request.Uri;
            using var plugin = new PluginDatastore(
                new PluginDatasourceConnectionPath("WaterData.ArcGis.Plugin.DataSource_Datasource", uri));

            foreach (var table_name in plugin.GetTableNames())
            {
                using var table = plugin.OpenTable(table_name);
                //StandaloneTableFactory.Instance.CreateStandaloneTable(new StandaloneTableCreationParams(table), MapView.Active.Map);
                LayerFactory.Instance.CreateLayer<FeatureLayer>(
                    new FeatureLayerCreationParams(table as FeatureClass), MapView.Active.Map);
            }
        });
    }

    private class ComboBoxViewModel
    {
        public ComboBoxViewModel(NwisCode code)
        {
            Code = code;
        }

        public NwisCode Code { get; }

        public override string ToString()
        {
            return $"{Code.Label} - {Code.Code}";
        }
    }
}
