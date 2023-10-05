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


namespace NwisPlugin
{
    /// <summary>
    /// Interaction logic for ImportDockpaneView.xaml
    /// </summary>
    public partial class ImportDockpaneView : UserControl
    {
        private StateComboBoxItem selectedState;

        public ImportDockpaneView()
        {
            InitializeComponent();
            var states = new List<StateComboBoxItem>(50)
            {
                new("AL") {IsSelected = true},
                new("AK"),
                new("AZ"),
                new("AR"),
                new("CA"),
                new("CO"),
                new("CT"),
                new("DE"),
                new("DC"),
                new("FL"),
                new("GA"),
                new("HI"),
                new("ID"),
                new("IL"),
                new("IN"),
                new("IA"),
                new("KS"),
                new("KY"),
                new("LA"),
                new("ME"),
                new("MD"),
                new("MA"),
                new("MI"),
                new("MN"),
                new("MS"),
                new("MO"),
                new("MT"),
                new("NE"),
                new("NV"),
                new("NH"),
                new("NJ"),
                new("NM"),
                new("NY"),
                new("NC"),
                new("ND"),
                new("OH"),
                new("OK"),
                new("OR"),
                new("PA"),
                new("RI"),
                new("SC"),
                new("SD"),
                new("TN"),
                new("TX"),
                new("UT"),
                new("VT"),
                new("VA"),
                new("WA"),
                new("WV"),
                new("WI"),
                new("WY")
            };
            stateComboBox.ItemsSource = states;
        }

        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedState = stateComboBox.SelectedItem as StateComboBoxItem;
        }

        public async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            await QueuedTask.Run(() =>
            {
                var builder = new UriBuilder("http://nwis/site")
                {
                    Query = $"stateCd={selectedState.Label}"
                };
                var uri = builder.Uri;
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

        class StateComboBoxItem
        {
            public string Label { get; set; }
            public bool IsSelected { get; set; }

            public StateComboBoxItem(string label)
            {
                Label = label;
            }
        }
    }
}
