using NaKolachServer.Domain.Routes;

namespace NaKolachServer.Application.Routes;

public class GetRouteById(IRoutesRepository routesRepository)
{
    public async Task<Route?> Execute(Guid id, CancellationToken cancellationToken)
    {
        return await routesRepository.GetRouteById(id, cancellationToken)
            ?? throw new RouteNotFoundException($"Route with id {id} not found.");
    }
}