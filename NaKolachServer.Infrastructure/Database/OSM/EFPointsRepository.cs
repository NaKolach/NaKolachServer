using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Points;

using NetTopologySuite.Geometries;

namespace NaKolachServer.Infrastructure.Database.OSM;

public class EFPointsRepository(OSMDatabaseContext databaseContext) : IPointsRepository
{
    public async Task<MapPoint[]> GetPoints(PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var centerLocation = new Point(searchParams.Latitude, searchParams.Longitude) { SRID = 3857 };
        var pointsQueryable = databaseContext.PlanetOsmPoint
            .Where(p => p.Way.IsWithinDistance(centerLocation, searchParams.Radius))
            .AsQueryable();

        if (searchParams.Categories.Length != 0)
            pointsQueryable = pointsQueryable.Where(p => searchParams.Categories.Contains(p.Amenity ?? p.Shop ?? p.Tourism ?? p.Leisure));

        var points = await pointsQueryable
            .OrderBy(p => p.OsmId)
            .ToArrayAsync(cancellationToken);

        return [.. points.Select(p => new MapPoint(Id: p.OsmId, Name: p.Name, Category: p.Amenity, Latitude: p.Way.Coordinate.X, Longitude: p.Way.Coordinate.Y))];
    }
}