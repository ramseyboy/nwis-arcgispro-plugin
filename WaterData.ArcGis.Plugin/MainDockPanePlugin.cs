using System.Windows.Controls;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace WaterData.ArcGis.Plugin;

internal class MainDockPanePlugin : DockPane
{
    private const string _dockPaneID = "WaterData_ImportDockpane";
    private const string _menuID = "WaterData_ImportDockpane_Menu";

    /// <summary>
    ///     Text shown near the top of the DockPane.
    /// </summary>
    private string _heading = "Import NWIS Datasets";

    protected MainDockPanePlugin()
    {
    }

    public string Heading
    {
        get => _heading;
        set => SetProperty(ref _heading, value);
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

    #region Burger Button

    /// <summary>
    ///     Tooltip shown when hovering over the burger button.
    /// </summary>
    public string BurgerButtonTooltip => "Options";

    /// <summary>
    ///     Menu shown when burger button is clicked.
    /// </summary>
    public ContextMenu BurgerButtonMenu => FrameworkApplication.CreateContextMenu(_menuID);

    #endregion
}
