namespace NaKolachServer.Domain.Points;

public interface IPointsRepository
{
    public Task<MapPoint[]> GetPoints(PointsSearchParams searchParams, CancellationToken cancellationToken);
}