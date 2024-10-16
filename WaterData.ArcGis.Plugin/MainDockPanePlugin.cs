using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace WaterData.ArcGis.Plugin;

internal class MainDockPanePlugin : DockPane
{
    private const string _dockPaneID = "WaterData_ImportDockpane";

    protected MainDockPanePlugin()
    {
    }

    /// <summary>
    ///     Show the DockPane.
    /// </summary>
    internal static void Show()
    {
        var pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
        if (pane == null)
            return;

        pane.Activate();
    }
}
