using ArcGIS.Desktop.Framework.Threading.Tasks;

namespace WaterData.ArcGis.Abstractions.Esri;

public class EsriMapSessionExecutor: IMapSessionExecutor
{
    /// <inheritdoc />
    public async Task Queue(Action<IMapSession> job)
    {
        await QueuedTask.Run(() =>
        {
            var session = new EsriMapSession();
            job(session);
        });
    }
}
