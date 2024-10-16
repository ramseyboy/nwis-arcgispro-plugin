namespace WaterData.ArcGis.Abstractions;

public interface IMapSessionExecutor
{
    Task Queue(Action<IMapSession> job);
}
