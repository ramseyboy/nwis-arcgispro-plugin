using System.Windows;
using System.Windows.Controls;
using WaterData.ArcGis.Abstractions;
using WaterData.Nwis;
using WaterData.Nwis.Models.Codes;
using WaterData.Request;
using Envelope = NetTopologySuite.Geometries.Envelope;

namespace WaterData.ArcGis.Ui;

/// <summary>
///     Interaction logic for ImportDockpaneView.xaml
/// </summary>
public partial class SiteControl : UserControl
{
    private ComboBoxViewModel? _selectedCounty;
    private ComboBoxViewModel? _selectedHuc;
    private ComboBoxViewModel? _selectedState;

    private const string Heading = "Import NWIS Datasets";

    private readonly IMapSessionExecutor _mapSession;

    public SiteControl(IMapSessionExecutor mapSession)
    {
        _mapSession = mapSession;
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
        await _mapSession.Queue(session =>
        {
            var boundingBox = session.CurrentMapBounds();

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
            session.Render(request);
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
