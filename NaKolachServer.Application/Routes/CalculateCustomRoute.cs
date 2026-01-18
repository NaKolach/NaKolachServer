using NaKolachServer.Application.Utils;
using NaKolachServer.Domain.Points;
using NaKolachServer.Domain.Routes;
using NaKolachServer.Domain.Users;

using Newtonsoft.Json;

namespace NaKolachServer.Application.Routes;

public class CalculateCustomRoute(IPointsRepository pointsRepository, IRoutesRepository routesRepository, IRouteProvider routeProvider)
{
    public async Task<Route[]> Execute(UserContext userContext, CustomRouteSearchParams searchParams, CancellationToken cancellationToken)
    {
        var startPoint = CRSConverter.CRS4326to3857(searchParams.Longitude, searchParams.Latitude);

        var pointsOfInterest = new List<Point>();
        foreach (var id in searchParams.Points)
        {
            var pointOfInterest = await pointsRepository.GetPointById(id, cancellationToken)
                ?? throw new PointNotFoundException($"Point with id {id} not found.");

            var pointConverted = CRSConverter.CRS3857to4326(pointOfInterest.Longitude, pointOfInterest.Latitude);
            pointOfInterest = pointOfInterest with { Latitude = pointConverted.Y, Longitude = pointConverted.X };
            pointsOfInterest.Add(pointOfInterest);
        }

        var calculatedRoute = await routeProvider.CalculateRoute([
            new Coordinates(searchParams.Longitude, searchParams.Latitude),
            .. pointsOfInterest.Select(p => new Coordinates(p.Longitude, p.Latitude)),
            new Coordinates(searchParams.Longitude, searchParams.Latitude)],
         cancellationToken);

        var route = new Route(
            Id: Guid.NewGuid(),
            AuthorId: userContext.Id,
            Distance: calculatedRoute.Distance,
            Time: calculatedRoute.Time,
            Path: JsonConvert.SerializeObject(calculatedRoute.Paths),
            Categories: [.. pointsOfInterest.Where(p => p.Category is not null).Select(p => p.Category)],
            Points: JsonConvert.SerializeObject(pointsOfInterest),
            CreatedAt: DateTimeOffset.UtcNow
        );

        await routesRepository.InsertRoute(route, cancellationToken);

        return [route];
    }
}