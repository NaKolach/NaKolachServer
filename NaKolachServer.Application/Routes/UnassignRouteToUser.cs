using NaKolachServer.Domain.Routes;
using NaKolachServer.Domain.Users;

namespace NaKolachServer.Application.Routes;

public class UnassignAssignRouteToUser(IRoutesRepository routesRepository)
{
    public async Task Execute(UserContext userContext, Guid routeId, CancellationToken cancellationToken)
    {
        _ = await routesRepository.GetRouteById(routeId, cancellationToken)
            ?? throw new RouteNotFoundException($"Route with id {routeId} not found.");

        await routesRepository.RemoveUserRoute(new RouteUser(routeId, userContext.Id), cancellationToken);
    }
}