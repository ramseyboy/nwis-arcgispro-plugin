using ArcGIS.Desktop.Framework.Contracts;

namespace WaterData.ArcGis.Plugin;

/// <summary>
///     Button implementation to show the DockPane.
/// </summary>
internal class MainRibbonButtonPlugin : Button
{
    protected override void OnClick()
    {
        MainDockPanePlugin.Show();
    }
}
