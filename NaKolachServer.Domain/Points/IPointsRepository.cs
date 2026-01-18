namespace NaKolachServer.Domain.Points;

public interface IPointsRepository
{
    public Task<Point?> GetPointById(long id, CancellationToken cancellationToken);
    public Task<Point[]> GetPoints(PointsSearchParams searchParams, CancellationToken cancellationToken);
    public Task<Point?> GetRandomPointByCategory(string category, double latitude, double longitude, int radius, CancellationToken cancellationToken);
}