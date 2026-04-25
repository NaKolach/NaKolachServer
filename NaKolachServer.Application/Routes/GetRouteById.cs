using NaKolachServer.Domain.Points;
using NaKolachServer.Domain.Routes;

using Newtonsoft.Json;

namespace NaKolachServer.Application.Routes;

public class GetRouteById(IRoutesRepository routesRepository)
{
    public async Task<RouteResponse?> Execute(Guid id, CancellationToken cancellationToken)
    {
        var route = await routesRepository.GetRouteById(id, cancellationToken)
            ?? throw new RouteNotFoundException($"Route with id {id} not found.");

        return new RouteResponse(
            Id: Guid.NewGuid(),
            AuthorId: route.AuthorId,
            Distance: route.Distance,
            Time: route.Time,
            Paths: JsonConvert.DeserializeObject<double[][]>(route.Path),
            Categories: route.Categories,
            Points: JsonConvert.DeserializeObject<Point[]>(route.Points),
            CreatedAt: DateTimeOffset.UtcNow
        );
    }
}