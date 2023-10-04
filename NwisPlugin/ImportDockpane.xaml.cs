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
        public ImportDockpaneView()
        {
            InitializeComponent();
        }

        public async void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            await QueuedTask.Run(() =>
            {
                using (var plugin = new PluginDatastore(
                           new PluginDatasourceConnectionPath("NwisDataSourcePlugin_Datasource", new Uri("https://waterservices.usgs.gov/nwis"))))
                {
                    System.Diagnostics.Debug.Write("==========================\r\n");
                    foreach (var table_name in plugin.GetTableNames())
                    {
                        System.Diagnostics.Debug.Write($"Table: {table_name}\r\n");
                        //open each table....use the returned table name
                        //or just pass in the name of a csv file in the workspace folder
                        using (var table = plugin.OpenTable(table_name))
                        {
                            StandaloneTableFactory.Instance.CreateStandaloneTable(new StandaloneTableCreationParams(table), MapView.Active.Map);
                            //Add as a layer to the active map or scene
                            //LayerFactory.Instance.CreateLayer<FeatureLayer>(new FeatureLayerCreationParams((FeatureClass)table), MapView.Active.Map);
                        }
                    }
                }
            });
        }


    }
}
