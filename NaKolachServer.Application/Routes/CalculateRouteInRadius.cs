using NaKolachServer.Application.Utils;
using NaKolachServer.Domain.Points;
using NaKolachServer.Domain.Routes;
using NaKolachServer.Domain.Users;

using Newtonsoft.Json;

namespace NaKolachServer.Application.Routes;

public class CalculateRouteInRadius(IPointsRepository pointsRepository, IRoutesRepository routesRepository, IRouteProvider routeProvider)
{
    public async Task<PathWithPoints[]> Execute(UserContext userContext, PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var startPoint = CRSConverter.CRS4326to3857(searchParams.Longitude, searchParams.Latitude);

        var pointsOfInterest = new List<Point>();
        foreach (var category in searchParams.Categories)
        {
            var pointOfInterest = await pointsRepository.GetRandomPointByCategory(
                category, startPoint.X, startPoint.Y, searchParams.Radius, cancellationToken
            );

            if (pointOfInterest is null) return [];

            var pointConverted = CRSConverter.CRS3857to4326(pointOfInterest.Longitude, pointOfInterest.Latitude);
            pointOfInterest = pointOfInterest with { Latitude = pointConverted.Y, Longitude = pointConverted.X };
            pointsOfInterest.Add(pointOfInterest);
        }

        var route = await routeProvider.CalculateRoute([
            new Coordinates(searchParams.Longitude, searchParams.Latitude),
            .. pointsOfInterest.Select(p => new Coordinates(p.Longitude, p.Latitude)),
            new Coordinates(searchParams.Longitude, searchParams.Latitude)],
         cancellationToken);

        await routesRepository.InsertRoute(new Route(
            Id: Guid.NewGuid(),
            AuthorId: userContext.Id,
            Distance: route.Distance,
            Time: route.Time,
            Path: JsonConvert.SerializeObject(route.Paths),
            Categories: [.. pointsOfInterest.Where(p => p.Category is not null).Select(p => p.Category)],
            Points: JsonConvert.SerializeObject(pointsOfInterest),
            CreatedAt: DateTimeOffset.UtcNow
        ), cancellationToken);

        return [
            new PathWithPoints(
                route.Distance,
                route.Time,
                route.Paths,
                [.. pointsOfInterest]
            )
        ];
    }
}