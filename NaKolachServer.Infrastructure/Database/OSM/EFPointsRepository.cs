using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Points;

using NetTopologySuite.Geometries;

namespace NaKolachServer.Infrastructure.Database.OSM;

public class EFPointsRepository(OSMDatabaseContext databaseContext) : IPointsRepository
{
    public async Task<MapPoint[]> GetPoints(PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var centerLocation = new Point(searchParams.Latidude, searchParams.Longtidude) { SRID = 3857 };
        var pointsQueryable = databaseContext.PlanetOsmPoint
            .Where(p => p.Way.IsWithinDistance(centerLocation, searchParams.Radius))
            .AsQueryable();

        if (searchParams.Types.Length != 0)
        {
            var typesAsStrings = searchParams.Types.Select(a => a.ToString());
            pointsQueryable = pointsQueryable.Where(p => typesAsStrings.Contains(p.Amenity));
        }
        else
        {
            string[] types = ["bar", "cafe", "restaurant"];
            pointsQueryable = pointsQueryable.Where(p => types.Contains(p.Amenity));
        }

        var points = await pointsQueryable
            .OrderBy(p => p.OsmId)
            .ToArrayAsync(cancellationToken);

        return [.. points.Select(p => new MapPoint(Id: p.OsmId, Name: p.Name, Amenity: p.Amenity, Latidude: p.Way.Coordinate.X, Longtidude: p.Way.Coordinate.Y))];
    }
}