using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using WaterData.Models.Codes;
using WaterData.Request;


namespace NwisPlugin
{
    /// <summary>
    /// Interaction logic for ImportDockpaneView.xaml
    /// </summary>
    public partial class ImportDockpaneView : UserControl
    {
        private ComboBoxViewModel _selectedState;
        private ComboBoxViewModel _selectedCounty;
        private ComboBoxViewModel _selectedHuc;

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

        private List<ComboBoxViewModel> BuildComboBoxViewModels<T>(IWaterDataRequest<T> request) where T: NwisCode
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
                var request = NwisRequestBuilder
                    .Builder()
                    .Sites()
                    .CountyCode(_selectedCounty.Code)
                    .BuildRequest();

                var uri = request.Uri;
                using (var plugin = new PluginDatastore(
                           new PluginDatasourceConnectionPath("NwisDataSourcePlugin_Datasource", uri)))
                {
                    System.Diagnostics.Debug.Write("==========================\r\n");
                    foreach (var table_name in plugin.GetTableNames())
                    {
                        System.Diagnostics.Debug.Write($"Table: {table_name}\r\n");
                        //open each table....use the returned table name
                        //or just pass in the name of a csv file in the workspace folder
                        using (var table = plugin.OpenTable(table_name))
                        {
                            //StandaloneTableFactory.Instance.CreateStandaloneTable(new StandaloneTableCreationParams(table), MapView.Active.Map);
                            //Add as a layer to the active map or scene
                            LayerFactory.Instance.CreateLayer<FeatureLayer>(new FeatureLayerCreationParams(table as FeatureClass), MapView.Active.Map);
                        }
                    }
                }
            });
        }

        class ComboBoxViewModel
        {
            public NwisCode Code { get; }

            public ComboBoxViewModel(NwisCode code)
            {
                Code = code;
            }

            public override string ToString()
            {
                return $"{Code.Label} - {Code.Code}";
            }
        }
    }
}
