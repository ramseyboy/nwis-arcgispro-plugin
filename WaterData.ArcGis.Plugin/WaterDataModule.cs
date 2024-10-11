using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Events;

namespace WaterData.ArcGis.Plugin;

internal class WaterDataModule : Module
{
    private static WaterDataModule? _this;

    /// <summary>
    ///     Retrieve the singleton instance to this module here
    /// </summary>
    public static WaterDataModule Current =>
        _this ??= (WaterDataModule) FrameworkApplication.FindModule("WaterDataModule");

    #region Overrides

    /// <summary>
    ///     Called by Framework when ArcGIS Pro is closing
    /// </summary>
    /// <returns>False to prevent Pro from closing, otherwise True</returns>
    protected override bool CanUnload()
    {
        //TODO - add your business logic
        //return false to ~cancel~ Application close
        return true;
    }

    /// <inheritdoc />
    protected override void OnPaneClosing(Pane pane, CancelRoutedEventArgs e)
    {
        base.OnPaneClosing(pane, e);
    }

    /// <inheritdoc />
    protected override void OnPaneClosed(Pane pane)
    {
        base.OnPaneClosed(pane);
    }

    /// <inheritdoc />
    protected override void OnPaneOpened(Pane pane)
    {
        base.OnPaneOpened(pane);
    }

    /// <inheritdoc />
    protected override void OnPaneActivated(Pane incomingPane)
    {
        base.OnPaneActivated(incomingPane);
    }

    /// <inheritdoc />
    protected override void OnPaneDeactivated(Pane outgoingPane)
    {
        base.OnPaneDeactivated(outgoingPane);
    }

    #endregion Overrides
}
