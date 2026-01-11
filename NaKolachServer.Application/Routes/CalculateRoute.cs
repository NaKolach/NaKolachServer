using NaKolachServer.Application.Utils;
using NaKolachServer.Domain.Points;
using NaKolachServer.Domain.Routes;

namespace NaKolachServer.Application.Routes;

public class CalculateRoute(IPointsRepository pointsRepository, IRouteProvider routeProvider)
{
    public async Task<RouteFullDetails> Execute(PointsSearchParams searchParams, CancellationToken cancellationToken)
    {
        var startPoint = CRSConverter.CRS4326to3857(searchParams.Longitude, searchParams.Latitude);

        var pointsOfInterest = new List<Point>();
        foreach (var category in searchParams.Categories)
        {
            var pointOfInterest = await pointsRepository.GetRandomPointByCategory(
                category, startPoint.X, startPoint.Y, searchParams.Radius, cancellationToken
            ) ?? throw new Exception("No OSM points found in database.");

            var pointConverted = CRSConverter.CRS3857to4326(pointOfInterest.Longitude, pointOfInterest.Latitude);
            pointOfInterest = pointOfInterest with { Latitude = pointConverted.Y, Longitude = pointConverted.X };
            pointsOfInterest.Add(pointOfInterest);
        }

        var route = await routeProvider.CalculateRoute([
            new Coordinates(searchParams.Longitude, searchParams.Latitude),
            .. pointsOfInterest.Select(p => new Coordinates(p.Longitude, p.Latitude)),
            new Coordinates(searchParams.Longitude, searchParams.Latitude)],
         cancellationToken);

        return new RouteFullDetails([
            new PathWithPoints(
                route.Distance,
                route.Time,
                route.Paths,
                [.. pointsOfInterest]
            )
        ]);
    }
}