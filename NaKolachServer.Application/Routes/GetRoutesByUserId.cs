using NaKolachServer.Domain.Routes;

using Newtonsoft.Json;

namespace NaKolachServer.Application.Routes;

public class GetRoutesByUserId(IRoutesRepository routesRepository)
{
    public async Task<RouteData[]> Execute(Guid userId, CancellationToken cancellationToken)
    {
        return await routesRepository.GetRoutesByUserId(userId, cancellationToken);
    }
}