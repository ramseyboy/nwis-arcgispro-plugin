using NetTopologySuite.Geometries;
using WaterData.Request;

namespace WaterData.ArcGis.Abstractions;

public interface IMapSession
{
    public Envelope CurrentMapBounds();

    public void Render<T>(IWaterDataHttpRequest<T> request);
}
