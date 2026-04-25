using NaKolachServer.Domain.Routes;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Routes;

public class UpdateRouteName(IRoutesRepository routesRepository)
{
    public async Task Execute(UserContext userContext, Guid routeId, string name, CancellationToken cancellationToken)
    {
        _ = await routesRepository.GetRouteById(routeId, cancellationToken)
            ?? throw new RouteNotFoundException($"Route with id {routeId} not found.");

        await routesRepository.UpdateUserRouteName(routeId, userContext.Id, name, cancellationToken);
    }
}