using Microsoft.EntityFrameworkCore;

using NaKolachServer.Domain.Points;

namespace NaKolachServer.Infrastructure.Database.OSM;

public class EFPointsRepository(OSMDatabaseContext databaseContext) : IPointsRepository
{
    public async Task<Point[]> GetPoints(PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var centerLocation = new NetTopologySuite.Geometries.Point(searchParams.Latitude, searchParams.Longitude) { SRID = 3857 };
        var pointsQueryable = databaseContext.PlanetOsmPoint
            .Where(p => p.Way.IsWithinDistance(centerLocation, searchParams.Radius))
            .AsQueryable();

        if (searchParams.Categories.Length != 0)
            pointsQueryable = pointsQueryable
            .Where(p =>
                searchParams.Categories.Contains(p.Amenity)
                || searchParams.Categories.Contains(p.Shop)
                || searchParams.Categories.Contains(p.Tourism)
                || searchParams.Categories.Contains(p.Leisure)
            );

        return await pointsQueryable
            .OrderBy(p => p.OsmId)
            .Select(p => new Point(p.OsmId, p.Name, FindCategory(searchParams.Categories, p.Amenity, p.Shop, p.Tourism, p.Leisure), p.Way.Coordinate.X, p.Way.Coordinate.Y))
            .ToArrayAsync(cancellationToken);
    }

    // todo split to two methods: GetTotalCount, GetById
    public async Task<Point?> GetRandomPointByCategory(
        string category,
        double latitude,
        double longitude,
        int radius,
        CancellationToken cancellationToken
    )
    {
        var centerLocation = new NetTopologySuite.Geometries.Point(latitude, longitude) { SRID = 3857 };

        var totalPoints = await databaseContext.PlanetOsmPoint
            .Where(p => p.Way.IsWithinDistance(centerLocation, radius) &&
            p.Amenity == category
            || p.Shop == category
            || p.Tourism == category
            || p.Leisure == category
        )
        .CountAsync(cancellationToken);

        var randomIndex = Random.Shared.Next(0, totalPoints);
        return await databaseContext.PlanetOsmPoint
            .Skip(randomIndex)
            .Select(p => new Point(p.OsmId, p.Name, FindCategory(new[] { category }, p.Amenity, p.Shop, p.Tourism, p.Leisure), p.Way.Coordinate.X, p.Way.Coordinate.Y))
            .FirstOrDefaultAsync(cancellationToken);
    }

    private static string FindCategory(string[] categories, string? amenity, string? shop, string? tourism, string? leisure)
    {
        if (categories.Contains(amenity)) return amenity!;
        else if (categories.Contains(shop)) return shop!;
        else if (categories.Contains(tourism)) return tourism!;

        return leisure!;
    }
}